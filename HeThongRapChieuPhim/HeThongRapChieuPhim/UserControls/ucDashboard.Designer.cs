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
            pnlMain = new Panel();
            pnlBottom = new Panel();
            dgvTopMovies = new DataGridView();
            lblTableTitle = new Label();
            pnlCharts = new TableLayoutPanel();
            dgvMonthlyRevenue = new DataGridView();
            colMonth = new DataGridViewTextBoxColumn();
            colMonthRevenue = new DataGridViewTextBoxColumn();
            dgvMovieRevenue = new DataGridView();
            colMovieName = new DataGridViewTextBoxColumn();
            colMovieRevenue = new DataGridViewTextBoxColumn();
            pnlCards = new TableLayoutPanel();
            pnlKpiRevenue = new Panel();
            lblKpiRevenue = new Label();
            lblKpiRevenueTitle = new Label();
            lblKpiRevenueIcon = new Label();
            pnlKpiMonth = new Panel();
            lblKpiMonth = new Label();
            lblKpiMonthTitle = new Label();
            lblKpiMonthIcon = new Label();
            pnlKpiTickets = new Panel();
            lblKpiTickets = new Label();
            lblKpiTicketsTitle = new Label();
            lblKpiTicketsIcon = new Label();
            pnlKpiMovies = new Panel();
            lblKpiMovies = new Label();
            lblKpiMoviesTitle = new Label();
            lblKpiMoviesIcon = new Label();
            pnlHeader = new Panel();
            lblUserHeader = new Label();
            lblTitle = new Label();
            btnRefresh = new Button();
            pnlMain.SuspendLayout();
            pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopMovies).BeginInit();
            pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonthlyRevenue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMovieRevenue).BeginInit();
            pnlCards.SuspendLayout();
            pnlKpiRevenue.SuspendLayout();
            pnlKpiMonth.SuspendLayout();
            pnlKpiTickets.SuspendLayout();
            pnlKpiMovies.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.BackColor = SystemColors.Control;
            pnlMain.Controls.Add(pnlBottom);
            pnlMain.Controls.Add(pnlCharts);
            pnlMain.Controls.Add(pnlCards);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(18, 14, 18, 14);
            pnlMain.Size = new Size(1100, 750);
            pnlMain.TabIndex = 0;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = SystemColors.Control;
            pnlBottom.BorderStyle = BorderStyle.FixedSingle;
            pnlBottom.Controls.Add(dgvTopMovies);
            pnlBottom.Controls.Add(lblTableTitle);
            pnlBottom.Dock = DockStyle.Top;
            pnlBottom.Location = new Point(18, 461);
            pnlBottom.Margin = new Padding(0, 10, 0, 0);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(16, 12, 16, 12);
            pnlBottom.Size = new Size(1064, 235);
            pnlBottom.TabIndex = 3;
            // 
            // dgvTopMovies
            // 
            dgvTopMovies.ColumnHeadersHeight = 29;
            dgvTopMovies.Dock = DockStyle.Fill;
            dgvTopMovies.Location = new Point(16, 44);
            dgvTopMovies.Name = "dgvTopMovies";
            dgvTopMovies.RowHeadersWidth = 51;
            dgvTopMovies.Size = new Size(1030, 177);
            dgvTopMovies.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            lblTableTitle.Dock = DockStyle.Top;
            lblTableTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblTableTitle.Location = new Point(16, 12);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(1030, 32);
            lblTableTitle.TabIndex = 0;
            lblTableTitle.Text = "BẢNG XẾP HẠNG PHIM ĂN KHÁCH THEO SỐ VÉ";
            // 
            // pnlCharts
            // 
            pnlCharts.ColumnCount = 2;
            pnlCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            pnlCharts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            pnlCharts.Controls.Add(dgvMonthlyRevenue, 0, 0);
            pnlCharts.Controls.Add(dgvMovieRevenue, 1, 0);
            pnlCharts.Dock = DockStyle.Top;
            pnlCharts.Location = new Point(18, 181);
            pnlCharts.Margin = new Padding(0, 10, 0, 10);
            pnlCharts.Name = "pnlCharts";
            pnlCharts.RowCount = 1;
            pnlCharts.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlCharts.Size = new Size(1064, 280);
            pnlCharts.TabIndex = 2;
            // 
            // dgvMonthlyRevenue
            // 
            dgvMonthlyRevenue.AllowUserToAddRows = false;
            dgvMonthlyRevenue.AllowUserToDeleteRows = false;
            dgvMonthlyRevenue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMonthlyRevenue.ColumnHeadersHeight = 29;
            dgvMonthlyRevenue.Columns.AddRange(new DataGridViewColumn[] { colMonth, colMonthRevenue });
            dgvMonthlyRevenue.Dock = DockStyle.Fill;
            dgvMonthlyRevenue.Location = new Point(3, 3);
            dgvMonthlyRevenue.Margin = new Padding(3, 3, 8, 3);
            dgvMonthlyRevenue.Name = "dgvMonthlyRevenue";
            dgvMonthlyRevenue.ReadOnly = true;
            dgvMonthlyRevenue.RowHeadersVisible = false;
            dgvMonthlyRevenue.RowHeadersWidth = 51;
            dgvMonthlyRevenue.Size = new Size(574, 274);
            dgvMonthlyRevenue.TabIndex = 0;
            // 
            // colMonth
            // 
            colMonth.HeaderText = "Giá trị vé các tháng gần nhất";
            colMonth.MinimumWidth = 6;
            colMonth.Name = "colMonth";
            colMonth.ReadOnly = true;
            // 
            // colMonthRevenue
            // 
            colMonthRevenue.HeaderText = "Doanh thu (VNĐ)";
            colMonthRevenue.MinimumWidth = 6;
            colMonthRevenue.Name = "colMonthRevenue";
            colMonthRevenue.ReadOnly = true;
            // 
            // dgvMovieRevenue
            // 
            dgvMovieRevenue.AllowUserToAddRows = false;
            dgvMovieRevenue.AllowUserToDeleteRows = false;
            dgvMovieRevenue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovieRevenue.ColumnHeadersHeight = 29;
            dgvMovieRevenue.Columns.AddRange(new DataGridViewColumn[] { colMovieName, colMovieRevenue });
            dgvMovieRevenue.Dock = DockStyle.Fill;
            dgvMovieRevenue.Location = new Point(588, 3);
            dgvMovieRevenue.Name = "dgvMovieRevenue";
            dgvMovieRevenue.ReadOnly = true;
            dgvMovieRevenue.RowHeadersVisible = false;
            dgvMovieRevenue.RowHeadersWidth = 51;
            dgvMovieRevenue.Size = new Size(473, 274);
            dgvMovieRevenue.TabIndex = 1;
            // 
            // colMovieName
            // 
            colMovieName.HeaderText = "Giá trị vé theo phim (trước giảm giá)";
            colMovieName.MinimumWidth = 6;
            colMovieName.Name = "colMovieName";
            colMovieName.ReadOnly = true;
            // 
            // colMovieRevenue
            // 
            colMovieRevenue.HeaderText = "Doanh thu (VNĐ)";
            colMovieRevenue.MinimumWidth = 6;
            colMovieRevenue.Name = "colMovieRevenue";
            colMovieRevenue.ReadOnly = true;
            // 
            // pnlCards
            // 
            pnlCards.ColumnCount = 4;
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlCards.Controls.Add(pnlKpiRevenue, 0, 0);
            pnlCards.Controls.Add(pnlKpiMonth, 1, 0);
            pnlCards.Controls.Add(pnlKpiTickets, 2, 0);
            pnlCards.Controls.Add(pnlKpiMovies, 3, 0);
            pnlCards.Dock = DockStyle.Top;
            pnlCards.Location = new Point(18, 66);
            pnlCards.Margin = new Padding(0, 8, 0, 10);
            pnlCards.Name = "pnlCards";
            pnlCards.RowCount = 1;
            pnlCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlCards.Size = new Size(1064, 115);
            pnlCards.TabIndex = 1;
            // 
            // pnlKpiRevenue
            // 
            pnlKpiRevenue.BackColor = Color.White;
            pnlKpiRevenue.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiRevenue.Controls.Add(lblKpiRevenue);
            pnlKpiRevenue.Controls.Add(lblKpiRevenueTitle);
            pnlKpiRevenue.Controls.Add(lblKpiRevenueIcon);
            pnlKpiRevenue.Dock = DockStyle.Fill;
            pnlKpiRevenue.Location = new Point(6, 4);
            pnlKpiRevenue.Margin = new Padding(6, 4, 6, 4);
            pnlKpiRevenue.Name = "pnlKpiRevenue";
            pnlKpiRevenue.Size = new Size(254, 107);
            pnlKpiRevenue.TabIndex = 0;
            // 
            // lblKpiRevenue
            // 
            lblKpiRevenue.Font = new Font("Segoe UI", 15.5F, FontStyle.Bold);
            lblKpiRevenue.ForeColor = Color.FromArgb(0, 51, 102);
            lblKpiRevenue.Location = new Point(14, 38);
            lblKpiRevenue.Name = "lblKpiRevenue";
            lblKpiRevenue.Size = new Size(185, 36);
            lblKpiRevenue.TabIndex = 0;
            lblKpiRevenue.Text = "...";
            // 
            // lblKpiRevenueTitle
            // 
            lblKpiRevenueTitle.BackColor = Color.Transparent;
            lblKpiRevenueTitle.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblKpiRevenueTitle.ForeColor = Color.FromArgb(70, 70, 70);
            lblKpiRevenueTitle.Location = new Point(14, 12);
            lblKpiRevenueTitle.Name = "lblKpiRevenueTitle";
            lblKpiRevenueTitle.Size = new Size(200, 20);
            lblKpiRevenueTitle.TabIndex = 1;
            lblKpiRevenueTitle.Text = "TỔNG DOANH THU";
            // 
            // lblKpiRevenueIcon
            // 
            lblKpiRevenueIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblKpiRevenueIcon.ForeColor = Color.FromArgb(88, 145, 112);
            lblKpiRevenueIcon.Location = new Point(202, 39);
            lblKpiRevenueIcon.Name = "lblKpiRevenueIcon";
            lblKpiRevenueIcon.Size = new Size(38, 38);
            lblKpiRevenueIcon.TabIndex = 2;
            lblKpiRevenueIcon.Text = "₫";
            lblKpiRevenueIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlKpiMonth
            // 
            pnlKpiMonth.BackColor = Color.White;
            pnlKpiMonth.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiMonth.Controls.Add(lblKpiMonth);
            pnlKpiMonth.Controls.Add(lblKpiMonthTitle);
            pnlKpiMonth.Controls.Add(lblKpiMonthIcon);
            pnlKpiMonth.Dock = DockStyle.Fill;
            pnlKpiMonth.Location = new Point(272, 4);
            pnlKpiMonth.Margin = new Padding(6, 4, 6, 4);
            pnlKpiMonth.Name = "pnlKpiMonth";
            pnlKpiMonth.Size = new Size(254, 107);
            pnlKpiMonth.TabIndex = 1;
            // 
            // lblKpiMonth
            // 
            lblKpiMonth.Font = new Font("Segoe UI", 15.5F, FontStyle.Bold);
            lblKpiMonth.ForeColor = Color.FromArgb(0, 51, 102);
            lblKpiMonth.Location = new Point(14, 38);
            lblKpiMonth.Name = "lblKpiMonth";
            lblKpiMonth.Size = new Size(185, 36);
            lblKpiMonth.TabIndex = 0;
            lblKpiMonth.Text = "...";
            // 
            // lblKpiMonthTitle
            // 
            lblKpiMonthTitle.BackColor = Color.Transparent;
            lblKpiMonthTitle.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblKpiMonthTitle.ForeColor = Color.FromArgb(70, 70, 70);
            lblKpiMonthTitle.Location = new Point(14, 12);
            lblKpiMonthTitle.Name = "lblKpiMonthTitle";
            lblKpiMonthTitle.Size = new Size(200, 20);
            lblKpiMonthTitle.TabIndex = 1;
            lblKpiMonthTitle.Text = "DOANH THU THÁNG NÀY";
            // 
            // lblKpiMonthIcon
            // 
            lblKpiMonthIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblKpiMonthIcon.ForeColor = Color.FromArgb(88, 145, 112);
            lblKpiMonthIcon.Location = new Point(202, 39);
            lblKpiMonthIcon.Name = "lblKpiMonthIcon";
            lblKpiMonthIcon.Size = new Size(38, 38);
            lblKpiMonthIcon.TabIndex = 2;
            lblKpiMonthIcon.Text = "◷";
            lblKpiMonthIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlKpiTickets
            // 
            pnlKpiTickets.BackColor = Color.White;
            pnlKpiTickets.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiTickets.Controls.Add(lblKpiTickets);
            pnlKpiTickets.Controls.Add(lblKpiTicketsTitle);
            pnlKpiTickets.Controls.Add(lblKpiTicketsIcon);
            pnlKpiTickets.Dock = DockStyle.Fill;
            pnlKpiTickets.Location = new Point(538, 4);
            pnlKpiTickets.Margin = new Padding(6, 4, 6, 4);
            pnlKpiTickets.Name = "pnlKpiTickets";
            pnlKpiTickets.Size = new Size(254, 107);
            pnlKpiTickets.TabIndex = 2;
            // 
            // lblKpiTickets
            // 
            lblKpiTickets.Font = new Font("Segoe UI", 15.5F, FontStyle.Bold);
            lblKpiTickets.ForeColor = Color.FromArgb(0, 51, 102);
            lblKpiTickets.Location = new Point(14, 38);
            lblKpiTickets.Name = "lblKpiTickets";
            lblKpiTickets.Size = new Size(185, 36);
            lblKpiTickets.TabIndex = 0;
            lblKpiTickets.Text = "...";
            // 
            // lblKpiTicketsTitle
            // 
            lblKpiTicketsTitle.BackColor = Color.Transparent;
            lblKpiTicketsTitle.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblKpiTicketsTitle.ForeColor = Color.FromArgb(70, 70, 70);
            lblKpiTicketsTitle.Location = new Point(14, 12);
            lblKpiTicketsTitle.Name = "lblKpiTicketsTitle";
            lblKpiTicketsTitle.Size = new Size(200, 20);
            lblKpiTicketsTitle.TabIndex = 1;
            lblKpiTicketsTitle.Text = "TỔNG VÉ ĐÃ BÁN";
            lblKpiTicketsTitle.Click += lblKpiTicketsTitle_Click;
            // 
            // lblKpiTicketsIcon
            // 
            lblKpiTicketsIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblKpiTicketsIcon.ForeColor = Color.FromArgb(88, 145, 112);
            lblKpiTicketsIcon.Location = new Point(202, 39);
            lblKpiTicketsIcon.Name = "lblKpiTicketsIcon";
            lblKpiTicketsIcon.Size = new Size(38, 38);
            lblKpiTicketsIcon.TabIndex = 2;
            lblKpiTicketsIcon.Text = "▤";
            lblKpiTicketsIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlKpiMovies
            // 
            pnlKpiMovies.BackColor = Color.White;
            pnlKpiMovies.BorderStyle = BorderStyle.FixedSingle;
            pnlKpiMovies.Controls.Add(lblKpiMovies);
            pnlKpiMovies.Controls.Add(lblKpiMoviesTitle);
            pnlKpiMovies.Controls.Add(lblKpiMoviesIcon);
            pnlKpiMovies.Dock = DockStyle.Fill;
            pnlKpiMovies.Location = new Point(804, 4);
            pnlKpiMovies.Margin = new Padding(6, 4, 6, 4);
            pnlKpiMovies.Name = "pnlKpiMovies";
            pnlKpiMovies.Size = new Size(254, 107);
            pnlKpiMovies.TabIndex = 3;
            // 
            // lblKpiMovies
            // 
            lblKpiMovies.Font = new Font("Segoe UI", 15.5F, FontStyle.Bold);
            lblKpiMovies.ForeColor = Color.FromArgb(0, 51, 102);
            lblKpiMovies.Location = new Point(14, 38);
            lblKpiMovies.Name = "lblKpiMovies";
            lblKpiMovies.Size = new Size(185, 36);
            lblKpiMovies.TabIndex = 0;
            lblKpiMovies.Text = "...";
            // 
            // lblKpiMoviesTitle
            // 
            lblKpiMoviesTitle.BackColor = Color.Transparent;
            lblKpiMoviesTitle.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblKpiMoviesTitle.ForeColor = Color.FromArgb(70, 70, 70);
            lblKpiMoviesTitle.Location = new Point(14, 12);
            lblKpiMoviesTitle.Name = "lblKpiMoviesTitle";
            lblKpiMoviesTitle.Size = new Size(200, 20);
            lblKpiMoviesTitle.TabIndex = 1;
            lblKpiMoviesTitle.Text = "PHIM ĐANG CHIẾU";
            // 
            // lblKpiMoviesIcon
            // 
            lblKpiMoviesIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblKpiMoviesIcon.ForeColor = Color.FromArgb(88, 145, 112);
            lblKpiMoviesIcon.Location = new Point(202, 39);
            lblKpiMoviesIcon.Name = "lblKpiMoviesIcon";
            lblKpiMoviesIcon.Size = new Size(38, 38);
            lblKpiMoviesIcon.TabIndex = 2;
            lblKpiMoviesIcon.Text = "▣";
            lblKpiMoviesIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Transparent;
            pnlHeader.Controls.Add(lblUserHeader);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(18, 14);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1064, 52);
            pnlHeader.TabIndex = 0;
            // 
            // lblUserHeader
            // 
            lblUserHeader.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserHeader.AutoSize = true;
            lblUserHeader.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUserHeader.ForeColor = Color.DimGray;
            lblUserHeader.Location = new Point(920, 14);
            lblUserHeader.Name = "lblUserHeader";
            lblUserHeader.Size = new Size(109, 21);
            lblUserHeader.TabIndex = 2;
            lblUserHeader.Text = "Quản trị viên";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblTitle.Location = new Point(0, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(537, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TỔNG QUAN DOANH THU - HOẠT ĐỘNG RẠP";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(0, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 0;
            btnRefresh.Visible = false;
            // 
            // ucDashboard
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(pnlMain);
            Font = new Font("Segoe UI", 10F);
            Name = "ucDashboard";
            Size = new Size(1100, 750);
            pnlMain.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTopMovies).EndInit();
            pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMonthlyRevenue).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMovieRevenue).EndInit();
            pnlCards.ResumeLayout(false);
            pnlKpiRevenue.ResumeLayout(false);
            pnlKpiMonth.ResumeLayout(false);
            pnlKpiTickets.ResumeLayout(false);
            pnlKpiMovies.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);

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
