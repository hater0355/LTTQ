using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;
using HeThongRapChieuPhim.UserControls;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormMain : Form
    {
        // UserControls
        private ucChonPhim? _ucChonPhim;
        private ucHoaDon? _ucHoaDon;
        private ucTaiKhoanThuNgan? _ucTaiKhoanThuNgan;

        // Individual category & settings screens
        private ucPhim? _ucPhim;
        private ucPhongChieu? _ucPhongChieu;
        private ucNhanVien? _ucNhanVien;
        private ucLichLamViec? _ucLichLamViec;
        private ucDoAn? _ucDoAn;
        private ucCaiSuatChieu? _ucCaiSuatChieu;
        private ucCaiUuDai? _ucCaiUuDai;
        private ucCaiVoucher? _ucCaiVoucher;
        private bool _sidebarCollapsed;
        private readonly Dictionary<ToolStripMenuItem, bool> _sidebarRoleVisibility = new();

        public FormMain()
        {
            InitializeComponent();
            InitializeSidebarBehavior();
        }

        private sealed record SidebarItemLayout(string Text, Size Size, Padding Padding,
            ContentAlignment TextAlign, bool AutoSize);
        private readonly Dictionary<ToolStripItem, SidebarItemLayout> _expandedItems = new();
        private int _expandedSidebarWidth;
        private Padding _expandedMenuPadding;
        private string _expandedSystemText = "";

        private void InitializeSidebarBehavior()
        {
            _expandedSidebarWidth = sidebarPanel.Width;
            _expandedMenuPadding = menuStripMain.Padding;
            _expandedSystemText = btnSystem.Text;
            foreach (ToolStripItem item in menuStripMain.Items)
                _expandedItems[item] = new(item.Text ?? "", item.Size, item.Padding, item.TextAlign, item.AutoSize);
            _menuHome.Click += (_, _) => ShowDashboard();
        }

        private void SetSidebarCollapsed(bool collapsed)
        {
            _sidebarCollapsed = collapsed;
            sidebarPanel.Width = collapsed ? 48 : _expandedSidebarWidth;
            menuStripMain.Padding = collapsed ? new Padding(4) : _expandedMenuPadding;
            btnSystem.Text = collapsed ? "⚙" : _expandedSystemText;
            _sidebarToggle.ToolTipText = collapsed ? "Mở rộng thanh điều hướng" : "Thu gọn thanh điều hướng";
            foreach (var pair in _expandedItems)
            {
                ToolStripItem item = pair.Key;
                SidebarItemLayout layout = pair.Value;
                item.AutoSize = collapsed ? false : layout.AutoSize;
                item.Size = collapsed ? new Size(40, layout.Size.Height) : layout.Size;
                item.Padding = collapsed ? Padding.Empty : layout.Padding;
                item.TextAlign = collapsed ? ContentAlignment.MiddleCenter : layout.TextAlign;
                item.Text = collapsed && item == _sidebarToggle ? "☰" : layout.Text;
                if (item == _sidebarToggle) item.Visible = true;
                else if (item == _menuHome) item.Visible = !collapsed;
                else if (item is ToolStripMenuItem menuItem)
                    item.Visible = !collapsed && (!_sidebarRoleVisibility.TryGetValue(menuItem, out bool allowed) || allowed);
            }
        }
        private void FormMain_Load(object? sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignTime(this)) return;

            if (!string.IsNullOrEmpty(CurrentUser.HoTen))
            {
                lblStatusUser.Text = $"Người dùng: {CurrentUser.HoTen} ({AccessRoles.DisplayName(CurrentUser.VaiTro)})";
            }
            else
            {
                lblStatusUser.Text = "Người dùng: Chưa đăng nhập";
            }

            ApplyRolePermissions();
            timerClock.Start();
            UpdateClock();

            // Default screen based on role
            if (CurrentUser.IsSuperAdmin) ShowDashboard();
            else if (CurrentUser.IsFilmAdmin) ShowPhim();
            else if (CurrentUser.IsHumanResourcesAdmin) ShowNhanVien();
            else if (CurrentUser.IsFoodAndPromotionsAdmin) ShowDoAn();
            else if (CurrentUser.IsRevenueAdmin) ShowDashboard();
            else ShowBanHang();
        }

        private void ApplyRolePermissions()
        {
            bool isSuper = CurrentUser.IsSuperAdmin;

            // Phim, phòng chiếu, suất chiếu (Quản trị phim hoặc Admin tổng)
            menuPhim.Visible = isSuper || CurrentUser.IsFilmAdmin;
            menuPhongChieu.Visible = isSuper || CurrentUser.IsFilmAdmin;
            menuSuatChieu.Visible = isSuper || CurrentUser.IsFilmAdmin;

            // Bán vé, hóa đơn (Thu ngân, Doanh thu hoặc Admin tổng)
            menuBanHang.Visible = isSuper || CurrentUser.IsCashier;
            menuHoaDon.Visible = isSuper || CurrentUser.IsCashier || CurrentUser.IsRevenueAdmin;

            // Đồ ăn, combo, voucher (Quản trị đồ ăn & ưu đãi hoặc Admin tổng)
            menuDoAn.Visible = isSuper || CurrentUser.IsFoodAndPromotionsAdmin;
            menuCombo.Visible = isSuper || CurrentUser.IsFoodAndPromotionsAdmin;
            menuVoucher.Visible = isSuper || CurrentUser.IsFoodAndPromotionsAdmin;

            // Nhân sự & tài khoản (Admin nhân sự hoặc Admin tổng)
            menuNhanVien.Visible = isSuper || CurrentUser.IsHumanResourcesAdmin;
            menuTaiKhoanThuNgan.Visible = isSuper || CurrentUser.IsHumanResourcesAdmin;

            // Lịch ca: HR/Admin tổng quản lý; thu ngân chỉ xem lịch cá nhân.
            menuLichLamViec.Visible = isSuper || CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsCashier;

            // Báo cáo doanh thu (Admin doanh thu hoặc Admin tổng)
            menuDoanhThu.Visible = isSuper || CurrentUser.IsRevenueAdmin;

            foreach (ToolStripItem item in menuStripMain.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                    _sidebarRoleVisibility[menuItem] = menuItem.Visible;
            }

            if (_sidebarCollapsed)
                SetSidebarCollapsed(true);
        }

        private void TimerClock_Tick(object? sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblStatusClock.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void SwitchView(UserControl uc, string title)
        {
            lblStatusView.Text = $"Chức năng: {title}";
            pnlContent.SuspendLayout();
            dashboardLayout.Visible = false;
            foreach (Control view in pnlContent.Controls.Cast<Control>().ToArray())
                if (view != dashboardLayout) pnlContent.Controls.Remove(view);
            uc.Dock = DockStyle.Fill;
            uc.AutoScroll = true;
            uc.AutoScrollMinSize = new Size(1100, 720);
            pnlContent.Controls.Add(uc);
            uc.BringToFront();
            pnlContent.ResumeLayout();
        }

        public void ShowDashboard()
        {
            if (!RequireRole(CurrentUser.IsRevenueAdmin || CurrentUser.IsSuperAdmin, "Admin Doanh thu hoặc Admin tổng")) return;
            lblStatusView.Text = "Chức năng: Báo cáo doanh thu tổng quan";
            pnlContent.SuspendLayout();
            foreach (Control view in pnlContent.Controls.Cast<Control>().ToArray())
                if (view != dashboardLayout) pnlContent.Controls.Remove(view);
            dashboardLayout.Visible = true;
            dashboardLayout.BringToFront();
            pnlContent.ResumeLayout();
            LoadDashboardData();
        }

        public void ShowPhim()
        {
            if (!RequireRole(CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin, "Admin Phim & Rạp hoặc Admin tổng")) return;
            _ucPhim ??= new ucPhim();
            SwitchView(_ucPhim, "Danh mục phim chiếu rạp");
        }

        public void ShowPhongChieu()
        {
            if (!RequireRole(CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin, "Admin Phim & Rạp hoặc Admin tổng")) return;
            _ucPhongChieu ??= new ucPhongChieu();
            SwitchView(_ucPhongChieu, "Danh mục phòng chiếu");
        }

        public void ShowNhanVien()
        {
            if (!RequireRole(CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin, "Admin Nhân sự hoặc Admin tổng")) return;
            _ucNhanVien ??= new ucNhanVien();
            SwitchView(_ucNhanVien, "Danh mục nhân viên");
        }

        public void ShowLichLamViec()
        {
            if (!RequireRole(CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin || CurrentUser.IsCashier,
                    "Thu ngân, Admin Nhân sự hoặc Admin tổng")) return;
            _ucLichLamViec ??= new ucLichLamViec();
            SwitchView(_ucLichLamViec, "Lịch làm việc & thay ca");
        }

        public void ShowDoAn()
        {
            if (!RequireRole(CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin, "Admin Đồ ăn & Ưu đãi hoặc Admin tổng")) return;
            _ucDoAn ??= new ucDoAn();
            SwitchView(_ucDoAn, "Danh mục đồ ăn & nước uống");
        }

        public void ShowSuatChieu()
        {
            if (!RequireRole(CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin, "Admin Phim & Rạp hoặc Admin tổng")) return;
            _ucCaiSuatChieu ??= new ucCaiSuatChieu();
            SwitchView(_ucCaiSuatChieu, "Cài đặt lịch chiếu & suất chiếu");
        }

        public void ShowCombo()
        {
            if (!RequireRole(CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin, "Admin Đồ ăn & Ưu đãi hoặc Admin tổng")) return;
            _ucCaiUuDai ??= new ucCaiUuDai();
            SwitchView(_ucCaiUuDai, "Cài đặt ưu đãi combo");
        }

        public void ShowVoucher()
        {
            if (!RequireRole(CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin, "Admin Đồ ăn & Ưu đãi hoặc Admin tổng")) return;
            _ucCaiVoucher ??= new ucCaiVoucher();
            SwitchView(_ucCaiVoucher, "Cài đặt Voucher");
        }

        public void ShowBanHang()
        {
            if (!CurrentUser.IsCashier && !CurrentUser.IsSuperAdmin) return;
            if (_ucChonPhim is null)
            {
                _ucChonPhim = new ucChonPhim();
                _ucChonPhim.MovieSelected += ShowBookingForMovie;
            }
            SwitchView(_ucChonPhim, "Chọn phim mua vé");
            _ucChonPhim.LoadMovies();
        }

        private void ShowBookingForMovie(string movieId)
        {
            if (!CurrentUser.IsCashier && !CurrentUser.IsSuperAdmin) return;
            using var window = new FormBooking();
            var booking = window.Booking;
            if (!booking.SelectMovie(movieId))
            {
                MessageBox.Show("Phim này không còn suất chiếu khả dụng. Vui lòng chọn phim khác.", "Chọn phim",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            window.ShowDialog(this);
            _ucChonPhim?.LoadMovies();
        }
        public void ShowHoaDon()
        {
            if (!CurrentUser.IsCashier && !CurrentUser.IsRevenueAdmin && !CurrentUser.IsSuperAdmin) return;
            _ucHoaDon ??= new ucHoaDon();
            SwitchView(_ucHoaDon, "Quản lý & tra cứu hóa đơn");
            _ucHoaDon.LoadHoaDon();
        }

        public void ShowTaiKhoanThuNgan()
        {
            if (!RequireRole(CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin, "Admin Nhân sự hoặc Admin tổng")) return;
            _ucTaiKhoanThuNgan ??= new ucTaiKhoanThuNgan();
            SwitchView(_ucTaiKhoanThuNgan, "Quản lý tài khoản");
        }

        public void ShowDanhMuc(int tabIndex = 0)
        {
            switch (tabIndex)
            {
                case 0: ShowNhanVien(); break;
                case 1: ShowPhim(); break;
                case 2: ShowDoAn(); break;
                case 3: ShowPhongChieu(); break;
                default: ShowPhim(); break;
            }
        }

        public void ShowCaiDat(int tabIndex = 0)
        {
            switch (tabIndex)
            {
                case 0: ShowSuatChieu(); break;
                case 1: ShowCombo(); break;
                case 2: ShowVoucher(); break;
                default: ShowSuatChieu(); break;
            }
        }

        private bool RequireRole(bool allowed, string requiredRole)
        {
            if (allowed) return true;
            MessageBox.Show($"Chức năng này chỉ dành cho {requiredRole}.", "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void MenuDoiMatKhau_Click(object? sender, EventArgs e)
        {
            using var dlg = new FormDoiMatKhau();
            dlg.ShowDialog(this);
        }

        private void MenuDangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PaymentsApiClient.ClearSession();
                CurrentUser.Clear();
                Close();
            }
        }

        private void MenuThoat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MenuPhim_Click(object? sender, EventArgs e) => ShowPhim();
        private void MenuPhongChieu_Click(object? sender, EventArgs e) => ShowPhongChieu();
        private void MenuSuatChieu_Click(object? sender, EventArgs e) => ShowSuatChieu();
        private void MenuBanHang_Click(object? sender, EventArgs e) => ShowBanHang();
        private void MenuHoaDon_Click(object? sender, EventArgs e) => ShowHoaDon();
        private void MenuDoAn_Click(object? sender, EventArgs e) => ShowDoAn();
        private void MenuCombo_Click(object? sender, EventArgs e) => ShowCombo();
        private void MenuVoucher_Click(object? sender, EventArgs e) => ShowVoucher();
        private void MenuNhanVien_Click(object? sender, EventArgs e) => ShowNhanVien();
        private void MenuLichLamViec_Click(object? sender, EventArgs e) => ShowLichLamViec();
        private void MenuTaiKhoanThuNgan_Click(object? sender, EventArgs e) => ShowTaiKhoanThuNgan();
        private void MenuDoanhThu_Click(object? sender, EventArgs e) => ShowDashboard();

        private void SystemButton_Click(object? sender, EventArgs e)
        {
            systemMenu.Show(btnSystem, new Point(0, 0), ToolStripDropDownDirection.AboveRight);
        }

        private void _sidebarToggle_Click(object sender, EventArgs e) => SetSidebarCollapsed(!_sidebarCollapsed);
    }
}

