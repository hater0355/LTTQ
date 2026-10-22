namespace HeThongRapChieuPhim.Forms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlCard = new Panel();
            picLogo = new PictureBox();
            lblBrand = new Label();
            lblSub = new Label();
            lblUser = new Label();
            txtUsername = new TextBox();
            lblPass = new Label();
            txtPassword = new TextBox();
            lblStatus = new Label();
            btnLogin = new Button();
            btnExit = new Button();
            lblDemo = new Label();
            btnFillAdmin = new Button();
            pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.BackColor = SystemColors.Control;
            pnlCard.BorderStyle = BorderStyle.FixedSingle;
            pnlCard.Controls.Add(picLogo);
            pnlCard.Controls.Add(lblBrand);
            pnlCard.Controls.Add(lblSub);
            pnlCard.Controls.Add(lblUser);
            pnlCard.Controls.Add(txtUsername);
            pnlCard.Controls.Add(lblPass);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(lblStatus);
            pnlCard.Controls.Add(btnLogin);
            pnlCard.Controls.Add(btnExit);
            pnlCard.Controls.Add(lblDemo);
            pnlCard.Controls.Add(btnFillAdmin);
            pnlCard.Location = new Point(30, 25);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(25);
            pnlCard.Size = new Size(460, 585);
            pnlCard.TabIndex = 0;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(180, 20);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(100, 100);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // lblBrand
            // 
            lblBrand.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblBrand.ForeColor = Color.FromArgb(0, 51, 102);
            lblBrand.Location = new Point(0, 125);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(460, 35);
            lblBrand.TabIndex = 1;
            lblBrand.Text = "HỆ THỐNG RẠP CHIẾU PHIM";
            lblBrand.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSub
            // 
            lblSub.Font = new Font("Segoe UI", 9.5F);
            lblSub.ForeColor = Color.DimGray;
            lblSub.Location = new Point(0, 160);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(460, 25);
            lblSub.TabIndex = 2;
            lblSub.Text = "ĐĂNG NHẬP KHU VỰC LÀM VIỆC";
            lblSub.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUser.ForeColor = Color.Black;
            lblUser.Location = new Point(45, 195);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(127, 21);
            lblUser.TabIndex = 3;
            lblUser.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(45, 220);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(370, 27);
            txtUsername.TabIndex = 4;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPass.ForeColor = Color.Black;
            lblPass.Location = new Point(45, 265);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(86, 21);
            lblPass.TabIndex = 5;
            lblPass.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(45, 290);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(370, 27);
            txtPassword.TabIndex = 6;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.FromArgb(220, 38, 38);
            lblStatus.Location = new Point(45, 330);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(370, 20);
            lblStatus.TabIndex = 7;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.Location = new Point(45, 360);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(370, 40);
            btnLogin.TabIndex = 8;
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 10F);
            btnExit.Location = new Point(45, 410);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(370, 34);
            btnExit.TabIndex = 9;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += BtnExit_Click;
            // 
            // lblDemo
            // 
            lblDemo.AutoSize = true;
            lblDemo.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblDemo.ForeColor = Color.DimGray;
            lblDemo.Location = new Point(45, 465);
            lblDemo.Name = "lblDemo";
            lblDemo.Size = new Size(190, 20);
            lblDemo.TabIndex = 10;
            lblDemo.Text = "Tài khoản quản lý mặc định:";
            // 
            // btnFillAdmin
            // 
            btnFillAdmin.Font = new Font("Segoe UI", 9.5F);
            btnFillAdmin.Location = new Point(45, 490);
            btnFillAdmin.Name = "btnFillAdmin";
            btnFillAdmin.Size = new Size(370, 34);
            btnFillAdmin.TabIndex = 11;
            btnFillAdmin.Text = "Tự điền: admin / admin";
            btnFillAdmin.UseVisualStyleBackColor = true;
            btnFillAdmin.Click += BtnFillAdmin_Click;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(520, 640);
            Controls.Add(pnlCard);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ĐĂNG NHẬP HỆ THỐNG";
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblDemo;
        private System.Windows.Forms.Button btnFillAdmin;
    }
}
