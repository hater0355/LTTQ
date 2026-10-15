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
            pnlTop = new Panel();
            lblUserTop = new Label();
            lblClock = new Label();
            lblCurrentTitle = new Label();
            timerClock = new System.Windows.Forms.Timer(components);
            pnlContent = new Panel();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(227, 237, 229);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(245, 729);
            pnlSidebar.TabIndex = 0;
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
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblCurrentTitle;
        private System.Windows.Forms.Label lblUserTop;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Panel pnlContent;
    }
}
