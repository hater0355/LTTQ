using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    partial class ucTaiKhoanThuNgan
    {
        private TableLayoutPanel mainLayout = null!;
        private Panel pnlAccountList = null!;
        private Panel pnlAccountForm = null!;
        private Label lblEmptyState = null!;
        private DataGridView dgvAccounts = null!;
        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private ComboBox cboRole = null!;
        private Label lblStatus = null!;
        private Button btnCreate = null!;
        private Button btnChange = null!;
        private Button btnReload = null!;

        private void InitializeComponent()
        {
            mainLayout = new TableLayoutPanel();
            pnlAccountList = new Panel();
            pnlAccountForm = new Panel();
            lblEmptyState = new Label();
            dgvAccounts = new DataGridView();
            var lblHeading = new Label();
            var lblHint = new Label();
            var lblFormTitle = new Label();
            var lblRole = new Label();
            var lblUsername = new Label();
            var lblPassword = new Label();
            cboRole = new ComboBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnCreate = new Button();
            btnChange = new Button();
            btnReload = new Button();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
            mainLayout.SuspendLayout();
            pnlAccountList.SuspendLayout();
            pnlAccountForm.SuspendLayout();
            SuspendLayout();

            mainLayout.BackColor = SystemColors.Control;
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(18);
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lblHeading.Dock = DockStyle.Fill;
            lblHeading.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(0, 51, 102);
            lblHeading.Margin = new Padding(4);
            lblHeading.Text = "QUẢN LÝ TÀI KHOẢN QUẢN TRỊ & THU NGÂN";
            lblHeading.TextAlign = ContentAlignment.MiddleLeft;
            mainLayout.Controls.Add(lblHeading, 0, 0);
            mainLayout.SetColumnSpan(lblHeading, 2);

            lblHint.Dock = DockStyle.Fill;
            lblHint.Font = new Font("Segoe UI", 9F);
            lblHint.ForeColor = Color.DimGray;
            lblHint.Margin = new Padding(4);
            lblHint.Text = "Admin Nhân sự cấp tài khoản theo nhiệm vụ. Mật khẩu được lưu dạng băm và cần đổi ở lần đăng nhập đầu.";
            lblHint.TextAlign = ContentAlignment.MiddleLeft;
            mainLayout.Controls.Add(lblHint, 0, 1);
            mainLayout.SetColumnSpan(lblHint, 2);

            pnlAccountList.BackColor = SystemColors.Control;
            pnlAccountList.Dock = DockStyle.Fill;
            pnlAccountList.Margin = new Padding(4, 8, 12, 4);
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccounts.Dock = DockStyle.Fill;
            dgvAccounts.Margin = new Padding(0);
            dgvAccounts.MultiSelect = false;
            dgvAccounts.ReadOnly = true;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lblEmptyState.BackColor = SystemColors.Control;
            lblEmptyState.Dock = DockStyle.Fill;
            lblEmptyState.Font = new Font("Segoe UI", 12F);
            lblEmptyState.ForeColor = Color.DimGray;
            lblEmptyState.Text = "Chưa có tài khoản\r\nTạo tài khoản mới bằng biểu mẫu bên phải.";
            lblEmptyState.TextAlign = ContentAlignment.MiddleCenter;
            pnlAccountList.Controls.Add(dgvAccounts);
            pnlAccountList.Controls.Add(lblEmptyState);
            mainLayout.Controls.Add(pnlAccountList, 0, 2);

            pnlAccountForm.BackColor = SystemColors.Control;
            pnlAccountForm.BorderStyle = BorderStyle.FixedSingle;
            pnlAccountForm.Dock = DockStyle.Fill;
            pnlAccountForm.Margin = new Padding(4, 8, 4, 4);
            pnlAccountForm.Padding = new Padding(16);
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblFormTitle.Location = new Point(14, 14);
            lblFormTitle.Text = "TÀI KHOẢN MỚI";
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRole.ForeColor = Color.Black;
            lblRole.Location = new Point(14, 52);
            lblRole.Text = "Chức vụ";
            cboRole.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 9.5F);
            cboRole.Location = new Point(14, 76);
            cboRole.Size = new Size(280, 25);
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.ForeColor = Color.Black;
            lblUsername.Location = new Point(14, 120);
            lblUsername.Text = "Tên đăng nhập";
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(14, 144);
            txtUsername.PlaceholderText = "Nhập tên tài khoản";
            txtUsername.Size = new Size(280, 25);
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.Black;
            lblPassword.Location = new Point(14, 192);
            lblPassword.Text = "Mật khẩu ban đầu";
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(14, 216);
            txtPassword.PlaceholderText = "Tối thiểu 6 ký tự";
            txtPassword.Size = new Size(280, 25);
            txtPassword.UseSystemPasswordChar = true;

            btnCreate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCreate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCreate.Location = new Point(14, 264);
            btnCreate.Size = new Size(280, 38);
            btnCreate.Text = "Tạo Tài Khoản";
            btnCreate.UseVisualStyleBackColor = true;

            btnChange.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnChange.Font = new Font("Segoe UI", 9F);
            btnChange.Location = new Point(14, 312);
            btnChange.Size = new Size(280, 34);
            btnChange.Text = "Đổi Mật Khẩu Đã Chọn";
            btnChange.UseVisualStyleBackColor = true;

            btnReload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnReload.Font = new Font("Segoe UI", 9F);
            btnReload.Location = new Point(14, 356);
            btnReload.Size = new Size(280, 34);
            btnReload.Text = "Làm Mới Danh Sách";
            btnReload.UseVisualStyleBackColor = true;

            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(0, 51, 102);
            lblStatus.Location = new Point(14, 400);
            lblStatus.Size = new Size(280, 56);
            pnlAccountForm.Controls.Add(lblFormTitle);
            pnlAccountForm.Controls.Add(lblRole);
            pnlAccountForm.Controls.Add(cboRole);
            pnlAccountForm.Controls.Add(lblUsername);
            pnlAccountForm.Controls.Add(txtUsername);
            pnlAccountForm.Controls.Add(lblPassword);
            pnlAccountForm.Controls.Add(txtPassword);
            pnlAccountForm.Controls.Add(btnCreate);
            pnlAccountForm.Controls.Add(btnChange);
            pnlAccountForm.Controls.Add(btnReload);
            pnlAccountForm.Controls.Add(lblStatus);
            mainLayout.Controls.Add(pnlAccountForm, 1, 2);

            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(mainLayout);
            Name = "ucTaiKhoanThuNgan";
            Padding = new Padding(18);
            Size = new Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
            mainLayout.ResumeLayout(false);
            pnlAccountList.ResumeLayout(false);
            pnlAccountForm.ResumeLayout(false);
            pnlAccountForm.PerformLayout();
            ResumeLayout(false);
        }
    }
}
