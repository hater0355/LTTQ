using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormLogin : Form
    {
        private readonly string _selectedRole;

        public FormLogin() : this(AccessRoles.HumanResourcesAdmin)
        {
        }

        public FormLogin(string selectedRole)
        {
            _selectedRole = AccessRoles.Normalize(selectedRole);
            if (_selectedRole.Length == 0) throw new ArgumentException("Chức vụ đăng nhập không hợp lệ.", nameof(selectedRole));
            InitializeComponent();
            LoadLogo();
            lblSub.Text = AccessRoles.DisplayName(_selectedRole).ToUpperInvariant();
            lblDemo.Visible = false;
            btnFillAdmin.Visible = false;
            btnExit.Text = "QUAY LẠI";
        }

        private void LoadLogo()
        {
            try
            {
                string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Logo_Icon.png");
                if (!File.Exists(logoPath))
                {
                    logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Logo.png");
                }
                if (File.Exists(logoPath))
                {
                    using var stream = new FileStream(logoPath, FileMode.Open, FileAccess.Read);
                    picLogo.Image = Image.FromStream(stream);
                }
            }
            catch { }
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrEmpty(user) || string.IsNullOrWhiteSpace(pass))
            {
                lblStatus.Text = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";
                return;
            }

            lblStatus.Text = "Đang kiểm tra thông tin đăng nhập...";
            lblStatus.ForeColor = ThemeHelper.AccentGold;
            Application.DoEvents();

            try
            {
                // Kiểm tra kết nối
                if (!Function.TestConnection(out string err))
                {
                    lblStatus.ForeColor = ThemeHelper.AccentRed;
                    lblStatus.Text = "Không kết nối được SQL Server!";
                    MessageBox.Show(
                        $"Không thể kết nối đến cơ sở dữ liệu SQL Server!\n\nChi tiết: {err}\n\n" +
                        $"Vui lòng kiểm tra dịch vụ SQL Server và file cơ sở dữ liệu Database\\NHHCinema.sql.",
                        "Lỗi kết nối CSDL",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!Function.Authenticate(user, pass, out string authenticationError))
                {
                    lblStatus.ForeColor = ThemeHelper.AccentRed;
                    lblStatus.Text = authenticationError;
                    return;
                }

                if (!string.Equals(CurrentUser.VaiTro, _selectedRole, StringComparison.OrdinalIgnoreCase))
                {
                    string actualRole = AccessRoles.DisplayName(CurrentUser.VaiTro);
                    CurrentUser.Clear();
                    lblStatus.ForeColor = ThemeHelper.AccentRed;
                    lblStatus.Text = $"Tài khoản này thuộc khu vực: {actualRole}.";
                    return;
                }

                if (CurrentUser.MustChangePassword)
                {
                    using var changePassword = new FormDoiMatKhau();
                    if (changePassword.ShowDialog(this) != DialogResult.OK)
                    {
                        CurrentUser.Clear();
                        lblStatus.ForeColor = ThemeHelper.AccentRed;
                        lblStatus.Text = "Bạn cần đổi mật khẩu để tiếp tục.";
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = ThemeHelper.AccentRed;
                if (ex.Message.Contains("YeuCauDoiMatKhau", StringComparison.OrdinalIgnoreCase)
                    && ex.Message.Contains("Invalid column name", StringComparison.OrdinalIgnoreCase))
                {
                    const string migrationMessage =
                        "Database NHHCinemaDB chưa được cập nhật cho phiên bản phần mềm này.\n\n" +
                        "Mở SQL Server Management Studio, chọn database NHHCinemaDB và chạy lần lượt:\n" +
                        "1. Database\\Migrations\\20261014_RolesAndPasswordStorage.sql\n" +
                        "2. Database\\Migrations\\20261014_SalesAndPayOsSafety.sql\n\n" +
                        "Hai script này nâng cấp cộng dồn và giữ dữ liệu hiện có. Không chạy ResetDemoDatabase.sql.";
                    lblStatus.Text = "Database cần được cập nhật.";
                    MessageBox.Show(migrationMessage, "Cần cập nhật database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = "Lỗi đăng nhập: " + ex.Message;
                MessageBox.Show("Lỗi xác thực: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnFillAdmin_Click(object? sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            lblStatus.Text = "";
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
