namespace HeThongRapChieuPhim.UserControls
{
    partial class ucDashboard
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.dgvTopMovies = new System.Windows.Forms.DataGridView();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.pnlCharts = new System.Windows.Forms.TableLayoutPanel();
            this.chartMonthly = new HeThongRapChieuPhim.UIHelpers.ChartControl();
            this.chartMovie = new HeThongRapChieuPhim.UIHelpers.ChartControl();
            this.pnlCards = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblUserHeader = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopMovies)).BeginInit();
            this.pnlCharts.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(240, 245, 241);
            this.pnlMain.Controls.Add(this.pnlBottom);
            this.pnlMain.Controls.Add(this.pnlCharts);
            this.pnlMain.Controls.Add(this.pnlCards);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.pnlMain.Size = new System.Drawing.Size(1100, 750);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.dgvTopMovies);
            this.pnlBottom.Controls.Add(this.lblTableTitle);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBottom.Location = new System.Drawing.Point(18, 475);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlBottom.Size = new System.Drawing.Size(1064, 235);
            this.pnlBottom.TabIndex = 3;
            // 
            // dgvTopMovies
            // 
            this.dgvTopMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopMovies.Location = new System.Drawing.Point(16, 44);
            this.dgvTopMovies.Name = "dgvTopMovies";
            this.dgvTopMovies.Size = new System.Drawing.Size(1032, 179);
            this.dgvTopMovies.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(20, 83, 45);
            this.lblTableTitle.Location = new System.Drawing.Point(34, 12);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(1014, 32);
            this.lblTableTitle.TabIndex = 0;
            this.lblTableTitle.Text = "⌛ BẢNG XẾP HẠNG DOANH THU CÁC PHIM";
            // 
            // pnlCharts
            // 
            this.pnlCharts.ColumnCount = 2;
            this.pnlCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.pnlCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.pnlCharts.Controls.Add(this.chartMonthly, 0, 0);
            this.pnlCharts.Controls.Add(this.chartMovie, 1, 0);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharts.Location = new System.Drawing.Point(18, 195);
            this.pnlCharts.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.RowCount = 1;
            this.pnlCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCharts.Size = new System.Drawing.Size(1064, 280);
            this.pnlCharts.TabIndex = 2;
            // 
            // chartMonthly
            // 
            this.chartMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMonthly.Location = new System.Drawing.Point(3, 3);
            this.chartMonthly.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.chartMonthly.Name = "chartMonthly";
            this.chartMonthly.Size = new System.Drawing.Size(574, 274);
            this.chartMonthly.TabIndex = 0;
            this.chartMonthly.Title = "Doanh thu các tháng gần nhất (VNĐ)";
            this.chartMonthly.Type = HeThongRapChieuPhim.UIHelpers.ChartType.Bar;
            // 
            // chartMovie
            // 
            this.chartMovie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMovie.Location = new System.Drawing.Point(588, 3);
            this.chartMovie.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.chartMovie.Name = "chartMovie";
            this.chartMovie.Size = new System.Drawing.Size(473, 274);
            this.chartMovie.TabIndex = 1;
            this.chartMovie.Title = "Tỷ trọng doanh thu theo phim";
            this.chartMovie.Type = HeThongRapChieuPhim.UIHelpers.ChartType.Doughnut;
            // 
            // pnlCards
            // 
            this.pnlCards.ColumnCount = 4;
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(18, 70);
            this.pnlCards.Margin = new System.Windows.Forms.Padding(0, 8, 0, 10);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.RowCount = 1;
            this.pnlCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCards.Size = new System.Drawing.Size(1064, 115);
            this.pnlCards.TabIndex = 1;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblUserHeader);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(18, 14);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1064, 52);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblUserHeader
            // 
            this.lblUserHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserHeader.AutoSize = true;
            this.lblUserHeader.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUserHeader.ForeColor = System.Drawing.Color.FromArgb(45, 80, 60);
            this.lblUserHeader.Location = new System.Drawing.Point(920, 14);
            this.lblUserHeader.Name = "lblUserHeader";
            this.lblUserHeader.Size = new System.Drawing.Size(135, 17);
            this.lblUserHeader.TabIndex = 2;
            this.lblUserHeader.Text = "Quản trị viên NHH ∨";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(92, 135, 112);
            this.lblTitle.Location = new System.Drawing.Point(0, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TỔNG QUAN DOANH THU - HOẠT ĐỘNG RẠP";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Visible = false;
            // 
            // ucDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 245, 241);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucDashboard";
            this.Size = new System.Drawing.Size(1100, 750);
            this.pnlMain.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopMovies)).EndInit();
            this.pnlCharts.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUserHeader;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel pnlCards;
        private System.Windows.Forms.TableLayoutPanel pnlCharts;
        private HeThongRapChieuPhim.UIHelpers.ChartControl chartMonthly;
        private HeThongRapChieuPhim.UIHelpers.ChartControl chartMovie;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvTopMovies;
    }
}
