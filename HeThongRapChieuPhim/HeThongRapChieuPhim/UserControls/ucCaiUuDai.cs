using HeThongRapChieuPhim.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucCaiUuDai : UserControl
    {
        public ucCaiUuDai()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvCombo);
            RefreshData();
        }

        public void RefreshData()
        {
            LoadCombo();
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void LoadCombo()
        {
            try
            {
                string query = @"
                    SELECT 
                        MaCombo AS [Mã Combo],
                        TenCombo AS [Tên Combo],
                        SoLuongGhe AS [Số Ghế Bắt Buộc],
                        PhanTramGiam AS [% Giảm Giá Vé],
                        ISNULL(MoTa, N'') AS [Mô Tả / Lưu Ý]
                    FROM UuDaiCombo
                    ORDER BY MaCombo ASC";

                var dt = Function.Query(query);
                dgvCombo.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp ưu đãi combo: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaCombo.Clear();
            txtTenCombo.Clear();
            numSoLuongGhe.Value = 2;
            numPhanTramGiam.Value = 10;
            txtMoTa.Clear();
            txtMaCombo.Enabled = true;
            LoadCombo();
        }

        private void DgvCombo_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvCombo.Rows.Count) return;

            var row = dgvCombo.Rows[e.RowIndex];
            txtMaCombo.Text = row.Cells["Mã Combo"].Value?.ToString() ?? "";
            txtTenCombo.Text = row.Cells["Tên Combo"].Value?.ToString() ?? "";

            if (int.TryParse(row.Cells["Số Ghế Bắt Buộc"].Value?.ToString(), out int seats))
                numSoLuongGhe.Value = Math.Min(Math.Max(seats, numSoLuongGhe.Minimum), numSoLuongGhe.Maximum);

            if (decimal.TryParse(row.Cells["% Giảm Giá Vé"].Value?.ToString(), out decimal pct))
                numPhanTramGiam.Value = Math.Min(Math.Max(pct, numPhanTramGiam.Minimum), numPhanTramGiam.Maximum);

            txtMoTa.Text = row.Cells["Mô Tả / Lưu Ý"].Value?.ToString() ?? "";
            txtMaCombo.Enabled = false;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string ma = txtMaCombo.Text.Trim();
            string ten = txtTenCombo.Text.Trim();
            int seats = (int)numSoLuongGhe.Value;
            decimal pct = numPhanTramGiam.Value;
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng điền đủ Mã combo và Tên combo!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string check = "SELECT COUNT(*) FROM UuDaiCombo WHERE MaCombo = @Ma";
                int count = Convert.ToInt32(Function.Scalar(check, new[] { new DbArg("@Ma", ma) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã combo '{ma}' đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string insert = @"
                    INSERT INTO UuDaiCombo (MaCombo, TenCombo, SoLuongGhe, PhanTramGiam, MoTa)
                    VALUES (@Ma, @Ten, @Seats, @Pct, @MoTa)";

                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@Seats", seats),
                    new DbArg("@Pct", pct),
                    new DbArg("@MoTa", moTa)
                };
                Function.Execute(insert, p);

                MessageBox.Show("Thêm combo ưu đãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm combo: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string ma = txtMaCombo.Text.Trim();
            string ten = txtTenCombo.Text.Trim();
            int seats = (int)numSoLuongGhe.Value;
            decimal pct = numPhanTramGiam.Value;
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn combo cần sửa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string update = @"
                    UPDATE UuDaiCombo
                    SET TenCombo = @Ten, SoLuongGhe = @Seats, PhanTramGiam = @Pct, MoTa = @MoTa
                    WHERE MaCombo = @Ma";

                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@Seats", seats),
                    new DbArg("@Pct", pct),
                    new DbArg("@MoTa", moTa)
                };
                Function.Execute(update, p);

                MessageBox.Show("Cập nhật ưu đãi combo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa combo: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string ma = txtMaCombo.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn combo cần xóa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa gói ưu đãi [{ma}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Function.Execute("DELETE FROM UuDaiCombo WHERE MaCombo = @Ma", new[] { new DbArg("@Ma", ma) });
                    MessageBox.Show("Đã xóa gói ưu đãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa combo: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
