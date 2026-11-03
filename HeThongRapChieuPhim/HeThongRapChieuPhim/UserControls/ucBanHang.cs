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
            string MaDoAn, string TenDoAn, decimal DonGia, int SoLuong);

        // State tracking
        private string _currentMaPhim = "";
        private string _currentMaSuatChieu = "";
        private string _currentMaPhong = "";
        private decimal _currentGiaVe = 80000;
        private RoomVipSettings _vipSettings = new("", 0);
        private int _roomRows = 6;
        private int _roomCols = 10;
        private readonly HashSet<string> _bookedSeats = new();
        private readonly HashSet<string> _selectedSeats = new();
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
            // Selection comes from the movie catalog; keep only its data binding here.
            cboPhim.BindingContext = new BindingContext();
            InitializePageEvents();
            InitializeShowtimeOptions();
            InitializeBookingSteps();
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

        private Label lblShowtimeEmpty => showtimePage.lblShowtimeEmpty;
        private void InitializePageEvents()
        {
            cboPhim.SelectedIndexChanged += CboPhim_SelectedIndexChanged;
            cboSuatChieu.SelectedIndexChanged += CboSuatChieu_SelectedIndexChanged;
            cboCombo.SelectedIndexChanged += CboCombo_SelectedIndexChanged;
            btnReset.Click += BtnReset_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnShowQR.Click += BtnShowQR_Click;
            radTransfer.CheckedChanged += RadTransfer_CheckedChanged;
            btnHuyVoucher.Click += BtnHuyVoucher_Click;
            btnApDungVoucher.Click += BtnApDungVoucher_Click;
            txtVoucherCode.KeyDown += TxtVoucherCode_KeyDown;
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

        public bool SelectMovie(string movieId)
        {
            for (int i = 0; i < cboPhim.Items.Count; i++)
            {
                if (cboPhim.Items[i] is DataRowView movie && HasSaleableShowtimes(movie) &&
                    string.Equals(Convert.ToString(movie["MaPhim"]), movieId, StringComparison.OrdinalIgnoreCase))
                {
                    cboPhim.SelectedIndex = i;
                    CboPhim_SelectedIndexChanged(cboPhim, EventArgs.Empty);
                    return true;
                }
            }
            return false;
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
            _bookedSeats.Clear();
            _selectedSeats.Clear();
            cboSuatChieu.DataSource = null;
            RenderShowtimeOptions();
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
                    SELECT MaDoAn, TenDoAn, ISNULL(MoTa,N'') AS MoTa,
                           ISNULL(HinhAnhPath,N'') AS HinhAnhPath, Gia
                    FROM dbo.DoAn ORDER BY MaDoAn");
                foreach (DataRow row in dt.Rows)
                {
                    var item = new FoodMenuItem(
                        Convert.ToString(row["MaDoAn"]) ?? "",
                        Convert.ToString(row["TenDoAn"]) ?? "",
                        Convert.ToString(row["MoTa"]) ?? "",
                        Convert.ToString(row["HinhAnhPath"]) ?? "",
                        Convert.ToDecimal(row["Gia"]));
                    var card = new FoodItemCard(item);
                    card.Width = Math.Max(270, flpFoodItems.ClientSize.Width - 42);
                    card.QuantityChanged += FoodCard_QuantityChanged;

                    _foodCards.Add(card);
                    flpFoodItems.Controls.Add(card);
                    UpdateFoodCardQuantity(card);
                }

                ResizeFoodCards();

                if (_foodCards.Count == 0)
                    flpFoodItems.Controls.Add(foodPage.lblFoodEmpty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thực đơn đồ ăn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResizeFoodCards()
        {
            int scrollY = -flpFoodItems.AutoScrollPosition.Y;
            int cardWidth = Math.Max(270, flpFoodItems.ClientSize.Width
                - flpFoodItems.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 6);
            flpFoodItems.SuspendLayout();
            try
            {
                flpFoodItems.AutoScroll = false;
                foreach (var card in _foodCards) card.Width = cardWidth;
            }
            finally
            {
                flpFoodItems.ResumeLayout(true);
                flpFoodItems.AutoScroll = true;
                flpFoodItems.PerformLayout();
            }
            flpFoodItems.AutoScrollPosition = new Point(0, scrollY);
        }



        private void FoodCard_QuantityChanged(object? sender, FoodQuantityChangedEventArgs e)
        {
            string key = e.Item.MaDoAn;
            _selectedFood.TryGetValue(key, out var existing);
            int quantity = (existing?.SoLuong ?? 0) + e.Delta;

            if (quantity <= 0)
            {
                _selectedFood.Remove(key);
            }
            else if (quantity <= 20)
            {
                _selectedFood[key] = new SelectedFoodItem(
                    e.Item.MaDoAn, e.Item.TenDoAn, e.Item.DonGia, quantity);
            }

            if (sender is FoodItemCard card) UpdateFoodCardQuantity(card);
            CalculateTotals();
        }

        private void UpdateFoodCardQuantity(FoodItemCard card)
        {
            int quantity = _selectedFood.TryGetValue(card.Item.MaDoAn, out var selected) ? selected.SoLuong : 0;
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
                        CONVERT(VARCHAR(5), sc.GioChieu, 108) AS ShowTime,
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
                _bindingShowtimes = true;
                cboSuatChieu.DataSource = dt;
                cboSuatChieu.DisplayMember = "ShowInfo";
                cboSuatChieu.ValueMember = "MaSuatChieu";
                _bindingShowtimes = false;
                RenderShowtimeOptions();

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
                _bindingShowtimes = false;
                cboSuatChieu.DataSource = null;
                RenderShowtimeOptions();
            }
        }

        private void CboSuatChieu_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_bindingShowtimes) return;
            if (cboSuatChieu.SelectedItem is DataRowView drv)
            {
                _currentMaSuatChieu = drv["MaSuatChieu"].ToString() ?? "";
                _currentMaPhong = drv["MaPhong"].ToString() ?? "";
                UpdateShowtimeSelection();
                string tenPhong = drv["TenPhong"].ToString() ?? "";
                _roomRows = Convert.ToInt32(drv["SoHangGhe"]);
                _roomCols = Convert.ToInt32(drv["SoGheMoiHang"]);
                _currentGiaVe = Convert.ToDecimal(drv["GiaVe"]);
                try { _vipSettings = RoomVipService.Get(_currentMaPhong); }
                catch (Exception ex)
                {
                    ClearSelectedShowtime("Không tải được cấu hình VIP. Hãy chọn lại suất.");
                    MessageBox.Show(ex.Message, "Lỗi tải giá vé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblRoomInfo.Text = $"Phòng: {tenPhong} ({_roomRows * _roomCols} ghế) - Giá vé: {_currentGiaVe:N0} đ";

                _selectedSeats.Clear();
                if (!LoadBookedSeatsForShowtime(_currentMaSuatChieu)) return;
                RenderSeatMap();
                CalculateTotals();
            }
        }

        private bool LoadBookedSeatsForShowtime(string maSuatChieu)
        {
            _bookedSeats.Clear();
            if (string.IsNullOrEmpty(maSuatChieu)) return false;

            try
            {
                string query = "SELECT MaGhe FROM dbo.GheDatCho WHERE MaSuatChieu = @MaSC AND (HetHan IS NULL OR HetHan > SYSUTCDATETIME())";
                var dt = Function.Query(query, new[] { DbArg.VarChar20("@MaSC", maSuatChieu) });
                foreach (DataRow row in dt.Rows)
                {
                    _bookedSeats.Add(row["MaGhe"].ToString() ?? "");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ghế đã đặt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearSelectedShowtime("Không tải được trạng thái ghế. Hãy chọn lại suất để thử lại.");
                return false;
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
                }
                else
                {
                    _requiredComboSeats = Convert.ToInt32(drv["SoLuongGhe"]);
                    _comboDiscountPct = Convert.ToDecimal(drv["PhanTramGiam"]);
                }
                CalculateTotals();
            }
        }

        private void RenderSeatMap()
        {
            bool hasShowtime = !string.IsNullOrEmpty(_currentMaSuatChieu);
            pnlSeatMap.Visible = hasShowtime;
            lblScreen.Visible = hasShowtime;
            for (int row = 0; row < pnlSeatMap.Rows.Count; row++)
            {
                char rowLetter = (char)('A' + row);
                DataGridViewRow gridRow = pnlSeatMap.Rows[row];
                decimal rowFare = GetRowTicketPrice(rowLetter.ToString());
                gridRow.HeaderCell.Value = row < _roomRows
                    ? rowLetter + (_vipSettings.IsVip(rowLetter.ToString()) ? " VIP" : "")
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
                    cell.Style.BackColor = booked ? boxLegendBooked.BackColor : selected ? boxLegendSelected.BackColor
                        : _vipSettings.IsVip(rowLetter.ToString()) ? Color.FromArgb(255, 229, 153) : boxLegendAvailable.BackColor;
                    cell.Style.ForeColor = booked || selected ? Color.White : ThemeHelper.TextPrimary;
                    cell.Style.SelectionBackColor = cell.Style.BackColor;
                    cell.Style.SelectionForeColor = cell.Style.ForeColor;
                    string fareText = $"Giá vé: {rowFare:N0} đ";
                    cell.ToolTipText = $"{fareText} / ghế — " + (booked ? "Ghế đã được đặt" : selected ? "Ghế đang chọn" : "Ghế trống");
                    cell.ReadOnly = booked || !hasShowtime || !visibleSeat;
                }
            }
        }

        private void ConfigureSeatGrid()
        {
            pnlSeatMap.Rows.Clear();
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
                _selectedSeats.Add(seatCode);
            }

            RenderSeatMap();
            CalculateTotals();
        }

        private void SelectBestComboForSeats()
        {
            int bestIndex = -1;
            decimal bestDiscount = 0;
            for (int i = 0; i < cboCombo.Items.Count; i++)
            {
                if (cboCombo.Items[i] is not DataRowView row) continue;
                if (string.IsNullOrWhiteSpace(Convert.ToString(row["MaCombo"])))
                {
                    if (bestIndex < 0) bestIndex = i;
                    continue;
                }
                int seats = Convert.ToInt32(row["SoLuongGhe"]);
                decimal discount = Convert.ToDecimal(row["PhanTramGiam"]);
                if (seats > 0 && seats == _selectedSeats.Count && discount > bestDiscount)
                {
                    bestIndex = i;
                    bestDiscount = discount;
                }
            }
            if (cboCombo.SelectedIndex != bestIndex) cboCombo.SelectedIndex = bestIndex;
        }
        private void CalculateTotals()
        {
            SelectBestComboForSeats();
            // Movie info
            lblSumMovie.Text = $"Phim: {cboPhim.Text}";
            string showInfo = cboSuatChieu.SelectedItem is DataRowView showRow && showRow.Row.Table.Columns.Contains("ShowInfo")
                ? Convert.ToString(showRow["ShowInfo"]) ?? string.Empty
                : "Chưa chọn suất chiếu";
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
                foodItems.Add($"{kv.Value.TenDoAn} x{kv.Value.SoLuong}");
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
            _bookingTotal = finalTotal;
            UpdateStepSummary();
        }

        private decimal GetRowTicketPrice(string rowLetter) =>
            _vipSettings.Price(_currentGiaVe, rowLetter);

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
                    _selectedFood.Values.Select(x => new FoodOrder(x.MaDoAn, x.SoLuong)).ToArray(), maCombo,
                    string.IsNullOrWhiteSpace(_appliedVoucherCode) ? null : _appliedVoucherCode, CurrentUser.TenDangNhap,
                    sortedSeats.ToDictionary(seat => seat, seat => GetRowTicketPrice(seat[..1])));
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
                BookingCompleted?.Invoke(this, EventArgs.Empty);
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
            ShowBookingStep(1);
        }

        #region Liên kết control của các trang

        private Panel pnlLeft => showtimePage.pnlLeft;
        private Label lblRoomInfo => showtimePage.lblRoomInfo;
        private FlowLayoutPanel _showtimeOptions => showtimePage._showtimeOptions;
        private Button btnShowtimePreview1 => showtimePage.btnShowtimePreview1;
        private Button btnShowtimePreview2 => showtimePage.btnShowtimePreview2;
        private Button btnShowtimePreview3 => showtimePage.btnShowtimePreview3;
        private Button btnShowtimePreview4 => showtimePage.btnShowtimePreview4;
        private ComboBox cboSuatChieu => showtimePage.cboSuatChieu;
        private ComboBox cboShowRoom => showtimePage.cboShowRoom;
        private Label lblShowRoom => showtimePage.lblShowRoom;
        private ComboBox cboShowDate => showtimePage.cboShowDate;
        private Label lblShowDate => showtimePage.lblShowDate;
        private Label lblSec2 => showtimePage.lblSec2;
        private Panel pnlMovieMeta => showtimePage.pnlMovieMeta;
        private Label lblMovieInfo => showtimePage.lblMovieInfo;
        private PictureBox picPoster => showtimePage.picPoster;
        private ComboBox cboCombo => seatPage.cboCombo;
        private Label lblSec3 => seatPage.lblSec3;
        private Panel pnlCenter => seatPage.pnlCenter;
        private Panel tabSeats => seatPage.tabSeats;
        private DataGridView pnlSeatMap => seatPage.pnlSeatMap;
        private Label lblScreen => seatPage.lblScreen;
        private Panel pnlSeatHeader => seatPage.pnlSeatHeader;
        private Panel pnlLegend => seatPage.pnlLegend;
        private Panel pnlLegendAvailable => seatPage.pnlLegendAvailable;
        private Panel boxLegendAvailable => seatPage.boxLegendAvailable;
        private Label lblLegendAvailable => seatPage.lblLegendAvailable;
        private Panel pnlLegendSelected => seatPage.pnlLegendSelected;
        private Panel boxLegendSelected => seatPage.boxLegendSelected;
        private Label lblLegendSelected => seatPage.lblLegendSelected;
        private Panel pnlLegendBooked => seatPage.pnlLegendBooked;
        private Panel boxLegendBooked => seatPage.boxLegendBooked;
        private Label lblLegendBooked => seatPage.lblLegendBooked;
        private Label lblSeatMapTitle => seatPage.lblSeatMapTitle;
        private FlowLayoutPanel flpFoodItems => foodPage.flpFoodItems;
        private Label lblFoodHint => foodPage.lblFoodHint;
        private Label lblSec4 => foodPage.lblSec4;
        private Panel pnlRight => paymentPage.pnlRight;
        private Button btnReset => paymentPage.btnReset;
        private Button btnConfirm => paymentPage.btnConfirm;
        private GroupBox grpPayment => paymentPage.grpPayment;
        private Button btnShowQR => paymentPage.btnShowQR;
        private RadioButton radTransfer => paymentPage.radTransfer;
        private RadioButton radCash => paymentPage.radCash;
        private GroupBox grpVoucher => paymentPage.grpVoucher;
        private Label lblVoucherStatus => paymentPage.lblVoucherStatus;
        private Button btnHuyVoucher => paymentPage.btnHuyVoucher;
        private Button btnApDungVoucher => paymentPage.btnApDungVoucher;
        private TextBox txtVoucherCode => paymentPage.txtVoucherCode;
        private Panel pnlBillDetails => paymentPage.pnlBillDetails;
        private Label lblSumTotal => paymentPage.lblSumTotal;
        private Panel divLine => paymentPage.divLine;
        private Label lblSumFood => paymentPage.lblSumFood;
        private Label lblSumVoucher => paymentPage.lblSumVoucher;
        private Label lblSumDiscount => paymentPage.lblSumDiscount;
        private Label lblSumTicketPrice => paymentPage.lblSumTicketPrice;
        private Label lblSumSeats => paymentPage.lblSumSeats;
        private Label lblSumShow => paymentPage.lblSumShow;
        private Label lblSumMovie => paymentPage.lblSumMovie;
        private Label lblBillTitle => paymentPage.lblBillTitle;
        #endregion

        #region Chọn ngày, phòng và suất chiếu

        private DataTable? _allShowtimes;
        private bool _loadingShowDates;
        private bool _bindingShowtimes;
        private bool _loadingShowRooms;
        private sealed record ShowRoom(string Id, string Name);

        private void InitializeShowtimeOptions()
        {
            cboShowDate.SelectedIndexChanged += (_, _) => ApplyShowDate();
            cboShowRoom.DisplayMember = nameof(ShowRoom.Name);
            cboShowRoom.SelectedIndexChanged += (_, _) => { if (!_loadingShowRooms) ApplyShowRoom(); };
        }

        private void RenderShowtimeOptions()
        {
            _allShowtimes = cboSuatChieu.DataSource as DataTable;
            DateTime? previous = cboShowDate.SelectedItem is DateTime day ? day : null;
            _loadingShowDates = true;
            try
            {
                cboShowDate.Items.Clear();
                if (_allShowtimes != null)
                    foreach (DateTime date in _allShowtimes.AsEnumerable()
                        .Select(row => Convert.ToDateTime(row["NgayChieu"]).Date).Distinct().OrderBy(date => date))
                        cboShowDate.Items.Add(date);
                cboShowDate.Enabled = cboShowDate.Items.Count > 0;
                if (cboShowDate.Items.Count > 0)
                    cboShowDate.SelectedIndex = previous.HasValue && cboShowDate.Items.Contains(previous.Value)
                        ? cboShowDate.Items.IndexOf(previous.Value) : 0;
            }
            finally { _loadingShowDates = false; }
            ApplyShowDate();
        }

        private void ApplyShowDate()
        {
            if (_loadingShowDates) return;
            string? previous = (cboShowRoom.SelectedItem as ShowRoom)?.Id;
            _loadingShowRooms = true;
            try
            {
                cboShowRoom.Items.Clear();
                cboShowRoom.Items.Add(new ShowRoom("", "Tất cả phòng"));
                if (_allShowtimes != null && cboShowDate.SelectedItem is DateTime day)
                    foreach (var room in _allShowtimes.AsEnumerable()
                        .Where(row => Convert.ToDateTime(row["NgayChieu"]).Date == day.Date)
                        .Select(row => new ShowRoom(Convert.ToString(row["MaPhong"]) ?? "", Convert.ToString(row["TenPhong"]) ?? ""))
                        .Distinct().OrderBy(room => room.Name))
                        cboShowRoom.Items.Add(room);
                cboShowRoom.Enabled = cboShowRoom.Items.Count > 1;
                if (cboShowRoom.Items.Count > 0)
                {
                    int index = cboShowRoom.Items.Cast<ShowRoom>().ToList().FindIndex(room => room.Id == previous);
                    cboShowRoom.SelectedIndex = index >= 0 ? index : 0;
                }
            }
            finally { _loadingShowRooms = false; }
            ApplyShowRoom();
        }

        private void ApplyShowRoom()
        {
            _showtimeOptions.SuspendLayout();
            _showtimeOptions.Controls.Remove(lblShowtimeEmpty);
            try
            {
                while (_showtimeOptions.Controls.Count > 0)
                    _showtimeOptions.Controls[0].Dispose();
                DataTable? filtered = _allShowtimes?.Clone();
                if (filtered != null && cboShowDate.SelectedItem is DateTime day)
                    foreach (DataRow row in _allShowtimes!.Rows)
                        if (Convert.ToDateTime(row["NgayChieu"]).Date == day.Date
                            && cboShowRoom.SelectedItem is ShowRoom roomOption
                            && (roomOption.Id.Length == 0 || Convert.ToString(row["MaPhong"]) == roomOption.Id))
                            filtered.ImportRow(row);

                _bindingShowtimes = true;
                try
                {
                    cboSuatChieu.DataSource = filtered;
                    cboSuatChieu.SelectedIndex = -1;
                }
                finally { _bindingShowtimes = false; }
                ClearSelectedShowtime("Chọn giờ chiếu để xem ghế và giá vé.");
                if (filtered != null && filtered.Rows.Count > 0)
                {
                    bool allRooms = cboShowRoom.SelectedItem is ShowRoom selected && selected.Id.Length == 0;
                    foreach (DataRow row in filtered.Rows)
                    {
                        string id = Convert.ToString(row["MaSuatChieu"]) ?? "";
                        string time = Convert.ToString(row["ShowTime"]) ?? "";
                        string room = Convert.ToString(row["TenPhong"]) ?? "";
                        var button = new HeThongRapChieuPhim.UIHelpers.ShowtimeOption();
                        button.Bind(id, time, room, cboShowDate.Text, allRooms);
                        button.Chosen += (_, _) => cboSuatChieu.SelectedValue = id;
                        _showtimeOptions.Controls.Add(button);
                    }
                }
                else
                {
                    ClearSelectedShowtime("Không có suất chiếu còn bán cho lựa chọn này.");
                    _showtimeOptions.Controls.Add(lblShowtimeEmpty);
                    RenderSeatMap();
                    CalculateTotals();
                }
                UpdateShowtimeSelection();
            }
            finally { _showtimeOptions.ResumeLayout(true); }
        }

        private void ClearSelectedShowtime(string message)
        {
            _bindingShowtimes = true;
            try { cboSuatChieu.SelectedIndex = -1; }
            finally { _bindingShowtimes = false; }
            _currentMaSuatChieu = "";
            _currentMaPhong = "";
            _currentGiaVe = 0;
            _vipSettings = new("", 0);
            _selectedSeats.Clear();
            _bookedSeats.Clear();

            lblRoomInfo.Text = message;
            RenderSeatMap();
            CalculateTotals();
            UpdateShowtimeSelection();
        }

        private void UpdateShowtimeSelection()
        {
            foreach (Control control in _showtimeOptions.Controls)
            {
                if (control is not HeThongRapChieuPhim.UIHelpers.ShowtimeOption button) continue;
                button.SetSelected(Convert.ToString(button.Tag) == _currentMaSuatChieu);
            }
        }
        #endregion

        #region Điều hướng các bước bán vé

        private int _bookingStep;
        private bool _changingStep;
        private decimal _bookingTotal;
        public event EventHandler? BookingCancelled;
        public event EventHandler? BookingCompleted;
        public event EventHandler? BookingStepChanged;
        public int CurrentBookingStep => _bookingStep;

        private void InitializeBookingSteps()
        {
            _stepBack.Click += (_, _) => { if (_bookingStep == 0) BookingCancelled?.Invoke(this, EventArgs.Empty); else ShowBookingStep(_bookingStep - 1); };
            _stepNext.Click += (_, _) => AdvanceBookingStep();
            if (!UIHelpers.DesignTimeHelper.IsDesignTime(this))
                bookingPages.Selecting += (_, e) => { if (!_changingStep) e.Cancel = true; };
            if (!UIHelpers.DesignTimeHelper.IsDesignTime(this)) ShowBookingStep(0);
        }

        private bool AdvanceBookingStep()
        {
            if (_bookingStep == 0 && string.IsNullOrEmpty(_currentMaSuatChieu))
            {
                MessageBox.Show("Hãy chọn ngày, phòng và bấm giờ chiếu trước khi tiếp tục.", "Chọn suất chiếu");
                return false;
            }
            if (_bookingStep == 1 && (_selectedSeats.Count == 0 ||
                (_requiredComboSeats > 0 && _selectedSeats.Count != _requiredComboSeats)))
            {
                MessageBox.Show(_selectedSeats.Count == 0 ? "Hãy chọn ít nhất một ghế." : $"Combo yêu cầu chọn đúng {_requiredComboSeats} ghế.", "Chọn ghế");
                return false;
            }
            if (_bookingStep < 3) ShowBookingStep(_bookingStep + 1);
            return true;
        }

        private void ShowBookingStep(int step)
        {
            _bookingStep = step;
            _changingStep = true;
            try { bookingPages.SelectedIndex = step; }
            finally { _changingStep = false; }
            lblTitle.Text = bookingPages.TabPages[step].Text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
            _stepBack.Text = step == 0 ? "← Chọn phim khác" : "← Quay lại";
            _stepNext.Text = step == 0 ? "Chọn chỗ ngồi →" : step == 1 ? "Chọn bắp nước →" : "Thanh toán →";
            _stepNext.Visible = step < 3;
            navigation.ColumnStyles[2].Width = step == 3 ? 0 : 190;
            UpdateStepSummary();

            if (step >= 2) CalculateTotals();
            BookingStepChanged?.Invoke(this, EventArgs.Empty);
            if (step == 2)
            {
                ResizeFoodCards();
                _stepNext.Focus();
                flpFoodItems.AutoScrollPosition = Point.Empty;
            }
        }

        private void UpdateStepSummary()
        {
            _stepSummary.Text = _bookingStep == 0 ? "Bước 1 / 4 · Chọn ngày, phòng và giờ chiếu"
                : $"{cboPhim.Text} · {cboSuatChieu.Text}\nBước {_bookingStep + 1} / 4 · {_selectedSeats.Count} ghế · Tạm tính: {_bookingTotal:N0} đ";
        }
        #endregion
    }
}
