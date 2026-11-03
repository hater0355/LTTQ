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
        private string? _editingShowId;
        private string _pricedMovieId = "";

        public ucCaiSuatChieu()
        {
            InitializeComponent();
            _dtpFilterGioTu.Value = DateTime.Today.AddHours(10);
            _dtpFilterGioDen.Value = DateTime.Today.AddHours(23).AddMinutes(59);
            if (!DesignTimeHelper.IsDesignTime(this)) RefreshData();
        }

        private void FilterTimeChanged(object? sender, EventArgs e)
        {
            _dtpFilterGioTu.Enabled = _chkFilterGio.Checked;
            _dtpFilterGioDen.Enabled = _chkFilterGio.Checked;
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
            if (_isLoading) return;
            try
            {
                LoadMovieOptionsForDate(dtpNgayChieu.Value.Date);
                OnCriteriaChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải phim còn thời hạn cho ngày đã chọn: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DtpGioChieu_ValueChanged(object? sender, EventArgs e)
        {
            if (!_isLoading && !_isSelectingFromSlot) ValidateAndDisplayTimes();
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

                // Chỉ đưa phim còn hạn trong ngày đang xếp lịch vào danh sách chọn.
                LoadMovieOptionsForDate(dtpNgayChieu.Value.Date);

                // Load Filter Phim
                var dtFPhim = Function.Query("SELECT MaPhim, TenPhim FROM dbo.Phim ORDER BY TenPhim ASC");
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

                var dtFilterPhong = dtPhong.Copy();
                var allRoomRow = dtFilterPhong.NewRow();
                allRoomRow["MaPhong"] = "";
                allRoomRow["TenPhong"] = "-- Tất cả phòng --";
                dtFilterPhong.Rows.InsertAt(allRoomRow, 0);
                _cboFilterPhong.DataSource = dtFilterPhong;
                _cboFilterPhong.DisplayMember = "TenPhong";
                _cboFilterPhong.ValueMember = "MaPhong";

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

        private void LoadMovieOptionsForDate(DateTime scheduleDate, string includeMovieId = "")
        {
            string previouslySelectedMovie = cboPhim.SelectedValue?.ToString() ?? string.Empty;
            var dt = Function.Query(@"
                SELECT MaPhim, TenPhim, GiaPhim, ISNULL(ThoiLuong, 120) AS ThoiLuong,
                       CAST(1 AS bit) AS TrongThoiHan
                FROM dbo.Phim
                WHERE (NgayBatDauChieu IS NULL OR @Ngay >= NgayBatDauChieu)
                  AND (NgayKetThucChieu IS NULL OR @Ngay <= NgayKetThucChieu)
                ORDER BY TenPhim ASC", new[] { DbArg.Date("@Ngay", scheduleDate.Date) });

            if (!string.IsNullOrWhiteSpace(includeMovieId) &&
                !dt.AsEnumerable().Any(row => string.Equals(Convert.ToString(row["MaPhim"]), includeMovieId, StringComparison.OrdinalIgnoreCase)))
            {
                var oldMovie = Function.Query(@"
                    SELECT MaPhim, TenPhim, GiaPhim, ISNULL(ThoiLuong, 120) AS ThoiLuong,
                    CAST(0 AS bit) AS TrongThoiHan
                    FROM dbo.Phim WHERE MaPhim=@MaPhim", new[] { DbArg.VarChar20("@MaPhim", includeMovieId) });
                if (oldMovie.Rows.Count > 0)
                {
                    oldMovie.Rows[0]["TenPhim"] = Convert.ToString(oldMovie.Rows[0]["TenPhim"]) + " (ngoài thời hạn)";
                    dt.ImportRow(oldMovie.Rows[0]);
                }
            }

            bool wasLoading = _isLoading;
            _isLoading = true;
            try
            {
                cboPhim.DataSource = dt;
                cboPhim.DisplayMember = "TenPhim";
                cboPhim.ValueMember = "MaPhim";

                string desiredMovie = !string.IsNullOrWhiteSpace(includeMovieId) ? includeMovieId : previouslySelectedMovie;
                int desiredIndex = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.Equals(Convert.ToString(dt.Rows[i]["MaPhim"]), desiredMovie, StringComparison.OrdinalIgnoreCase))
                    {
                        desiredIndex = i;
                        break;
                    }
                }
                cboPhim.SelectedIndex = desiredIndex >= 0 ? desiredIndex : (dt.Rows.Count > 0 ? 0 : -1);
            }
            finally
            {
                _isLoading = wasLoading;
            }
        }

        private void BtnAutoMa_Click(object? sender, EventArgs e)
        {
            if (_editingShowId != null) return;
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
            btnSua.Enabled = false;

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

                string movieId = Convert.ToString(drv["MaPhim"]) ?? "";
                if (_pricedMovieId != movieId && decimal.TryParse(drv["GiaPhim"]?.ToString(), out decimal gia))
                {
                    numGiaSuatChieu.Value = Math.Max(numGiaSuatChieu.Minimum, Math.Min(numGiaSuatChieu.Maximum, gia));
                }
                _pricedMovieId = movieId;
            }
            else
            {
                _currentThoiLuong = 120;
                lblThoiLuong.Text = "⏱️ Chưa có phim hợp lệ";
                numGiaSuatChieu.Value = numGiaSuatChieu.Minimum;
                cboKhungGio.Items.Clear();
                cboKhungGio.Items.Add("-- Không có phim còn thời hạn trong ngày này --");
                cboKhungGio.SelectedIndex = 0;
                lblKetThucVal.Text = "--";
                lblDonPhongVal.Text = "--";
                lblStatus.Text = $"⛔ Không có phim còn thời hạn chiếu vào ngày {dtpNgayChieu.Value:dd/MM/yyyy}. Hãy chọn ngày khác hoặc gia hạn phim.";
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                btnThem.Enabled = false;
                return;
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
            string currentMaSC = _editingShowId ?? "";

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
            TimeSpan openTime = ShowtimeService.OpeningTime;  // 10:00 sáng
            TimeSpan closeTime = ShowtimeService.ClosingTime; // 00:00 đêm (24:00)
            int movieMinutes = _currentThoiLuong;
            int cleanMinutes = ShowtimeService.CleaningMinutes; // 1 tiếng dọn phòng
            int totalOccupy = movieMinutes + cleanMinutes;

            // Offer every quarter-hour that can fit the complete screening and cleaning.
            for (TimeSpan cur = openTime; cur.Add(TimeSpan.FromMinutes(totalOccupy)) <= closeTime;
                 cur = cur.Add(TimeSpan.FromMinutes(15)))
            {
                var slot = CreateSlotItem(cur, movieMinutes, cleanMinutes, existingShows);
                if (ngayChieu.Add(cur) <= DateTime.Now)
                {
                    slot.IsAvailable = false;
                    slot.Note = "Giờ chiếu đã qua";
                }
                slotList.Add(slot);
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

            // Suggestions must never overwrite the time entered or loaded for editing.
            cboKhungGio.SelectedIndex = -1;
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
                    DbArg.VarChar20("@Phong", maPhong),
                    DbArg.Date("@Ngay", ngayChieu),
                    DbArg.VarChar20("@Exclude", excludeMaSC)
                });

                foreach (DataRow row in dt.Rows)
                {
                    if (row["GioChieu"] is TimeSpan start)
                    {
                        int tl = Convert.ToInt32(row["ThoiLuong"]);
                        var end = start.Add(TimeSpan.FromMinutes(tl));
                        var clean = end.Add(TimeSpan.FromMinutes(ShowtimeService.CleaningMinutes));
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

            if (clean > ShowtimeService.ClosingTime)
            {
                return new TimeSlotItem
                {
                    StartTime = start,
                    EndTime = end,
                    CleanEndTime = clean,
                    IsAvailable = false,
                    Note = "Không đủ thời gian dọn phòng trước giờ đóng cửa"
                };
            }

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
                if (ShowtimeService.Overlaps(newStart, newClean, ex.StartTime, ex.CleanEndTime))
                {
                    return ex;
                }
            }
            return null;
        }

        private void CboKhungGio_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoading) return;
            if (cboKhungGio.SelectedItem is TimeSlotItem item)
            {
                if (!item.IsAvailable)
                {
                    MessageBox.Show(
                        $"Khung giờ này không thể chọn!\n\nLý do: {item.Note}\n\nQuy tắc rạp: Cần khoảng cách 1 tiếng giữa các lần chiếu để dọn phòng vệ sinh.",
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
            btnSua.Enabled = false;
            TimeSpan start = dtpGioChieu.Value.TimeOfDay;
            TimeSpan end = start.Add(TimeSpan.FromMinutes(_currentThoiLuong));
            TimeSpan clean = end.Add(TimeSpan.FromMinutes(ShowtimeService.CleaningMinutes));

            lblKetThucVal.Text = FormatTimeSpan(end);
            lblDonPhongVal.Text = FormatTimeSpan(clean);

            string maPhong = cboPhongChieu.SelectedValue?.ToString() ?? "";
            DateTime ngay = dtpNgayChieu.Value.Date;
            string currentMa = _editingShowId ?? "";
            if (ngay.Add(start) <= DateTime.Now)
            {
                lblStatus.Text = "Không thể tạo hoặc sửa suất chiếu đã bắt đầu.";
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                btnThem.Enabled = false;
                return;
            }

            // 1. Kiểm tra giờ mở cửa 10:00
            if (start < ShowtimeService.OpeningTime)
            {
                lblStatus.Text = $"⛔ Rạp chỉ hoạt động từ 10:00 sáng! Giờ bắt đầu ({FormatTimeSpan(start)}) không hợp lệ.";
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                btnThem.Enabled = false;
                return;
            }

            // Phim phải kết thúc và phòng phải được dọn xong trước giờ đóng cửa.
            if (clean > ShowtimeService.ClosingTime)
            {
                lblStatus.Text = $"⛔ Dọn phòng xong lúc {FormatTimeSpan(clean)}, vượt quá giờ đóng cửa rạp (00:00)! Vui lòng chọn giờ sớm hơn.";
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                btnThem.Enabled = false;
                return;
            }

            if (cboPhim.SelectedItem is not DataRowView selectedMovie)
            {
                lblStatus.Text = "⛔ Ngày này không có phim còn thời hạn để xếp lịch.";
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                btnThem.Enabled = false;
                return;
            }
            if (selectedMovie.Row.Table.Columns.Contains("TrongThoiHan") &&
                !Convert.ToBoolean(selectedMovie["TrongThoiHan"]))
            {
                lblStatus.Text = "⛔ Phim này đã ngoài thời hạn phát hành. Hãy gia hạn phim trước khi xếp lịch.";
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
            btnThem.Enabled = _editingShowId == null;
            btnSua.Enabled = _editingShowId != null;
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
                string filterPhong = _cboFilterPhong.SelectedValue?.ToString() ?? "";
                bool filterByDate = dtpFilterNgay.Checked;
                DateTime fDate = dtpFilterNgay.Value.Date;
                bool filterByTime = _chkFilterGio.Checked;
                TimeSpan startTime = _dtpFilterGioTu.Value.TimeOfDay;
                TimeSpan endTime = _dtpFilterGioDen.Value.TimeOfDay;
                if (filterByTime && startTime > endTime)
                {
                    MessageBox.Show("Giờ bắt đầu phải nhỏ hơn hoặc bằng giờ kết thúc.", "Khoảng giờ không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"
                    SELECT 
                        sc.MaSuatChieu AS [Mã Suất Chiếu],
                        p.TenPhim AS [Tên Phim],
                        pc.TenPhong AS [Phòng Chiếu],
                        ISNULL(p.ThoiLuong, 120) AS [Thời Lượng],
                        CONVERT(VARCHAR(10), sc.NgayChieu, 103) AS [Ngày Chiếu],
                        CONVERT(VARCHAR(5), sc.GioChieu, 108) AS [Giờ Bắt Đầu],
                        ISNULL(sc.GiaSuatChieu, p.GiaPhim) AS [Giá Vé (VNĐ)],
                        CASE
                            WHEN p.NgayBatDauChieu IS NULL AND p.NgayKetThucChieu IS NULL THEN N'Chưa giới hạn (phim cũ)'
                            WHEN sc.NgayChieu < p.NgayBatDauChieu OR sc.NgayChieu > p.NgayKetThucChieu THEN N'Ngoài thời hạn phim'
                            ELSE N'Còn thời hạn'
                        END AS [Thời Hạn Phim],
                        sc.MaPhim AS [MaPhim],
                        sc.MaPhong AS [MaPhong],
                        sc.NgayChieu AS [NgayChieuRaw],
                        sc.GioChieu AS [GioChieuRaw]
                    FROM SuatChieu sc
                    JOIN Phim p ON sc.MaPhim = p.MaPhim
                    JOIN PhongChieu pc ON sc.MaPhong = pc.MaPhong
                    WHERE (@MaPhim = '' OR sc.MaPhim = @MaPhim)
                      AND (@MaPhong = '' OR sc.MaPhong = @MaPhong)
                      AND (@FilterDate = 0 OR sc.NgayChieu = @Ngay)
                      AND (@FilterTime = 0 OR (sc.GioChieu >= @StartTime AND sc.GioChieu <= @EndTime))
                    ORDER BY sc.NgayChieu DESC, sc.GioChieu ASC";

                var p = new[]
                {
                    DbArg.VarChar20("@MaPhim", filterPhim),
                    DbArg.VarChar20("@MaPhong", filterPhong),
                    DbArg.Bit("@FilterDate", filterByDate),
                    DbArg.Date("@Ngay", fDate),
                    DbArg.Bit("@FilterTime", filterByTime),
                    DbArg.Time("@StartTime", startTime),
                    DbArg.Time("@EndTime", endTime)
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
                        var clean = end.Add(TimeSpan.FromMinutes(ShowtimeService.CleaningMinutes));
                        row["Giờ Kết Thúc"] = FormatTimeSpan(end);
                        row["Dọn Xong Lúc"] = FormatTimeSpan(clean);
                    }
                }

                dgvSuatChieu.DataSource = dt;

                if (dgvSuatChieu.Columns["MaPhim"] is { } movieColumn) movieColumn.Visible = false;
                if (dgvSuatChieu.Columns["MaPhong"] is { } roomColumn) roomColumn.Visible = false;
                if (dgvSuatChieu.Columns["NgayChieuRaw"] is { } dateColumn) dateColumn.Visible = false;
                if (dgvSuatChieu.Columns["GioChieuRaw"] is { } timeColumn) timeColumn.Visible = false;

                foreach (string centeredColumnName in new[] { "Mã Suất Chiếu", "Phòng Chiếu", "Thời Lượng", "Ngày Chiếu", "Giờ Bắt Đầu", "Giờ Kết Thúc", "Dọn Xong Lúc", "Thời Hạn Phim" })
                    if (dgvSuatChieu.Columns[centeredColumnName] is { } centeredColumn)
                        centeredColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (dgvSuatChieu.Columns["Giá Vé (VNĐ)"] is { } ticketPriceColumn)
                {
                    ticketPriceColumn.DefaultCellStyle.Format = "N0";
                    ticketPriceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            _editingShowId = txtMaSuatChieu.Text;
            txtMaSuatChieu.ReadOnly = true;
            lblTitle.Text = "CHỈNH SỬA SUẤT CHIẾU";

            string maPhim = row.Cells["MaPhim"].Value?.ToString() ?? "";
            _pricedMovieId = maPhim;
            _isLoading = true;
            try
            {
                if (row.Cells["NgayChieuRaw"].Value is DateTime dt)
                    dtpNgayChieu.Value = dt;
                else if (DateTime.TryParse(row.Cells["NgayChieuRaw"].Value?.ToString(), out DateTime parsedDt))
                    dtpNgayChieu.Value = parsedDt;

                LoadMovieOptionsForDate(dtpNgayChieu.Value.Date, maPhim);
                if (!string.IsNullOrEmpty(maPhim)) cboPhim.SelectedValue = maPhim;

                string maPhong = row.Cells["MaPhong"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(maPhong)) cboPhongChieu.SelectedValue = maPhong;

                if (row.Cells["GioChieuRaw"].Value is TimeSpan ts)
                    dtpGioChieu.Value = DateTime.Today.Add(ts);
                else if (TimeSpan.TryParse(row.Cells["GioChieuRaw"].Value?.ToString(), out TimeSpan parsedTs))
                    dtpGioChieu.Value = DateTime.Today.Add(parsedTs);

                if (decimal.TryParse(row.Cells["Giá Vé (VNĐ)"].Value?.ToString(), out decimal gia))
                    numGiaSuatChieu.Value = Math.Max(numGiaSuatChieu.Minimum, Math.Min(numGiaSuatChieu.Maximum, gia));
            }
            finally
            {
                _isLoading = false;
            }

            OnCriteriaChanged();
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (_editingShowId != null) return;
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
            if (gio < ShowtimeService.OpeningTime)
            {
                MessageBox.Show("Rạp chỉ hoạt động từ 10:00 sáng đến 00:00 đêm!\nGiờ bắt đầu không được sớm hơn 10:00.", "Giờ chiếu ngoài giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan end = gio.Add(TimeSpan.FromMinutes(_currentThoiLuong));
            TimeSpan clean = end.Add(TimeSpan.FromMinutes(ShowtimeService.CleaningMinutes));
            if (clean > ShowtimeService.ClosingTime)
            {
                MessageBox.Show($"Dọn phòng xong lúc {FormatTimeSpan(clean)}, vượt quá 00:00 đêm (giờ đóng cửa rạp)!\nVui lòng chọn giờ bắt đầu sớm hơn.", "Vượt quá giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                int count = Convert.ToInt32(Function.Scalar(checkQuery, new[] { DbArg.VarChar20("@Ma", maSC) }));
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
            if (_editingShowId == null) return;
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

            if (gio < ShowtimeService.OpeningTime)
            {
                MessageBox.Show("Rạp chỉ hoạt động từ 10:00 sáng đến 00:00 đêm!\nGiờ bắt đầu không được sớm hơn 10:00.", "Giờ chiếu ngoài giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan end = gio.Add(TimeSpan.FromMinutes(_currentThoiLuong));
            TimeSpan clean = end.Add(TimeSpan.FromMinutes(ShowtimeService.CleaningMinutes));
            if (clean > ShowtimeService.ClosingTime)
            {
                MessageBox.Show($"Dọn phòng xong lúc {FormatTimeSpan(clean)}, vượt quá 00:00 đêm (giờ đóng cửa rạp)!", "Vượt quá giờ rạp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            if (_editingShowId == null) return;
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
                ShowtimeService.Delete(maSC);
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
            _editingShowId = null;
            _pricedMovieId = "";
            txtMaSuatChieu.ReadOnly = false;
            lblTitle.Text = "THÊM SUẤT CHIẾU MỚI";
            BtnAutoMa_Click(null, EventArgs.Empty);
            _isLoading = true;
            try
            {
                dtpNgayChieu.Value = DateTime.Today;
                LoadMovieOptionsForDate(DateTime.Today);
                if (cboPhim.Items.Count > 0) cboPhim.SelectedIndex = 0;
                if (cboPhongChieu.Items.Count > 0) cboPhongChieu.SelectedIndex = 0;
                dtpGioChieu.Value = DateTime.Today.AddHours(10); // 10:00 sáng
            }
            finally
            {
                _isLoading = false;
            }
            OnCriteriaChanged();
            txtMaSuatChieu.Focus();
        }
    }
}
