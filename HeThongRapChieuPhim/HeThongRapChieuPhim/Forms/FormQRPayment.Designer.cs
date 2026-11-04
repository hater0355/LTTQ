namespace HeThongRapChieuPhim.Forms
{
    partial class FormQRPayment
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
            lblTitle = new Label();
            picQr = new PictureBox();
            lblInfo = new Label();
            btnConfirm = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)picQr).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 83, 45);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(434, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUÉT MÃ QR ĐỂ THANH TOÁN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picQr
            // 
            picQr.BackColor = Color.White;
            picQr.BorderStyle = BorderStyle.FixedSingle;
            picQr.Location = new Point(97, 55);
            picQr.Name = "picQr";
            picQr.Size = new Size(240, 240);
            picQr.SizeMode = PictureBoxSizeMode.Zoom;
            picQr.TabIndex = 1;
            picQr.TabStop = false;
            picQr.Click += picQr_Click;
            // 
            // lblInfo
            // 
            lblInfo.Font = new Font("Segoe UI", 10.5F);
            lblInfo.ForeColor = Color.FromArgb(20, 50, 35);
            lblInfo.Location = new Point(22, 310);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(390, 130);
            lblInfo.TabIndex = 2;
            lblInfo.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(16, 185, 129);
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(35, 460);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(220, 42);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "✅ Đã Nhận Tiền / Xác Nhận";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += BtnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(230, 238, 233);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.FromArgb(20, 50, 35);
            btnCancel.Location = new Point(275, 460);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 42);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Đóng";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // FormQRPayment
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(434, 521);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(lblInfo);
            Controls.Add(picQr);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormQRPayment";
            StartPosition = FormStartPosition.CenterParent;
            Text = "THANH TOÁN CHUYỂN KHOẢN QR CODE - NHH CINEMA";
            ((System.ComponentModel.ISupportInitialize)picQr).EndInit();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picQr;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}
