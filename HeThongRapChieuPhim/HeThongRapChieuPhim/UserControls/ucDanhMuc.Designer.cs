namespace HeThongRapChieuPhim.UserControls
{
    partial class ucDanhMuc
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
            this.pnlSubNav = new System.Windows.Forms.Panel();
            this.btnPhong = new System.Windows.Forms.Button();
            this.btnDoAn = new System.Windows.Forms.Button();
            this.btnPhim = new System.Windows.Forms.Button();
            this.btnNV = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlSubNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSubNav
            // 
            this.pnlSubNav.BackColor = System.Drawing.Color.FromArgb(227, 237, 229);
            this.pnlSubNav.Controls.Add(this.btnPhong);
            this.pnlSubNav.Controls.Add(this.btnDoAn);
            this.pnlSubNav.Controls.Add(this.btnPhim);
            this.pnlSubNav.Controls.Add(this.btnNV);
            this.pnlSubNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubNav.Location = new System.Drawing.Point(0, 0);
            this.pnlSubNav.Name = "pnlSubNav";
            this.pnlSubNav.Padding = new System.Windows.Forms.Padding(15, 6, 15, 6);
            this.pnlSubNav.Size = new System.Drawing.Size(1080, 52);
            this.pnlSubNav.TabIndex = 0;
            // 
            // btnPhong
            // 
            this.btnPhong.BackColor = System.Drawing.Color.Transparent;
            this.btnPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPhong.FlatAppearance.BorderSize = 0;
            this.btnPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPhong.ForeColor = System.Drawing.Color.FromArgb(60, 95, 75);
            this.btnPhong.Location = new System.Drawing.Point(515, 8);
            this.btnPhong.Name = "btnPhong";
            this.btnPhong.Size = new System.Drawing.Size(130, 32);
            this.btnPhong.TabIndex = 3;
            this.btnPhong.Text = "Phòng Chiếu";
            this.btnPhong.UseVisualStyleBackColor = true;
            this.btnPhong.Click += new System.EventHandler(this.BtnPhong_Click);
            // 
            // btnDoAn
            // 
            this.btnDoAn.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnDoAn.Location = new System.Drawing.Point(305, 8);
            this.btnDoAn.Name = "btnDoAn";
            this.btnDoAn.Size = new System.Drawing.Size(130, 32);
            this.btnDoAn.TabIndex = 2;
            this.btnDoAn.Text = "Đồ Ăn (F&B)";
            this.btnDoAn.UseVisualStyleBackColor = true;
            this.btnDoAn.Click += new System.EventHandler(this.BtnDoAn_Click);
            // 
            // btnPhim
            // 
            this.btnPhim.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPhim.Location = new System.Drawing.Point(155, 8);
            this.btnPhim.Name = "btnPhim";
            this.btnPhim.Size = new System.Drawing.Size(130, 32);
            this.btnPhim.TabIndex = 1;
            this.btnPhim.Text = "Phim & Poster";
            this.btnPhim.UseVisualStyleBackColor = true;
            this.btnPhim.Click += new System.EventHandler(this.BtnPhim_Click);
            // 
            // btnNV
            // 
            this.btnNV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNV.Location = new System.Drawing.Point(15, 8);
            this.btnNV.Name = "btnNV";
            this.btnNV.Size = new System.Drawing.Size(120, 32);
            this.btnNV.TabIndex = 0;
            this.btnNV.Text = "Nhân Viên";
            this.btnNV.UseVisualStyleBackColor = true;
            this.btnNV.Click += new System.EventHandler(this.BtnNV_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 52);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1080, 628);
            this.pnlBody.TabIndex = 1;
            // 
            // ucDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlSubNav);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucDanhMuc";
            this.Size = new System.Drawing.Size(1080, 680);
            this.pnlSubNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlSubNav;
        private System.Windows.Forms.Button btnNV;
        private System.Windows.Forms.Button btnPhim;
        private System.Windows.Forms.Button btnDoAn;
        private System.Windows.Forms.Button btnPhong;
        private System.Windows.Forms.Panel pnlBody;
    }
}
