namespace HeThongRapChieuPhim.UserControls
{
    partial class ucCaiSuatChieu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlContainer = new Panel();
            pnlGrid = new Panel();
            dgvSuatChieu = new DataGridView();
            pnlActions = new Panel();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblFPhim = new Label();
            cboFilterPhim = new ComboBox();
            dtpFilterNgay = new DateTimePicker();
            btnFilter = new Button();
            pnlForm = new GroupBox();
            lblMa = new Label();
            txtMaSuatChieu = new TextBox();
            btnAutoMa = new Button();
            lblPhim = new Label();
            cboPhim = new ComboBox();
            lblThoiLuong = new Label();
            lblPhong = new Label();
            cboPhongChieu = new ComboBox();
            lblNgay = new Label();
            dtpNgayChieu = new DateTimePicker();
            lblKhungGio = new Label();
            cboKhungGio = new ComboBox();
            btnChonSlotDau = new Button();
            lblGia = new Label();
            numGiaSuatChieu = new NumericUpDown();
            lblVnd = new Label();
            lblGio = new Label();
            dtpGioChieu = new DateTimePicker();
            lblKetThucTitle = new Label();
            lblKetThucVal = new Label();
            lblDonPhongTitle = new Label();
            lblDonPhongVal = new Label();
            lblRule = new Label();
            pnlStatusBox = new Panel();
            lblStatus = new Label();
            lblTitle = new Label();
            pnlContainer.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSuatChieu).BeginInit();
            pnlActions.SuspendLayout();
            pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numGiaSuatChieu).BeginInit();
            pnlStatusBox.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.Controls.Add(pnlGrid);
            pnlContainer.Controls.Add(pnlActions);
            pnlContainer.Controls.Add(pnlForm);
            pnlContainer.Controls.Add(lblTitle);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Padding = new Padding(20);
            pnlContainer.Size = new Size(1200, 680);
            pnlContainer.TabIndex = 0;
            // 
            // pnlGrid
            // 
            pnlGrid.BackColor = Color.White;
            pnlGrid.Controls.Add(dgvSuatChieu);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(20, 345);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(5);
            pnlGrid.Size = new Size(1160, 315);
            pnlGrid.TabIndex = 3;
            // 
            // dgvSuatChieu
            // 
            dgvSuatChieu.ColumnHeadersHeight = 29;
            dgvSuatChieu.Dock = DockStyle.Fill;
            dgvSuatChieu.Location = new Point(5, 5);
            dgvSuatChieu.Name = "dgvSuatChieu";
            dgvSuatChieu.RowHeadersWidth = 51;
            dgvSuatChieu.Size = new Size(1150, 305);
            dgvSuatChieu.TabIndex = 0;
            dgvSuatChieu.CellClick += DgvSuatChieu_CellClick;
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnThem);
            pnlActions.Controls.Add(btnSua);
            pnlActions.Controls.Add(btnXoa);
            pnlActions.Controls.Add(btnLamMoi);
            pnlActions.Controls.Add(lblFPhim);
            pnlActions.Controls.Add(cboFilterPhim);
            pnlActions.Controls.Add(dtpFilterNgay);
            pnlActions.Controls.Add(btnFilter);
            pnlActions.Dock = DockStyle.Top;
            pnlActions.Location = new Point(20, 290);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(0, 10, 0, 10);
            pnlActions.Size = new Size(1160, 55);
            pnlActions.TabIndex = 2;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.FromArgb(16, 185, 129);
            btnThem.Cursor = Cursors.Hand;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.Location = new Point(0, 8);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(160, 38);
            btnThem.TabIndex = 0;
            btnThem.Text = "➕ Thêm Suất Chiếu";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += BtnThem_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.FromArgb(59, 130, 246);
            btnSua.Cursor = Cursors.Hand;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSua.ForeColor = Color.White;
            btnSua.Location = new Point(170, 8);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 38);
            btnSua.TabIndex = 1;
            btnSua.Text = "✏️ Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += BtnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.FromArgb(220, 38, 38);
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXoa.ForeColor = Color.White;
            btnXoa.Location = new Point(280, 8);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 38);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "🗑️ Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += BtnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.FromArgb(230, 238, 233);
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.FromArgb(20, 50, 35);
            btnLamMoi.Location = new Point(390, 8);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 38);
            btnLamMoi.TabIndex = 3;
            btnLamMoi.Text = "🔄 Làm Mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += BtnLamMoi_Click;
            // 
            // lblFPhim
            // 
            lblFPhim.AutoSize = true;
            lblFPhim.Font = new Font("Segoe UI", 9F);
            lblFPhim.ForeColor = Color.FromArgb(20, 50, 35);
            lblFPhim.Location = new Point(530, 16);
            lblFPhim.Name = "lblFPhim";
            lblFPhim.Size = new Size(73, 20);
            lblFPhim.TabIndex = 4;
            lblFPhim.Text = "Lọc phim:";
            // 
            // cboFilterPhim
            // 
            cboFilterPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterPhim.Font = new Font("Segoe UI", 9.5F);
            cboFilterPhim.FormattingEnabled = true;
            cboFilterPhim.Location = new Point(595, 12);
            cboFilterPhim.Name = "cboFilterPhim";
            cboFilterPhim.Size = new Size(160, 29);
            cboFilterPhim.TabIndex = 5;
            // 
            // dtpFilterNgay
            // 
            dtpFilterNgay.Font = new Font("Segoe UI", 9.5F);
            dtpFilterNgay.Format = DateTimePickerFormat.Short;
            dtpFilterNgay.Location = new Point(764, 12);
            dtpFilterNgay.Name = "dtpFilterNgay";
            dtpFilterNgay.ShowCheckBox = true;
            dtpFilterNgay.Size = new Size(115, 29);
            dtpFilterNgay.TabIndex = 6;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(245, 158, 11);
            btnFilter.Cursor = Cursors.Hand;
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilter.ForeColor = Color.Black;
            btnFilter.Location = new Point(890, 10);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(70, 32);
            btnFilter.TabIndex = 7;
            btnFilter.Text = "Lọc";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += BtnFilter_Click;
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblMa);
            pnlForm.Controls.Add(txtMaSuatChieu);
            pnlForm.Controls.Add(btnAutoMa);
            pnlForm.Controls.Add(lblPhim);
            pnlForm.Controls.Add(cboPhim);
            pnlForm.Controls.Add(lblThoiLuong);
            pnlForm.Controls.Add(lblPhong);
            pnlForm.Controls.Add(cboPhongChieu);
            pnlForm.Controls.Add(lblNgay);
            pnlForm.Controls.Add(dtpNgayChieu);
            pnlForm.Controls.Add(lblKhungGio);
            pnlForm.Controls.Add(cboKhungGio);
            pnlForm.Controls.Add(btnChonSlotDau);
            pnlForm.Controls.Add(lblGia);
            pnlForm.Controls.Add(numGiaSuatChieu);
            pnlForm.Controls.Add(lblVnd);
            pnlForm.Controls.Add(lblGio);
            pnlForm.Controls.Add(dtpGioChieu);
            pnlForm.Controls.Add(lblKetThucTitle);
            pnlForm.Controls.Add(lblKetThucVal);
            pnlForm.Controls.Add(lblDonPhongTitle);
            pnlForm.Controls.Add(lblDonPhongVal);
            pnlForm.Controls.Add(lblRule);
            pnlForm.Controls.Add(pnlStatusBox);
            pnlForm.Dock = DockStyle.Top;
            pnlForm.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            pnlForm.ForeColor = Color.FromArgb(20, 83, 45);
            pnlForm.Location = new Point(20, 65);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(15);
            pnlForm.Size = new Size(1160, 225);
            pnlForm.TabIndex = 1;
            pnlForm.TabStop = false;
            pnlForm.Text = " Thiết lập lịch chiếu phim thông minh (10:00 - 00:00) ";
            // 
            // lblMa
            // 
            lblMa.AutoSize = true;
            lblMa.Font = new Font("Segoe UI", 9.5F);
            lblMa.ForeColor = Color.FromArgb(20, 50, 35);
            lblMa.Location = new Point(20, 32);
            lblMa.Name = "lblMa";
            lblMa.Size = new Size(109, 21);
            lblMa.TabIndex = 0;
            lblMa.Text = "Mã suất chiếu:";
            // 
            // txtMaSuatChieu
            // 
            txtMaSuatChieu.Font = new Font("Segoe UI", 10F);
            txtMaSuatChieu.Location = new Point(125, 28);
            txtMaSuatChieu.Name = "txtMaSuatChieu";
            txtMaSuatChieu.Size = new Size(100, 30);
            txtMaSuatChieu.TabIndex = 1;
            // 
            // btnAutoMa
            // 
            btnAutoMa.BackColor = Color.FromArgb(230, 238, 233);
            btnAutoMa.Cursor = Cursors.Hand;
            btnAutoMa.FlatAppearance.BorderSize = 0;
            btnAutoMa.FlatStyle = FlatStyle.Flat;
            btnAutoMa.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnAutoMa.ForeColor = Color.FromArgb(20, 50, 35);
            btnAutoMa.Location = new Point(230, 27);
            btnAutoMa.Name = "btnAutoMa";
            btnAutoMa.Size = new Size(85, 28);
            btnAutoMa.TabIndex = 2;
            btnAutoMa.Text = "⚡ Tạo mã";
            btnAutoMa.UseVisualStyleBackColor = false;
            btnAutoMa.Click += BtnAutoMa_Click;
            // 
            // lblPhim
            // 
            lblPhim.AutoSize = true;
            lblPhim.Font = new Font("Segoe UI", 9.5F);
            lblPhim.ForeColor = Color.FromArgb(20, 50, 35);
            lblPhim.Location = new Point(330, 32);
            lblPhim.Name = "lblPhim";
            lblPhim.Size = new Size(90, 21);
            lblPhim.TabIndex = 3;
            lblPhim.Text = "Chọn phim:";
            // 
            // cboPhim
            // 
            cboPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhim.Font = new Font("Segoe UI", 10F);
            cboPhim.FormattingEnabled = true;
            cboPhim.Location = new Point(410, 28);
            cboPhim.Name = "cboPhim";
            cboPhim.Size = new Size(240, 31);
            cboPhim.TabIndex = 4;
            cboPhim.SelectedIndexChanged += CboPhim_SelectedIndexChanged;
            // 
            // lblThoiLuong
            // 
            lblThoiLuong.AutoSize = true;
            lblThoiLuong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblThoiLuong.ForeColor = Color.FromArgb(217, 119, 6);
            lblThoiLuong.Location = new Point(655, 32);
            lblThoiLuong.Name = "lblThoiLuong";
            lblThoiLuong.Size = new Size(74, 21);
            lblThoiLuong.TabIndex = 5;
            lblThoiLuong.Text = "⏱️ 120p";
            // 
            // lblPhong
            // 
            lblPhong.AutoSize = true;
            lblPhong.Font = new Font("Segoe UI", 9.5F);
            lblPhong.ForeColor = Color.FromArgb(20, 50, 35);
            lblPhong.Location = new Point(780, 32);
            lblPhong.Name = "lblPhong";
            lblPhong.Size = new Size(99, 21);
            lblPhong.TabIndex = 6;
            lblPhong.Text = "Phòng chiếu:";
            // 
            // cboPhongChieu
            // 
            cboPhongChieu.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhongChieu.Font = new Font("Segoe UI", 10F);
            cboPhongChieu.FormattingEnabled = true;
            cboPhongChieu.Location = new Point(870, 28);
            cboPhongChieu.Name = "cboPhongChieu";
            cboPhongChieu.Size = new Size(200, 31);
            cboPhongChieu.TabIndex = 7;
            cboPhongChieu.SelectedIndexChanged += CboPhongChieu_SelectedIndexChanged;
            // 
            // lblNgay
            // 
            lblNgay.AutoSize = true;
            lblNgay.Font = new Font("Segoe UI", 9.5F);
            lblNgay.ForeColor = Color.FromArgb(20, 50, 35);
            lblNgay.Location = new Point(20, 74);
            lblNgay.Name = "lblNgay";
            lblNgay.Size = new Size(91, 21);
            lblNgay.TabIndex = 8;
            lblNgay.Text = "Ngày chiếu:";
            // 
            // dtpNgayChieu
            // 
            dtpNgayChieu.Font = new Font("Segoe UI", 10F);
            dtpNgayChieu.Format = DateTimePickerFormat.Short;
            dtpNgayChieu.Location = new Point(125, 70);
            dtpNgayChieu.Name = "dtpNgayChieu";
            dtpNgayChieu.Size = new Size(120, 30);
            dtpNgayChieu.TabIndex = 9;
            dtpNgayChieu.ValueChanged += DtpNgayChieu_ValueChanged;
            // 
            // lblKhungGio
            // 
            lblKhungGio.AutoSize = true;
            lblKhungGio.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblKhungGio.ForeColor = Color.FromArgb(20, 83, 45);
            lblKhungGio.Location = new Point(260, 74);
            lblKhungGio.Name = "lblKhungGio";
            lblKhungGio.Size = new Size(162, 21);
            lblKhungGio.TabIndex = 10;
            lblKhungGio.Text = "⚡ Khung giờ gợi ý:";
            // 
            // cboKhungGio
            // 
            cboKhungGio.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhungGio.Font = new Font("Segoe UI", 9.5F);
            cboKhungGio.FormattingEnabled = true;
            cboKhungGio.Location = new Point(395, 70);
            cboKhungGio.Name = "cboKhungGio";
            cboKhungGio.Size = new Size(405, 29);
            cboKhungGio.TabIndex = 11;
            cboKhungGio.SelectedIndexChanged += CboKhungGio_SelectedIndexChanged;
            // 
            // btnChonSlotDau
            // 
            btnChonSlotDau.BackColor = Color.FromArgb(59, 130, 246);
            btnChonSlotDau.Cursor = Cursors.Hand;
            btnChonSlotDau.FlatAppearance.BorderSize = 0;
            btnChonSlotDau.FlatStyle = FlatStyle.Flat;
            btnChonSlotDau.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnChonSlotDau.ForeColor = Color.White;
            btnChonSlotDau.Location = new Point(805, 69);
            btnChonSlotDau.Name = "btnChonSlotDau";
            btnChonSlotDau.Size = new Size(125, 29);
            btnChonSlotDau.TabIndex = 12;
            btnChonSlotDau.Text = "⚡ Điền slot trống";
            btnChonSlotDau.UseVisualStyleBackColor = false;
            btnChonSlotDau.Click += BtnChonSlotDau_Click;
            // 
            // lblGia
            // 
            lblGia.AutoSize = true;
            lblGia.Font = new Font("Segoe UI", 9.5F);
            lblGia.ForeColor = Color.FromArgb(20, 50, 35);
            lblGia.Location = new Point(945, 74);
            lblGia.Name = "lblGia";
            lblGia.Size = new Size(56, 21);
            lblGia.TabIndex = 13;
            lblGia.Text = "Giá vé:";
            // 
            // numGiaSuatChieu
            // 
            numGiaSuatChieu.Font = new Font("Segoe UI", 10F);
            numGiaSuatChieu.Increment = new decimal(new int[] { 5000, 0, 0, 0 });
            numGiaSuatChieu.Location = new Point(1000, 70);
            numGiaSuatChieu.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numGiaSuatChieu.Name = "numGiaSuatChieu";
            numGiaSuatChieu.Size = new Size(110, 30);
            numGiaSuatChieu.TabIndex = 14;
            numGiaSuatChieu.Value = new decimal(new int[] { 80000, 0, 0, 0 });
            // 
            // lblVnd
            // 
            lblVnd.AutoSize = true;
            lblVnd.Font = new Font("Segoe UI", 9.5F);
            lblVnd.ForeColor = Color.FromArgb(90, 115, 100);
            lblVnd.Location = new Point(1115, 74);
            lblVnd.Name = "lblVnd";
            lblVnd.Size = new Size(43, 21);
            lblVnd.TabIndex = 15;
            lblVnd.Text = "VNĐ";
            // 
            // lblGio
            // 
            lblGio.AutoSize = true;
            lblGio.Font = new Font("Segoe UI", 9.5F);
            lblGio.ForeColor = Color.FromArgb(20, 50, 35);
            lblGio.Location = new Point(20, 116);
            lblGio.Name = "lblGio";
            lblGio.Size = new Size(94, 21);
            lblGio.TabIndex = 16;
            lblGio.Text = "Giờ bắt đầu:";
            // 
            // dtpGioChieu
            // 
            dtpGioChieu.CustomFormat = "HH:mm";
            dtpGioChieu.Font = new Font("Segoe UI", 10F);
            dtpGioChieu.Format = DateTimePickerFormat.Custom;
            dtpGioChieu.Location = new Point(125, 112);
            dtpGioChieu.Name = "dtpGioChieu";
            dtpGioChieu.ShowUpDown = true;
            dtpGioChieu.Size = new Size(90, 30);
            dtpGioChieu.TabIndex = 17;
            dtpGioChieu.ValueChanged += DtpGioChieu_ValueChanged;
            // 
            // lblKetThucTitle
            // 
            lblKetThucTitle.AutoSize = true;
            lblKetThucTitle.Font = new Font("Segoe UI", 9.5F);
            lblKetThucTitle.ForeColor = Color.FromArgb(20, 50, 35);
            lblKetThucTitle.Location = new Point(230, 116);
            lblKetThucTitle.Name = "lblKetThucTitle";
            lblKetThucTitle.Size = new Size(97, 21);
            lblKetThucTitle.TabIndex = 18;
            lblKetThucTitle.Text = "Giờ kết thúc:";
            // 
            // lblKetThucVal
            // 
            lblKetThucVal.AutoSize = true;
            lblKetThucVal.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblKetThucVal.ForeColor = Color.FromArgb(217, 119, 6);
            lblKetThucVal.Location = new Point(315, 114);
            lblKetThucVal.Name = "lblKetThucVal";
            lblKetThucVal.Size = new Size(57, 25);
            lblKetThucVal.TabIndex = 19;
            lblKetThucVal.Text = "12:00";
            // 
            // lblDonPhongTitle
            // 
            lblDonPhongTitle.AutoSize = true;
            lblDonPhongTitle.Font = new Font("Segoe UI", 9.5F);
            lblDonPhongTitle.ForeColor = Color.FromArgb(20, 50, 35);
            lblDonPhongTitle.Location = new Point(380, 116);
            lblDonPhongTitle.Name = "lblDonPhongTitle";
            lblDonPhongTitle.Size = new Size(172, 21);
            lblDonPhongTitle.TabIndex = 20;
            lblDonPhongTitle.Text = "Dọn phòng xong (+1h):";
            // 
            // lblDonPhongVal
            // 
            lblDonPhongVal.AutoSize = true;
            lblDonPhongVal.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblDonPhongVal.ForeColor = Color.FromArgb(16, 163, 74);
            lblDonPhongVal.Location = new Point(530, 114);
            lblDonPhongVal.Name = "lblDonPhongVal";
            lblDonPhongVal.Size = new Size(57, 25);
            lblDonPhongVal.TabIndex = 21;
            lblDonPhongVal.Text = "13:00";
            // 
            // lblRule
            // 
            lblRule.AutoSize = true;
            lblRule.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblRule.ForeColor = Color.FromArgb(90, 115, 100);
            lblRule.Location = new Point(620, 116);
            lblRule.Name = "lblRule";
            lblRule.Size = new Size(604, 20);
            lblRule.TabIndex = 22;
            lblRule.Text = "📌 Giờ hoạt động: 10:00 - 00:00. Sau mỗi suất chiếu cách nhau 1 tiếng để dọn vệ sinh phòng.";
            // 
            // pnlStatusBox
            // 
            pnlStatusBox.BackColor = Color.FromArgb(240, 245, 241);
            pnlStatusBox.Controls.Add(lblStatus);
            pnlStatusBox.Location = new Point(20, 155);
            pnlStatusBox.Name = "pnlStatusBox";
            pnlStatusBox.Padding = new Padding(12, 6, 12, 6);
            pnlStatusBox.Size = new Size(1110, 48);
            pnlStatusBox.TabIndex = 23;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(16, 185, 129);
            lblStatus.Location = new Point(12, 6);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(1086, 36);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Đang kiểm tra lịch chiếu phòng...";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 83, 45);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1160, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "⏰ CÀI ĐẶT SUẤT CHIẾU (GẮN PHIM - PHÒNG - KHUNG GIỜ CHIẾU)";
            // 
            // ucCaiSuatChieu
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 245, 241);
            Controls.Add(pnlContainer);
            Font = new Font("Segoe UI", 10F);
            Name = "ucCaiSuatChieu";
            Size = new Size(1200, 680);
            pnlContainer.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSuatChieu).EndInit();
            pnlActions.ResumeLayout(false);
            pnlActions.PerformLayout();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numGiaSuatChieu).EndInit();
            pnlStatusBox.ResumeLayout(false);
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox pnlForm;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMaSuatChieu;
        private System.Windows.Forms.Button btnAutoMa;
        private System.Windows.Forms.Label lblPhim;
        private System.Windows.Forms.ComboBox cboPhim;
        private System.Windows.Forms.Label lblThoiLuong;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cboPhongChieu;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.DateTimePicker dtpNgayChieu;
        private System.Windows.Forms.Label lblKhungGio;
        private System.Windows.Forms.ComboBox cboKhungGio;
        private System.Windows.Forms.Button btnChonSlotDau;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.NumericUpDown numGiaSuatChieu;
        private System.Windows.Forms.Label lblVnd;
        private System.Windows.Forms.Label lblGio;
        private System.Windows.Forms.DateTimePicker dtpGioChieu;
        private System.Windows.Forms.Label lblKetThucTitle;
        private System.Windows.Forms.Label lblKetThucVal;
        private System.Windows.Forms.Label lblDonPhongTitle;
        private System.Windows.Forms.Label lblDonPhongVal;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.Panel pnlStatusBox;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblFPhim;
        private System.Windows.Forms.ComboBox cboFilterPhim;
        private System.Windows.Forms.DateTimePicker dtpFilterNgay;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvSuatChieu;
    }
}
