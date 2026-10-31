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
        private ucDashboard? _ucDashboard;
        private ucBanHang? _ucBanHang;
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
            ApplyAppearance();
        }

        private void ApplyAppearance()
        {
            var teal = Color.FromArgb(0, 154, 130);
            var tealDark = Color.FromArgb(0, 112, 98);
            menuStripMain.Dock = DockStyle.Left;
            menuStripMain.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            menuStripMain.AutoSize = false;
            menuStripMain.Width = 232;
            menuStripMain.CanOverflow = false;
            menuStripMain.GripStyle = ToolStripGripStyle.Hidden;
            menuStripMain.BackColor = teal;
            menuStripMain.ForeColor = Color.White;
            menuStripMain.Padding = new Padding(8);

            _sidebarToggle.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _sidebarToggle.Size = new Size(216, 42);
            _sidebarToggle.Text = "☰    CinemaManager";
            _sidebarToggle.ForeColor = Color.White;
            _sidebarToggle.BackColor = Color.FromArgb(5, 48, 45);
            _sidebarToggle.TextAlign = ContentAlignment.MiddleLeft;
            _sidebarToggle.Padding = new Padding(10, 8, 8, 8);
            _sidebarToggle.Click += (_, _) => SetSidebarCollapsed(!_sidebarCollapsed);

            _menuHome.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _menuHome.Size = new Size(216, 42);
            _menuHome.Text = "⌂  Trang chủ";
            _menuHome.ForeColor = Color.White;
            _menuHome.BackColor = Color.FromArgb(0, 112, 98);
            _menuHome.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            _menuHome.TextAlign = ContentAlignment.MiddleLeft;
            _menuHome.Padding = new Padding(10, 8, 8, 8);
            _menuHome.Click += (_, _) => ShowDashboard();

            var iconMap = new Dictionary<ToolStripMenuItem, string>
            {
                [menuHeThong] = "⚙", [menuPhim] = "▣", [menuPhongChieu] = "▦", [menuSuatChieu] = "◷",
                [menuBanHang] = "▤", [menuHoaDon] = "▧", [menuDoAn] = "♨", [menuCombo] = "✦",
                [menuVoucher] = "%", [menuNhanVien] = "♙", [menuLichLamViec] = "▣",
                [menuTaiKhoanThuNgan] = "♟", [menuDoanhThu] = "▥"
            };
            foreach (ToolStripItem item in menuStripMain.Items)
            {
                item.ForeColor = Color.White;
                item.BackColor = Color.FromArgb(5, 48, 45);
                item.AutoSize = false;
                item.Size = new Size(216, 42);
                item.Overflow = ToolStripItemOverflow.Never;
                item.TextAlign = ContentAlignment.MiddleLeft;
                item.Padding = new Padding(10, 8, 8, 8);
                if (item is ToolStripDropDownItem dropDown)
                    StyleDropDownItems(dropDown);
            }

            foreach (var pair in iconMap)
            {
                string label = (pair.Key.Text ?? string.Empty).Trim();
                if (label.StartsWith(pair.Value, StringComparison.Ordinal))
                    label = label[pair.Value.Length..].Trim();
                const string escapedAmpersand = "\u0001";
                label = label.Replace("&&", escapedAmpersand).Replace("&", "").Replace(escapedAmpersand, "&");
                pair.Key.Tag = (label, pair.Value);
                pair.Key.Text = $"{pair.Value}   {EscapeMenuAmpersands(label)}";
                pair.Key.Font = new Font("Segoe UI", 9.5F);
            }

            menuStripMain.Renderer = new ToolStripProfessionalRenderer(new AppMenuColorTable(teal, tealDark));
            SetSidebarCollapsed(false);
            statusStripMain.BackColor = Color.White;
            statusStripMain.ForeColor = Color.FromArgb(83, 96, 107);
            statusStripMain.SizingGrip = false;
            pnlContent.BackColor = Color.FromArgb(243, 246, 249);
            BackColor = Color.FromArgb(243, 246, 249);
        }

        private void SetSidebarCollapsed(bool collapsed)
        {
            _sidebarCollapsed = collapsed;
            int width = collapsed ? 48 : 232;
            int itemWidth = collapsed ? width - 8 : width - 16;
            menuStripMain.Width = width;
            menuStripMain.Padding = collapsed ? new Padding(4) : new Padding(8);
            _sidebarToggle.Size = new Size(itemWidth, 42);
            _sidebarToggle.Text = collapsed ? "☰" : "☰    CinemaManager";
            _sidebarToggle.ToolTipText = collapsed ? "Mở rộng thanh điều hướng" : "Thu gọn thanh điều hướng";
            _sidebarToggle.TextAlign = collapsed ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
            _sidebarToggle.Padding = collapsed ? new Padding(0) : new Padding(10, 8, 8, 8);
            _menuHome.Size = new Size(itemWidth, 42);
            _menuHome.Text = collapsed ? "⌂" : "⌂  Trang chủ";
            _menuHome.TextAlign = collapsed ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;

            foreach (ToolStripItem item in menuStripMain.Items)
            {
                item.Size = new Size(itemWidth, 42);
                item.Padding = collapsed ? new Padding(0) : new Padding(10, 8, 8, 8);
                if (item == _sidebarToggle)
                {
                    item.Visible = true;
                    continue;
                }
                if (item == _menuHome)
                {
                    item.Visible = !collapsed;
                    continue;
                }
                if (item is not ToolStripMenuItem menuItem || item.Tag is not ValueTuple<string, string> value)
                    continue;

                if (collapsed)
                    menuItem.Visible = false;
                else if (_sidebarRoleVisibility.TryGetValue(menuItem, out bool roleAllowsItem))
                    menuItem.Visible = roleAllowsItem;
                else
                    menuItem.Visible = true;

                menuItem.Text = collapsed ? value.Item2 : $"{value.Item2}   {EscapeMenuAmpersands(value.Item1)}";
                menuItem.ToolTipText = collapsed ? value.Item1 : string.Empty;
                menuItem.TextAlign = collapsed ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
            }
        }

        private static string EscapeMenuAmpersands(string label) => label.Replace("&", "&&");

        private static void StyleDropDownItems(ToolStripDropDownItem parent)
        {
            foreach (ToolStripItem item in parent.DropDownItems)
            {
                item.ForeColor = Color.FromArgb(45, 57, 67);
                item.BackColor = Color.White;
                if (item is ToolStripDropDownItem child)
                    StyleDropDownItems(child);
            }
        }

        private sealed class AppMenuColorTable : ProfessionalColorTable
        {
            private readonly Color _baseColor;
            private readonly Color _selectedColor;

            public AppMenuColorTable(Color baseColor, Color selectedColor)
            {
                _baseColor = baseColor;
                _selectedColor = selectedColor;
                UseSystemColors = false;
            }

            public override Color MenuStripGradientBegin => _baseColor;
            public override Color MenuStripGradientEnd => _baseColor;
            public override Color ToolStripDropDownBackground => Color.White;
            public override Color MenuItemSelected => Color.FromArgb(224, 244, 239);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(224, 244, 239);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(224, 244, 239);
            public override Color MenuItemPressedGradientBegin => _selectedColor;
            public override Color MenuItemPressedGradientEnd => _selectedColor;
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color SeparatorDark => Color.FromArgb(230, 235, 239);
            public override Color SeparatorLight => Color.White;
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
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            uc.AutoScroll = true;
            uc.AutoScrollMinSize = new Size(1100, 720);
            pnlContent.Controls.Add(uc);
            pnlContent.ResumeLayout();
        }

        public void ShowDashboard()
        {
            if (!RequireRole(CurrentUser.IsRevenueAdmin || CurrentUser.IsSuperAdmin, "Admin Doanh thu hoặc Admin tổng")) return;
            _ucDashboard ??= new ucDashboard();
            SwitchView(_ucDashboard, "Báo cáo doanh thu tổng quan");
            _ucDashboard.LoadDashboardData();
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
            _ucBanHang ??= new ucBanHang();
            SwitchView(_ucBanHang, "Bán vé xem phim & Bắp nước");
            _ucBanHang.LoadData();
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
            SwitchView(_ucTaiKhoanThuNgan, "Quản lý tài khoản thu ngân");
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
    }
}
