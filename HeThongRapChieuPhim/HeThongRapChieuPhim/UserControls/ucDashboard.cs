using HeThongRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucDashboard : UserControl
    {
        private Label lblTotalRevenue = null!;
        private Label lblMonthRevenue = null!;
        private Label lblTotalTickets = null!;
        private Label lblTotalMovies = null!;

        public ucDashboard()
        {
            InitializeComponent();
            StyleBottomCard();
            ThemeHelper.StyleDataGridView(dgvTopMovies);
            SetupRankingBadgePainting();
            InitKpiCards();

            this.Load += (s, e) =>
            {
                if (!string.IsNullOrEmpty(CurrentUser.HoTen))
                {
                    lblUserHeader.Text = $"{CurrentUser.HoTen} ∨";
                }
                LoadDashboardData();
            };
        }

        private void StyleBottomCard()
        {
            pnlBottom.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new RectangleF(0, 0, pnlBottom.Width - 1, pnlBottom.Height - 1);
                using var path = ThemeHelper.CreateRoundedPath(rect, 14);
                using var brush = new SolidBrush(Color.White);
                g.FillPath(brush, path);
                using var pen = new Pen(ThemeHelper.BorderColor, 1.2f);
                g.DrawPath(pen, path);

                // Draw small stylized hourglass vector icon
                int hx = 16, hy = 20;
                using var hPen = new Pen(ThemeHelper.PrimaryDark, 1.5f);
                using var hBrush = new SolidBrush(Color.FromArgb(120, ThemeHelper.PrimaryGreen));
                g.DrawLine(hPen, hx, hy, hx + 12, hy);
                g.DrawLine(hPen, hx, hy + 14, hx + 12, hy + 14);
                var topTri = new Point[] { new Point(hx + 1, hy), new Point(hx + 11, hy), new Point(hx + 6, hy + 7) };
                var botTri = new Point[] { new Point(hx + 6, hy + 7), new Point(hx + 1, hy + 14), new Point(hx + 11, hy + 14) };
                g.DrawPolygon(hPen, topTri);
                g.DrawPolygon(hPen, botTri);
                g.FillPolygon(hBrush, botTri);
            };
        }

        private void SetupRankingBadgePainting()
        {
            dgvTopMovies.CellPainting += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvTopMovies.Columns[e.ColumnIndex].Name == "Hạng")
                {
                    e.PaintBackground(e.CellBounds, true);
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    string rankText = e.Value?.ToString() ?? "";
                    int.TryParse(rankText, out int rankNum);

                    // Badge pill
                    int badgeW = 28;
                    int badgeH = 24;
                    int bx = e.CellBounds.X + (e.CellBounds.Width - badgeW) / 2;
                    int by = e.CellBounds.Y + (e.CellBounds.Height - badgeH) / 2;
                    var bRect = new RectangleF(bx, by, badgeW, badgeH);

                    Color bgPill = rankNum <= 3 ? Color.FromArgb(220, 252, 231) : Color.FromArgb(240, 245, 241);
                    Color textPill = rankNum <= 3 ? Color.FromArgb(21, 128, 61) : ThemeHelper.TextMuted;

                    using (var path = ThemeHelper.CreateRoundedPath(bRect, 8))
                    using (var brushBg = new SolidBrush(bgPill))
                    using (var brushText = new SolidBrush(textPill))
                    using (var font = new Font("Segoe UI", 9f, FontStyle.Bold))
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.FillPath(brushBg, path);
                        g.DrawString(rankText, font, brushText, bRect, sf);
                    }

                    e.Handled = true;
                }
            };
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void InitKpiCards()
        {
            lblTotalRevenue = new Label();
            lblMonthRevenue = new Label();
            lblTotalTickets = new Label();
            lblTotalMovies = new Label();

            pnlCards.Controls.Clear();
            pnlCards.Controls.Add(CreateKpiCard("TỔNG DOANH THU", lblTotalRevenue, KpiIconHelper.GetCoinsIcon()), 0, 0);
            pnlCards.Controls.Add(CreateKpiCard("DOANH THU THÁNG NÀY", lblMonthRevenue, KpiIconHelper.GetLogoEmblemIcon()), 1, 0);
            pnlCards.Controls.Add(CreateKpiCard("TỔNG VÉ ĐÃ BÁN", lblTotalTickets, KpiIconHelper.GetTicketIcon()), 2, 0);
            pnlCards.Controls.Add(CreateKpiCard("PHIM ĐANG CHIẾU", lblTotalMovies, KpiIconHelper.GetClapperIcon()), 3, 0);
        }

        private Panel CreateKpiCard(string title, Label lblValue, Image? iconImage)
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(6, 4, 6, 4)
            };

            var lblT = new Label
            {
                Text = title,
                ForeColor = ThemeHelper.TextMuted,
                Font = new Font("Segoe UI", 8.2f, FontStyle.Bold),
                Location = new Point(14, 12),
                Size = new Size(200, 20),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblValue.Text = "...";
            lblValue.ForeColor = ThemeHelper.PrimaryDark;
            lblValue.Font = new Font("Segoe UI", 15.5f, FontStyle.Bold);
            lblValue.Location = new Point(14, 38);
            lblValue.Size = new Size(185, 36);
            lblValue.TextAlign = ContentAlignment.MiddleLeft;
            lblValue.BackColor = Color.Transparent;

            pnl.Resize += (s, e) =>
            {
                lblT.Width = Math.Max(160, pnl.Width - 24);
                lblValue.Width = Math.Max(120, pnl.Width - 68);
            };

            pnl.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var rect = new RectangleF(0, 0, pnl.Width - 1, pnl.Height - 1);
                using var path = ThemeHelper.CreateRoundedPath(rect, 14);
                using var brush = new SolidBrush(Color.White);
                g.FillPath(brush, path);
                using var pen = new Pen(ThemeHelper.BorderColor, 1.2f);
                g.DrawPath(pen, path);

                // Draw decorative 3D icon on the right side of the card
                if (iconImage != null)
                {
                    int iconSize = Math.Min(50, pnl.Height - 24);
                    int iconX = pnl.Width - iconSize - 14;
                    int iconY = (pnl.Height - iconSize) / 2 + 6;
                    g.DrawImage(iconImage, new Rectangle(iconX, iconY, iconSize, iconSize));
                }
            };

            pnl.Controls.Add(lblValue);
            pnl.Controls.Add(lblT);
            return pnl;
        }

        public void LoadDashboardData()
        {
            try
            {
                // 1. KPI Queries
                string qKpi = @"
                    SELECT 
                        (SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE TrangThaiThanhToan='Paid') AS TongDoanhThu,
                        (SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE TrangThaiThanhToan='Paid' AND MONTH(NgayLap) = MONTH(GETDATE()) AND YEAR(NgayLap) = YEAR(GETDATE())) AS DoanhThuThang,
                        (SELECT ISNULL(SUM(SoLuongVe), 0) FROM HoaDon WHERE TrangThaiThanhToan='Paid') AS TongVe,
                        (SELECT COUNT(*) FROM Phim) AS TongPhim";

                var dtKpi = Function.Query(qKpi);
                if (dtKpi.Rows.Count > 0)
                {
                    var r = dtKpi.Rows[0];
                    decimal totalRev = Convert.ToDecimal(r["TongDoanhThu"]);
                    decimal monthRev = Convert.ToDecimal(r["DoanhThuThang"]);
                    int totalTickets = Convert.ToInt32(r["TongVe"]);
                    int totalMovies = Convert.ToInt32(r["TongPhim"]);

                    lblTotalRevenue.Text = $"{totalRev:N0} đ";
                    lblMonthRevenue.Text = $"{monthRev:N0} đ";
                    lblTotalTickets.Text = $"{totalTickets:N0} vé";
                    lblTotalMovies.Text = $"{totalMovies:N0} phim";
                }

                // 2. Monthly Revenue Chart (Bar)
                string qMonthly = @"
                    SELECT 
                        FORMAT(NgayLap, 'MM/yyyy') AS Thang,
                        YEAR(NgayLap) AS Nam,
                        MONTH(NgayLap) AS ThangSo,
                        ISNULL(SUM(TongTien), 0) AS DoanhThu
                    FROM HoaDon
                    WHERE TrangThaiThanhToan='Paid' AND NgayLap >= DATEADD(MONTH, -6, GETDATE())
                    GROUP BY FORMAT(NgayLap, 'MM/yyyy'), YEAR(NgayLap), MONTH(NgayLap)
                    ORDER BY Nam ASC, ThangSo ASC";

                var dtMonthly = Function.Query(qMonthly);
                var salesByMonth = new Dictionary<string, decimal>();
                foreach (DataRow r in dtMonthly.Rows)
                {
                    string m = r["Thang"].ToString() ?? "";
                    salesByMonth[m] = Convert.ToDecimal(r["DoanhThu"]);
                }

                var monthlyData = new List<ChartDataItem>();
                var now = DateTime.Now;
                for (int i = 5; i >= 0; i--)
                {
                    var targetDate = now.AddMonths(-i);
                    string key = targetDate.ToString("MM/yyyy");
                    string label = "T" + targetDate.ToString("MM/yy");
                    decimal val = salesByMonth.ContainsKey(key) ? salesByMonth[key] : 0;
                    monthlyData.Add(new ChartDataItem(label, val, Color.FromArgb(88, 145, 112)));
                }

                chartMonthly.SetData(monthlyData);

                // 3. Movie Revenue Share Chart (Doughnut)
                string qMovie = @"
                    SELECT TOP 5
                        p.TenPhim,
                        ISNULL(SUM(hd.TongTien), 0) AS DoanhThu
                    FROM Phim p
                    LEFT JOIN SuatChieu sc ON p.MaPhim = sc.MaPhim
                    LEFT JOIN HoaDon hd ON sc.MaSuatChieu = hd.MaSuatChieu AND hd.TrangThaiThanhToan='Paid'
                    GROUP BY p.TenPhim
                    HAVING ISNULL(SUM(hd.TongTien), 0) > 0
                    ORDER BY DoanhThu DESC";

                var dtMovie = Function.Query(qMovie);
                var movieData = new List<ChartDataItem>();
                Color[] colors = {
                    Color.FromArgb(88, 130, 105),
                    Color.FromArgb(133, 182, 149),
                    Color.FromArgb(183, 217, 195),
                    Color.FromArgb(168, 163, 102),
                    Color.FromArgb(55, 95, 72)
                };

                int ci = 0;
                foreach (DataRow r in dtMovie.Rows)
                {
                    string name = r["TenPhim"]?.ToString() ?? "";
                    if (name.Length > 16) name = name.Substring(0, 14) + "..";
                    movieData.Add(new ChartDataItem(
                        name,
                        Convert.ToDecimal(r["DoanhThu"]),
                        colors[ci % colors.Length]
                    ));
                    ci++;
                }

                if (movieData.Count == 0)
                {
                    movieData.Add(new ChartDataItem("Chưa có số liệu", 0, Color.FromArgb(88, 130, 105)));
                }
                chartMovie.SetData(movieData);

                // 4. Top Movies Grid
                string qTop = @"
                    SELECT TOP 10
                        ROW_NUMBER() OVER(ORDER BY ISNULL(SUM(hd.TongTien), 0) DESC) AS [Hạng],
                        p.MaPhim AS [Mã Phim],
                        p.TenPhim AS [Tên Phim],
                        p.TheLoai AS [Thể Loại],
                        COUNT(DISTINCT hd.MaHD) AS [Số Hóa Đơn],
                        ISNULL(SUM(hd.SoLuongVe), 0) AS [Số Vé Bán],
                        ISNULL(SUM(hd.TongTien), 0) AS [Tổng Doanh Thu (VNĐ)]
                    FROM Phim p
                    LEFT JOIN SuatChieu sc ON p.MaPhim = sc.MaPhim
                    LEFT JOIN HoaDon hd ON sc.MaSuatChieu = hd.MaSuatChieu AND hd.TrangThaiThanhToan='Paid'
                    GROUP BY p.MaPhim, p.TenPhim, p.TheLoai
                    ORDER BY [Tổng Doanh Thu (VNĐ)] DESC";

                var dtTop = Function.Query(qTop);
                dgvTopMovies.DataSource = dtTop;

                if (dgvTopMovies.Columns["Tổng Doanh Thu (VNĐ)"] != null)
                {
                    dgvTopMovies.Columns["Tổng Doanh Thu (VNĐ)"].DefaultCellStyle.Format = "N0";
                    dgvTopMovies.Columns["Tổng Doanh Thu (VNĐ)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTopMovies.Columns["Tổng Doanh Thu (VNĐ)"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
                if (dgvTopMovies.Columns["Số Vé Bán"] != null)
                {
                    dgvTopMovies.Columns["Số Vé Bán"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvTopMovies.Columns["Số Hóa Đơn"] != null)
                {
                    dgvTopMovies.Columns["Số Hóa Đơn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvTopMovies.Columns["Hạng"] != null)
                {
                    try
                    {
                        dgvTopMovies.Columns["Hạng"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvTopMovies.Columns["Hạng"].Width = 65;
                    }
                    catch { }
                    dgvTopMovies.Columns["Hạng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp Dashboard: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
