using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public sealed class ucTaiKhoanThuNgan : UserControl
    {
        private readonly TextBox _username = new();
        private readonly TextBox _password = new();
        private readonly ComboBox _role = new();
        private readonly DataGridView _accounts = new();
        private readonly Label _emptyState = new();
        private readonly Label _status = new();

        public ucTaiKhoanThuNgan()
        {
            if (!AccessRoles.IsAllowedToManageAccounts(CurrentUser.VaiTro)) throw new UnauthorizedAccessException("Chỉ Admin tổng hoặc Admin Nhân sự được quản lý tài khoản.");
            BackColor = Color.White;
            Padding = new Padding(18);
            BuildLayout();
            LoadAccounts();
        }

        private void BuildLayout()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(18),
                BackColor = Color.White
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var heading = new Label { Text = "QUẢN LÝ TÀI KHOẢN QUẢN TRỊ & THU NGÂN", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(20, 83, 45), TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(4) };
            layout.Controls.Add(heading, 0, 0);
            layout.SetColumnSpan(heading, 2);
            var hint = new Label { Text = "Admin Nhân sự cấp tài khoản theo nhiệm vụ. Mật khẩu được lưu dạng băm và cần đổi ở lần đăng nhập đầu.", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9), ForeColor = Color.DimGray, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(4) };
            layout.Controls.Add(hint, 0, 1);
            layout.SetColumnSpan(hint, 2);

            _accounts.ReadOnly = true;
            _accounts.MultiSelect = false;
            _accounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _accounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _accounts.AllowUserToAddRows = false;
            _accounts.Dock = DockStyle.Fill;
            _accounts.Margin = new Padding(4, 8, 12, 4);
            ThemeHelper.StyleDataGridView(_accounts);
            var listPanel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(4, 8, 12, 4), BackColor = Color.White };
            listPanel.Controls.Add(_accounts);
            _emptyState.Text = "♙\r\nChưa có tài khoản\r\nTạo tài khoản mới bằng biểu mẫu bên phải.";
            _emptyState.Dock = DockStyle.Fill;
            _emptyState.TextAlign = ContentAlignment.MiddleCenter;
            _emptyState.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            _emptyState.ForeColor = ThemeHelper.TextMuted;
            _emptyState.BackColor = Color.White;
            listPanel.Controls.Add(_emptyState);
            layout.Controls.Add(listPanel, 0, 2);

            var panel = new Panel { Dock = DockStyle.Fill, Margin = new Padding(4, 8, 4, 4), BackColor = Color.FromArgb(248, 250, 248), Padding = new Padding(16) };
            panel.Paint += (_, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var path = ThemeHelper.CreateRoundedPath(new RectangleF(1, 1, panel.Width - 3, panel.Height - 3), 12);
                using var pen = new Pen(ThemeHelper.BorderColor, 1.2f);
                e.Graphics.DrawPath(pen, path);
            };
            layout.Controls.Add(panel, 1, 2);
            var formTitle = new Label { Text = "TÀI KHOẢN MỚI", AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = ThemeHelper.PrimaryDark, Location = new Point(14, 14) };
            panel.Controls.Add(formTitle);
            var roleLabel = new Label { Text = "Chức vụ", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = ThemeHelper.TextPrimary, Location = new Point(14, 52) };
            panel.Controls.Add(roleLabel);
            _role.SetBounds(14, 76, 280, 34);
            _role.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _role.DropDownStyle = ComboBoxStyle.DropDownList;
            _role.Font = new Font("Segoe UI", 9.5F);
            _role.DisplayMember = "Text";
            _role.ValueMember = "Value";
            _role.DataSource = AccessRoles.ManagedRoles.Select(role => new { Text = AccessRoles.DisplayName(role), Value = role }).ToList();
            _role.SelectedValue = AccessRoles.Cashier;
            panel.Controls.Add(_role);
            var usernameLabel = new Label { Text = "Tên đăng nhập", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = ThemeHelper.TextPrimary, Location = new Point(14, 120) };
            panel.Controls.Add(usernameLabel);
            _username.SetBounds(14, 144, 280, 34);
            _username.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _username.Font = new Font("Segoe UI", 10);
            _username.BorderStyle = BorderStyle.FixedSingle;
            _username.PlaceholderText = "Nhập tên tài khoản";
            panel.Controls.Add(_username);
            var passwordLabel = new Label { Text = "Mật khẩu ban đầu", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = ThemeHelper.TextPrimary, Location = new Point(14, 192) };
            panel.Controls.Add(passwordLabel);
            _password.SetBounds(14, 216, 280, 34);
            _password.UseSystemPasswordChar = true;
            _password.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _password.Font = new Font("Segoe UI", 10);
            _password.BorderStyle = BorderStyle.FixedSingle;
            _password.PlaceholderText = "Tối thiểu 6 ký tự";
            panel.Controls.Add(_password);

            var create = new Button { Text = "＋  TẠO TÀI KHOẢN", Location = new Point(14, 264), Size = new Size(280, 44), BackColor = ThemeHelper.PrimaryDark, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
            create.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            create.FlatAppearance.BorderSize = 0;
            create.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 113, 63);
            create.Click += (_, _) => CreateAccount();
            panel.Controls.Add(create);
            var change = new Button { Text = "ĐỔI MẬT KHẨU ĐÃ CHỌN", Location = new Point(14, 318), Size = new Size(280, 40), BackColor = ThemeHelper.LightGreenBadge, ForeColor = ThemeHelper.PrimaryDark, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
            change.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            change.FlatAppearance.BorderColor = ThemeHelper.BorderColor;
            change.FlatAppearance.MouseOverBackColor = Color.FromArgb(201, 231, 209);
            change.Click += (_, _) => ChangePassword();
            panel.Controls.Add(change);
            var reload = new Button { Text = "↻  LÀM MỚI DANH SÁCH", Location = new Point(14, 370), Size = new Size(280, 36), BackColor = Color.White, ForeColor = ThemeHelper.TextMuted, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9), Cursor = Cursors.Hand };
            reload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            reload.FlatAppearance.BorderColor = ThemeHelper.BorderColor;
            reload.FlatAppearance.MouseOverBackColor = ThemeHelper.SurfaceLighter;
            reload.Click += (_, _) => LoadAccounts();
            panel.Controls.Add(reload);
            _status.SetBounds(14, 414, 280, 56);
            _status.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _status.ForeColor = ThemeHelper.PrimaryDark;
            _status.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            panel.Controls.Add(_status);
            panel.SizeChanged += (_, _) =>
            {
                int width = Math.Max(150, panel.ClientSize.Width - 28);
                foreach (Control control in panel.Controls)
                    if (control != formTitle && control != roleLabel && control != usernameLabel && control != passwordLabel)
                        control.Width = width;
            };
            Controls.Add(layout);
        }

        private void LoadAccounts()
        {
            try
            {
                var accounts = Function.GetManagedAccounts();
                _accounts.DataSource = accounts;
                _accounts.ClearSelection();
                _emptyState.Visible = accounts.Rows.Count == 0;
                if (_emptyState.Visible) _emptyState.BringToFront();
                _status.Text = string.Empty;
            }
            catch (Exception ex) { _status.Text = ex.Message; }
        }

        private void CreateAccount()
        {
            if (!Function.CreateManagedAccount(_username.Text, _password.Text, Convert.ToString(_role.SelectedValue) ?? string.Empty, out string error)) { _status.Text = error; return; }
            _status.Text = $"Đã tạo tài khoản {Convert.ToString(_role.Text)}. Tài khoản sẽ cần đổi mật khẩu khi đăng nhập lần đầu.";
            _password.Clear();
            LoadAccounts();
        }

        private void ChangePassword()
        {
            if (_accounts.CurrentRow == null) { _status.Text = "Hãy chọn tài khoản cần đổi mật khẩu."; return; }
            string username = Convert.ToString(_accounts.CurrentRow.Cells["Tên đăng nhập"].Value) ?? string.Empty;
            if (!Function.ChangeManagedAccountPassword(username, _password.Text, out string error)) { _status.Text = error; return; }
            _status.Text = $"Đã đổi mật khẩu cho {username}.";
            _password.Clear();
        }
    }
}
