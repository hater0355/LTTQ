using System;
using System.Drawing;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.UIHelpers
{
    public static class ThemeHelper
    {
        // Light Sage & Emerald Modern Cinema Theme (matching brand identity)
        public static readonly Color AppBackground = Color.FromArgb(240, 245, 241);      // #F0F5F1 Light Mint Gray
        public static readonly Color CardBackground = Color.FromArgb(255, 255, 255);     // #FFFFFF Pure White Card
        public static readonly Color SidebarBackground = Color.FromArgb(227, 237, 229);  // #E3EDE5 Light Sage Green
        public static readonly Color SidebarBorder = Color.FromArgb(209, 224, 213);      // #D1E0D5 Sage Border
        public static readonly Color HeaderBackground = Color.FromArgb(227, 237, 229);   // #E3EDE5 Header

        // Brand & Accents
        public static readonly Color PrimaryDark = Color.FromArgb(20, 83, 45);           // #14532D Deep Forest Emerald
        public static readonly Color PrimaryGreen = Color.FromArgb(16, 163, 74);         // #10A34A Vibrant Leaf Green
        public static readonly Color LightGreenBadge = Color.FromArgb(220, 252, 231);    // #DCFCE7 Mint Badge
        public static readonly Color AccentGold = Color.FromArgb(217, 119, 6);           // #D97706 Warm Cinema Gold
        public static readonly Color AccentRed = Color.FromArgb(220, 38, 38);            // #DC2626 Cinema Red
        public static readonly Color AccentGreen = Color.FromArgb(16, 163, 74);          // #10A34A
        public static readonly Color AccentBlue = Color.FromArgb(37, 99, 235);           // #2563EB Royal Blue

        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(20, 50, 35);           // #143223 Dark Green Text
        public static readonly Color TextMuted = Color.FromArgb(90, 115, 100);           // #5A7364 Gray-Green Muted
        public static readonly Color BorderColor = Color.FromArgb(218, 232, 222);        // #DAE8DE Card Border

        // Backward compatibility mappings
        public static readonly Color BackgroundDark = AppBackground;
        public static readonly Color SurfaceDark = CardBackground;
        public static readonly Color SurfaceLighter = Color.FromArgb(244, 249, 245);
        public static readonly Color TextWhite = TextPrimary;

        // Seat Colors
        public static readonly Color SeatAvailable = Color.FromArgb(218, 230, 222);      // Ghế trống
        public static readonly Color SeatSelected = Color.FromArgb(16, 163, 74);         // Ghế đang chọn
        public static readonly Color SeatBooked = Color.FromArgb(220, 38, 38);           // Ghế đã bán

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.FromArgb(230, 240, 233);
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
            dgv.RowTemplate.Height = 40;

            // Header Style
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(226, 237, 228);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = PrimaryDark;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 2, 8, 2);
            dgv.ColumnHeadersHeight = 44;

            // Rows Style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 235, 220);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 81, 50);
            dgv.DefaultCellStyle.Padding = new Padding(8, 2, 8, 2);

            // Alternating Row Style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 249, 245);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 235, 220);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 81, 50);
        }

    }
}
