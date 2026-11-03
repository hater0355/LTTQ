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
            ConfigureSeatPreviewGrid();
            clbVipRows.ItemCheck += (_, _) => { if (IsHandleCreated) BeginInvoke(new Action(UpdateTotalSeatsAndPreview)); };
            numVipPercent.ValueChanged += (_, _) => UpdateTotalSeatsAndPreview();
            if (!DesignTimeHelper.IsDesignTime(this)) LoadPhongChieu();
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
            if (clbVipRows != null && clbVipRows.Items.Count != rows)
            {
                var selected = clbVipRows.CheckedItems.Cast<string>().ToHashSet();
                clbVipRows.Items.Clear();
                for (int i=0;i<rows;i++)
                {
                    string letter = ((char)('A'+i)).ToString();
                    clbVipRows.Items.Add(letter, selected.Contains(letter));
                }
            }
            char lastRowChar = (char)('A' + rows - 1);
            lblTongGhe.Text = $"⚡ Tổng số ghế: {rows * cols} ghế (Hàng A - {lastRowChar}, Ghế 1 - {cols})";

            RenderSeatPreview(rows, cols);
        }

        private void RenderSeatPreview(int rows, int cols)
        {
            for (int row = 0; row < pnlSeatPreview.Rows.Count; row++)
            {
                DataGridViewRow gridRow = pnlSeatPreview.Rows[row];
                gridRow.Visible = row < rows;
                gridRow.HeaderCell.Value = row < rows ? ((char)('A' + row)).ToString() : null;
                for (int column = 0; column < pnlSeatPreview.Columns.Count; column++)
                {
                    DataGridViewCell cell = gridRow.Cells[column];
                    bool visibleSeat = row < rows && column < cols;
                    pnlSeatPreview.Columns[column].Visible = column < cols;
                    cell.Value = visibleSeat ? (column + 1).ToString() : string.Empty;
                    cell.Style.BackColor = clbVipRows != null && clbVipRows.CheckedItems.Contains(((char)('A' + row)).ToString())
                        ? Color.FromArgb(255, 229, 153) : ThemeHelper.SeatAvailable;
                    cell.Style.ForeColor = ThemeHelper.TextPrimary;
                    cell.Style.SelectionBackColor = cell.Style.BackColor;
                    cell.Style.SelectionForeColor = cell.Style.ForeColor;
                }
            }
        }

        private void ConfigureSeatPreviewGrid()
        {
            pnlSeatPreview.Rows.Clear();
            for (int row = 0; row < 26; row++) pnlSeatPreview.Rows.Add();
            RenderSeatPreview((int)numSoHangGhe.Value, (int)numSoGheMoiHang.Value);
        }

        private void LoadPhongChieu()
        {
            try
            {
                RoomVipService.EnsureSchema();
                string query = @"
                    SELECT 
                        MaPhong AS [Mã Phòng],
                        TenPhong AS [Tên Phòng],
                        SoHangGhe AS [Số Hàng Ghế (A-Z)],
                        SoGheMoiHang AS [Số Ghế/Hàng],
                        (SoHangGhe * SoGheMoiHang) AS [Tổng Số Ghế],
                        ISNULL((SELECT HangVip FROM dbo.PhongChieuVip v WHERE v.MaPhong=PhongChieu.MaPhong),'') AS [Hàng VIP],
                        ISNULL((SELECT PhanTramTang FROM dbo.PhongChieuVip v WHERE v.MaPhong=PhongChieu.MaPhong),0) AS [Tăng giá VIP (%)]
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
            for (int i=0;i<clbVipRows.Items.Count;i++) clbVipRows.SetItemChecked(i,false);
            numVipPercent.Value = 0;
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

            try
            {
                var vip = RoomVipService.Get(txtMaPhong.Text);
                for (int i=0;i<clbVipRows.Items.Count;i++)
                    clbVipRows.SetItemChecked(i, vip.IsVip(Convert.ToString(clbVipRows.Items[i]) ?? ""));
                numVipPercent.Value = vip.Percent;
            }
            catch (Exception ex) { MessageBox.Show("Không tải được cấu hình VIP: " + ex.Message); return; }
            txtMaPhong.Enabled = false;
            UpdateTotalSeatsAndPreview();
        }

        private RoomVipSettings ReadVipSettings() => new(string.Join(",", clbVipRows.CheckedItems.Cast<string>()), numVipPercent.Value);

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
                int count = Convert.ToInt32(Function.Scalar(check, new[] { DbArg.VarChar20("@Ma", ma) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã phòng '{ma}' đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RoomVipService.SaveRoom(ma, ten, rows, cols, ReadVipSettings(), update: false);

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
                RoomVipService.SaveRoom(ma, ten, rows, cols, ReadVipSettings(), update: true);

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
                    Function.Execute("DELETE FROM PhongChieu WHERE MaPhong = @Ma", new[] { DbArg.VarChar20("@Ma", ma) });
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
