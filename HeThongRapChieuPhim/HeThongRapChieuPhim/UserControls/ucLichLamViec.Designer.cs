namespace HeThongRapChieuPhim.UserControls
{
    partial class ucLichLamViec
    {
        private void InitializeComponent()
        {
            _layout = new TableLayoutPanel();
            header = new Label();
            filterPanel = new FlowLayoutPanel();
            monthLabel = new Label();
            _dtpMonth = new DateTimePicker();
            btnReload = new Button();
            _lblReadOnly = new Label();
            _managementPanel = new Panel();
            flow = new FlowLayoutPanel();
            employeeLabel = new Label();
            _cboEmployee = new ComboBox();
            dayLabel = new Label();
            _dtpWorkDay = new DateTimePicker();
            shiftLabel = new Label();
            _cboShift = new ComboBox();
            _btnAdd = new Button();
            _btnChange = new Button();
            _btnComplete = new Button();
            _btnCancel = new Button();
            gridLayout = new TableLayoutPanel();
            schedulePanel = new Panel();
            _dgvSchedule = new DataGridView();
            scheduleTitle = new Label();
            totalsPanel = new Panel();
            _dgvMonthlyTotals = new DataGridView();
            totalsTitle = new Label();
            _layout.SuspendLayout();
            filterPanel.SuspendLayout();
            _managementPanel.SuspendLayout();
            flow.SuspendLayout();
            gridLayout.SuspendLayout();
            schedulePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvSchedule).BeginInit();
            totalsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvMonthlyTotals).BeginInit();
            SuspendLayout();
            // 
            // _layout
            // 
            _layout.ColumnCount = 1;
            _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _layout.Controls.Add(header, 0, 0);
            _layout.Controls.Add(filterPanel, 0, 1);
            _layout.Controls.Add(_managementPanel, 0, 2);
            _layout.Controls.Add(gridLayout, 0, 3);
            _layout.Dock = DockStyle.Fill;
            _layout.Location = new Point(0, 0);
            _layout.Name = "_layout";
            _layout.Padding = new Padding(14, 10, 14, 12);
            _layout.RowCount = 4;
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _layout.Size = new Size(1250, 800);
            _layout.TabIndex = 0;
            // 
            // header
            // 
            header.Dock = DockStyle.Fill;
            header.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            header.ForeColor = Color.FromArgb(0, 0, 64);
            header.Location = new Point(17, 10);
            header.Name = "header";
            header.Size = new Size(1216, 54);
            header.TabIndex = 0;
            header.Text = "LỊCH LÀM VIỆC & THAY CA";
            header.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // filterPanel
            // 
            filterPanel.AutoScroll = true;
            filterPanel.Controls.Add(monthLabel);
            filterPanel.Controls.Add(_dtpMonth);
            filterPanel.Controls.Add(btnReload);
            filterPanel.Controls.Add(_lblReadOnly);
            filterPanel.Dock = DockStyle.Fill;
            filterPanel.Location = new Point(17, 67);
            filterPanel.Name = "filterPanel";
            filterPanel.Padding = new Padding(0, 7, 0, 0);
            filterPanel.Size = new Size(1216, 42);
            filterPanel.TabIndex = 1;
            filterPanel.WrapContents = false;
            // 
            // monthLabel
            // 
            monthLabel.AutoSize = true;
            monthLabel.Location = new Point(0, 15);
            monthLabel.Margin = new Padding(0, 8, 8, 0);
            monthLabel.Name = "monthLabel";
            monthLabel.Size = new Size(89, 21);
            monthLabel.TabIndex = 0;
            monthLabel.Text = "Tháng xem:";
            // 
            // _dtpMonth
            // 
            _dtpMonth.CustomFormat = "MM/yyyy";
            _dtpMonth.Format = DateTimePickerFormat.Custom;
            _dtpMonth.Location = new Point(100, 10);
            _dtpMonth.Name = "_dtpMonth";
            _dtpMonth.ShowUpDown = true;
            _dtpMonth.Size = new Size(110, 29);
            _dtpMonth.TabIndex = 1;
            _dtpMonth.ValueChanged += MonthChanged;
            // 
            // btnReload
            // 
            btnReload.Location = new Point(216, 10);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(92, 30);
            btnReload.TabIndex = 2;
            btnReload.Text = "Tải lại";
            btnReload.Click += ReloadClicked;
            // 
            // _lblReadOnly
            // 
            _lblReadOnly.AutoSize = true;
            _lblReadOnly.Location = new Point(314, 7);
            _lblReadOnly.Name = "_lblReadOnly";
            _lblReadOnly.Size = new Size(543, 21);
            _lblReadOnly.TabIndex = 3;
            _lblReadOnly.Text = "Chỉ xem lịch của tài khoản đang đăng nhập; liên hệ quản lý nhân sự để đổi ca.";
            _lblReadOnly.Visible = false;
            // 
            // _managementPanel
            // 
            _managementPanel.Controls.Add(flow);
            _managementPanel.Dock = DockStyle.Fill;
            _managementPanel.Location = new Point(17, 115);
            _managementPanel.Name = "_managementPanel";
            _managementPanel.Size = new Size(1216, 70);
            _managementPanel.TabIndex = 2;
            // 
            // flow
            // 
            flow.AutoScroll = true;
            flow.Controls.Add(employeeLabel);
            flow.Controls.Add(_cboEmployee);
            flow.Controls.Add(dayLabel);
            flow.Controls.Add(_dtpWorkDay);
            flow.Controls.Add(shiftLabel);
            flow.Controls.Add(_cboShift);
            flow.Controls.Add(_btnAdd);
            flow.Controls.Add(_btnChange);
            flow.Controls.Add(_btnComplete);
            flow.Controls.Add(_btnCancel);
            flow.Dock = DockStyle.Fill;
            flow.Location = new Point(0, 0);
            flow.Name = "flow";
            flow.Padding = new Padding(0, 7, 0, 0);
            flow.Size = new Size(1216, 70);
            flow.TabIndex = 0;
            flow.WrapContents = false;
            // 
            // employeeLabel
            // 
            employeeLabel.AutoSize = true;
            employeeLabel.Location = new Point(0, 15);
            employeeLabel.Margin = new Padding(0, 8, 8, 0);
            employeeLabel.Name = "employeeLabel";
            employeeLabel.Size = new Size(78, 21);
            employeeLabel.TabIndex = 0;
            employeeLabel.Text = "Thu ngân:";
            // 
            // _cboEmployee
            // 
            _cboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboEmployee.Location = new Point(89, 10);
            _cboEmployee.Name = "_cboEmployee";
            _cboEmployee.Size = new Size(230, 29);
            _cboEmployee.TabIndex = 1;
            // 
            // dayLabel
            // 
            dayLabel.AutoSize = true;
            dayLabel.Location = new Point(322, 15);
            dayLabel.Margin = new Padding(0, 8, 8, 0);
            dayLabel.Name = "dayLabel";
            dayLabel.Size = new Size(50, 21);
            dayLabel.TabIndex = 2;
            dayLabel.Text = "Ngày:";
            // 
            // _dtpWorkDay
            // 
            _dtpWorkDay.Format = DateTimePickerFormat.Short;
            _dtpWorkDay.Location = new Point(383, 10);
            _dtpWorkDay.Name = "_dtpWorkDay";
            _dtpWorkDay.Size = new Size(120, 29);
            _dtpWorkDay.TabIndex = 3;
            _dtpWorkDay.ValueChanged += WorkDayChanged;
            // 
            // shiftLabel
            // 
            shiftLabel.AutoSize = true;
            shiftLabel.Location = new Point(506, 15);
            shiftLabel.Margin = new Padding(0, 8, 8, 0);
            shiftLabel.Name = "shiftLabel";
            shiftLabel.Size = new Size(31, 21);
            shiftLabel.TabIndex = 4;
            shiftLabel.Text = "Ca:";
            // 
            // _cboShift
            // 
            _cboShift.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboShift.Location = new Point(548, 10);
            _cboShift.Name = "_cboShift";
            _cboShift.Size = new Size(190, 29);
            _cboShift.TabIndex = 5;
            // 
            // _btnAdd
            // 
            _btnAdd.BackColor = Color.SeaGreen;
            _btnAdd.FlatStyle = FlatStyle.Flat;
            _btnAdd.ForeColor = Color.White;
            _btnAdd.Location = new Point(744, 10);
            _btnAdd.Name = "_btnAdd";
            _btnAdd.Size = new Size(82, 32);
            _btnAdd.TabIndex = 6;
            _btnAdd.Text = "Phân ca";
            _btnAdd.UseVisualStyleBackColor = false;
            _btnAdd.Click += AddClicked;
            // 
            // _btnChange
            // 
            _btnChange.BackColor = Color.SteelBlue;
            _btnChange.FlatStyle = FlatStyle.Flat;
            _btnChange.ForeColor = Color.White;
            _btnChange.Location = new Point(832, 10);
            _btnChange.Name = "_btnChange";
            _btnChange.Size = new Size(112, 32);
            _btnChange.TabIndex = 7;
            _btnChange.Text = "Đổi ca/người";
            _btnChange.UseVisualStyleBackColor = false;
            _btnChange.Click += ChangeClicked;
            // 
            // _btnComplete
            // 
            _btnComplete.BackColor = Color.DarkGoldenrod;
            _btnComplete.FlatStyle = FlatStyle.Flat;
            _btnComplete.ForeColor = Color.White;
            _btnComplete.Location = new Point(950, 10);
            _btnComplete.Name = "_btnComplete";
            _btnComplete.Size = new Size(132, 32);
            _btnComplete.TabIndex = 8;
            _btnComplete.Text = "Đánh dấu đã làm";
            _btnComplete.UseVisualStyleBackColor = false;
            _btnComplete.Click += CompleteClicked;
            // 
            // _btnCancel
            // 
            _btnCancel.BackColor = Color.IndianRed;
            _btnCancel.FlatStyle = FlatStyle.Flat;
            _btnCancel.ForeColor = Color.White;
            _btnCancel.Location = new Point(1088, 10);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(76, 32);
            _btnCancel.TabIndex = 9;
            _btnCancel.Text = "Hủy ca";
            _btnCancel.UseVisualStyleBackColor = false;
            _btnCancel.Click += CancelClicked;
            // 
            // gridLayout
            // 
            gridLayout.ColumnCount = 1;
            gridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            gridLayout.Controls.Add(schedulePanel, 0, 0);
            gridLayout.Controls.Add(totalsPanel, 0, 1);
            gridLayout.Dock = DockStyle.Fill;
            gridLayout.Location = new Point(17, 191);
            gridLayout.Name = "gridLayout";
            gridLayout.RowCount = 2;
            gridLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            gridLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            gridLayout.Size = new Size(1216, 594);
            gridLayout.TabIndex = 3;
            // 
            // schedulePanel
            // 
            schedulePanel.Controls.Add(_dgvSchedule);
            schedulePanel.Controls.Add(scheduleTitle);
            schedulePanel.Dock = DockStyle.Fill;
            schedulePanel.Location = new Point(3, 3);
            schedulePanel.Name = "schedulePanel";
            schedulePanel.Size = new Size(1210, 338);
            schedulePanel.TabIndex = 0;
            // 
            // _dgvSchedule
            // 
            _dgvSchedule.AllowUserToAddRows = false;
            _dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvSchedule.ColumnHeadersHeight = 29;
            _dgvSchedule.Dock = DockStyle.Fill;
            _dgvSchedule.Location = new Point(0, 32);
            _dgvSchedule.Name = "_dgvSchedule";
            _dgvSchedule.ReadOnly = true;
            _dgvSchedule.RowHeadersWidth = 51;
            _dgvSchedule.Size = new Size(1210, 306);
            _dgvSchedule.TabIndex = 0;
            _dgvSchedule.SelectionChanged += AssignmentChanged;
            // 
            // scheduleTitle
            // 
            scheduleTitle.Dock = DockStyle.Top;
            scheduleTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            scheduleTitle.Location = new Point(0, 0);
            scheduleTitle.Name = "scheduleTitle";
            scheduleTitle.Size = new Size(1210, 32);
            scheduleTitle.TabIndex = 1;
            scheduleTitle.Text = "Lịch phân ca trong tháng";
            // 
            // totalsPanel
            // 
            totalsPanel.Controls.Add(_dgvMonthlyTotals);
            totalsPanel.Controls.Add(totalsTitle);
            totalsPanel.Dock = DockStyle.Fill;
            totalsPanel.Location = new Point(3, 347);
            totalsPanel.Name = "totalsPanel";
            totalsPanel.Size = new Size(1210, 244);
            totalsPanel.TabIndex = 1;
            // 
            // _dgvMonthlyTotals
            // 
            _dgvMonthlyTotals.AllowUserToAddRows = false;
            _dgvMonthlyTotals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvMonthlyTotals.ColumnHeadersHeight = 29;
            _dgvMonthlyTotals.Dock = DockStyle.Fill;
            _dgvMonthlyTotals.Location = new Point(0, 32);
            _dgvMonthlyTotals.Name = "_dgvMonthlyTotals";
            _dgvMonthlyTotals.ReadOnly = true;
            _dgvMonthlyTotals.RowHeadersWidth = 51;
            _dgvMonthlyTotals.Size = new Size(1210, 212);
            _dgvMonthlyTotals.TabIndex = 0;
            // 
            // totalsTitle
            // 
            totalsTitle.Dock = DockStyle.Top;
            totalsTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            totalsTitle.Location = new Point(0, 0);
            totalsTitle.Name = "totalsTitle";
            totalsTitle.Size = new Size(1210, 32);
            totalsTitle.TabIndex = 1;
            totalsTitle.Text = "Số ca theo thu ngân trong tháng";
            // 
            // ucLichLamViec
            // 
            BackColor = Color.FromArgb(243, 246, 248);
            Controls.Add(_layout);
            Font = new Font("Segoe UI", 9.5F);
            Name = "ucLichLamViec";
            Size = new Size(1250, 800);
            _layout.ResumeLayout(false);
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            _managementPanel.ResumeLayout(false);
            flow.ResumeLayout(false);
            flow.PerformLayout();
            gridLayout.ResumeLayout(false);
            schedulePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgvSchedule).EndInit();
            totalsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgvMonthlyTotals).EndInit();


            _dgvSchedule.BackgroundColor = Color.White;
            _dgvSchedule.GridColor = Color.FromArgb(235, 239, 242);
            _dgvSchedule.BorderStyle = BorderStyle.None;
            _dgvSchedule.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            _dgvSchedule.EnableHeadersVisualStyles = false;
            _dgvSchedule.RowHeadersVisible = false;
            _dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvSchedule.MultiSelect = false;
            _dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvSchedule.AllowUserToAddRows = false;
            _dgvSchedule.AllowUserToDeleteRows = false;
            _dgvSchedule.ReadOnly = true;
            _dgvSchedule.RowTemplate.Height = 28;

            // Header Style
            _dgvSchedule.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _dgvSchedule.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
            _dgvSchedule.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 120, 131);
            _dgvSchedule.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            _dgvSchedule.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _dgvSchedule.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 2, 6, 2);
            _dgvSchedule.ColumnHeadersHeight = 36;

            // Rows Style
            _dgvSchedule.DefaultCellStyle.BackColor = Color.White;
            _dgvSchedule.DefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 59);
            _dgvSchedule.DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            _dgvSchedule.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 244, 239);
            _dgvSchedule.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 48, 59);
            _dgvSchedule.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);

            // Alternating Row Style
            _dgvSchedule.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            _dgvSchedule.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 59);
            _dgvSchedule.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 244, 239);
            _dgvSchedule.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 48, 59);

            _dgvMonthlyTotals.BackgroundColor = Color.White;
            _dgvMonthlyTotals.GridColor = Color.FromArgb(235, 239, 242);
            _dgvMonthlyTotals.BorderStyle = BorderStyle.None;
            _dgvMonthlyTotals.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            _dgvMonthlyTotals.EnableHeadersVisualStyles = false;
            _dgvMonthlyTotals.RowHeadersVisible = false;
            _dgvMonthlyTotals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvMonthlyTotals.MultiSelect = false;
            _dgvMonthlyTotals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvMonthlyTotals.AllowUserToAddRows = false;
            _dgvMonthlyTotals.AllowUserToDeleteRows = false;
            _dgvMonthlyTotals.ReadOnly = true;
            _dgvMonthlyTotals.RowTemplate.Height = 28;

            // Header Style
            _dgvMonthlyTotals.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _dgvMonthlyTotals.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
            _dgvMonthlyTotals.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 120, 131);
            _dgvMonthlyTotals.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            _dgvMonthlyTotals.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _dgvMonthlyTotals.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 2, 6, 2);
            _dgvMonthlyTotals.ColumnHeadersHeight = 36;

            // Rows Style
            _dgvMonthlyTotals.DefaultCellStyle.BackColor = Color.White;
            _dgvMonthlyTotals.DefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 59);
            _dgvMonthlyTotals.DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            _dgvMonthlyTotals.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 244, 239);
            _dgvMonthlyTotals.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 48, 59);
            _dgvMonthlyTotals.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);

            // Alternating Row Style
            _dgvMonthlyTotals.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            _dgvMonthlyTotals.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 59);
            _dgvMonthlyTotals.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 244, 239);
            _dgvMonthlyTotals.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 48, 59);

            ResumeLayout(false);
        }
        internal TableLayoutPanel _layout;
        internal Label header;
        internal FlowLayoutPanel filterPanel;
        internal Label monthLabel;
        internal DateTimePicker _dtpMonth;
        internal Button btnReload;
        internal Label _lblReadOnly;
        internal Panel _managementPanel;
        internal FlowLayoutPanel flow;
        internal Label employeeLabel;
        internal ComboBox _cboEmployee;
        internal Label dayLabel;
        internal DateTimePicker _dtpWorkDay;
        internal Label shiftLabel;
        internal ComboBox _cboShift;
        internal Button _btnAdd;
        internal Button _btnChange;
        internal Button _btnComplete;
        internal Button _btnCancel;
        internal TableLayoutPanel gridLayout;
        internal Panel schedulePanel;
        internal Panel totalsPanel;
        internal Label scheduleTitle;
        internal Label totalsTitle;
        internal DataGridView _dgvSchedule;
        internal DataGridView _dgvMonthlyTotals;
    }
}
