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
            this.dgvMonthlyRevenue = new System.Windows.Forms.DataGridView();
            this.colMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMonthRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMovieRevenue = new System.Windows.Forms.DataGridView();
            this.colMovieName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMovieRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCards = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpiRevenue = new System.Windows.Forms.Panel();
            this.pnlKpiMonth = new System.Windows.Forms.Panel();
            this.pnlKpiTickets = new System.Windows.Forms.Panel();
            this.pnlKpiMovies = new System.Windows.Forms.Panel();
            this.lblKpiRevenueTitle = new System.Windows.Forms.Label();
            this.lblKpiMonthTitle = new System.Windows.Forms.Label();
            this.lblKpiTicketsTitle = new System.Windows.Forms.Label();
            this.lblKpiMoviesTitle = new System.Windows.Forms.Label();
            this.lblKpiRevenue = new System.Windows.Forms.Label();
            this.lblKpiMonth = new System.Windows.Forms.Label();
            this.lblKpiTickets = new System.Windows.Forms.Label();
            this.lblKpiMovies = new System.Windows.Forms.Label();
            this.lblKpiRevenueIcon = new System.Windows.Forms.Label();
            this.lblKpiMonthIcon = new System.Windows.Forms.Label();
            this.lblKpiTicketsIcon = new System.Windows.Forms.Label();
            this.lblKpiMoviesIcon = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblUserHeader = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopMovies)).BeginInit();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthlyRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovieRevenue)).BeginInit();
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
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.pnlCharts.Controls.Add(this.dgvMonthlyRevenue, 0, 0);
            this.pnlCharts.Controls.Add(this.dgvMovieRevenue, 1, 0);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharts.Location = new System.Drawing.Point(18, 195);
            this.pnlCharts.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.RowCount = 1;
            this.pnlCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCharts.Size = new System.Drawing.Size(1064, 280);
            this.pnlCharts.TabIndex = 2;
            // 
            // dgvMonthlyRevenue
            // 
            this.dgvMonthlyRevenue.AllowUserToAddRows = false;
            this.dgvMonthlyRevenue.AllowUserToDeleteRows = false;
            this.dgvMonthlyRevenue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMonthlyRevenue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colMonth, this.colMonthRevenue });
            this.dgvMonthlyRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMonthlyRevenue.Location = new System.Drawing.Point(3, 3);
            this.dgvMonthlyRevenue.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.dgvMonthlyRevenue.Name = "dgvMonthlyRevenue";
            this.dgvMonthlyRevenue.ReadOnly = true;
            this.dgvMonthlyRevenue.RowHeadersVisible = false;
            this.dgvMonthlyRevenue.Size = new System.Drawing.Size(574, 274);
            this.dgvMonthlyRevenue.TabIndex = 0;
            this.colMonth.HeaderText = "Doanh thu các tháng gần nhất";
            this.colMonth.Name = "colMonth";
            this.colMonth.ReadOnly = true;
            this.colMonthRevenue.HeaderText = "Doanh thu (VNĐ)";
            this.colMonthRevenue.Name = "colMonthRevenue";
            this.colMonthRevenue.ReadOnly = true;
            // 
            // dgvMovieRevenue
            // 
            this.dgvMovieRevenue.AllowUserToAddRows = false;
            this.dgvMovieRevenue.AllowUserToDeleteRows = false;
            this.dgvMovieRevenue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovieRevenue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colMovieName, this.colMovieRevenue });
            this.dgvMovieRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovieRevenue.Location = new System.Drawing.Point(588, 3);
            this.dgvMovieRevenue.Margin = new System.Windows.Forms.Padding(3);
            this.dgvMovieRevenue.Name = "dgvMovieRevenue";
            this.dgvMovieRevenue.ReadOnly = true;
            this.dgvMovieRevenue.RowHeadersVisible = false;
            this.dgvMovieRevenue.Size = new System.Drawing.Size(473, 274);
            this.dgvMovieRevenue.TabIndex = 1;
            this.colMovieName.HeaderText = "Tỷ trọng doanh thu theo phim";
            this.colMovieName.Name = "colMovieName";
            this.colMovieName.ReadOnly = true;
            this.colMovieRevenue.HeaderText = "Doanh thu (VNĐ)";
            this.colMovieRevenue.Name = "colMovieRevenue";
            this.colMovieRevenue.ReadOnly = true;
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
            this.pnlKpiRevenue.BackColor = System.Drawing.Color.White;
            this.pnlKpiRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiRevenue.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiRevenue.Controls.Add(this.lblKpiRevenue);
            this.pnlKpiRevenue.Controls.Add(this.lblKpiRevenueTitle);
            this.pnlKpiRevenue.Controls.Add(this.lblKpiRevenueIcon);
            this.pnlKpiMonth.BackColor = System.Drawing.Color.White;
            this.pnlKpiMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiMonth.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiMonth.Controls.Add(this.lblKpiMonth);
            this.pnlKpiMonth.Controls.Add(this.lblKpiMonthTitle);
            this.pnlKpiMonth.Controls.Add(this.lblKpiMonthIcon);
            this.pnlKpiTickets.BackColor = System.Drawing.Color.White;
            this.pnlKpiTickets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiTickets.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiTickets.Controls.Add(this.lblKpiTickets);
            this.pnlKpiTickets.Controls.Add(this.lblKpiTicketsTitle);
            this.pnlKpiTickets.Controls.Add(this.lblKpiTicketsIcon);
            this.pnlKpiMovies.BackColor = System.Drawing.Color.White;
            this.pnlKpiMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiMovies.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiMovies.Controls.Add(this.lblKpiMovies);
            this.pnlKpiMovies.Controls.Add(this.lblKpiMoviesTitle);
            this.pnlKpiMovies.Controls.Add(this.lblKpiMoviesIcon);
            this.pnlKpiRevenue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiTickets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiMovies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKpiRevenueTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblKpiRevenueTitle.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.TextMuted;
            this.lblKpiRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this.lblKpiRevenueTitle.Location = new System.Drawing.Point(14, 12);
            this.lblKpiRevenueTitle.Size = new System.Drawing.Size(200, 20);
            this.lblKpiRevenueTitle.Text = "TỔNG DOANH THU";
            this.lblKpiMonthTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblKpiMonthTitle.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.TextMuted;
            this.lblKpiMonthTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this.lblKpiMonthTitle.Location = new System.Drawing.Point(14, 12);
            this.lblKpiMonthTitle.Size = new System.Drawing.Size(200, 20);
            this.lblKpiMonthTitle.Text = "DOANH THU THÁNG NÀY";
            this.lblKpiTicketsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblKpiTicketsTitle.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.TextMuted;
            this.lblKpiTicketsTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this.lblKpiTicketsTitle.Location = new System.Drawing.Point(14, 12);
            this.lblKpiTicketsTitle.Size = new System.Drawing.Size(200, 20);
            this.lblKpiTicketsTitle.Text = "TỔNG VÉ ĐÃ BÁN";
            this.lblKpiMoviesTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblKpiMoviesTitle.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.TextMuted;
            this.lblKpiMoviesTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this.lblKpiMoviesTitle.Location = new System.Drawing.Point(14, 12);
            this.lblKpiMoviesTitle.Size = new System.Drawing.Size(200, 20);
            this.lblKpiMoviesTitle.Text = "PHIM ĐANG CHIẾU";
            this.lblKpiRevenue.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.PrimaryDark;
            this.lblKpiRevenue.Font = new System.Drawing.Font("Segoe UI", 15.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiRevenue.Location = new System.Drawing.Point(14, 38);
            this.lblKpiRevenue.Size = new System.Drawing.Size(185, 36);
            this.lblKpiRevenue.Text = "...";
            this.lblKpiMonth.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.PrimaryDark;
            this.lblKpiMonth.Font = new System.Drawing.Font("Segoe UI", 15.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiMonth.Location = new System.Drawing.Point(14, 38);
            this.lblKpiMonth.Size = new System.Drawing.Size(185, 36);
            this.lblKpiMonth.Text = "...";
            this.lblKpiTickets.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.PrimaryDark;
            this.lblKpiTickets.Font = new System.Drawing.Font("Segoe UI", 15.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiTickets.Location = new System.Drawing.Point(14, 38);
            this.lblKpiTickets.Size = new System.Drawing.Size(185, 36);
            this.lblKpiTickets.Text = "...";
            this.lblKpiMovies.ForeColor = HeThongRapChieuPhim.UIHelpers.ThemeHelper.PrimaryDark;
            this.lblKpiMovies.Font = new System.Drawing.Font("Segoe UI", 15.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiMovies.Location = new System.Drawing.Point(14, 38);
            this.lblKpiMovies.Size = new System.Drawing.Size(185, 36);
            this.lblKpiMovies.Text = "...";
            this.lblKpiRevenueIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblKpiRevenueIcon.ForeColor = System.Drawing.Color.FromArgb(88, 145, 112);
            this.lblKpiRevenueIcon.Location = new System.Drawing.Point(202, 39);
            this.lblKpiRevenueIcon.Size = new System.Drawing.Size(38, 38);
            this.lblKpiRevenueIcon.Text = "₫";
            this.lblKpiRevenueIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpiMonthIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblKpiMonthIcon.ForeColor = System.Drawing.Color.FromArgb(88, 145, 112);
            this.lblKpiMonthIcon.Location = new System.Drawing.Point(202, 39);
            this.lblKpiMonthIcon.Size = new System.Drawing.Size(38, 38);
            this.lblKpiMonthIcon.Text = "◷";
            this.lblKpiMonthIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpiTicketsIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblKpiTicketsIcon.ForeColor = System.Drawing.Color.FromArgb(88, 145, 112);
            this.lblKpiTicketsIcon.Location = new System.Drawing.Point(202, 39);
            this.lblKpiTicketsIcon.Size = new System.Drawing.Size(38, 38);
            this.lblKpiTicketsIcon.Text = "▤";
            this.lblKpiTicketsIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKpiMoviesIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblKpiMoviesIcon.ForeColor = System.Drawing.Color.FromArgb(88, 145, 112);
            this.lblKpiMoviesIcon.Location = new System.Drawing.Point(202, 39);
            this.lblKpiMoviesIcon.Size = new System.Drawing.Size(38, 38);
            this.lblKpiMoviesIcon.Text = "▣";
            this.lblKpiMoviesIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlCards.Controls.Add(this.pnlKpiRevenue, 0, 0);
            this.pnlCards.Controls.Add(this.pnlKpiMonth, 1, 0);
            this.pnlCards.Controls.Add(this.pnlKpiTickets, 2, 0);
            this.pnlCards.Controls.Add(this.pnlKpiMovies, 3, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthlyRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovieRevenue)).EndInit();
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
        private System.Windows.Forms.Panel pnlKpiRevenue;
        private System.Windows.Forms.Panel pnlKpiMonth;
        private System.Windows.Forms.Panel pnlKpiTickets;
        private System.Windows.Forms.Panel pnlKpiMovies;
        private System.Windows.Forms.Label lblKpiRevenueTitle;
        private System.Windows.Forms.Label lblKpiMonthTitle;
        private System.Windows.Forms.Label lblKpiTicketsTitle;
        private System.Windows.Forms.Label lblKpiMoviesTitle;
        private System.Windows.Forms.Label lblKpiRevenue;
        private System.Windows.Forms.Label lblKpiMonth;
        private System.Windows.Forms.Label lblKpiTickets;
        private System.Windows.Forms.Label lblKpiMovies;
        private System.Windows.Forms.Label lblKpiRevenueIcon;
        private System.Windows.Forms.Label lblKpiMonthIcon;
        private System.Windows.Forms.Label lblKpiTicketsIcon;
        private System.Windows.Forms.Label lblKpiMoviesIcon;
        private System.Windows.Forms.TableLayoutPanel pnlCharts;
        private System.Windows.Forms.DataGridView dgvMonthlyRevenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMonthRevenue;
        private System.Windows.Forms.DataGridView dgvMovieRevenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMovieName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMovieRevenue;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvTopMovies;
    }
}
