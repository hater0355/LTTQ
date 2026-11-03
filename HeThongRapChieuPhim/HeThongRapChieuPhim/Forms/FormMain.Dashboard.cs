using System.Data;
using System.Globalization;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormMain
    {
        private void DashboardRefresh_Click(object? sender, EventArgs e) => LoadDashboardData();
        public void LoadDashboardData()
        {
            if (DesignTimeHelper.IsDesignTime(this)) return;
            try
            {
                lblTitle.Text = $"Chào mừng trở lại, {(string.IsNullOrWhiteSpace(CurrentUser.HoTen) ? "Admin" : CurrentUser.HoTen)}!";
                lblDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy   HH:mm", CultureInfo.GetCultureInfo("vi-VN"));
                var kpi = Function.Query(@"SELECT ISNULL(SUM(TongTien),0) TongDoanhThu,
                    ISNULL(SUM(CASE WHEN NgayLap >= DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1)
                    AND NgayLap < DATEADD(MONTH,1,DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1))
                    THEN TongTien ELSE 0 END),0) DoanhThuThang, ISNULL(SUM(CAST(SoLuongVe AS bigint)),0) TongVe
                    FROM HoaDon WHERE TrangThaiThanhToan='Paid'").Rows[0];
                lblRevenue.Text = $"{Convert.ToDecimal(kpi["TongDoanhThu"]):N0} ₫";
                lblMonth.Text = $"{Convert.ToDecimal(kpi["DoanhThuThang"]):N0} ₫";
                lblTickets.Text = $"{Convert.ToInt64(kpi["TongVe"]):N0} vé";
                var movies = Function.Query(@"SELECT p.TenPhim,p.TheLoai,p.ThoiLuong FROM Phim p WHERE EXISTS (
                    SELECT 1 FROM SuatChieu sc WHERE sc.MaPhim=p.MaPhim
                    AND sc.NgayChieu < DATEADD(DAY,7,CAST(GETDATE() AS date))
                    AND DATEADD(SECOND,DATEDIFF(SECOND,CAST('00:00' AS time),sc.GioChieu),CAST(sc.NgayChieu AS datetime))>GETDATE())
                    ORDER BY p.TenPhim");
                lblMovies.Text = $"{movies.Rows.Count} phim";
                dgvNowShowing.DataSource = movies;
                var weekly = Function.Query(@"SELECT CAST(NgayLap AS date) Ngay,SUM(TongTien) DoanhThu FROM HoaDon
                    WHERE TrangThaiThanhToan='Paid' AND NgayLap>=DATEADD(DAY,-6,CAST(GETDATE() AS date))
                    AND NgayLap<DATEADD(DAY,1,CAST(GETDATE() AS date)) GROUP BY CAST(NgayLap AS date)");
                chartWeekly.Series[0].Points.Clear();
                for (int i=-6;i<=0;i++)
                {
                    var day=DateTime.Today.AddDays(i);
                    var row=weekly.Rows.Cast<DataRow>().FirstOrDefault(r=>Convert.ToDateTime(r["Ngay"]).Date==day);
                    decimal total=row==null ? 0 : Convert.ToDecimal(row["DoanhThu"]);
                    int index=chartWeekly.Series[0].Points.AddXY(day.ToString("dd/MM"),total);
                    chartWeekly.Series[0].Points[index].ToolTip=$"{day:dd/MM/yyyy}: {total:N0} ₫";
                }
                chartWeekly.ChartAreas[0].AxisY.Maximum = weekly.Rows.Count == 0 ? 1 : double.NaN;
                chartWeekly.ChartAreas[0].AxisY.Interval = weekly.Rows.Count == 0 ? 1 : double.NaN;
                chartWeekly.Titles.Clear();
                if (weekly.Rows.Count == 0) chartWeekly.Titles.Add("Chưa có doanh thu trong 7 ngày qua");
                var revenue=Function.Query(@"SELECT TOP 5 p.TenPhim,SUM(hd.TienVe) DoanhThu
                    FROM HoaDon hd JOIN SuatChieu sc ON sc.MaSuatChieu=hd.MaSuatChieu JOIN Phim p ON p.MaPhim=sc.MaPhim
                    WHERE hd.TrangThaiThanhToan='Paid' GROUP BY p.MaPhim,p.TenPhim ORDER BY DoanhThu DESC,p.TenPhim,p.MaPhim");
                chartMovieRevenue.Series[0].Points.Clear();
                foreach(DataRow row in revenue.Rows)
                {
                    int index=chartMovieRevenue.Series[0].Points.AddXY(row["TenPhim"],row["DoanhThu"]);
                    chartMovieRevenue.Series[0].Points[index].ToolTip=$"{row["TenPhim"]}: {Convert.ToDecimal(row["DoanhThu"]):N0} ₫ (trước giảm giá)";
                }
                // Xếp hạng theo số vé đã thanh toán, không dùng doanh thu.
                dgvTopMovies.DataSource=Function.Query(@"SELECT TOP 10
                    ROW_NUMBER() OVER(ORDER BY SUM(CAST(hd.SoLuongVe AS bigint)) DESC,p.TenPhim,p.MaPhim) Hang,
                    p.TenPhim,SUM(CAST(hd.SoLuongVe AS bigint)) SoVe FROM HoaDon hd
                    JOIN SuatChieu sc ON sc.MaSuatChieu=hd.MaSuatChieu JOIN Phim p ON p.MaPhim=sc.MaPhim
                    WHERE hd.TrangThaiThanhToan='Paid' GROUP BY p.MaPhim,p.TenPhim
                    HAVING SUM(CAST(hd.SoLuongVe AS bigint))>0 ORDER BY SoVe DESC,p.TenPhim,p.MaPhim");
                dgvShowtimes.DataSource=Function.Query(@"SELECT TOP 20 p.TenPhim,ph.TenPhong,sc.NgayChieu,sc.GioChieu
                    FROM SuatChieu sc JOIN Phim p ON p.MaPhim=sc.MaPhim JOIN PhongChieu ph ON ph.MaPhong=sc.MaPhong
                    WHERE sc.NgayChieu<DATEADD(DAY,7,CAST(GETDATE() AS date))
                    AND DATEADD(SECOND,DATEDIFF(SECOND,CAST('00:00' AS time),sc.GioChieu),CAST(sc.NgayChieu AS datetime))>GETDATE()
                    ORDER BY sc.NgayChieu,sc.GioChieu,sc.MaSuatChieu");
                lblStatus.Text=dgvTopMovies.Rows.Count==0 ? "Chưa có vé đã thanh toán để xếp hạng."
                    : "Top phim tính trên toàn bộ vé đã thanh toán.";
            }
            catch(Exception ex)
            {
                chartWeekly.Series[0].Points.Clear();
                chartMovieRevenue.Series[0].Points.Clear();
                dgvTopMovies.DataSource=null; dgvShowtimes.DataSource=null; dgvNowShowing.DataSource=null;
                lblRevenue.Text=lblMonth.Text=lblTickets.Text=lblMovies.Text="—";
                lblStatus.Text="Không tải được dữ liệu. Nhấn Làm mới để thử lại.";
                MessageBox.Show($"Không thể tải dữ liệu tổng quan: {ex.Message}","Lỗi CSDL",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}

