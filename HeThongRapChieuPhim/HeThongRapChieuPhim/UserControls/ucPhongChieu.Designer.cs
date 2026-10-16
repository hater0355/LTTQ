namespace HeThongRapChieuPhim.UserControls
{
    partial class ucPhongChieu
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
            this.pnlSplit = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.dgvPhongChieu = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.GroupBox();
            this.pnlSeatPreview = new System.Windows.Forms.DataGridView();
            this.lblSeatPreviewScreen = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlForm = new System.Windows.Forms.GroupBox();
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTenPhong = new System.Windows.Forms.TextBox();
            this.lblHang = new System.Windows.Forms.Label();
            this.numSoHangGhe = new System.Windows.Forms.NumericUpDown();
            this.lblCot = new System.Windows.Forms.Label();
            this.numSoGheMoiHang = new System.Windows.Forms.NumericUpDown();
            this.lblTongGhe = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongChieu)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoHangGhe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoGheMoiHang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlSplit);
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
            // pnlSplit
            // 
            this.pnlSplit.ColumnCount = 2;
            this.pnlSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.pnlSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.pnlSplit.Controls.Add(this.pnlLeft, 0, 0);
            this.pnlSplit.Controls.Add(this.pnlRight, 1, 0);
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplit.Location = new System.Drawing.Point(20, 245);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.RowCount = 1;
            this.pnlSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlSplit.Size = new System.Drawing.Size(1040, 415);
            this.pnlSplit.TabIndex = 3;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.dgvPhongChieu);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(3, 3);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(5);
            this.pnlLeft.Size = new System.Drawing.Size(493, 409);
            this.pnlLeft.TabIndex = 0;
            // 
            // dgvPhongChieu
            // 
            this.dgvPhongChieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhongChieu.Location = new System.Drawing.Point(5, 5);
            this.dgvPhongChieu.Name = "dgvPhongChieu";
            this.dgvPhongChieu.Size = new System.Drawing.Size(483, 399);
            this.dgvPhongChieu.TabIndex = 0;
            this.dgvPhongChieu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPhongChieu_CellClick);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.pnlSeatPreview);
            this.pnlRight.Controls.Add(this.lblSeatPreviewScreen);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.pnlRight.Location = new System.Drawing.Point(502, 3);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(535, 409);
            this.pnlRight.TabIndex = 1;
            this.pnlRight.TabStop = false;
            this.pnlRight.Text = " 🪑 Mô phỏng sơ đồ ghế của phòng ";
            // 
            // pnlSeatPreview
            // 
            this.pnlSeatPreview.AllowUserToAddRows = false;
            this.pnlSeatPreview.AllowUserToDeleteRows = false;
            this.pnlSeatPreview.AllowUserToResizeColumns = false;
            this.pnlSeatPreview.AllowUserToResizeRows = false;
            this.pnlSeatPreview.ColumnHeadersHeight = 24;
            this.pnlSeatPreview.MultiSelect = false;
            this.pnlSeatPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.pnlSeatPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSeatPreview.Location = new System.Drawing.Point(10, 50);
            this.pnlSeatPreview.Name = "pnlSeatPreview";
            this.pnlSeatPreview.ReadOnly = true;
            this.pnlSeatPreview.RowHeadersWidth = 42;
            this.pnlSeatPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.pnlSeatPreview.Size = new System.Drawing.Size(515, 349);
            this.pnlSeatPreview.TabIndex = 0;
            this.lblSeatPreviewScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.lblSeatPreviewScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeatPreviewScreen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSeatPreviewScreen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(130)))), ((int)(((byte)(30)))));
            this.lblSeatPreviewScreen.Location = new System.Drawing.Point(10, 28);
            this.lblSeatPreviewScreen.Name = "lblSeatPreviewScreen";
            this.lblSeatPreviewScreen.Size = new System.Drawing.Size(515, 22);
            this.lblSeatPreviewScreen.TabIndex = 1;
            this.lblSeatPreviewScreen.Text = "MÀN HÌNH CHIẾU PHIM";
            this.lblSeatPreviewScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnThem);
            this.pnlActions.Controls.Add(this.btnSua);
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(20, 190);
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
            this.btnThem.Text = "➕ Thêm Phòng";
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
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.lblMa);
            this.pnlForm.Controls.Add(this.txtMaPhong);
            this.pnlForm.Controls.Add(this.lblTen);
            this.pnlForm.Controls.Add(this.txtTenPhong);
            this.pnlForm.Controls.Add(this.lblHang);
            this.pnlForm.Controls.Add(this.numSoHangGhe);
            this.pnlForm.Controls.Add(this.lblCot);
            this.pnlForm.Controls.Add(this.numSoGheMoiHang);
            this.pnlForm.Controls.Add(this.lblTongGhe);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlForm.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.pnlForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.pnlForm.Location = new System.Drawing.Point(20, 65);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(15);
            this.pnlForm.Size = new System.Drawing.Size(1040, 125);
            this.pnlForm.TabIndex = 1;
            this.pnlForm.TabStop = false;
            this.pnlForm.Text = " Thông tin phòng chiếu ";
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = true;
            this.lblMa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblMa.Location = new System.Drawing.Point(20, 32);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(73, 17);
            this.lblMa.TabIndex = 0;
            this.lblMa.Text = "Mã phòng:";
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaPhong.Location = new System.Drawing.Point(105, 28);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(130, 25);
            this.txtMaPhong.TabIndex = 1;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblTen.Location = new System.Drawing.Point(255, 32);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(74, 17);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Tên phòng:";
            // 
            // txtTenPhong
            // 
            this.txtTenPhong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenPhong.Location = new System.Drawing.Point(340, 28);
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.Size = new System.Drawing.Size(230, 25);
            this.txtTenPhong.TabIndex = 3;
            // 
            // lblHang
            // 
            this.lblHang.AutoSize = true;
            this.lblHang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblHang.Location = new System.Drawing.Point(590, 32);
            this.lblHang.Name = "lblHang";
            this.lblHang.Size = new System.Drawing.Size(118, 17);
            this.lblHang.TabIndex = 4;
            this.lblHang.Text = "Số hàng ghế (A-Z):";
            // 
            // numSoHangGhe
            // 
            this.numSoHangGhe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSoHangGhe.Location = new System.Drawing.Point(725, 28);
            this.numSoHangGhe.Maximum = new decimal(new int[] {
            26,
            0,
            0,
            0});
            this.numSoHangGhe.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoHangGhe.Name = "numSoHangGhe";
            this.numSoHangGhe.Size = new System.Drawing.Size(70, 25);
            this.numSoHangGhe.TabIndex = 5;
            this.numSoHangGhe.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numSoHangGhe.ValueChanged += new System.EventHandler(this.NumSoHangGhe_ValueChanged);
            // 
            // lblCot
            // 
            this.lblCot.AutoSize = true;
            this.lblCot.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblCot.Location = new System.Drawing.Point(815, 32);
            this.lblCot.Name = "lblCot";
            this.lblCot.Size = new System.Drawing.Size(122, 17);
            this.lblCot.TabIndex = 6;
            this.lblCot.Text = "Số ghế/hàng (1-N):";
            // 
            // numSoGheMoiHang
            // 
            this.numSoGheMoiHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSoGheMoiHang.Location = new System.Drawing.Point(945, 28);
            this.numSoGheMoiHang.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numSoGheMoiHang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoGheMoiHang.Name = "numSoGheMoiHang";
            this.numSoGheMoiHang.Size = new System.Drawing.Size(70, 25);
            this.numSoGheMoiHang.TabIndex = 7;
            this.numSoGheMoiHang.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSoGheMoiHang.ValueChanged += new System.EventHandler(this.NumSoGheMoiHang_ValueChanged);
            // 
            // lblTongGhe
            // 
            this.lblTongGhe.AutoSize = true;
            this.lblTongGhe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongGhe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTongGhe.Location = new System.Drawing.Point(20, 75);
            this.lblTongGhe.Name = "lblTongGhe";
            this.lblTongGhe.Size = new System.Drawing.Size(332, 17);
            this.lblTongGhe.TabIndex = 8;
            this.lblTongGhe.Text = "⚡ Tổng số ghế: 60 ghế (Hàng A - F, Ghế 1 - 10)";
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
            this.lblTitle.Text = "🏛️ QUẢN LÝ PHÒNG CHIẾU & CẤU HÌNH SƠ ĐỒ GHẾ";
            // 
            // ucPhongChieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 245, 241);
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucPhongChieu";
            this.Size = new System.Drawing.Size(1080, 680);
            this.pnlContainer.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongChieu)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoHangGhe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoGheMoiHang)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox pnlForm;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTenPhong;
        private System.Windows.Forms.Label lblHang;
        private System.Windows.Forms.NumericUpDown numSoHangGhe;
        private System.Windows.Forms.Label lblCot;
        private System.Windows.Forms.NumericUpDown numSoGheMoiHang;
        private System.Windows.Forms.Label lblTongGhe;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.TableLayoutPanel pnlSplit;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.DataGridView dgvPhongChieu;
        private System.Windows.Forms.GroupBox pnlRight;
        private System.Windows.Forms.DataGridView pnlSeatPreview;
        private System.Windows.Forms.Label lblSeatPreviewScreen;
    }
}
