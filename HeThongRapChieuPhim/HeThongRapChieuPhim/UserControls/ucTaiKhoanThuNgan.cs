using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public sealed partial class ucTaiKhoanThuNgan : UserControl
    {
        public ucTaiKhoanThuNgan()
        {
            InitializeComponent();
            if (DesignTimeHelper.IsDesignTime(this)) return;
            if (!AccessRoles.IsAllowedToManageAccounts(CurrentUser.VaiTro)) throw new UnauthorizedAccessException("Chỉ Admin tổng hoặc Admin Nhân sự được quản lý tài khoản.");
            cboRole.DisplayMember = "Text";
            cboRole.ValueMember = "Value";
            cboRole.DataSource = AccessRoles.ManagedRoles.Select(role => new { Text = AccessRoles.DisplayName(role), Value = role }).ToList();
            cboRole.SelectedValue = AccessRoles.Cashier;

            btnCreate.Click += (_, _) => CreateAccount();
            btnChange.Click += (_, _) => ChangePassword();
            btnReload.Click += (_, _) => LoadAccounts();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            try
            {
                var accounts = Function.GetManagedAccounts();
                dgvAccounts.DataSource = accounts;
                dgvAccounts.ClearSelection();
                lblEmptyState.Visible = accounts.Rows.Count == 0;
                if (lblEmptyState.Visible) lblEmptyState.BringToFront();
                lblStatus.Text = string.Empty;
            }
            catch (Exception ex) { lblStatus.Text = ex.Message; }
        }

        private void CreateAccount()
        {
            if (!Function.CreateManagedAccount(txtUsername.Text, txtPassword.Text, Convert.ToString(cboRole.SelectedValue) ?? string.Empty, out string error)) { lblStatus.Text = error; return; }
            lblStatus.Text = AccessRoles.Normalize(Convert.ToString(cboRole.SelectedValue) ?? string.Empty) == AccessRoles.Cashier
                ? "Đã tạo tài khoản thu ngân. Nhân viên phụ trách được chọn riêng ở từng ca."
                : $"Đã tạo tài khoản {Convert.ToString(cboRole.Text)}. Tài khoản sẽ cần đổi mật khẩu khi đăng nhập lần đầu.";
            txtUsername.Clear();
            txtPassword.Clear();
            LoadAccounts();
        }

        private void ChangePassword()
        {
            if (dgvAccounts.CurrentRow == null) { lblStatus.Text = "Hãy chọn tài khoản cần đổi mật khẩu."; return; }
            string username = Convert.ToString(dgvAccounts.CurrentRow.Cells["Tên đăng nhập"].Value) ?? string.Empty;
            if (!Function.ChangeManagedAccountPassword(username, txtPassword.Text, out string error)) { lblStatus.Text = error; return; }
            lblStatus.Text = $"Đã đổi mật khẩu cho {username}.";
            txtPassword.Clear();
        }

        private void lblHeading_Click(object sender, EventArgs e)
        {

        }
    }
}
