using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Collections.Generic;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.Forms;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucDashboard : UserControl
    {
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        internal bool SkipAutomaticDataLoad { get; set; }

        private readonly Color _canvas = Color.FromArgb(243, 247, 249);
        private readonly Color _teal = Color.FromArgb(0, 169, 130);
        private readonly Color _ink = Color.FromArgb(25, 39, 50);
        private readonly Color _muted = Color.FromArgb(111, 126, 137);
        private TableLayoutPanel? _screen;
        private BarChartPanel? _weeklyChart;
        private BarChartPanel? _movieChart;
        private FlowLayoutPanel? _movieCards;
        private DataGridView? _showGrid;
        private ComboBox? _showPicker;
        private FlowLayoutPanel? _seatGrid;
        private Label? _seatLegend;
        private Label? _dateText;
        private Label? _revenueValue;
        private Label? _monthValue;
        private Label? _ticketsValue;
        private Label? _moviesValue;

        private sealed record ChartValue(string Label, decimal Value, string? FullLabel = null);
        private sealed record ShowOption(string Id, string Room, string Name)
        {
            public override string ToString() => Name;
        }

        public ucDashboard()
        {
            InitializeComponent();
            BuildReferenceDashboard();
            this.Load += (_, _) =>
            {
                if (SkipAutomaticDataLoad || DesignTimeHelper.IsDesignTime(this)) return;
                if (!string.IsNullOrWhiteSpace(CurrentUser.HoTen)) lblUserHeader.Text = CurrentUser.HoTen;
                LoadDashboardData();
            };
        }

        private void BuildReferenceDashboard()
        {
            pnlMain.Visible = false;
            BackColor = _canvas;
            Controls.Remove(pnlMain);

            _screen = new TableLayoutPanel
            {
                Name = "dashboardLayout",
                Dock = DockStyle.Fill,
                BackColor = _canvas,
                Padding = new Padding(18, 14, 18, 14),
                ColumnCount = 1,
                RowCount = 5,
                AutoScroll = true,
                GrowStyle = TableLayoutPanelGrowStyle.AddRows
            };
            _screen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            _screen.RowStyles.Add(new RowStyle(SizeType.Absolute, 66));
            _screen.RowStyles.Add(new RowStyle(SizeType.Absolute, 96));
            _screen.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
            _screen.RowStyles.Add(new RowStyle(SizeType.Absolute, 274));
            _screen.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));

            _screen.Controls.Add(BuildHeader(), 0, 0);
            _screen.Controls.Add(BuildKpis(), 0, 1);
            _screen.Controls.Add(BuildAnalytics(), 0, 2);
            _screen.Controls.Add(BuildOperations(), 0, 3);
            _screen.Controls.Add(BuildQuickActions(), 0, 4);
            Controls.Add(_screen);
            _screen.BringToFront();
        }

        private Control BuildHeader()
        {
            var header = new Panel { Dock = DockStyle.Fill, BackColor = _canvas, Padding = new Padding(2, 3, 0, 0) };
            var title = MakeLabel("Chào mừng trở lại, Admin!", 17, true, _ink);
            string userName = string.IsNullOrWhiteSpace(CurrentUser.HoTen) ? "Admin" : CurrentUser.HoTen;
            title.Text = $"Chào mừng trở lại, {userName}!";
            title.Location = new Point(0, 1);
            title.AutoSize = true;
            header.Controls.Add(title);

            var subtitle = MakeLabel("Hệ thống quản lý rạp chiếu phim", 9, false, _muted);
            subtitle.Location = new Point(1, 33);
            subtitle.AutoSize = true;
            header.Controls.Add(subtitle);

            _dateText = MakeLabel(DateTime.Now.ToString("dddd, dd/MM/yyyy   HH:mm", CultureInfo.GetCultureInfo("vi-VN")), 9, true, _ink);
            _dateText.AutoSize = false;
            _dateText.TextAlign = ContentAlignment.MiddleCenter;
            _dateText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _dateText.Size = new Size(225, 36);
            _dateText.Location = new Point(Math.Max(0, header.Width - 240), 9);
            header.Controls.Add(_dateText);
            header.Resize += (_, _) => _dateText.Location = new Point(Math.Max(0, header.ClientSize.Width - _dateText.Width), 9);

            btnRefresh.Visible = true;
            btnRefresh.Text = "↻  Làm mới";
            btnRefresh.Size = new Size(104, 36);
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(Math.Max(0, header.Width - 350), 9);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.BackColor = _teal;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            header.Controls.Add(btnRefresh);
            header.Resize += (_, _) => btnRefresh.Location = new Point(Math.Max(0, header.ClientSize.Width - 350), 9);
            btnRefresh.Click -= BtnRefresh_Click;
            btnRefresh.Click += BtnRefresh_Click;
            return header;
        }

        private Control BuildKpis()
        {
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, BackColor = _canvas, Padding = new Padding(0, 4, 0, 2) };
            for (int i = 0; i < 4; i++) layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            _revenueValue = AddKpi(layout, 0, "TỔNG DOANH THU", "0 ₫", "₫", Color.FromArgb(0, 169, 130));
            _monthValue = AddKpi(layout, 1, "DOANH THU THÁNG NÀY", "0 ₫", "◷", Color.FromArgb(55, 145, 219));
            _ticketsValue = AddKpi(layout, 2, "TỔNG VÉ ĐÃ BÁN", "0", "▤", Color.FromArgb(139, 101, 211));
            _moviesValue = AddKpi(layout, 3, "PHIM ĐANG CHIẾU", "0", "▣", Color.FromArgb(240, 151, 44));
            return layout;
        }

        private Label AddKpi(TableLayoutPanel layout, int index, string caption, string valueText, string iconText, Color accent)
        {
            var card = CardPanel(new Padding(13, 10, 12, 8));
            card.Margin = new Padding(index == 0 ? 0 : 5, 0, index == 3 ? 0 : 5, 0);
            var captionLabel = MakeLabel(caption, 8.3F, true, _muted);
            captionLabel.Dock = DockStyle.Top;
            captionLabel.Height = 22;
            card.Controls.Add(captionLabel);
            var icon = MakeLabel(iconText, 17, true, accent);
            icon.Dock = DockStyle.Right;
            icon.Width = 32;
            icon.TextAlign = ContentAlignment.MiddleCenter;
            card.Controls.Add(icon);
            var value = MakeLabel(valueText, 15, true, _ink);
            value.Dock = DockStyle.Fill;
            value.TextAlign = ContentAlignment.MiddleLeft;
            card.Controls.Add(value);
            layout.Controls.Add(card, index, 0);
            return value;
        }

        private Control BuildAnalytics()
        {
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, BackColor = _canvas };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var weeklyCard = CardPanel(new Padding(14, 10, 12, 8));
            weeklyCard.Margin = new Padding(0, 0, 5, 0);
            weeklyCard.Controls.Add(SectionHeading("Doanh thu 7 ngày qua", "Theo tổng thanh toán", "▥"));
            _weeklyChart = new BarChartPanel { Dock = DockStyle.Fill, BackColor = Color.White, BarColor = _teal, FormatValue = FormatCompactMoney };
            _weeklyChart.Values = new[]
            {
                new ChartValue("24/10", 3_200_000), new ChartValue("25/10", 4_500_000),
                new ChartValue("26/10", 3_800_000), new ChartValue("27/10", 5_100_000),
                new ChartValue("28/10", 5_400_000), new ChartValue("29/10", 6_200_000),
                new ChartValue("30/10", 7_800_000)
            };
            weeklyCard.Controls.Add(_weeklyChart);
            layout.Controls.Add(weeklyCard, 0, 0);

            var rankingCard = CardPanel(new Padding(10, 10, 10, 8));
            rankingCard.Margin = new Padding(5, 0, 5, 0);
            rankingCard.Controls.Add(SectionHeading("Top doanh thu phim", "Vé đã thanh toán", "▥"));
            _movieChart = new BarChartPanel { Dock = DockStyle.Fill, BackColor = Color.White, BarColor = Color.FromArgb(89, 130, 220), FormatValue = FormatCompactMoney, CompactLabels = true };
            _movieChart.Values = new[]
            {
                new ChartValue("Dune 2", 18_500_000), new ChartValue("Joker 2", 15_200_000),
                new ChartValue("Deadpool", 12_700_000), new ChartValue("Inside Out", 9_100_000),
                new ChartValue("Venom 3", 6_400_000)
            };
            rankingCard.Controls.Add(_movieChart);
            layout.Controls.Add(rankingCard, 1, 0);

            var moviesCard = CardPanel(new Padding(12, 10, 10, 8));
            moviesCard.Margin = new Padding(5, 0, 0, 0);
            moviesCard.Controls.Add(SectionHeading("Phim đang chiếu", "Suất trong 7 ngày tới", "›"));
            _movieCards = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, Padding = new Padding(0, 5, 0, 0), BackColor = Color.White };
            moviesCard.Controls.Add(_movieCards);
            layout.Controls.Add(moviesCard, 2, 0);
            return layout;
        }

        private Control BuildOperations()
        {
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, BackColor = _canvas, Padding = new Padding(0, 10, 0, 0) };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var scheduleCard = CardPanel(new Padding(12, 10, 12, 10));
            scheduleCard.Margin = new Padding(0, 0, 5, 0);
            scheduleCard.Controls.Add(SectionHeading("Suất chiếu sắp tới", "Lịch trong 7 ngày", "+"));
            _showGrid = new DataGridView
            {
                Name = "dgvDashboardShowtimes", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
                AllowUserToDeleteRows = false, AllowUserToResizeRows = false, RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None, CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(235, 239, 242), EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 30, RowTemplate = { Height = 27 },
                DefaultCellStyle = { Font = new Font("Segoe UI", 8.5F), SelectionBackColor = Color.FromArgb(222, 244, 238), SelectionForeColor = Color.FromArgb(35, 55, 65) },
                ColumnHeadersDefaultCellStyle = { BackColor = Color.FromArgb(247, 249, 250), ForeColor = Color.FromArgb(78, 91, 100), Font = new Font("Segoe UI", 8.3F, FontStyle.Bold) }
            };
            _showGrid.Columns.Add("Film", "Phim");
            _showGrid.Columns.Add("Room", "Phòng");
            _showGrid.Columns.Add("Date", "Ngày");
            _showGrid.Columns.Add("Time", "Giờ");
            scheduleCard.Controls.Add(_showGrid);
            layout.Controls.Add(scheduleCard, 0, 0);

            var seatsCard = CardPanel(new Padding(12, 10, 12, 10));
            seatsCard.Margin = new Padding(5, 0, 0, 0);
            var seatHeading = new TableLayoutPanel { Dock = DockStyle.Top, Height = 38, ColumnCount = 2, RowCount = 1, BackColor = Color.White };
            seatHeading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54));
            seatHeading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46));
            var seatTitle = MakeLabel("Sơ đồ ghế", 10.5F, true, _ink);
            seatTitle.Dock = DockStyle.Fill;
            seatTitle.TextAlign = ContentAlignment.MiddleLeft;
            seatHeading.Controls.Add(seatTitle, 0, 0);
            _showPicker = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 8.2F), Margin = new Padding(4) };
            _showPicker.SelectedIndexChanged += (_, _) => RefreshSeatMap();
            seatHeading.Controls.Add(_showPicker, 1, 0);
            seatsCard.Controls.Add(seatHeading);
            var screenLabel = MakeLabel("▰  MÀN HÌNH", 8, true, Color.FromArgb(144, 157, 166));
            screenLabel.Dock = DockStyle.Top;
            screenLabel.Height = 26;
            screenLabel.TextAlign = ContentAlignment.MiddleCenter;
            seatsCard.Controls.Add(screenLabel);
            _seatGrid = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, Padding = new Padding(3, 4, 3, 4), BackColor = Color.White };
            seatsCard.Controls.Add(_seatGrid);
            _seatLegend = MakeLabel("■ Trống     ■ Đã đặt     ■ Đang giữ", 8, false, _muted);
            _seatLegend.Dock = DockStyle.Bottom;
            _seatLegend.Height = 24;
            _seatLegend.TextAlign = ContentAlignment.MiddleCenter;
            seatsCard.Controls.Add(_seatLegend);
            layout.Controls.Add(seatsCard, 1, 0);
            return layout;
        }

        private Control BuildQuickActions()
        {
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, BackColor = _canvas, Padding = new Padding(0, 10, 0, 0) };
            for (int i = 0; i < 4; i++) layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            AddQuickAction(layout, 0, "▣   Thêm phim mới", Color.FromArgb(0, 169, 130), () => (FindForm() as FormMain)?.ShowPhim());
            AddQuickAction(layout, 1, "◷   Thêm suất chiếu", Color.FromArgb(42, 139, 217), () => (FindForm() as FormMain)?.ShowSuatChieu());
            AddQuickAction(layout, 2, "▦   Quản lý phòng chiếu", Color.FromArgb(132, 95, 211), () => (FindForm() as FormMain)?.ShowPhongChieu());
            AddQuickAction(layout, 3, "♙   Quản lý nhân viên", Color.FromArgb(240, 151, 44), () => (FindForm() as FormMain)?.ShowNhanVien());
            return layout;
        }

        private void AddQuickAction(TableLayoutPanel parent, int column, string text, Color accent, Action action)
        {
            var button = new Button
            {
                Text = text, Dock = DockStyle.Fill, Height = 42, Margin = new Padding(column == 0 ? 0 : 5, 0, column == 3 ? 0 : 5, 0),
                BackColor = Color.White, ForeColor = _ink, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8.7F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(9, 0, 4, 0), Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderColor = Color.FromArgb(230, 235, 238);
            button.FlatAppearance.BorderSize = 1;
            button.Click += (_, _) => action();
            parent.Controls.Add(button, column, 0);
        }

        private Panel CardPanel(Padding padding) => new()
        {
            Dock = DockStyle.Fill, BackColor = Color.White, Padding = padding,
            BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(0)
        };

        private Control SectionHeading(string title, string subtitle, string glyph)
        {
            var panel = new Panel { Dock = DockStyle.Top, Height = 43, BackColor = Color.White };
            var name = MakeLabel(title, 10, true, _ink);
            name.AutoSize = true;
            name.Location = new Point(0, 0);
            panel.Controls.Add(name);
            var hint = MakeLabel(subtitle, 8, false, _muted);
            hint.AutoSize = true;
            hint.Location = new Point(0, 22);
            panel.Controls.Add(hint);
            var icon = MakeLabel(glyph, 13, true, _teal);
            icon.Dock = DockStyle.Right;
            icon.Width = 26;
            icon.TextAlign = ContentAlignment.TopRight;
            panel.Controls.Add(icon);
            return panel;
        }

        private Label MakeLabel(string text, float size, bool bold, Color color) => new()
        {
            Text = text, Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular),
            ForeColor = color, BackColor = Color.Transparent, AutoEllipsis = true
        };

        private void BtnRefresh_Click(object? sender, EventArgs e) => LoadDashboardData();

        public void LoadDashboardData()
        {
            if (DesignTimeHelper.IsDesignTime(this)) return;
            try
            {
                var kpi = Function.Query(@"
                    SELECT
                        (SELECT ISNULL(SUM(TongTien),0) FROM HoaDon WHERE TrangThaiThanhToan='Paid') AS TongDoanhThu,
                        (SELECT ISNULL(SUM(TongTien),0) FROM HoaDon WHERE TrangThaiThanhToan='Paid' AND NgayLap >= DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1) AND NgayLap < DATEADD(MONTH,1,DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1))) AS DoanhThuThang,
                        (SELECT ISNULL(SUM(SoLuongVe),0) FROM HoaDon WHERE TrangThaiThanhToan='Paid') AS TongVe,
                        (SELECT COUNT(DISTINCT MaPhim) FROM SuatChieu WHERE NgayChieu >= CAST(GETDATE() AS date) AND NgayChieu < DATEADD(DAY,7,CAST(GETDATE() AS date))) AS PhimDangChieu");
                if (kpi.Rows.Count > 0)
                {
                    var row = kpi.Rows[0];
                    _revenueValue!.Text = $"{Convert.ToDecimal(row["TongDoanhThu"]):N0} ₫";
                    _monthValue!.Text = $"{Convert.ToDecimal(row["DoanhThuThang"]):N0} ₫";
                    _ticketsValue!.Text = $"{Convert.ToInt32(row["TongVe"]):N0} vé";
                    _moviesValue!.Text = $"{Convert.ToInt32(row["PhimDangChieu"]):N0} phim";
                }

                LoadWeeklyRevenue();
                LoadMovieRevenueRanking();
                LoadNowShowing();
                LoadUpcomingShowtimes();
                _dateText!.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy   HH:mm", CultureInfo.GetCultureInfo("vi-VN"));
            }
            catch (Exception ex)
            {
                _weeklyChart!.Values = Array.Empty<ChartValue>();
                _weeklyChart.Invalidate();
                _movieChart!.Values = Array.Empty<ChartValue>();
                _movieChart.Invalidate();
                MessageBox.Show($"Không thể tải dữ liệu tổng quan: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWeeklyRevenue()
        {
            var data = Function.Query(@"
                SELECT CAST(NgayLap AS date) AS Ngay, ISNULL(SUM(TongTien),0) AS DoanhThu
                FROM HoaDon
                WHERE TrangThaiThanhToan='Paid' AND NgayLap >= DATEADD(DAY,-6,CAST(GETDATE() AS date))
                GROUP BY CAST(NgayLap AS date)");
            var totals = data.Rows.Cast<DataRow>().ToDictionary(r => Convert.ToDateTime(r["Ngay"]).Date, r => Convert.ToDecimal(r["DoanhThu"]));
            var points = Enumerable.Range(0, 7).Select(offset =>
            {
                var day = DateTime.Today.AddDays(offset - 6);
                totals.TryGetValue(day, out decimal value);
                return new ChartValue(day.ToString("dd/MM"), value, day.ToString("dddd dd/MM", CultureInfo.GetCultureInfo("vi-VN")));
            }).ToArray();
            _weeklyChart!.Values = points;
            _weeklyChart.Invalidate();
        }

        private void LoadMovieRevenueRanking()
        {
            var data = Function.Query(@"
                SELECT TOP 5 p.TenPhim, ISNULL(SUM(hd.TienVe),0) AS DoanhThu
                FROM Phim p
                LEFT JOIN SuatChieu sc ON sc.MaPhim=p.MaPhim
                LEFT JOIN HoaDon hd ON hd.MaSuatChieu=sc.MaSuatChieu AND hd.TrangThaiThanhToan='Paid'
                GROUP BY p.MaPhim,p.TenPhim
                HAVING ISNULL(SUM(hd.TienVe),0)>0
                ORDER BY DoanhThu DESC");
            _movieChart!.Values = data.Rows.Cast<DataRow>()
                .Select(r => new ChartValue(ShortName(Convert.ToString(r["TenPhim"]) ?? "Phim"), Convert.ToDecimal(r["DoanhThu"]), Convert.ToString(r["TenPhim"])))
                .ToArray();
            _movieChart.Invalidate();
        }

        private void LoadNowShowing()
        {
            var data = Function.Query(@"
                SELECT TOP 3 p.MaPhim,p.TenPhim,p.TheLoai,p.ThoiLuong,p.PosterPath,MIN(sc.NgayChieu) AS NgayChieu
                FROM Phim p INNER JOIN SuatChieu sc ON sc.MaPhim=p.MaPhim
                WHERE sc.NgayChieu>=CAST(GETDATE() AS date) AND sc.NgayChieu<DATEADD(DAY,7,CAST(GETDATE() AS date))
                GROUP BY p.MaPhim,p.TenPhim,p.TheLoai,p.ThoiLuong,p.PosterPath
                ORDER BY MIN(sc.NgayChieu),p.TenPhim");
            _movieCards!.SuspendLayout();
            _movieCards.Controls.Clear();
            if (data.Rows.Count == 0)
                _movieCards.Controls.Add(MakeLabel("Chưa có phim xếp lịch trong 7 ngày tới.", 8.5F, false, _muted));

            foreach (DataRow row in data.Rows)
            {
                var film = new Panel { Width = Math.Max(200, _movieCards.ClientSize.Width - 24), Height = 58, BackColor = Color.White, Margin = new Padding(0, 0, 0, 5) };
                var poster = new PictureBox { Location = new Point(0, 0), Size = new Size(43, 56), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.FromArgb(239, 243, 245) };
                poster.Image = ImageAssetHelper.Load(Convert.ToString(row["PosterPath"]));
                if (poster.Image is null)
                {
                    string movieCode = Convert.ToString(row["MaPhim"]) ?? "";
                    poster.Image = ImageAssetHelper.Load(Path.Combine("Assets", "MoviePosters", movieCode + ".png"))
                        ?? ImageAssetHelper.Load(Path.Combine("Assets", "Movies", movieCode + ".jpg"));
                }
                film.Controls.Add(poster);
                var title = MakeLabel(Convert.ToString(row["TenPhim"]) ?? "Phim", 8.5F, true, _ink);
                title.Location = new Point(51, 1);
                title.Size = new Size(Math.Max(110, film.Width - 58), 24);
                title.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                film.Controls.Add(title);
                var meta = MakeLabel($"{Convert.ToString(row["TheLoai"])}  •  {Convert.ToInt32(row["ThoiLuong"])} phút", 7.5F, false, _muted);
                meta.Location = new Point(51, 27);
                meta.Size = new Size(Math.Max(110, film.Width - 58), 20);
                meta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                film.Controls.Add(meta);
                _movieCards.Controls.Add(film);
            }
            _movieCards.ResumeLayout();
        }

        private void LoadUpcomingShowtimes()
        {
            var data = Function.Query(@"
                SELECT TOP 10 sc.MaSuatChieu,sc.MaPhong,p.TenPhim,ph.TenPhong,sc.NgayChieu,sc.GioChieu,ph.SoHangGhe,ph.SoGheMoiHang
                FROM SuatChieu sc INNER JOIN Phim p ON p.MaPhim=sc.MaPhim INNER JOIN PhongChieu ph ON ph.MaPhong=sc.MaPhong
                WHERE sc.NgayChieu>=CAST(GETDATE() AS date) AND sc.NgayChieu<DATEADD(DAY,7,CAST(GETDATE() AS date))
                ORDER BY sc.NgayChieu,sc.GioChieu");
            _showGrid!.Rows.Clear();
            _showPicker!.Items.Clear();
            _showGrid.Tag = data;
            var options = new List<ShowOption>();
            foreach (DataRow row in data.Rows)
            {
                string id = Convert.ToString(row["MaSuatChieu"]) ?? "";
                string room = Convert.ToString(row["MaPhong"]) ?? "";
                string name = Convert.ToString(row["TenPhim"]) ?? "";
                DateTime date = Convert.ToDateTime(row["NgayChieu"]);
                string time = DateTime.Today.Add((TimeSpan)row["GioChieu"]).ToString("HH:mm");
                string roomName = Convert.ToString(row["TenPhong"]) ?? "—";
                _showGrid.Rows.Add(name, roomName, date.ToString("dd/MM"), time);
                options.Add(new ShowOption(id, room, $"{roomName} · {time}"));
            }
            if (data.Rows.Count == 0)
            {
                _showGrid.Rows.Add("Chưa có suất chiếu sắp tới", "—", "—", "—");
                _seatGrid!.Controls.Clear();
                _seatGrid.Controls.Add(MakeLabel("Chưa có suất để hiển thị sơ đồ ghế.", 8.5F, false, _muted));
                return;
            }
            _showPicker.Items.AddRange(options.Cast<object>().ToArray());
            if (_showPicker.Items.Count > 0) _showPicker.SelectedIndex = 0;
        }

        private void RefreshSeatMap()
        {
            if (_showPicker?.SelectedItem is not ShowOption show || _showGrid?.Tag is not DataTable data) return;
            DataRow? showRow = data.Rows.Cast<DataRow>().FirstOrDefault(r => Convert.ToString(r["MaSuatChieu"]) == show.Id);
            if (showRow is null) return;
            int rows = Math.Clamp(Convert.ToInt32(showRow["SoHangGhe"]), 1, 12);
            int columns = Math.Clamp(Convert.ToInt32(showRow["SoGheMoiHang"]), 1, 16);
            var occupied = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                var seats = Function.Query(@"
                    SELECT DISTINCT ct.MaGhe
                    FROM ChiTietHoaDon_Ghe ct INNER JOIN HoaDon hd ON hd.MaHD=ct.MaHD AND hd.MaSuatChieu=ct.MaSuatChieu
                    WHERE ct.MaSuatChieu=@MaSuatChieu AND hd.TrangThaiThanhToan='Paid'
                    UNION
                    SELECT gd.MaGhe FROM GheDatCho gd
                    WHERE gd.MaSuatChieu=@MaSuatChieu AND (gd.HetHan IS NULL OR gd.HetHan>GETDATE())",
                    new[] { DbArg.VarChar20("@MaSuatChieu", show.Id) });
                foreach (DataRow seat in seats.Rows) occupied.Add(Convert.ToString(seat["MaGhe"]) ?? "");
            }
            catch { /* A missing reservation table should not hide the rest of the dashboard. */ }

            _seatGrid!.SuspendLayout();
            _seatGrid.Controls.Clear();
            int seatSize = columns > 12 ? 19 : 23;
            for (int row = 0; row < rows; row++)
            {
                var seatRow = new FlowLayoutPanel { Width = columns * (seatSize + 3) + 45, Height = seatSize + 5, WrapContents = false, BackColor = Color.White, Margin = new Padding(0, 1, 0, 1) };
                var rowName = MakeLabel(((char)('A' + row)).ToString(), 7.5F, true, _muted);
                rowName.Size = new Size(18, seatSize);
                rowName.TextAlign = ContentAlignment.MiddleCenter;
                seatRow.Controls.Add(rowName);
                for (int col = 1; col <= columns; col++)
                {
                    string seatName = $"{(char)('A' + row)}{col}";
                    bool isTaken = occupied.Contains(seatName);
                    var seat = new Label
                    {
                        Text = "", Width = seatSize, Height = seatSize, Margin = new Padding(1),
                        BackColor = isTaken ? Color.FromArgb(231, 96, 83) : Color.FromArgb(24, 177, 112),
                        BorderStyle = BorderStyle.FixedSingle, AccessibleName = seatName,
                        AccessibleDescription = isTaken ? "Đã có vé" : "Còn trống"
                    };
                    _seatToolTip.SetToolTip(seat, $"Ghế {seatName}: {(isTaken ? "đã đặt" : "còn trống")}");
                    seatRow.Controls.Add(seat);
                }
                _seatGrid.Controls.Add(seatRow);
            }
            _seatLegend!.Text = $"■ Còn trống  {rows * columns - occupied.Count}       ■ Đã đặt  {occupied.Count}";
            _seatGrid.ResumeLayout();
        }

        private readonly ToolTip _seatToolTip = new() { InitialDelay = 250, ReshowDelay = 100, AutoPopDelay = 4000 };

        private static string ShortName(string name) => name.Length > 9 ? name[..8] + "…" : name;
        private static string FormatCompactMoney(decimal value) => value >= 1_000_000 ? $"{value / 1_000_000m:0.#}tr" : value >= 1_000 ? $"{value / 1_000m:0.#}k" : value.ToString("0");

        private sealed class BarChartPanel : Panel
        {
            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public ChartValue[] Values { get; set; } = Array.Empty<ChartValue>();
            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public Color BarColor { get; set; } = Color.MediumSeaGreen;
            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public Func<decimal, string> FormatValue { get; set; } = x => x.ToString("N0");
            [System.ComponentModel.Browsable(false)]
            [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public bool CompactLabels { get; set; }
            private readonly ToolTip _tip = new();

            public BarChartPanel()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
                Paint += PaintChart;
                MouseMove += ShowPointTooltip;
            }

            private void PaintChart(object? sender, PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);
                int left = CompactLabels ? 30 : 54;
                int right = 8;
                int top = 17;
                int bottom = CompactLabels ? 42 : 30;
                int plotWidth = Math.Max(10, ClientSize.Width - left - right);
                int plotHeight = Math.Max(10, ClientSize.Height - top - bottom);
                decimal max = Math.Max(1, Values.Length == 0 ? 0 : Values.Max(v => v.Value));
                using var gridPen = new Pen(Color.FromArgb(231, 236, 239));
                using var gridFont = new Font("Segoe UI", 7F);
                using var labelFont = new Font("Segoe UI", CompactLabels ? 6.8F : 7.7F);
                using var mutedBrush = new SolidBrush(Color.FromArgb(125, 139, 148));
                for (int line = 0; line <= 3; line++)
                {
                    int y = top + plotHeight * line / 3;
                    g.DrawLine(gridPen, left, y, left + plotWidth, y);
                    if (!CompactLabels)
                    {
                        decimal val = max * (3 - line) / 3m;
                        var text = FormatValue(val);
                        var measured = g.MeasureString(text, gridFont);
                        g.DrawString(text, gridFont, mutedBrush, Math.Max(0, left - measured.Width - 6), y - 7);
                    }
                }
                if (Values.Length == 0)
                {
                    using var emptyFont = new Font("Segoe UI", 8.5F);
                    var message = "Chưa có doanh thu đã thanh toán";
                    var size = g.MeasureString(message, emptyFont);
                    g.DrawString(message, emptyFont, mutedBrush, Math.Max(left, (ClientSize.Width - size.Width) / 2), top + plotHeight / 2 - 8);
                    return;
                }
                float slot = plotWidth / (float)Values.Length;
                float barWidth = Math.Clamp(slot * (CompactLabels ? 0.48f : 0.58f), 8f, CompactLabels ? 24f : 44f);
                using var barBrush = new SolidBrush(BarColor);
                using var valueBrush = new SolidBrush(Color.FromArgb(74, 88, 98));
                for (int i = 0; i < Values.Length; i++)
                {
                    var point = Values[i];
                    float h = (float)(plotHeight * point.Value / max);
                    float x = left + slot * i + (slot - barWidth) / 2;
                    float y = top + plotHeight - h;
                    if (h > 0)
                    {
                        using var path = RoundedBar(new RectangleF(x, y, barWidth, h), Math.Min(5, barWidth / 3));
                        g.FillPath(barBrush, path);
                    }
                    var label = point.Label;
                    if (CompactLabels)
                    {
                        string valueLabel = FormatValue(point.Value);
                        var valueSize = g.MeasureString(valueLabel, labelFont);
                        g.DrawString(valueLabel, labelFont, valueBrush, x + (barWidth - valueSize.Width) / 2, Math.Max(0, y - valueSize.Height - 1));
                    }
                    var labelSize = g.MeasureString(label, labelFont);
                    g.DrawString(label, labelFont, mutedBrush, left + slot * i + (slot - labelSize.Width) / 2, top + plotHeight + 5);
                }
            }

            private void ShowPointTooltip(object? sender, MouseEventArgs e)
            {
                if (Values.Length == 0) return;
                int left = CompactLabels ? 30 : 54;
                int right = 8;
                float slot = Math.Max(1, ClientSize.Width - left - right) / (float)Values.Length;
                int index = Math.Clamp((int)((e.X - left) / slot), 0, Values.Length - 1);
                var point = Values[index];
                string label = point.FullLabel ?? point.Label;
                _tip.SetToolTip(this, $"{label}: {point.Value:N0} ₫");
            }

            private static GraphicsPath RoundedBar(RectangleF rect, float radius)
            {
                var path = new GraphicsPath();
                if (rect.Height < radius * 2) radius = Math.Max(1, rect.Height / 2);
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom);
                path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
                path.AddLine(rect.X, rect.Bottom, rect.X, rect.Y + radius);
                path.CloseFigure();
                return path;
            }
        }

        private void lblKpiTicketsTitle_Click(object sender, EventArgs e)
        {
            // The label is decorative; the dashboard data is refreshed using the toolbar button.
        }
    }
}
