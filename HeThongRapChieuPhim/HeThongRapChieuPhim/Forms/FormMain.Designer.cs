using System.Windows.Forms.DataVisualization.Charting;
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
            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            ChartArea chartArea2 = new ChartArea();
            Series series2 = new Series();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            sidebarPanel = new Panel();
            menuStripMain = new MenuStrip();
            _sidebarToggle = new ToolStripButton();
            _menuHome = new ToolStripButton();
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
            btnSystem = new Button();
            systemMenu = new ContextMenuStrip(components);
            menuDoiMatKhau = new ToolStripMenuItem();
            sepHeThong1 = new ToolStripSeparator();
            menuDangXuat = new ToolStripMenuItem();
            menuThoat = new ToolStripMenuItem();
            menuHeThong = new ToolStripMenuItem();
            statusStripMain = new StatusStrip();
            lblStatusUser = new ToolStripStatusLabel();
            lblStatusSpring1 = new ToolStripStatusLabel();
            lblStatusView = new ToolStripStatusLabel();
            lblStatusSpring2 = new ToolStripStatusLabel();
            lblStatusClock = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            pnlContent = new Panel();
            dashboardLayout = new TableLayoutPanel();
            headerLayout = new TableLayoutPanel();
            lblTitle = new Label();
            btnRefresh = new Button();
            lblDate = new Label();
            kpiLayout = new TableLayoutPanel();
            pnlRevenue = new Panel();
            lblRevenue = new Label();
            lblRevenueTitle = new Label();
            pnlMonth = new Panel();
            lblMonth = new Label();
            lblMonthTitle = new Label();
            pnlTickets = new Panel();
            lblTickets = new Label();
            lblTicketsTitle = new Label();
            pnlMovies = new Panel();
            lblMovies = new Label();
            lblMoviesTitle = new Label();
            analyticsLayout = new TableLayoutPanel();
            pnlchartWeekly = new Panel();
            chartWeekly = new Chart();
            pnlchartWeeklyTitle = new Label();
            pnlchartMovieRevenue = new Panel();
            chartMovieRevenue = new Chart();
            pnlchartMovieRevenueTitle = new Label();
            pnlNowShowing = new Panel();
            dgvNowShowing = new DataGridView();
            pnlNowShowingTitle = new Label();
            bottomLayout = new TableLayoutPanel();
            pnlShowtimes = new Panel();
            dgvShowtimes = new DataGridView();
            pnlShowtimesTitle = new Label();
            pnlTopMovies = new Panel();
            dgvTopMovies = new DataGridView();
            pnlTopMoviesTitle = new Label();
            lblStatus = new Label();
            sidebarPanel.SuspendLayout();
            menuStripMain.SuspendLayout();
            systemMenu.SuspendLayout();
            statusStripMain.SuspendLayout();
            pnlContent.SuspendLayout();
            dashboardLayout.SuspendLayout();
            headerLayout.SuspendLayout();
            kpiLayout.SuspendLayout();
            pnlRevenue.SuspendLayout();
            pnlMonth.SuspendLayout();
            pnlTickets.SuspendLayout();
            pnlMovies.SuspendLayout();
            analyticsLayout.SuspendLayout();
            pnlchartWeekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartWeekly).BeginInit();
            pnlchartMovieRevenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartMovieRevenue).BeginInit();
            pnlNowShowing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNowShowing).BeginInit();
            bottomLayout.SuspendLayout();
            pnlShowtimes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShowtimes).BeginInit();
            pnlTopMovies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopMovies).BeginInit();
            SuspendLayout();
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.White;
            sidebarPanel.Controls.Add(menuStripMain);
            sidebarPanel.Controls.Add(btnSystem);
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(232, 700);
            sidebarPanel.TabIndex = 3;
            // 
            // menuStripMain
            // 
            menuStripMain.AutoSize = false;
            menuStripMain.BackColor = Color.White;
            menuStripMain.Dock = DockStyle.Fill;
            menuStripMain.Font = new Font("Segoe UI", 9.5F);
            menuStripMain.ForeColor = Color.Black;
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { _sidebarToggle, _menuHome, menuPhim, menuPhongChieu, menuSuatChieu, menuBanHang, menuHoaDon, menuDoAn, menuCombo, menuVoucher, menuNhanVien, menuLichLamViec, menuTaiKhoanThuNgan, menuDoanhThu });
            menuStripMain.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(6, 4, 0, 4);
            menuStripMain.Size = new Size(232, 656);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStripMain";
            // 
            // _sidebarToggle
            // 
            _sidebarToggle.BackColor = Color.White;
            _sidebarToggle.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _sidebarToggle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            _sidebarToggle.ForeColor = Color.Navy;
            _sidebarToggle.AutoSize = false;
            _sidebarToggle.Name = "_sidebarToggle";
            _sidebarToggle.Padding = new Padding(10, 8, 8, 8);
            _sidebarToggle.Size = new Size(225, 50);
            _sidebarToggle.Text = "☰    CinemaManager";
            _sidebarToggle.TextAlign = ContentAlignment.MiddleLeft;
            _sidebarToggle.ToolTipText = "Thu gọn thanh điều hướng";
            _sidebarToggle.Click += _sidebarToggle_Click;
            // 
            // _menuHome
            // 
            _menuHome.BackColor = Color.White;
            _menuHome.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _menuHome.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _menuHome.ForeColor = Color.Black;
            _menuHome.AutoSize = false;
            _menuHome.Name = "_menuHome";
            _menuHome.Padding = new Padding(10, 8, 8, 8);
            _menuHome.Size = new Size(225, 43);
            _menuHome.Text = "⌂  Trang chủ";
            _menuHome.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // menuPhim
            // 
            menuPhim.BackColor = Color.White;
            menuPhim.ForeColor = Color.Black;
            menuPhim.AutoSize = false;
            menuPhim.Name = "menuPhim";
            menuPhim.Padding = new Padding(10, 8, 8, 8);
            menuPhim.Size = new Size(225, 41);
            menuPhim.Text = "▣   Phim Chiếu Rạp";
            menuPhim.Click += MenuPhim_Click;
            // 
            // menuPhongChieu
            // 
            menuPhongChieu.BackColor = Color.White;
            menuPhongChieu.ForeColor = Color.Black;
            menuPhongChieu.AutoSize = false;
            menuPhongChieu.Name = "menuPhongChieu";
            menuPhongChieu.Padding = new Padding(10, 8, 8, 8);
            menuPhongChieu.Size = new Size(225, 41);
            menuPhongChieu.Text = "▦   Phòng Chiếu";
            menuPhongChieu.Click += MenuPhongChieu_Click;
            // 
            // menuSuatChieu
            // 
            menuSuatChieu.BackColor = Color.White;
            menuSuatChieu.ForeColor = Color.Black;
            menuSuatChieu.AutoSize = false;
            menuSuatChieu.Name = "menuSuatChieu";
            menuSuatChieu.Padding = new Padding(10, 8, 8, 8);
            menuSuatChieu.Size = new Size(225, 41);
            menuSuatChieu.Text = "◷   Lịch Chiếu & Suất Chiếu";
            menuSuatChieu.Click += MenuSuatChieu_Click;
            // 
            // menuBanHang
            // 
            menuBanHang.BackColor = Color.White;
            menuBanHang.ForeColor = Color.Black;
            menuBanHang.AutoSize = false;
            menuBanHang.Name = "menuBanHang";
            menuBanHang.Padding = new Padding(10, 8, 8, 8);
            menuBanHang.Size = new Size(225, 41);
            menuBanHang.Text = "▤   Bán Vé & Bắp Nước";
            menuBanHang.Click += MenuBanHang_Click;
            // 
            // menuHoaDon
            // 
            menuHoaDon.BackColor = Color.White;
            menuHoaDon.ForeColor = Color.Black;
            menuHoaDon.AutoSize = false;
            menuHoaDon.Name = "menuHoaDon";
            menuHoaDon.Padding = new Padding(10, 8, 8, 8);
            menuHoaDon.Size = new Size(225, 41);
            menuHoaDon.Text = "▧   Tra Cứu Hóa Đơn";
            menuHoaDon.Click += MenuHoaDon_Click;
            // 
            // menuDoAn
            // 
            menuDoAn.BackColor = Color.White;
            menuDoAn.ForeColor = Color.Black;
            menuDoAn.AutoSize = false;
            menuDoAn.Name = "menuDoAn";
            menuDoAn.Padding = new Padding(10, 8, 8, 8);
            menuDoAn.Size = new Size(225, 41);
            menuDoAn.Text = "♨   Đồ Ăn & Nước Uống";
            menuDoAn.Click += MenuDoAn_Click;
            // 
            // menuCombo
            // 
            menuCombo.BackColor = Color.White;
            menuCombo.ForeColor = Color.Black;
            menuCombo.AutoSize = false;
            menuCombo.Name = "menuCombo";
            menuCombo.Padding = new Padding(10, 8, 8, 8);
            menuCombo.Size = new Size(225, 41);
            menuCombo.Text = "✦   Cài Đặt Ưu Đãi Combo";
            menuCombo.Click += MenuCombo_Click;
            // 
            // menuVoucher
            // 
            menuVoucher.BackColor = Color.White;
            menuVoucher.ForeColor = Color.Black;
            menuVoucher.AutoSize = false;
            menuVoucher.Name = "menuVoucher";
            menuVoucher.Padding = new Padding(10, 8, 8, 8);
            menuVoucher.Size = new Size(225, 41);
            menuVoucher.Text = "%   Cài Đặt Voucher";
            menuVoucher.Click += MenuVoucher_Click;
            // 
            // menuNhanVien
            // 
            menuNhanVien.BackColor = Color.White;
            menuNhanVien.ForeColor = Color.Black;
            menuNhanVien.AutoSize = false;
            menuNhanVien.Name = "menuNhanVien";
            menuNhanVien.Padding = new Padding(10, 8, 8, 8);
            menuNhanVien.Size = new Size(225, 41);
            menuNhanVien.Text = "♙   Nhân Viên";
            menuNhanVien.Click += MenuNhanVien_Click;
            // 
            // menuLichLamViec
            // 
            menuLichLamViec.BackColor = Color.White;
            menuLichLamViec.ForeColor = Color.Black;
            menuLichLamViec.AutoSize = false;
            menuLichLamViec.Name = "menuLichLamViec";
            menuLichLamViec.Padding = new Padding(10, 8, 8, 8);
            menuLichLamViec.Size = new Size(225, 41);
            menuLichLamViec.Text = "▣   Lịch & Thay Ca";
            menuLichLamViec.Click += MenuLichLamViec_Click;
            // 
            // menuTaiKhoanThuNgan
            // 
            menuTaiKhoanThuNgan.BackColor = Color.White;
            menuTaiKhoanThuNgan.ForeColor = Color.Black;
            menuTaiKhoanThuNgan.AutoSize = false;
            menuTaiKhoanThuNgan.Name = "menuTaiKhoanThuNgan";
            menuTaiKhoanThuNgan.Padding = new Padding(10, 8, 8, 8);
            menuTaiKhoanThuNgan.Size = new Size(225, 41);
            menuTaiKhoanThuNgan.Text = "♟   Quản Lý Tài Khoản";
            menuTaiKhoanThuNgan.Click += MenuTaiKhoanThuNgan_Click;
            // 
            // menuDoanhThu
            // 
            menuDoanhThu.BackColor = Color.White;
            menuDoanhThu.ForeColor = Color.Black;
            menuDoanhThu.AutoSize = false;
            menuDoanhThu.Name = "menuDoanhThu";
            menuDoanhThu.Padding = new Padding(10, 8, 8, 8);
            menuDoanhThu.Size = new Size(225, 41);
            menuDoanhThu.Text = "▥   Báo Cáo Doanh Thu";
            menuDoanhThu.Click += MenuDoanhThu_Click;
            // 
            // btnSystem
            // 
            btnSystem.BackColor = Color.White;
            btnSystem.Cursor = Cursors.Hand;
            btnSystem.Dock = DockStyle.Bottom;
            btnSystem.FlatAppearance.BorderSize = 0;
            btnSystem.FlatStyle = FlatStyle.Flat;
            btnSystem.Font = new Font("Segoe UI", 10F);
            btnSystem.ForeColor = Color.Black;
            btnSystem.Location = new Point(0, 656);
            btnSystem.Name = "btnSystem";
            btnSystem.Size = new Size(232, 44);
            btnSystem.TabIndex = 1;
            btnSystem.Text = "Hệ thống";
            btnSystem.UseVisualStyleBackColor = false;
            btnSystem.Click += SystemButton_Click;
            // 
            // systemMenu
            // 
            systemMenu.ImageScalingSize = new Size(20, 20);
            systemMenu.Items.AddRange(new ToolStripItem[] { menuDoiMatKhau, sepHeThong1, menuDangXuat, menuThoat });
            systemMenu.Name = "systemMenu";
            systemMenu.Size = new Size(168, 82);
            // 
            // menuDoiMatKhau
            // 
            menuDoiMatKhau.AutoSize = false;
            menuDoiMatKhau.Name = "menuDoiMatKhau";
            menuDoiMatKhau.Size = new Size(167, 24);
            menuDoiMatKhau.Text = "Đổi mật khẩu";
            menuDoiMatKhau.Click += MenuDoiMatKhau_Click;
            // 
            // sepHeThong1
            // 
            sepHeThong1.Name = "sepHeThong1";
            sepHeThong1.Size = new Size(164, 6);
            // 
            // menuDangXuat
            // 
            menuDangXuat.AutoSize = false;
            menuDangXuat.Name = "menuDangXuat";
            menuDangXuat.Size = new Size(167, 24);
            menuDangXuat.Text = "Đăng xuất";
            menuDangXuat.Click += MenuDangXuat_Click;
            // 
            // menuThoat
            // 
            menuThoat.AutoSize = false;
            menuThoat.Name = "menuThoat";
            menuThoat.Size = new Size(167, 24);
            menuThoat.Text = "Thoát";
            menuThoat.Click += MenuThoat_Click;
            // 
            // menuHeThong
            // 
            menuHeThong.BackColor = Color.White;
            menuHeThong.ForeColor = Color.Black;
            menuHeThong.AutoSize = false;
            menuHeThong.Name = "menuHeThong";
            menuHeThong.Padding = new Padding(10, 8, 8, 8);
            menuHeThong.Size = new Size(225, 41);
            menuHeThong.Text = "⚙   Hệ thống";
            // 
            // statusStripMain
            // 
            statusStripMain.Font = new Font("Segoe UI", 9F);
            statusStripMain.ImageScalingSize = new Size(20, 20);
            statusStripMain.Items.AddRange(new ToolStripItem[] { lblStatusUser, lblStatusSpring1, lblStatusView, lblStatusSpring2, lblStatusClock });
            statusStripMain.Location = new Point(232, 674);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(968, 26);
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
            lblStatusSpring1.Size = new Size(358, 20);
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
            lblStatusSpring2.Size = new Size(358, 20);
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
            pnlContent.BackColor = Color.FromArgb(243, 246, 249);
            pnlContent.Controls.Add(dashboardLayout);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(232, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(968, 674);
            pnlContent.TabIndex = 2;
            // 
            // dashboardLayout
            // 
            dashboardLayout.AutoScroll = true;
            dashboardLayout.AutoScrollMinSize = new Size(960, 700);
            dashboardLayout.BackColor = Color.FromArgb(243, 246, 249);
            dashboardLayout.ColumnCount = 1;
            dashboardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            dashboardLayout.Controls.Add(headerLayout, 0, 0);
            dashboardLayout.Controls.Add(kpiLayout, 0, 1);
            dashboardLayout.Controls.Add(analyticsLayout, 0, 2);
            dashboardLayout.Controls.Add(bottomLayout, 0, 3);
            dashboardLayout.Controls.Add(lblStatus, 0, 4);
            dashboardLayout.Dock = DockStyle.Fill;
            dashboardLayout.Location = new Point(0, 0);
            dashboardLayout.Name = "dashboardLayout";
            dashboardLayout.Padding = new Padding(16);
            dashboardLayout.RowCount = 5;
            dashboardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            dashboardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            dashboardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            dashboardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            dashboardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            dashboardLayout.Size = new Size(968, 674);
            dashboardLayout.TabIndex = 0;
            // 
            // headerLayout
            // 
            headerLayout.ColumnCount = 3;
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            headerLayout.Controls.Add(lblTitle, 0, 0);
            headerLayout.Controls.Add(btnRefresh, 1, 0);
            headerLayout.Controls.Add(lblDate, 2, 0);
            headerLayout.Dock = DockStyle.Fill;
            headerLayout.Location = new Point(19, 19);
            headerLayout.Name = "headerLayout";
            headerLayout.RowCount = 1;
            headerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            headerLayout.Size = new Size(954, 62);
            headerLayout.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoEllipsis = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(608, 62);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Chào mừng trở lại!";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(0, 169, 130);
            btnRefresh.Dock = DockStyle.Fill;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(617, 12);
            btnRefresh.Margin = new Padding(3, 12, 3, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(104, 38);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += DashboardRefresh_Click;
            // 
            // lblDate
            // 
            lblDate.Dock = DockStyle.Fill;
            lblDate.Location = new Point(727, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(224, 62);
            lblDate.TabIndex = 2;
            lblDate.Text = "Ngày cập nhật";
            lblDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // kpiLayout
            // 
            kpiLayout.ColumnCount = 4;
            kpiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            kpiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            kpiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            kpiLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            kpiLayout.Controls.Add(pnlRevenue, 0, 0);
            kpiLayout.Controls.Add(pnlMonth, 1, 0);
            kpiLayout.Controls.Add(pnlTickets, 2, 0);
            kpiLayout.Controls.Add(pnlMovies, 3, 0);
            kpiLayout.Dock = DockStyle.Fill;
            kpiLayout.Location = new Point(19, 87);
            kpiLayout.Name = "kpiLayout";
            kpiLayout.RowCount = 1;
            kpiLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            kpiLayout.Size = new Size(954, 94);
            kpiLayout.TabIndex = 1;
            // 
            // pnlRevenue
            // 
            pnlRevenue.BackColor = Color.White;
            pnlRevenue.BorderStyle = BorderStyle.FixedSingle;
            pnlRevenue.Controls.Add(lblRevenue);
            pnlRevenue.Controls.Add(lblRevenueTitle);
            pnlRevenue.Dock = DockStyle.Fill;
            pnlRevenue.Location = new Point(4, 4);
            pnlRevenue.Margin = new Padding(4);
            pnlRevenue.Name = "pnlRevenue";
            pnlRevenue.Padding = new Padding(12);
            pnlRevenue.Size = new Size(230, 86);
            pnlRevenue.TabIndex = 0;
            // 
            // lblRevenue
            // 
            lblRevenue.Dock = DockStyle.Fill;
            lblRevenue.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblRevenue.Location = new Point(12, 35);
            lblRevenue.Name = "lblRevenue";
            lblRevenue.Size = new Size(204, 37);
            lblRevenue.TabIndex = 0;
            lblRevenue.Text = "0";
            lblRevenue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.Dock = DockStyle.Top;
            lblRevenueTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.DimGray;
            lblRevenueTitle.Location = new Point(12, 12);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(204, 23);
            lblRevenueTitle.TabIndex = 1;
            lblRevenueTitle.Text = "TỔNG DOANH THU";
            // 
            // pnlMonth
            // 
            pnlMonth.BackColor = Color.White;
            pnlMonth.BorderStyle = BorderStyle.FixedSingle;
            pnlMonth.Controls.Add(lblMonth);
            pnlMonth.Controls.Add(lblMonthTitle);
            pnlMonth.Dock = DockStyle.Fill;
            pnlMonth.Location = new Point(242, 4);
            pnlMonth.Margin = new Padding(4);
            pnlMonth.Name = "pnlMonth";
            pnlMonth.Padding = new Padding(12);
            pnlMonth.Size = new Size(230, 86);
            pnlMonth.TabIndex = 1;
            // 
            // lblMonth
            // 
            lblMonth.Dock = DockStyle.Fill;
            lblMonth.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblMonth.Location = new Point(12, 35);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(204, 37);
            lblMonth.TabIndex = 0;
            lblMonth.Text = "0";
            lblMonth.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMonthTitle
            // 
            lblMonthTitle.Dock = DockStyle.Top;
            lblMonthTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMonthTitle.ForeColor = Color.DimGray;
            lblMonthTitle.Location = new Point(12, 12);
            lblMonthTitle.Name = "lblMonthTitle";
            lblMonthTitle.Size = new Size(204, 23);
            lblMonthTitle.TabIndex = 1;
            lblMonthTitle.Text = "DOANH THU THÁNG NÀY";
            // 
            // pnlTickets
            // 
            pnlTickets.BackColor = Color.White;
            pnlTickets.BorderStyle = BorderStyle.FixedSingle;
            pnlTickets.Controls.Add(lblTickets);
            pnlTickets.Controls.Add(lblTicketsTitle);
            pnlTickets.Dock = DockStyle.Fill;
            pnlTickets.Location = new Point(480, 4);
            pnlTickets.Margin = new Padding(4);
            pnlTickets.Name = "pnlTickets";
            pnlTickets.Padding = new Padding(12);
            pnlTickets.Size = new Size(230, 86);
            pnlTickets.TabIndex = 2;
            // 
            // lblTickets
            // 
            lblTickets.Dock = DockStyle.Fill;
            lblTickets.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblTickets.Location = new Point(12, 35);
            lblTickets.Name = "lblTickets";
            lblTickets.Size = new Size(204, 37);
            lblTickets.TabIndex = 0;
            lblTickets.Text = "0";
            lblTickets.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTicketsTitle
            // 
            lblTicketsTitle.Dock = DockStyle.Top;
            lblTicketsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTicketsTitle.ForeColor = Color.DimGray;
            lblTicketsTitle.Location = new Point(12, 12);
            lblTicketsTitle.Name = "lblTicketsTitle";
            lblTicketsTitle.Size = new Size(204, 23);
            lblTicketsTitle.TabIndex = 1;
            lblTicketsTitle.Text = "TỔNG VÉ ĐÃ BÁN";
            // 
            // pnlMovies
            // 
            pnlMovies.BackColor = Color.White;
            pnlMovies.BorderStyle = BorderStyle.FixedSingle;
            pnlMovies.Controls.Add(lblMovies);
            pnlMovies.Controls.Add(lblMoviesTitle);
            pnlMovies.Dock = DockStyle.Fill;
            pnlMovies.Location = new Point(718, 4);
            pnlMovies.Margin = new Padding(4);
            pnlMovies.Name = "pnlMovies";
            pnlMovies.Padding = new Padding(12);
            pnlMovies.Size = new Size(232, 86);
            pnlMovies.TabIndex = 3;
            // 
            // lblMovies
            // 
            lblMovies.Dock = DockStyle.Fill;
            lblMovies.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblMovies.Location = new Point(12, 35);
            lblMovies.Name = "lblMovies";
            lblMovies.Size = new Size(206, 37);
            lblMovies.TabIndex = 0;
            lblMovies.Text = "0";
            lblMovies.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMoviesTitle
            // 
            lblMoviesTitle.Dock = DockStyle.Top;
            lblMoviesTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMoviesTitle.ForeColor = Color.DimGray;
            lblMoviesTitle.Location = new Point(12, 12);
            lblMoviesTitle.Name = "lblMoviesTitle";
            lblMoviesTitle.Size = new Size(206, 23);
            lblMoviesTitle.TabIndex = 1;
            lblMoviesTitle.Text = "PHIM ĐANG CHIẾU";
            // 
            // analyticsLayout
            // 
            analyticsLayout.ColumnCount = 3;
            analyticsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            analyticsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            analyticsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            analyticsLayout.Controls.Add(pnlchartWeekly, 0, 0);
            analyticsLayout.Controls.Add(pnlchartMovieRevenue, 1, 0);
            analyticsLayout.Controls.Add(pnlNowShowing, 2, 0);
            analyticsLayout.Dock = DockStyle.Fill;
            analyticsLayout.Location = new Point(19, 187);
            analyticsLayout.Name = "analyticsLayout";
            analyticsLayout.RowCount = 1;
            analyticsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            analyticsLayout.Size = new Size(954, 257);
            analyticsLayout.TabIndex = 2;
            // 
            // pnlchartWeekly
            // 
            pnlchartWeekly.BackColor = Color.White;
            pnlchartWeekly.BorderStyle = BorderStyle.FixedSingle;
            pnlchartWeekly.Controls.Add(chartWeekly);
            pnlchartWeekly.Controls.Add(pnlchartWeeklyTitle);
            pnlchartWeekly.Dock = DockStyle.Fill;
            pnlchartWeekly.Location = new Point(4, 4);
            pnlchartWeekly.Margin = new Padding(4);
            pnlchartWeekly.Name = "pnlchartWeekly";
            pnlchartWeekly.Padding = new Padding(10);
            pnlchartWeekly.Size = new Size(392, 249);
            pnlchartWeekly.TabIndex = 0;
            // 
            // chartWeekly
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea1.AxisY.LabelStyle.Format = "#,##0";
            chartArea1.AxisY.MajorGrid.LineColor = Color.FromArgb(232, 237, 241);
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "Main";
            chartWeekly.ChartAreas.Add(chartArea1);
            chartWeekly.Dock = DockStyle.Fill;
            chartWeekly.Location = new Point(10, 48);
            chartWeekly.Name = "chartWeekly";
            series1.ChartArea = "Main";
            series1.Color = Color.FromArgb(0, 169, 130);
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.LabelFormat = "#,##0";
            series1.Name = "DoanhThu";
            chartWeekly.Series.Add(series1);
            chartWeekly.Size = new Size(370, 189);
            chartWeekly.TabIndex = 0;
            // 
            // pnlchartWeeklyTitle
            // 
            pnlchartWeeklyTitle.Dock = DockStyle.Top;
            pnlchartWeeklyTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pnlchartWeeklyTitle.Location = new Point(10, 10);
            pnlchartWeeklyTitle.Name = "pnlchartWeeklyTitle";
            pnlchartWeeklyTitle.Size = new Size(370, 38);
            pnlchartWeeklyTitle.TabIndex = 1;
            pnlchartWeeklyTitle.Text = "Doanh thu 7 ngày qua (VNĐ)";
            pnlchartWeeklyTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlchartMovieRevenue
            // 
            pnlchartMovieRevenue.BackColor = Color.White;
            pnlchartMovieRevenue.BorderStyle = BorderStyle.FixedSingle;
            pnlchartMovieRevenue.Controls.Add(chartMovieRevenue);
            pnlchartMovieRevenue.Controls.Add(pnlchartMovieRevenueTitle);
            pnlchartMovieRevenue.Dock = DockStyle.Fill;
            pnlchartMovieRevenue.Location = new Point(404, 4);
            pnlchartMovieRevenue.Margin = new Padding(4);
            pnlchartMovieRevenue.Name = "pnlchartMovieRevenue";
            pnlchartMovieRevenue.Padding = new Padding(10);
            pnlchartMovieRevenue.Size = new Size(306, 249);
            pnlchartMovieRevenue.TabIndex = 1;
            // 
            // chartMovieRevenue
            // 
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisX.IsReversed = true;
            chartArea2.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Enabled = false;
            chartArea2.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea2.AxisY.MajorGrid.LineColor = Color.FromArgb(232, 237, 241);
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "Main";
            chartMovieRevenue.ChartAreas.Add(chartArea2);
            chartMovieRevenue.Dock = DockStyle.Fill;
            chartMovieRevenue.Location = new Point(10, 48);
            chartMovieRevenue.Name = "chartMovieRevenue";
            series2.ChartArea = "Main";
            series2.ChartType = SeriesChartType.Bar;
            series2.Color = Color.FromArgb(89, 130, 220);
            series2.IsValueShownAsLabel = true;
            series2.IsVisibleInLegend = false;
            series2.IsXValueIndexed = true;
            series2.LabelFormat = "#,##0";
            series2.Name = "DoanhThuPhim";
            chartMovieRevenue.Series.Add(series2);
            chartMovieRevenue.Size = new Size(284, 189);
            chartMovieRevenue.TabIndex = 0;
            // 
            // pnlchartMovieRevenueTitle
            // 
            pnlchartMovieRevenueTitle.Dock = DockStyle.Top;
            pnlchartMovieRevenueTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pnlchartMovieRevenueTitle.Location = new Point(10, 10);
            pnlchartMovieRevenueTitle.Name = "pnlchartMovieRevenueTitle";
            pnlchartMovieRevenueTitle.Size = new Size(284, 38);
            pnlchartMovieRevenueTitle.TabIndex = 1;
            pnlchartMovieRevenueTitle.Text = "Top doanh thu vé phim (trước giảm giá)";
            pnlchartMovieRevenueTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlNowShowing
            // 
            pnlNowShowing.BackColor = Color.White;
            pnlNowShowing.BorderStyle = BorderStyle.FixedSingle;
            pnlNowShowing.Controls.Add(dgvNowShowing);
            pnlNowShowing.Controls.Add(pnlNowShowingTitle);
            pnlNowShowing.Dock = DockStyle.Fill;
            pnlNowShowing.Location = new Point(718, 4);
            pnlNowShowing.Margin = new Padding(4);
            pnlNowShowing.Name = "pnlNowShowing";
            pnlNowShowing.Padding = new Padding(10);
            pnlNowShowing.Size = new Size(232, 249);
            pnlNowShowing.TabIndex = 2;
            // 
            // dgvNowShowing
            // 
            dgvNowShowing.AllowUserToAddRows = false;
            dgvNowShowing.AllowUserToDeleteRows = false;
            dgvNowShowing.AllowUserToResizeRows = false;
            dgvNowShowing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNowShowing.BackgroundColor = Color.White;
            dgvNowShowing.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 244, 241);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvNowShowing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvNowShowing.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 242, 235);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvNowShowing.DefaultCellStyle = dataGridViewCellStyle2;
            dgvNowShowing.Dock = DockStyle.Fill;
            dgvNowShowing.EnableHeadersVisualStyles = false;
            dgvNowShowing.Location = new Point(10, 48);
            dgvNowShowing.MultiSelect = false;
            dgvNowShowing.Name = "dgvNowShowing";
            dgvNowShowing.ReadOnly = true;
            dgvNowShowing.RowHeadersVisible = false;
            dgvNowShowing.RowHeadersWidth = 51;
            dgvNowShowing.RowTemplate.Height = 30;
            dgvNowShowing.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNowShowing.Size = new Size(210, 189);
            dgvNowShowing.TabIndex = 0;
            // 
            // pnlNowShowingTitle
            // 
            pnlNowShowingTitle.Dock = DockStyle.Top;
            pnlNowShowingTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pnlNowShowingTitle.Location = new Point(10, 10);
            pnlNowShowingTitle.Name = "pnlNowShowingTitle";
            pnlNowShowingTitle.Size = new Size(210, 38);
            pnlNowShowingTitle.TabIndex = 1;
            pnlNowShowingTitle.Text = "Phim đang chiếu • 7 ngày tới";
            pnlNowShowingTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // bottomLayout
            // 
            bottomLayout.ColumnCount = 2;
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            bottomLayout.Controls.Add(pnlShowtimes, 0, 0);
            bottomLayout.Controls.Add(pnlTopMovies, 1, 0);
            bottomLayout.Dock = DockStyle.Fill;
            bottomLayout.Location = new Point(19, 450);
            bottomLayout.Name = "bottomLayout";
            bottomLayout.RowCount = 1;
            bottomLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bottomLayout.Size = new Size(954, 236);
            bottomLayout.TabIndex = 3;
            // 
            // pnlShowtimes
            // 
            pnlShowtimes.BackColor = Color.White;
            pnlShowtimes.BorderStyle = BorderStyle.FixedSingle;
            pnlShowtimes.Controls.Add(dgvShowtimes);
            pnlShowtimes.Controls.Add(pnlShowtimesTitle);
            pnlShowtimes.Dock = DockStyle.Fill;
            pnlShowtimes.Location = new Point(4, 4);
            pnlShowtimes.Margin = new Padding(4);
            pnlShowtimes.Name = "pnlShowtimes";
            pnlShowtimes.Padding = new Padding(10);
            pnlShowtimes.Size = new Size(516, 228);
            pnlShowtimes.TabIndex = 0;
            // 
            // dgvShowtimes
            // 
            dgvShowtimes.AllowUserToAddRows = false;
            dgvShowtimes.AllowUserToDeleteRows = false;
            dgvShowtimes.AllowUserToResizeRows = false;
            dgvShowtimes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShowtimes.BackgroundColor = Color.White;
            dgvShowtimes.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(235, 244, 241);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvShowtimes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvShowtimes.ColumnHeadersHeight = 32;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(220, 242, 235);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvShowtimes.DefaultCellStyle = dataGridViewCellStyle4;
            dgvShowtimes.Dock = DockStyle.Fill;
            dgvShowtimes.EnableHeadersVisualStyles = false;
            dgvShowtimes.Location = new Point(10, 48);
            dgvShowtimes.MultiSelect = false;
            dgvShowtimes.Name = "dgvShowtimes";
            dgvShowtimes.ReadOnly = true;
            dgvShowtimes.RowHeadersVisible = false;
            dgvShowtimes.RowHeadersWidth = 51;
            dgvShowtimes.RowTemplate.Height = 30;
            dgvShowtimes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShowtimes.Size = new Size(494, 168);
            dgvShowtimes.TabIndex = 0;
            // 
            // pnlShowtimesTitle
            // 
            pnlShowtimesTitle.Dock = DockStyle.Top;
            pnlShowtimesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pnlShowtimesTitle.Location = new Point(10, 10);
            pnlShowtimesTitle.Name = "pnlShowtimesTitle";
            pnlShowtimesTitle.Size = new Size(494, 38);
            pnlShowtimesTitle.TabIndex = 1;
            pnlShowtimesTitle.Text = "Suất chiếu sắp tới • 7 ngày";
            pnlShowtimesTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTopMovies
            // 
            pnlTopMovies.BackColor = Color.White;
            pnlTopMovies.BorderStyle = BorderStyle.FixedSingle;
            pnlTopMovies.Controls.Add(dgvTopMovies);
            pnlTopMovies.Controls.Add(pnlTopMoviesTitle);
            pnlTopMovies.Dock = DockStyle.Fill;
            pnlTopMovies.Location = new Point(528, 4);
            pnlTopMovies.Margin = new Padding(4);
            pnlTopMovies.Name = "pnlTopMovies";
            pnlTopMovies.Padding = new Padding(10);
            pnlTopMovies.Size = new Size(422, 228);
            pnlTopMovies.TabIndex = 1;
            // 
            // dgvTopMovies
            // 
            dgvTopMovies.AllowUserToAddRows = false;
            dgvTopMovies.AllowUserToDeleteRows = false;
            dgvTopMovies.AllowUserToResizeRows = false;
            dgvTopMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopMovies.BackgroundColor = Color.White;
            dgvTopMovies.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(235, 244, 241);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvTopMovies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvTopMovies.ColumnHeadersHeight = 32;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(220, 242, 235);
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvTopMovies.DefaultCellStyle = dataGridViewCellStyle6;
            dgvTopMovies.Dock = DockStyle.Fill;
            dgvTopMovies.EnableHeadersVisualStyles = false;
            dgvTopMovies.Location = new Point(10, 48);
            dgvTopMovies.MultiSelect = false;
            dgvTopMovies.Name = "dgvTopMovies";
            dgvTopMovies.ReadOnly = true;
            dgvTopMovies.RowHeadersVisible = false;
            dgvTopMovies.RowHeadersWidth = 51;
            dgvTopMovies.RowTemplate.Height = 30;
            dgvTopMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopMovies.Size = new Size(400, 168);
            dgvTopMovies.TabIndex = 0;
            // 
            // pnlTopMoviesTitle
            // 
            pnlTopMoviesTitle.Dock = DockStyle.Top;
            pnlTopMoviesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pnlTopMoviesTitle.Location = new Point(10, 10);
            pnlTopMoviesTitle.Name = "pnlTopMoviesTitle";
            pnlTopMoviesTitle.Size = new Size(400, 38);
            pnlTopMoviesTitle.TabIndex = 1;
            pnlTopMoviesTitle.Text = "Top 10 phim bán chạy nhất theo số vé";
            pnlTopMoviesTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.Location = new Point(19, 689);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(954, 27);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Chỉ tính hóa đơn đã thanh toán.";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1200, 700);
            Controls.Add(pnlContent);
            Controls.Add(statusStripMain);
            Controls.Add(sidebarPanel);
            Font = new Font("Segoe UI", 9.5F);
            MainMenuStrip = menuStripMain;
            MinimumSize = new Size(1000, 600);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM - BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            sidebarPanel.ResumeLayout(false);
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            systemMenu.ResumeLayout(false);
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            pnlContent.ResumeLayout(false);
            dashboardLayout.ResumeLayout(false);
            headerLayout.ResumeLayout(false);
            kpiLayout.ResumeLayout(false);
            pnlRevenue.ResumeLayout(false);
            pnlMonth.ResumeLayout(false);
            pnlTickets.ResumeLayout(false);
            pnlMovies.ResumeLayout(false);
            analyticsLayout.ResumeLayout(false);
            pnlchartWeekly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartWeekly).EndInit();
            pnlchartMovieRevenue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartMovieRevenue).EndInit();
            pnlNowShowing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNowShowing).EndInit();
            bottomLayout.ResumeLayout(false);
            pnlShowtimes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvShowtimes).EndInit();
            pnlTopMovies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTopMovies).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Button btnSystem;
        private System.Windows.Forms.ContextMenuStrip systemMenu;
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
        private TableLayoutPanel dashboardLayout;
        private TableLayoutPanel headerLayout;
        private Label lblTitle;
        private Button btnRefresh;
        private Label lblDate;
        private TableLayoutPanel kpiLayout;
        private Panel pnlRevenue;
        private Label lblRevenue;
        private Label lblRevenueTitle;
        private Panel pnlMonth;
        private Label lblMonth;
        private Label lblMonthTitle;
        private Panel pnlTickets;
        private Label lblTickets;
        private Label lblTicketsTitle;
        private Panel pnlMovies;
        private Label lblMovies;
        private Label lblMoviesTitle;
        private TableLayoutPanel analyticsLayout;
        private TableLayoutPanel bottomLayout;
        private Chart chartWeekly;
        private Panel pnlchartWeekly;
        private Label pnlchartWeeklyTitle;
        private Chart chartMovieRevenue;
        private Panel pnlchartMovieRevenue;
        private Label pnlchartMovieRevenueTitle;
        private DataGridView dgvNowShowing;
        private Panel pnlNowShowing;
        private Label pnlNowShowingTitle;
        private DataGridView dgvShowtimes;
        private Panel pnlShowtimes;
        private Label pnlShowtimesTitle;
        private DataGridView dgvTopMovies;
        private Panel pnlTopMovies;
        private Label pnlTopMoviesTitle;
        private Label lblStatus;
    }
}

