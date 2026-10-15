using System.Data;
using HeThongRapChieuPhim.Data;

namespace HeThongRapChieuPhim.Classes;

public sealed record FoodOrder(string MaDoAn, int SoLuong);
public sealed record SaleDraft(string MaSuatChieu, IReadOnlyList<string> Ghe, IReadOnlyList<FoodOrder> DoAn, string? MaCombo, string? MaVoucher, string TenDangNhapNguoiBan);
public sealed record SaleResult(
    string MaHD, long? PayOSOrderCode, DateTime NgayLap, string TenPhim, string TenPhong, string SuatChieu,
    string DanhSachGhe, int SoLuongVe, string TenCombo, string DanhSachDoAn,
    decimal TienVe, decimal TienGiamGia, decimal TienDoAn, decimal TongTien, string PhuongThucTT);

/// <summary>Business rules for calculating and storing sales. All SQL remains in the Core layer.</summary>
public static class SalesService
{
    public static SaleResult CreateCashSale(SaleDraft draft) => CreateSale(draft, pendingPayOs: false);
    public static SaleResult CreatePendingPayOsSale(SaleDraft draft) => CreateSale(draft, pendingPayOs: true);

    private static SaleResult CreateSale(SaleDraft draft, bool pendingPayOs)
    {
        var show = DataProcesser.Query(@"
            SELECT sc.MaSuatChieu, sc.NgayChieu, sc.GioChieu, GETDATE() AS ServerNow, pc.TenPhong, pc.SoHangGhe, pc.SoGheMoiHang,
                   p.TenPhim, p.ThoiLuong, ISNULL(sc.GiaSuatChieu, p.GiaPhim) AS GiaVe
            FROM dbo.SuatChieu sc
            JOIN dbo.PhongChieu pc ON pc.MaPhong = sc.MaPhong
            JOIN dbo.Phim p ON p.MaPhim = sc.MaPhim
            WHERE sc.MaSuatChieu = @show", new DbArg("@show", draft.MaSuatChieu));
        if (show.Rows.Count != 1) throw new InvalidOperationException("Suất chiếu không tồn tại.");
        var s = show.Rows[0];
        var showAt = Convert.ToDateTime(s["NgayChieu"]).Date.Add((TimeSpan)s["GioChieu"]);
        if (showAt <= Convert.ToDateTime(s["ServerNow"])) throw new InvalidOperationException("Không thể bán vé cho suất chiếu đã bắt đầu hoặc đã qua.");

        var seats = draft.Ghe.Select(x => x.Trim().ToUpperInvariant()).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x).ToArray();
        if (seats.Length == 0) throw new InvalidOperationException("Cần chọn ít nhất một ghế.");
        int rows = Convert.ToInt32(s["SoHangGhe"]), cols = Convert.ToInt32(s["SoGheMoiHang"]);
        foreach (var seat in seats)
        {
            if (seat.Length < 2 || seat[0] < 'A' || seat[0] >= 'A' + rows ||
                !int.TryParse(seat[1..], out int col) || col < 1 || col > cols || seat != $"{seat[0]}{col}")
                throw new InvalidOperationException($"Mã ghế {seat} không thuộc phòng chiếu.");
        }

        string comboName = string.Empty;
        int comboSeats = 0;
        decimal comboPct = 0;
        if (!string.IsNullOrWhiteSpace(draft.MaCombo))
        {
            var combo = DataProcesser.Query("SELECT TenCombo, SoLuongGhe, PhanTramGiam FROM dbo.UuDaiCombo WHERE MaCombo=@id", new DbArg("@id", draft.MaCombo));
            if (combo.Rows.Count != 1) throw new InvalidOperationException("Combo đã chọn không còn tồn tại.");
            comboName = Convert.ToString(combo.Rows[0]["TenCombo"]) ?? string.Empty;
            comboSeats = Convert.ToInt32(combo.Rows[0]["SoLuongGhe"]);
            comboPct = Convert.ToDecimal(combo.Rows[0]["PhanTramGiam"]);
            if (seats.Length != comboSeats) throw new InvalidOperationException($"Combo yêu cầu chọn đúng {comboSeats} ghế.");
        }

        var foodData = new List<(string Id, string Name, int Qty, decimal Price)>();
        foreach (var food in draft.DoAn)
        {
            if (food.SoLuong is < 1 or > 20) throw new InvalidOperationException("Số lượng mỗi món phải từ 1 đến 20.");
            var row = DataProcesser.Query("SELECT TenDoAn, Gia FROM dbo.DoAn WHERE MaDoAn=@id", new DbArg("@id", food.MaDoAn));
            if (row.Rows.Count != 1) throw new InvalidOperationException($"Món {food.MaDoAn} không còn tồn tại.");
            foodData.Add((food.MaDoAn, Convert.ToString(row.Rows[0]["TenDoAn"]) ?? food.MaDoAn, food.SoLuong, Convert.ToDecimal(row.Rows[0]["Gia"])));
        }

        string voucherName = string.Empty, voucherType = string.Empty;
        decimal voucherValue = 0;
        if (!string.IsNullOrWhiteSpace(draft.MaVoucher))
        {
            var voucher = DataProcesser.Query(@"
                SELECT TenVoucher, LoaiGiam, GiaTriGiam
                FROM dbo.Voucher v
                WHERE v.MaVoucher=@id AND v.TrangThai=1 AND v.HanSuDung>=CONVERT(date, GETDATE())
                  AND NOT EXISTS (SELECT 1 FROM dbo.VoucherClaim c WHERE c.MaVoucher=v.MaVoucher)",
                new DbArg("@id", draft.MaVoucher));
            if (voucher.Rows.Count != 1) throw new InvalidOperationException("Voucher không còn hiệu lực hoặc đã được giữ cho giao dịch khác.");
            voucherName = Convert.ToString(voucher.Rows[0]["TenVoucher"]) ?? string.Empty;
            voucherType = Convert.ToString(voucher.Rows[0]["LoaiGiam"]) ?? string.Empty;
            voucherValue = Convert.ToDecimal(voucher.Rows[0]["GiaTriGiam"]);
        }

        decimal ticketPrice = Convert.ToDecimal(s["GiaVe"]);
        decimal tickets = ticketPrice * seats.Length;
        decimal foodTotal = foodData.Sum(f => f.Price * f.Qty);
        decimal comboDiscount = comboSeats > 0 ? tickets * comboPct / 100m : 0;
        decimal voucherDiscount = 0;
        if (voucherType.Contains("phần trăm", StringComparison.OrdinalIgnoreCase))
            voucherDiscount = Math.Max(0, tickets - comboDiscount) * voucherValue / 100m;
        else if (voucherType.Contains("vé", StringComparison.OrdinalIgnoreCase))
            voucherDiscount = Math.Min((int)voucherValue, seats.Length) * ticketPrice;
        else if (!string.IsNullOrEmpty(voucherType))
            voucherDiscount = Math.Min(voucherValue, Math.Max(0, tickets - comboDiscount) + foodTotal);
        decimal totalDiscount = Math.Min(tickets + foodTotal, comboDiscount + voucherDiscount);
        decimal total = tickets + foodTotal - totalDiscount;
        long sequence = DataProcesser.NextSaleSequence();
        string maHD = $"HD{sequence:D18}";
        long? orderCode = pendingPayOs ? sequence : null;
        string seller = draft.TenDangNhapNguoiBan.Trim();
        if (seller.Length == 0) throw new InvalidOperationException("Không xác định được tài khoản thu ngân.");
        var sellerRows = DataProcesser.Query("SELECT MaNV,VaiTro FROM dbo.TaiKhoan WHERE TenDangNhap=@user AND TrangThai=1", new DbArg("@user", seller));
        if (sellerRows.Rows.Count != 1 || !new[] { "Cashier", "Thu ngân", "Nhân viên" }.Contains(Convert.ToString(sellerRows.Rows[0]["VaiTro"]), StringComparer.OrdinalIgnoreCase))
            throw new UnauthorizedAccessException("Chỉ tài khoản thu ngân đang hoạt động mới được tạo giao dịch.");
        object maNv = sellerRows.Rows[0]["MaNV"] is DBNull ? DBNull.Value : sellerRows.Rows[0]["MaNV"];

        string paymentState = pendingPayOs ? "Pending" : "Paid";
        var statements = new List<TransactionCommand>
        {
            new(@"INSERT INTO dbo.HoaDon
                (MaHD, NgayLap, MaNV, MaSuatChieu, MaCombo, MaVoucher, DanhSachGhe, SoLuongVe, TienVe, TienGiamGia, TienDoAn, TongTien, PhuongThucTT, GhiChu, TenDangNhapNguoiBan, TrangThaiThanhToan, PayOSOrderCode)
                VALUES (@bill, GETDATE(), @employee, @show, @combo, @voucher, @seats, @qty, @tickets, @discount, @food, @total, @method, @note, @seller, @state, @orderCode)",
                [new("@bill", maHD), new("@employee", maNv), new("@show", draft.MaSuatChieu), new("@combo", (object?)draft.MaCombo ?? DBNull.Value),
                 new("@voucher", (object?)draft.MaVoucher ?? DBNull.Value), new("@seats", string.Join(", ", seats)), new("@qty", seats.Length),
                 new("@tickets", Math.Round(tickets, 0)), new("@discount", Math.Round(totalDiscount, 0)), new("@food", Math.Round(foodTotal, 0)),
                 new("@total", Math.Round(total, 0)), new("@method", pendingPayOs ? "PayOS" : "Tiền mặt"),
                 new("@note", (object?)(!string.IsNullOrEmpty(draft.MaVoucher) ? $"Voucher {draft.MaVoucher}: {voucherName}" : null) ?? DBNull.Value),
                 new("@seller", seller), new("@state", paymentState), new("@orderCode", (object?)orderCode ?? DBNull.Value)]),
        };
        foreach (var seat in seats)
        {
            statements.Add(new TransactionCommand("INSERT INTO dbo.ChiTietHoaDon_Ghe (MaHD, MaSuatChieu, MaGhe) VALUES (@bill,@show,@seat)", [new("@bill", maHD), new("@show", draft.MaSuatChieu), new("@seat", seat)]));
            statements.Add(new TransactionCommand("INSERT INTO dbo.GheDatCho (MaSuatChieu,MaGhe,MaHD,HetHan) VALUES (@show,@seat,@bill,@expiry)", [new("@show", draft.MaSuatChieu), new("@seat", seat), new("@bill", maHD), new("@expiry", pendingPayOs ? DateTime.UtcNow.AddMinutes(15) : DBNull.Value)]));
        }
        foreach (var food in foodData)
            statements.Add(new TransactionCommand("INSERT INTO dbo.ChiTietHoaDon_DoAn (MaHD,MaDoAn,SoLuong,DonGia,ThanhTien) VALUES (@bill,@food,@qty,@price,@subtotal)",
                [new("@bill", maHD), new("@food", food.Id), new("@qty", food.Qty), new("@price", food.Price), new("@subtotal", food.Price * food.Qty)]));
        if (!string.IsNullOrWhiteSpace(draft.MaVoucher))
        {
            statements.Add(new TransactionCommand("INSERT INTO dbo.VoucherClaim(MaVoucher,MaHD,TrangThai,HetHan) VALUES(@voucher,@bill,@state,@expiry)",
                [new("@voucher", draft.MaVoucher), new("@bill", maHD), new("@state", pendingPayOs ? "Pending" : "Used"), new("@expiry", pendingPayOs ? DateTime.UtcNow.AddMinutes(15) : DBNull.Value)]));
            if (!pendingPayOs)
                statements.Add(new TransactionCommand("UPDATE dbo.Voucher SET TrangThai=0 WHERE MaVoucher=@voucher AND TrangThai=1 AND HanSuDung>=CONVERT(date,GETDATE())", [new("@voucher", draft.MaVoucher)], ExpectedRows: 1));
        }
        DataProcesser.ExecuteTransaction(statements, System.Data.IsolationLevel.Serializable);

        return new SaleResult(maHD, orderCode, DateTime.Now, Convert.ToString(s["TenPhim"]) ?? string.Empty,
            Convert.ToString(s["TenPhong"]) ?? string.Empty, Convert.ToDateTime(s["GioChieu"]).ToString("HH:mm"),
            string.Join(", ", seats), seats.Length, comboName,
            string.Join(", ", foodData.Select(f => $"{f.Name} (x{f.Qty})")), Math.Round(tickets, 0),
            Math.Round(totalDiscount, 0), Math.Round(foodTotal, 0), Math.Round(total, 0), pendingPayOs ? "Chuyển khoản PayOS" : "Tiền mặt");
    }

    public static void SetPayOsLink(long orderCode, string paymentLinkId)
    {
        int affected = DataProcesser.Execute("UPDATE dbo.HoaDon SET PayOSPaymentLinkId=@link WHERE PayOSOrderCode=@code AND TrangThaiThanhToan='Pending'",
            new DbArg("@link", paymentLinkId), new DbArg("@code", orderCode));
        if (affected != 1) throw new InvalidOperationException("Không thể gắn link PayOS với hóa đơn đang chờ.");
    }

    public static string GetPaymentStatus(long orderCode)
    {
        var value = DataProcesser.Scalar("SELECT TrangThaiThanhToan FROM dbo.HoaDon WHERE PayOSOrderCode=@code", new DbArg("@code", orderCode));
        return Convert.ToString(value) ?? throw new KeyNotFoundException("Không tìm thấy giao dịch.");
    }

    public static bool CompletePayOsPayment(long orderCode, int amount, string paymentLinkId)
    {
        var rows = DataProcesser.Query("SELECT MaHD,TongTien,TrangThaiThanhToan,PayOSPaymentLinkId FROM dbo.HoaDon WHERE PayOSOrderCode=@code", new DbArg("@code", orderCode));
        if (rows.Rows.Count != 1) throw new KeyNotFoundException("Không tìm thấy hóa đơn PayOS.");
        var row = rows.Rows[0];
        if (Convert.ToDecimal(row["TongTien"]) != amount) throw new InvalidOperationException("Số tiền webhook không khớp hóa đơn.");
        string savedLinkId = Convert.ToString(row["PayOSPaymentLinkId"]) ?? string.Empty;
        if (savedLinkId.Length == 0 || !string.Equals(savedLinkId, paymentLinkId, StringComparison.Ordinal))
            throw new InvalidOperationException("Mã payment link webhook không khớp hóa đơn.");
        if (Convert.ToString(row["TrangThaiThanhToan"]) == "Paid") return true;
        if (Convert.ToString(row["TrangThaiThanhToan"]) != "Pending") return false;
        try
        {
            DataProcesser.ExecuteTransaction([
                new TransactionCommand("UPDATE dbo.HoaDon SET TrangThaiThanhToan='Paid',GhiChu=CONCAT(ISNULL(GhiChu,''),' | PayOS đã thanh toán') WHERE PayOSOrderCode=@code AND TrangThaiThanhToan='Pending' AND TongTien=@amount AND PayOSPaymentLinkId=@link", [new("@code", orderCode), new("@amount", amount), new("@link", paymentLinkId)], 1),
                new TransactionCommand("UPDATE dbo.GheDatCho SET HetHan=NULL WHERE MaHD=(SELECT MaHD FROM dbo.HoaDon WHERE PayOSOrderCode=@code)", [new("@code", orderCode)]),
                new TransactionCommand("UPDATE v SET TrangThai=0 FROM dbo.Voucher v JOIN dbo.VoucherClaim c ON c.MaVoucher=v.MaVoucher JOIN dbo.HoaDon h ON h.MaHD=c.MaHD WHERE h.PayOSOrderCode=@code AND c.TrangThai='Pending'", [new("@code", orderCode)]),
                new TransactionCommand("UPDATE dbo.VoucherClaim SET TrangThai='Used',HetHan=NULL WHERE MaHD=(SELECT MaHD FROM dbo.HoaDon WHERE PayOSOrderCode=@code) AND TrangThai='Pending'", [new("@code", orderCode)])
            ], IsolationLevel.Serializable);
        }
        catch (InvalidOperationException)
        {
            if (GetPaymentStatus(orderCode) == "Paid") return true;
            throw;
        }
        return true;
    }

    public static bool CancelPendingPayOs(long orderCode, string reason)
    {
        var rows = DataProcesser.Query("SELECT MaHD FROM dbo.HoaDon WHERE PayOSOrderCode=@code AND TrangThaiThanhToan='Pending'", new DbArg("@code", orderCode));
        if (rows.Rows.Count == 0) return false;
        string maHD = Convert.ToString(rows.Rows[0]["MaHD"])!;
        DataProcesser.ExecuteTransaction([
            new TransactionCommand("UPDATE dbo.HoaDon SET TrangThaiThanhToan=@state,GhiChu=CONCAT(ISNULL(GhiChu,''),' | ',@reason) WHERE MaHD=@bill AND TrangThaiThanhToan='Pending'", [new("@state", reason.Contains("hết hạn", StringComparison.OrdinalIgnoreCase) ? "Expired" : "Cancelled"), new("@reason", reason), new("@bill", maHD)], 1),
            new TransactionCommand("DELETE FROM dbo.GheDatCho WHERE MaHD=@bill", [new("@bill", maHD)]),
            new TransactionCommand("DELETE FROM dbo.VoucherClaim WHERE MaHD=@bill AND TrangThai='Pending'", [new("@bill", maHD)])
        ], System.Data.IsolationLevel.Serializable);
        return true;
    }

    public static IReadOnlyList<long> GetExpiredPayOsOrders() => DataProcesser.Query(
        "SELECT PayOSOrderCode FROM dbo.HoaDon WHERE TrangThaiThanhToan='Pending' AND PayOSOrderCode IS NOT NULL AND NgayLap < DATEADD(MINUTE,-15,GETDATE())")
        .Rows.Cast<DataRow>().Select(r => Convert.ToInt64(r[0])).ToArray();

}
