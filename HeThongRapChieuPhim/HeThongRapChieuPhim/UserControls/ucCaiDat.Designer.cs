namespace HeThongRapChieuPhim.UserControls
{
    partial class ucCaiDat
    {
        private System.ComponentModel.IContainer components = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlSubNav = new System.Windows.Forms.Panel();
            this.btnSuatChieu = new System.Windows.Forms.Button();
            this.btnCombo = new System.Windows.Forms.Button();
            this.btnVoucher = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlSubNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSubNav
            // 
            this.pnlSubNav.BackColor = System.Drawing.Color.FromArgb(227, 237, 229);
            this.pnlSubNav.Controls.Add(this.btnSuatChieu);
            this.pnlSubNav.Controls.Add(this.btnCombo);
            this.pnlSubNav.Controls.Add(this.btnVoucher);
            this.pnlSubNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubNav.Location = new System.Drawing.Point(0, 0);
            this.pnlSubNav.Name = "pnlSubNav";
            this.pnlSubNav.Padding = new System.Windows.Forms.Padding(15, 6, 15, 6);
            this.pnlSubNav.Size = new System.Drawing.Size(1000, 52);
            this.pnlSubNav.TabIndex = 0;
            // 
            // btnSuatChieu
            // 
            this.btnSuatChieu.BackColor = System.Drawing.Color.Transparent;
            this.btnSuatChieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuatChieu.FlatAppearance.BorderSize = 0;
            this.btnSuatChieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuatChieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSuatChieu.ForeColor = System.Drawing.Color.FromArgb(60, 95, 75);
            this.btnSuatChieu.Location = new System.Drawing.Point(15, 8);
            this.btnSuatChieu.Name = "btnSuatChieu";
            this.btnSuatChieu.Size = new System.Drawing.Size(160, 32);
            this.btnSuatChieu.TabIndex = 0;
            this.btnSuatChieu.Text = "Cài Đặt Suất Chiếu";
            this.btnSuatChieu.UseVisualStyleBackColor = true;
            this.btnSuatChieu.Click += new System.EventHandler(this.BtnSuatChieu_Click);
            // 
            // btnCombo
            // 
            this.btnCombo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCombo.Location = new System.Drawing.Point(180, 8);
            this.btnCombo.Name = "btnCombo";
            this.btnCombo.Size = new System.Drawing.Size(170, 32);
            this.btnCombo.TabIndex = 1;
            this.btnCombo.Text = "Cài Đặt Ưu Đãi Combo";
            this.btnCombo.UseVisualStyleBackColor = true;
            this.btnCombo.Click += new System.EventHandler(this.BtnCombo_Click);
            // 
            // btnVoucher
            // 
            this.btnVoucher.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnVoucher.Location = new System.Drawing.Point(365, 8);
            this.btnVoucher.Name = "btnVoucher";
            this.btnVoucher.Size = new System.Drawing.Size(150, 32);
            this.btnVoucher.TabIndex = 2;
            this.btnVoucher.Text = "Cài Đặt Voucher";
            this.btnVoucher.UseVisualStyleBackColor = true;
            this.btnVoucher.Click += new System.EventHandler(this.BtnVoucher_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 52);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1000, 648);
            this.pnlBody.TabIndex = 1;
            // 
            // ucCaiDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlSubNav);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "ucCaiDat";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlSubNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSubNav;
        private System.Windows.Forms.Button btnSuatChieu;
        private System.Windows.Forms.Button btnCombo;
        private System.Windows.Forms.Button btnVoucher;
        private System.Windows.Forms.Panel pnlBody;
    }
}
