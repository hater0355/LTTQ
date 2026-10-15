using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace HeThongRapChieuPhim.UIHelpers
{
    public static class KpiIconHelper
    {
        private static Image? _coinsIcon;
        private static Image? _ticketIcon;
        private static Image? _clapperIcon;
        private static Image? _logoIcon;

        public static Image GetCoinsIcon()
        {
            if (_coinsIcon != null) return _coinsIcon;
            int size = 96;
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                Color mintLight = Color.FromArgb(220, 245, 230);
                Color mintMid = Color.FromArgb(160, 210, 180);
                Color mintDark = Color.FromArgb(110, 170, 135);
                Color mintEdge = Color.FromArgb(80, 140, 105);

                void DrawCoin(float cx, float cy, float w, float h, float thick)
                {
                    var rectTop = new RectangleF(cx - w / 2, cy - h / 2, w, h);
                    var rectBottom = new RectangleF(cx - w / 2, cy - h / 2 + thick, w, h);

                    using var path = new GraphicsPath();
                    path.AddArc(rectTop, 0, 180);
                    path.AddLine(cx + w / 2, cy, cx + w / 2, cy + thick);
                    path.AddArc(rectBottom, 180, -180);
                    path.AddLine(cx - w / 2, cy + thick, cx - w / 2, cy);
                    path.CloseFigure();

                    using var bSide = new LinearGradientBrush(new PointF(cx - w / 2, cy), new PointF(cx + w / 2, cy), mintMid, mintDark);
                    g.FillPath(bSide, path);

                    using var pEdge = new Pen(mintEdge, 1.2f);
                    g.DrawArc(pEdge, rectBottom, 0, 180);

                    using var bTop = new LinearGradientBrush(new PointF(cx - w / 2, cy - h / 2), new PointF(cx + w / 2, cy + h / 2), mintLight, mintMid);
                    g.FillEllipse(bTop, rectTop);
                    g.DrawEllipse(pEdge, rectTop);
                }

                // Left stack
                DrawCoin(32, 48, 30, 14, 7);
                DrawCoin(32, 40, 30, 14, 7);
                DrawCoin(32, 32, 30, 14, 7);

                // Right stack
                DrawCoin(64, 46, 28, 13, 6);
                DrawCoin(64, 38, 28, 13, 6);
                DrawCoin(64, 30, 28, 13, 6);
                DrawCoin(64, 22, 28, 13, 6);

                // Front stack
                DrawCoin(48, 68, 34, 15, 8);
                DrawCoin(48, 58, 34, 15, 8);
                DrawCoin(48, 48, 34, 15, 8);
            }
            _coinsIcon = bmp;
            return _coinsIcon;
        }

        public static Image GetTicketIcon()
        {
            if (_ticketIcon != null) return _ticketIcon;
            int size = 96;
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.TranslateTransform(48, 48);
                g.RotateTransform(-22);

                float w = 62, h = 34;
                using var path = new GraphicsPath();
                path.AddLine(-w / 2 + 6, -h / 2, w / 2 - 6, -h / 2);
                path.AddArc(w / 2 - 6, -h / 2, 6, 6, 270, 90);
                path.AddLine(w / 2, -h / 2 + 6, w / 2, -6);
                path.AddArc(w / 2 - 5, -6, 10, 10, 90, -180);
                path.AddLine(w / 2, 4, w / 2, h / 2 - 6);
                path.AddArc(w / 2 - 6, h / 2 - 6, 6, 6, 0, 90);
                path.AddLine(w / 2 - 6, h / 2, -w / 2 + 6, h / 2);
                path.AddArc(-w / 2, h / 2 - 6, 6, 6, 90, 90);
                path.AddLine(-w / 2, h / 2 - 6, -w / 2, 4);
                path.AddArc(-w / 2 - 5, -6, 10, 10, 270, -180);
                path.AddLine(-w / 2, -6, -w / 2, -h / 2 + 6);
                path.AddArc(-w / 2, -h / 2, 6, 6, 180, 90);
                path.CloseFigure();

                using var bGrad = new LinearGradientBrush(new PointF(-w / 2, -h / 2), new PointF(w / 2, h / 2),
                    Color.FromArgb(225, 246, 232), Color.FromArgb(160, 212, 178));
                g.FillPath(bGrad, path);

                using var pBorder = new Pen(Color.FromArgb(90, 160, 120), 1.4f);
                g.DrawPath(pBorder, path);

                // Perforation line
                using var pDash = new Pen(Color.FromArgb(90, 160, 120), 1.2f);
                pDash.DashPattern = new[] { 3f, 2f };
                g.DrawLine(pDash, 8, -h / 2 + 3, 8, h / 2 - 3);

                // Mini star on ticket
                PointF[] starPts = new PointF[]
                {
                    new PointF(-12, -10), new PointF(-9, -3), new PointF(-2, -3),
                    new PointF(-7, 1), new PointF(-5, 8), new PointF(-12, 4),
                    new PointF(-19, 8), new PointF(-17, 1), new PointF(-22, -3),
                    new PointF(-15, -3)
                };
                using var bStar = new LinearGradientBrush(new PointF(-22, -10), new PointF(-2, 8),
                    Color.FromArgb(80, 155, 115), Color.FromArgb(50, 125, 85));
                g.FillPolygon(bStar, starPts);
            }
            _ticketIcon = bmp;
            return _ticketIcon;
        }

        public static Image GetClapperIcon()
        {
            if (_clapperIcon != null) return _clapperIcon;
            int size = 96;
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Board body
                var rectBoard = new RectangleF(24, 38, 52, 40);
                using var bBoard = new LinearGradientBrush(new PointF(24, 38), new PointF(76, 78),
                    Color.FromArgb(215, 232, 222), Color.FromArgb(165, 190, 175));
                g.FillRectangle(bBoard, rectBoard);

                using var pEdge = new Pen(Color.FromArgb(100, 135, 115), 1.4f);
                g.DrawRectangle(pEdge, 24, 38, 52, 40);

                // Play icon
                PointF[] playPts = new PointF[]
                {
                    new PointF(44, 50),
                    new PointF(58, 58),
                    new PointF(44, 66)
                };
                using var bPlay = new LinearGradientBrush(new PointF(44, 50), new PointF(58, 66),
                    Color.FromArgb(248, 253, 250), Color.FromArgb(195, 222, 206));
                g.FillPolygon(bPlay, playPts);
                g.DrawPolygon(pEdge, playPts);

                // Angled clapper stick
                var state = g.Save();
                g.TranslateTransform(24, 36);
                g.RotateTransform(-14);

                var stickRect = new RectangleF(0, -12, 54, 12);
                using var bStick = new SolidBrush(Color.FromArgb(85, 115, 100));
                g.FillRectangle(bStick, stickRect);
                g.DrawRectangle(pEdge, 0, -12, 54, 12);

                using var bStripe = new SolidBrush(Color.FromArgb(240, 248, 242));
                for (int i = 4; i < 50; i += 12)
                {
                    PointF[] sPts = new PointF[]
                    {
                        new PointF(i, 0),
                        new PointF(i + 5, -12),
                        new PointF(i + 10, -12),
                        new PointF(i + 5, 0)
                    };
                    g.FillPolygon(bStripe, sPts);
                }
                g.Restore(state);
            }
            _clapperIcon = bmp;
            return _clapperIcon;
        }

        public static Image? GetLogoEmblemIcon()
        {
            if (_logoIcon != null) return _logoIcon;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Logo_Icon.png");
            if (File.Exists(path))
            {
                try
                {
                    using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    _logoIcon = Image.FromStream(stream);
                }
                catch { }
            }
            return _logoIcon;
        }
    }
}
