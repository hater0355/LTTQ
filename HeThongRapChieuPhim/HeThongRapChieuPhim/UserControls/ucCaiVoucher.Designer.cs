namespace HeThongRapChieuPhim.UserControls
{
    partial class ucCaiVoucher
    {
        private System.ComponentModel.IContainer components = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvVoucher = new System.Windows.Forms.DataGridView();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.dtpHanSuDung = new System.Windows.Forms.DateTimePicker();
            this.lblHanSuDung = new System.Windows.Forms.Label();
            this.lblDonViGiaTri = new System.Windows.Forms.Label();
            this.numGiaTriGiam = new System.Windows.Forms.NumericUpDown();
            this.lblGiaTriGiam = new System.Windows.Forms.Label();
            this.cboLoaiGiam = new System.Windows.Forms.ComboBox();
            this.lblLoaiGiam = new System.Windows.Forms.Label();
            this.txtTenVoucher = new System.Windows.Forms.TextBox();
            this.lblTenVoucher = new System.Windows.Forms.Label();
            this.btnAutoMa = new System.Windows.Forms.Button();
            this.txtMaVoucher = new System.Windows.Forms.TextBox();
            this.lblMaVoucher = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTriGiam)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(1000, 650);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.SystemColors.Control;
            this.pnlRight.Controls.Add(this.dgvVoucher);
            this.pnlRight.Controls.Add(this.pnlSearch);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(375, 15);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(12);
            this.pnlRight.Size = new System.Drawing.Size(610, 620);
            this.pnlRight.TabIndex = 1;
            // 
            // dgvVoucher
            // 
            this.dgvVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVoucher.Location = new System.Drawing.Point(12, 60);
            this.dgvVoucher.Name = "dgvVoucher";
            this.dgvVoucher.Size = new System.Drawing.Size(586, 548);
            this.dgvVoucher.TabIndex = 1;
            this.dgvVoucher.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVoucher_CellClick);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.btnTimKiem);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(12, 12);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(586, 48);
            this.pnlSearch.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnTimKiem.ForeColor = System.Drawing.Color.Black;
            this.btnTimKiem.Location = new System.Drawing.Point(486, 8);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(95, 32);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.BtnTimKiem_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.txtSearch.Location = new System.Drawing.Point(82, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Nhập mã voucher hoặc tên voucher để tìm...";
            this.txtSearch.Size = new System.Drawing.Size(395, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblSearch.Location = new System.Drawing.Point(5, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(68, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // pnlLeft
            // 
            this.pnlLeft.AutoScroll = true;
            this.pnlLeft.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLeft.Controls.Add(this.pnlButtons);
            this.pnlLeft.Controls.Add(this.cboTrangThai);
            this.pnlLeft.Controls.Add(this.lblTrangThai);
            this.pnlLeft.Controls.Add(this.txtMoTa);
            this.pnlLeft.Controls.Add(this.lblMoTa);
            this.pnlLeft.Controls.Add(this.dtpHanSuDung);
            this.pnlLeft.Controls.Add(this.lblHanSuDung);
            this.pnlLeft.Controls.Add(this.lblDonViGiaTri);
            this.pnlLeft.Controls.Add(this.numGiaTriGiam);
            this.pnlLeft.Controls.Add(this.lblGiaTriGiam);
            this.pnlLeft.Controls.Add(this.cboLoaiGiam);
            this.pnlLeft.Controls.Add(this.lblLoaiGiam);
            this.pnlLeft.Controls.Add(this.txtTenVoucher);
            this.pnlLeft.Controls.Add(this.lblTenVoucher);
            this.pnlLeft.Controls.Add(this.btnAutoMa);
            this.pnlLeft.Controls.Add(this.txtMaVoucher);
            this.pnlLeft.Controls.Add(this.lblMaVoucher);
            this.pnlLeft.Controls.Add(this.lblFormTitle);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(15, 15);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(15);
            this.pnlLeft.Size = new System.Drawing.Size(360, 620);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnXoa);
            this.pnlButtons.Controls.Add(this.btnSua);
            this.pnlButtons.Controls.Add(this.btnThem);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(15, 498);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(330, 95);
            this.pnlButtons.TabIndex = 17;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnLamMoi.ForeColor = System.Drawing.Color.Black;
            this.btnLamMoi.Location = new System.Drawing.Point(170, 50);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(155, 34);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnXoa.ForeColor = System.Drawing.Color.Black;
            this.btnXoa.Location = new System.Drawing.Point(5, 50);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(155, 34);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.Location = new System.Drawing.Point(170, 8);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(155, 34);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnThem.ForeColor = System.Drawing.Color.Black;
            this.btnThem.Location = new System.Drawing.Point(5, 8);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(155, 34);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.BackColor = System.Drawing.Color.White;
            this.cboTrangThai.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThai.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Còn hiệu lực",
            "Đã sử dụng / Đã khóa"});
            this.cboTrangThai.Location = new System.Drawing.Point(15, 473);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(330, 25);
            this.cboTrangThai.TabIndex = 16;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblTrangThai.Location = new System.Drawing.Point(15, 448);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblTrangThai.Size = new System.Drawing.Size(330, 25);
            this.lblTrangThai.TabIndex = 15;
            this.lblTrangThai.Text = "Trạng thái voucher:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.BackColor = System.Drawing.Color.White;
            this.txtMoTa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMoTa.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMoTa.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.txtMoTa.Location = new System.Drawing.Point(15, 388);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(330, 60);
            this.txtMoTa.TabIndex = 14;
            // 
            // lblMoTa
            // 
            this.lblMoTa.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblMoTa.Location = new System.Drawing.Point(15, 363);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblMoTa.Size = new System.Drawing.Size(330, 25);
            this.lblMoTa.TabIndex = 13;
            this.lblMoTa.Text = "Mô tả / Lưu ý voucher:";
            // 
            // dtpHanSuDung
            // 
            this.dtpHanSuDung.CustomFormat = "dd/MM/yyyy";
            this.dtpHanSuDung.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpHanSuDung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHanSuDung.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHanSuDung.Location = new System.Drawing.Point(15, 338);
            this.dtpHanSuDung.Name = "dtpHanSuDung";
            this.dtpHanSuDung.Size = new System.Drawing.Size(330, 25);
            this.dtpHanSuDung.TabIndex = 12;
            // 
            // lblHanSuDung
            // 
            this.lblHanSuDung.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHanSuDung.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblHanSuDung.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblHanSuDung.Location = new System.Drawing.Point(15, 313);
            this.lblHanSuDung.Name = "lblHanSuDung";
            this.lblHanSuDung.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblHanSuDung.Size = new System.Drawing.Size(330, 25);
            this.lblHanSuDung.TabIndex = 11;
            this.lblHanSuDung.Text = "Hạn sử dụng:";
            // 
            // lblDonViGiaTri
            // 
            this.lblDonViGiaTri.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDonViGiaTri.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblDonViGiaTri.ForeColor = System.Drawing.Color.FromArgb(217, 119, 6);
            this.lblDonViGiaTri.Location = new System.Drawing.Point(15, 293);
            this.lblDonViGiaTri.Name = "lblDonViGiaTri";
            this.lblDonViGiaTri.Size = new System.Drawing.Size(330, 20);
            this.lblDonViGiaTri.TabIndex = 10;
            this.lblDonViGiaTri.Text = "Đơn vị tính: % giá trị vé";
            // 
            // numGiaTriGiam
            // 
            this.numGiaTriGiam.BackColor = System.Drawing.Color.White;
            this.numGiaTriGiam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numGiaTriGiam.Dock = System.Windows.Forms.DockStyle.Top;
            this.numGiaTriGiam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.numGiaTriGiam.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.numGiaTriGiam.Location = new System.Drawing.Point(15, 268);
            this.numGiaTriGiam.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numGiaTriGiam.Name = "numGiaTriGiam";
            this.numGiaTriGiam.Size = new System.Drawing.Size(330, 25);
            this.numGiaTriGiam.TabIndex = 9;
            this.numGiaTriGiam.ThousandsSeparator = true;
            this.numGiaTriGiam.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblGiaTriGiam
            // 
            this.lblGiaTriGiam.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGiaTriGiam.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriGiam.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblGiaTriGiam.Location = new System.Drawing.Point(15, 243);
            this.lblGiaTriGiam.Name = "lblGiaTriGiam";
            this.lblGiaTriGiam.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblGiaTriGiam.Size = new System.Drawing.Size(330, 25);
            this.lblGiaTriGiam.TabIndex = 8;
            this.lblGiaTriGiam.Text = "Giá trị giảm:";
            // 
            // cboLoaiGiam
            // 
            this.cboLoaiGiam.BackColor = System.Drawing.Color.White;
            this.cboLoaiGiam.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboLoaiGiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiGiam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiGiam.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.cboLoaiGiam.FormattingEnabled = true;
            this.cboLoaiGiam.Items.AddRange(new object[] {
            "Giảm phần trăm (%)",
            "Giảm số tiền (VNĐ)",
            "Giảm số vé (Vé)"});
            this.cboLoaiGiam.Location = new System.Drawing.Point(15, 218);
            this.cboLoaiGiam.Name = "cboLoaiGiam";
            this.cboLoaiGiam.Size = new System.Drawing.Size(330, 25);
            this.cboLoaiGiam.TabIndex = 7;
            this.cboLoaiGiam.SelectedIndexChanged += new System.EventHandler(this.CboLoaiGiam_SelectedIndexChanged);
            // 
            // lblLoaiGiam
            // 
            this.lblLoaiGiam.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLoaiGiam.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLoaiGiam.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblLoaiGiam.Location = new System.Drawing.Point(15, 193);
            this.lblLoaiGiam.Name = "lblLoaiGiam";
            this.lblLoaiGiam.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblLoaiGiam.Size = new System.Drawing.Size(330, 25);
            this.lblLoaiGiam.TabIndex = 6;
            this.lblLoaiGiam.Text = "Hình thức giảm:";
            // 
            // txtTenVoucher
            // 
            this.txtTenVoucher.BackColor = System.Drawing.Color.White;
            this.txtTenVoucher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenVoucher.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTenVoucher.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenVoucher.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.txtTenVoucher.Location = new System.Drawing.Point(15, 168);
            this.txtTenVoucher.Name = "txtTenVoucher";
            this.txtTenVoucher.Size = new System.Drawing.Size(330, 25);
            this.txtTenVoucher.TabIndex = 5;
            // 
            // lblTenVoucher
            // 
            this.lblTenVoucher.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTenVoucher.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenVoucher.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblTenVoucher.Location = new System.Drawing.Point(15, 143);
            this.lblTenVoucher.Name = "lblTenVoucher";
            this.lblTenVoucher.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblTenVoucher.Size = new System.Drawing.Size(330, 25);
            this.lblTenVoucher.TabIndex = 4;
            this.lblTenVoucher.Text = "Tên voucher:";
            // 
            // btnAutoMa
            // 
            this.btnAutoMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoMa.BackColor = System.Drawing.Color.FromArgb(230, 238, 233);
            this.btnAutoMa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoMa.FlatAppearance.BorderSize = 0;
            this.btnAutoMa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoMa.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAutoMa.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.btnAutoMa.Location = new System.Drawing.Point(265, 115);
            this.btnAutoMa.Name = "btnAutoMa";
            this.btnAutoMa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoMa.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnAutoMa.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.btnAutoMa.ForeColor = System.Drawing.Color.Black;
            this.btnAutoMa.Location = new System.Drawing.Point(265, 93);
            this.btnAutoMa.Name = "btnAutoMa";
            this.btnAutoMa.Size = new System.Drawing.Size(80, 25);
            this.btnAutoMa.TabIndex = 3;
            this.btnAutoMa.Text = "Tự tạo";
            this.btnAutoMa.UseVisualStyleBackColor = true;
            this.btnAutoMa.Click += new System.EventHandler(this.BtnAutoMa_Click);
            // 
            // txtMaVoucher
            // 
            this.txtMaVoucher.BackColor = System.Drawing.Color.White;
            this.txtMaVoucher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaVoucher.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMaVoucher.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtMaVoucher.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.txtMaVoucher.Location = new System.Drawing.Point(15, 118);
            this.txtMaVoucher.Name = "txtMaVoucher";
            this.txtMaVoucher.Size = new System.Drawing.Size(330, 25);
            this.txtMaVoucher.TabIndex = 2;
            // 
            // lblMaVoucher
            // 
            this.lblMaVoucher.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMaVoucher.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaVoucher.ForeColor = System.Drawing.Color.FromArgb(20, 50, 35);
            this.lblMaVoucher.Location = new System.Drawing.Point(15, 93);
            this.lblMaVoucher.Name = "lblMaVoucher";
            this.lblMaVoucher.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblMaVoucher.Size = new System.Drawing.Size(330, 25);
            this.lblMaVoucher.TabIndex = 1;
            this.lblMaVoucher.Text = "Mã voucher (duy nhất):";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);
            this.lblFormTitle.Location = new System.Drawing.Point(15, 15);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(330, 78);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "THÔNG TIN VOUCHER KHUYẾN MÃI";
            // 
            // ucCaiVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucCaiVoucher";
            this.Size = new System.Drawing.Size(1000, 650);
            this.pnlMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucher)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numGiaTriGiam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblMaVoucher;
        private System.Windows.Forms.TextBox txtMaVoucher;
        private System.Windows.Forms.Button btnAutoMa;
        private System.Windows.Forms.Label lblTenVoucher;
        private System.Windows.Forms.TextBox txtTenVoucher;
        private System.Windows.Forms.Label lblLoaiGiam;
        private System.Windows.Forms.ComboBox cboLoaiGiam;
        private System.Windows.Forms.Label lblGiaTriGiam;
        private System.Windows.Forms.NumericUpDown numGiaTriGiam;
        private System.Windows.Forms.Label lblDonViGiaTri;
        private System.Windows.Forms.Label lblHanSuDung;
        private System.Windows.Forms.DateTimePicker dtpHanSuDung;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvVoucher;
    }
}
