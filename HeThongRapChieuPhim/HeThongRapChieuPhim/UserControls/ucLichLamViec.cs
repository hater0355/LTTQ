using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    /// <summary>Employee shift roster. HR/Admin can schedule and update shifts; cashiers see their own roster.</summary>
    public partial class ucLichLamViec : UserControl
    {
        private readonly bool _canManage;
        private long? _selectedAssignmentId;
        private bool _isLoading;

        public ucLichLamViec()
        {
            _canManage = CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin;
            _isLoading = true;
            InitializeComponent();
            _dtpMonth.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            _isLoading = false;
            if (!DesignTimeHelper.IsDesignTime(this))
            {
                _managementPanel.Visible = _canManage;
                _lblReadOnly.Visible = !_canManage;
                _layout.RowStyles[2].Height = _canManage ? 76 : 8;
            }
            if (!DesignTimeHelper.IsDesignTime(this))
            {
                LoadLookups();
                LoadRoster();
            }
        }

        private void MonthChanged(object? sender, EventArgs e)
        {
            UpdateMonthHeader();
            if (!_isLoading && !DesignTimeHelper.IsDesignTime(this)) LoadRoster();
        }
        private void ReloadClicked(object? sender, EventArgs e) { LoadLookups(); LoadRoster(); }
        private void WorkDayChanged(object? sender, EventArgs e) { if (!_isLoading) SyncMonthToWorkDay(); }
        private void AssignmentChanged(object? sender, EventArgs e) => ReadSelectedAssignment();
        private void AddClicked(object? sender, EventArgs e) => AddAssignment();
        private void ChangeClicked(object? sender, EventArgs e) => ChangeAssignment();
        private void CompleteClicked(object? sender, EventArgs e) => CompleteAssignment();
        private void CancelClicked(object? sender, EventArgs e) => CancelAssignment();

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
                    FROM dbo.NhanVien WHERE ChucVu=N'Thu ngân' ORDER BY MaNV");
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
                      AND nv.ChucVu=N'Thu ngân' AND (@maNV IS NULL OR pc.MaNV = @maNV)
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
                    WHERE nv.ChucVu=N'Thu ngân' AND (@maNV IS NULL OR nv.MaNV = @maNV)
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
                MessageBox.Show("Không tải được lịch ca. Hãy chạy Database/NHHcinema.sql.\n" + ex.Message,
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
                MessageBox.Show("Hãy chọn hồ sơ thu ngân trước khi phân ca.", "Thiếu nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (!CurrentUser.IsSuperAdmin && !CurrentUser.IsHumanResourcesAdmin) return;
            if (!TryReadInputs(out string maNV, out byte maCa, out DateTime ngay)) return;
            try
            {
                int affected = Function.Execute(@"
                    INSERT dbo.LichLamViec (MaNV, NgayLam, MaCa, TrangThai)
                    SELECT MaNV, @ngay, @maCa, 'Scheduled' FROM dbo.NhanVien WITH (UPDLOCK, HOLDLOCK)
                    WHERE MaNV=@maNV AND ChucVu=N'Thu ngân'",
                    new[] { DbArg.VarChar20("@maNV", maNV), DbArg.Date("@ngay", ngay), DbArg.TinyInt("@maCa", maCa) });
                if (affected != 1) throw new InvalidOperationException("Chỉ có thể phân ca cho thu ngân. Hãy tải lại danh sách.");
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
            if (!CurrentUser.IsSuperAdmin && !CurrentUser.IsHumanResourcesAdmin) return;
            if (!_selectedAssignmentId.HasValue || !TryReadInputs(out string maNV, out byte maCa, out DateTime ngay)) return;
            try
            {
                int affected = Function.Execute(@"
                    UPDATE dbo.LichLamViec
                    SET MaNV=@maNV, NgayLam=@ngay, MaCa=@maCa
                    WHERE MaPhanCa=@id AND TrangThai='Scheduled'
                      AND EXISTS (SELECT 1 FROM dbo.NhanVien nv WITH (UPDLOCK, HOLDLOCK) WHERE nv.MaNV=@maNV AND nv.ChucVu=N'Thu ngân')",
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
            if (!CurrentUser.IsSuperAdmin && !CurrentUser.IsHumanResourcesAdmin) return;
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
            if (!CurrentUser.IsSuperAdmin && !CurrentUser.IsHumanResourcesAdmin) return;
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
