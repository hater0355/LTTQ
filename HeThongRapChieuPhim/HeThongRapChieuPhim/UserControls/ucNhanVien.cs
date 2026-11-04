using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucNhanVien : UserControl
    {
        public ucNhanVien()
        {
            InitializeComponent();
            txtSDT.MaxLength = 15;
            txtCCCD.MaxLength = 20;
            txtSDT.KeyPress += NumericTextBox_KeyPress;
            txtCCCD.KeyPress += NumericTextBox_KeyPress;
            txtSDT.TextChanged += NumericTextBox_TextChanged;
            txtCCCD.TextChanged += NumericTextBox_TextChanged;
            if (!DesignTimeHelper.IsDesignTime(this)) LoadNhanVien();
        }

        private static bool IsAsciiDigit(char value) => value >= '0' && value <= '9';

        private static bool ContainsOnlyDigits(string value)
        {
            foreach (char character in value)
                if (!IsAsciiDigit(character)) return false;
            return true;
        }

        private static void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !IsAsciiDigit(e.KeyChar)) e.Handled = true;
        }

        // KeyPress does not see pasted text, so sanitize paste/input-method changes too.
        private static void NumericTextBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox textBox) return;

            string original = textBox.Text;
            int originalCaret = textBox.SelectionStart;
            var digits = new System.Text.StringBuilder(original.Length);
            int filteredCaret = 0;
            for (int i = 0; i < original.Length; i++)
            {
                if (!IsAsciiDigit(original[i])) continue;
                digits.Append(original[i]);
                if (i < originalCaret) filteredCaret++;
            }

            string filtered = digits.ToString();
            if (filtered == original) return;
            textBox.Text = filtered;
            textBox.SelectionStart = Math.Min(filteredCaret, filtered.Length);
        }

        private static bool ValidateNumericIdentity(string phone, string citizenId)
        {
            if (ContainsOnlyDigits(phone) && ContainsOnlyDigits(citizenId)) return true;
            MessageBox.Show("Số điện thoại và CCCD chỉ được chứa chữ số 0–9.", "Dữ liệu không hợp lệ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            LoadNhanVien(txtSearch.Text.Trim());
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadNhanVien(txtSearch.Text.Trim());
            }
        }

        private void LoadNhanVien(string searchKeyword = "")
        {
            try
            {
                string query = @"
                    SELECT 
                        nv.MaNV AS [Mã NV],
                        nv.HoTen AS [Họ Tên],
                        CONVERT(VARCHAR(10), nv.NgaySinh, 103) AS [Ngày Sinh],
                        nv.SDT AS [Số ĐT],
                        nv.CCCD AS [CCCD],
                        nv.ChucVu AS [Chức Vụ],
                        (SELECT COUNT_BIG(1)
                         FROM dbo.LichLamViec ll
                         WHERE ll.MaNV = nv.MaNV
                           AND ll.TrangThai = 'Completed'
                           AND ll.NgayLam >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
                           AND ll.NgayLam < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)))
                            AS [Số ca đã làm tháng này]
                    FROM NhanVien nv
                    WHERE (@kw = '' OR nv.MaNV LIKE '%' + @kw + '%' OR nv.HoTen LIKE '%' + @kw + '%' OR nv.SDT LIKE '%' + @kw + '%')
                    ORDER BY nv.MaNV ASC";

                var parameters = new[] { DbArg.NVarChar4000("@kw", searchKeyword) };
                var dt = Function.Query(query, parameters);
                dgvNhanVien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp danh sách nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtCCCD.Clear();
            dtpNgaySinh.Value = DateTime.Now.AddYears(-20);
            cboChucVu.SelectedIndex = 0;
            txtSearch.Clear();
            txtMaNV.Enabled = true;
            LoadNhanVien();
        }

        private void DgvNhanVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhanVien.Rows.Count) return;

            var row = dgvNhanVien.Rows[e.RowIndex];
            txtMaNV.Text = row.Cells["Mã NV"].Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells["Họ Tên"].Value?.ToString() ?? "";
            txtSDT.Text = row.Cells["Số ĐT"].Value?.ToString() ?? "";
            txtCCCD.Text = row.Cells["CCCD"].Value?.ToString() ?? "";
            string chucVu = row.Cells["Chức Vụ"].Value?.ToString() ?? "Nhân viên";
            cboChucVu.SelectedItem = chucVu;

            if (DateTime.TryParseExact(row.Cells["Ngày Sinh"].Value?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ns))
            {
                dtpNgaySinh.Value = ns;
            }

            txtMaNV.Enabled = false; // Khóa mã khi xem/sửa
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string chucVu = cboChucVu.SelectedItem?.ToString() ?? "Nhân viên";
            DateTime ngaySinh = dtpNgaySinh.Value;

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng điền đủ Mã NV, Họ Tên và Số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateNumericIdentity(sdt, cccd)) return;

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
                int count = Convert.ToInt32(Function.Scalar(checkQuery, new[] { DbArg.VarChar20("@MaNV", maNV) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã nhân viên '{maNV}' đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Insert NhanVien
                string insertNV = @"
                    INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, SDT, CCCD, ChucVu)
                    VALUES (@MaNV, @HoTen, @NgaySinh, @SDT, @CCCD, @ChucVu)";

                var pNV = new[]
                {
                    DbArg.VarChar20("@MaNV", maNV),
                    DbArg.NVarChar100("@HoTen", hoTen),
                    DbArg.Date("@NgaySinh", ngaySinh),
                    DbArg.VarChar15("@SDT", sdt),
                    DbArg.VarChar20("@CCCD", cccd),
                    DbArg.NVarChar50("@ChucVu", chucVu)
                };
                Function.Execute(insertNV, pNV);
                MessageBox.Show("Thêm hồ sơ nhân viên thành công. Tài khoản đăng nhập được tạo riêng trong mục Tài Khoản Thu Ngân.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string chucVu = cboChucVu.SelectedItem?.ToString() ?? "Nhân viên";
            DateTime ngaySinh = dtpNgaySinh.Value;

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateNumericIdentity(sdt, cccd)) return;

            try
            {
                string updateNV = @"
                    UPDATE NhanVien
                    SET HoTen = @HoTen, NgaySinh = @NgaySinh, SDT = @SDT, CCCD = @CCCD, ChucVu = @ChucVu
                    WHERE MaNV = @MaNV";

                var pNV = new[]
                {
                    DbArg.VarChar20("@MaNV", maNV),
                    DbArg.NVarChar100("@HoTen", hoTen),
                    DbArg.Date("@NgaySinh", ngaySinh),
                    DbArg.VarChar15("@SDT", sdt),
                    DbArg.VarChar20("@CCCD", cccd),
                    DbArg.NVarChar50("@ChucVu", chucVu)
                };
                Function.Execute(updateNV, pNV);

                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isCurrentAccount = maNV.Equals(CurrentUser.MaNV, StringComparison.OrdinalIgnoreCase);
            bool hasLinkedAccount;
            bool hasShiftHistory;
            try
            {
                hasLinkedAccount = Convert.ToInt32(Function.Scalar(
                    "SELECT COUNT(*) FROM dbo.TaiKhoan WHERE MaNV=@id",
                    new[] { DbArg.VarChar20("@id", maNV) })) > 0;
                hasShiftHistory = Convert.ToInt32(Function.Scalar(
                    "SELECT COUNT(*) FROM dbo.LichLamViec WHERE MaNV=@id",
                    new[] { DbArg.VarChar20("@id", maNV) })) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kiểm tra quyền liên kết hồ sơ; không xóa nhân viên.\n" + ex.Message, "Lỗi kiểm tra quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (isCurrentAccount || hasLinkedAccount || hasShiftHistory)
            {
                MessageBox.Show("Không thể xóa hồ sơ đang đăng nhập, còn liên kết tài khoản hoặc đã có lịch sử phân ca.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa hồ sơ nhân viên [{maNV}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int affected = Function.Execute(@"DELETE nv FROM dbo.NhanVien nv
                        WHERE nv.MaNV=@MaNV
                          AND NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan tk WHERE tk.MaNV=nv.MaNV)",
                        new[] { DbArg.VarChar20("@MaNV", maNV) });
                    if (affected != 1) throw new InvalidOperationException("Hồ sơ đã bị thay đổi hoặc đang được dùng bởi tài khoản hệ thống.");

                    MessageBox.Show("Đã xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa nhân viên (có thể do đã phát sinh hóa đơn): {ex.Message}", "Lỗi ràng buộc CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
