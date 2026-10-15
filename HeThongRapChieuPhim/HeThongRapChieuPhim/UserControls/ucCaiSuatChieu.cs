using HeThongRapChieuPhim.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucCaiSuatChieu : UserControl
    {
        private bool _scheduleLookupFailed;
        private bool _isLoading = false;
        private bool _isSelectingFromSlot = false;
        private int _currentThoiLuong = 120;

        public ucCaiSuatChieu()
        {
            InitializeComponent();
            ThemeHelper.StyleDataGridView(dgvSuatChieu);
            RefreshData();
        }

        public void RefreshData()
        {
            LoadDropdowns();
            LoadSuatChieu();
        }

        private void CboPhim_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_isLoading) OnCriteriaChanged();
        }

        private void CboPhongChieu_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_isLoading) OnCriteriaChanged();
        }

        private void DtpNgayChieu_ValueChanged(object? sender, EventArgs e)
        {
            if (!_isLoading) OnCriteriaChanged();
        }

        private void DtpGioChieu_ValueChanged(object? sender, EventArgs e)
        {
            if (!_isSelectingFromSlot) ValidateAndDisplayTimes();
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnFilter_Click(object? sender, EventArgs e)
        {
            LoadSuatChieu();
        }

        private void LoadDropdowns()
        {
            try
            {
                _isLoading = true;

                // Load Phim kèm ThoiLuong
                var dtPhim = Function.Query("SELECT MaPhim, TenPhim, GiaPhim, ISNULL(ThoiLuong, 120) AS ThoiLuong FROM Phim ORDER BY TenPhim ASC");
                cboPhim.DataSource = dtPhim;
                cboPhim.DisplayMember = "TenPhim";
                cboPhim.ValueMember = "MaPhim";

                // Load Filter Phim
                var dtFPhim = dtPhim.Copy();
                var allRow = dtFPhim.NewRow();
                allRow["MaPhim"] = "";
                allRow["TenPhim"] = "-- Tất cả phim --";
                dtFPhim.Rows.InsertAt(allRow, 0);
                cboFilterPhim.DataSource = dtFPhim;
                cboFilterPhim.DisplayMember = "TenPhim";
                cboFilterPhim.ValueMember = "MaPhim";

                // Load PhongChieu
                var dtPhong = Function.Query("SELECT MaPhong, TenPhong FROM PhongChieu ORDER BY MaPhong ASC");
                cboPhongChieu.DataSource = dtPhong;
                cboPhongChieu.DisplayMember = "TenPhong";
                cboPhongChieu.ValueMember = "MaPhong";

                _isLoading = false;

                // Sinh mã gợi ý ban đầu nếu ô mã đang trống
                if (string.IsNullOrWhiteSpace(txtMaSuatChieu.Text))
                {
                    BtnAutoMa_Click(null, EventArgs.Empty);
                }

                OnCriteriaChanged();
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show("Lỗi tải danh sách phim/phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAutoMa_Click(object? sender, EventArgs e)
        {
            try
            {
                var dt = Function.Query("SELECT MaSuatChieu FROM SuatChieu");
                int maxIndex = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string ma = row["MaSuatChieu"]?.ToString() ?? "";
                    if (ma.StartsWith("SC", StringComparison.OrdinalIgnoreCase))
                    {
                        string numPart = ma.Substring(2);
                        if (int.TryParse(numPart, out int idx))
                        {
                            if (idx > maxIndex) maxIndex = idx;
                        }
                    }
                }
                txtMaSuatChieu.Text = $"SC{(maxIndex + 1):D3}";
            }
            catch
            {
                txtMaSuatChieu.Text = $"SC{DateTime.Now:HHmmss}";
            }
        }

        private void OnCriteriaChanged()
        {
            if (_isLoading) return;

            // 1. Cập nhật thời lượng và giá vé từ phim
            if (cboPhim.SelectedItem is DataRowView drv)
            {
                if (int.TryParse(drv["ThoiLuong"]?.ToString(), out int tl) && tl > 0)
                {
                    _currentThoiLuong = tl;
                }
                else
                {
                    _currentThoiLuong = 120;
                }
                lblThoiLuong.Text = $"⏱️ {_currentThoiLuong}p";

                if (decimal.TryParse(drv["GiaPhim"]?.ToString(), out decimal gia))
                {
                    numGiaSuatChieu.Value = Math.Max(numGiaSuatChieu.Minimum, Math.Min(numGiaSuatChieu.Maximum, gia));
                }
            }

            // 2. Tính toán và nạp các khung giờ gợi ý (10:00 - 00:00) cho phòng và ngày này
            PopulateSuggestedSlots();

            // 3. Tính toán và hiển thị giờ kết thúc, dọn phòng, trạng thái
            ValidateAndDisplayTimes();
        }

        private class TimeSlotItem
        {
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public TimeSpan CleanEndTime { get; set; }
            public bool IsAvailable { get; set; }
            public string Note { get; set; } = "";

            public override string ToString()
            {
                string startStr = $"{StartTime.Hours:D2}:{StartTime.Minutes:D2}";
                string endStr = EndTime.TotalHours >= 24 ? "00:00" : $"{EndTime.Hours:D2}:{EndTime.Minutes:D2}";
                string cleanStr = CleanEndTime.TotalHours >= 24 ? "00:00" : $"{CleanEndTime.Hours:D2}:{CleanEndTime.Minutes:D2}";

                if (IsAvailable)
                {
                    return $"🟢 {startStr} - {endStr} (Trống | Dọn xong: {cleanStr})";
                }
                else
                {
                    return $"⛔ {startStr} - {endStr} [ĐÃ KÍN: {Note}]";
                }
            }
        }

        private void PopulateSuggestedSlots()
        {
            string maPhong = cboPhongChieu.SelectedValue?.ToString() ?? "";
            DateTime ngayChieu = dtpNgayChieu.Value.Date;
            string currentMaSC = txtMaSuatChieu.Text.Trim();

            cboKhungGio.Items.Clear();

            if (string.IsNullOrEmpty(maPhong))
            {
                cboKhungGio.Items.Add("-- Vui lòng chọn phòng chiếu --");
                cboKhungGio.SelectedIndex = 0;
                return;
            }

            // Lấy các suất chiếu đã có của phòng trong ngày
            var existingShows = GetExistingShowsForRoom(maPhong, ngayChieu, currentMaSC);
            if (_scheduleLookupFailed) return;

            var slotList = new List<TimeSlotItem>();
            TimeSpan openTime = TimeSpan.FromHours(10);  // 10:00 sáng
            TimeSpan closeTime = TimeSpan.FromHours(24); // 00:00 đêm (24:00)
            int movieMinutes = _currentThoiLuong;
            int cleanMinutes = 60; // 1 tiếng dọn phòng
            int totalOccupy = movieMinutes + cleanMinutes;

            // Sinh các mốc slot bắt đầu từ 10:00
            TimeSpan cur = openTime;
            while (cur.Add(TimeSpan.FromMinutes(movieMinutes)) <= closeTime)
            {
                var slot = CreateSlotItem(cur, movieMinutes, cleanMinutes, existingShows);
                slotList.Add(slot);

                // Nếu slot này bị trùng với 1 suất đã có E:
                // Ta có thể nhảy tiếp sau khi suất đó dọn phòng xong (E.CleanEnd) hoặc nhảy chu kỳ slot
                var conflict = FindFirstConflict(slot.StartTime, slot.CleanEndTime, existingShows);
                if (conflict != null && conflict.CleanEndTime > cur)
                {
                    cur = conflict.CleanEndTime;
                }
                else
                {
                    cur = cur.Add(TimeSpan.FromMinutes(totalOccupy));
                }
            }

            // Kiểm tra thêm các mốc bắt đầu ngay sau khi mỗi suất đã có dọn phòng xong (nếu chưa có trong list)
            foreach (var ex in existingShows)
            {
                if (ex.CleanEndTime >= openTime && ex.CleanEndTime.Add(TimeSpan.FromMinutes(movieMinutes)) <= closeTime)
                {
                    bool alreadyExists = slotList.Exists(s => Math.Abs((s.StartTime - ex.CleanEndTime).TotalMinutes) < 15);
                    if (!alreadyExists)
                    {
                        var extraSlot = CreateSlotItem(ex.CleanEndTime, movieMinutes, cleanMinutes, existingShows);
                        slotList.Add(extraSlot);
                    }
                }
            }

            // Sắp xếp các slot theo giờ bắt đầu tăng dần
            slotList.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));

            if (slotList.Count == 0)
            {
                cboKhungGio.Items.Add("-- Không có khung giờ khả dụng --");
                cboKhungGio.SelectedIndex = 0;
                return;
            }

            foreach (var slot in slotList)
            {
                cboKhungGio.Items.Add(slot);
            }

            // Mặc định chọn slot trống đầu tiên nếu có
            int firstAvailableIndex = slotList.FindIndex(s => s.IsAvailable);
            if (firstAvailableIndex >= 0)
            {
                cboKhungGio.SelectedIndex = firstAvailableIndex;
            }
            else
            {
                cboKhungGio.SelectedIndex = 0;
            }
        }

        private class ExistingShow
        {
            public string MaSC { get; set; } = "";
            public string TenPhim { get; set; } = "";
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public TimeSpan CleanEndTime { get; set; }
        }

        private List<ExistingShow> GetExistingShowsForRoom(string maPhong, DateTime ngayChieu, string excludeMaSC)
        {
            var list = new List<ExistingShow>();
            _scheduleLookupFailed = false;
            try
            {
                string query = @"
                    SELECT sc.MaSuatChieu, sc.GioChieu, p.TenPhim, ISNULL(p.ThoiLuong, 120) AS ThoiLuong
                    FROM SuatChieu sc
                    JOIN Phim p ON sc.MaPhim = p.MaPhim
                    WHERE sc.MaPhong = @Phong AND sc.NgayChieu = @Ngay
                      AND (@Exclude = '' OR sc.MaSuatChieu <> @Exclude)
                    ORDER BY sc.GioChieu ASC";

                var dt = Function.Query(query, new[] {
                    new DbArg("@Phong", maPhong),
                    new DbArg("@Ngay", ngayChieu),
                    new DbArg("@Exclude", excludeMaSC)
                });

                foreach (DataRow row in dt.Rows)
                {
                    if (row["GioChieu"] is TimeSpan start)
                    {
                        int tl = Convert.ToInt32(row["ThoiLuong"]);
                        var end = start.Add(TimeSpan.FromMinutes(tl));
                        var clean = end.Add(TimeSpan.FromMinutes(60));
                        list.Add(new ExistingShow
                        {
                            MaSC = row["MaSuatChieu"]?.ToString() ?? "",
                            TenPhim = row["TenPhim"]?.ToString() ?? "",
                            StartTime = start,
                            EndTime = end,
                            CleanEndTime = clean
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _scheduleLookupFailed = true;
                MessageBox.Show("Không thể kiểm tra lịch phòng chiếu. Hãy kiểm tra kết nối dữ liệu rồi thử lại.\n" + ex.Message,
                    "Không kiểm tra được lịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }

        private TimeSlotItem CreateSlotItem(TimeSpan start, int movieMinutes, int cleanMinutes, List<ExistingShow> existingShows)
        {
            var end = start.Add(TimeSpan.FromMinutes(movieMinutes));
            var clean = end.Add(TimeSpan.FromMinutes(cleanMinutes));

            var conflict = FindFirstConflict(start, clean, existingShows);
            if (conflict != null)
            {
                return new TimeSlotItem
                {
                    StartTime = start,
                    EndTime = end,
                    CleanEndTime = clean,
                    IsAvailable = false,
                    Note = $"{conflict.TenPhim} ({FormatTimeSpan(conflict.StartTime)}-{FormatTimeSpan(conflict.EndTime)} + dọn 1h)"
                };
            }
            else
            {
                return new TimeSlotItem
                {
                    StartTime = start,
                    EndTime = end,
                    CleanEndTime = clean,
                    IsAvailable = true
                };
            }
        }

        private ExistingShow? FindFirstConflict(TimeSpan newStart, TimeSpan newClean, List<ExistingShow> existingShows)
        {
            foreach (var ex in existingShows)
            {
                // Xung đột khi khoảng chiếm dụng [newStart, newClean) giao với [ex.StartTime, ex.CleanEndTime)
                if (newStart < ex.CleanEndTime && ex.StartTime < newClean)
                {
                    return ex;
                }
            }
            return null;
        }

        private void CboKhungGio_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboKhungGio.SelectedItem is TimeSlotItem item)
            {
                if (!item.IsAvailable)
                {
                    MessageBox.Show(
                        $"Khung giờ này không thể chọn vì phòng chiếu đã kín!\n\nLý do: {item.Note}\n\nQuy tắc rạp: Cần khoảng cách 1 tiếng giữa các lần chiếu để dọn phòng vệ sinh.",
                        "Khung giờ đã có suất chiếu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                _isSelectingFromSlot = true;
                dtpGioChieu.Value = DateTime.Today.Add(item.StartTime);
                _isSelectingFromSlot = false;
                ValidateAndDisplayTimes();
            }
        }

        private void BtnChonSlotDau_Click(object? sender, EventArgs e)
        {
            foreach (var obj in cboKhungGio.Items)
            {
                if (obj is TimeSlotItem item && item.IsAvailable)
                {
                    cboKhungGio.SelectedItem = item;
                    return;
                }
            }
            MessageBox.Show("Phòng chiếu này trong ngày đã kín tất cả các khung giờ từ 10:00 đến 00:00!\nVui lòng chọn phòng chiếu hoặc ngày chiếu khác.", "Đã kín lịch chiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ValidateAndDisplayTimes()
        {
            TimeSpan start = dtpGioChieu.Value.TimeOfDay;
            TimeSpan end = start.Add(TimeSpan.FromMinutes(_currentThoiLuong));
            TimeSpan clean = end.Add(TimeSpan.FromMinutes(60));

            lblKetThucVal.Text = FormatTimeSpan(end);
            lblDonPhongVal.Text = FormatTimeSpan(clean);

            string maPhong = cboPhongChieu.SelectedValue?.ToString() ?? "";
            DateTime ngay = dtpNgayChieu.Value.Date;
            string currentMa = txtMaSuatChieu.Text.Trim();

            // 1. Kiểm tra giờ mở cửa 10:00
            if (start < TimeSpan.FromHours(10))
            {
                lblStatus.Text = $"⛔ Rạp chỉ hoạt động từ 10:00 sáng! Giờ bắt đầu ({FormatTimeSpan(start)}) không hợp lệ.";
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                btnThem.Enabled = false;
                return;
            }

            // 2. Kiểm tra giờ kết thúc không vượt quá 00:00 đêm (24:00)
            if (end > TimeSpan.FromHours(24))
            {
                lblStatus.Text = $"⛔ Phim kết thúc lúc {FormatTimeSpan(end)}, vượt quá giờ đóng cửa rạp (00:00)! Vui lòng chọn giờ sớm hơn.";
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                btnThem.Enabled = false;
                return;
            }

            // 3. Kiểm tra trùng lịch phòng chiếu và thời gian dọn phòng 1 tiếng
            if (!string.IsNullOrEmpty(maPhong))
            {
                var existing = GetExistingShowsForRoom(maPhong, ngay, currentMa);
                if (_scheduleLookupFailed) return;
                var conflict = FindFirstConflict(start, clean, existing);
                if (conflict != null)
                {
                    lblStatus.Text = $"⛔ TRÙNG LỊCH: Phòng đã có suất [{conflict.MaSC}] phim '{conflict.TenPhim}' " +
                                     $"({FormatTimeSpan(conflict.StartTime)} - {FormatTimeSpan(conflict.EndTime)}, dọn phòng đến {FormatTimeSpan(conflict.CleanEndTime)})!";
                    lblStatus.ForeColor = ThemeHelper.AccentRed;
                    btnThem.Enabled = false;
                    return;
                }
            }

            // Hợp lệ
            string tenPhong = cboPhongChieu.Text;
            lblStatus.Text = $"✅ HỢP LỆ: {tenPhong} trống từ {FormatTimeSpan(start)} đến {FormatTimeSpan(clean)}. Sẵn sàng tạo suất chiếu!";
            lblStatus.ForeColor = ThemeHelper.AccentGreen;
            btnThem.Enabled = true;
        }

        private string FormatTimeSpan(TimeSpan ts)
        {
            int totalHours = (int)ts.TotalHours;
            int minutes = ts.Minutes;
            if (totalHours == 24 && minutes == 0) return "00:00 (Hết ngày)";
            if (totalHours >= 24)
            {
                int h = totalHours % 24;
                return $"{h:D2}:{minutes:D2} (+1 ngày)";
            }
            return $"{totalHours:D2}:{minutes:D2}";
        }

        private void LoadSuatChieu()
        {
            try
            {
                string filterPhim = cboFilterPhim.SelectedValue?.ToString() ?? "";
                bool filterByDate = dtpFilterNgay.Checked;
                DateTime fDate = dtpFilterNgay.Value.Date;

                string query = @"
                    SELECT 
                        sc.MaSuatChieu AS [Mã Suất Chiếu],
                        p.TenPhim AS [Tên Phim],
                        pc.TenPhong AS [Phòng Chiếu],
                        ISNULL(p.ThoiLuong, 120) AS [Thời Lượng],
                        CONVERT(VARCHAR(10), sc.NgayChieu, 103) AS [Ngày Chiếu],
                        CONVERT(VARCHAR(5), sc.GioChieu, 108) AS [Giờ Bắt Đầu],
                        ISNULL(sc.GiaSuatChieu, p.GiaPhim) AS [Giá Vé (VNĐ)],
                        sc.MaPhim AS [MaPhim],
                        sc.MaPhong AS [MaPhong],
                        sc.NgayChieu AS [NgayChieuRaw],
                        sc.GioChieu AS [GioChieuRaw]
                    FROM SuatChieu sc
                    JOIN Phim p ON sc.MaPhim = p.MaPhim
                    JOIN PhongChieu pc ON sc.MaPhong = pc.MaPhong
                    WHERE (@MaPhim = '' OR sc.MaPhim = @MaPhim)
                      AND (@FilterDate = 0 OR sc.NgayChieu = @Ngay)
                    ORDER BY sc.NgayChieu DESC, sc.GioChieu ASC";

                var p = new[]
                {
                    new DbArg("@MaPhim", filterPhim),
                    new DbArg("@FilterDate", filterByDate ? 1 : 0),
                    new DbArg("@Ngay", fDate)
                };

                var dt = Function.Query(query, p);

                // Thêm các cột tính toán [Giờ Kết Thúc] và [Dọn Phòng Xong]
                if (!dt.Columns.Contains("Giờ Kết Thúc"))
                    dt.Columns.Add("Giờ Kết Thúc", typeof(string));
                if (!dt.Columns.Contains("Dọn Xong Lúc"))
                    dt.Columns.Add("Dọn Xong Lúc", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["GioChieuRaw"] is TimeSpan start && int.TryParse(row["Thời Lượng"]?.ToString(), out int tl))
                    {
                        var end = start.Add(TimeSpan.FromMinutes(tl));
                        var clean = end.Add(TimeSpan.FromMinutes(60));
                        row["Giờ Kết Thúc"] = FormatTimeSpan(end);
                        row["Dọn Xong Lúc"] = FormatTimeSpan(clean);
                    }
                }

                dgvSuatChieu.DataSource = dt;

                if (dgvSuatChieu.Columns.Contains("MaPhim")) dgvSuatChieu.Columns["MaPhim"].Visible = false;
                if (dgvSuatChieu.Columns.Contains("MaPhong")) dgvSuatChieu.Columns["MaPhong"].Visible = false;
                if (dgvSuatChieu.Columns.Contains("NgayChieuRaw")) dgvSuatChieu.Columns["NgayChieuRaw"].Visible = false;
                if (dgvSuatChieu.Columns.Contains("GioChieuRaw")) dgvSuatChieu.Columns["GioChieuRaw"].Visible = false;

                if (dgvSuatChieu.Columns.Contains("Mã Suất Chiếu"))
                    dgvSuatChieu.Columns["Mã Suất Chiếu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvSuatChieu.Columns.Contains("Phòng Chiếu"))
                    dgvSuatChieu.Columns["Phòng Chiếu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvSuatChieu.Columns.Contains("Thời Lượng"))
                    dgvSuatChieu.Columns["Thời Lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvSuatChieu.Columns.Contains("Ngày Chiếu"))
                    dgvSuatChieu.Columns["Ngày Chiếu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvSuatChieu.Columns.Contains("Giờ Bắt Đầu"))
                    dgvSuatChieu.Columns["Giờ Bắt Đầu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvSuatChieu.Columns.Contains("Giờ Kết Thúc"))
                    dgvSuatChieu.Columns["Giờ Kết Thúc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvSuatChieu.Columns.Contains("Dọn Xong Lúc"))
                    dgvSuatChieu.Columns["Dọn Xong Lúc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (dgvSuatChieu.Columns.Contains("Giá Vé (VNĐ)"))
                {
                    dgvSuatChieu.Columns["Giá Vé (VNĐ)"].DefaultCellStyle.Format = "N0";
                    dgvSuatChieu.Columns["Giá Vé (VNĐ)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách suất chiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSuatChieu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSuatChieu.Rows.Count) return;

            var row = dgvSuatChieu.Rows[e.RowIndex];
            txtMaSuatChieu.Text = row.Cells["Mã Suất Chiếu"].Value?.ToString() ?? "";

            string maPhim = row.Cells["MaPhim"].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(maPhim)) cboPhim.SelectedValue = maPhim;

            string maPhong = row.Cells["MaPhong"].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(maPhong)) cboPhongChieu.SelectedValue = maPhong;

            if (row.Cells["NgayChieuRaw"].Value is DateTime dt)
                dtpNgayChieu.Value = dt;
            else if (DateTime.TryParse(row.Cells["NgayChieuRaw"].Value?.ToString(), out DateTime parsedDt))
                dtpNgayChieu.Value = parsedDt;

            if (row.Cells["GioChieuRaw"].Value is TimeSpan ts)
                dtpGioChieu.Value = DateTime.Today.Add(ts);
            else if (TimeSpan.TryParse(row.Cells["GioChieuRaw"].Value?.ToString(), out TimeSpan parsedTs))
                dtpGioChieu.Value = DateTime.Today.Add(parsedTs);

            if (decimal.TryParse(row.Cells["Giá Vé (VNĐ)"].Value?.ToString(), out decimal gia))
                numGiaSuatChieu.Value = Math.Max(numGiaSuatChieu.Minimum, Math.Min(numGiaSuatChieu.Maximum, gia));

            OnCriteriaChanged();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string maSC = txtMaSuatChieu.Text.Trim();
            if (string.IsNullOrEmpty(maSC))
            {
                BtnAutoMa_Click(null, EventArgs.Empty);
                maSC = txtMaSuatChieu.Text.Trim();
            }

            string maPhim = cboPhim.SelectedValue?.ToString() ?? "";
            string maPhong = cboPhongChieu.SelectedValue?.ToString() ?? "";
            if (string.IsNullOrEmpty(maPhim) || string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng chọn Phim và Phòng chiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime ngay = dtpNgayChieu.Value.Date;
            TimeSpan gio = dtpGioChieu.Value.TimeOfDay;

            // Kiểm tra giờ mở/đóng cửa và trùng phòng có 1 tiếng dọn dẹp
            if (gio < TimeSpan.FromHours(10))
            {
                MessageBox.Show("Rạp chỉ hoạt động từ 10:00 sáng đến 00:00 đêm!\nGiờ bắt đầu không được sớm hơn 10:00.", "Giờ chiếu ngoài giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan end = gio.Add(TimeSpan.FromMinutes(_currentThoiLuong));
            if (end > TimeSpan.FromHours(24))
            {
                MessageBox.Show($"Phim kết thúc lúc {FormatTimeSpan(end)}, vượt quá 00:00 đêm (giờ đóng cửa rạp)!\nVui lòng chọn giờ bắt đầu sớm hơn.", "Vượt quá giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan clean = end.Add(TimeSpan.FromMinutes(60));
            var existing = GetExistingShowsForRoom(maPhong, ngay, "");
            if (_scheduleLookupFailed) return;
            var conflict = FindFirstConflict(gio, clean, existing);
            if (conflict != null)
            {
                MessageBox.Show(
                    $"Không thể thêm suất chiếu vì phòng đã có lịch!\n\n" +
                    $"Trùng với suất [{conflict.MaSC}] phim '{conflict.TenPhim}' " +
                    $"({FormatTimeSpan(conflict.StartTime)} - {FormatTimeSpan(conflict.EndTime)}, dọn phòng đến {FormatTimeSpan(conflict.CleanEndTime)})\n\n" +
                    $"Quy tắc: Các lần chiếu cùng phòng phải cách nhau ít nhất 1 tiếng để dọn phòng vệ sinh.",
                    "Xung đột lịch chiếu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM SuatChieu WHERE MaSuatChieu = @Ma";
                int count = Convert.ToInt32(Function.Scalar(checkQuery, new[] { new DbArg("@Ma", maSC) }));
                if (count > 0)
                {
                    MessageBox.Show($"Mã suất chiếu '{maSC}' đã tồn tại! Vui lòng chọn hoặc tự tạo mã khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ShowtimeService.Save(maSC, maPhim, maPhong, ngay, gio, numGiaSuatChieu.Value, update: false);
                MessageBox.Show($"Thêm suất chiếu '{maSC}' thành công!\nChiếu từ {FormatTimeSpan(gio)} đến {FormatTimeSpan(end)} (Dọn phòng đến {FormatTimeSpan(clean)}).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadSuatChieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm suất chiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string maSC = txtMaSuatChieu.Text.Trim();
            if (string.IsNullOrEmpty(maSC))
            {
                MessageBox.Show("Vui lòng chọn suất chiếu cần sửa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhim = cboPhim.SelectedValue?.ToString() ?? "";
            string maPhong = cboPhongChieu.SelectedValue?.ToString() ?? "";
            DateTime ngay = dtpNgayChieu.Value.Date;
            TimeSpan gio = dtpGioChieu.Value.TimeOfDay;

            if (gio < TimeSpan.FromHours(10))
            {
                MessageBox.Show("Rạp chỉ hoạt động từ 10:00 sáng đến 00:00 đêm!\nGiờ bắt đầu không được sớm hơn 10:00.", "Giờ chiếu ngoài giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan end = gio.Add(TimeSpan.FromMinutes(_currentThoiLuong));
            if (end > TimeSpan.FromHours(24))
            {
                MessageBox.Show($"Phim kết thúc lúc {FormatTimeSpan(end)}, vượt quá 00:00 đêm (giờ đóng cửa rạp)!", "Vượt quá giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan clean = end.Add(TimeSpan.FromMinutes(60));
            var existing = GetExistingShowsForRoom(maPhong, ngay, maSC);
            if (_scheduleLookupFailed) return;
            var conflict = FindFirstConflict(gio, clean, existing);
            if (conflict != null)
            {
                MessageBox.Show(
                    $"Không thể cập nhật vì bị trùng lịch phòng chiếu!\n\n" +
                    $"Trùng với suất [{conflict.MaSC}] phim '{conflict.TenPhim}' " +
                    $"({FormatTimeSpan(conflict.StartTime)} - {FormatTimeSpan(conflict.EndTime)}, dọn phòng đến {FormatTimeSpan(conflict.CleanEndTime)})\n\n" +
                    $"Quy tắc: Các lần chiếu cùng phòng phải cách nhau ít nhất 1 tiếng để dọn phòng.",
                    "Xung đột lịch chiếu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                ShowtimeService.Save(maSC, maPhim, maPhong, ngay, gio, numGiaSuatChieu.Value, update: true);
                MessageBox.Show("Cập nhật suất chiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSuatChieu();
                OnCriteriaChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string maSC = txtMaSuatChieu.Text.Trim();
            if (string.IsNullOrEmpty(maSC))
            {
                MessageBox.Show("Vui lòng chọn suất chiếu cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa suất chiếu '{maSC}'?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.OK) return;

            try
            {
                string deleteQuery = "DELETE FROM SuatChieu WHERE MaSuatChieu = @Ma";
                Function.Execute(deleteQuery, new[] { new DbArg("@Ma", maSC) });
                MessageBox.Show("Đã xóa suất chiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadSuatChieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa suất chiếu (có thể đã có vé được bán): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            BtnAutoMa_Click(null, EventArgs.Empty);
            if (cboPhim.Items.Count > 0) cboPhim.SelectedIndex = 0;
            if (cboPhongChieu.Items.Count > 0) cboPhongChieu.SelectedIndex = 0;
            dtpNgayChieu.Value = DateTime.Today;
            dtpGioChieu.Value = DateTime.Today.AddHours(10); // 10:00 sáng
            OnCriteriaChanged();
            txtMaSuatChieu.Focus();
        }
    }
}
