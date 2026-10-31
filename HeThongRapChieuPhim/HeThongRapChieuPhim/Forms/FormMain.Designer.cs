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
            menuStripMain = new MenuStrip();
            _sidebarToggle = new ToolStripButton();
            _menuHome = new ToolStripButton();
            menuHeThong = new ToolStripMenuItem();
            menuDoiMatKhau = new ToolStripMenuItem();
            sepHeThong1 = new ToolStripSeparator();
            menuDangXuat = new ToolStripMenuItem();
            menuThoat = new ToolStripMenuItem();
            menuPhim = new ToolStripMenuItem();
            menuPhongChieu = new ToolStripMenuItem();
            menuSuatChieu = new ToolStripMenuItem();
            menuBanHang = new ToolStripMenuItem();
            menuHoaDon = new ToolStripMenuItem();
            menuDoAn = new ToolStripMenuItem();
            menuCombo = new ToolStripMenuItem();
            menuVoucher = new ToolStripMenuItem();
            menuNhanVien = new ToolStripMenuItem();
            menuLichLamViec = new ToolStripMenuItem();
            menuTaiKhoanThuNgan = new ToolStripMenuItem();
            menuDoanhThu = new ToolStripMenuItem();
            statusStripMain = new StatusStrip();
            lblStatusUser = new ToolStripStatusLabel();
            lblStatusSpring1 = new ToolStripStatusLabel();
            lblStatusView = new ToolStripStatusLabel();
            lblStatusSpring2 = new ToolStripStatusLabel();
            lblStatusClock = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            pnlContent = new Panel();
            _ucDashboard = new HeThongRapChieuPhim.UserControls.ucDashboard();
            menuStripMain.SuspendLayout();
            statusStripMain.SuspendLayout();
            pnlContent.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = Color.FromArgb(5, 48, 45);
            menuStripMain.ForeColor = Color.White;
            menuStripMain.Dock = DockStyle.Left;
            menuStripMain.AutoSize = false;
            menuStripMain.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            menuStripMain.Padding = new Padding(8);
            menuStripMain.Font = new Font("Segoe UI", 9.5F);
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { _sidebarToggle, _menuHome, menuHeThong, menuPhim, menuPhongChieu, menuSuatChieu, menuBanHang, menuHoaDon, menuDoAn, menuCombo, menuVoucher, menuNhanVien, menuLichLamViec, menuTaiKhoanThuNgan, menuDoanhThu });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(6, 4, 0, 4);
            menuStripMain.Size = new Size(232, 700);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStripMain";
            // 
            // _sidebarToggle
            // 
            _sidebarToggle.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _sidebarToggle.BackColor = Color.FromArgb(5, 48, 45);
            _sidebarToggle.ForeColor = Color.White;
            _sidebarToggle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            _sidebarToggle.TextAlign = ContentAlignment.MiddleLeft;
            _sidebarToggle.Padding = new Padding(10, 8, 8, 8);
            _sidebarToggle.Name = "btnSidebarToggle";
            _sidebarToggle.Size = new Size(212, 42);
            _sidebarToggle.Text = "☰    CinemaManager";
            _sidebarToggle.ToolTipText = "Thu gọn thanh điều hướng";
            // 
            // _menuHome
            // 
            _menuHome.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _menuHome.BackColor = Color.FromArgb(0, 139, 113);
            _menuHome.ForeColor = Color.White;
            _menuHome.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _menuHome.TextAlign = ContentAlignment.MiddleLeft;
            _menuHome.Padding = new Padding(10, 8, 8, 8);
            _menuHome.Name = "menuHome";
            _menuHome.Size = new Size(212, 42);
            _menuHome.Text = "⌂  Trang chủ";
            // 
            // menuHeThong
            // 
            menuHeThong.DropDownItems.AddRange(new ToolStripItem[] { menuDoiMatKhau, sepHeThong1, menuDangXuat, menuThoat });
            menuHeThong.Name = "menuHeThong";
            menuHeThong.BackColor = Color.FromArgb(5, 48, 45);
            menuHeThong.ForeColor = Color.White;
            menuHeThong.Size = new Size(212, 42);
            menuHeThong.Padding = new Padding(10, 8, 8, 8);
            menuHeThong.Text = "⚙   Hệ thống";
            // 
            // menuDoiMatKhau
            // 
            menuDoiMatKhau.Name = "menuDoiMatKhau";
            menuDoiMatKhau.Size = new Size(187, 26);
            menuDoiMatKhau.Text = "Đổi mật khẩu";
            menuDoiMatKhau.Click += MenuDoiMatKhau_Click;
            // 
            // sepHeThong1
            // 
            sepHeThong1.Name = "sepHeThong1";
            sepHeThong1.Size = new Size(184, 6);
            // 
            // menuDangXuat
            // 
            menuDangXuat.Name = "menuDangXuat";
            menuDangXuat.Size = new Size(187, 26);
            menuDangXuat.Text = "Đăng xuất";
            menuDangXuat.Click += MenuDangXuat_Click;
            // 
            // menuThoat
            // 
            menuThoat.Name = "menuThoat";
            menuThoat.Size = new Size(187, 26);
            menuThoat.Text = "Thoát";
            menuThoat.Click += MenuThoat_Click;
            // 
            // menuPhim
            // 
            menuPhim.Name = "menuPhim";
            menuPhim.BackColor = Color.FromArgb(5, 48, 45);
            menuPhim.ForeColor = Color.White;
            menuPhim.Size = new Size(212, 42);
            menuPhim.Padding = new Padding(10, 8, 8, 8);
            menuPhim.Text = "▣   Phim Chiếu Rạp";
            menuPhim.Click += MenuPhim_Click;
            // 
            // menuPhongChieu
            // 
            menuPhongChieu.Name = "menuPhongChieu";
            menuPhongChieu.BackColor = Color.FromArgb(5, 48, 45);
            menuPhongChieu.ForeColor = Color.White;
            menuPhongChieu.Size = new Size(212, 42);
            menuPhongChieu.Padding = new Padding(10, 8, 8, 8);
            menuPhongChieu.Text = "▦   Phòng Chiếu";
            menuPhongChieu.Click += MenuPhongChieu_Click;
            // 
            // menuSuatChieu
            // 
            menuSuatChieu.Name = "menuSuatChieu";
            menuSuatChieu.BackColor = Color.FromArgb(5, 48, 45);
            menuSuatChieu.ForeColor = Color.White;
            menuSuatChieu.Size = new Size(212, 42);
            menuSuatChieu.Padding = new Padding(10, 8, 8, 8);
            menuSuatChieu.Text = "◷   Lịch Chiếu & Suất Chiếu";
            menuSuatChieu.Click += MenuSuatChieu_Click;
            // 
            // menuBanHang
            // 
            menuBanHang.Name = "menuBanHang";
            menuBanHang.BackColor = Color.FromArgb(5, 48, 45);
            menuBanHang.ForeColor = Color.White;
            menuBanHang.Size = new Size(212, 42);
            menuBanHang.Padding = new Padding(10, 8, 8, 8);
            menuBanHang.Text = "▤   Bán Vé & Bắp Nước";
            menuBanHang.Click += MenuBanHang_Click;
            // 
            // menuHoaDon
            // 
            menuHoaDon.Name = "menuHoaDon";
            menuHoaDon.BackColor = Color.FromArgb(5, 48, 45);
            menuHoaDon.ForeColor = Color.White;
            menuHoaDon.Size = new Size(212, 42);
            menuHoaDon.Padding = new Padding(10, 8, 8, 8);
            menuHoaDon.Text = "▧   Tra Cứu Hóa Đơn";
            menuHoaDon.Click += MenuHoaDon_Click;
            // 
            // menuDoAn
            // 
            menuDoAn.Name = "menuDoAn";
            menuDoAn.BackColor = Color.FromArgb(5, 48, 45);
            menuDoAn.ForeColor = Color.White;
            menuDoAn.Size = new Size(212, 42);
            menuDoAn.Padding = new Padding(10, 8, 8, 8);
            menuDoAn.Text = "♨   Đồ Ăn & Nước Uống";
            menuDoAn.Click += MenuDoAn_Click;
            // 
            // menuCombo
            // 
            menuCombo.Name = "menuCombo";
            menuCombo.BackColor = Color.FromArgb(5, 48, 45);
            menuCombo.ForeColor = Color.White;
            menuCombo.Size = new Size(212, 42);
            menuCombo.Padding = new Padding(10, 8, 8, 8);
            menuCombo.Text = "✦   Cài Đặt Ưu Đãi Combo";
            menuCombo.Click += MenuCombo_Click;
            // 
            // menuVoucher
            // 
            menuVoucher.Name = "menuVoucher";
            menuVoucher.BackColor = Color.FromArgb(5, 48, 45);
            menuVoucher.ForeColor = Color.White;
            menuVoucher.Size = new Size(212, 42);
            menuVoucher.Padding = new Padding(10, 8, 8, 8);
            menuVoucher.Text = "%   Cài Đặt Voucher";
            menuVoucher.Click += MenuVoucher_Click;
            // 
            // menuNhanVien
            // 
            menuNhanVien.Name = "menuNhanVien";
            menuNhanVien.BackColor = Color.FromArgb(5, 48, 45);
            menuNhanVien.ForeColor = Color.White;
            menuNhanVien.Size = new Size(212, 42);
            menuNhanVien.Padding = new Padding(10, 8, 8, 8);
            menuNhanVien.Text = "♙   Nhân Viên";
            menuNhanVien.Click += MenuNhanVien_Click;
            // 
            // menuLichLamViec
            // 
            menuLichLamViec.Name = "menuLichLamViec";
            menuLichLamViec.BackColor = Color.FromArgb(5, 48, 45);
            menuLichLamViec.ForeColor = Color.White;
            menuLichLamViec.Size = new Size(212, 42);
            menuLichLamViec.Padding = new Padding(10, 8, 8, 8);
            menuLichLamViec.Text = "▣   Lịch & Thay Ca";
            menuLichLamViec.Click += MenuLichLamViec_Click;
            // 
            // menuTaiKhoanThuNgan
            // 
            menuTaiKhoanThuNgan.Name = "menuTaiKhoanThuNgan";
            menuTaiKhoanThuNgan.BackColor = Color.FromArgb(5, 48, 45);
            menuTaiKhoanThuNgan.ForeColor = Color.White;
            menuTaiKhoanThuNgan.Size = new Size(212, 42);
            menuTaiKhoanThuNgan.Padding = new Padding(10, 8, 8, 8);
            menuTaiKhoanThuNgan.Text = "♟   Quản Lý Tài Khoản";
            menuTaiKhoanThuNgan.Click += MenuTaiKhoanThuNgan_Click;
            // 
            // menuDoanhThu
            // 
            menuDoanhThu.Name = "menuDoanhThu";
            menuDoanhThu.BackColor = Color.FromArgb(5, 48, 45);
            menuDoanhThu.ForeColor = Color.White;
            menuDoanhThu.Size = new Size(212, 42);
            menuDoanhThu.Padding = new Padding(10, 8, 8, 8);
            menuDoanhThu.Text = "▥   Báo Cáo Doanh Thu";
            menuDoanhThu.Click += MenuDoanhThu_Click;
            // 
            // statusStripMain
            // 
            statusStripMain.Font = new Font("Segoe UI", 9F);
            statusStripMain.ImageScalingSize = new Size(20, 20);
            statusStripMain.Items.AddRange(new ToolStripItem[] { lblStatusUser, lblStatusSpring1, lblStatusView, lblStatusSpring2, lblStatusClock });
            statusStripMain.Location = new Point(198, 674);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(1002, 26);
            statusStripMain.TabIndex = 1;
            statusStripMain.Text = "statusStripMain";
            // 
            // lblStatusUser
            // 
            lblStatusUser.Name = "lblStatusUser";
            lblStatusUser.Size = new Size(92, 20);
            lblStatusUser.Text = "Người dùng:";
            // 
            // lblStatusSpring1
            // 
            lblStatusSpring1.Name = "lblStatusSpring1";
            lblStatusSpring1.Size = new Size(375, 20);
            lblStatusSpring1.Spring = true;
            // 
            // lblStatusView
            // 
            lblStatusView.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusView.ForeColor = Color.FromArgb(0, 51, 102);
            lblStatusView.Name = "lblStatusView";
            lblStatusView.Size = new Size(71, 20);
            lblStatusView.Text = "Sẵn sàng";
            // 
            // lblStatusSpring2
            // 
            lblStatusSpring2.Name = "lblStatusSpring2";
            lblStatusSpring2.Size = new Size(375, 20);
            lblStatusSpring2.Spring = true;
            // 
            // lblStatusClock
            // 
            lblStatusClock.Name = "lblStatusClock";
            lblStatusClock.Size = new Size(74, 20);
            lblStatusClock.Text = "Thời gian:";
            // 
            // timerClock
            // 
            timerClock.Interval = 1000;
            timerClock.Tick += TimerClock_Tick;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = SystemColors.Control;
            pnlContent.Controls.Add(_ucDashboard);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(198, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1002, 674);
            pnlContent.TabIndex = 2;
            // 
            // _ucDashboard
            // 
            _ucDashboard.BackColor = Color.FromArgb(243, 246, 249);
            _ucDashboard.Font = new Font("Segoe UI", 10F);
            _ucDashboard.Location = new Point(0, 0);
            _ucDashboard.Name = "_ucDashboard";
            _ucDashboard.Dock = DockStyle.Fill;
            _ucDashboard.Size = new Size(1002, 674);
            _ucDashboard.TabIndex = 0;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1200, 700);
            Controls.Add(pnlContent);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            Font = new Font("Segoe UI", 9.5F);
            MainMenuStrip = menuStripMain;
            MinimumSize = new Size(1000, 600);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM - BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            pnlContent.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripButton _sidebarToggle;
        private System.Windows.Forms.ToolStripButton _menuHome;
        private System.Windows.Forms.ToolStripMenuItem menuHeThong;
        private System.Windows.Forms.ToolStripMenuItem menuDoiMatKhau;
        private System.Windows.Forms.ToolStripMenuItem menuTaiKhoanThuNgan;
        private System.Windows.Forms.ToolStripSeparator sepHeThong1;
        private System.Windows.Forms.ToolStripMenuItem menuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem menuThoat;
        private System.Windows.Forms.ToolStripMenuItem menuPhim;
        private System.Windows.Forms.ToolStripMenuItem menuPhongChieu;
        private System.Windows.Forms.ToolStripMenuItem menuSuatChieu;
        private System.Windows.Forms.ToolStripMenuItem menuBanHang;
        private System.Windows.Forms.ToolStripMenuItem menuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem menuDoAn;
        private System.Windows.Forms.ToolStripMenuItem menuCombo;
        private System.Windows.Forms.ToolStripMenuItem menuVoucher;
        private System.Windows.Forms.ToolStripMenuItem menuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem menuLichLamViec;
        private System.Windows.Forms.ToolStripMenuItem menuDoanhThu;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusUser;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSpring1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusView;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusSpring2;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusClock;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Panel pnlContent;
    }
}
