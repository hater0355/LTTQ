namespace HeThongRapChieuPhim.Forms
{
    partial class FormBooking
    {
        private void InitializeComponent()
        {

            SuspendLayout();
            bookingControl = new HeThongRapChieuPhim.UserControls.ucBanHang();
            bookingControl.Name = "bookingControl";
            bookingControl.Dock = DockStyle.Fill;
            Controls.Add(bookingControl);
            Text = "Bán vé • 4 bước";
            ClientSize = new Size(1050, 710);
            SelectionClientSize = new Size(1050, 710);
            PaymentClientSize = new Size(600, 710);
            FoodClientSize = new Size(600, 710);
            MinimumSize = new Size(550, 650);
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            MinimizeBox = false;

            Name = "FormBooking";
            ResumeLayout(false);
            PerformLayout();
        }
        private HeThongRapChieuPhim.UserControls.ucBanHang bookingControl;


    }
}
