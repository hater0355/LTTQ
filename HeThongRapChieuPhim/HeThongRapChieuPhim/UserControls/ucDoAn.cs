using HeThongRapChieuPhim.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucDoAn : UserControl
    {
        public ucDoAn()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvDoAn);
            if (!DesignTimeHelper.IsDesignTime(this)) LoadDoAn();
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            LoadDoAn(txtSearch.Text.Trim());
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDoAn(txtSearch.Text.Trim());
            }
        }

        private void LoadDoAn(string searchKeyword = "")
        {
            try
            {
                string query = @"
                    SELECT 
                        MaDoAn AS [Mã Đồ Ăn],
                        TenDoAn AS [Tên Món],
                        Loai AS [Phân Loại],
                        Gia AS [Đơn Giá (VNĐ)]
                    FROM DoAn
                    WHERE (@kw = '' OR MaDoAn LIKE '%' + @kw + '%' OR TenDoAn LIKE '%' + @kw + '%' OR Loai LIKE '%' + @kw + '%')
                    ORDER BY MaDoAn ASC";

                var dt = Function.Query(query, new[] { new DbArg("@kw", searchKeyword) });
                dgvDoAn.DataSource = dt;

                if (dgvDoAn.Columns["Đơn Giá (VNĐ)"] != null)
                {
                    dgvDoAn.Columns["Đơn Giá (VNĐ)"].DefaultCellStyle.Format = "N0";
                    dgvDoAn.Columns["Đơn Giá (VNĐ)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp thực đơn: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaDoAn.Clear();
            txtTenDoAn.Clear();
            cboLoai.SelectedIndex = 0;
            numGia.Value = 45000;
            txtSearch.Clear();
            txtMaDoAn.Enabled = true;
            LoadDoAn();
        }

        private void DgvDoAn_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDoAn.Rows.Count) return;

            var row = dgvDoAn.Rows[e.RowIndex];
            txtMaDoAn.Text = row.Cells["Mã Đồ Ăn"].Value?.ToString() ?? "";
            txtTenDoAn.Text = row.Cells["Tên Món"].Value?.ToString() ?? "";
            cboLoai.SelectedItem = row.Cells["Phân Loại"].Value?.ToString() ?? "Bắp rang";

            if (decimal.TryParse(row.Cells["Đơn Giá (VNĐ)"].Value?.ToString(), out decimal gia))
            {
                numGia.Value = Math.Min(Math.Max(gia, numGia.Minimum), numGia.Maximum);
            }

            txtMaDoAn.Enabled = false;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string ma = txtMaDoAn.Text.Trim();
            string ten = txtTenDoAn.Text.Trim();
            string loai = cboLoai.SelectedItem?.ToString() ?? "Bắp rang";
            decimal gia = numGia.Value;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng điền đủ Mã đồ ăn và Tên món!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string check = "SELECT COUNT(*) FROM DoAn WHERE MaDoAn = @Ma";
                int count = Convert.ToInt32(Function.Scalar(check, new[] { new DbArg("@Ma", ma) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã đồ ăn '{ma}' đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string insert = "INSERT INTO DoAn (MaDoAn, TenDoAn, Loai, Gia) VALUES (@Ma, @Ten, @Loai, @Gia)";
                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@Loai", loai),
                    new DbArg("@Gia", gia)
                };
                Function.Execute(insert, p);

                MessageBox.Show("Thêm món mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm món: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string ma = txtMaDoAn.Text.Trim();
            string ten = txtTenDoAn.Text.Trim();
            string loai = cboLoai.SelectedItem?.ToString() ?? "Bắp rang";
            decimal gia = numGia.Value;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn món cần sửa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string update = "UPDATE DoAn SET TenDoAn = @Ten, Loai = @Loai, Gia = @Gia WHERE MaDoAn = @Ma";
                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@Loai", loai),
                    new DbArg("@Gia", gia)
                };
                Function.Execute(update, p);

                MessageBox.Show("Cập nhật thông tin món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa món: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string ma = txtMaDoAn.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn món cần xóa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa món [{ma}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Function.Execute("DELETE FROM DoAn WHERE MaDoAn = @Ma", new[] { new DbArg("@Ma", ma) });
                    MessageBox.Show("Đã xóa món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa món (có thể đã xuất hiện trong hóa đơn cũ): {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
