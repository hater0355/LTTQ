using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;
using HeThongRapChieuPhim.UserControls;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormMain : Form
    {
        private Button? _activeNavButton = null;
        private Panel[] _navSectionPool = Array.Empty<Panel>();
        private Label[] _navSectionLabels = Array.Empty<Label>();
        private Button[] _navButtonPool = Array.Empty<Button>();
        private int _nextNavSection;
        private int _nextNavButton;

        // UserControls
        private ucDashboard? _ucDashboard;
        private ucDanhMuc? _ucDanhMuc;
        private ucCaiDat? _ucCaiDat;
        private ucBanHang? _ucBanHang;
        private ucHoaDon? _ucHoaDon;
        private ucTaiKhoanThuNgan? _ucTaiKhoanThuNgan;

        public FormMain()
        {
            InitializeComponent();
            _navSectionPool = [pnlNavSection1, pnlNavSection2, pnlNavSection3, pnlNavSection4];
            _navSectionLabels = [lblNavSection1, lblNavSection2, lblNavSection3, lblNavSection4];
            _navButtonPool = [btnNav1, btnNav2, btnNav3, btnNav4, btnNav5, btnNav6, btnNav7, btnNav8, btnNav9, btnNav10];
            foreach (var button in _navButtonPool)
            {
                button.MouseEnter += NavButton_MouseEnter;
                button.MouseLeave += NavButton_MouseLeave;
                button.Click += NavButton_Click;
            }
        }

        private void FormMain_Load(object? sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignTime(this)) return;
            if (!string.IsNullOrEmpty(CurrentUser.HoTen))
            {
                lblUserTop.Text = $"{CurrentUser.HoTen} • {AccessRoles.DisplayName(CurrentUser.VaiTro)}";
            }

            BuildSidebar();
            timerClock.Start();

            if (CurrentUser.IsSuperAdmin) ShowDashboard();
            else if (CurrentUser.IsFilmAdmin) ShowDanhMuc(1);
            else if (CurrentUser.IsHumanResourcesAdmin) ShowDanhMuc(0);
            else if (CurrentUser.IsFoodAndPromotionsAdmin) ShowDanhMuc(2);
            else if (CurrentUser.IsRevenueAdmin) ShowDashboard();
            else ShowBanHang();
        }

        private void TimerClock_Tick(object? sender, EventArgs e)
        {
            lblClock.Text = $"🕒 {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void BuildSidebar()
        {
            pnlSidebar.BackColor = ThemeHelper.SidebarBackground;
            pnlNav.Controls.Clear();
            _nextNavSection = 0;
            _nextNavButton = 0;
            pnlBrand.BackColor = ThemeHelper.SidebarBackground;
            pnlNav.BackColor = ThemeHelper.SidebarBackground;
            pnlBottomNav.BackColor = ThemeHelper.SidebarBackground;
            picLogo.Location = new Point((pnlSidebar.Width - picLogo.Width) / 2, 12);

            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Logo_Icon.png");
            if (!File.Exists(logoPath))
            {
                logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Logo.png");
            }
            if (File.Exists(logoPath))
            {
                try
                {
                    using var stream = new FileStream(logoPath, FileMode.Open, FileAccess.Read);
                    picLogo.Image = Image.FromStream(stream);
                }
                catch { }
            }

            Button? firstButton = null;
            if (CurrentUser.IsSuperAdmin)
            {
                AddNavSection(pnlNav, "👑", "TOÀN QUYỀN QUẢN TRỊ");
                firstButton = AddNavButton(pnlNav, "📊", "Doanh thu tổng quan", (s, e) => ShowDashboard());
                AddNavButton(pnlNav, "📄", "Tra cứu hóa đơn", (s, e) => ShowHoaDon());
                AddNavSection(pnlNav, "📁", "QUẢN LÝ DANH MỤC");
                AddNavButton(pnlNav, "👥", "Nhân viên", (s, e) => ShowDanhMuc(0));
                AddNavButton(pnlNav, "🎬", "Phim", (s, e) => ShowDanhMuc(1));
                AddNavButton(pnlNav, "🥤", "Đồ ăn nước uống", (s, e) => ShowDanhMuc(2));
                AddNavButton(pnlNav, "🏛", "Phòng chiếu", (s, e) => ShowDanhMuc(3));
                AddNavSection(pnlNav, "⚙", "CẤU HÌNH RẠP");
                AddNavButton(pnlNav, "⏰", "Suất chiếu", (s, e) => ShowCaiDat(0));
                AddNavButton(pnlNav, "🎁", "Combo", (s, e) => ShowCaiDat(1));
                AddNavButton(pnlNav, "🎟", "Voucher", (s, e) => ShowCaiDat(2));
                AddNavSection(pnlNav, "🔐", "TÀI KHOẢN");
                AddNavButton(pnlNav, "🔐", "Admin & Thu ngân", (s, e) => ShowTaiKhoanThuNgan());
            }
            else if (CurrentUser.IsFilmAdmin)
            {
                AddNavSection(pnlNav, "🎬", "QUẢN LÝ PHIM & RẠP");
                firstButton = AddNavButton(pnlNav, "🎞", "Danh sách phim", (s, e) => ShowDanhMuc(1));
                AddNavButton(pnlNav, "🏛", "Phòng chiếu", (s, e) => ShowDanhMuc(3));
                AddNavSection(pnlNav, "⚙", "LỊCH CHIẾU");
                AddNavButton(pnlNav, "⏰", "Suất chiếu", (s, e) => ShowCaiDat(0));
            }
            else if (CurrentUser.IsHumanResourcesAdmin)
            {
                AddNavSection(pnlNav, "👥", "NHÂN SỰ & TÀI KHOẢN");
                firstButton = AddNavButton(pnlNav, "👤", "Nhân viên", (s, e) => ShowDanhMuc(0));
                AddNavButton(pnlNav, "🔐", "Tài khoản Admin & Thu ngân", (s, e) => ShowTaiKhoanThuNgan());
            }
            else if (CurrentUser.IsFoodAndPromotionsAdmin)
            {
                AddNavSection(pnlNav, "🥤", "ĐỒ ĂN & NƯỚC UỐNG");
                firstButton = AddNavButton(pnlNav, "🥤", "Danh mục đồ ăn", (s, e) => ShowDanhMuc(2));
                AddNavSection(pnlNav, "🎁", "ƯU ĐÃI");
                AddNavButton(pnlNav, "🎁", "Combo", (s, e) => ShowCaiDat(1));
                AddNavButton(pnlNav, "🎟", "Voucher", (s, e) => ShowCaiDat(2));
            }
            else if (CurrentUser.IsRevenueAdmin)
            {
                AddNavSection(pnlNav, "📊", "BÁO CÁO KINH DOANH");
                firstButton = AddNavButton(pnlNav, "📊", "Tổng quan doanh thu", (s, e) => ShowDashboard());
                AddNavButton(pnlNav, "📄", "Tra cứu hóa đơn", (s, e) => ShowHoaDon());
            }
            else if (CurrentUser.IsCashier)
            {
                AddNavSection(pnlNav, "💼", "HOẠT ĐỘNG KINH DOANH");
                firstButton = AddNavButton(pnlNav, "🎫", "Bán vé & bắp nước", (s, e) => ShowBanHang());
                AddNavButton(pnlNav, "📄", "Lịch sử hóa đơn", (s, e) => ShowHoaDon());
            }

            if (firstButton != null)
            {
                _activeNavButton = firstButton;
                HighlightButton(firstButton);
            }

        }

        private void BtnLogout_MouseEnter(object? sender, EventArgs e)
        {
            btnLogout.BackColor = Color.FromArgb(42, 78, 58);
            btnLogout.Invalidate();
        }

        private void BtnLogout_MouseLeave(object? sender, EventArgs e)
        {
            btnLogout.BackColor = Color.FromArgb(60, 107, 80);
            btnLogout.Invalidate();
        }

        private void AddNavSection(FlowLayoutPanel pnl, string icon, string title)
        {
            if (_nextNavSection >= _navSectionPool.Length) return;
            int index = _nextNavSection++;
            var section = _navSectionPool[index];
            _navSectionLabels[index].Text = $"{icon}  {title}";
            section.BackColor = Color.Transparent;
            pnl.Controls.Add(section);
        }

        private Button AddNavButton(FlowLayoutPanel pnl, string icon, string title, EventHandler onClick, bool isChild = false)
        {
            if (_nextNavButton >= _navButtonPool.Length) return _navButtonPool[^1];
            var btn = _navButtonPool[_nextNavButton++];
            btn.Tag = onClick;
            btn.Text = $"{icon}  {title}";
            btn.Padding = new Padding(isChild ? 22 : 12, 0, 6, 0);
            btn.BackColor = ThemeHelper.SidebarBackground;
            pnl.Controls.Add(btn);
            return btn;
        }

        private void NavButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button button && button != _activeNavButton)
            {
                button.BackColor = Color.FromArgb(218, 232, 222);
                button.Invalidate();
            }
        }

        private void NavButton_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button button && button != _activeNavButton)
            {
                button.BackColor = ThemeHelper.SidebarBackground;
                button.Invalidate();
            }
        }

        private void NavButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button || button.Tag is not EventHandler action) return;
            HighlightButton(button);
            action(sender, e);
        }

        private void HighlightButton(Button btn)
        {
            var prev = _activeNavButton;
            _activeNavButton = btn;

            if (prev != null)
            {
                prev.BackColor = ThemeHelper.SidebarBackground;
                prev.Invalidate();
            }

            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(15, 81, 50);
        }

        private void SwitchView(UserControl uc, string title)
        {
            string cleanTitle = title;
            foreach (var prefix in new[] { "📊 ", "📁 ", "⚙️ ", "🎟️ ", "📄 " })
            {
                if (cleanTitle.StartsWith(prefix))
                {
                    cleanTitle = cleanTitle.Substring(prefix.Length);
                    break;
                }
            }
            lblCurrentTitle.Text = $"🗂 {cleanTitle}";
            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
            pnlContent.ResumeLayout();
        }

        public void ShowDashboard()
        {
            if (!RequireRole(CurrentUser.IsRevenueAdmin || CurrentUser.IsSuperAdmin, "Admin Doanh thu hoặc Admin tổng")) return;
            _ucDashboard ??= new ucDashboard();
            SwitchView(_ucDashboard, "📊 TRANG CHÍNH (DASHBOARD DOANH THU)");
            _ucDashboard.LoadDashboardData();
        }

        public void ShowDanhMuc(int tabIndex = 0)
        {
            bool allowed = tabIndex switch
            {
                0 => CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin,
                1 or 3 => CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin,
                2 => CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin,
                _ => false
            };
            if (!RequireRole(allowed, "Admin phụ trách danh mục này")) return;
            _ucDanhMuc ??= new ucDanhMuc(tabIndex);
            SwitchView(_ucDanhMuc, "📁 QUẢN LÝ DANH MỤC HỆ THỐNG");
            _ucDanhMuc.SelectTab(tabIndex);
        }

        public void ShowCaiDat(int tabIndex = 0)
        {
            bool allowed = CurrentUser.IsSuperAdmin || (tabIndex == 0 ? CurrentUser.IsFilmAdmin
                : tabIndex is 1 or 2 && CurrentUser.IsFoodAndPromotionsAdmin);
            if (!RequireRole(allowed, "Admin phụ trách cấu hình này")) return;
            _ucCaiDat ??= new ucCaiDat(tabIndex);
            SwitchView(_ucCaiDat, "⚙️ CÀI ĐẶT THÔNG SỐ RẠP");
            _ucCaiDat.SelectTab(tabIndex);
        }

        public void ShowBanHang()
        {
            if (!CurrentUser.IsCashier) return;
            _ucBanHang ??= new ucBanHang();
            SwitchView(_ucBanHang, "🎟️ BÁN VÉ XEM PHIM & BẮP NƯỚC");
            _ucBanHang.LoadData();
        }

        public void ShowHoaDon()
        {
            if (!CurrentUser.IsCashier && !CurrentUser.IsRevenueAdmin && !CurrentUser.IsSuperAdmin) return;
            _ucHoaDon ??= new ucHoaDon();
            SwitchView(_ucHoaDon, "📄 QUẢN LÝ & TRA CỨU HÓA ĐƠN");
            _ucHoaDon.LoadHoaDon();
        }

        public void ShowTaiKhoanThuNgan()
        {
            if (!RequireRole(CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin, "Admin Nhân sự hoặc Admin tổng")) return;
            _ucTaiKhoanThuNgan ??= new ucTaiKhoanThuNgan();
            SwitchView(_ucTaiKhoanThuNgan, "🔐 TẠO/QUẢN LÝ TÀI KHOẢN THU NGÂN");
        }

        private bool RequireRole(bool allowed, string requiredRole)
        {
            if (allowed) return true;
            MessageBox.Show($"Chức năng này chỉ dành cho {requiredRole}.", "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CurrentUser.Clear();
                Close();
            }
        }
    }
}
