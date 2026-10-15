namespace HeThongRapChieuPhim.UserControls
{
    partial class ucPhim
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvPhim = new System.Windows.Forms.DataGridView();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.pnlForm = new System.Windows.Forms.GroupBox();
            this.lblMaPhim = new System.Windows.Forms.Label();
            this.txtMaPhim = new System.Windows.Forms.TextBox();
            this.lblTenPhim = new System.Windows.Forms.Label();
            this.txtTenPhim = new System.Windows.Forms.TextBox();
            this.lblTheLoai = new System.Windows.Forms.Label();
            this.cboTheLoai = new System.Windows.Forms.ComboBox();
            this.lblNgonNgu = new System.Windows.Forms.Label();
            this.cboNgonNgu = new System.Windows.Forms.ComboBox();
            this.lblDoTuoi = new System.Windows.Forms.Label();
            this.cboDoTuoi = new System.Windows.Forms.ComboBox();
            this.lblThoiLuong = new System.Windows.Forms.Label();
            this.numThoiLuong = new System.Windows.Forms.NumericUpDown();
            this.lblPhut = new System.Windows.Forms.Label();
            this.lblGiaPhim = new System.Windows.Forms.Label();
            this.numGiaPhim = new System.Windows.Forms.NumericUpDown();
            this.lblVnd = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.lblPosterPath = new System.Windows.Forms.Label();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThoiLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaPhim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlGrid);
            this.pnlContainer.Controls.Add(this.pnlActions);
            this.pnlContainer.Controls.Add(this.pnlForm);
            this.pnlContainer.Controls.Add(this.lblTitle);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContainer.Size = new System.Drawing.Size(1080, 680);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvPhim);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(20, 360);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(5);
            this.pnlGrid.Size = new System.Drawing.Size(1040, 300);
            this.pnlGrid.TabIndex = 3;
            // 
            // dgvPhim
            // 
            this.dgvPhim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhim.Location = new System.Drawing.Point(5, 5);
            this.dgvPhim.Name = "dgvPhim";
            this.dgvPhim.Size = new System.Drawing.Size(1030, 290);
            this.dgvPhim.TabIndex = 0;
            this.dgvPhim.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPhim_CellClick);
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnThem);
            this.pnlActions.Controls.Add(this.btnSua);
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Controls.Add(this.lblSearch);
            this.pnlActions.Controls.Add(this.txtSearch);
            this.pnlActions.Controls.Add(this.btnTimKiem);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(20, 305);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlActions.Size = new System.Drawing.Size(1040, 55);
            this.pnlActions.TabIndex = 2;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(0, 8);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(140, 38);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "➕ Thêm Phim";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(150, 8);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 38);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "✏️ Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(260, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 38);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.btnLamMoi.Location = new System.Drawing.Point(370, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 38);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "🔄 Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblSearch.Location = new System.Drawing.Point(510, 16);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(46, 17);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "🔍 Tìm:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(560, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 25);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.Black;
            this.btnTimKiem.Location = new System.Drawing.Point(770, 10);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(70, 32);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.BtnTimKiem_Click);
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.lblMaPhim);
            this.pnlForm.Controls.Add(this.txtMaPhim);
            this.pnlForm.Controls.Add(this.lblTenPhim);
            this.pnlForm.Controls.Add(this.txtTenPhim);
            this.pnlForm.Controls.Add(this.lblTheLoai);
            this.pnlForm.Controls.Add(this.cboTheLoai);
            this.pnlForm.Controls.Add(this.lblNgonNgu);
            this.pnlForm.Controls.Add(this.cboNgonNgu);
            this.pnlForm.Controls.Add(this.lblDoTuoi);
            this.pnlForm.Controls.Add(this.cboDoTuoi);
            this.pnlForm.Controls.Add(this.lblThoiLuong);
            this.pnlForm.Controls.Add(this.numThoiLuong);
            this.pnlForm.Controls.Add(this.lblPhut);
            this.pnlForm.Controls.Add(this.lblGiaPhim);
            this.pnlForm.Controls.Add(this.numGiaPhim);
            this.pnlForm.Controls.Add(this.lblVnd);
            this.pnlForm.Controls.Add(this.lblMoTa);
            this.pnlForm.Controls.Add(this.txtMoTa);
            this.pnlForm.Controls.Add(this.picPoster);
            this.pnlForm.Controls.Add(this.lblPosterPath);
            this.pnlForm.Controls.Add(this.btnChonAnh);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlForm.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.pnlForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.pnlForm.Location = new System.Drawing.Point(20, 65);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(15);
            this.pnlForm.Size = new System.Drawing.Size(1040, 240);
            this.pnlForm.TabIndex = 1;
            this.pnlForm.TabStop = false;
            this.pnlForm.Text = " Thông tin phim & Poster & Nội dung tóm tắt ";
            // 
            // lblMaPhim
            // 
            this.lblMaPhim.AutoSize = true;
            this.lblMaPhim.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMaPhim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblMaPhim.Location = new System.Drawing.Point(20, 30);
            this.lblMaPhim.Name = "lblMaPhim";
            this.lblMaPhim.Size = new System.Drawing.Size(61, 17);
            this.lblMaPhim.TabIndex = 0;
            this.lblMaPhim.Text = "Mã phim:";
            // 
            // txtMaPhim
            // 
            this.txtMaPhim.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaPhim.Location = new System.Drawing.Point(100, 26);
            this.txtMaPhim.Name = "txtMaPhim";
            this.txtMaPhim.Size = new System.Drawing.Size(150, 25);
            this.txtMaPhim.TabIndex = 1;
            // 
            // lblTenPhim
            // 
            this.lblTenPhim.AutoSize = true;
            this.lblTenPhim.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTenPhim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblTenPhim.Location = new System.Drawing.Point(270, 30);
            this.lblTenPhim.Name = "lblTenPhim";
            this.lblTenPhim.Size = new System.Drawing.Size(64, 17);
            this.lblTenPhim.TabIndex = 2;
            this.lblTenPhim.Text = "Tên phim:";
            // 
            // txtTenPhim
            // 
            this.txtTenPhim.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenPhim.Location = new System.Drawing.Point(350, 26);
            this.txtTenPhim.Name = "txtTenPhim";
            this.txtTenPhim.Size = new System.Drawing.Size(390, 25);
            this.txtTenPhim.TabIndex = 3;
            // 
            // lblTheLoai
            // 
            this.lblTheLoai.AutoSize = true;
            this.lblTheLoai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTheLoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblTheLoai.Location = new System.Drawing.Point(20, 70);
            this.lblTheLoai.Name = "lblTheLoai";
            this.lblTheLoai.Size = new System.Drawing.Size(57, 17);
            this.lblTheLoai.TabIndex = 4;
            this.lblTheLoai.Text = "Thể loại:";
            // 
            // cboTheLoai
            // 
            this.cboTheLoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTheLoai.FormattingEnabled = true;
            this.cboTheLoai.Items.AddRange(new object[] {
            "Hành động",
            "Kinh dị",
            "Tình cảm",
            "Hoạt hình",
            "Khoa học viễn tưởng",
            "Hài hước",
            "Lịch sử / Chiến tranh",
            "Phiêu lưu"});
            this.cboTheLoai.Location = new System.Drawing.Point(100, 66);
            this.cboTheLoai.Name = "cboTheLoai";
            this.cboTheLoai.Size = new System.Drawing.Size(150, 25);
            this.cboTheLoai.TabIndex = 5;
            // 
            // lblNgonNgu
            // 
            this.lblNgonNgu.AutoSize = true;
            this.lblNgonNgu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgonNgu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblNgonNgu.Location = new System.Drawing.Point(270, 70);
            this.lblNgonNgu.Name = "lblNgonNgu";
            this.lblNgonNgu.Size = new System.Drawing.Size(70, 17);
            this.lblNgonNgu.TabIndex = 6;
            this.lblNgonNgu.Text = "Ngôn ngữ:";
            // 
            // cboNgonNgu
            // 
            this.cboNgonNgu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNgonNgu.FormattingEnabled = true;
            this.cboNgonNgu.Items.AddRange(new object[] {
            "Tiếng Việt",
            "Tiếng Anh - Phụ đề",
            "Lồng tiếng Việt",
            "Tiếng Hàn - Phụ đề",
            "Tiếng Nhật - Phụ đề"});
            this.cboNgonNgu.Location = new System.Drawing.Point(350, 66);
            this.cboNgonNgu.Name = "cboNgonNgu";
            this.cboNgonNgu.Size = new System.Drawing.Size(160, 25);
            this.cboNgonNgu.TabIndex = 7;
            // 
            // lblDoTuoi
            // 
            this.lblDoTuoi.AutoSize = true;
            this.lblDoTuoi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDoTuoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblDoTuoi.Location = new System.Drawing.Point(530, 70);
            this.lblDoTuoi.Name = "lblDoTuoi";
            this.lblDoTuoi.Size = new System.Drawing.Size(54, 17);
            this.lblDoTuoi.TabIndex = 8;
            this.lblDoTuoi.Text = "Độ tuổi:";
            // 
            // cboDoTuoi
            // 
            this.cboDoTuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoTuoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDoTuoi.FormattingEnabled = true;
            this.cboDoTuoi.Items.AddRange(new object[] {
            "P (Mọi lứa tuổi)",
            "K (Dưới 13t cần PH)",
            "T13 (13+)",
            "T16 (16+)",
            "T18 (18+)"});
            this.cboDoTuoi.Location = new System.Drawing.Point(595, 66);
            this.cboDoTuoi.Name = "cboDoTuoi";
            this.cboDoTuoi.Size = new System.Drawing.Size(145, 25);
            this.cboDoTuoi.TabIndex = 9;
            // 
            // lblThoiLuong
            // 
            this.lblThoiLuong.AutoSize = true;
            this.lblThoiLuong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThoiLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblThoiLuong.Location = new System.Drawing.Point(20, 110);
            this.lblThoiLuong.Name = "lblThoiLuong";
            this.lblThoiLuong.Size = new System.Drawing.Size(74, 17);
            this.lblThoiLuong.TabIndex = 10;
            this.lblThoiLuong.Text = "Thời lượng:";
            // 
            // numThoiLuong
            // 
            this.numThoiLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numThoiLuong.Location = new System.Drawing.Point(100, 106);
            this.numThoiLuong.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numThoiLuong.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numThoiLuong.Name = "numThoiLuong";
            this.numThoiLuong.Size = new System.Drawing.Size(100, 25);
            this.numThoiLuong.TabIndex = 11;
            this.numThoiLuong.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // lblPhut
            // 
            this.lblPhut.AutoSize = true;
            this.lblPhut.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPhut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(115)))), ((int)(((byte)(100)))));
            this.lblPhut.Location = new System.Drawing.Point(205, 110);
            this.lblPhut.Name = "lblPhut";
            this.lblPhut.Size = new System.Drawing.Size(35, 17);
            this.lblPhut.TabIndex = 12;
            this.lblPhut.Text = "phút";
            // 
            // lblGiaPhim
            // 
            this.lblGiaPhim.AutoSize = true;
            this.lblGiaPhim.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGiaPhim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblGiaPhim.Location = new System.Drawing.Point(270, 110);
            this.lblGiaPhim.Name = "lblGiaPhim";
            this.lblGiaPhim.Size = new System.Drawing.Size(73, 17);
            this.lblGiaPhim.TabIndex = 13;
            this.lblGiaPhim.Text = "Giá vé gốc:";
            // 
            // numGiaPhim
            // 
            this.numGiaPhim.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numGiaPhim.Increment = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numGiaPhim.Location = new System.Drawing.Point(350, 106);
            this.numGiaPhim.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numGiaPhim.Name = "numGiaPhim";
            this.numGiaPhim.Size = new System.Drawing.Size(160, 25);
            this.numGiaPhim.TabIndex = 14;
            this.numGiaPhim.Value = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            // 
            // lblVnd
            // 
            this.lblVnd.AutoSize = true;
            this.lblVnd.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(115)))), ((int)(((byte)(100)))));
            this.lblVnd.Location = new System.Drawing.Point(515, 110);
            this.lblVnd.Name = "lblVnd";
            this.lblVnd.Size = new System.Drawing.Size(35, 17);
            this.lblVnd.TabIndex = 15;
            this.lblVnd.Text = "VNĐ";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblMoTa.Location = new System.Drawing.Point(20, 150);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(74, 17);
            this.lblMoTa.TabIndex = 16;
            this.lblMoTa.Text = "Mô tả phim:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMoTa.Location = new System.Drawing.Point(100, 146);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size = new System.Drawing.Size(640, 75);
            this.txtMoTa.TabIndex = 17;
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.picPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPoster.Location = new System.Drawing.Point(780, 25);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(130, 160);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPoster.TabIndex = 18;
            this.picPoster.TabStop = false;
            // 
            // lblPosterPath
            // 
            this.lblPosterPath.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPosterPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(115)))), ((int)(((byte)(100)))));
            this.lblPosterPath.Location = new System.Drawing.Point(780, 195);
            this.lblPosterPath.Name = "lblPosterPath";
            this.lblPosterPath.Size = new System.Drawing.Size(240, 25);
            this.lblPosterPath.TabIndex = 19;
            this.lblPosterPath.Text = "Chưa chọn ảnh poster";
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnChonAnh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonAnh.FlatAppearance.BorderSize = 0;
            this.btnChonAnh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonAnh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChonAnh.ForeColor = System.Drawing.Color.White;
            this.btnChonAnh.Location = new System.Drawing.Point(920, 25);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(105, 34);
            this.btnChonAnh.TabIndex = 20;
            this.btnChonAnh.Text = "📁 Chọn Poster";
            this.btnChonAnh.UseVisualStyleBackColor = false;
            this.btnChonAnh.Click += new System.EventHandler(this.BtnChonAnh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1040, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🎬 QUẢN LÝ DANH MỤC PHIM CHIẾU RẠP";
            // 
            // ucPhim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 245, 241);
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucPhim";
            this.Size = new System.Drawing.Size(1080, 680);
            this.pnlContainer.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThoiLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaPhim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox pnlForm;
        private System.Windows.Forms.Label lblMaPhim;
        private System.Windows.Forms.TextBox txtMaPhim;
        private System.Windows.Forms.Label lblTenPhim;
        private System.Windows.Forms.TextBox txtTenPhim;
        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.Label lblNgonNgu;
        private System.Windows.Forms.ComboBox cboNgonNgu;
        private System.Windows.Forms.Label lblDoTuoi;
        private System.Windows.Forms.ComboBox cboDoTuoi;
        private System.Windows.Forms.Label lblThoiLuong;
        private System.Windows.Forms.NumericUpDown numThoiLuong;
        private System.Windows.Forms.Label lblPhut;
        private System.Windows.Forms.Label lblGiaPhim;
        private System.Windows.Forms.NumericUpDown numGiaPhim;
        private System.Windows.Forms.Label lblVnd;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.Label lblPosterPath;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvPhim;
    }
}
