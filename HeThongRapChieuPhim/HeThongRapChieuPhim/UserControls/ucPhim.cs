using HeThongRapChieuPhim.Classes;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucPhim : UserControl
    {
        public ucPhim()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvPhim);
            EnsureMoTaColumn();
            LoadPhim();
        }

        private static void EnsureMoTaColumn()
        {
            try
            {
                string sql = @"
                    IF COL_LENGTH('dbo.Phim', 'MoTa') IS NULL
                    BEGIN
                        ALTER TABLE dbo.Phim ADD MoTa NVARCHAR(MAX) NULL;
                    END";
                Function.Execute(sql);
            }
            catch { }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            LoadPhim(txtSearch.Text.Trim());
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadPhim(txtSearch.Text.Trim());
            }
        }

        private void LoadPhim(string searchKeyword = "")
        {
            try
            {
                string query = @"
                    SELECT 
                        MaPhim AS [Mã Phim],
                        TenPhim AS [Tên Phim],
                        TheLoai AS [Thể Loại],
                        NgonNgu AS [Ngôn Ngữ],
                        DoTuoi AS [Độ Tuổi],
                        ThoiLuong AS [Thời Lượng (Phút)],
                        GiaPhim AS [Giá Vé Gốc (VNĐ)],
                        PosterPath AS [Ảnh Poster],
                        ISNULL(MoTa, N'') AS [Mô Tả Phim]
                    FROM Phim
                    WHERE (@kw = '' OR MaPhim LIKE '%' + @kw + '%' OR TenPhim LIKE '%' + @kw + '%' OR TheLoai LIKE '%' + @kw + '%')
                    ORDER BY MaPhim ASC";

                var dt = Function.Query(query, new[] { new DbArg("@kw", searchKeyword) });
                dgvPhim.DataSource = dt;

                if (dgvPhim.Columns["Giá Vé Gốc (VNĐ)"] != null)
                {
                    dgvPhim.Columns["Giá Vé Gốc (VNĐ)"].DefaultCellStyle.Format = "N0";
                    dgvPhim.Columns["Giá Vé Gốc (VNĐ)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp danh sách phim: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaPhim.Clear();
            txtTenPhim.Clear();
            cboTheLoai.SelectedIndex = 0;
            cboNgonNgu.SelectedIndex = 0;
            cboDoTuoi.SelectedIndex = 0;
            numThoiLuong.Value = 120;
            numGiaPhim.Value = 80000;
            txtMoTa.Clear();
            lblPosterPath.Text = "Chưa chọn ảnh poster";
            picPoster.Image = null;
            txtSearch.Clear();
            txtMaPhim.Enabled = true;
            LoadPhim();
        }

        private void DgvPhim_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPhim.Rows.Count) return;

            var row = dgvPhim.Rows[e.RowIndex];
            txtMaPhim.Text = row.Cells["Mã Phim"].Value?.ToString() ?? "";
            txtTenPhim.Text = row.Cells["Tên Phim"].Value?.ToString() ?? "";
            cboTheLoai.SelectedItem = row.Cells["Thể Loại"].Value?.ToString() ?? "Hành động";
            cboNgonNgu.SelectedItem = row.Cells["Ngôn Ngữ"].Value?.ToString() ?? "Tiếng Việt";
            cboDoTuoi.SelectedItem = row.Cells["Độ Tuổi"].Value?.ToString() ?? "P (Mọi lứa tuổi)";

            if (int.TryParse(row.Cells["Thời Lượng (Phút)"].Value?.ToString(), out int tl))
                numThoiLuong.Value = Math.Min(Math.Max(tl, numThoiLuong.Minimum), numThoiLuong.Maximum);

            if (decimal.TryParse(row.Cells["Giá Vé Gốc (VNĐ)"].Value?.ToString(), out decimal gia))
                numGiaPhim.Value = Math.Min(Math.Max(gia, numGiaPhim.Minimum), numGiaPhim.Maximum);

            txtMoTa.Text = row.Cells["Mô Tả Phim"].Value?.ToString() ?? "";

            string posterFileName = row.Cells["Ảnh Poster"].Value?.ToString() ?? "";
            lblPosterPath.Text = string.IsNullOrEmpty(posterFileName) ? "Chưa chọn ảnh poster" : posterFileName;

            if (!string.IsNullOrEmpty(posterFileName))
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", posterFileName);
                if (File.Exists(fullPath))
                {
                    try { picPoster.Image = Image.FromFile(fullPath); } catch { picPoster.Image = null; }
                }
                else
                {
                    picPoster.Image = null;
                }
            }
            else
            {
                picPoster.Image = null;
            }

            txtMaPhim.Enabled = false;
        }

        private void BtnChonAnh_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Chọn ảnh poster phim",
                Filter = "Hình ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Tất cả tệp (*.*)|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string assetsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
                    if (!Directory.Exists(assetsDir)) Directory.CreateDirectory(assetsDir);

                    string fileName = Path.GetFileName(ofd.FileName);
                    string destPath = Path.Combine(assetsDir, fileName);

                    if (!string.Equals(ofd.FileName, destPath, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Copy(ofd.FileName, destPath, true);
                    }

                    lblPosterPath.Text = fileName;
                    picPoster.Image = Image.FromFile(destPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi nạp ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string ma = txtMaPhim.Text.Trim();
            string ten = txtTenPhim.Text.Trim();
            string theLoai = cboTheLoai.SelectedItem?.ToString() ?? "Hành động";
            string ngonNgu = cboNgonNgu.SelectedItem?.ToString() ?? "Tiếng Việt";
            string doTuoi = cboDoTuoi.SelectedItem?.ToString() ?? "P (Mọi lứa tuổi)";
            int thoiLuong = (int)numThoiLuong.Value;
            decimal giaVe = numGiaPhim.Value;
            string poster = lblPosterPath.Text.Equals("Chưa chọn ảnh poster", StringComparison.OrdinalIgnoreCase) ? "" : lblPosterPath.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng điền đủ Mã phim và Tên phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string check = "SELECT COUNT(*) FROM Phim WHERE MaPhim = @Ma";
                int count = Convert.ToInt32(Function.Scalar(check, new[] { new DbArg("@Ma", ma) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã phim '{ma}' đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string insert = @"
                    INSERT INTO Phim (MaPhim, TenPhim, TheLoai, NgonNgu, DoTuoi, ThoiLuong, GiaPhim, PosterPath, MoTa)
                    VALUES (@Ma, @Ten, @TheLoai, @NgonNgu, @DoTuoi, @ThoiLuong, @GiaVe, @Poster, @MoTa)";

                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@TheLoai", theLoai),
                    new DbArg("@NgonNgu", ngonNgu),
                    new DbArg("@DoTuoi", doTuoi),
                    new DbArg("@ThoiLuong", thoiLuong),
                    new DbArg("@GiaVe", giaVe),
                    new DbArg("@Poster", poster),
                    new DbArg("@MoTa", moTa)
                };
                Function.Execute(insert, p);

                MessageBox.Show("Thêm phim mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm phim: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string ma = txtMaPhim.Text.Trim();
            string ten = txtTenPhim.Text.Trim();
            string theLoai = cboTheLoai.SelectedItem?.ToString() ?? "Hành động";
            string ngonNgu = cboNgonNgu.SelectedItem?.ToString() ?? "Tiếng Việt";
            string doTuoi = cboDoTuoi.SelectedItem?.ToString() ?? "P (Mọi lứa tuổi)";
            int thoiLuong = (int)numThoiLuong.Value;
            decimal giaVe = numGiaPhim.Value;
            string poster = lblPosterPath.Text.Equals("Chưa chọn ảnh poster", StringComparison.OrdinalIgnoreCase) ? "" : lblPosterPath.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn phim cần sửa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string update = @"
                    UPDATE Phim
                    SET TenPhim = @Ten, TheLoai = @TheLoai, NgonNgu = @NgonNgu, DoTuoi = @DoTuoi,
                        ThoiLuong = @ThoiLuong, GiaPhim = @GiaVe, PosterPath = @Poster, MoTa = @MoTa
                    WHERE MaPhim = @Ma";

                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@TheLoai", theLoai),
                    new DbArg("@NgonNgu", ngonNgu),
                    new DbArg("@DoTuoi", doTuoi),
                    new DbArg("@ThoiLuong", thoiLuong),
                    new DbArg("@GiaVe", giaVe),
                    new DbArg("@Poster", poster),
                    new DbArg("@MoTa", moTa)
                };
                Function.Execute(update, p);

                MessageBox.Show("Cập nhật thông tin phim thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa phim: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string ma = txtMaPhim.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn phim cần xóa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phim [{ma}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Function.Execute("DELETE FROM Phim WHERE MaPhim = @Ma", new[] { new DbArg("@Ma", ma) });
                    MessageBox.Show("Đã xóa phim thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa phim (có thể phim này đã được cài vào Suất Chiếu): {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
