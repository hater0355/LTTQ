using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormQRPayment : Form
    {
        private readonly PayOsCheckout? _checkout;
        private readonly System.Windows.Forms.Timer _pollTimer = new() { Interval = 3000 };
        private bool _closingAfterResult;
        public bool IsPaymentConfirmed { get; private set; }

        public FormQRPayment()
        {
            InitializeComponent();
        }

        public FormQRPayment(PayOsCheckout checkout) : this()
        {
            _checkout = checkout ?? throw new ArgumentNullException(nameof(checkout));
            GenerateQr();
            btnConfirm.Text = "Mở trang PayOS";
            lblInfo.Text = $"Hóa đơn: {checkout.MaHD}\nSố tiền: {checkout.TongTien:N0} VNĐ\n" +
                "Mở ứng dụng ngân hàng để quét QR. Hóa đơn chỉ được xác nhận sau khi PayOS báo thanh toán thành công.";
            _pollTimer.Tick += PollTimer_Tick;
            Shown += (_, _) => _pollTimer.Start();
            FormClosing += FormQRPayment_FormClosing;
            FormClosed += (_, _) => _pollTimer.Dispose();
        }

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            if (_checkout is null) return;
            try { Process.Start(new ProcessStartInfo(_checkout.CheckoutUrl) { UseShellExecute = true }); }
            catch (Exception ex) { MessageBox.Show("Không thể mở trang PayOS: " + ex.Message, "PayOS", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            if (_checkout is null) return;
            _pollTimer.Stop();
            try
            {
                PaymentsApiClient.Cancel(_checkout.OrderCode);
                if (PaymentsApiClient.GetStatus(_checkout.OrderCode) == "Paid")
                {
                    IsPaymentConfirmed = true;
                    _closingAfterResult = true;
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show("Không xác nhận được việc hủy giao dịch với máy chủ: " + ex.Message, "PayOS", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            _closingAfterResult = true;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PollTimer_Tick(object? sender, EventArgs e)
        {
            if (_checkout is null) return;
            try
            {
                string status = PaymentsApiClient.GetStatus(_checkout.OrderCode);
                if (status == "Paid")
                {
                    IsPaymentConfirmed = true;
                    _pollTimer.Stop();
                    _closingAfterResult = true;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (status is "Cancelled" or "Expired")
                {
                    _pollTimer.Stop();
                    lblInfo.Text += $"\n\nGiao dịch đã { (status == "Expired" ? "hết hạn" : "bị hủy") }.";
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = $"Đang chờ xác nhận PayOS...\n{ex.Message}";
            }
        }

        private void FormQRPayment_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _pollTimer.Stop();
            if (_checkout is null || _closingAfterResult || IsPaymentConfirmed) return;
            try { PaymentsApiClient.Cancel(_checkout.OrderCode); } catch { }
        }

        private void GenerateQr()
        {
            if (_checkout is null) return;
            try { picQr.Image = QrCodeHelper.GenerateQrCode(_checkout.QrCode, 8); }
            catch (Exception ex) { lblInfo.Text = "Không thể tạo mã QR PayOS: " + ex.Message; }
        }
    }
}
