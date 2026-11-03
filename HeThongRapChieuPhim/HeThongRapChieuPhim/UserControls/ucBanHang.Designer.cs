namespace HeThongRapChieuPhim.UserControls
{
    partial class ucBanHang
    {
        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblTitle = new Label();
            lblCashierInfo = new Label();
            pnlBody = new TableLayoutPanel();
            bookingPages = new TabControl();
            showtimeTab = new TabPage();
            showtimePage = new ucBanHangShowtimes();
            seatTab = new TabPage();
            seatPage = new ucBanHangSeats();
            foodTab = new TabPage();
            foodPage = new ucBanHangFood();
            paymentTab = new TabPage();
            paymentPage = new ucBanHangPayment();
            navigation = new TableLayoutPanel();
            _stepBack = new Button();
            _stepSummary = new Label();
            _stepNext = new Button();
            cboPhim = new ComboBox();
            lblSec1 = new Label();
            pnlTop.SuspendLayout();
            pnlBody.SuspendLayout();
            bookingPages.SuspendLayout();
            showtimeTab.SuspendLayout();
            seatTab.SuspendLayout();
            paymentTab.SuspendLayout();
            navigation.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblCashierInfo);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(12, 4, 12, 4);
            pnlTop.Size = new Size(1050, 72);
            pnlTop.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 4);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1026, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BÁN VÉ • 4 BƯỚC";
            // 
            // lblCashierInfo
            // 
            lblCashierInfo.Dock = DockStyle.Bottom;
            lblCashierInfo.Font = new Font("Segoe UI", 9F);
            lblCashierInfo.Location = new Point(12, 44);
            lblCashierInfo.Name = "lblCashierInfo";
            lblCashierInfo.Size = new Size(1026, 24);
            lblCashierInfo.TabIndex = 1;
            lblCashierInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlBody
            // 
            pnlBody.ColumnCount = 1;
            pnlBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlBody.Controls.Add(bookingPages, 0, 0);
            pnlBody.Controls.Add(navigation, 0, 1);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 72);
            pnlBody.Name = "pnlBody";
            pnlBody.RowCount = 2;
            pnlBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            pnlBody.Size = new Size(1050, 648);
            pnlBody.TabIndex = 0;
            // 
            // bookingPages
            // 
            bookingPages.Controls.Add(showtimeTab);
            bookingPages.Controls.Add(seatTab);
            bookingPages.Controls.Add(foodTab);
            bookingPages.Controls.Add(paymentTab);
            bookingPages.Dock = DockStyle.Fill;
            bookingPages.Location = new Point(3, 3);
            bookingPages.Name = "bookingPages";
            bookingPages.SelectedIndex = 0;
            bookingPages.Size = new Size(1044, 598);
            bookingPages.TabIndex = 0;
            // 
            // showtimeTab
            // 
            showtimeTab.Controls.Add(showtimePage);
            showtimeTab.Location = new Point(4, 32);
            showtimeTab.Name = "showtimeTab";
            showtimeTab.Size = new Size(1036, 562);
            showtimeTab.TabIndex = 0;
            showtimeTab.Text = "1. Suất chiếu";
            // 
            // showtimePage
            // 
            showtimePage.Dock = DockStyle.Fill;
            showtimePage.Font = new Font("Segoe UI", 10F);
            showtimePage.Location = new Point(0, 0);
            showtimePage.Name = "showtimePage";
            showtimePage.Size = new Size(1036, 562);
            showtimePage.TabIndex = 0;
            // 
            // seatTab
            // 
            seatTab.Controls.Add(seatPage);
            seatTab.Location = new Point(4, 29);
            seatTab.Name = "seatTab";
            seatTab.Size = new Size(1036, 565);
            seatTab.TabIndex = 1;
            seatTab.Text = "2. Chọn ghế";
            // 
            // seatPage
            // 
            seatPage.Dock = DockStyle.Fill;
            seatPage.Font = new Font("Segoe UI", 10F);
            seatPage.Location = new Point(0, 0);
            seatPage.Name = "seatPage";
            seatPage.Size = new Size(1036, 565);
            seatPage.TabIndex = 0;
            // 
            // paymentTab
            // 
            foodTab.Name = "foodTab";
            foodTab.Text = "3. Bắp nước";
            foodTab.TabIndex = 2;
            foodTab.Controls.Add(foodPage);
            foodPage.Name = "foodPage";
            foodPage.Dock = DockStyle.Fill;
            paymentTab.Controls.Add(paymentPage);
            paymentTab.Location = new Point(4, 29);
            paymentTab.Name = "paymentTab";
            paymentTab.Size = new Size(1036, 565);
            paymentTab.TabIndex = 3;
            paymentTab.Text = "4. Thanh toán";
            // 
            // paymentPage
            // 
            paymentPage.Dock = DockStyle.Fill;
            paymentPage.Font = new Font("Segoe UI", 10F);
            paymentPage.Location = new Point(0, 0);
            paymentPage.Name = "paymentPage";
            paymentPage.Size = new Size(1036, 565);
            paymentPage.TabIndex = 0;
            // 
            // navigation
            // 
            navigation.ColumnCount = 3;
            navigation.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165F));
            navigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            navigation.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            navigation.Controls.Add(_stepBack, 0, 0);
            navigation.Controls.Add(_stepSummary, 1, 0);
            navigation.Controls.Add(_stepNext, 2, 0);
            navigation.Dock = DockStyle.Fill;
            navigation.Location = new Point(3, 607);
            navigation.Name = "navigation";
            navigation.Padding = new Padding(4, 8, 4, 8);
            navigation.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            navigation.Size = new Size(1044, 58);
            navigation.TabIndex = 1;
            // 
            // _stepBack
            // 
            _stepBack.Dock = DockStyle.Fill;
            _stepBack.Location = new Point(7, 11);
            _stepBack.Name = "_stepBack";
            _stepBack.Size = new Size(159, 36);
            _stepBack.TabIndex = 0;
            _stepBack.Text = "← Quay lại";
            // 
            // _stepSummary
            // 
            _stepSummary.Dock = DockStyle.Fill;
            _stepSummary.Location = new Point(172, 8);
            _stepSummary.Name = "_stepSummary";
            _stepSummary.Size = new Size(675, 42);
            _stepSummary.TabIndex = 1;
            _stepSummary.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _stepNext
            // 
            _stepNext.BackColor = Color.FromArgb(0, 150, 130);
            _stepNext.Dock = DockStyle.Fill;
            _stepNext.FlatStyle = FlatStyle.Flat;
            _stepNext.ForeColor = Color.White;
            _stepNext.Location = new Point(853, 11);
            _stepNext.Name = "_stepNext";
            _stepNext.Size = new Size(184, 36);
            _stepNext.TabIndex = 2;
            _stepNext.Text = "Tiếp tục →";
            _stepNext.UseVisualStyleBackColor = false;
            // 
            // cboPhim
            // 
            cboPhim.Location = new Point(0, 0);
            cboPhim.Name = "cboPhim";
            cboPhim.Size = new Size(121, 28);
            cboPhim.TabIndex = 0;
            cboPhim.Visible = false;
            // 
            // lblSec1
            // 
            lblSec1.Location = new Point(0, 0);
            lblSec1.Name = "lblSec1";
            lblSec1.Size = new Size(100, 23);
            lblSec1.TabIndex = 0;
            lblSec1.Visible = false;
            // 
            // ucBanHang
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlBody);
            Controls.Add(pnlTop);
            Font = new Font("Segoe UI", 10F);
            Name = "ucBanHang";
            Size = new Size(1050, 720);
            pnlTop.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            bookingPages.ResumeLayout(false);
            showtimeTab.ResumeLayout(false);
            seatTab.ResumeLayout(false);
            paymentTab.ResumeLayout(false);
            navigation.ResumeLayout(false);
            ResumeLayout(false);
        }
        internal Panel pnlTop;
        internal Label lblTitle;
        internal Label lblCashierInfo;
        internal TableLayoutPanel pnlBody;
        internal TabControl bookingPages;
        internal TabPage showtimeTab;
        internal TabPage seatTab;
        internal TabPage foodTab;
        internal ucBanHangFood foodPage;
        internal TabPage paymentTab;
        internal ucBanHangShowtimes showtimePage;
        internal ucBanHangSeats seatPage;
        internal ucBanHangPayment paymentPage;
        internal TableLayoutPanel navigation;
        internal Button _stepBack;
        internal Button _stepNext;
        internal Label _stepSummary;
        internal ComboBox cboPhim;
        internal Label lblSec1;
    }
}
