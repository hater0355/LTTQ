using System.Drawing.Drawing2D;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms
{
    public sealed class FormChonKhuVuc : Form
    {
        private static readonly Color Ink = Color.FromArgb(20, 56, 43);
        private static readonly Color Green = Color.FromArgb(22, 112, 75);
        private readonly string _imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Beta_Cinemas_Home.png");

        public FormChonKhuVuc()
        {
            Text = "Beta Cinemas | Chọn khu vực làm việc";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1240, 790);
            MinimumSize = new Size(1060, 720);
            BackColor = Color.FromArgb(246, 249, 247);
            Font = new Font("Segoe UI", 10F);

            BuildLayout();
        }

        private void BuildLayout()
        {
            var outer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(24),
                BackColor = BackColor
            };
            outer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 395));
            outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            Controls.Add(outer);

            var hero = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(15, 108, 153), Margin = new Padding(0, 0, 22, 0) };
            hero.Paint += (_, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = ThemeHelper.CreateRoundedPath(new RectangleF(1, 1, hero.Width - 3, hero.Height - 3), 22);
                using var pen = new Pen(Color.FromArgb(205, 223, 218), 1.2f);
                e.Graphics.DrawPath(pen, path);
            };
            var heroLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.FromArgb(11, 124, 172),
                Padding = new Padding(12)
            };
            heroLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            heroLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 57));
            heroLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35));
            heroLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 8));

            var art = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(11, 124, 172),
                SizeMode = PictureBoxSizeMode.Zoom,
                Margin = new Padding(0, 0, 0, 6)
            };
            if (File.Exists(_imagePath))
            {
                using var stream = new FileStream(_imagePath, FileMode.Open, FileAccess.Read);
                using var source = Image.FromStream(stream);
                art.Image = new Bitmap(source);
            }
            heroLayout.Controls.Add(art, 0, 0);

            var heroText = new Label
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(28, 18, 28, 20),
                Text = "CHÀO MỪNG ĐẾN VỚI BETA CINEMAS\r\n\r\nHệ thống quản lý rạp chiếu phim\r\n\r\nChọn đúng khu vực công việc để đăng nhập và bắt đầu.",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.TopLeft
            };
            heroLayout.Controls.Add(heroText, 0, 1);
            outer.Controls.Add(hero, 0, 0);

            var content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                BackColor = Color.Transparent,
                Padding = new Padding(8, 4, 4, 4)
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 96));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 33.4f));
            outer.Controls.Add(content, 1, 0);

            var intro = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            var heading = new Label
            {
                Text = "CHỌN KHU VỰC LÀM VIỆC",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Ink,
                AutoSize = true,
                Location = new Point(8, 6)
            };
            var subheading = new Label
            {
                Text = "Mỗi tài khoản chỉ truy cập các chức năng thuộc nhiệm vụ được giao.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(103, 124, 113),
                AutoSize = true,
                Location = new Point(10, 48)
            };
            intro.Controls.Add(heading);
            intro.Controls.Add(subheading);
            content.Controls.Add(intro, 0, 0);
            content.SetColumnSpan(intro, 2);

            AddRoleCard(content, 0, 1, "👑", "ADMIN TỔNG", "Toàn quyền • Tất cả trang quản lý", AccessRoles.SuperAdmin, Color.FromArgb(255, 239, 211));
            AddRoleCard(content, 1, 1, "🎬", "PHIM & SUẤT CHIẾU", "Phim • Phòng chiếu • Lịch chiếu", AccessRoles.FilmAdmin, Color.FromArgb(228, 242, 251));
            AddRoleCard(content, 0, 2, "👥", "NHÂN SỰ & TÀI KHOẢN", "Nhân viên • Tài khoản admin • Thu ngân", AccessRoles.HumanResourcesAdmin, Color.FromArgb(232, 246, 237));
            AddRoleCard(content, 1, 2, "🥤", "ĐỒ ĂN & ƯU ĐÃI", "Đồ ăn nước uống • Combo • Voucher", AccessRoles.FoodAndPromotionsAdmin, Color.FromArgb(255, 244, 225));
            AddRoleCard(content, 0, 3, "📊", "DOANH THU", "Báo cáo • Thống kê • Tra cứu hóa đơn", AccessRoles.RevenueAdmin, Color.FromArgb(241, 236, 252));
            AddRoleCard(content, 1, 3, "🎟️", "THU NGÂN", "Bán vé • Bắp nước • Lịch sử hóa đơn", AccessRoles.Cashier, Color.FromArgb(226, 245, 246));

            var exit = new Button
            {
                Text = "THOÁT ỨNG DỤNG",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(83, 103, 92),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            exit.Margin = new Padding(0, 4, 0, 0);
            exit.FlatAppearance.BorderColor = Color.FromArgb(218, 229, 222);
            exit.Click += (_, _) => Close();
            heroLayout.Controls.Add(exit, 0, 2);
            hero.Controls.Add(heroLayout);
        }

        private void AddRoleCard(TableLayoutPanel parent, int column, int row, string icon, string title, string description, string role, Color tint)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(7),
                Padding = new Padding(14),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = role
            };
            card.Paint += (_, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = ThemeHelper.CreateRoundedPath(new RectangleF(1, 1, card.Width - 3, card.Height - 3), 15);
                using var brush = new SolidBrush(card.BackColor);
                using var pen = new Pen(Color.FromArgb(222, 232, 226), 1.2f);
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            };
            var badge = new Label
            {
                Text = icon,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Emoji", 20F),
                BackColor = tint,
                ForeColor = Green,
                Size = new Size(48, 48),
                Location = new Point(14, 14)
            };
            var name = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Ink,
                AutoSize = false,
                Location = new Point(72, 16),
                Size = new Size(245, 24)
            };
            var detail = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 8.3F),
                ForeColor = Color.FromArgb(105, 125, 114),
                AutoSize = false,
                Location = new Point(72, 43),
                Size = new Size(250, 35)
            };
            var action = new Button
            {
                Text = "ĐĂNG NHẬP  →",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Green,
                BackColor = tint,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Location = new Point(72, 83),
                Size = new Size(150, 34),
                Tag = role
            };
            action.FlatAppearance.BorderSize = 0;
            action.Click += RoleSelected;
            card.Click += RoleSelected;
            badge.Click += RoleSelected;
            name.Click += RoleSelected;
            detail.Click += RoleSelected;
            card.Controls.Add(badge);
            card.Controls.Add(name);
            card.Controls.Add(detail);
            card.Controls.Add(action);
            parent.Controls.Add(card, column, row);
        }

        private void RoleSelected(object? sender, EventArgs e)
        {
            string role = sender is Control control
                ? Convert.ToString(control.Tag) ?? string.Empty
                : string.Empty;
            if (string.IsNullOrWhiteSpace(role)) return;

            using var login = new FormLogin(role);
            if (login.ShowDialog(this) != DialogResult.OK) return;

            Hide();
            try
            {
                using var main = new FormMain();
                main.ShowDialog(this);
            }
            finally
            {
                CurrentUser.Clear();
                Show();
                Activate();
            }
        }
    }
}
