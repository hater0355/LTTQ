using System;
using System.Drawing;
using System.IO;
using QRCoder;

namespace HeThongRapChieuPhim.UIHelpers
{
    public static class QrCodeHelper
    {
        public static Image GenerateQrCode(string content, int pixelsPerModule = 10)
        {
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Nội dung mã QR không được để trống.", nameof(content));
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(pixelsPerModule);
            using var ms = new MemoryStream(qrCodeBytes);
            using var decoded = Image.FromStream(ms);
            return new Bitmap(decoded);
        }
    }
}
