using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormInHoaDon : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string MaHD { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public DateTime NgayXuat { get; set; } = DateTime.Now;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public DateTime? NgayChieu { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string TenPhim { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string PhongChieu { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string SuatChieu { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string DanhSachGhe { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public int SoLuongVe { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string TenCombo { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string DanhSachDoAn { get; set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public decimal TienVe { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public decimal TienGiamGia { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public decimal TienDoAn { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public decimal TongTien { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public string PhuongThucTT { get; set; } = "Tiền mặt";

        private PrintDocument printDoc = null!;
        private PrintDocument ticketPrintDoc = null!;
        private string[] _seatCodes = Array.Empty<string>();
        private int _ticketPrintIndex;

        public FormInHoaDon()
        {
            InitializeComponent();
            printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
            ticketPrintDoc = new PrintDocument { DocumentName = "Vé xem phim" };
            ticketPrintDoc.PrintPage += TicketPrintDoc_PrintPage;
            Load += (_, _) => UpdateTicketPreview();
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateTicketPreview()
        {
            string comboLine = string.IsNullOrWhiteSpace(TenCombo) ? "" : $"\r\nCombo: {TenCombo}";
            string foodLine = string.IsNullOrWhiteSpace(DanhSachDoAn) ? "" : $"\r\nBắp nước: {DanhSachDoAn}";
            var lines = new System.Collections.Generic.List<string>
            {
                "Địa chỉ: Tầng 5, Vincom Mega Mall, Hà Nội",
                "Hotline: 1900 6868 - Website: nhhcinema.vn",
                "",
                "HÓA ĐƠN XEM PHIM",
                $"Mã hóa đơn: {MaHD}",
                $"Thời gian: {NgayXuat:dd/MM/yyyy HH:mm:ss}",
                "──────────────────────────────────",
                $"Phim: {TenPhim}",
                $"Phòng: {PhongChieu}     Suất: {SuatChieu}",
                $"Ghế: {DanhSachGhe} ({SoLuongVe} vé)"
            };

            if (!string.IsNullOrWhiteSpace(comboLine)) lines.Add(comboLine.TrimStart());
            if (!string.IsNullOrWhiteSpace(foodLine)) lines.Add(foodLine.TrimStart());

            lines.AddRange(new[]
            {
                "──────────────────────────────────",
                $"Tiền vé: {TienVe:N0} đ"
            });
            if (TienGiamGia > 0) lines.Add($"Giảm giá ưu đãi: -{TienGiamGia:N0} đ");
            if (TienDoAn > 0) lines.Add($"Tiền bắp nước (F&B): {TienDoAn:N0} đ");
            lines.AddRange(new[]
            {
                "──────────────────────────────────",
                $"TỔNG THANH TOÁN: {TongTien:N0} đ",
                $"Hình thức thanh toán: {PhuongThucTT}",
                "",
                "Cảm ơn quý khách & Chúc bạn xem phim vui vẻ!",
                "Vé đã mua không hỗ trợ hoàn tiền mặt. Vui lòng giữ cuống vé."
            });

            lblTicketPreview.Text = string.Join(Environment.NewLine, lines);
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
            PrintDocumentWithDialog(printDoc, "Lỗi khi in hóa đơn");
        }

        private void BtnExportExcel_Click(object? sender, EventArgs e)
        {
            string safeBillId = string.Concat(MaHD.Select(character =>
                Path.GetInvalidFileNameChars().Contains(character) ? '_' : character));
            if (string.IsNullOrWhiteSpace(safeBillId)) safeBillId = "HoaDon";

            using var dialog = new SaveFileDialog
            {
                Title = "Lưu hóa đơn Excel",
                Filter = "Tệp Excel (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                AddExtension = true,
                FileName = $"HoaDon_{safeBillId}.xlsx",
                OverwritePrompt = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                InvoiceExcelExporter.Export(dialog.FileName, BuildExcelRows());
                MessageBox.Show($"Đã lưu hóa đơn Excel tại:\n{dialog.FileName}", "Xuất hóa đơn thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lưu hóa đơn Excel: {ex.Message}", "Lỗi xuất hóa đơn",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private (string Label, string Value)[] BuildExcelRows() =>
        [
            ("NHH CINEMA", ""),
            ("HÓA ĐƠN THANH TOÁN VÉ PHIM", ""),
            ("Mã hóa đơn", MaHD),
            ("Thời gian lập", NgayXuat.ToString("dd/MM/yyyy HH:mm:ss")),
            ("Phim", TenPhim),
            ("Ngày chiếu", NgayChieu?.ToString("dd/MM/yyyy") ?? ""),
            ("Phòng", PhongChieu),
            ("Suất chiếu", SuatChieu),
            ("Ghế", $"{DanhSachGhe} ({SoLuongVe} vé)"),
            ("Combo", TenCombo),
            ("Đồ ăn / thức uống", DanhSachDoAn),
            ("Tiền vé", $"{TienVe:N0} đ"),
            ("Giảm giá", $"{TienGiamGia:N0} đ"),
            ("Tiền đồ ăn / thức uống", $"{TienDoAn:N0} đ"),
            ("Tổng thanh toán", $"{TongTien:N0} đ"),
            ("Hình thức thanh toán", PhuongThucTT)
        ];

        private void BtnPrintTickets_Click(object? sender, EventArgs e)
        {
            _seatCodes = (DanhSachGhe ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(seat => !string.IsNullOrWhiteSpace(seat))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
            if (_seatCodes.Length == 0)
            {
                MessageBox.Show("Hóa đơn chưa có danh sách ghế để in vé.", "Không có ghế", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _ticketPrintIndex = 0;
            PrintDocumentWithDialog(ticketPrintDoc, "Lỗi khi in vé");
        }

        private void PrintDocumentWithDialog(PrintDocument document, string errorTitle)
        {
            try
            {
                using var dlg = new PrintDialog
                {
                    Document = document,
                    UseEXDialog = true
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    document.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{errorTitle}: {ex.Message}", "Lỗi In ấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TicketPrintDoc_PrintPage(object? sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null || _ticketPrintIndex >= _seatCodes.Length) return;
            DrawSingleSeatTicket(e.Graphics, e.MarginBounds, _seatCodes[_ticketPrintIndex], _ticketPrintIndex + 1, _seatCodes.Length);
            _ticketPrintIndex++;
            e.HasMorePages = _ticketPrintIndex < _seatCodes.Length;
        }

        private void DrawSingleSeatTicket(Graphics g, Rectangle bounds, string seat, int ticketNumber, int totalTickets)
        {
            g.Clear(Color.White);
            using var headerFont = new Font("Segoe UI", 15f, FontStyle.Bold);
            using var titleFont = new Font("Segoe UI", 12f, FontStyle.Bold);
            using var labelFont = new Font("Segoe UI", 10f, FontStyle.Regular);
            using var valueFont = new Font("Segoe UI", 11f, FontStyle.Bold);
            using var seatFont = new Font("Segoe UI", 22f, FontStyle.Bold);
            using var brush = new SolidBrush(Color.Black);
            using var gold = new SolidBrush(Color.FromArgb(180, 100, 0));
            using var border = new Pen(Color.FromArgb(180, 100, 0), 2);

            var box = Rectangle.Inflate(bounds, -12, -12);
            g.DrawRectangle(border, box);
            int x = box.Left + 18;
            int y = box.Top + 20;
            int width = box.Width - 36;
            using (var centered = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString("NHH CINEMA", headerFont, gold, new RectangleF(x, y, width, 30), centered);
                y += 38;
                g.DrawString("VÉ XEM PHIM", titleFont, brush, new RectangleF(x, y, width, 26), centered);
                y += 36;
            }

            g.DrawLine(border, x, y, x + width, y);
            y += 18;
            g.DrawString($"Mã hóa đơn: {MaHD}", labelFont, brush, x, y);
            y += 26;
            g.DrawString("Phim:", labelFont, brush, x, y);
            y += 20;
            g.DrawString(TenPhim, valueFont, brush, x, y);
            y += 34;
            g.DrawString($"Phòng: {PhongChieu}", labelFont, brush, x, y);
            y += 25;
            string showDate = NgayChieu.HasValue ? NgayChieu.Value.ToString("dd/MM/yyyy") : "";
            g.DrawString($"Ngày: {showDate}     Suất: {SuatChieu}", labelFont, brush, x, y);
            y += 32;
            g.DrawLine(border, x, y, x + width, y);
            y += 15;
            using (var centered = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString($"GHẾ {seat}", seatFont, gold, new RectangleF(x, y, width, 48), centered);
            y += 56;
            using (var centered = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString($"Vé {ticketNumber} / {totalTickets}", labelFont, brush, new RectangleF(x, y, width, 24), centered);
        }
    }
}
