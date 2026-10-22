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
            dgvMonthlyRevenue.Columns["colMonthRevenue"].DefaultCellStyle.Format = "N0";
            dgvMovieRevenue.Columns["colMovieRevenue"].DefaultCellStyle.Format = "N0";

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

        private void lblKpiTicketsTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
