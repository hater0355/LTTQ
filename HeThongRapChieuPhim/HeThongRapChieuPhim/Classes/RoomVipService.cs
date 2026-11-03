using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongRapChieuPhim.Classes;

public sealed record RoomVipSettings(string Rows, decimal Percent)
{
    public bool IsVip(string row) => Rows.Split(',', StringSplitOptions.RemoveEmptyEntries)
        .Contains(row, StringComparer.OrdinalIgnoreCase);
    public decimal Price(decimal basePrice, string row) => Math.Round(
        basePrice * (IsVip(row) ? 1 + Percent / 100m : 1), 0, MidpointRounding.AwayFromZero);
}

public static class RoomVipService
{
    public const string SchemaSql = @"
        IF OBJECT_ID(N'dbo.PhongChieuVip',N'U') IS NULL
        CREATE TABLE dbo.PhongChieuVip (
            MaPhong VARCHAR(20) NOT NULL PRIMARY KEY REFERENCES dbo.PhongChieu(MaPhong) ON DELETE CASCADE,
            HangVip VARCHAR(51) NOT NULL DEFAULT '',
            PhanTramTang DECIMAL(7,2) NOT NULL DEFAULT 0 CHECK (PhanTramTang BETWEEN 0 AND 500)
        );";

    public static RoomVipSettings Get(string roomId)
    {
        EnsureSchema();
        var data = DataProcesser.Query("SELECT HangVip,PhanTramTang FROM dbo.PhongChieuVip WHERE MaPhong=@id",
            DbArg.VarChar20("@id", roomId));
        return data.Rows.Count == 0 ? new("", 0) : new(
            Convert.ToString(data.Rows[0]["HangVip"]) ?? "", Convert.ToDecimal(data.Rows[0]["PhanTramTang"]));
    }

    public static void EnsureSchema() => DataProcesser.Execute(SchemaSql);

    public static void SaveRoom(string id, string name, int rows, int columns, RoomVipSettings vip, bool update)
    {
        if (rows is < 1 or > 26 || columns is < 1 or > 30 || vip.Percent is < 0 or > 500)
            throw new InvalidOperationException("Số ghế hoặc phần trăm tăng giá không hợp lệ.");
        string[] selectedRows = vip.Rows.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (selectedRows.Any(row => row.Length != 1 || row[0] < 'A' || row[0] >= 'A' + rows))
            throw new InvalidOperationException("Hàng VIP phải nằm trong số hàng của phòng.");
        EnsureSchema();
        DataProcesser.ExecuteInTransaction((connection, transaction) =>
        {
            using var command = new SqlCommand(@"
                IF @update=1 AND EXISTS (SELECT 1 FROM dbo.PhongChieu WITH (UPDLOCK,HOLDLOCK)
                    WHERE MaPhong=@id AND (SoHangGhe<>@rows OR SoGheMoiHang<>@cols))
                    AND EXISTS (SELECT 1 FROM dbo.SuatChieu WITH (UPDLOCK,HOLDLOCK) WHERE MaPhong=@id)
                    THROW 51110,N'Phòng đã có lịch chiếu. Không thể đổi số hàng/cột ghế; vẫn có thể chỉnh hàng VIP và phụ thu.',1;
                IF @update=1
                    UPDATE dbo.PhongChieu SET TenPhong=@name,SoHangGhe=@rows,SoGheMoiHang=@cols WHERE MaPhong=@id;
                ELSE
                    INSERT dbo.PhongChieu(MaPhong,TenPhong,SoHangGhe,SoGheMoiHang) VALUES(@id,@name,@rows,@cols);
                IF NOT EXISTS (SELECT 1 FROM dbo.PhongChieu WHERE MaPhong=@id)
                    THROW 51111,N'Không tìm thấy phòng cần sửa.',1;
                UPDATE dbo.PhongChieuVip SET HangVip=@vip,PhanTramTang=@pct WHERE MaPhong=@id;
                IF @@ROWCOUNT=0 INSERT dbo.PhongChieuVip(MaPhong,HangVip,PhanTramTang) VALUES(@id,@vip,@pct);",
                connection, transaction);
            foreach (var arg in new[] { DbArg.VarChar20("@id", id), DbArg.NVarChar100("@name", name),
                DbArg.Int("@rows", rows), DbArg.Int("@cols", columns), DbArg.Bit("@update", update),
                DbArg.VarChar("@vip", string.Join(",", selectedRows.Distinct().Order()), 51),
                DbArg.Decimal("@pct", vip.Percent, 7, 2) }) command.Parameters.Add(arg.CreateSqlParameter());
            command.ExecuteNonQuery();
            return true;
        }, IsolationLevel.Serializable);
    }
}
