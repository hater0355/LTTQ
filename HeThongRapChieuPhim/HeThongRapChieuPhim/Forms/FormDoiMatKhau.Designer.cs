using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms;

partial class FormDoiMatKhau
{
    private TextBox txtNewPassword = null!;
    private TextBox txtConfirmPassword = null!;
    private Label lblStatus = null!;
    private Label lblHint = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;

    private void InitializeComponent()
    {
        var lblTitle = new Label();
        var lblNewPassword = new Label();
        var lblConfirmPassword = new Label();
        txtNewPassword = new TextBox();
        txtConfirmPassword = new TextBox();
        lblStatus = new Label();
        lblHint = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = ThemeHelper.PrimaryDark;
        lblTitle.Location = new Point(28, 24);
        lblTitle.Text = "BẢO VỆ TÀI KHOẢN ADMIN";

        lblHint.ForeColor = ThemeHelper.TextMuted;
        lblHint.Location = new Point(30, 66);
        lblHint.Size = new Size(380, 38);

        lblNewPassword.AutoSize = true;
        lblNewPassword.Location = new Point(30, 116);
        lblNewPassword.Text = "Mật khẩu mới (ít nhất 8 ký tự)";

        txtNewPassword.Location = new Point(30, 140);
        txtNewPassword.Size = new Size(380, 34);
        txtNewPassword.UseSystemPasswordChar = true;
        txtNewPassword.PlaceholderText = "Nhập mật khẩu mới";

        lblConfirmPassword.AutoSize = true;
        lblConfirmPassword.Location = new Point(30, 184);
        lblConfirmPassword.Text = "Nhập lại mật khẩu";

        txtConfirmPassword.Location = new Point(30, 208);
        txtConfirmPassword.Size = new Size(380, 34);
        txtConfirmPassword.UseSystemPasswordChar = true;
        txtConfirmPassword.PlaceholderText = "Xác nhận mật khẩu mới";

        lblStatus.ForeColor = ThemeHelper.AccentRed;
        lblStatus.Location = new Point(30, 248);
        lblStatus.Size = new Size(380, 28);

        btnSave.BackColor = ThemeHelper.PrimaryDark;
        btnSave.Cursor = Cursors.Hand;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(30, 282);
        btnSave.Size = new Size(220, 38);
        btnSave.Text = "LƯU MẬT KHẨU";
        btnSave.UseVisualStyleBackColor = false;

        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(260, 282);
        btnCancel.Size = new Size(150, 38);
        btnCancel.Text = "Thoát";
        btnCancel.UseVisualStyleBackColor = true;

        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        CancelButton = btnCancel;
        ClientSize = new Size(440, 330);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(lblStatus);
        Controls.Add(txtConfirmPassword);
        Controls.Add(lblConfirmPassword);
        Controls.Add(txtNewPassword);
        Controls.Add(lblNewPassword);
        Controls.Add(lblHint);
        Controls.Add(lblTitle);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormDoiMatKhau";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Đổi mật khẩu lần đầu";
        ResumeLayout(false);
        PerformLayout();
    }
}
