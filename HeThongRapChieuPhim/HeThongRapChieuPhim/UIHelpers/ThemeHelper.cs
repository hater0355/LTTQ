using System;
using System.Drawing;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.UIHelpers
{
    public static class ThemeHelper
    {
        // Standard Windows Forms Palette (Student LTTQ style)
        public static readonly Color AppBackground = SystemColors.Control;
        public static readonly Color CardBackground = Color.White;
        public static readonly Color SidebarBackground = SystemColors.Control;
        public static readonly Color SidebarBorder = SystemColors.ControlDark;
        public static readonly Color HeaderBackground = SystemColors.Control;

        // Brand & Accents
        public static readonly Color PrimaryDark = Color.FromArgb(0, 51, 102);           // Classic Navy
        public static readonly Color PrimaryGreen = Color.FromArgb(0, 102, 204);         // Classic Blue / Accent
        public static readonly Color LightGreenBadge = Color.FromArgb(235, 240, 245);
        public static readonly Color AccentGold = Color.FromArgb(180, 100, 0);
        public static readonly Color AccentRed = Color.FromArgb(204, 0, 0);
        public static readonly Color AccentGreen = Color.FromArgb(0, 128, 0);
        public static readonly Color AccentBlue = Color.FromArgb(0, 102, 204);

        // Text Colors
        public static readonly Color TextPrimary = Color.Black;
        public static readonly Color TextMuted = Color.FromArgb(70, 70, 70);
        public static readonly Color BorderColor = Color.LightGray;

        // Backward compatibility mappings
        public static readonly Color BackgroundDark = AppBackground;
        public static readonly Color SurfaceDark = CardBackground;
        public static readonly Color SurfaceLighter = Color.FromArgb(245, 245, 245);
        public static readonly Color TextWhite = TextPrimary;

        // Seat Colors
        public static readonly Color SeatAvailable = Color.White;
        public static readonly Color SeatSelected = Color.FromArgb(135, 206, 250);     // Light sky blue
        public static readonly Color SeatBooked = Color.FromArgb(255, 160, 160);       // Light red

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.EnableHeadersVisualStyles = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 28;

            // Header Style
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 2, 6, 2);
            dgv.ColumnHeadersHeight = 32;

            // Rows Style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            dgv.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dgv.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);

            // Alternating Row Style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
        }

    }
}
