using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.UIHelpers
{
    public enum ChartType
    {
        Bar,
        Doughnut
    }

    public class ChartDataItem
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public Color CustomColor { get; set; } = Color.Empty;

        public ChartDataItem(string label, decimal value, Color? color = null)
        {
            Label = label;
            Value = value;
            if (color.HasValue) CustomColor = color.Value;
        }
    }

    public class ChartControl : UserControl
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ChartType Type { get; set; } = ChartType.Bar;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Title { get; set; } = "Biểu đồ thống kê";

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public List<ChartDataItem> Data { get; set; } = new List<ChartDataItem>();

        public void SetData(List<ChartDataItem> data)
        {
            Data = data ?? new List<ChartDataItem>();
            Invalidate();
        }

        // Harmonious nature green & cinema palette (matching Image 1)
        private readonly Color[] _defaultPalette = new[]
        {
            Color.FromArgb(88, 130, 105),   // #588269 Olive/Forest Green
            Color.FromArgb(133, 182, 149),  // #85B695 Leaf Green
            Color.FromArgb(183, 217, 195),  // #B7D9C3 Pale Mint
            Color.FromArgb(168, 163, 102),  // #A8A366 Warm Olive Gold
            Color.FromArgb(55, 95, 72),     // #375F48 Deep Forest Green
            Color.FromArgb(100, 160, 125),  // #64A07D Sage Green
            Color.FromArgb(217, 119, 6)     // #D97706 Warm Cinema Gold
        };

        public ChartControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            ForeColor = ThemeHelper.TextPrimary;
            Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            Size = new Size(400, 260);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Paint Card Container with rounded corners & subtle border
            var cardRect = new RectangleF(0, 0, Width - 1, Height - 1);
            using (var path = ThemeHelper.CreateRoundedPath(cardRect, 14))
            {
                using (var brushBg = new SolidBrush(Color.White))
                {
                    g.FillPath(brushBg, path);
                }
                using (var penBorder = new Pen(ThemeHelper.BorderColor, 1.2f))
                {
                    g.DrawPath(penBorder, path);
                }
            }

            // Header Title with custom vector icon
            if (Type == ChartType.Bar)
            {
                // Mini 3-bar icon
                using (var b1 = new SolidBrush(Color.FromArgb(183, 217, 195)))
                using (var b2 = new SolidBrush(Color.FromArgb(133, 182, 149)))
                using (var b3 = new SolidBrush(ThemeHelper.PrimaryDark))
                {
                    g.FillRectangle(b1, 18, 22, 3, 7);
                    g.FillRectangle(b2, 23, 18, 3, 11);
                    g.FillRectangle(b3, 28, 14, 3, 15);
                }
                using (var titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
                using (var brush = new SolidBrush(ThemeHelper.PrimaryDark))
                {
                    g.DrawString(Title.Replace("📊", "").Trim(), titleFont, brush, new PointF(36, 12));
                }
            }
            else
            {
                // Mini doughnut ring icon
                using (var bOuter = new SolidBrush(ThemeHelper.PrimaryDark))
                using (var bInner = new SolidBrush(Color.White))
                {
                    g.FillEllipse(bOuter, 18, 14, 14, 14);
                    g.FillEllipse(bInner, 22, 18, 6, 6);
                }
                using (var titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
                using (var brush = new SolidBrush(ThemeHelper.PrimaryDark))
                {
                    g.DrawString(Title.Replace("🍩", "").Trim(), titleFont, brush, new PointF(38, 12));
                }
            }

            if (Data == null || Data.Count == 0 || Data.All(x => x.Value == 0))
            {
                using (var brush = new SolidBrush(ThemeHelper.TextMuted))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    g.DrawString("Chưa có dữ liệu thống kê", Font, brush, ClientRectangle, sf);
                }
                return;
            }

            if (Type == ChartType.Bar)
            {
                DrawBarChart(g);
            }
            else
            {
                DrawDoughnutChart(g);
            }
        }

        private void DrawBarChart(Graphics g)
        {
            int topMargin = 48;
            int bottomMargin = 38;
            int leftMargin = 55;
            int rightMargin = 20;

            var chartArea = new Rectangle(leftMargin, topMargin, Width - leftMargin - rightMargin, Height - topMargin - bottomMargin);
            if (chartArea.Width <= 0 || chartArea.Height <= 0) return;

            decimal maxValue = Data.Max(d => d.Value);
            if (maxValue <= 0) maxValue = 1;

            // Draw Y Grid lines (4 horizontal lines)
            using (var pen = new Pen(Color.FromArgb(235, 242, 237), 1f))
            using (var font = new Font("Segoe UI", 8f))
            using (var brush = new SolidBrush(ThemeHelper.TextMuted))
            {
                for (int i = 0; i <= 3; i++)
                {
                    float y = chartArea.Bottom - (chartArea.Height * i / 3f);
                    g.DrawLine(pen, chartArea.Left, y, chartArea.Right, y);

                    decimal val = maxValue * i / 3;
                    string label = FormatMoneyShort(val);
                    g.DrawString(label, font, brush, new PointF(8, y - 7));
                }
            }

            // Draw Bars
            int count = Math.Max(1, Data.Count);
            float totalBarSpace = chartArea.Width / (float)count;
            // Cap bar width to max 48px so fewer bars don't stretch abnormally wide
            float barWidth = Math.Min(48f, Math.Max(16f, totalBarSpace * 0.55f));

            for (int i = 0; i < count; i++)
            {
                var item = Data[i];
                float barHeight = (float)(item.Value / maxValue) * (chartArea.Height - 12);
                float x = chartArea.Left + (i * totalBarSpace) + (totalBarSpace - barWidth) / 2f;
                float y = chartArea.Bottom - barHeight;

                Color topColor = Color.FromArgb(178, 218, 192);
                Color bottomColor = Color.FromArgb(92, 148, 115);

                if (item.CustomColor != Color.Empty)
                {
                    bottomColor = item.CustomColor;
                    topColor = ControlPaint.Light(bottomColor, 0.4f);
                }

                if (barHeight > 4)
                {
                    var rect = new RectangleF(x, y, barWidth, barHeight);

                    // Soft drop shadow behind bar
                    var shadowRect = new RectangleF(x + 2, y + 2, barWidth, barHeight);
                    using (var sPath = CreateTopRoundedBarPath(shadowRect, 6))
                    using (var sBrush = new SolidBrush(Color.FromArgb(18, 0, 0, 0)))
                    {
                        g.FillPath(sBrush, sPath);
                    }

                    // Bar with rounded top corners
                    using (var barPath = CreateTopRoundedBarPath(rect, 6))
                    {
                        using (var brush = new LinearGradientBrush(new PointF(x, y), new PointF(x, chartArea.Bottom), topColor, bottomColor))
                        {
                            g.FillPath(brush, barPath);
                        }
                        using (var pen = new Pen(Color.FromArgb(50, bottomColor), 1f))
                        {
                            g.DrawPath(pen, barPath);
                        }
                    }
                }

                // Value on top of bar
                if (item.Value > 0)
                {
                    using (var font = new Font("Segoe UI", 7.5f, FontStyle.Bold))
                    using (var brush = new SolidBrush(ThemeHelper.PrimaryDark))
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                    {
                        g.DrawString(FormatMoneyShort(item.Value), font, brush, new PointF(x + barWidth / 2, Math.Max(topMargin - 15, y - 15)), sf);
                    }
                }

                // X Label (Month or Category)
                using (var font = new Font("Segoe UI", 8f))
                using (var brush = new SolidBrush(ThemeHelper.TextMuted))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                {
                    g.DrawString(item.Label, font, brush, new PointF(x + barWidth / 2, chartArea.Bottom + 6), sf);
                }
            }
        }

        private static GraphicsPath CreateTopRoundedBarPath(RectangleF rect, float radius)
        {
            var path = new GraphicsPath();
            float d = radius * 2;
            path.AddLine(rect.X, rect.Bottom, rect.X, rect.Y + radius);
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom);
            path.CloseFigure();
            return path;
        }

        private void DrawDoughnutChart(Graphics g)
        {
            int topMargin = 45;
            int rightLegendWidth = Math.Min(160, Width / 2);
            int chartDiameter = Math.Min(Width - rightLegendWidth - 45, Height - topMargin - 20);
            if (chartDiameter <= 10) return;

            var chartRect = new Rectangle(28, topMargin + (Height - topMargin - chartDiameter) / 2, chartDiameter, chartDiameter);
            decimal total = Data.Sum(d => d.Value);
            if (total <= 0) return;

            float currentAngle = -90f;
            for (int i = 0; i < Data.Count; i++)
            {
                var item = Data[i];
                if (item.Value <= 0) continue;

                float sweepAngle = (float)(item.Value / total) * 360f;
                Color sliceColor = item.CustomColor != Color.Empty ? item.CustomColor : _defaultPalette[i % _defaultPalette.Length];

                using (var brush = new SolidBrush(sliceColor))
                {
                    g.FillPie(brush, chartRect, currentAngle, sweepAngle);
                }

                using (var pen = new Pen(Color.White, 2f))
                {
                    g.DrawPie(pen, chartRect, currentAngle, sweepAngle);
                }

                currentAngle += sweepAngle;
            }

            // Hole in the middle for Doughnut effect (white circle)
            int holeDiameter = (int)(chartDiameter * 0.54);
            var holeRect = new Rectangle(
                chartRect.X + (chartDiameter - holeDiameter) / 2,
                chartRect.Y + (chartDiameter - holeDiameter) / 2,
                holeDiameter,
                holeDiameter
            );
            using (var brush = new SolidBrush(Color.White))
            {
                g.FillEllipse(brush, holeRect);
            }
            using (var penHole = new Pen(Color.FromArgb(230, 240, 233), 1f))
            {
                g.DrawEllipse(penHole, holeRect);
            }

            // "TỔNG" in center of hole (matching Image 1)
            using (var fontBold = new Font("Segoe UI", 10.5f, FontStyle.Bold))
            using (var brushPrimary = new SolidBrush(ThemeHelper.PrimaryDark))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString("TỔNG", fontBold, brushPrimary, holeRect, sf);
            }

            // Legend on right (colored dots + movie titles)
            int legendX = chartRect.Right + 20;
            int legendY = topMargin + 10;
            for (int i = 0; i < Math.Min(Data.Count, 6); i++)
            {
                var item = Data[i];
                Color color = item.CustomColor != Color.Empty ? item.CustomColor : _defaultPalette[i % _defaultPalette.Length];

                // Circular bullet
                using (var brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, legendX, legendY + 4, 10, 10);
                }

                // Label
                using (var font = new Font("Segoe UI", 9f))
                using (var brush = new SolidBrush(ThemeHelper.TextPrimary))
                {
                    string text = item.Label;
                    if (text.Length > 16) text = text.Substring(0, 14) + "..";
                    g.DrawString(text, font, brush, legendX + 16, legendY);
                }

                legendY += 24;
            }
        }

        private static string FormatMoneyShort(decimal val)
        {
            if (val >= 1_000_000_000) return (val / 1_000_000_000m).ToString("0.#") + "B";
            if (val >= 1_000_000) return (val / 1_000_000m).ToString("0.#") + "tr";
            if (val >= 1_000) return (val / 1_000m).ToString("0.#") + "k";
            return val.ToString("N0");
        }
    }
}
