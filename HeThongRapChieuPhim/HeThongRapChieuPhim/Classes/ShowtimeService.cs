using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongRapChieuPhim.Classes;

/// <summary>Validates and writes schedules atomically so concurrent admins cannot overlap a room's timetable.</summary>
public static class ShowtimeService
{
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
            DateTime endAt = startAt.AddMinutes(duration + 60);
            if (start < TimeSpan.FromHours(10) || endAt.Date != startAt.Date)
                throw new InvalidOperationException("Suất chiếu phải bắt đầu từ 10:00 và kết thúc/dọn phòng trước 00:00.");
            if (update)
            {
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
                  AND DATEADD(MINUTE, DATEDIFF(MINUTE, 0, sc.GioChieu) + p.ThoiLuong + 60, CAST(sc.NgayChieu AS datetime)) > @start
                  AND DATEADD(MINUTE, DATEDIFF(MINUTE, 0, @startTime) + @duration + 60, CAST(@day AS datetime)) >
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

            using var pricingCmd = new SqlCommand(@"
                SELECT pc.SoHangGhe, ISNULL(sc.GiaSuatChieu, p.GiaPhim)
                FROM dbo.SuatChieu sc
                JOIN dbo.PhongChieu pc ON pc.MaPhong=sc.MaPhong
                JOIN dbo.Phim p ON p.MaPhim=sc.MaPhim
                WHERE sc.MaSuatChieu=@id", connection, transaction);
            pricingCmd.Parameters.Add(DbArg.VarChar20("@id", showId).CreateSqlParameter());
            int rowCount;
            decimal baseFare;
            using (var pricingReader = pricingCmd.ExecuteReader())
            {
                if (!pricingReader.Read()) throw new InvalidOperationException("Không đọc được thông tin giá suất chiếu.");
                rowCount = pricingReader.GetInt32(0);
                baseFare = pricingReader.GetDecimal(1);
            }

            using (var deletePrices = new SqlCommand("DELETE FROM dbo.GiaVeHangGhe WHERE MaSuatChieu=@id", connection, transaction))
            {
                deletePrices.Parameters.Add(DbArg.VarChar20("@id", showId).CreateSqlParameter());
                deletePrices.ExecuteNonQuery();
            }

            for (int row = 0; row < rowCount; row++)
            {
                decimal factor = rowCount == 1 ? 1m : 0.8m + 0.4m * row / (rowCount - 1m);
                decimal rowFare = Math.Round(baseFare * factor, 0, MidpointRounding.AwayFromZero);
                using var insertPrice = new SqlCommand(
                    "INSERT INTO dbo.GiaVeHangGhe (MaSuatChieu, HangGhe, GiaVe) VALUES (@show,@row,@fare)",
                    connection, transaction);
                insertPrice.Parameters.Add(DbArg.VarChar20("@show", showId).CreateSqlParameter());
                insertPrice.Parameters.Add(DbArg.VarChar10("@row", ((char)('A' + row)).ToString()).CreateSqlParameter());
                insertPrice.Parameters.Add(DbArg.Decimal18_0("@fare", rowFare).CreateSqlParameter());
                insertPrice.ExecuteNonQuery();
            }
            return true;
        }, IsolationLevel.Serializable);
    }
}
