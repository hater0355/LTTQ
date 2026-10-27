using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    /// <summary>Employee shift roster. HR/Admin can schedule and update shifts; cashiers see their own roster.</summary>
    public sealed class ucLichLamViec : UserControl
    {
        private readonly bool _canManage;
        private readonly TableLayoutPanel _layout = new();
        private readonly DateTimePicker _dtpMonth = new();
        private readonly DateTimePicker _dtpWorkDay = new();
        private readonly ComboBox _cboEmployee = new();
        private readonly ComboBox _cboShift = new();
        private readonly DataGridView _dgvSchedule = new();
        private readonly DataGridView _dgvMonthlyTotals = new();
        private readonly Panel _managementPanel = new();
        private readonly Label _lblReadOnly = new();
        private readonly Button _btnAdd = new();
        private readonly Button _btnChange = new();
        private readonly Button _btnComplete = new();
        private readonly Button _btnCancel = new();
        private long? _selectedAssignmentId;
        private bool _isLoading;

        public ucLichLamViec()
        {
            _canManage = CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin;
            BuildLayout();
            if (!DesignTimeHelper.IsDesignTime(this))
            {
                LoadLookups();
                LoadRoster();
            }
        }

        private void BuildLayout()
        {
            Dock = DockStyle.Fill;
            BackColor = ThemeHelper.AppBackground;
            Font = new Font("Segoe UI", 9.5f);

            _layout.Dock = DockStyle.Fill;
            _layout.Padding = new Padding(14, 10, 14, 12);
            _layout.ColumnCount = 1;
            _layout.RowCount = 4;
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, _canManage ? 76 : 8));
            _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(_layout);

            var header = new Label
            {
                Text = "LỊCH LÀM VIỆC & THAY CA",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = ThemeHelper.PrimaryDark,
                TextAlign = ContentAlignment.MiddleLeft
            };
            _layout.Controls.Add(header, 0, 0);

            var filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 7, 0, 0)
            };
            filterPanel.Controls.Add(new Label { Text = "Tháng xem:", AutoSize = true, Margin = new Padding(0, 8, 8, 0) });
            _dtpMonth.Format = DateTimePickerFormat.Custom;
            _dtpMonth.CustomFormat = "MM/yyyy";
            _dtpMonth.ShowUpDown = true;
            _dtpMonth.Width = 110;
            _dtpMonth.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            _dtpMonth.ValueChanged += (_, _) => { if (!_isLoading) LoadRoster(); };
            filterPanel.Controls.Add(_dtpMonth);
            var btnReload = CreateButton("Tải lại", ThemeHelper.PrimaryGreen, 92);
            btnReload.Click += (_, _) => { LoadLookups(); LoadRoster(); };
            filterPanel.Controls.Add(btnReload);
            _lblReadOnly.Text = "Thu ngân chỉ xem lịch và số ca của tài khoản đang đăng nhập; liên hệ Admin Nhân sự nếu cần đổi ca.";
            _lblReadOnly.AutoSize = true;
            _lblReadOnly.ForeColor = ThemeHelper.TextMuted;
            _lblReadOnly.Margin = new Padding(16, 8, 0, 0);
            _lblReadOnly.Visible = !_canManage;
            filterPanel.Controls.Add(_lblReadOnly);
            _layout.Controls.Add(filterPanel, 0, 1);

            BuildManagementPanel();
            _layout.Controls.Add(_managementPanel, 0, 2);

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                Panel1MinSize = 150,
                Panel2MinSize = 120,
                BorderStyle = BorderStyle.FixedSingle
            };
            split.SizeChanged += (_, _) =>
            {
                int desiredDistance = (int)(split.Height * 0.58);
                if (desiredDistance >= split.Panel1MinSize
                    && split.Height - desiredDistance - split.SplitterWidth >= split.Panel2MinSize
                    && split.SplitterDistance != desiredDistance)
                    split.SplitterDistance = desiredDistance;
            };
            BuildGridSection(split.Panel1, "Lịch phân ca trong tháng", _dgvSchedule);
            BuildGridSection(split.Panel2, "Số ca theo nhân viên trong tháng", _dgvMonthlyTotals);
            _layout.Controls.Add(split, 0, 3);

            _dtpMonth.ValueChanged += (_, _) => UpdateMonthHeader();
            UpdateMonthHeader();
        }

        private void BuildManagementPanel()
        {
            _managementPanel.Dock = DockStyle.Fill;
            _managementPanel.Visible = _canManage;
            _managementPanel.Padding = new Padding(0, 5, 0, 4);
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 7, 0, 0)
            };
            _managementPanel.Controls.Add(flow);

            flow.Controls.Add(new Label { Text = "Nhân viên:", AutoSize = true, Margin = new Padding(0, 8, 5, 0) });
            _cboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboEmployee.Width = 185;
            _cboEmployee.Margin = new Padding(0, 2, 8, 0);
            flow.Controls.Add(_cboEmployee);

            flow.Controls.Add(new Label { Text = "Ngày:", AutoSize = true, Margin = new Padding(0, 8, 5, 0) });
            _dtpWorkDay.Format = DateTimePickerFormat.Short;
            _dtpWorkDay.Width = 110;
            _dtpWorkDay.Margin = new Padding(0, 2, 8, 0);
            _dtpWorkDay.ValueChanged += (_, _) => SyncMonthToWorkDay();
            flow.Controls.Add(_dtpWorkDay);

            flow.Controls.Add(new Label { Text = "Ca:", AutoSize = true, Margin = new Padding(0, 8, 5, 0) });
            _cboShift.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboShift.Width = 175;
            _cboShift.Margin = new Padding(0, 2, 8, 0);
            flow.Controls.Add(_cboShift);

            _btnAdd.Text = "Phân ca";
            _btnAdd.Click += (_, _) => AddAssignment();
            flow.Controls.Add(_btnAdd);
            _btnChange.Text = "Đổi ca/người";
            _btnChange.Click += (_, _) => ChangeAssignment();
            flow.Controls.Add(_btnChange);
            _btnComplete.Text = "Đánh dấu đã làm";
            _btnComplete.Click += (_, _) => CompleteAssignment();
            flow.Controls.Add(_btnComplete);
            _btnCancel.Text = "Hủy ca";
            _btnCancel.Click += (_, _) => CancelAssignment();
            flow.Controls.Add(_btnCancel);

            foreach (var button in new[] { _btnAdd, _btnChange, _btnComplete, _btnCancel })
            {
                button.Width = button == _btnComplete ? 128 : button == _btnChange ? 112 : button == _btnCancel ? 76 : 82;
                button.Height = 32;
                button.Margin = new Padding(0, 0, 5, 0);
                button.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.ForeColor = Color.White;
            }
            _btnAdd.BackColor = ThemeHelper.AccentGreen;
            _btnChange.BackColor = ThemeHelper.AccentBlue;
            _btnComplete.BackColor = ThemeHelper.AccentGold;
            _btnCancel.BackColor = ThemeHelper.AccentRed;
        }

        private void BuildGridSection(Control parent, string title, DataGridView grid)
        {
            var label = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 32,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = ThemeHelper.PrimaryDark,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(7, 0, 0, 0)
            };
            grid.Dock = DockStyle.Fill;
            ThemeHelper.StyleDataGridView(grid);
            parent.Controls.Add(grid);
            parent.Controls.Add(label);
            grid.SelectionChanged += (_, _) => ReadSelectedAssignment();
        }

        private static Button CreateButton(string text, Color color, int width)
        {
            return new Button
            {
                Text = text,
                Width = width,
                Height = 30,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(10, 1, 0, 0)
            };
        }

        private void UpdateMonthHeader()
        {
            if (_dtpMonth.Value == DateTime.MinValue) return;
            Text = $"Lịch ca tháng {_dtpMonth.Value:MM/yyyy}";
        }

        private void SyncMonthToWorkDay()
        {
            if (_dtpMonth.Value.Year == _dtpWorkDay.Value.Year && _dtpMonth.Value.Month == _dtpWorkDay.Value.Month) return;
            _dtpMonth.Value = new DateTime(_dtpWorkDay.Value.Year, _dtpWorkDay.Value.Month, 1);
        }

        private void LoadLookups()
        {
            if (!_canManage) return;
            try
            {
                _isLoading = true;
                var employees = Function.Query(@"
                    SELECT MaNV, CONCAT(MaNV, N' - ', HoTen) AS TenHienThi
                    FROM dbo.NhanVien ORDER BY MaNV");
                _cboEmployee.DataSource = employees;
                _cboEmployee.DisplayMember = "TenHienThi";
                _cboEmployee.ValueMember = "MaNV";

                var shifts = Function.Query(@"
                    SELECT MaCa,
                           CONCAT(TenCa, N' (', CONVERT(CHAR(5), GioBatDau, 108), N' - ', CONVERT(CHAR(5), GioKetThuc, 108), N')') AS TenHienThi
                    FROM dbo.CaLamViec ORDER BY MaCa");
                _cboShift.DataSource = shifts;
                _cboShift.DisplayMember = "TenHienThi";
                _cboShift.ValueMember = "MaCa";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách nhân viên/ca làm. Hãy kiểm tra migration lịch ca đã chạy chưa.\n" + ex.Message,
                    "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { _isLoading = false; }
        }

        private void LoadRoster()
        {
            try
            {
                DateTime start = new(_dtpMonth.Value.Year, _dtpMonth.Value.Month, 1);
                DateTime end = start.AddMonths(1);
                string? maNV = _canManage ? null : CurrentUser.MaNV;
                _isLoading = true;

                _dgvSchedule.DataSource = Function.Query(@"
                    SELECT pc.MaPhanCa, pc.NgayLam,
                           CONVERT(VARCHAR(10), pc.NgayLam, 103) AS [Ngày làm],
                           pc.MaCa,
                           ca.TenCa AS [Ca],
                           CONVERT(CHAR(5), ca.GioBatDau, 108) AS [Bắt đầu],
                           CONVERT(CHAR(5), ca.GioKetThuc, 108) AS [Kết thúc],
                           nv.MaNV AS [Mã NV], nv.HoTen AS [Họ tên],
                           CASE pc.TrangThai WHEN 'Scheduled' THEN N'Đã phân' WHEN 'Completed' THEN N'Đã làm' ELSE N'Đã hủy' END AS [Trạng thái],
                           pc.TrangThai AS TrangThaiCode
                    FROM dbo.LichLamViec pc
                    INNER JOIN dbo.NhanVien nv ON nv.MaNV = pc.MaNV
                    INNER JOIN dbo.CaLamViec ca ON ca.MaCa = pc.MaCa
                    WHERE pc.NgayLam >= @start AND pc.NgayLam < @end
                      AND (@maNV IS NULL OR pc.MaNV = @maNV)
                    ORDER BY pc.NgayLam, ca.GioBatDau, nv.MaNV",
                    new[] { DbArg.Date("@start", start), DbArg.Date("@end", end), DbArg.VarChar20("@maNV", maNV) });
                SetColumnVisible(_dgvSchedule, "MaPhanCa", false);
                SetColumnVisible(_dgvSchedule, "NgayLam", false);
                SetColumnVisible(_dgvSchedule, "MaCa", false);
                SetColumnVisible(_dgvSchedule, "TrangThaiCode", false);
                if (GetColumn(_dgvSchedule, "Ngày làm") is { } dayColumn) dayColumn.FillWeight = 75;
                if (GetColumn(_dgvSchedule, "Trạng thái") is { } statusColumn) statusColumn.FillWeight = 75;

                _dgvMonthlyTotals.DataSource = Function.Query(@"
                    SELECT nv.MaNV AS [Mã NV], nv.HoTen AS [Họ tên], nv.ChucVu AS [Chức vụ],
                           SUM(CASE WHEN pc.TrangThai = 'Completed' THEN 1 ELSE 0 END) AS [Số ca đã làm],
                           SUM(CASE WHEN pc.TrangThai = 'Scheduled' THEN 1 ELSE 0 END) AS [Số ca sắp tới]
                    FROM dbo.NhanVien nv
                    LEFT JOIN dbo.LichLamViec pc
                      ON pc.MaNV = nv.MaNV AND pc.NgayLam >= @start AND pc.NgayLam < @end
                     AND pc.TrangThai IN ('Scheduled', 'Completed')
                    WHERE (@maNV IS NULL OR nv.MaNV = @maNV)
                    GROUP BY nv.MaNV, nv.HoTen, nv.ChucVu
                    ORDER BY nv.MaNV",
                    new[] { DbArg.Date("@start", start), DbArg.Date("@end", end), DbArg.VarChar20("@maNV", maNV) });
                if (GetColumn(_dgvMonthlyTotals, "Số ca đã làm") is { } countColumn)
                    countColumn.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                _selectedAssignmentId = null;
                UpdateActionButtons(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được lịch ca. Hãy chạy migration Database/Migrations/20261023_CashierWorkShifts.sql.\n" + ex.Message,
                    "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { _isLoading = false; }
        }

        private static DataGridViewColumn? GetColumn(DataGridView grid, string name) =>
            grid.Columns.Contains(name) ? grid.Columns[name] : null;

        private static void SetColumnVisible(DataGridView grid, string name, bool visible)
        {
            if (GetColumn(grid, name) is { } column) column.Visible = visible;
        }

        private void ReadSelectedAssignment()
        {
            if (_isLoading || _dgvSchedule.CurrentRow == null) return;
            var row = _dgvSchedule.CurrentRow;
            if (!long.TryParse(Convert.ToString(row.Cells["MaPhanCa"].Value), out long id))
            {
                _selectedAssignmentId = null;
                UpdateActionButtons(string.Empty);
                return;
            }

            _selectedAssignmentId = id;
            string status = Convert.ToString(row.Cells["TrangThaiCode"].Value) ?? string.Empty;
            if (DateTime.TryParse(Convert.ToString(row.Cells["NgayLam"].Value), out DateTime workDay))
                _dtpWorkDay.Value = workDay;
            string employeeId = Convert.ToString(row.Cells["Mã NV"].Value) ?? string.Empty;
            if (_cboEmployee.DataSource != null) _cboEmployee.SelectedValue = employeeId;
            if (_cboShift.DataSource != null && row.Cells["MaCa"].Value is object shiftId && shiftId != DBNull.Value)
                _cboShift.SelectedValue = shiftId;
            UpdateActionButtons(status);
        }

        private void UpdateActionButtons(string status)
        {
            bool selectedScheduled = _canManage && _selectedAssignmentId.HasValue && status == "Scheduled";
            _btnChange.Enabled = selectedScheduled;
            _btnComplete.Enabled = selectedScheduled;
            _btnCancel.Enabled = selectedScheduled;
        }

        private bool TryReadInputs(out string maNV, out byte maCa, out DateTime ngay)
        {
            maNV = Convert.ToString(_cboEmployee.SelectedValue) ?? string.Empty;
            maCa = 0;
            ngay = _dtpWorkDay.Value.Date;
            if (maNV.Length == 0 || _cboEmployee.SelectedValue == null)
            {
                MessageBox.Show("Hãy thêm hồ sơ nhân viên trước khi phân ca.", "Thiếu nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!byte.TryParse(Convert.ToString(_cboShift.SelectedValue), out maCa))
            {
                MessageBox.Show("Không tìm thấy ca làm hợp lệ.", "Thiếu ca", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void AddAssignment()
        {
            if (!TryReadInputs(out string maNV, out byte maCa, out DateTime ngay)) return;
            try
            {
                Function.Execute(@"
                    INSERT dbo.LichLamViec (MaNV, NgayLam, MaCa, TrangThai)
                    VALUES (@maNV, @ngay, @maCa, 'Scheduled')",
                    new[] { DbArg.VarChar20("@maNV", maNV), DbArg.Date("@ngay", ngay), DbArg.TinyInt("@maCa", maCa) });
                LoadRoster();
            }
            catch (DuplicateRecordException)
            {
                MessageBox.Show("Nhân viên này đã được xếp một ca trong ngày đã chọn.", "Trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { ShowDatabaseError("Không thể phân ca", ex); }
        }

        private void ChangeAssignment()
        {
            if (!_selectedAssignmentId.HasValue || !TryReadInputs(out string maNV, out byte maCa, out DateTime ngay)) return;
            try
            {
                int affected = Function.Execute(@"
                    UPDATE dbo.LichLamViec
                    SET MaNV=@maNV, NgayLam=@ngay, MaCa=@maCa
                    WHERE MaPhanCa=@id AND TrangThai='Scheduled'",
                    new[] { DbArg.VarChar20("@maNV", maNV), DbArg.Date("@ngay", ngay), DbArg.TinyInt("@maCa", maCa), DbArg.BigInt("@id", _selectedAssignmentId.Value) });
                if (affected != 1) throw new InvalidOperationException("Ca đã hoàn thành/hủy hoặc đã thay đổi. Tải lại danh sách rồi thử lại.");
                LoadRoster();
            }
            catch (DuplicateRecordException)
            {
                MessageBox.Show("Nhân viên đã có ca khác trong ngày mới. Mỗi người chỉ nhận một ca/ngày.", "Trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { ShowDatabaseError("Không thể đổi ca", ex); }
        }

        private void CompleteAssignment()
        {
            if (!_selectedAssignmentId.HasValue) return;
            try
            {
                int affected = Function.Execute(@"
                    UPDATE pc SET TrangThai='Completed'
                    FROM dbo.LichLamViec pc
                    INNER JOIN dbo.CaLamViec ca ON ca.MaCa=pc.MaCa
                    WHERE pc.MaPhanCa=@id AND pc.TrangThai='Scheduled'
                      AND DATEADD(SECOND, DATEDIFF(SECOND, CONVERT(TIME(0), '00:00'), ca.GioKetThuc), CONVERT(DATETIME2, pc.NgayLam)) <= SYSDATETIME()",
                    new[] { DbArg.BigInt("@id", _selectedAssignmentId.Value) });
                if (affected != 1)
                    throw new InvalidOperationException("Chỉ có thể đánh dấu đã làm sau khi ca kết thúc; ca có thể đã được cập nhật ở nơi khác.");
                LoadRoster();
            }
            catch (Exception ex) { ShowDatabaseError("Không thể cập nhật trạng thái ca", ex); }
        }

        private void CancelAssignment()
        {
            if (!_selectedAssignmentId.HasValue) return;
            if (MessageBox.Show("Đánh dấu ca này đã hủy? Lịch vẫn được lưu để đối chiếu.", "Xác nhận hủy ca",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                int affected = Function.Execute(@"
                    UPDATE dbo.LichLamViec SET TrangThai='Cancelled'
                    WHERE MaPhanCa=@id AND TrangThai='Scheduled'",
                    new[] { DbArg.BigInt("@id", _selectedAssignmentId.Value) });
                if (affected != 1) throw new InvalidOperationException("Ca đã hoàn thành/hủy hoặc đã thay đổi. Tải lại danh sách rồi thử lại.");
                LoadRoster();
            }
            catch (Exception ex) { ShowDatabaseError("Không thể hủy ca", ex); }
        }

        private static void ShowDatabaseError(string action, Exception ex) =>
            MessageBox.Show(action + ".\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
