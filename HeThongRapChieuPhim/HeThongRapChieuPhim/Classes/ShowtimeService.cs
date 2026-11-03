using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongRapChieuPhim.Classes;

/// <summary>Validates and writes schedules atomically so concurrent admins cannot overlap a room's timetable.</summary>
public static class ShowtimeService
{
    public const int CleaningMinutes = 60;
    public static readonly TimeSpan OpeningTime = TimeSpan.FromHours(10);
    public static readonly TimeSpan ClosingTime = TimeSpan.FromHours(24);

    public static bool IsWithinOperatingHours(TimeSpan start, int duration) =>
        duration > 0 && start >= OpeningTime && start < ClosingTime
        && start.Add(TimeSpan.FromMinutes(duration + CleaningMinutes)) <= ClosingTime;

    public static bool Overlaps(TimeSpan start, TimeSpan cleanEnd, TimeSpan otherStart, TimeSpan otherCleanEnd) =>
        start < otherCleanEnd && otherStart < cleanEnd;

    public static void Save(string showId, string movieId, string roomId, DateTime day, TimeSpan start, decimal price, bool update)
    {
        if (string.IsNullOrWhiteSpace(showId) || string.IsNullOrWhiteSpace(movieId) || string.IsNullOrWhiteSpace(roomId))
            throw new InvalidOperationException("Mã suất, phim và phòng chiếu là bắt buộc.");
        DateTime startAt = day.Date.Add(start);
        if (startAt <= DateTime.Now) throw new InvalidOperationException("Không thể tạo hoặc sửa suất chiếu trong quá khứ.");

        DataProcesser.ExecuteInTransaction((connection, transaction) =>
        {
            using (var clockCmd = new SqlCommand("SELECT GETDATE()", connection, transaction))
            {
                if (startAt <= Convert.ToDateTime(clockCmd.ExecuteScalar()))
                    throw new InvalidOperationException("Không thể tạo hoặc sửa suất chiếu trong quá khứ.");
            }
            using var durationCmd = new SqlCommand(@"
                SELECT ThoiLuong, NgayBatDauChieu, NgayKetThucChieu
                FROM dbo.Phim WITH (UPDLOCK,HOLDLOCK) WHERE MaPhim=@movie", connection, transaction);
            durationCmd.Parameters.Add(DbArg.VarChar20("@movie", movieId).CreateSqlParameter());
            using var movieReader = durationCmd.ExecuteReader();
            if (!movieReader.Read()) throw new InvalidOperationException("Không tìm thấy phim đã chọn.");
            int duration = movieReader.GetInt32(0);
            DateTime? releaseStart = movieReader.IsDBNull(1) ? null : movieReader.GetDateTime(1).Date;
            DateTime? releaseEnd = movieReader.IsDBNull(2) ? null : movieReader.GetDateTime(2).Date;
            movieReader.Close();
            if (releaseStart.HasValue && day.Date < releaseStart.Value)
                throw new InvalidOperationException($"Chưa đến ngày phát hành của phim ({releaseStart:dd/MM/yyyy}).");
            if (releaseEnd.HasValue && day.Date > releaseEnd.Value)
                throw new InvalidOperationException($"Phim đã hết hạn phát hành từ ngày {releaseEnd:dd/MM/yyyy}. Hãy gia hạn phim trước khi xếp lịch.");
            if (price < 0) throw new InvalidOperationException("Giá vé không được âm.");
            if (!IsWithinOperatingHours(start, duration))
                throw new InvalidOperationException("Suất chiếu phải bắt đầu từ 10:00 và kết thúc/dọn phòng trước 00:00.");
            if (update)
            {
                using var oldShow = new SqlCommand(@"
                    SELECT CASE WHEN DATEADD(SECOND, DATEDIFF(SECOND,CONVERT(time,'00:00'),GioChieu),CAST(NgayChieu AS datetime2))
                        > SYSDATETIME() THEN 1 ELSE 0 END
                    FROM dbo.SuatChieu WITH (UPDLOCK,HOLDLOCK) WHERE MaSuatChieu=@id", connection, transaction);
                oldShow.Parameters.Add(DbArg.VarChar20("@id", showId).CreateSqlParameter());
                object? originalStart = oldShow.ExecuteScalar();
                if (originalStart == null) throw new InvalidOperationException("Không tìm thấy suất chiếu cần sửa.");
                if (Convert.ToInt32(originalStart) != 1)
                    throw new InvalidOperationException("Không thể sửa suất chiếu đã bắt đầu.");
                using var salesCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.HoaDon WITH (UPDLOCK,HOLDLOCK) WHERE MaSuatChieu=@id AND TrangThaiThanhToan IN ('Paid','Pending')", connection, transaction);
                salesCmd.Parameters.Add(DbArg.VarChar20("@id", showId).CreateSqlParameter());
                if (Convert.ToInt32(salesCmd.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("Không thể sửa suất chiếu đã có giao dịch hoặc ghế đang được giữ.");
            }

            const string conflictSql = @"
                SELECT TOP (1) sc.MaSuatChieu, p.TenPhim, sc.GioChieu, p.ThoiLuong
                FROM dbo.SuatChieu sc WITH (UPDLOCK,HOLDLOCK)
                JOIN dbo.Phim p ON p.MaPhim=sc.MaPhim
                WHERE sc.MaPhong=@room AND sc.NgayChieu=@day
                  AND (@update=0 OR sc.MaSuatChieu<>@id)
                  AND DATEADD(MINUTE, DATEDIFF(MINUTE, 0, sc.GioChieu) + p.ThoiLuong + @clean, CAST(sc.NgayChieu AS datetime)) > @start
                  AND DATEADD(MINUTE, DATEDIFF(MINUTE, 0, @startTime) + @duration + @clean, CAST(@day AS datetime)) >
                      DATEADD(MINUTE, DATEDIFF(MINUTE, 0, sc.GioChieu), CAST(sc.NgayChieu AS datetime))
                ORDER BY sc.GioChieu";
            using var conflictCmd = new SqlCommand(conflictSql, connection, transaction);
            conflictCmd.Parameters.Add(DbArg.VarChar20("@room", roomId).CreateSqlParameter());
            conflictCmd.Parameters.Add(DbArg.Date("@day", day.Date).CreateSqlParameter());
            conflictCmd.Parameters.Add(DbArg.Bit("@update", update).CreateSqlParameter());
            conflictCmd.Parameters.Add(DbArg.VarChar20("@id", showId).CreateSqlParameter());
            conflictCmd.Parameters.Add(DbArg.DateTime("@start", startAt).CreateSqlParameter());
            conflictCmd.Parameters.Add(DbArg.Time("@startTime", start).CreateSqlParameter());
            conflictCmd.Parameters.Add(DbArg.Int("@duration", duration).CreateSqlParameter());
            conflictCmd.Parameters.Add(DbArg.Int("@clean", CleaningMinutes).CreateSqlParameter());
            using (var reader = conflictCmd.ExecuteReader())
            {
                if (reader.Read()) throw new InvalidOperationException($"Xung đột lịch với suất {reader.GetString(0)} ({reader.GetString(1)}). Cần chừa 60 phút dọn phòng.");
            }

            string sql = update
                ? "UPDATE dbo.SuatChieu SET MaPhim=@movie,MaPhong=@room,NgayChieu=@day,GioChieu=@time,GiaSuatChieu=@price WHERE MaSuatChieu=@id"
                : "INSERT INTO dbo.SuatChieu(MaSuatChieu,MaPhim,MaPhong,NgayChieu,GioChieu,GiaSuatChieu) VALUES(@id,@movie,@room,@day,@time,@price)";
            using var saveCmd = new SqlCommand(sql, connection, transaction);
            saveCmd.Parameters.Add(DbArg.VarChar20("@id", showId).CreateSqlParameter());
            saveCmd.Parameters.Add(DbArg.VarChar20("@movie", movieId).CreateSqlParameter());
            saveCmd.Parameters.Add(DbArg.VarChar20("@room", roomId).CreateSqlParameter());
            saveCmd.Parameters.Add(DbArg.Date("@day", day.Date).CreateSqlParameter());
            saveCmd.Parameters.Add(DbArg.Time("@time", start).CreateSqlParameter());
            saveCmd.Parameters.Add(DbArg.Decimal18_0("@price", price).CreateSqlParameter());
            if (saveCmd.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không tìm thấy suất chiếu cần cập nhật.");

            return true;
        }, IsolationLevel.Serializable);
    }

    public static void Delete(string showId)
    {
        DataProcesser.ExecuteInTransaction((connection, transaction) =>
        {
            using var command = new SqlCommand(@"
                DECLARE @start datetime2;
                SELECT @start=DATEADD(SECOND,DATEDIFF(SECOND,CONVERT(time,'00:00'),GioChieu),CAST(NgayChieu AS datetime2))
                FROM dbo.SuatChieu WITH (UPDLOCK,HOLDLOCK) WHERE MaSuatChieu=@id;
                IF @start IS NULL THROW 51103,N'Không tìm thấy suất chiếu.',1;
                IF @start<=SYSDATETIME() THROW 51104,N'Không thể xóa suất chiếu đã bắt đầu.',1;
                IF EXISTS (SELECT 1 FROM dbo.HoaDon WITH (UPDLOCK,HOLDLOCK) WHERE MaSuatChieu=@id)
                    THROW 51105,N'Suất chiếu đã có hóa đơn. Cần giữ lịch sử giao dịch, không thể xóa.',1;
                DELETE FROM dbo.SuatChieu WHERE MaSuatChieu=@id;", connection, transaction);
            command.Parameters.Add(DbArg.VarChar20("@id", showId).CreateSqlParameter());
            command.ExecuteNonQuery();
            return true;
        }, IsolationLevel.Serializable);
    }
}
