namespace HeThongRapChieuPhim.Forms
{
    partial class FormMain
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
            components = new System.ComponentModel.Container();
            pnlSidebar = new Panel();
            pnlNav = new FlowLayoutPanel();
            pnlNavSection1 = new Panel();
            lblNavSection1 = new Label();
            btnNav1 = new Button();
            btnNav2 = new Button();
            pnlNavSection2 = new Panel();
            lblNavSection2 = new Label();
            btnNav3 = new Button();
            btnNav4 = new Button();
            btnNav5 = new Button();
            btnNav6 = new Button();
            pnlNavSection3 = new Panel();
            lblNavSection3 = new Label();
            btnNav7 = new Button();
            btnNav8 = new Button();
            btnNav9 = new Button();
            pnlNavSection4 = new Panel();
            lblNavSection4 = new Label();
            btnNav10 = new Button();
            pnlBottomNav = new Panel();
            btnLogout = new Button();
            pnlBrand = new Panel();
            picLogo = new PictureBox();
            pnlTop = new Panel();
            lblUserTop = new Label();
            lblClock = new Label();
            lblCurrentTitle = new Label();
            timerClock = new System.Windows.Forms.Timer(components);
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
            pnlNav.SuspendLayout();
            pnlNavSection1.SuspendLayout();
            pnlNavSection2.SuspendLayout();
            pnlNavSection3.SuspendLayout();
            pnlNavSection4.SuspendLayout();
            pnlBottomNav.SuspendLayout();
            pnlBrand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(227, 237, 229);
            pnlSidebar.Controls.Add(pnlNav);
            pnlSidebar.Controls.Add(pnlBottomNav);
            pnlSidebar.Controls.Add(pnlBrand);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(245, 729);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlNav
            // 
            pnlNav.AutoScroll = true;
            pnlNav.BackColor = Color.FromArgb(227, 237, 229);
            pnlNav.Controls.Add(pnlNavSection1);
            pnlNav.Controls.Add(btnNav1);
            pnlNav.Controls.Add(btnNav2);
            pnlNav.Controls.Add(pnlNavSection2);
            pnlNav.Controls.Add(btnNav3);
            pnlNav.Controls.Add(btnNav4);
            pnlNav.Controls.Add(btnNav5);
            pnlNav.Controls.Add(btnNav6);
            pnlNav.Controls.Add(pnlNavSection3);
            pnlNav.Controls.Add(btnNav7);
            pnlNav.Controls.Add(btnNav8);
            pnlNav.Controls.Add(btnNav9);
            pnlNav.Controls.Add(pnlNavSection4);
            pnlNav.Controls.Add(btnNav10);
            pnlNav.Dock = DockStyle.Fill;
            pnlNav.FlowDirection = FlowDirection.TopDown;
            pnlNav.Location = new Point(0, 112);
            pnlNav.Name = "pnlNav";
            pnlNav.Padding = new Padding(6, 4, 6, 4);
            pnlNav.Size = new Size(245, 557);
            pnlNav.TabIndex = 0;
            pnlNav.WrapContents = false;
            // 
            // pnlNavSection1
            // 
            pnlNavSection1.BackColor = Color.Transparent;
            pnlNavSection1.Controls.Add(lblNavSection1);
            pnlNavSection1.Location = new Point(10, 14);
            pnlNavSection1.Margin = new Padding(4, 10, 4, 2);
            pnlNavSection1.Name = "pnlNavSection1";
            pnlNavSection1.Size = new Size(228, 24);
            pnlNavSection1.TabIndex = 0;
            // 
            // lblNavSection1
            // 
            lblNavSection1.Dock = DockStyle.Fill;
            lblNavSection1.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblNavSection1.ForeColor = Color.FromArgb(55, 90, 70);
            lblNavSection1.Location = new Point(0, 0);
            lblNavSection1.Name = "lblNavSection1";
            lblNavSection1.Size = new Size(228, 24);
            lblNavSection1.TabIndex = 0;
            lblNavSection1.Text = "👑  TOÀN QUYỀN QUẢN TRỊ";
            lblNavSection1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnNav1
            // 
            btnNav1.BackColor = Color.FromArgb(227, 237, 229);
            btnNav1.Cursor = Cursors.Hand;
            btnNav1.FlatAppearance.BorderSize = 0;
            btnNav1.FlatStyle = FlatStyle.Flat;
            btnNav1.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav1.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav1.Location = new Point(10, 42);
            btnNav1.Margin = new Padding(4, 2, 4, 2);
            btnNav1.Name = "btnNav1";
            btnNav1.Size = new Size(228, 36);
            btnNav1.TabIndex = 1;
            btnNav1.Text = "Doanh thu tổng quan";
            btnNav1.TextAlign = ContentAlignment.MiddleLeft;
            btnNav1.UseVisualStyleBackColor = false;
            // 
            // btnNav2
            // 
            btnNav2.BackColor = Color.FromArgb(227, 237, 229);
            btnNav2.Cursor = Cursors.Hand;
            btnNav2.FlatAppearance.BorderSize = 0;
            btnNav2.FlatStyle = FlatStyle.Flat;
            btnNav2.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav2.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav2.Location = new Point(10, 82);
            btnNav2.Margin = new Padding(4, 2, 4, 2);
            btnNav2.Name = "btnNav2";
            btnNav2.Size = new Size(228, 36);
            btnNav2.TabIndex = 2;
            btnNav2.Text = "Tra cứu hóa đơn";
            btnNav2.TextAlign = ContentAlignment.MiddleLeft;
            btnNav2.UseVisualStyleBackColor = false;
            // 
            // pnlNavSection2
            // 
            pnlNavSection2.BackColor = Color.Transparent;
            pnlNavSection2.Controls.Add(lblNavSection2);
            pnlNavSection2.Location = new Point(10, 130);
            pnlNavSection2.Margin = new Padding(4, 10, 4, 2);
            pnlNavSection2.Name = "pnlNavSection2";
            pnlNavSection2.Size = new Size(228, 24);
            pnlNavSection2.TabIndex = 3;
            // 
            // lblNavSection2
            // 
            lblNavSection2.Dock = DockStyle.Fill;
            lblNavSection2.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblNavSection2.ForeColor = Color.FromArgb(55, 90, 70);
            lblNavSection2.Location = new Point(0, 0);
            lblNavSection2.Name = "lblNavSection2";
            lblNavSection2.Size = new Size(228, 24);
            lblNavSection2.TabIndex = 0;
            lblNavSection2.Text = "📁  QUẢN LÝ DANH MỤC";
            lblNavSection2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnNav3
            // 
            btnNav3.BackColor = Color.FromArgb(227, 237, 229);
            btnNav3.Cursor = Cursors.Hand;
            btnNav3.FlatAppearance.BorderSize = 0;
            btnNav3.FlatStyle = FlatStyle.Flat;
            btnNav3.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav3.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav3.Location = new Point(10, 158);
            btnNav3.Margin = new Padding(4, 2, 4, 2);
            btnNav3.Name = "btnNav3";
            btnNav3.Size = new Size(228, 36);
            btnNav3.TabIndex = 4;
            btnNav3.Text = "Nhân viên";
            btnNav3.TextAlign = ContentAlignment.MiddleLeft;
            btnNav3.UseVisualStyleBackColor = false;
            // 
            // btnNav4
            // 
            btnNav4.BackColor = Color.FromArgb(227, 237, 229);
            btnNav4.Cursor = Cursors.Hand;
            btnNav4.FlatAppearance.BorderSize = 0;
            btnNav4.FlatStyle = FlatStyle.Flat;
            btnNav4.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav4.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav4.Location = new Point(10, 198);
            btnNav4.Margin = new Padding(4, 2, 4, 2);
            btnNav4.Name = "btnNav4";
            btnNav4.Size = new Size(228, 36);
            btnNav4.TabIndex = 5;
            btnNav4.Text = "Phim";
            btnNav4.TextAlign = ContentAlignment.MiddleLeft;
            btnNav4.UseVisualStyleBackColor = false;
            // 
            // btnNav5
            // 
            btnNav5.BackColor = Color.FromArgb(227, 237, 229);
            btnNav5.Cursor = Cursors.Hand;
            btnNav5.FlatAppearance.BorderSize = 0;
            btnNav5.FlatStyle = FlatStyle.Flat;
            btnNav5.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav5.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav5.Location = new Point(10, 238);
            btnNav5.Margin = new Padding(4, 2, 4, 2);
            btnNav5.Name = "btnNav5";
            btnNav5.Size = new Size(228, 36);
            btnNav5.TabIndex = 6;
            btnNav5.Text = "Đồ ăn nước uống";
            btnNav5.TextAlign = ContentAlignment.MiddleLeft;
            btnNav5.UseVisualStyleBackColor = false;
            // 
            // btnNav6
            // 
            btnNav6.BackColor = Color.FromArgb(227, 237, 229);
            btnNav6.Cursor = Cursors.Hand;
            btnNav6.FlatAppearance.BorderSize = 0;
            btnNav6.FlatStyle = FlatStyle.Flat;
            btnNav6.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav6.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav6.Location = new Point(10, 278);
            btnNav6.Margin = new Padding(4, 2, 4, 2);
            btnNav6.Name = "btnNav6";
            btnNav6.Size = new Size(228, 36);
            btnNav6.TabIndex = 7;
            btnNav6.Text = "Phòng chiếu";
            btnNav6.TextAlign = ContentAlignment.MiddleLeft;
            btnNav6.UseVisualStyleBackColor = false;
            // 
            // pnlNavSection3
            // 
            pnlNavSection3.BackColor = Color.Transparent;
            pnlNavSection3.Controls.Add(lblNavSection3);
            pnlNavSection3.Location = new Point(10, 326);
            pnlNavSection3.Margin = new Padding(4, 10, 4, 2);
            pnlNavSection3.Name = "pnlNavSection3";
            pnlNavSection3.Size = new Size(228, 24);
            pnlNavSection3.TabIndex = 8;
            // 
            // lblNavSection3
            // 
            lblNavSection3.Dock = DockStyle.Fill;
            lblNavSection3.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblNavSection3.ForeColor = Color.FromArgb(55, 90, 70);
            lblNavSection3.Location = new Point(0, 0);
            lblNavSection3.Name = "lblNavSection3";
            lblNavSection3.Size = new Size(228, 24);
            lblNavSection3.TabIndex = 0;
            lblNavSection3.Text = "⚙  CẤU HÌNH RẠP";
            lblNavSection3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnNav7
            // 
            btnNav7.BackColor = Color.FromArgb(227, 237, 229);
            btnNav7.Cursor = Cursors.Hand;
            btnNav7.FlatAppearance.BorderSize = 0;
            btnNav7.FlatStyle = FlatStyle.Flat;
            btnNav7.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav7.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav7.Location = new Point(10, 354);
            btnNav7.Margin = new Padding(4, 2, 4, 2);
            btnNav7.Name = "btnNav7";
            btnNav7.Size = new Size(228, 36);
            btnNav7.TabIndex = 9;
            btnNav7.Text = "Suất chiếu";
            btnNav7.TextAlign = ContentAlignment.MiddleLeft;
            btnNav7.UseVisualStyleBackColor = false;
            // 
            // btnNav8
            // 
            btnNav8.BackColor = Color.FromArgb(227, 237, 229);
            btnNav8.Cursor = Cursors.Hand;
            btnNav8.FlatAppearance.BorderSize = 0;
            btnNav8.FlatStyle = FlatStyle.Flat;
            btnNav8.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav8.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav8.Location = new Point(10, 394);
            btnNav8.Margin = new Padding(4, 2, 4, 2);
            btnNav8.Name = "btnNav8";
            btnNav8.Size = new Size(228, 36);
            btnNav8.TabIndex = 10;
            btnNav8.Text = "Combo";
            btnNav8.TextAlign = ContentAlignment.MiddleLeft;
            btnNav8.UseVisualStyleBackColor = false;
            // 
            // btnNav9
            // 
            btnNav9.BackColor = Color.FromArgb(227, 237, 229);
            btnNav9.Cursor = Cursors.Hand;
            btnNav9.FlatAppearance.BorderSize = 0;
            btnNav9.FlatStyle = FlatStyle.Flat;
            btnNav9.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav9.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav9.Location = new Point(10, 434);
            btnNav9.Margin = new Padding(4, 2, 4, 2);
            btnNav9.Name = "btnNav9";
            btnNav9.Size = new Size(228, 36);
            btnNav9.TabIndex = 11;
            btnNav9.Text = "Voucher";
            btnNav9.TextAlign = ContentAlignment.MiddleLeft;
            btnNav9.UseVisualStyleBackColor = false;
            // 
            // pnlNavSection4
            // 
            pnlNavSection4.BackColor = Color.Transparent;
            pnlNavSection4.Controls.Add(lblNavSection4);
            pnlNavSection4.Location = new Point(10, 482);
            pnlNavSection4.Margin = new Padding(4, 10, 4, 2);
            pnlNavSection4.Name = "pnlNavSection4";
            pnlNavSection4.Size = new Size(228, 24);
            pnlNavSection4.TabIndex = 12;
            // 
            // lblNavSection4
            // 
            lblNavSection4.Dock = DockStyle.Fill;
            lblNavSection4.Font = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            lblNavSection4.ForeColor = Color.FromArgb(55, 90, 70);
            lblNavSection4.Location = new Point(0, 0);
            lblNavSection4.Name = "lblNavSection4";
            lblNavSection4.Size = new Size(228, 24);
            lblNavSection4.TabIndex = 0;
            lblNavSection4.Text = "🔐  TÀI KHOẢN";
            lblNavSection4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnNav10
            // 
            btnNav10.BackColor = Color.FromArgb(227, 237, 229);
            btnNav10.Cursor = Cursors.Hand;
            btnNav10.FlatAppearance.BorderSize = 0;
            btnNav10.FlatStyle = FlatStyle.Flat;
            btnNav10.Font = new Font("Segoe UI Emoji", 9.2F);
            btnNav10.ForeColor = Color.FromArgb(30, 55, 40);
            btnNav10.Location = new Point(10, 510);
            btnNav10.Margin = new Padding(4, 2, 4, 2);
            btnNav10.Name = "btnNav10";
            btnNav10.Size = new Size(228, 36);
            btnNav10.TabIndex = 13;
            btnNav10.Text = "Admin & Thu ngân";
            btnNav10.TextAlign = ContentAlignment.MiddleLeft;
            btnNav10.UseVisualStyleBackColor = false;
            // 
            // pnlBottomNav
            // 
            pnlBottomNav.BackColor = Color.FromArgb(227, 237, 229);
            pnlBottomNav.Controls.Add(btnLogout);
            pnlBottomNav.Dock = DockStyle.Bottom;
            pnlBottomNav.Location = new Point(0, 669);
            pnlBottomNav.Name = "pnlBottomNav";
            pnlBottomNav.Padding = new Padding(12, 10, 12, 10);
            pnlBottomNav.Size = new Size(245, 60);
            pnlBottomNav.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(60, 107, 80);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(12, 10);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(221, 40);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "🚪  ĐĂNG XUẤT";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += BtnLogout_Click;
            btnLogout.MouseEnter += BtnLogout_MouseEnter;
            btnLogout.MouseLeave += BtnLogout_MouseLeave;
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.FromArgb(227, 237, 229);
            pnlBrand.Controls.Add(picLogo);
            pnlBrand.Dock = DockStyle.Top;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Size = new Size(245, 112);
            pnlBrand.TabIndex = 2;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(78, 12);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(88, 88);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(227, 237, 229);
            pnlTop.Controls.Add(lblUserTop);
            pnlTop.Controls.Add(lblClock);
            pnlTop.Controls.Add(lblCurrentTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(245, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15, 8, 15, 8);
            pnlTop.Size = new Size(1105, 58);
            pnlTop.TabIndex = 1;
            // 
            // lblUserTop
            // 
            lblUserTop.AutoSize = true;
            lblUserTop.Dock = DockStyle.Right;
            lblUserTop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserTop.ForeColor = Color.FromArgb(20, 83, 45);
            lblUserTop.Location = new Point(697, 8);
            lblUserTop.Name = "lblUserTop";
            lblUserTop.Padding = new Padding(0, 10, 15, 0);
            lblUserTop.Size = new Size(192, 33);
            lblUserTop.TabIndex = 2;
            lblUserTop.Text = "Quản trị viên NHH ∨";
            lblUserTop.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblClock
            // 
            lblClock.AutoSize = true;
            lblClock.Dock = DockStyle.Right;
            lblClock.Font = new Font("Segoe UI", 10F);
            lblClock.ForeColor = Color.FromArgb(45, 82, 64);
            lblClock.Location = new Point(889, 8);
            lblClock.Name = "lblClock";
            lblClock.Padding = new Padding(0, 10, 10, 0);
            lblClock.Size = new Size(201, 33);
            lblClock.TabIndex = 1;
            lblClock.Text = "🕒 11/10/2026 12:00:00";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCurrentTitle
            // 
            lblCurrentTitle.AutoSize = true;
            lblCurrentTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCurrentTitle.ForeColor = Color.FromArgb(20, 83, 45);
            lblCurrentTitle.Location = new Point(38, 16);
            lblCurrentTitle.Name = "lblCurrentTitle";
            lblCurrentTitle.Size = new Size(424, 28);
            lblCurrentTitle.TabIndex = 0;
            lblCurrentTitle.Text = "TRANG CHÍNH (DASHBOARD DOANH THU)";
            // 
            // timerClock
            // 
            timerClock.Interval = 1000;
            timerClock.Tick += TimerClock_Tick;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(240, 245, 241);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(245, 58);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1105, 671);
            pnlContent.TabIndex = 2;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 245, 241);
            ClientSize = new Size(1350, 729);
            Controls.Add(pnlContent);
            Controls.Add(pnlTop);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1100, 650);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM - NHH CINEMA";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            pnlSidebar.ResumeLayout(false);
            pnlNav.ResumeLayout(false);
            pnlNavSection1.ResumeLayout(false);
            pnlNavSection2.ResumeLayout(false);
            pnlNavSection3.ResumeLayout(false);
            pnlNavSection4.ResumeLayout(false);
            pnlBottomNav.ResumeLayout(false);
            pnlBrand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.FlowLayoutPanel pnlNav;
        private System.Windows.Forms.Panel pnlNavSection1;
        private System.Windows.Forms.Label lblNavSection1;
        private System.Windows.Forms.Panel pnlNavSection2;
        private System.Windows.Forms.Label lblNavSection2;
        private System.Windows.Forms.Panel pnlNavSection3;
        private System.Windows.Forms.Label lblNavSection3;
        private System.Windows.Forms.Panel pnlNavSection4;
        private System.Windows.Forms.Label lblNavSection4;
        private System.Windows.Forms.Button btnNav1;
        private System.Windows.Forms.Button btnNav2;
        private System.Windows.Forms.Button btnNav3;
        private System.Windows.Forms.Button btnNav4;
        private System.Windows.Forms.Button btnNav5;
        private System.Windows.Forms.Button btnNav6;
        private System.Windows.Forms.Button btnNav7;
        private System.Windows.Forms.Button btnNav8;
        private System.Windows.Forms.Button btnNav9;
        private System.Windows.Forms.Button btnNav10;
        private System.Windows.Forms.Panel pnlBottomNav;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblCurrentTitle;
        private System.Windows.Forms.Label lblUserTop;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Panel pnlContent;
    }
}
