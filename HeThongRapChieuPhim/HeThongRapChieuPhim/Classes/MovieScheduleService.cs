using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongRapChieuPhim.Classes;

public static class MovieScheduleService
{
    // The caller's detail update and these schedule checks share a transaction.
    public static void UpdateDetails(string movieId, int duration, DateTime releaseStart,
        DateTime releaseEnd, string updateSql, DbArg[] parameters)
    {
        DataProcesser.ExecuteInTransaction((connection, transaction) =>
        {
            using var check = new SqlCommand(@"
                DECLARE @oldDuration INT;
                SELECT @oldDuration=ThoiLuong FROM dbo.Phim WITH (UPDLOCK,HOLDLOCK) WHERE MaPhim=@movie;
                IF @oldDuration IS NULL THROW 51100, N'Không tìm thấy phim cần sửa.', 1;
                IF @oldDuration<>@duration AND EXISTS
                    (SELECT 1 FROM dbo.SuatChieu WITH (UPDLOCK,HOLDLOCK) WHERE MaPhim=@movie)
                    THROW 51101, N'Phim đã có lịch chiếu. Không thể đổi thời lượng vì sẽ ảnh hưởng lịch phòng và lịch sử suất chiếu.', 1;
                IF EXISTS (SELECT 1 FROM dbo.SuatChieu WITH (UPDLOCK,HOLDLOCK)
                    WHERE MaPhim=@movie AND NgayChieu>=CONVERT(date,GETDATE())
                      AND (NgayChieu<@from OR NgayChieu>@to))
                    THROW 51102, N'Thời hạn phát hành mới loại bỏ suất chiếu đã xếp. Hãy điều chỉnh lịch chiếu trước.', 1;",
                connection, transaction);
            check.Parameters.Add(DbArg.VarChar20("@movie", movieId).CreateSqlParameter());
            check.Parameters.Add(DbArg.Int("@duration", duration).CreateSqlParameter());
            check.Parameters.Add(DbArg.Date("@from", releaseStart).CreateSqlParameter());
            check.Parameters.Add(DbArg.Date("@to", releaseEnd).CreateSqlParameter());
            check.ExecuteNonQuery();
            using var update = new SqlCommand(updateSql, connection, transaction);
            foreach (var parameter in parameters) update.Parameters.Add(parameter.CreateSqlParameter());
            if (update.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không tìm thấy phim cần sửa.");
            return true;
        }, IsolationLevel.Serializable);
    }
}
