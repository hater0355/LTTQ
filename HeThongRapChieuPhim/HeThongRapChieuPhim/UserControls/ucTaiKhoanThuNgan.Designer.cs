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
            lblHeading = new Label();
            lblHint = new Label();
            pnlAccountList = new Panel();
            dgvAccounts = new DataGridView();
            lblEmptyState = new Label();
            pnlAccountForm = new Panel();
            lblFormTitle = new Label();
            lblRole = new Label();
            cboRole = new ComboBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnCreate = new Button();
            btnChange = new Button();
            btnReload = new Button();
            lblStatus = new Label();
            mainLayout.SuspendLayout();
            pnlAccountList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
            pnlAccountForm.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.BackColor = SystemColors.Control;
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            mainLayout.Controls.Add(lblHeading, 0, 0);
            mainLayout.Controls.Add(lblHint, 0, 1);
            mainLayout.Controls.Add(pnlAccountList, 0, 2);
            mainLayout.Controls.Add(pnlAccountForm, 1, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(21, 21);
            mainLayout.Margin = new Padding(3, 4, 3, 4);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(21);
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1101, 723);
            mainLayout.TabIndex = 0;
            // 
            // lblHeading
            // 
            mainLayout.SetColumnSpan(lblHeading, 2);
            lblHeading.Dock = DockStyle.Fill;
            lblHeading.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(0, 51, 102);
            lblHeading.Location = new Point(26, 26);
            lblHeading.Margin = new Padding(5);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(1049, 39);
            lblHeading.TabIndex = 0;
            lblHeading.Text = "QUẢN LÝ TÀI KHOẢN ";
            lblHeading.TextAlign = ContentAlignment.MiddleLeft;
            lblHeading.Click += lblHeading_Click;
            // 
            // lblHint
            // 
            mainLayout.SetColumnSpan(lblHint, 2);
            lblHint.Dock = DockStyle.Fill;
            lblHint.Font = new Font("Segoe UI", 9F);
            lblHint.ForeColor = Color.DimGray;
            lblHint.Location = new Point(26, 75);
            lblHint.Margin = new Padding(5);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(1049, 30);
            lblHint.TabIndex = 1;
            lblHint.Text = "Admin Nhân sự cấp tài khoản theo nhiệm vụ. Mật khẩu được lưu dạng băm và cần đổi ở lần đăng nhập đầu.";
            lblHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlAccountList
            // 
            pnlAccountList.BackColor = SystemColors.Control;
            pnlAccountList.Controls.Add(dgvAccounts);
            pnlAccountList.Controls.Add(lblEmptyState);
            pnlAccountList.Dock = DockStyle.Fill;
            pnlAccountList.Location = new Point(26, 119);
            pnlAccountList.Margin = new Padding(5, 9, 14, 5);
            pnlAccountList.Name = "pnlAccountList";
            pnlAccountList.Size = new Size(690, 578);
            pnlAccountList.TabIndex = 2;
            // 
            // dgvAccounts
            // 
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccounts.ColumnHeadersHeight = 29;
            dgvAccounts.Dock = DockStyle.Fill;
            dgvAccounts.Location = new Point(0, 0);
            dgvAccounts.Margin = new Padding(0);
            dgvAccounts.MultiSelect = false;
            dgvAccounts.Name = "dgvAccounts";
            dgvAccounts.ReadOnly = true;
            dgvAccounts.RowHeadersWidth = 51;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.Size = new Size(690, 578);
            dgvAccounts.TabIndex = 0;
            // 
            // lblEmptyState
            // 
            lblEmptyState.BackColor = SystemColors.Control;
            lblEmptyState.Dock = DockStyle.Fill;
            lblEmptyState.Font = new Font("Segoe UI", 12F);
            lblEmptyState.ForeColor = Color.DimGray;
            lblEmptyState.Location = new Point(0, 0);
            lblEmptyState.Name = "lblEmptyState";
            lblEmptyState.Size = new Size(690, 578);
            lblEmptyState.TabIndex = 1;
            lblEmptyState.Text = "Chưa có tài khoản\r\nTạo tài khoản mới bằng biểu mẫu bên phải.";
            lblEmptyState.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlAccountForm
            // 
            pnlAccountForm.BackColor = SystemColors.Control;
            pnlAccountForm.BorderStyle = BorderStyle.FixedSingle;
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
            pnlAccountForm.Dock = DockStyle.Fill;
            pnlAccountForm.Location = new Point(735, 119);
            pnlAccountForm.Margin = new Padding(5, 9, 5, 5);
            pnlAccountForm.Name = "pnlAccountForm";
            pnlAccountForm.Padding = new Padding(18, 19, 18, 19);
            pnlAccountForm.Size = new Size(340, 578);
            pnlAccountForm.TabIndex = 3;
            // 
            // lblFormTitle
            // 
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblFormTitle.Location = new Point(16, 16);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(159, 25);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "TÀI KHOẢN MỚI";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRole.ForeColor = Color.Black;
            lblRole.Location = new Point(16, 61);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(65, 20);
            lblRole.TabIndex = 1;
            lblRole.Text = "Chức vụ";
            // 
            // cboRole
            // 
            cboRole.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 9.5F);
            cboRole.Location = new Point(16, 89);
            cboRole.Margin = new Padding(3, 4, 3, 4);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(431, 29);
            cboRole.TabIndex = 2;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.ForeColor = Color.Black;
            lblUsername.Location = new Point(16, 141);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(112, 20);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Tên đăng nhập";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(16, 169);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Nhập tên tài khoản";
            txtUsername.Size = new Size(432, 30);
            txtUsername.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.Black;
            lblPassword.Location = new Point(16, 226);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(135, 20);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu ban đầu";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(16, 254);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Tối thiểu 6 ký tự";
            txtPassword.Size = new Size(432, 30);
            txtPassword.TabIndex = 6;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnCreate
            // 
            btnCreate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCreate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCreate.Location = new Point(16, 311);
            btnCreate.Margin = new Padding(3, 4, 3, 4);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(432, 45);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Tạo Tài Khoản";
            btnCreate.UseVisualStyleBackColor = true;
            // 
            // btnChange
            // 
            btnChange.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnChange.Font = new Font("Segoe UI", 9F);
            btnChange.Location = new Point(16, 367);
            btnChange.Margin = new Padding(3, 4, 3, 4);
            btnChange.Name = "btnChange";
            btnChange.Size = new Size(432, 40);
            btnChange.TabIndex = 8;
            btnChange.Text = "Đổi Mật Khẩu Đã Chọn";
            btnChange.UseVisualStyleBackColor = true;
            // 
            // btnReload
            // 
            btnReload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnReload.Font = new Font("Segoe UI", 9F);
            btnReload.Location = new Point(16, 419);
            btnReload.Margin = new Padding(3, 4, 3, 4);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(432, 40);
            btnReload.TabIndex = 9;
            btnReload.Text = "Làm Mới Danh Sách";
            btnReload.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(0, 51, 102);
            lblStatus.Location = new Point(16, 471);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(432, 66);
            lblStatus.TabIndex = 10;
            // 
            // ucTaiKhoanThuNgan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(mainLayout);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ucTaiKhoanThuNgan";
            Padding = new Padding(21);
            Size = new Size(1143, 765);
            mainLayout.ResumeLayout(false);
            pnlAccountList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
            pnlAccountForm.ResumeLayout(false);
            pnlAccountForm.PerformLayout();


            dgvAccounts.BackgroundColor = Color.White;
            dgvAccounts.GridColor = Color.FromArgb(235, 239, 242);
            dgvAccounts.BorderStyle = BorderStyle.None;
            dgvAccounts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAccounts.EnableHeadersVisualStyles = false;
            dgvAccounts.RowHeadersVisible = false;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.MultiSelect = false;
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AllowUserToDeleteRows = false;
            dgvAccounts.ReadOnly = true;
            dgvAccounts.RowTemplate.Height = 28;

            // Header Style
            dgvAccounts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAccounts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
            dgvAccounts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 120, 131);
            dgvAccounts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvAccounts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAccounts.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 2, 6, 2);
            dgvAccounts.ColumnHeadersHeight = 36;

            // Rows Style
            dgvAccounts.DefaultCellStyle.BackColor = Color.White;
            dgvAccounts.DefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 59);
            dgvAccounts.DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            dgvAccounts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 244, 239);
            dgvAccounts.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 48, 59);
            dgvAccounts.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);

            // Alternating Row Style
            dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            dgvAccounts.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 59);
            dgvAccounts.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 244, 239);
            dgvAccounts.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 48, 59);

            ResumeLayout(false);
        }

        private Label lblHeading;
        private Label lblHint;
        private Label lblFormTitle;
        private Label lblRole;
        private Label lblUsername;
        private Label lblPassword;
    }
}
