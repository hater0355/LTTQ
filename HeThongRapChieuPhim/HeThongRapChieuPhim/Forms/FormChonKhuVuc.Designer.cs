using System.Drawing;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.Forms
{
    partial class FormChonKhuVuc
    {
        private TableLayoutPanel rootLayout = null!;
        private Panel heroPanel = null!;
        private TableLayoutPanel heroLayout = null!;
        private PictureBox picHero = null!;
        private Label lblWelcome = null!;
        private Button btnExit = null!;
        private TableLayoutPanel contentLayout = null!;
        private Panel introPanel = null!;
        private Label lblHeading = null!;
        private Label lblSubheading = null!;
        private Button btnAdmin = null!;
        private Button btnMovies = null!;
        private Button btnPeople = null!;
        private Button btnFood = null!;
        private Button btnRevenue = null!;
        private Button btnCashier = null!;

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            heroPanel = new Panel();
            heroLayout = new TableLayoutPanel();
            picHero = new PictureBox();
            lblWelcome = new Label();
            btnExit = new Button();
            contentLayout = new TableLayoutPanel();
            introPanel = new Panel();
            lblHeading = new Label();
            lblSubheading = new Label();
            btnAdmin = new Button();
            btnMovies = new Button();
            btnPeople = new Button();
            btnFood = new Button();
            btnRevenue = new Button();
            btnCashier = new Button();
            rootLayout.SuspendLayout();
            heroPanel.SuspendLayout();
            heroLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHero).BeginInit();
            contentLayout.SuspendLayout();
            introPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.BackColor = SystemColors.Control;
            rootLayout.ColumnCount = 2;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 380F));
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(heroPanel, 0, 0);
            rootLayout.Controls.Add(contentLayout, 1, 0);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(20);
            rootLayout.RowCount = 1;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(1240, 790);
            rootLayout.TabIndex = 0;
            // 
            // heroPanel
            // 
            heroPanel.BackColor = Color.FromArgb(0, 51, 102);
            heroPanel.Controls.Add(heroLayout);
            heroPanel.Dock = DockStyle.Fill;
            heroPanel.Location = new Point(20, 20);
            heroPanel.Margin = new Padding(0, 0, 20, 0);
            heroPanel.Name = "heroPanel";
            heroPanel.Padding = new Padding(12);
            heroPanel.Size = new Size(360, 750);
            heroPanel.TabIndex = 0;
            // 
            // heroLayout
            // 
            heroLayout.BackColor = Color.FromArgb(0, 51, 102);
            heroLayout.ColumnCount = 1;
            heroLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            heroLayout.Controls.Add(picHero, 0, 0);
            heroLayout.Controls.Add(lblWelcome, 0, 1);
            heroLayout.Controls.Add(btnExit, 0, 2);
            heroLayout.Dock = DockStyle.Fill;
            heroLayout.Location = new Point(12, 12);
            heroLayout.Name = "heroLayout";
            heroLayout.Padding = new Padding(10);
            heroLayout.RowCount = 3;
            heroLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            heroLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            heroLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            heroLayout.Size = new Size(336, 726);
            heroLayout.TabIndex = 0;
            // 
            // picHero
            // 
            picHero.BackColor = Color.FromArgb(0, 51, 102);
            picHero.Dock = DockStyle.Fill;
            picHero.Location = new Point(10, 10);
            picHero.Margin = new Padding(0, 0, 0, 6);
            picHero.Name = "picHero";
            picHero.Size = new Size(316, 382);
            picHero.SizeMode = PictureBoxSizeMode.Zoom;
            picHero.TabIndex = 0;
            picHero.TabStop = false;
            picHero.Click += picHero_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.BackColor = Color.FromArgb(0, 51, 102);
            lblWelcome.Dock = DockStyle.Fill;
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(13, 398);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Padding = new Padding(15);
            lblWelcome.Size = new Size(310, 247);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM\r\n\r\nChào mừng bạn đến với phần mềm quản lý rạp chiếu phim.\r\n\r\nVui lòng chọn khu vực làm việc phù hợp để đăng nhập.";
            lblWelcome.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnExit
            // 
            btnExit.BackColor = SystemColors.Control;
            btnExit.Cursor = Cursors.Hand;
            btnExit.Dock = DockStyle.Fill;
            btnExit.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnExit.ForeColor = Color.Black;
            btnExit.Location = new Point(10, 649);
            btnExit.Margin = new Padding(0, 4, 0, 0);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(316, 67);
            btnExit.TabIndex = 2;
            btnExit.Text = "THOÁT ỨNG DỤNG";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // contentLayout
            // 
            contentLayout.BackColor = Color.Transparent;
            contentLayout.ColumnCount = 2;
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            contentLayout.Controls.Add(introPanel, 0, 0);
            contentLayout.Controls.Add(btnAdmin, 0, 1);
            contentLayout.Controls.Add(btnMovies, 1, 1);
            contentLayout.Controls.Add(btnPeople, 0, 2);
            contentLayout.Controls.Add(btnFood, 1, 2);
            contentLayout.Controls.Add(btnRevenue, 0, 3);
            contentLayout.Controls.Add(btnCashier, 1, 3);
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.Location = new Point(403, 23);
            contentLayout.Name = "contentLayout";
            contentLayout.Padding = new Padding(8, 4, 4, 4);
            contentLayout.RowCount = 4;
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.4F));
            contentLayout.Size = new Size(814, 744);
            contentLayout.TabIndex = 1;
            // 
            // introPanel
            // 
            introPanel.BackColor = Color.Transparent;
            contentLayout.SetColumnSpan(introPanel, 2);
            introPanel.Controls.Add(lblHeading);
            introPanel.Controls.Add(lblSubheading);
            introPanel.Dock = DockStyle.Fill;
            introPanel.Location = new Point(11, 7);
            introPanel.Name = "introPanel";
            introPanel.Size = new Size(796, 74);
            introPanel.TabIndex = 0;
            // 
            // lblHeading
            // 
            lblHeading.AutoSize = true;
            lblHeading.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(0, 51, 102);
            lblHeading.Location = new Point(8, 6);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(396, 41);
            lblHeading.TabIndex = 0;
            lblHeading.Text = "CHỌN KHU VỰC LÀM VIỆC";
            // 
            // lblSubheading
            // 
            lblSubheading.AutoSize = true;
            lblSubheading.Font = new Font("Segoe UI", 9.5F);
            lblSubheading.ForeColor = Color.DimGray;
            lblSubheading.Location = new Point(10, 45);
            lblSubheading.Name = "lblSubheading";
            lblSubheading.Size = new Size(478, 21);
            lblSubheading.TabIndex = 1;
            lblSubheading.Text = "Mỗi tài khoản chỉ truy cập các chức năng thuộc nhiệm vụ được giao.";
            // 
            // btnAdmin
            // 
            btnAdmin.BackColor = Color.White;
            btnAdmin.Cursor = Cursors.Hand;
            btnAdmin.Dock = DockStyle.Fill;
            btnAdmin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdmin.ForeColor = Color.Black;
            btnAdmin.Location = new Point(15, 91);
            btnAdmin.Margin = new Padding(7);
            btnAdmin.Name = "btnAdmin";
            btnAdmin.Padding = new Padding(12);
            btnAdmin.Size = new Size(387, 204);
            btnAdmin.TabIndex = 1;
            btnAdmin.Text = "ADMIN TỔNG\r\nToàn quyền • Tất cả trang quản lý";
            btnAdmin.TextAlign = ContentAlignment.MiddleLeft;
            btnAdmin.UseVisualStyleBackColor = true;
            // 
            // btnMovies
            // 
            btnMovies.BackColor = Color.White;
            btnMovies.Cursor = Cursors.Hand;
            btnMovies.Dock = DockStyle.Fill;
            btnMovies.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMovies.ForeColor = Color.Black;
            btnMovies.Location = new Point(416, 91);
            btnMovies.Margin = new Padding(7);
            btnMovies.Name = "btnMovies";
            btnMovies.Padding = new Padding(12);
            btnMovies.Size = new Size(387, 204);
            btnMovies.TabIndex = 2;
            btnMovies.Text = "PHIM & SUẤT CHIẾU\r\nPhim • Phòng chiếu • Lịch chiếu";
            btnMovies.TextAlign = ContentAlignment.MiddleLeft;
            btnMovies.UseVisualStyleBackColor = true;
            // 
            // btnPeople
            // 
            btnPeople.BackColor = Color.White;
            btnPeople.Cursor = Cursors.Hand;
            btnPeople.Dock = DockStyle.Fill;
            btnPeople.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPeople.ForeColor = Color.Black;
            btnPeople.Location = new Point(15, 309);
            btnPeople.Margin = new Padding(7);
            btnPeople.Name = "btnPeople";
            btnPeople.Padding = new Padding(12);
            btnPeople.Size = new Size(387, 204);
            btnPeople.TabIndex = 3;
            btnPeople.Text = "NHÂN SỰ & TÀI KHOẢN\r\nNhân viên • Admin • Thu ngân";
            btnPeople.TextAlign = ContentAlignment.MiddleLeft;
            btnPeople.UseVisualStyleBackColor = true;
            // 
            // btnFood
            // 
            btnFood.BackColor = Color.White;
            btnFood.Cursor = Cursors.Hand;
            btnFood.Dock = DockStyle.Fill;
            btnFood.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFood.ForeColor = Color.Black;
            btnFood.Location = new Point(416, 309);
            btnFood.Margin = new Padding(7);
            btnFood.Name = "btnFood";
            btnFood.Padding = new Padding(12);
            btnFood.Size = new Size(387, 204);
            btnFood.TabIndex = 4;
            btnFood.Text = "ĐỒ ĂN & ƯU ĐÃI\r\nĐồ ăn • Combo • Voucher";
            btnFood.TextAlign = ContentAlignment.MiddleLeft;
            btnFood.UseVisualStyleBackColor = true;
            // 
            // btnRevenue
            // 
            btnRevenue.BackColor = Color.White;
            btnRevenue.Cursor = Cursors.Hand;
            btnRevenue.Dock = DockStyle.Fill;
            btnRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRevenue.ForeColor = Color.Black;
            btnRevenue.Location = new Point(15, 527);
            btnRevenue.Margin = new Padding(7);
            btnRevenue.Name = "btnRevenue";
            btnRevenue.Padding = new Padding(12);
            btnRevenue.Size = new Size(387, 206);
            btnRevenue.TabIndex = 5;
            btnRevenue.Text = "DOANH THU\r\nBáo cáo • Thống kê • Hóa đơn";
            btnRevenue.TextAlign = ContentAlignment.MiddleLeft;
            btnRevenue.UseVisualStyleBackColor = true;
            // 
            // btnCashier
            // 
            btnCashier.BackColor = Color.White;
            btnCashier.Cursor = Cursors.Hand;
            btnCashier.Dock = DockStyle.Fill;
            btnCashier.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCashier.ForeColor = Color.Black;
            btnCashier.Location = new Point(416, 527);
            btnCashier.Margin = new Padding(7);
            btnCashier.Name = "btnCashier";
            btnCashier.Padding = new Padding(12);
            btnCashier.Size = new Size(387, 206);
            btnCashier.TabIndex = 6;
            btnCashier.Text = "THU NGÂN\r\nBán vé • Bắp nước • Hóa đơn";
            btnCashier.TextAlign = ContentAlignment.MiddleLeft;
            btnCashier.UseVisualStyleBackColor = true;
            // 
            // FormChonKhuVuc
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1240, 790);
            Controls.Add(rootLayout);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1060, 720);
            Name = "FormChonKhuVuc";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Rạp Chiếu Phim - Chọn khu vực làm việc";
            rootLayout.ResumeLayout(false);
            heroPanel.ResumeLayout(false);
            heroLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picHero).EndInit();
            contentLayout.ResumeLayout(false);
            introPanel.ResumeLayout(false);
            introPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}
