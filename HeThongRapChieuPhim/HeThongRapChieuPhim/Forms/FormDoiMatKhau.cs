using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms;

public sealed class FormDoiMatKhau : Form
{
    private readonly TextBox _newPassword = new();
    private readonly TextBox _confirmPassword = new();
    private readonly Label _status = new();

    public FormDoiMatKhau()
    {
        Text = "Đổi mật khẩu lần đầu";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(440, 330);
        BackColor = Color.White;
        Font = new Font("Segoe UI", 10);

        var title = new Label { Text = "BẢO VỆ TÀI KHOẢN ADMIN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = ThemeHelper.PrimaryDark, Location = new Point(28, 24), AutoSize = true };
        var hint = new Label { Text = $"Tài khoản {CurrentUser.TenDangNhap} cần đổi mật khẩu mẫu trước khi tiếp tục.", ForeColor = ThemeHelper.TextMuted, Location = new Point(30, 66), Size = new Size(380, 38) };
        var firstLabel = new Label { Text = "Mật khẩu mới (ít nhất 8 ký tự)", Location = new Point(30, 116), AutoSize = true };
        _newPassword.SetBounds(30, 140, 380, 34);
        _newPassword.UseSystemPasswordChar = true;
        _newPassword.PlaceholderText = "Nhập mật khẩu mới";
        var confirmLabel = new Label { Text = "Nhập lại mật khẩu", Location = new Point(30, 184), AutoSize = true };
        _confirmPassword.SetBounds(30, 208, 380, 34);
        _confirmPassword.UseSystemPasswordChar = true;
        _confirmPassword.PlaceholderText = "Xác nhận mật khẩu mới";
        _status.SetBounds(30, 248, 380, 28);
        _status.ForeColor = ThemeHelper.AccentRed;

        var save = new Button { Text = "LƯU MẬT KHẨU", Bounds = new Rectangle(30, 282, 220, 38), BackColor = ThemeHelper.PrimaryDark, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
        save.FlatAppearance.BorderSize = 0;
        save.Click += Save_Click;
        var cancel = new Button { Text = "Thoát", Bounds = new Rectangle(260, 282, 150, 38), DialogResult = DialogResult.Cancel };
        cancel.Click += (_, _) => CurrentUser.Clear();
        Controls.AddRange([title, hint, firstLabel, _newPassword, confirmLabel, _confirmPassword, _status, save, cancel]);
        AcceptButton = save;
        CancelButton = cancel;
    }

    private void Save_Click(object? sender, EventArgs e)
    {
        if (_newPassword.Text != _confirmPassword.Text)
        {
            _status.Text = "Hai mật khẩu không khớp.";
            return;
        }
        try
        {
            if (!Function.ChangeCurrentPassword(_newPassword.Text, out var error))
            {
                _status.Text = error;
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex) { _status.Text = "Không thể đổi mật khẩu: " + ex.Message; }
    }
}
