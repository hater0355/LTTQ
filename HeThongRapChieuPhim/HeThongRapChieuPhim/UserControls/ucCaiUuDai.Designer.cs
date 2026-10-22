namespace HeThongRapChieuPhim.UserControls
{
    partial class ucCaiUuDai
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
            this.dgvCombo = new System.Windows.Forms.DataGridView();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlForm = new System.Windows.Forms.GroupBox();
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMaCombo = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTenCombo = new System.Windows.Forms.TextBox();
            this.lblGhe = new System.Windows.Forms.Label();
            this.numSoLuongGhe = new System.Windows.Forms.NumericUpDown();
            this.lblGiam = new System.Windows.Forms.Label();
            this.numPhanTramGiam = new System.Windows.Forms.NumericUpDown();
            this.lblPct = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblQuyDinh = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCombo)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongGhe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhanTramGiam)).BeginInit();
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
            this.pnlContainer.Size = new System.Drawing.Size(1100, 680);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.SystemColors.Control;
            this.pnlGrid.Controls.Add(this.dgvCombo);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(20, 280);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(5);
            this.pnlGrid.Size = new System.Drawing.Size(1060, 380);
            this.pnlGrid.TabIndex = 3;
            // 
            // dgvCombo
            // 
            this.dgvCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCombo.Location = new System.Drawing.Point(5, 5);
            this.dgvCombo.Name = "dgvCombo";
            this.dgvCombo.Size = new System.Drawing.Size(1050, 370);
            this.dgvCombo.TabIndex = 0;
            this.dgvCombo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCombo_CellClick);
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnThem);
            this.pnlActions.Controls.Add(this.btnSua);
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(20, 225);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlActions.Size = new System.Drawing.Size(1060, 55);
            this.pnlActions.TabIndex = 2;
            // 
            // btnThem
            // 
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnThem.ForeColor = System.Drawing.Color.Black;
            this.btnThem.Location = new System.Drawing.Point(0, 8);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 34);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.Location = new System.Drawing.Point(110, 8);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 34);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnXoa.ForeColor = System.Drawing.Color.Black;
            this.btnXoa.Location = new System.Drawing.Point(220, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 34);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.btnLamMoi.ForeColor = System.Drawing.Color.Black;
            this.btnLamMoi.Location = new System.Drawing.Point(330, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 34);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.SystemColors.Control;
            this.pnlForm.Controls.Add(this.lblMa);
            this.pnlForm.Controls.Add(this.txtMaCombo);
            this.pnlForm.Controls.Add(this.lblTen);
            this.pnlForm.Controls.Add(this.txtTenCombo);
            this.pnlForm.Controls.Add(this.lblGhe);
            this.pnlForm.Controls.Add(this.numSoLuongGhe);
            this.pnlForm.Controls.Add(this.lblGiam);
            this.pnlForm.Controls.Add(this.numPhanTramGiam);
            this.pnlForm.Controls.Add(this.lblPct);
            this.pnlForm.Controls.Add(this.lblMoTa);
            this.pnlForm.Controls.Add(this.txtMoTa);
            this.pnlForm.Controls.Add(this.lblQuyDinh);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlForm.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.pnlForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.pnlForm.Location = new System.Drawing.Point(20, 65);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(15);
            this.pnlForm.Size = new System.Drawing.Size(1060, 160);
            this.pnlForm.TabIndex = 1;
            this.pnlForm.TabStop = false;
            this.pnlForm.Text = " Thiết lập gói ưu đãi / combo vé ";
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = true;
            this.lblMa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblMa.Location = new System.Drawing.Point(20, 32);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(76, 17);
            this.lblMa.TabIndex = 0;
            this.lblMa.Text = "Mã combo:";
            // 
            // txtMaCombo
            // 
            this.txtMaCombo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaCombo.Location = new System.Drawing.Point(120, 28);
            this.txtMaCombo.Name = "txtMaCombo";
            this.txtMaCombo.Size = new System.Drawing.Size(140, 25);
            this.txtMaCombo.TabIndex = 1;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblTen.Location = new System.Drawing.Point(280, 32);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(77, 17);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Tên combo:";
            // 
            // txtTenCombo
            // 
            this.txtTenCombo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenCombo.Location = new System.Drawing.Point(370, 28);
            this.txtTenCombo.Name = "txtTenCombo";
            this.txtTenCombo.Size = new System.Drawing.Size(260, 25);
            this.txtTenCombo.TabIndex = 3;
            // 
            // lblGhe
            // 
            this.lblGhe.AutoSize = true;
            this.lblGhe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGhe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblGhe.Location = new System.Drawing.Point(650, 32);
            this.lblGhe.Name = "lblGhe";
            this.lblGhe.Size = new System.Drawing.Size(92, 17);
            this.lblGhe.TabIndex = 4;
            this.lblGhe.Text = "Số lượng ghế:";
            // 
            // numSoLuongGhe
            // 
            this.numSoLuongGhe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSoLuongGhe.Location = new System.Drawing.Point(760, 28);
            this.numSoLuongGhe.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numSoLuongGhe.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuongGhe.Name = "numSoLuongGhe";
            this.numSoLuongGhe.Size = new System.Drawing.Size(70, 25);
            this.numSoLuongGhe.TabIndex = 5;
            this.numSoLuongGhe.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblGiam
            // 
            this.lblGiam.AutoSize = true;
            this.lblGiam.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGiam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblGiam.Location = new System.Drawing.Point(850, 32);
            this.lblGiam.Name = "lblGiam";
            this.lblGiam.Size = new System.Drawing.Size(109, 17);
            this.lblGiam.TabIndex = 6;
            this.lblGiam.Text = "% Giảm vé phim:";
            // 
            // numPhanTramGiam
            // 
            this.numPhanTramGiam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numPhanTramGiam.Location = new System.Drawing.Point(975, 28);
            this.numPhanTramGiam.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPhanTramGiam.Name = "numPhanTramGiam";
            this.numPhanTramGiam.Size = new System.Drawing.Size(70, 25);
            this.numPhanTramGiam.TabIndex = 7;
            this.numPhanTramGiam.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblPct
            // 
            this.lblPct.AutoSize = true;
            this.lblPct.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(115)))), ((int)(((byte)(100)))));
            this.lblPct.Location = new System.Drawing.Point(1050, 32);
            this.lblPct.Name = "lblPct";
            this.lblPct.Size = new System.Drawing.Size(19, 17);
            this.lblPct.TabIndex = 8;
            this.lblPct.Text = "%";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.lblMoTa.Location = new System.Drawing.Point(20, 75);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(91, 17);
            this.lblMoTa.TabIndex = 9;
            this.lblMoTa.Text = "Mô tả / Lưu ý:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMoTa.Location = new System.Drawing.Point(120, 71);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(510, 25);
            this.txtMoTa.TabIndex = 10;
            // 
            // lblQuyDinh
            // 
            this.lblQuyDinh.AutoSize = true;
            this.lblQuyDinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblQuyDinh.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblQuyDinh.Location = new System.Drawing.Point(20, 115);
            this.lblQuyDinh.Name = "lblQuyDinh";
            this.lblQuyDinh.Size = new System.Drawing.Size(750, 15);
            this.lblQuyDinh.TabIndex = 11;
            this.lblQuyDinh.Text = "Quy định: Khách chọn combo này bắt buộc phải chọn đúng số lượng ghế đã quy định. Giảm giá chỉ tính trên tiền vé phim (không giảm đồ ăn).";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1060, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CÀI ĐẶT ƯU ĐÃI COMBO";
            // 
            // ucCaiUuDai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucCaiUuDai";
            this.Size = new System.Drawing.Size(1100, 680);
            this.pnlContainer.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCombo)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongGhe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhanTramGiam)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox pnlForm;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMaCombo;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTenCombo;
        private System.Windows.Forms.Label lblGhe;
        private System.Windows.Forms.NumericUpDown numSoLuongGhe;
        private System.Windows.Forms.Label lblGiam;
        private System.Windows.Forms.NumericUpDown numPhanTramGiam;
        private System.Windows.Forms.Label lblPct;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblQuyDinh;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvCombo;
    }
}
