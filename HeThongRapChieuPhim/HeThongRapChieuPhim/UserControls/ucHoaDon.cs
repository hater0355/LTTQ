using HeThongRapChieuPhim.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using HeThongRapChieuPhim.Forms;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucHoaDon : UserControl
    {
        public ucHoaDon()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvHoaDon);
            LoadHoaDon();
        }

        private void BtnFilter_Click(object? sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            chkLocNgay.Checked = false;
            LoadHoaDon();
        }

        public void LoadHoaDon()
        {
            try
            {
                string kw = txtSearch.Text.Trim();
                bool filterDate = chkLocNgay.Checked;
                DateTime from = dtpTuNgay.Value.Date;
                DateTime toExclusive = dtpDenNgay.Value.Date.AddDays(1);
                var dt = InvoiceService.Search(kw, filterDate, from, toExclusive);
                dgvHoaDon.DataSource = dt;

                // Format currency columns
                string[] moneyCols = { "Tiền Vé", "Giảm Giá", "Tiền F&B", "Tổng Thanh Toán" };
                foreach (var col in moneyCols)
                {
                    if (dgvHoaDon.Columns[col] != null)
                    {
                        dgvHoaDon.Columns[col].DefaultCellStyle.Format = "N0";
                        dgvHoaDon.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Calculate total revenue
                decimal total = 0;
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(r["Trạng Thái Thanh Toán"]?.ToString(), "Paid", StringComparison.OrdinalIgnoreCase) &&
                        decimal.TryParse(r["Tổng Thanh Toán"]?.ToString(), out decimal m))
                        total += m;
                }
                lblTotalRevenue.Text = $"Tổng doanh thu danh sách: {total:N0} đ ({dt.Rows.Count} hóa đơn)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp danh sách hóa đơn: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrintAgain_Click(object? sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 hóa đơn trong bảng để xem lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvHoaDon.SelectedRows[0];
            string maHD = row.Cells["Mã HĐ"].Value?.ToString() ?? "";
            if (!string.Equals(row.Cells["Trạng Thái Thanh Toán"].Value?.ToString(), "Paid", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ hóa đơn đã thanh toán mới được in vé.", "Giao dịch chưa hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var data = InvoiceService.GetPrintData(maHD);
                if (data is null) return;
                var r = data.Header;
                var frmPrint = new FormInHoaDon
                {
                    MaHD = maHD,
                    NgayXuat = Convert.ToDateTime(r["NgayLap"]),
                    TenPhim = r["TenPhim"]?.ToString() ?? "",
                    PhongChieu = r["TenPhong"]?.ToString() ?? "",
                    SuatChieu = r["GioChieu"]?.ToString() ?? "",
                    DanhSachGhe = r["DanhSachGhe"]?.ToString() ?? "",
                    SoLuongVe = Convert.ToInt32(r["SoLuongVe"]),
                    TenCombo = r["TenCombo"]?.ToString() ?? "",
                    DanhSachDoAn = data.FoodDescription,
                    TienVe = Convert.ToDecimal(r["TienVe"]),
                    TienGiamGia = Convert.ToDecimal(r["TienGiamGia"]),
                    TienDoAn = Convert.ToDecimal(r["TienDoAn"]),
                    TongTien = Convert.ToDecimal(r["TongTien"]),
                    PhuongThucTT = r["PhuongThucTT"]?.ToString() ?? "Tiền mặt"
                };

                frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
