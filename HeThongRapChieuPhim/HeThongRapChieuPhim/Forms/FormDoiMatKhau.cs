using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms;

public sealed partial class FormDoiMatKhau : Form
{
    public FormDoiMatKhau()
    {
        InitializeComponent();
        lblHint.Text = $"Tài khoản {CurrentUser.TenDangNhap} cần đổi mật khẩu mẫu trước khi tiếp tục.";
        btnSave.Click += Save_Click;
        btnCancel.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };
    }

    public string NewPassword { get; private set; } = string.Empty;

    private void Save_Click(object? sender, EventArgs e)
    {
        if (txtNewPassword.Text != txtConfirmPassword.Text)
        {
            lblStatus.Text = "Hai mật khẩu không khớp.";
            return;
        }
        try
        {
            if (!Function.ChangeCurrentPassword(txtNewPassword.Text, out var error))
            {
                lblStatus.Text = error;
                return;
            }
            NewPassword = txtNewPassword.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex) { lblStatus.Text = "Không thể đổi mật khẩu: " + ex.Message; }
    }
}
