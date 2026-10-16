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
            pnlCard.BackColor = Color.White;
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
            lblBrand.ForeColor = Color.FromArgb(20, 83, 45);
            lblBrand.Location = new Point(0, 125);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(460, 35);
            lblBrand.TabIndex = 1;
            lblBrand.Text = "NHH CINEMA";
            lblBrand.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSub
            // 
            lblSub.Font = new Font("Segoe UI", 9.5F);
            lblSub.ForeColor = Color.FromArgb(85, 115, 95);
            lblSub.Location = new Point(0, 160);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(460, 25);
            lblSub.TabIndex = 2;
            lblSub.Text = "HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM";
            lblSub.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(20, 50, 35);
            lblUser.Location = new Point(45, 195);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(127, 21);
            lblUser.TabIndex = 3;
            lblUser.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(45, 220);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(370, 34);
            txtUsername.TabIndex = 4;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPass.ForeColor = Color.FromArgb(20, 50, 35);
            lblPass.Location = new Point(45, 265);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(86, 21);
            lblPass.TabIndex = 5;
            lblPass.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(45, 290);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(370, 34);
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
            btnLogin.BackColor = Color.FromArgb(16, 163, 74);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(45, 360);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(370, 44);
            btnLogin.TabIndex = 8;
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(215, 228, 220);
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(40, 70, 55);
            btnExit.Location = new Point(45, 415);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(370, 36);
            btnExit.TabIndex = 9;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            // 
            // lblDemo
            // 
            lblDemo.AutoSize = true;
            lblDemo.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblDemo.ForeColor = Color.FromArgb(85, 115, 95);
            lblDemo.Location = new Point(45, 465);
            lblDemo.Name = "lblDemo";
            lblDemo.Size = new Size(190, 20);
            lblDemo.TabIndex = 10;
            lblDemo.Text = "Tài khoản quản lý mặc định:";
            // 
            // btnFillAdmin
            // 
            btnFillAdmin.BackColor = Color.FromArgb(220, 252, 231);
            btnFillAdmin.Cursor = Cursors.Hand;
            btnFillAdmin.FlatAppearance.BorderSize = 0;
            btnFillAdmin.FlatStyle = FlatStyle.Flat;
            btnFillAdmin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnFillAdmin.ForeColor = Color.FromArgb(21, 128, 61);
            btnFillAdmin.Location = new Point(45, 490);
            btnFillAdmin.Name = "btnFillAdmin";
            btnFillAdmin.Size = new Size(370, 34);
            btnFillAdmin.TabIndex = 11;
            btnFillAdmin.Text = "👑 Tự điền: admin / admin";
            btnFillAdmin.UseVisualStyleBackColor = false;
            btnFillAdmin.Click += BtnFillAdmin_Click;
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 245, 241);
            ClientSize = new Size(520, 640);
            Controls.Add(pnlCard);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ĐĂNG NHẬP HỆ THỐNG - NHH CINEMA";
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
