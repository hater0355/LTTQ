namespace HeThongRapChieuPhim.UserControls
{
    partial class ucHoaDon
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
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.colMaHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThoiGianXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenPhim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSuatChieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGheNgoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongVe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongThanhToan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHinhThucTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThaiTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnPrintAgain = new System.Windows.Forms.Button();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.GroupBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlGrid);
            this.pnlContainer.Controls.Add(this.pnlActions);
            this.pnlContainer.Controls.Add(this.pnlFilter);
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
            this.pnlGrid.BackColor = System.Drawing.SystemColors.Control;
            this.pnlGrid.Controls.Add(this.dgvHoaDon);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(20, 210);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(5);
            this.pnlGrid.Size = new System.Drawing.Size(1040, 450);
            this.pnlGrid.TabIndex = 3;
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AllowUserToDeleteRows = false;
            this.dgvHoaDon.AutoGenerateColumns = false;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.Location = new System.Drawing.Point(5, 5);
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.MultiSelect = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colMaHD,
                this.colThoiGianXuat,
                this.colTenPhim,
                this.colPhong,
                this.colSuatChieu,
                this.colGheNgoi,
                this.colSoLuongVe,
                this.colTongThanhToan,
                this.colHinhThucTT,
                this.colTrangThaiTT});
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.Size = new System.Drawing.Size(1030, 440);
            this.dgvHoaDon.TabIndex = 0;
            //
            // Invoice history columns
            //
            this.colMaHD.DataPropertyName = "Mã HĐ";
            this.colMaHD.HeaderText = "Mã HĐ";
            this.colMaHD.Name = "colMaHD";
            this.colMaHD.ReadOnly = true;
            this.colMaHD.FillWeight = 75F;
            this.colThoiGianXuat.DataPropertyName = "Thời Gian Xuất";
            this.colThoiGianXuat.HeaderText = "Thời Gian Xuất";
            this.colThoiGianXuat.Name = "colThoiGianXuat";
            this.colThoiGianXuat.ReadOnly = true;
            this.colThoiGianXuat.FillWeight = 115F;
            this.colTenPhim.DataPropertyName = "Tên Phim";
            this.colTenPhim.HeaderText = "Tên Phim";
            this.colTenPhim.Name = "colTenPhim";
            this.colTenPhim.ReadOnly = true;
            this.colTenPhim.FillWeight = 135F;
            this.colPhong.DataPropertyName = "Phòng";
            this.colPhong.HeaderText = "Phòng";
            this.colPhong.Name = "colPhong";
            this.colPhong.ReadOnly = true;
            this.colPhong.FillWeight = 75F;
            this.colSuatChieu.DataPropertyName = "Suất Chiếu";
            this.colSuatChieu.HeaderText = "Suất Chiếu";
            this.colSuatChieu.Name = "colSuatChieu";
            this.colSuatChieu.ReadOnly = true;
            this.colSuatChieu.FillWeight = 80F;
            this.colGheNgoi.DataPropertyName = "Ghế Ngồi";
            this.colGheNgoi.HeaderText = "Ghế Ngồi";
            this.colGheNgoi.Name = "colGheNgoi";
            this.colGheNgoi.ReadOnly = true;
            this.colGheNgoi.FillWeight = 95F;
            this.colSoLuongVe.DataPropertyName = "SL Vé";
            this.colSoLuongVe.HeaderText = "SL Vé";
            this.colSoLuongVe.Name = "colSoLuongVe";
            this.colSoLuongVe.ReadOnly = true;
            this.colSoLuongVe.FillWeight = 55F;
            this.colTongThanhToan.DataPropertyName = "Tổng Thanh Toán";
            this.colTongThanhToan.HeaderText = "Tổng Thanh Toán";
            this.colTongThanhToan.Name = "colTongThanhToan";
            this.colTongThanhToan.ReadOnly = true;
            this.colTongThanhToan.FillWeight = 100F;
            this.colHinhThucTT.DataPropertyName = "Hình Thức TT";
            this.colHinhThucTT.HeaderText = "Hình Thức TT";
            this.colHinhThucTT.Name = "colHinhThucTT";
            this.colHinhThucTT.ReadOnly = true;
            this.colHinhThucTT.FillWeight = 90F;
            this.colTrangThaiTT.DataPropertyName = "Trạng Thái Thanh Toán";
            this.colTrangThaiTT.HeaderText = "Trạng Thái Thanh Toán";
            this.colTrangThaiTT.Name = "colTrangThaiTT";
            this.colTrangThaiTT.ReadOnly = true;
            this.colTrangThaiTT.FillWeight = 115F;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnPrintAgain);
            this.pnlActions.Controls.Add(this.lblTotalRevenue);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(20, 155);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlActions.Size = new System.Drawing.Size(1040, 55);
            this.pnlActions.TabIndex = 2;
            // 
            // btnPrintAgain
            // 
            this.btnPrintAgain.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPrintAgain.Location = new System.Drawing.Point(0, 8);
            this.btnPrintAgain.Name = "btnPrintAgain";
            this.btnPrintAgain.Size = new System.Drawing.Size(180, 36);
            this.btnPrintAgain.TabIndex = 0;
            this.btnPrintAgain.Text = "Xem / In Lại Hóa Đơn";
            this.btnPrintAgain.UseVisualStyleBackColor = true;
            this.btnPrintAgain.Click += new System.EventHandler(this.BtnPrintAgain_Click);
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTotalRevenue.Location = new System.Drawing.Point(350, 14);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(236, 20);
            this.lblTotalRevenue.TabIndex = 2;
            this.lblTotalRevenue.Text = "Tổng doanh thu danh sách: 0 đ";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.chkLocNgay);
            this.pnlFilter.Controls.Add(this.lblTu);
            this.pnlFilter.Controls.Add(this.dtpTuNgay);
            this.pnlFilter.Controls.Add(this.lblDen);
            this.pnlFilter.Controls.Add(this.dtpDenNgay);
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Controls.Add(this.btnRefresh);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.pnlFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.pnlFilter.Location = new System.Drawing.Point(20, 65);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlFilter.Size = new System.Drawing.Size(1040, 90);
            this.pnlFilter.TabIndex = 1;
            this.pnlFilter.TabStop = false;
            this.pnlFilter.Text = " Bộ lọc tìm kiếm hóa đơn ";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(20, 32);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(95, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Mã HĐ / Phim:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(130, 28);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(160, 25);
            this.txtSearch.TabIndex = 1;
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkLocNgay.ForeColor = System.Drawing.Color.Black;
            this.chkLocNgay.Location = new System.Drawing.Point(310, 31);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(111, 21);
            this.chkLocNgay.TabIndex = 2;
            this.chkLocNgay.Text = "Lọc theo ngày:";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTu.ForeColor = System.Drawing.Color.Black;
            this.lblTu.Location = new System.Drawing.Point(430, 32);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(26, 17);
            this.lblTu.TabIndex = 3;
            this.lblTu.Text = "Từ:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(460, 28);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(115, 25);
            this.dtpTuNgay.TabIndex = 4;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDen.ForeColor = System.Drawing.Color.Black;
            this.lblDen.Location = new System.Drawing.Point(585, 32);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(34, 17);
            this.lblDen.TabIndex = 5;
            this.lblDen.Text = "Đến:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(625, 28);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(115, 25);
            this.dtpDenNgay.TabIndex = 6;
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnFilter.Location = new System.Drawing.Point(760, 25);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 32);
            this.btnFilter.TabIndex = 7;
            this.btnFilter.Text = "Lọc Dữ Liệu";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnRefresh.Location = new System.Drawing.Point(870, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 32);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Làm Mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1040, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ & LỊCH SỬ HÓA ĐƠN BÁN VÉ";
            // 
            // ucHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucHoaDon";
            this.Size = new System.Drawing.Size(1080, 680);
            this.pnlContainer.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox pnlFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnPrintAgain;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThoiGianXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenPhim;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSuatChieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGheNgoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongVe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongThanhToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHinhThucTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThaiTT;
    }
}
