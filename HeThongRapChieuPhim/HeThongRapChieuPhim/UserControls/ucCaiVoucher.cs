using HeThongRapChieuPhim.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucCaiVoucher : UserControl
    {
        public ucCaiVoucher()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvVoucher);
            cboLoaiGiam.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            dtpHanSuDung.Value = DateTime.Today.AddMonths(3);
            this.Load += (s, e) =>
            {
                if (!DesignTimeHelper.IsDesignTime(this)) RefreshData();
            };
        }

        public void RefreshData()
        {
            LoadVoucher();
        }

        private void CboLoaiGiam_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string loai = cboLoaiGiam.SelectedItem?.ToString() ?? "";
            if (loai.Contains("phần trăm", StringComparison.OrdinalIgnoreCase))
            {
                lblDonViGiaTri.Text = "Đơn vị tính: % trên tiền vé (tối đa 100%)";
                numGiaTriGiam.Maximum = 100;
                numGiaTriGiam.Increment = 5;
                if (numGiaTriGiam.Value > 100) numGiaTriGiam.Value = 10;
                else if (numGiaTriGiam.Value == 0) numGiaTriGiam.Value = 10;
            }
            else if (loai.Contains("số tiền", StringComparison.OrdinalIgnoreCase))
            {
                lblDonViGiaTri.Text = "Đơn vị tính: VNĐ trừ thẳng vào hóa đơn";
                numGiaTriGiam.Maximum = 10000000;
                numGiaTriGiam.Increment = 10000;
                if (numGiaTriGiam.Value < 1000) numGiaTriGiam.Value = 30000;
            }
            else // Giảm số vé
            {
                lblDonViGiaTri.Text = "Đơn vị tính: Số vé miễn phí (ví dụ 1 vé)";
                numGiaTriGiam.Maximum = 10;
                numGiaTriGiam.Increment = 1;
                if (numGiaTriGiam.Value > 10 || numGiaTriGiam.Value == 0) numGiaTriGiam.Value = 1;
            }
        }

        private void BtnAutoMa_Click(object? sender, EventArgs e)
        {
            string prefix = "VC";
            string loai = cboLoaiGiam.SelectedItem?.ToString() ?? "";
            if (loai.Contains("phần trăm", StringComparison.OrdinalIgnoreCase)) prefix = "VCPCT";
            else if (loai.Contains("vé", StringComparison.OrdinalIgnoreCase)) prefix = "VCTCK";

            string randomPart = new Random().Next(100, 999).ToString();
            txtMaVoucher.Text = $"{prefix}{DateTime.Now:yyMM}{randomPart}";
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMaVoucher.Clear();
            txtTenVoucher.Clear();
            cboLoaiGiam.SelectedIndex = 0;
            numGiaTriGiam.Value = 10;
            dtpHanSuDung.Value = DateTime.Today.AddMonths(3);
            txtMoTa.Clear();
            cboTrangThai.SelectedIndex = 0;
            txtSearch.Clear();
            txtMaVoucher.Enabled = true;
            LoadVoucher();
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            LoadVoucher(txtSearch.Text.Trim());
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadVoucher(txtSearch.Text.Trim());
            }
        }

        private void LoadVoucher(string searchKeyword = "")
        {
            try
            {
                string query = @"
                    SELECT 
                        MaVoucher AS [Mã Voucher],
                        TenVoucher AS [Tên Voucher],
                        LoaiGiam AS [Hình Thức Giảm],
                        GiaTriGiam AS [Mức Giảm],
                        CONVERT(VARCHAR(10), HanSuDung, 103) AS [Hạn Sử Dụng],
                        CASE 
                            WHEN TrangThai = 1 AND HanSuDung >= CAST(GETDATE() AS DATE) THEN N'🟢 Còn hiệu lực'
                            WHEN TrangThai = 0 THEN N'🔴 Đã sử dụng / Đã khóa'
                            ELSE N'⌛ Hết hạn'
                        END AS [Trạng Thái],
                        ISNULL(MoTa, N'') AS [Mô Tả / Lưu Ý]
                    FROM Voucher
                    WHERE (@kw = '' OR MaVoucher LIKE '%' + @kw + '%' OR TenVoucher LIKE '%' + @kw + '%' OR LoaiGiam LIKE '%' + @kw + '%')
                    ORDER BY HanSuDung DESC, MaVoucher ASC";

                var dt = Function.Query(query, new[] { DbArg.NVarChar4000("@kw", searchKeyword) });
                dgvVoucher.DataSource = dt;

                if (dgvVoucher.Columns["Mức Giảm"] is { } discountColumn)
                {
                    discountColumn.DefaultCellStyle.Format = "N0";
                    discountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvVoucher.Columns["Hạn Sử Dụng"] is { } expiryColumn)
                {
                    expiryColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvVoucher.Columns["Trạng Thái"] is { } statusColumn)
                {
                    statusColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp danh sách voucher: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvVoucher_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvVoucher.Rows.Count) return;

            var row = dgvVoucher.Rows[e.RowIndex];
            txtMaVoucher.Text = row.Cells["Mã Voucher"].Value?.ToString() ?? "";
            txtTenVoucher.Text = row.Cells["Tên Voucher"].Value?.ToString() ?? "";

            string loai = row.Cells["Hình Thức Giảm"].Value?.ToString() ?? "";
            if (loai.Contains("phần trăm", StringComparison.OrdinalIgnoreCase)) cboLoaiGiam.SelectedIndex = 0;
            else if (loai.Contains("số tiền", StringComparison.OrdinalIgnoreCase)) cboLoaiGiam.SelectedIndex = 1;
            else if (loai.Contains("vé", StringComparison.OrdinalIgnoreCase)) cboLoaiGiam.SelectedIndex = 2;

            if (decimal.TryParse(row.Cells["Mức Giảm"].Value?.ToString(), out decimal val))
            {
                numGiaTriGiam.Value = Math.Min(Math.Max(val, numGiaTriGiam.Minimum), numGiaTriGiam.Maximum);
            }

            if (DateTime.TryParseExact(row.Cells["Hạn Sử Dụng"].Value?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime han))
            {
                dtpHanSuDung.Value = han;
            }

            string tt = row.Cells["Trạng Thái"].Value?.ToString() ?? "";
            cboTrangThai.SelectedIndex = tt.Contains("Còn hiệu lực", StringComparison.OrdinalIgnoreCase) ? 0 : 1;

            txtMoTa.Text = row.Cells["Mô Tả / Lưu Ý"].Value?.ToString() ?? "";
            txtMaVoucher.Enabled = false;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string ma = txtMaVoucher.Text.Trim();
            string ten = txtTenVoucher.Text.Trim();
            string loai = cboLoaiGiam.SelectedItem?.ToString() ?? "Giảm phần trăm (%)";
            decimal giaTri = numGiaTriGiam.Value;
            DateTime han = dtpHanSuDung.Value.Date;
            string moTa = txtMoTa.Text.Trim();
            bool trangThai = cboTrangThai.SelectedIndex == 0;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng điền đủ Mã voucher và Tên voucher!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string check = "SELECT COUNT(*) FROM Voucher WHERE MaVoucher = @Ma";
                int count = Convert.ToInt32(Function.Scalar(check, new[] { DbArg.VarChar20("@Ma", ma) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã voucher '{ma}' đã tồn tại! Vui lòng chọn mã khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string insert = @"
                    INSERT INTO Voucher (MaVoucher, TenVoucher, LoaiGiam, GiaTriGiam, HanSuDung, MoTa, TrangThai)
                    VALUES (@Ma, @Ten, @Loai, @GiaTri, @Han, @MoTa, @TrangThai)";

                var p = new[]
                {
                    DbArg.VarChar20("@Ma", ma),
                    DbArg.NVarChar100("@Ten", ten),
                    DbArg.NVarChar50("@Loai", loai),
                    DbArg.Decimal18_2("@GiaTri", giaTri),
                    DbArg.Date("@Han", han),
                    DbArg.NVarChar255("@MoTa", moTa),
                    DbArg.Bit("@TrangThai", trangThai)
                };
                Function.Execute(insert, p);

                MessageBox.Show("Thêm voucher khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm voucher: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string ma = txtMaVoucher.Text.Trim();
            string ten = txtTenVoucher.Text.Trim();
            string loai = cboLoaiGiam.SelectedItem?.ToString() ?? "Giảm phần trăm (%)";
            decimal giaTri = numGiaTriGiam.Value;
            DateTime han = dtpHanSuDung.Value.Date;
            string moTa = txtMoTa.Text.Trim();
            bool trangThai = cboTrangThai.SelectedIndex == 0;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn voucher cần sửa từ bảng danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string update = @"
                    UPDATE Voucher
                    SET TenVoucher = @Ten, LoaiGiam = @Loai, GiaTriGiam = @GiaTri, HanSuDung = @Han, MoTa = @MoTa, TrangThai = @TrangThai
                    WHERE MaVoucher = @Ma";

                var p = new[]
                {
                    DbArg.VarChar20("@Ma", ma),
                    DbArg.NVarChar100("@Ten", ten),
                    DbArg.NVarChar50("@Loai", loai),
                    DbArg.Decimal18_2("@GiaTri", giaTri),
                    DbArg.Date("@Han", han),
                    DbArg.NVarChar255("@MoTa", moTa),
                    DbArg.Bit("@TrangThai", trangThai)
                };
                Function.Execute(update, p);

                MessageBox.Show("Cập nhật thông tin voucher thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật voucher: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string ma = txtMaVoucher.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn voucher cần xóa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa voucher [{ma}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Function.Execute("DELETE FROM Voucher WHERE MaVoucher = @Ma", new[] { DbArg.VarChar20("@Ma", ma) });
                    MessageBox.Show("Đã xóa voucher thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa voucher: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
