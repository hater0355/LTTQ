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
            menuStripMain = new System.Windows.Forms.MenuStrip();
            menuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            menuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            menuTaiKhoanThuNgan = new System.Windows.Forms.ToolStripMenuItem();
            sepHeThong1 = new System.Windows.Forms.ToolStripSeparator();
            menuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            menuThoat = new System.Windows.Forms.ToolStripMenuItem();
            menuPhim = new System.Windows.Forms.ToolStripMenuItem();
            menuPhongChieu = new System.Windows.Forms.ToolStripMenuItem();
            menuSuatChieu = new System.Windows.Forms.ToolStripMenuItem();
            menuBanHang = new System.Windows.Forms.ToolStripMenuItem();
            menuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            menuDoAn = new System.Windows.Forms.ToolStripMenuItem();
            menuCombo = new System.Windows.Forms.ToolStripMenuItem();
            menuVoucher = new System.Windows.Forms.ToolStripMenuItem();
            menuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            menuDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            statusStripMain = new System.Windows.Forms.StatusStrip();
            lblStatusUser = new System.Windows.Forms.ToolStripStatusLabel();
            lblStatusSpring1 = new System.Windows.Forms.ToolStripStatusLabel();
            lblStatusView = new System.Windows.Forms.ToolStripStatusLabel();
            lblStatusSpring2 = new System.Windows.Forms.ToolStripStatusLabel();
            lblStatusClock = new System.Windows.Forms.ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            pnlContent = new System.Windows.Forms.Panel();
            menuStripMain.SuspendLayout();
            statusStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            menuStripMain.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                menuHeThong,
                menuPhim,
                menuPhongChieu,
                menuSuatChieu,
                menuBanHang,
                menuHoaDon,
                menuDoAn,
                menuCombo,
                menuVoucher,
                menuNhanVien,
                menuTaiKhoanThuNgan,
                menuDoanhThu
            });
            menuStripMain.Location = new System.Drawing.Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new System.Windows.Forms.Padding(6, 4, 0, 4);
            menuStripMain.Size = new System.Drawing.Size(1200, 33);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStripMain";
            // 
            // menuHeThong
            // 
            menuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                menuDoiMatKhau,
                sepHeThong1,
                menuDangXuat,
                menuThoat
            });
            menuHeThong.Name = "menuHeThong";
            menuHeThong.Size = new System.Drawing.Size(86, 25);
            menuHeThong.Text = "&Hệ thống";
            // 
            // menuDoiMatKhau
            // 
            menuDoiMatKhau.Name = "menuDoiMatKhau";
            menuDoiMatKhau.Size = new System.Drawing.Size(180, 26);
            menuDoiMatKhau.Text = "Đổi mật khẩu";
            menuDoiMatKhau.Click += MenuDoiMatKhau_Click;
            // 
            // sepHeThong1
            // 
            sepHeThong1.Name = "sepHeThong1";
            sepHeThong1.Size = new System.Drawing.Size(177, 6);
            // 
            // menuDangXuat
            // 
            menuDangXuat.Name = "menuDangXuat";
            menuDangXuat.Size = new System.Drawing.Size(180, 26);
            menuDangXuat.Text = "Đăng xuất";
            menuDangXuat.Click += MenuDangXuat_Click;
            // 
            // menuThoat
            // 
            menuThoat.Name = "menuThoat";
            menuThoat.Size = new System.Drawing.Size(180, 26);
            menuThoat.Text = "Thoát";
            menuThoat.Click += MenuThoat_Click;
            // 
            // menuPhim
            // 
            menuPhim.Name = "menuPhim";
            menuPhim.Size = new System.Drawing.Size(125, 25);
            menuPhim.Text = "Phim Chiếu Rạp";
            menuPhim.Click += MenuPhim_Click;
            // 
            // menuPhongChieu
            // 
            menuPhongChieu.Name = "menuPhongChieu";
            menuPhongChieu.Size = new System.Drawing.Size(105, 25);
            menuPhongChieu.Text = "Phòng Chiếu";
            menuPhongChieu.Click += MenuPhongChieu_Click;
            // 
            // menuSuatChieu
            // 
            menuSuatChieu.Name = "menuSuatChieu";
            menuSuatChieu.Size = new System.Drawing.Size(170, 25);
            menuSuatChieu.Text = "Lịch Chiếu && Suất Chiếu";
            menuSuatChieu.Click += MenuSuatChieu_Click;
            // 
            // menuBanHang
            // 
            menuBanHang.Name = "menuBanHang";
            menuBanHang.Size = new System.Drawing.Size(145, 25);
            menuBanHang.Text = "Bán Vé && Bắp Nước";
            menuBanHang.Click += MenuBanHang_Click;
            // 
            // menuHoaDon
            // 
            menuHoaDon.Name = "menuHoaDon";
            menuHoaDon.Size = new System.Drawing.Size(130, 25);
            menuHoaDon.Text = "Tra Cứu Hóa Đơn";
            menuHoaDon.Click += MenuHoaDon_Click;
            // 
            // menuDoAn
            // 
            menuDoAn.Name = "menuDoAn";
            menuDoAn.Size = new System.Drawing.Size(145, 25);
            menuDoAn.Text = "Đồ Ăn && Nước Uống";
            menuDoAn.Click += MenuDoAn_Click;
            // 
            // menuCombo
            // 
            menuCombo.Name = "menuCombo";
            menuCombo.Size = new System.Drawing.Size(160, 25);
            menuCombo.Text = "Cài Đặt Ưu Đãi Combo";
            menuCombo.Click += MenuCombo_Click;
            // 
            // menuVoucher
            // 
            menuVoucher.Name = "menuVoucher";
            menuVoucher.Size = new System.Drawing.Size(125, 25);
            menuVoucher.Text = "Cài Đặt Voucher";
            menuVoucher.Click += MenuVoucher_Click;
            // 
            // menuNhanVien
            // 
            menuNhanVien.Name = "menuNhanVien";
            menuNhanVien.Size = new System.Drawing.Size(90, 25);
            menuNhanVien.Text = "Nhân Viên";
            menuNhanVien.Click += MenuNhanVien_Click;
            // 
            // menuTaiKhoanThuNgan
            // 
            menuTaiKhoanThuNgan.Name = "menuTaiKhoanThuNgan";
            menuTaiKhoanThuNgan.Size = new System.Drawing.Size(135, 25);
            menuTaiKhoanThuNgan.Text = "Quản Lý Tài Khoản";
            menuTaiKhoanThuNgan.Click += MenuTaiKhoanThuNgan_Click;
            // 
            // menuDoanhThu
            // 
            menuDoanhThu.Name = "menuDoanhThu";
            menuDoanhThu.Size = new System.Drawing.Size(145, 25);
            menuDoanhThu.Text = "Báo Cáo Doanh Thu";
            menuDoanhThu.Click += MenuDoanhThu_Click;
            // 
            // statusStripMain
            // 
            statusStripMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                lblStatusUser,
                lblStatusSpring1,
                lblStatusView,
                lblStatusSpring2,
                lblStatusClock
            });
            statusStripMain.Location = new System.Drawing.Point(0, 674);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new System.Drawing.Size(1200, 26);
            statusStripMain.TabIndex = 1;
            statusStripMain.Text = "statusStripMain";
            // 
            // lblStatusUser
            // 
            lblStatusUser.Name = "lblStatusUser";
            lblStatusUser.Size = new System.Drawing.Size(87, 20);
            lblStatusUser.Text = "Người dùng:";
            // 
            // lblStatusSpring1
            // 
            lblStatusSpring1.Name = "lblStatusSpring1";
            lblStatusSpring1.Size = new System.Drawing.Size(394, 20);
            lblStatusSpring1.Spring = true;
            // 
            // lblStatusView
            // 
            lblStatusView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblStatusView.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);
            lblStatusView.Name = "lblStatusView";
            lblStatusView.Size = new System.Drawing.Size(68, 20);
            lblStatusView.Text = "Sẵn sàng";
            // 
            // lblStatusSpring2
            // 
            lblStatusSpring2.Name = "lblStatusSpring2";
            lblStatusSpring2.Size = new System.Drawing.Size(394, 20);
            lblStatusSpring2.Spring = true;
            // 
            // lblStatusClock
            // 
            lblStatusClock.Name = "lblStatusClock";
            lblStatusClock.Size = new System.Drawing.Size(74, 20);
            lblStatusClock.Text = "Thời gian:";
            // 
            // timerClock
            // 
            timerClock.Interval = 1000;
            timerClock.Tick += TimerClock_Tick;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = System.Drawing.SystemColors.Control;
            pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlContent.Location = new System.Drawing.Point(0, 33);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new System.Drawing.Size(1200, 641);
            pnlContent.TabIndex = 2;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(1200, 700);
            Controls.Add(pnlContent);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            MainMenuStrip = menuStripMain;
            MinimumSize = new System.Drawing.Size(1000, 600);
            Name = "FormMain";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM - BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += FormMain_Load;
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStripMain;
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
