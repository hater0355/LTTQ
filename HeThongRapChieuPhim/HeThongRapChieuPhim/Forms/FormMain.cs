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
        }

        private void FormMain_Load(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentUser.HoTen))
            {
                lblUserTop.Text = $"{CurrentUser.HoTen} • {AccessRoles.DisplayName(CurrentUser.VaiTro)}";
            }

            pnlTop.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                // Draw clean slate/card icon before lblCurrentTitle
                using var b = new SolidBrush(ThemeHelper.PrimaryDark);
                g.FillRectangle(b, 16, 21, 14, 13);
                using var bWhite = new SolidBrush(Color.White);
                g.FillRectangle(bWhite, 19, 24, 8, 2);
                g.FillRectangle(bWhite, 19, 28, 5, 2);
            };

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
            pnlSidebar.Controls.Clear();
            pnlSidebar.BackColor = ThemeHelper.SidebarBackground;

            // Brand Logo in Sidebar (clean centered emblem matching mockup)
            var pnlBrand = new Panel
            {
                Dock = DockStyle.Top,
                Height = 112,
                BackColor = ThemeHelper.SidebarBackground
            };

            // Logo Icon PictureBox
            var picLogo = new PictureBox
            {
                Size = new Size(88, 88),
                Location = new Point((pnlSidebar.Width - 88) / 2, 12),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

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

            pnlBrand.Controls.Add(picLogo);

            // Nav buttons container
            var pnlNav = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(6, 4, 6, 4),
                BackColor = ThemeHelper.SidebarBackground
            };

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

            // Bottom logout in sidebar
            var pnlBottomNav = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = ThemeHelper.SidebarBackground,
                Padding = new Padding(12, 10, 12, 10)
            };

            var btnLogout = new Button
            {
                Text = "",
                BackColor = Color.FromArgb(60, 107, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var r = new RectangleF(1, 1, btnLogout.Width - 3, btnLogout.Height - 3);
                using var path = ThemeHelper.CreateRoundedPath(r, 10);
                using var brush = new SolidBrush(btnLogout.BackColor);
                g.FillPath(brush, path);

                // Draw door icon and text
                using var fontEmoji = new Font("Segoe UI Emoji", 10f);
                var iconR = new Rectangle(50, 0, 22, btnLogout.Height);
                TextRenderer.DrawText(g, "🚪", fontEmoji, iconR, Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                using var fontText = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                var textR = new Rectangle(74, 0, btnLogout.Width - 78, btnLogout.Height);
                TextRenderer.DrawText(g, "ĐĂNG XUẤT", fontText, textR, Color.White, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            };
            btnLogout.MouseEnter += (s, e) => { btnLogout.BackColor = Color.FromArgb(42, 78, 58); btnLogout.Invalidate(); };
            btnLogout.MouseLeave += (s, e) => { btnLogout.BackColor = Color.FromArgb(60, 107, 80); btnLogout.Invalidate(); };
            btnLogout.Click += BtnLogout_Click;
            pnlBottomNav.Controls.Add(btnLogout);

            pnlSidebar.Controls.Add(pnlNav);
            pnlSidebar.Controls.Add(pnlBottomNav);
            pnlSidebar.Controls.Add(pnlBrand);
        }

        private void AddNavSection(FlowLayoutPanel pnl, string icon, string title)
        {
            var pnlS = new Panel
            {
                Width = 228,
                Height = 24,
                Margin = new Padding(4, 10, 4, 2),
                BackColor = Color.Transparent
            };
            pnlS.Paint += (s, e) =>
            {
                var g = e.Graphics;
                using var fontEmoji = new Font("Segoe UI Emoji", 8f);
                using var fontText = new Font("Segoe UI", 8.2f, FontStyle.Bold);
                Color c = Color.FromArgb(55, 90, 70);

                var iconR = new Rectangle(6, 0, 18, pnlS.Height);
                TextRenderer.DrawText(g, icon, fontEmoji, iconR, c, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                var textR = new Rectangle(24, 0, pnlS.Width - 26, pnlS.Height);
                TextRenderer.DrawText(g, title, fontText, textR, c, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            };
            pnl.Controls.Add(pnlS);
        }

        private Button AddNavButton(FlowLayoutPanel pnl, string icon, string title, EventHandler onClick, bool isChild = false)
        {
            var btn = new Button
            {
                Text = "",
                Width = 228,
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeHelper.SidebarBackground,
                Margin = new Padding(4, 2, 4, 2),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            btn.Paint += (s, e) =>
            {
                var b = (Button)s!;
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                bool isActive = (b == _activeNavButton);
                if (isActive)
                {
                    var r = new RectangleF(1, 1, b.Width - 3, b.Height - 3);
                    using var path = ThemeHelper.CreateRoundedPath(r, 8);
                    using var brush = new SolidBrush(Color.White);
                    g.FillPath(brush, path);
                    using var pen = new Pen(Color.FromArgb(179, 209, 189), 1.2f);
                    g.DrawPath(pen, path);
                }

                int iconX = isChild ? 22 : 12;
                int textX = isChild ? 44 : 34;

                // Draw Icon
                if (!string.IsNullOrEmpty(icon))
                {
                    using var fontEmoji = new Font("Segoe UI Emoji", 9.5f);
                    var iconRect = new Rectangle(iconX, 0, 20, b.Height);
                    TextRenderer.DrawText(g, icon, fontEmoji, iconRect,
                        isActive ? Color.FromArgb(15, 81, 50) : Color.FromArgb(45, 75, 58),
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                }

                // Draw Text
                using var fontText = new Font("Segoe UI", 9.2f, isActive ? FontStyle.Bold : FontStyle.Regular);
                var textRect = new Rectangle(textX, 0, b.Width - textX - 6, b.Height);
                TextRenderer.DrawText(g, title, fontText, textRect,
                    isActive ? Color.FromArgb(15, 81, 50) : Color.FromArgb(30, 55, 40),
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
            };

            btn.MouseEnter += (s, e) =>
            {
                if (btn != _activeNavButton)
                {
                    btn.BackColor = Color.FromArgb(218, 232, 222);
                    btn.Invalidate();
                }
            };
            btn.MouseLeave += (s, e) =>
            {
                if (btn != _activeNavButton)
                {
                    btn.BackColor = ThemeHelper.SidebarBackground;
                    btn.Invalidate();
                }
            };

            btn.Click += (s, e) =>
            {
                HighlightButton(btn);
                onClick(s, e);
            };
            pnl.Controls.Add(btn);
            return btn;
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
            btn.Invalidate();
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
            lblCurrentTitle.Text = cleanTitle;
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
