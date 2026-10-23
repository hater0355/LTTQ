namespace HeThongRapChieuPhim.Forms
{
    partial class FormInHoaDon
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrintTickets = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTicket = new System.Windows.Forms.Panel();
            this.lblTicketHeading = new System.Windows.Forms.Label();
            this.lblTicketPreview = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlBottom
            //
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.pnlBottom.Controls.Add(this.btnPrint);
            this.pnlBottom.Controls.Add(this.btnExportExcel);
            this.pnlBottom.Controls.Add(this.btnPrintTickets);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 621);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlBottom.Size = new System.Drawing.Size(484, 60);
            this.pnlBottom.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(310, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 40);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "🖨️ In Hóa Đơn";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            //
            // btnExportExcel
            //
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(112)))), ((int)(((byte)(66)))));
            this.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportExcel.FlatAppearance.BorderSize = 0;
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(165, 10);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(135, 40);
            this.btnExportExcel.TabIndex = 3;
            this.btnExportExcel.Text = "Lưu Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // btnPrintTickets
            this.btnPrintTickets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.btnPrintTickets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintTickets.FlatAppearance.BorderSize = 0;
            this.btnPrintTickets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintTickets.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrintTickets.ForeColor = System.Drawing.Color.White;
            this.btnPrintTickets.Location = new System.Drawing.Point(10, 10);
            this.btnPrintTickets.Name = "btnPrintTickets";
            this.btnPrintTickets.Size = new System.Drawing.Size(145, 40);
            this.btnPrintTickets.TabIndex = 2;
            this.btnPrintTickets.Text = "🎟️ In vé từng ghế";
            this.btnPrintTickets.UseVisualStyleBackColor = false;
            this.btnPrintTickets.Click += new System.EventHandler(this.BtnPrintTickets_Click);
            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(233)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.btnClose.Location = new System.Drawing.Point(470, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // pnlTicket
            // 
            this.pnlTicket.BackColor = System.Drawing.Color.White;
            this.pnlTicket.Controls.Add(this.lblTicketPreview);
            this.pnlTicket.Controls.Add(this.lblTicketHeading);
            this.pnlTicket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTicket.Location = new System.Drawing.Point(0, 0);
            this.pnlTicket.Name = "pnlTicket";
            this.pnlTicket.Padding = new System.Windows.Forms.Padding(25);
            this.pnlTicket.Size = new System.Drawing.Size(484, 621);
            this.pnlTicket.TabIndex = 1;
            // 
            // lblTicketHeading
            // 
            this.lblTicketHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketHeading.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTicketHeading.ForeColor = System.Drawing.Color.FromArgb(180, 100, 0);
            this.lblTicketHeading.Location = new System.Drawing.Point(25, 25);
            this.lblTicketHeading.Name = "lblTicketHeading";
            this.lblTicketHeading.Size = new System.Drawing.Size(434, 36);
            this.lblTicketHeading.TabIndex = 0;
            this.lblTicketHeading.Text = "★ RẠP CHIẾU PHIM NHH CINEMA ★";
            this.lblTicketHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTicketPreview
            // 
            this.lblTicketPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketPreview.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTicketPreview.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.lblTicketPreview.Location = new System.Drawing.Point(25, 61);
            this.lblTicketPreview.Name = "lblTicketPreview";
            this.lblTicketPreview.Padding = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.lblTicketPreview.Size = new System.Drawing.Size(434, 535);
            this.lblTicketPreview.TabIndex = 1;
            this.lblTicketPreview.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // FormInHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(620, 681);
            this.Controls.Add(this.pnlTicket);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HÓA ĐƠN THANH TOÁN VÉ PHIM - NHH CINEMA";
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrintTickets;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlTicket;
        private System.Windows.Forms.Label lblTicketHeading;
        private System.Windows.Forms.Label lblTicketPreview;
    }
}
