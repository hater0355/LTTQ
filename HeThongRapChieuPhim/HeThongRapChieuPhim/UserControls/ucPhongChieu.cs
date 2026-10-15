using HeThongRapChieuPhim.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucPhongChieu : UserControl
    {
        public ucPhongChieu()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvPhongChieu);
            LoadPhongChieu();
            UpdateTotalSeatsAndPreview();
        }

        private void NumSoHangGhe_ValueChanged(object? sender, EventArgs e)
        {
            UpdateTotalSeatsAndPreview();
        }

        private void NumSoGheMoiHang_ValueChanged(object? sender, EventArgs e)
        {
            UpdateTotalSeatsAndPreview();
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void UpdateTotalSeatsAndPreview()
        {
            int rows = (int)numSoHangGhe.Value;
            int cols = (int)numSoGheMoiHang.Value;
            char lastRowChar = (char)('A' + rows - 1);
            lblTongGhe.Text = $"⚡ Tổng số ghế: {rows * cols} ghế (Hàng A - {lastRowChar}, Ghế 1 - {cols})";

            RenderSeatPreview(rows, cols);
        }

        private void RenderSeatPreview(int rows, int cols)
        {
            pnlSeatPreview.SuspendLayout();
            pnlSeatPreview.Controls.Clear();

            // Màn hình chiếu biểu trưng
            var lblScreen = new Label
            {
                Text = "──────── MÀN HÌNH CHIẾU PHIM (SCREEN) ────────",
                ForeColor = ThemeHelper.AccentGold,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(20, 15),
                Size = new Size(cols * 38 + 50, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlSeatPreview.Controls.Add(lblScreen);

            int startX = 25;
            int startY = 48;
            int seatSize = 32;
            int gap = 6;

            for (int r = 0; r < rows; r++)
            {
                char rowChar = (char)('A' + r);

                var lblRow = new Label
                {
                    Text = rowChar.ToString(),
                    ForeColor = ThemeHelper.TextMuted,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    Location = new Point(startX - 20, startY + r * (seatSize + gap) + 6),
                    Size = new Size(18, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlSeatPreview.Controls.Add(lblRow);

                for (int c = 1; c <= cols; c++)
                {
                    string seatName = $"{rowChar}{c}";
                    var btnSeat = new Button
                    {
                        Text = c.ToString(),
                        Location = new Point(startX + (c - 1) * (seatSize + gap), startY + r * (seatSize + gap)),
                        Size = new Size(seatSize, seatSize),
                        BackColor = ThemeHelper.SeatAvailable,
                        ForeColor = ThemeHelper.TextPrimary,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                        Tag = seatName
                    };
                    btnSeat.FlatAppearance.BorderSize = 0;
                    pnlSeatPreview.Controls.Add(btnSeat);
                }
            }

            pnlSeatPreview.ResumeLayout();
        }

        private void LoadPhongChieu()
        {
            try
            {
                string query = @"
                    SELECT 
                        MaPhong AS [Mã Phòng],
                        TenPhong AS [Tên Phòng],
                        SoHangGhe AS [Số Hàng Ghế (A-Z)],
                        SoGheMoiHang AS [Số Ghế/Hàng],
                        (SoHangGhe * SoGheMoiHang) AS [Tổng Số Ghế]
                    FROM PhongChieu
                    ORDER BY MaPhong ASC";

                var dt = Function.Query(query);
                dgvPhongChieu.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp danh sách phòng chiếu: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            numSoHangGhe.Value = 6;
            numSoGheMoiHang.Value = 10;
            txtMaPhong.Enabled = true;
            UpdateTotalSeatsAndPreview();
            LoadPhongChieu();
        }

        private void DgvPhongChieu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPhongChieu.Rows.Count) return;

            var row = dgvPhongChieu.Rows[e.RowIndex];
            txtMaPhong.Text = row.Cells["Mã Phòng"].Value?.ToString() ?? "";
            txtTenPhong.Text = row.Cells["Tên Phòng"].Value?.ToString() ?? "";

            if (int.TryParse(row.Cells["Số Hàng Ghế (A-Z)"].Value?.ToString(), out int rows))
                numSoHangGhe.Value = Math.Min(Math.Max(rows, numSoHangGhe.Minimum), numSoHangGhe.Maximum);

            if (int.TryParse(row.Cells["Số Ghế/Hàng"].Value?.ToString(), out int cols))
                numSoGheMoiHang.Value = Math.Min(Math.Max(cols, numSoGheMoiHang.Minimum), numSoGheMoiHang.Maximum);

            txtMaPhong.Enabled = false;
            UpdateTotalSeatsAndPreview();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string ma = txtMaPhong.Text.Trim();
            string ten = txtTenPhong.Text.Trim();
            int rows = (int)numSoHangGhe.Value;
            int cols = (int)numSoGheMoiHang.Value;
            int total = rows * cols;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng điền đủ Mã phòng và Tên phòng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string check = "SELECT COUNT(*) FROM PhongChieu WHERE MaPhong = @Ma";
                int count = Convert.ToInt32(Function.Scalar(check, new[] { new DbArg("@Ma", ma) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã phòng '{ma}' đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string insert = @"
                    INSERT INTO PhongChieu (MaPhong, TenPhong, SoHangGhe, SoGheMoiHang)
                    VALUES (@Ma, @Ten, @Rows, @Cols)";

                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@Rows", rows),
                    new DbArg("@Cols", cols)
                };
                Function.Execute(insert, p);

                MessageBox.Show("Thêm phòng chiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm phòng: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string ma = txtMaPhong.Text.Trim();
            string ten = txtTenPhong.Text.Trim();
            int rows = (int)numSoHangGhe.Value;
            int cols = (int)numSoGheMoiHang.Value;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string update = @"
                    UPDATE PhongChieu
                    SET TenPhong = @Ten, SoHangGhe = @Rows, SoGheMoiHang = @Cols
                    WHERE MaPhong = @Ma";

                var p = new[]
                {
                    new DbArg("@Ma", ma),
                    new DbArg("@Ten", ten),
                    new DbArg("@Rows", rows),
                    new DbArg("@Cols", cols)
                };
                Function.Execute(update, p);

                MessageBox.Show("Cập nhật phòng chiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sửa phòng: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string ma = txtMaPhong.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa từ bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng chiếu [{ma}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Function.Execute("DELETE FROM PhongChieu WHERE MaPhong = @Ma", new[] { new DbArg("@Ma", ma) });
                    MessageBox.Show("Đã xóa phòng chiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa phòng (có thể phòng đã được gắn vào Suất Chiếu): {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
