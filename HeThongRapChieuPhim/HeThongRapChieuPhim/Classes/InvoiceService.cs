using System.Data;

namespace HeThongRapChieuPhim.Classes;

public sealed record InvoicePrintData(DataRow Header, string FoodDescription);

public static class InvoiceService
{
    public static DataTable Search(string keyword, bool filterDate, DateTime from, DateTime toExclusive) => DataProcesser.Query(@"
        SELECT hd.MaHD AS [Mã HĐ], CONVERT(VARCHAR(19),hd.NgayLap,120) AS [Thời Gian Xuất], hd.MaNV AS [Mã NV Bán],
               COALESCE(hd.TenDangNhapNguoiBan,nv.HoTen,N'Không rõ') AS [Tài Khoản Thu Ngân],
               p.TenPhim AS [Tên Phim], pc.TenPhong AS [Phòng], sc.GioChieu AS [Suất Chiếu], hd.DanhSachGhe AS [Ghế Ngồi],
               hd.SoLuongVe AS [SL Vé], hd.TienVe AS [Tiền Vé], hd.TienGiamGia AS [Giảm Giá], hd.TienDoAn AS [Tiền F&B],
               hd.TongTien AS [Tổng Thanh Toán], hd.PhuongThucTT AS [Hình Thức TT], hd.TrangThaiThanhToan AS [Trạng Thái Thanh Toán]
        FROM dbo.HoaDon hd
        LEFT JOIN dbo.NhanVien nv ON hd.MaNV=nv.MaNV
        LEFT JOIN dbo.SuatChieu sc ON hd.MaSuatChieu=sc.MaSuatChieu
        LEFT JOIN dbo.Phim p ON sc.MaPhim=p.MaPhim
        LEFT JOIN dbo.PhongChieu pc ON sc.MaPhong=pc.MaPhong
        WHERE (@kw='' OR hd.MaHD LIKE '%'+@kw+'%' OR p.TenPhim LIKE '%'+@kw+'%' OR hd.DanhSachGhe LIKE '%'+@kw+'%')
          AND (@filterDate=0 OR (hd.NgayLap>=@from AND hd.NgayLap<@to))
        ORDER BY hd.NgayLap DESC",
        DbArg.NVarChar4000("@kw", keyword), DbArg.Bit("@filterDate", filterDate), DbArg.DateTime("@from", from), DbArg.DateTime("@to", toExclusive));

    public static InvoicePrintData? GetPrintData(string billId)
    {
        var header = DataProcesser.Query(@"
            SELECT TOP 1 hd.MaHD,hd.NgayLap,p.TenPhim,pc.TenPhong,sc.NgayChieu,sc.GioChieu,hd.DanhSachGhe,hd.SoLuongVe,
                   ISNULL(cb.TenCombo,N'') AS TenCombo,hd.TienVe,hd.TienGiamGia,hd.TienDoAn,hd.TongTien,hd.PhuongThucTT,hd.TrangThaiThanhToan
            FROM dbo.HoaDon hd LEFT JOIN dbo.SuatChieu sc ON hd.MaSuatChieu=sc.MaSuatChieu
            LEFT JOIN dbo.Phim p ON sc.MaPhim=p.MaPhim LEFT JOIN dbo.PhongChieu pc ON sc.MaPhong=pc.MaPhong
            LEFT JOIN dbo.UuDaiCombo cb ON hd.MaCombo=cb.MaCombo WHERE hd.MaHD=@id AND hd.TrangThaiThanhToan='Paid'",
            DbArg.VarChar20("@id", billId));
        if (header.Rows.Count == 0) return null;
        var foods = DataProcesser.Query(@"
            SELECT da.TenDoAn, kc.TenKichCo, ct.MaKichCo, ct.SoLuong
            FROM dbo.ChiTietHoaDon_DoAn ct
            JOIN dbo.DoAn da ON ct.MaDoAn=da.MaDoAn
            LEFT JOIN dbo.DoAnKichCo kc ON kc.MaDoAn=ct.MaDoAn AND kc.MaKichCo=ct.MaKichCo
            WHERE ct.MaHD=@id",
            DbArg.VarChar20("@id", billId));
        string foodDescription = string.Join(", ", foods.Rows.Cast<DataRow>().Select(r =>
        {
            string foodName = Convert.ToString(r["TenDoAn"]) ?? string.Empty;
            string sizeName = r["TenKichCo"] is DBNull ? string.Empty : Convert.ToString(r["TenKichCo"]) ?? string.Empty;
            const string mediumSuffix = " cỡ vừa";
            if (!string.IsNullOrWhiteSpace(sizeName) && foodName.EndsWith(mediumSuffix, StringComparison.OrdinalIgnoreCase))
                foodName = foodName[..^mediumSuffix.Length].TrimEnd();
            string sizeSuffix = string.IsNullOrWhiteSpace(sizeName) ? string.Empty : $" - {sizeName}";
            return $"{foodName}{sizeSuffix} (x{r["SoLuong"]})";
        }));
        return new InvoicePrintData(header.Rows[0], foodDescription);
    }
}
