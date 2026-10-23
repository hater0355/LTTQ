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

        public ucDashboard()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvTopMovies);
            ThemeHelper.StyleDataGridView(dgvMonthlyRevenue);
            ThemeHelper.StyleDataGridView(dgvMovieRevenue);
            dgvMonthlyRevenue.CellPainting += RevenueGrid_CellPainting;
            dgvMovieRevenue.CellPainting += RevenueGrid_CellPainting;
            if (dgvMonthlyRevenue.Columns["colMonthRevenue"] is { } monthColumn)
                monthColumn.DefaultCellStyle.Format = "N0";
            if (dgvMovieRevenue.Columns["colMovieRevenue"] is { } movieRevenueColumn)
                movieRevenueColumn.DefaultCellStyle.Format = "N0";

            this.Load += (s, e) =>
            {
                if (DesignTimeHelper.IsDesignTime(this)) return;
                if (!string.IsNullOrEmpty(CurrentUser.HoTen))
                {
                    lblUserHeader.Text = $"{CurrentUser.HoTen} ∨";
                }
                LoadDashboardData();
            };
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void RevenueGrid_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (sender is not DataGridView grid || e.RowIndex < 0 || e.ColumnIndex < 0 ||
                e.ColumnIndex != grid.Columns["colMonthRevenue"]?.Index &&
                e.ColumnIndex != grid.Columns["colMovieRevenue"]?.Index)
                return;
            if (e.Graphics is null || e.CellStyle is null) return;

            e.PaintBackground(e.CellBounds, true);
            if (e.CellBounds.Width < 90 || !decimal.TryParse(Convert.ToString(e.Value), out decimal value))
            {
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
                return;
            }

            decimal maxValue = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (decimal.TryParse(Convert.ToString(row.Cells[e.ColumnIndex].Value), out decimal rowValue))
                    maxValue = Math.Max(maxValue, rowValue);
            }

            int padding = 8;
            int trackWidth = Math.Max(20, (int)((e.CellBounds.Width - padding * 3) * 0.55));
            int barWidth = maxValue <= 0 ? 0 : (int)(trackWidth * Math.Clamp(value / maxValue, 0m, 1m));
            int barHeight = 12;
            int barY = e.CellBounds.Top + (e.CellBounds.Height - barHeight) / 2;
            var track = new Rectangle(e.CellBounds.Left + padding, barY, trackWidth, barHeight);
            var bar = new Rectangle(track.Left, track.Top, barWidth, barHeight);
            using (var trackBrush = new SolidBrush(Color.FromArgb(235, 239, 243)))
            using (var barBrush = new SolidBrush(ReferenceEquals(grid, dgvMonthlyRevenue)
                ? Color.FromArgb(38, 116, 179)
                : Color.FromArgb(194, 133, 31)))
            {
                e.Graphics.FillRectangle(trackBrush, track);
                if (bar.Width > 0) e.Graphics.FillRectangle(barBrush, bar);
            }

            string label = value.ToString("N0");
            var textBounds = new Rectangle(track.Right + padding, e.CellBounds.Top, Math.Max(0, e.CellBounds.Right - track.Right - padding * 2), e.CellBounds.Height);
            TextRenderer.DrawText(e.Graphics, label, e.CellStyle.Font, textBounds, e.CellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            e.Handled = true;
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

                    lblKpiRevenue.Text = $"{totalRev:N0} đ";
                    lblKpiMonth.Text = $"{monthRev:N0} đ";
                    lblKpiTickets.Text = $"{totalTickets:N0} vé";
                    lblKpiMovies.Text = $"{totalMovies:N0} phim";
                }

                // 2. Monthly Revenue Chart (Bar)
                string qMonthly = @"
                    SELECT 
                        FORMAT(NgayLap, 'MM/yyyy') AS Thang,
                        YEAR(NgayLap) AS Nam,
                        MONTH(NgayLap) AS ThangSo,
                        ISNULL(SUM(TienVe), 0) AS DoanhThu
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

                dgvMonthlyRevenue.Rows.Clear();
                var now = DateTime.Now;
                for (int i = 5; i >= 0; i--)
                {
                    var targetDate = now.AddMonths(-i);
                    string key = targetDate.ToString("MM/yyyy");
                    string label = "T" + targetDate.ToString("MM/yy");
                    decimal val = salesByMonth.ContainsKey(key) ? salesByMonth[key] : 0;
                    dgvMonthlyRevenue.Rows.Add(label, val);
                }

                // 3. Gross ticket value by movie, excluding food and before discounts.
                string qMovie = @"
                    SELECT TOP 5
                        p.TenPhim,
                        ISNULL(SUM(hd.TienVe), 0) AS DoanhThu
                    FROM Phim p
                    LEFT JOIN SuatChieu sc ON p.MaPhim = sc.MaPhim
                    LEFT JOIN HoaDon hd ON sc.MaSuatChieu = hd.MaSuatChieu AND hd.TrangThaiThanhToan='Paid'
                    GROUP BY p.TenPhim
                    HAVING ISNULL(SUM(hd.TongTien), 0) > 0
                    ORDER BY DoanhThu DESC";

                var dtMovie = Function.Query(qMovie);
                dgvMovieRevenue.Rows.Clear();
                foreach (DataRow r in dtMovie.Rows)
                {
                    string name = r["TenPhim"]?.ToString() ?? "";
                    if (name.Length > 16) name = name.Substring(0, 14) + "..";
                    dgvMovieRevenue.Rows.Add(name, Convert.ToDecimal(r["DoanhThu"]));
                }

                if (dgvMovieRevenue.Rows.Count == 0)
                {
                    dgvMovieRevenue.Rows.Add("Chưa có số liệu", 0);
                }

                // 4. Top Movies Grid
                string qTop = @"
                    SELECT TOP 10
                        ROW_NUMBER() OVER(ORDER BY ISNULL(SUM(hd.SoLuongVe), 0) DESC, ISNULL(SUM(hd.TienVe), 0) DESC) AS [Hạng],
                        p.MaPhim AS [Mã Phim],
                        p.TenPhim AS [Tên Phim],
                        p.TheLoai AS [Thể Loại],
                        COUNT(DISTINCT hd.MaHD) AS [Số Hóa Đơn],
                        ISNULL(SUM(hd.SoLuongVe), 0) AS [Số Vé Bán],
                        ISNULL(SUM(hd.TienVe), 0) AS [Doanh Thu Vé (Trước Giảm Giá) (VNĐ)]
                    FROM Phim p
                    LEFT JOIN SuatChieu sc ON p.MaPhim = sc.MaPhim
                    LEFT JOIN HoaDon hd ON sc.MaSuatChieu = hd.MaSuatChieu AND hd.TrangThaiThanhToan='Paid'
                    GROUP BY p.MaPhim, p.TenPhim, p.TheLoai
                    ORDER BY [Số Vé Bán] DESC, [Doanh Thu Vé (Trước Giảm Giá) (VNĐ)] DESC";

                var dtTop = Function.Query(qTop);
                dgvTopMovies.DataSource = dtTop;

                if (dgvTopMovies.Columns["Doanh Thu Vé (Trước Giảm Giá) (VNĐ)"] is { } ticketRevenueColumn)
                {
                    ticketRevenueColumn.DefaultCellStyle.Format = "N0";
                    ticketRevenueColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    ticketRevenueColumn.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
                if (dgvTopMovies.Columns["Số Vé Bán"] is { } ticketsSoldColumn)
                {
                    ticketsSoldColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvTopMovies.Columns["Số Hóa Đơn"] is { } invoiceCountColumn)
                {
                    invoiceCountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvTopMovies.Columns["Hạng"] is { } rankColumn)
                {
                    try
                    {
                        rankColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        rankColumn.Width = 65;
                    }
                    catch { }
                    rankColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp Dashboard: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblKpiTicketsTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
