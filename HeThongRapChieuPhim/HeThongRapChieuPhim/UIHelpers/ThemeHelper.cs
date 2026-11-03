using System;
using System.Drawing;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.UIHelpers
{
    public static class ThemeHelper
    {
        // Shared WinForms palette
        public static readonly Color AppBackground = Color.FromArgb(243, 246, 249);
        public static readonly Color CardBackground = Color.White;
        public static readonly Color SidebarBackground = Color.White;
        public static readonly Color SidebarBorder = Color.FromArgb(225, 231, 235);
        public static readonly Color HeaderBackground = Color.White;

        // Brand & Accents
        public static readonly Color PrimaryDark = Color.FromArgb(0, 91, 82);
        public static readonly Color PrimaryGreen = Color.FromArgb(0, 154, 130);
        public static readonly Color LightGreenBadge = Color.FromArgb(224, 244, 239);
        public static readonly Color AccentGold = Color.FromArgb(191, 126, 39);
        public static readonly Color AccentRed = Color.FromArgb(194, 72, 72);
        public static readonly Color AccentGreen = Color.FromArgb(42, 137, 91);
        public static readonly Color AccentBlue = Color.FromArgb(64, 137, 179);

        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(35, 48, 59);
        public static readonly Color TextMuted = Color.FromArgb(107, 120, 131);
        public static readonly Color BorderColor = Color.FromArgb(225, 231, 235);

        // Backward compatibility mappings
        public static readonly Color BackgroundDark = AppBackground;
        public static readonly Color SurfaceDark = CardBackground;
        public static readonly Color SurfaceLighter = Color.FromArgb(247, 249, 250);
        public static readonly Color TextWhite = TextPrimary;

        // Seat Colors
        public static readonly Color SeatAvailable = Color.White;
        public static readonly Color SeatSelected = Color.FromArgb(22, 163, 74);       // Selected seats
        public static readonly Color SeatBooked = Color.FromArgb(255, 160, 160);       // Light red

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.FromArgb(235, 239, 242);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 28;

            // Header Style
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextMuted;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 2, 6, 2);
            dgv.ColumnHeadersHeight = 36;

            // Rows Style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = LightGreenBadge;
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);

            // Alternating Row Style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = LightGreenBadge;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextPrimary;
        }

    }
}
