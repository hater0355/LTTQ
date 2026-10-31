using HeThongRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.Forms;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucBanHang : UserControl
    {
        private sealed record SelectedFoodItem(
            string MaDoAn, string MaKichCo, string TenDoAn, string TenKichCo, decimal DonGia, int SoLuong);

        // State tracking
        private string _currentMaPhim = "";
        private string _currentMaSuatChieu = "";
        private string _currentMaPhong = "";
        private decimal _currentGiaVe = 80000;
        private int _roomRows = 6;
        private int _roomCols = 10;
        private readonly HashSet<string> _bookedSeats = new();
        private readonly HashSet<string> _selectedSeats = new();
        private readonly Dictionary<string, decimal> _rowTicketPrices = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, SelectedFoodItem> _selectedFood = new(StringComparer.OrdinalIgnoreCase);
        private readonly List<FoodItemCard> _foodCards = new();
        private bool _loadingMovies;
        private int _lastAvailableMovieIndex = -1;
        private int _requiredComboSeats = 0;
        private decimal _comboDiscountPct = 0;
        private string _appliedVoucherCode = "";
        private string _appliedVoucherName = "";
        private string _appliedVoucherType = "";
        private decimal _appliedVoucherValue = 0;

        public ucBanHang()
        {
            InitializeComponent();
            flpFoodItems.SizeChanged += (_, _) => ResizeFoodCards();
            cboPhim.DrawMode = DrawMode.OwnerDrawFixed;
            cboPhim.ItemHeight = cboPhim.Font.Height + 6;
            cboPhim.DrawItem += CboPhim_DrawItem;
            cboPhim.SelectionChangeCommitted += CboPhim_SelectionChangeCommitted;
            ConfigureSeatGrid();
            pnlSeatMap.CellClick += PnlSeatMap_CellClick;
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadData();
            }
        }

        private void BtnShowQR_Click(object? sender, EventArgs e)
        {
            if (radTransfer.Checked) BtnConfirm_Click(sender, e);
        }

        private void RadTransfer_CheckedChanged(object? sender, EventArgs e)
        {
            btnShowQR.Visible = radTransfer.Checked;
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            ResetSelection();
        }

        private void TxtVoucherCode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApDungVoucher();
            }
        }

        private void BtnApDungVoucher_Click(object? sender, EventArgs e)
        {
            ApDungVoucher();
        }

        private void BtnHuyVoucher_Click(object? sender, EventArgs e)
        {
            HuyVoucher();
        }

        private void ApDungVoucher()
        {
            string code = txtVoucherCode.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Vui lòng nhập mã voucher khuyến mãi!", "Chưa nhập mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVoucherCode.Focus();
                return;
            }

            try
            {
                string sql = "SELECT MaVoucher, TenVoucher, LoaiGiam, GiaTriGiam, HanSuDung, TrangThai FROM Voucher WHERE UPPER(MaVoucher) = @Ma";
                var dt = Function.Query(sql, new[] { DbArg.VarChar20("@Ma", code) });

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"Mã voucher '{code}' không tồn tại trong hệ thống!\nVui lòng kiểm tra và nhập lại mã chính xác.",
                        "Mã Voucher Không Hợp Lệ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtVoucherCode.SelectAll();
                    txtVoucherCode.Focus();
                    HuyVoucher(silent: true);
                    return;
                }

                var row = dt.Rows[0];
                bool trangThai = Convert.ToBoolean(row["TrangThai"]);
                if (!trangThai)
                {
                    MessageBox.Show(
                        $"Voucher '{code}' đã được sử dụng hoặc đang bị tạm khóa, không thể dùng lại!\nVui lòng nhập mã khác.",
                        "Voucher Đã Sử Dụng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtVoucherCode.SelectAll();
                    txtVoucherCode.Focus();
                    HuyVoucher(silent: true);
                    return;
                }

                DateTime hanSuDung = Convert.ToDateTime(row["HanSuDung"]);
                if (hanSuDung.Date < DateTime.Today)
                {
                    MessageBox.Show(
                        $"Voucher '{code}' đã hết hạn sử dụng vào ngày {hanSuDung:dd/MM/yyyy}!\nVui lòng chọn voucher khác.",
                        "Voucher Hết Hạn",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtVoucherCode.SelectAll();
                    txtVoucherCode.Focus();
                    HuyVoucher(silent: true);
                    return;
                }

                // Áp dụng thành công
                _appliedVoucherCode = row["MaVoucher"].ToString() ?? code;
                _appliedVoucherName = row["TenVoucher"].ToString() ?? "";
                _appliedVoucherType = row["LoaiGiam"].ToString() ?? "";
                _appliedVoucherValue = Convert.ToDecimal(row["GiaTriGiam"]);

                txtVoucherCode.Text = _appliedVoucherCode;
                txtVoucherCode.Enabled = false;
                btnApDungVoucher.Visible = false;
                btnHuyVoucher.Visible = true;

                lblVoucherStatus.ForeColor = Color.FromArgb(16, 185, 129);
                lblVoucherStatus.Text = $"✅ Đã nhận: {_appliedVoucherName}";

                CalculateTotals();

                string moTaGiam = _appliedVoucherType.Contains("phần trăm", StringComparison.OrdinalIgnoreCase)
                    ? $"{_appliedVoucherValue:0.#}% giá vé"
                    : (_appliedVoucherType.Contains("vé", StringComparison.OrdinalIgnoreCase)
                        ? $"{_appliedVoucherValue:0} vé phim"
                        : $"{_appliedVoucherValue:N0} VNĐ");

                MessageBox.Show(
                    $"Áp dụng thành công voucher '{_appliedVoucherCode}'!\n\n" +
                    $"• Ưu đãi: {_appliedVoucherName}\n" +
                    $"• Hình thức: {_appliedVoucherType}\n" +
                    $"• Mức giảm: {moTaGiam}",
                    "Áp dụng voucher thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra voucher: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HuyVoucher(bool silent = false)
        {
            _appliedVoucherCode = "";
            _appliedVoucherName = "";
            _appliedVoucherType = "";
            _appliedVoucherValue = 0;

            txtVoucherCode.Enabled = true;
            txtVoucherCode.Clear();
            btnApDungVoucher.Visible = true;
            btnHuyVoucher.Visible = false;
            lblVoucherStatus.ForeColor = ThemeHelper.TextMuted;
            lblVoucherStatus.Text = "Nhập mã voucher để được giảm thêm";

            CalculateTotals();
            if (!silent)
            {
                txtVoucherCode.Focus();
            }
        }

        public void LoadData()
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            lblCashierInfo.Text = $"👤 Nhân viên: {CurrentUser.HoTen} (Mã: {CurrentUser.MaNV})";
            LoadMovies();
            LoadCombos();
            LoadFoods();
        }

        private void LoadMovies()
        {
            string previouslySelectedMovie = _currentMaPhim;
            int selectedIndex = -1;
            try
            {
                var dt = Function.Query(@"
                    SELECT p.MaPhim, p.TenPhim, p.TheLoai, p.DoTuoi, p.ThoiLuong, p.GiaPhim,
                           ISNULL(p.MoTa, '') AS MoTa, p.PosterPath,
                           CAST(CASE WHEN EXISTS (
                               SELECT 1 FROM dbo.SuatChieu sc
                               WHERE sc.MaPhim = p.MaPhim
                                 AND (p.NgayBatDauChieu IS NULL OR sc.NgayChieu >= p.NgayBatDauChieu)
                                 AND (p.NgayKetThucChieu IS NULL OR sc.NgayChieu <= p.NgayKetThucChieu)
                                 AND (sc.NgayChieu > CONVERT(date, GETDATE())
                                      OR (sc.NgayChieu = CONVERT(date, GETDATE())
                                          AND sc.GioChieu > CONVERT(time, GETDATE())))
                           ) THEN 1 ELSE 0 END AS bit) AS CoSuatKhaDung
                    FROM dbo.Phim p
                    WHERE EXISTS (
                        SELECT 1 FROM dbo.SuatChieu sc
                        WHERE sc.MaPhim = p.MaPhim
                          AND (p.NgayBatDauChieu IS NULL OR sc.NgayChieu >= p.NgayBatDauChieu)
                          AND (p.NgayKetThucChieu IS NULL OR sc.NgayChieu <= p.NgayKetThucChieu)
                          AND sc.NgayChieu >= CONVERT(date, GETDATE())
                    )
                    ORDER BY p.TenPhim ASC");

                _loadingMovies = true;
                cboPhim.DataSource = dt;
                cboPhim.DisplayMember = "TenPhim";
                cboPhim.ValueMember = "MaPhim";

                int firstAvailableIndex = -1;
                int previousAvailableIndex = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.DefaultView[i];
                    if (!HasSaleableShowtimes(row)) continue;
                    if (firstAvailableIndex < 0) firstAvailableIndex = i;
                    if (string.Equals(Convert.ToString(row["MaPhim"]), previouslySelectedMovie, StringComparison.OrdinalIgnoreCase))
                        previousAvailableIndex = i;
                }

                selectedIndex = previousAvailableIndex >= 0 ? previousAvailableIndex : firstAvailableIndex;
                cboPhim.SelectedIndex = selectedIndex;
                _lastAvailableMovieIndex = selectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục phim: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingMovies = false;
            }

            if (selectedIndex >= 0 && cboPhim.SelectedItem is DataRowView selectedMovie)
            {
                string selectedMovieId = Convert.ToString(selectedMovie["MaPhim"]) ?? string.Empty;
                if (string.Equals(selectedMovieId, previouslySelectedMovie, StringComparison.OrdinalIgnoreCase))
                    LoadShowtimesForMovie(selectedMovieId);
                else
                    CboPhim_SelectedIndexChanged(cboPhim, EventArgs.Empty);
            }
            else
            {
                ClearMovieSelection();
            }
        }

        private static bool HasSaleableShowtimes(DataRowView movie) =>
            movie.Row.Table.Columns.Contains("CoSuatKhaDung") &&
            movie["CoSuatKhaDung"] is not DBNull && Convert.ToBoolean(movie["CoSuatKhaDung"]);

        private void CboPhim_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= cboPhim.Items.Count)
            {
                e.DrawBackground();
                return;
            }

            var movie = cboPhim.Items[e.Index] as DataRowView;
            bool available = movie is not null && HasSaleableShowtimes(movie);
            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            e.Graphics.FillRectangle(selected ? SystemBrushes.Highlight : SystemBrushes.Window, e.Bounds);

            string name = movie is null ? string.Empty : Convert.ToString(movie["TenPhim"]) ?? string.Empty;
            if (!available) name += " (đã hết suất)";
            Color textColor = !available
                ? SystemColors.GrayText
                : selected ? SystemColors.HighlightText : SystemColors.WindowText;
            TextRenderer.DrawText(e.Graphics, name, cboPhim.Font, e.Bounds, textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            e.DrawFocusRectangle();
        }

        private void CboPhim_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            if (cboPhim.SelectedItem is DataRowView movie && !HasSaleableShowtimes(movie))
                cboPhim.SelectedIndex = _lastAvailableMovieIndex;
        }

        private void ClearMovieSelection()
        {
            _currentMaPhim = string.Empty;
            _currentMaSuatChieu = string.Empty;
            _currentMaPhong = string.Empty;
            _currentGiaVe = 0;
            _rowTicketPrices.Clear();
            _bookedSeats.Clear();
            _selectedSeats.Clear();
            cboSuatChieu.DataSource = null;
            lblMovieInfo.Text = "Không có phim với suất chiếu hôm nay hoặc sắp tới.";
            lblRoomInfo.Text = "Không có suất chiếu còn bán.";
            LoadPoster(string.Empty);
            RenderSeatMap();
            CalculateTotals();
        }

        private void LoadCombos()
        {
            try
            {
                var dt = Function.Query("SELECT MaCombo, TenCombo, SoLuongGhe, PhanTramGiam FROM UuDaiCombo ORDER BY MaCombo ASC");
                var rowNone = dt.NewRow();
                rowNone["MaCombo"] = "";
                rowNone["TenCombo"] = "-- Không áp dụng combo --";
                rowNone["SoLuongGhe"] = 0;
                rowNone["PhanTramGiam"] = 0;
                dt.Rows.InsertAt(rowNone, 0);

                cboCombo.DataSource = dt;
                cboCombo.DisplayMember = "TenCombo";
                cboCombo.ValueMember = "MaCombo";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải combo ưu đãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFoods()
        {
            try
            {
                foreach (var card in _foodCards) card.Dispose();
                _foodCards.Clear();
                flpFoodItems.Controls.Clear();

                var dt = Function.Query(@"
                    SELECT da.MaDoAn, da.TenDoAn, ISNULL(da.MoTa,N'') AS MoTa,
                           ISNULL(da.HinhAnhPath,N'') AS HinhAnhPath,
                           kc.MaKichCo, kc.TenKichCo, kc.DonGia
                    FROM dbo.DoAn da
                    INNER JOIN dbo.DoAnKichCo kc ON kc.MaDoAn=da.MaDoAn
                    ORDER BY da.MaDoAn,
                        CASE kc.MaKichCo WHEN 'S' THEN 1 WHEN 'M' THEN 2 WHEN 'L' THEN 3 ELSE 4 END,
                        kc.TenKichCo");

                var groupedFoods = dt.AsEnumerable()
                    .GroupBy(row => Convert.ToString(row["MaDoAn"]) ?? string.Empty, StringComparer.OrdinalIgnoreCase);

                foreach (var group in groupedFoods)
                {
                    var first = group.First();
                    string foodName = Convert.ToString(first["TenDoAn"]) ?? group.Key;
                    const string mediumSuffix = " cỡ vừa";
                    if (foodName.EndsWith(mediumSuffix, StringComparison.OrdinalIgnoreCase))
                        foodName = foodName[..^mediumSuffix.Length].TrimEnd();
                    var item = new FoodMenuItem(
                        group.Key,
                        foodName,
                        Convert.ToString(first["MoTa"]) ?? string.Empty,
                        Convert.ToString(first["HinhAnhPath"]) ?? string.Empty,
                        group.Select(row => new FoodSizeOption(
                            Convert.ToString(row["MaKichCo"]) ?? string.Empty,
                            Convert.ToString(row["TenKichCo"]) ?? string.Empty,
                            Convert.ToDecimal(row["DonGia"]))).ToArray());

                    var card = new FoodItemCard(item);
                    card.Width = Math.Max(270, flpFoodItems.ClientSize.Width - 42);
                    card.QuantityChanged += FoodCard_QuantityChanged;
                    card.SelectedSizeChanged += (_, _) => UpdateFoodCardQuantity(card);
                    _foodCards.Add(card);
                    flpFoodItems.Controls.Add(card);
                    UpdateFoodCardQuantity(card);
                }

                ResizeFoodCards();

                if (_foodCards.Count == 0)
                    flpFoodItems.Controls.Add(new Label
                    {
                        AutoSize = false,
                        Size = new Size(280, 52),
                        Text = "Chưa có món nào có cỡ bán. Hãy thêm cỡ/giá món trong CSDL.",
                        Font = new Font("Segoe UI", 8.5f),
                        ForeColor = ThemeHelper.TextMuted,
                        TextAlign = ContentAlignment.MiddleCenter
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thực đơn đồ ăn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResizeFoodCards()
        {
            int cardWidth = Math.Max(270, flpFoodItems.ClientSize.Width - 42);
            foreach (var card in _foodCards)
                card.Width = cardWidth;
        }

        private static string FoodSelectionKey(string maDoAn, string maKichCo) => $"{maDoAn}|{maKichCo}";

        private void FoodCard_QuantityChanged(object? sender, FoodQuantityChangedEventArgs e)
        {
            string key = FoodSelectionKey(e.Item.MaDoAn, e.Size.MaKichCo);
            _selectedFood.TryGetValue(key, out var existing);
            int quantity = (existing?.SoLuong ?? 0) + e.Delta;

            if (quantity <= 0)
            {
                _selectedFood.Remove(key);
            }
            else if (quantity <= 20)
            {
                _selectedFood[key] = new SelectedFoodItem(
                    e.Item.MaDoAn, e.Size.MaKichCo, e.Item.TenDoAn, e.Size.TenKichCo, e.Size.DonGia, quantity);
            }

            if (sender is FoodItemCard card) UpdateFoodCardQuantity(card);
            CalculateTotals();
        }

        private void UpdateFoodCardQuantity(FoodItemCard card)
        {
            var selectedSize = card.SelectedSize;
            int quantity = selectedSize is null ? 0 :
                _selectedFood.TryGetValue(FoodSelectionKey(card.Item.MaDoAn, selectedSize.MaKichCo), out var selected)
                    ? selected.SoLuong : 0;
            card.SetSelectedQuantity(quantity);
        }

        private void RefreshFoodCardQuantities()
        {
            foreach (var card in _foodCards) UpdateFoodCardQuantity(card);
        }

        private void CboPhim_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_loadingMovies) return;

            if (cboPhim.SelectedItem is DataRowView drv)
            {
                if (!HasSaleableShowtimes(drv))
                {
                    // Keep the previous valid choice when a cashier clicks a disabled movie.
                    if (cboPhim.SelectedIndex != _lastAvailableMovieIndex)
                        cboPhim.SelectedIndex = _lastAvailableMovieIndex;
                    return;
                }

                string selectedMovieId = Convert.ToString(drv["MaPhim"]) ?? string.Empty;
                _lastAvailableMovieIndex = cboPhim.SelectedIndex;
                if (string.Equals(selectedMovieId, _currentMaPhim, StringComparison.OrdinalIgnoreCase))
                    return;

                _currentMaPhim = selectedMovieId;
                string ten = drv["TenPhim"].ToString() ?? "";
                string theLoai = drv["TheLoai"].ToString() ?? "";
                string doTuoi = drv["DoTuoi"].ToString() ?? "";
                string thoiLuong = drv["ThoiLuong"].ToString() ?? "";
                _currentGiaVe = Convert.ToDecimal(drv["GiaPhim"]);
                string poster = drv["PosterPath"].ToString() ?? "";

                string moTa = drv.Row.Table.Columns.Contains("MoTa") ? (drv["MoTa"]?.ToString() ?? "") : "";
                string moTaShort = string.IsNullOrWhiteSpace(moTa) ? "" : "\n" + (moTa.Length > 90 ? moTa.Substring(0, 87) + "..." : moTa);

                lblMovieInfo.Text = 
                    $"Phim: {ten}\n" +
                    $"Thể loại: {theLoai}\n" +
                    $"Độ tuổi: {doTuoi} | {thoiLuong} phút\n" +
                    $"Giá vé cơ bản: {_currentGiaVe:N0} đ" +
                    moTaShort;

                LoadPoster(poster);
                LoadShowtimesForMovie(_currentMaPhim);
            }
        }

        private void LoadPoster(string relativeOrFullPath)
        {
            if (picPoster.Image != null)
            {
                picPoster.Image.Dispose();
                picPoster.Image = null;
            }

            picPoster.Image = ImageAssetHelper.Load(relativeOrFullPath);
        }

        private void LoadShowtimesForMovie(string maPhim)
        {
            try
            {
                string query = @"
                    SELECT 
                        sc.MaSuatChieu,
                        sc.NgayChieu,
                        (CONVERT(VARCHAR(5), sc.NgayChieu, 103) + ' ' + 
                         CONVERT(VARCHAR(5), sc.GioChieu, 108) + ' - ' + pc.TenPhong) AS ShowInfo,
                        sc.MaPhong,
                        pc.TenPhong,
                        pc.SoHangGhe,
                        pc.SoGheMoiHang,
                        ISNULL(sc.GiaSuatChieu, p.GiaPhim) AS GiaVe
                    FROM SuatChieu sc
                    JOIN PhongChieu pc ON sc.MaPhong = pc.MaPhong
                    JOIN Phim p ON sc.MaPhim = p.MaPhim
                    WHERE sc.MaPhim = @MaPhim
                      AND (p.NgayBatDauChieu IS NULL OR sc.NgayChieu >= p.NgayBatDauChieu)
                      AND (p.NgayKetThucChieu IS NULL OR sc.NgayChieu <= p.NgayKetThucChieu)
                      AND (sc.NgayChieu > CONVERT(date, GETDATE())
                           OR (sc.NgayChieu = CONVERT(date, GETDATE())
                               AND sc.GioChieu > CONVERT(time, GETDATE())))
                    ORDER BY sc.NgayChieu ASC, sc.GioChieu ASC";

                var dt = Function.Query(query, new[] { DbArg.VarChar20("@MaPhim", maPhim) });
                cboSuatChieu.DataSource = dt;
                cboSuatChieu.DisplayMember = "ShowInfo";
                cboSuatChieu.ValueMember = "MaSuatChieu";

                if (dt.Rows.Count == 0)
                {
                    lblRoomInfo.Text = "Phim này hiện chưa có suất chiếu!";
                    _currentMaSuatChieu = "";
                    _bookedSeats.Clear();
                    _selectedSeats.Clear();
                    RenderSeatMap();
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải suất chiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboSuatChieu_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboSuatChieu.SelectedItem is DataRowView drv)
            {
                _currentMaSuatChieu = drv["MaSuatChieu"].ToString() ?? "";
                _currentMaPhong = drv["MaPhong"].ToString() ?? "";
                string tenPhong = drv["TenPhong"].ToString() ?? "";
                _roomRows = Convert.ToInt32(drv["SoHangGhe"]);
                _roomCols = Convert.ToInt32(drv["SoGheMoiHang"]);
                _currentGiaVe = Convert.ToDecimal(drv["GiaVe"]);
                _rowTicketPrices.Clear();
                try
                {
                    var fares = Function.Query("SELECT HangGhe, GiaVe FROM dbo.GiaVeHangGhe WHERE MaSuatChieu=@MaSC",
                        new[] { DbArg.VarChar20("@MaSC", _currentMaSuatChieu) });
                    foreach (DataRow fare in fares.Rows)
                        _rowTicketPrices[Convert.ToString(fare["HangGhe"]) ?? ""] = Convert.ToDecimal(fare["GiaVe"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không tải được giá vé theo hàng ghế. Hãy chạy migration giá vé trong SSMS.\n" + ex.Message,
                        "Thiếu bảng giá ghế", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                lblRoomInfo.Text = $"Phòng: {tenPhong} ({_roomRows * _roomCols} ghế) - Giá vé thay đổi theo hàng ghế";

                _selectedSeats.Clear();
                LoadBookedSeatsForShowtime(_currentMaSuatChieu);
                RenderSeatMap();
                CalculateTotals();
            }
        }

        private void LoadBookedSeatsForShowtime(string maSuatChieu)
        {
            _bookedSeats.Clear();
            if (string.IsNullOrEmpty(maSuatChieu)) return;

            try
            {
                string query = "SELECT MaGhe FROM dbo.GheDatCho WHERE MaSuatChieu = @MaSC AND (HetHan IS NULL OR HetHan > SYSUTCDATETIME())";
                var dt = Function.Query(query, new[] { DbArg.VarChar20("@MaSC", maSuatChieu) });
                foreach (DataRow row in dt.Rows)
                {
                    _bookedSeats.Add(row["MaGhe"].ToString() ?? "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ghế đã đặt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboCombo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboCombo.SelectedItem is DataRowView drv)
            {
                string ma = drv["MaCombo"].ToString() ?? "";
                if (string.IsNullOrEmpty(ma))
                {
                    _requiredComboSeats = 0;
                    _comboDiscountPct = 0;
                    lblComboNote.Text = "Không áp dụng ưu đãi combo";
                }
                else
                {
                    _requiredComboSeats = Convert.ToInt32(drv["SoLuongGhe"]);
                    _comboDiscountPct = Convert.ToDecimal(drv["PhanTramGiam"]);
                    lblComboNote.Text = $"Yêu cầu chọn đúng {_requiredComboSeats} ghế. Giảm {_comboDiscountPct:0.#}% giá vé!";
                }
                CalculateTotals();
            }
        }

        private void RenderSeatMap()
        {
            bool hasShowtime = !string.IsNullOrEmpty(_currentMaSuatChieu);
            for (int row = 0; row < pnlSeatMap.Rows.Count; row++)
            {
                char rowLetter = (char)('A' + row);
                DataGridViewRow gridRow = pnlSeatMap.Rows[row];
                decimal rowFare = GetRowTicketPrice(rowLetter.ToString());
                gridRow.HeaderCell.Value = row < _roomRows
                    ? (rowFare > 0 ? $"{rowLetter}\n{rowFare:N0}đ" : rowLetter.ToString())
                    : null;
                gridRow.Visible = row < _roomRows;
                for (int column = 0; column < pnlSeatMap.Columns.Count; column++)
                {
                    DataGridViewCell cell = gridRow.Cells[column];
                    bool visibleSeat = row < _roomRows && column < _roomCols;
                    pnlSeatMap.Columns[column].Visible = column < _roomCols;
                    string seatCode = $"{rowLetter}{column + 1}";
                    cell.Value = hasShowtime && visibleSeat ? seatCode : string.Empty;
                    bool booked = hasShowtime && _bookedSeats.Contains(seatCode);
                    bool selected = hasShowtime && _selectedSeats.Contains(seatCode);
                    cell.Style.BackColor = booked ? ThemeHelper.SeatBooked : selected ? ThemeHelper.SeatSelected : ThemeHelper.SeatAvailable;
                    cell.Style.ForeColor = booked || selected ? Color.White : ThemeHelper.TextPrimary;
                    cell.Style.SelectionBackColor = cell.Style.BackColor;
                    cell.Style.SelectionForeColor = cell.Style.ForeColor;
                    string fareText = rowFare > 0 ? $"Giá vé: {rowFare:N0} đ" : "Chưa có giá cho hàng ghế này";
                    cell.ToolTipText = $"{fareText} / ghế — " + (booked ? "Ghế đã được đặt" : selected ? "Ghế đang chọn" : "Ghế trống");
                    cell.ReadOnly = booked || !hasShowtime || !visibleSeat;
                }
            }
        }

        private void ConfigureSeatGrid()
        {
            pnlSeatMap.AutoGenerateColumns = false;
            pnlSeatMap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            pnlSeatMap.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            pnlSeatMap.BackgroundColor = Color.White;
            pnlSeatMap.BorderStyle = BorderStyle.None;
            pnlSeatMap.GridColor = Color.FromArgb(218, 230, 222);
            pnlSeatMap.ColumnHeadersHeight = 28;
            pnlSeatMap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            pnlSeatMap.RowTemplate.Height = 42;
            pnlSeatMap.RowHeadersWidth = 90;
            pnlSeatMap.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pnlSeatMap.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlSeatMap.RowHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            pnlSeatMap.Columns.Clear();
            pnlSeatMap.Rows.Clear();
            for (int column = 1; column <= 30; column++)
            {
                pnlSeatMap.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = $"colSeat{column}",
                    HeaderText = column.ToString(),
                    Width = 44,
                    MinimumWidth = 42,
                    FlatStyle = FlatStyle.Flat,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
            }
            for (int row = 0; row < 26; row++)
            {
                int index = pnlSeatMap.Rows.Add();
                pnlSeatMap.Rows[index].HeaderCell.Value = ((char)('A' + row)).ToString();
            }
            RenderSeatMap();
        }

        private void PnlSeatMap_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _roomRows || e.ColumnIndex < 0 || e.ColumnIndex >= _roomCols ||
                string.IsNullOrEmpty(_currentMaSuatChieu)) return;
            HandleSeatClick($"{(char)('A' + e.RowIndex)}{e.ColumnIndex + 1}");
        }

        private void HandleSeatClick(string seatCode)
        {
            if (_bookedSeats.Contains(seatCode)) return;

            if (_selectedSeats.Contains(seatCode))
            {
                _selectedSeats.Remove(seatCode);
            }
            else
            {
                // Nếu đang chọn combo và đã chọn đủ số ghế combo quy định
                if (_requiredComboSeats > 0 && _selectedSeats.Count >= _requiredComboSeats)
                {
                    MessageBox.Show(
                        $"Combo đang chọn yêu cầu ĐÚNG {_requiredComboSeats} ghế!\n" +
                        $"Nếu muốn chọn thêm ghế, vui lòng bỏ chọn bớt ghế cũ hoặc đổi sang combo khác / Không áp dụng combo.",
                        "Ràng buộc Combo ghế",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                _selectedSeats.Add(seatCode);
            }

            RenderSeatMap();
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            // Movie info
            lblSumMovie.Text = $"Phim: {cboPhim.Text}";
            string showInfo = cboSuatChieu.SelectedItem is DataRowView showRow && showRow.Row.Table.Columns.Contains("ShowInfo")
                ? Convert.ToString(showRow["ShowInfo"]) ?? string.Empty
                : cboSuatChieu.Text;
            lblSumShow.Text = $"Suất: {showInfo}";

            // Seats
            var sortedSeats = _selectedSeats.OrderBy(x => x).ToList();
            string seatListStr = string.Join(", ", sortedSeats.Select(seat =>
                $"{seat} ({GetRowTicketPrice(seat[..1]):N0} đ)"));
            lblSumSeats.Text = $"Ghế ({sortedSeats.Count} vé): {seatListStr}";

            // Ticket price
            decimal rawTicketPrice = sortedSeats.Sum(seat => GetRowTicketPrice(seat[..1]));
            lblSumTicketPrice.Text = $"Tiền vé gốc: {rawTicketPrice:N0} đ";

            // Discount combo
            decimal comboDiscount = 0;
            if (_requiredComboSeats > 0 && _comboDiscountPct > 0)
            {
                // Chỉ giảm khi chọn ĐỦ số lượng ghế combo
                if (sortedSeats.Count == _requiredComboSeats)
                {
                    comboDiscount = rawTicketPrice * (_comboDiscountPct / 100m);
                    lblSumDiscount.Text = $"Ưu đãi combo ({_comboDiscountPct:0.#}% vé): -{comboDiscount:N0} đ";
                    lblSumDiscount.ForeColor = ThemeHelper.AccentGreen;
                }
                else
                {
                    lblSumDiscount.Text = $"Ưu đãi combo: Cần chọn đủ {_requiredComboSeats} ghế (đang chọn {sortedSeats.Count})";
                    lblSumDiscount.ForeColor = ThemeHelper.AccentRed;
                }
            }
            else
            {
                lblSumDiscount.Text = "Ưu đãi combo: 0 đ";
                lblSumDiscount.ForeColor = ThemeHelper.TextMuted;
            }

            // Food
            decimal foodTotal = 0;
            var foodItems = new List<string>();
            foreach (var kv in _selectedFood)
            {
                decimal itemTotal = kv.Value.DonGia * kv.Value.SoLuong;
                foodTotal += itemTotal;
                foodItems.Add($"{kv.Value.TenDoAn} - {kv.Value.TenKichCo} x{kv.Value.SoLuong}");
            }
            string foodDescription = string.Join(", ", foodItems);
            lblSumFood.Text = foodTotal > 0
                ? $"Đồ ăn: {foodTotal:N0} đ\n{foodDescription}"
                : "Đồ ăn: 0 đ";
            lblSumFood.Tag = foodDescription;

            // Discount Voucher
            decimal voucherDiscount = 0;
            if (!string.IsNullOrEmpty(_appliedVoucherCode))
            {
                if (_appliedVoucherType.Contains("phần trăm", StringComparison.OrdinalIgnoreCase))
                {
                    decimal veConLai = Math.Max(0, rawTicketPrice - comboDiscount);
                    voucherDiscount = veConLai * (_appliedVoucherValue / 100m);
                    lblSumVoucher.Text = $"Voucher: -{voucherDiscount:N0} đ ({_appliedVoucherValue:0.#}%)";
                }
                else if (_appliedVoucherType.Contains("vé", StringComparison.OrdinalIgnoreCase))
                {
                    int soVeGiam = Math.Min((int)_appliedVoucherValue, sortedSeats.Count);
                    voucherDiscount = sortedSeats.Select(seat => GetRowTicketPrice(seat[..1]))
                        .OrderBy(price => price).Take(soVeGiam).Sum();
                    lblSumVoucher.Text = $"Voucher: -{voucherDiscount:N0} đ ({soVeGiam} vé)";
                }
                else // Giảm số tiền
                {
                    decimal maxGiam = Math.Max(0, rawTicketPrice - comboDiscount) + foodTotal;
                    voucherDiscount = Math.Min(_appliedVoucherValue, maxGiam);
                    lblSumVoucher.Text = $"Voucher: -{voucherDiscount:N0} đ";
                }
                lblSumVoucher.ForeColor = ThemeHelper.AccentGreen;
            }
            else
            {
                lblSumVoucher.Text = "Voucher: Chưa dùng";
                lblSumVoucher.ForeColor = ThemeHelper.TextMuted;
            }

            // Final Total
            decimal totalDiscount = comboDiscount + voucherDiscount;
            decimal finalTotal = Math.Max(0, rawTicketPrice + foodTotal - totalDiscount);
            lblSumTotal.Text = $"TỔNG TIỀN:\n{finalTotal:N0} đ";
        }

        private decimal GetRowTicketPrice(string rowLetter) =>
            _rowTicketPrices.TryGetValue(rowLetter, out decimal price) ? price : 0m;

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentMaSuatChieu))
            {
                MessageBox.Show("Vui lòng chọn Phim và Suất chiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var sortedSeats = _selectedSeats.OrderBy(x => x).ToList();
            if (sortedSeats.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một ghế ngồi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra ràng buộc combo
            if (_requiredComboSeats > 0 && sortedSeats.Count != _requiredComboSeats)
            {
                MessageBox.Show(
                    $"Gói combo đang chọn yêu cầu quý khách phải chọn ĐÚNG {_requiredComboSeats} ghế!\n" +
                    $"Hiện tại bạn đang chọn {sortedSeats.Count} ghế.\n" +
                    $"Vui lòng chọn thêm hoặc bớt ghế cho đúng số lượng để được giảm giá.",
                    "Chưa đủ số lượng ghế combo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                string maCombo = cboCombo.SelectedValue?.ToString() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(maCombo)) maCombo = null!;
                var draft = new SaleDraft(_currentMaSuatChieu, sortedSeats,
                    _selectedFood.Values.Select(x => new FoodOrder(x.MaDoAn, x.SoLuong, x.MaKichCo)).ToArray(), maCombo,
                    string.IsNullOrWhiteSpace(_appliedVoucherCode) ? null : _appliedVoucherCode, CurrentUser.TenDangNhap);
                SaleResult sale;

                if (radTransfer.Checked)
                {
                    var checkout = PaymentsApiClient.CreateCheckout(draft);
                    using var qr = new FormQRPayment(checkout);
                    if (qr.ShowDialog(this) != DialogResult.OK) return;
                    sale = checkout.Sale;
                }
                else
                {
                    var confirm = MessageBox.Show($"Xác nhận bán {sortedSeats.Count} ghế cho {cboPhim.Text}?", "Xác nhận bán vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                    sale = SalesService.CreateCashSale(draft);
                }

                using var frmIn = new FormInHoaDon
                {
                    MaHD = sale.MaHD, NgayXuat = sale.NgayLap, TenPhim = sale.TenPhim, PhongChieu = sale.TenPhong,
                    SuatChieu = sale.SuatChieu, DanhSachGhe = sale.DanhSachGhe, SoLuongVe = sale.SoLuongVe,
                    NgayChieu = (cboSuatChieu.SelectedItem as DataRowView) is { } selectedShow
                        ? Convert.ToDateTime(selectedShow["NgayChieu"])
                        : null,
                    TenCombo = sale.TenCombo, DanhSachDoAn = sale.DanhSachDoAn, TienVe = sale.TienVe,
                    TienGiamGia = sale.TienGiamGia, TienDoAn = sale.TienDoAn, TongTien = sale.TongTien,
                    PhuongThucTT = sale.PhuongThucTT
                };
                frmIn.ShowDialog(this);
                LoadBookedSeatsForShowtime(_currentMaSuatChieu);
                _selectedSeats.Clear();
                _selectedFood.Clear();
                RefreshFoodCardQuantities();
                HuyVoucher(silent: true);
                RenderSeatMap();
                CalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetSelection()
        {
            _selectedSeats.Clear();
            _selectedFood.Clear();
            RefreshFoodCardQuantities();
            HuyVoucher(silent: true);
            RenderSeatMap();
            CalculateTotals();
        }
    }
}

