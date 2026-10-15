using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormInHoaDon : Form
    {
        public string MaHD { get; set; } = "";
        public DateTime NgayXuat { get; set; } = DateTime.Now;
        public string TenPhim { get; set; } = "";
        public string PhongChieu { get; set; } = "";
        public string SuatChieu { get; set; } = "";
        public string DanhSachGhe { get; set; } = "";
        public int SoLuongVe { get; set; } = 0;
        public string TenCombo { get; set; } = "";
        public string DanhSachDoAn { get; set; } = "";
        public decimal TienVe { get; set; } = 0;
        public decimal TienGiamGia { get; set; } = 0;
        public decimal TienDoAn { get; set; } = 0;
        public decimal TongTien { get; set; } = 0;
        public string PhuongThucTT { get; set; } = "Tiền mặt";

        private PrintDocument printDoc = null!;

        public FormInHoaDon()
        {
            InitializeComponent();
            printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void PnlTicket_Paint(object? sender, PaintEventArgs e)
        {
            DrawTicketContent(e.Graphics, pnlTicket.ClientRectangle);
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (e.Graphics != null)
            {
                DrawTicketContent(e.Graphics, e.MarginBounds);
            }
        }

        private void DrawTicketContent(Graphics g, Rectangle bounds)
        {
            g.Clear(Color.White);

            using var fontHeader = new Font("Segoe UI", 14f, FontStyle.Bold);
            using var fontSubHeader = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            using var fontTitle = new Font("Segoe UI", 11.5f, FontStyle.Bold);
            using var fontBold = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            using var fontRegular = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            using var fontSmall = new Font("Segoe UI", 8.5f, FontStyle.Italic);
            using var brushBlack = new SolidBrush(Color.Black);
            using var brushGray = new SolidBrush(Color.FromArgb(90, 90, 90));
            using var brushGold = new SolidBrush(Color.FromArgb(180, 100, 0));
            using var penDashed = new Pen(Color.FromArgb(160, 160, 160), 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };

            int y = bounds.Top + 15;
            int left = bounds.Left + 15;
            int width = bounds.Width - 30;
            int right = left + width;

            // 1. Cinema Header
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString("★ RẠP CHIẾU PHIM NHH CINEMA ★", fontHeader, brushGold, new RectangleF(left, y, width, 25), sf);
                y += 26;
                g.DrawString("Địa chỉ: Tầng 5, Vincom Mega Mall, Hà Nội", fontSubHeader, brushGray, new RectangleF(left, y, width, 18), sf);
                y += 18;
                g.DrawString("Hotline: 1900 6868 - Website: nhhcinema.vn", fontSubHeader, brushGray, new RectangleF(left, y, width, 18), sf);
                y += 24;

                g.DrawLine(penDashed, left, y, right, y);
                y += 10;

                g.DrawString("HÓA ĐƠN XEM PHIM", fontTitle, brushBlack, new RectangleF(left, y, width, 22), sf);
                y += 24;
            }

            // 2. Bill Meta
            g.DrawString($"Mã HĐ: {MaHD}", fontBold, brushBlack, left, y);
            y += 20;
            g.DrawString($"Thời gian: {NgayXuat:dd/MM/yyyy HH:mm:ss}", fontRegular, brushBlack, left, y);
            y += 22;

            g.DrawLine(penDashed, left, y, right, y);
            y += 10;

            // 3. Movie Info
            g.DrawString("PHIM:", fontBold, brushBlack, left, y);
            g.DrawString(TenPhim, fontBold, brushGold, left + 70, y);
            y += 22;

            g.DrawString("PHÒNG:", fontRegular, brushBlack, left, y);
            g.DrawString(PhongChieu, fontBold, brushBlack, left + 70, y);
            g.DrawString("SUẤT:", fontRegular, brushBlack, left + 180, y);
            g.DrawString(SuatChieu, fontBold, brushBlack, left + 230, y);
            y += 22;

            g.DrawString("GHẾ:", fontRegular, brushBlack, left, y);
            g.DrawString($"{DanhSachGhe} ({SoLuongVe} vé)", fontBold, brushBlack, left + 70, y);
            y += 22;

            if (!string.IsNullOrEmpty(TenCombo))
            {
                g.DrawString("COMBO:", fontRegular, brushBlack, left, y);
                g.DrawString(TenCombo, fontRegular, brushBlack, left + 70, y);
                y += 20;
            }

            if (!string.IsNullOrEmpty(DanhSachDoAn))
            {
                g.DrawString("BẮP NƯỚC:", fontRegular, brushBlack, left, y);
                g.DrawString(DanhSachDoAn, fontRegular, brushBlack, left + 70, y);
                y += 22;
            }

            g.DrawLine(penDashed, left, y, right, y);
            y += 10;

            // 4. Financial Summary
            void DrawLineItem(string label, decimal amount, bool isBold = false)
            {
                var f = isBold ? fontBold : fontRegular;
                g.DrawString(label, f, brushBlack, left, y);
                string strVal = $"{amount:N0} đ";
                var sz = g.MeasureString(strVal, f);
                g.DrawString(strVal, f, isBold ? brushGold : brushBlack, right - sz.Width, y);
                y += 20;
            }

            DrawLineItem("Tiền vé:", TienVe);
            if (TienGiamGia > 0)
            {
                DrawLineItem("Giảm giá ưu đãi:", -TienGiamGia);
            }
            if (TienDoAn > 0)
            {
                DrawLineItem("Tiền bắp nước (F&B):", TienDoAn);
            }

            y += 4;
            g.DrawLine(penDashed, left, y, right, y);
            y += 8;

            DrawLineItem("TỔNG TIỀN THANH TOÁN:", TongTien, true);
            y += 2;
            g.DrawString($"Hình thức thanh toán: {PhuongThucTT}", fontRegular, brushBlack, left, y);
            y += 25;

            // 5. Footer
            g.DrawLine(penDashed, left, y, right, y);
            y += 12;
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString("Cảm ơn quý khách & Chúc bạn xem phim vui vẻ!", fontSmall, brushGray, new RectangleF(left, y, width, 18), sf);
                y += 16;
                g.DrawString("Vé đã mua không hỗ trợ hoàn tiền mặt. Vui lòng giữ cuống vé.", fontSmall, brushGray, new RectangleF(left, y, width, 18), sf);
            }
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            try
            {
                using var dlg = new PrintDialog
                {
                    Document = printDoc,
                    UseEXDialog = true
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in hóa đơn: {ex.Message}", "Lỗi In ấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
