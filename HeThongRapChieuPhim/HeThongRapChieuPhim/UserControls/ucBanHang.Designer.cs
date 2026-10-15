namespace HeThongRapChieuPhim.UserControls
{
    partial class ucBanHang
    {
        private System.ComponentModel.IContainer components = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblTitle = new Label();
            lblCashierInfo = new Label();
            pnlBody = new TableLayoutPanel();
            pnlLeft = new Panel();
            pnlFoodList = new FlowLayoutPanel();
            lblSec4 = new Label();
            lblComboNote = new Label();
            cboCombo = new ComboBox();
            lblSec3 = new Label();
            lblRoomInfo = new Label();
            cboSuatChieu = new ComboBox();
            lblSec2 = new Label();
            pnlMovieMeta = new Panel();
            lblMovieInfo = new Label();
            picPoster = new PictureBox();
            cboPhim = new ComboBox();
            lblSec1 = new Label();
            pnlCenter = new Panel();
            pnlSeatMap = new Panel();
            lblScreen = new Label();
            pnlSeatHeader = new Panel();
            pnlLegend = new Panel();
            pnlLegendAvailable = new Panel();
            boxLegendAvailable = new Panel();
            lblLegendAvailable = new Label();
            pnlLegendSelected = new Panel();
            boxLegendSelected = new Panel();
            lblLegendSelected = new Label();
            pnlLegendBooked = new Panel();
            boxLegendBooked = new Panel();
            lblLegendBooked = new Label();
            lblSeatMapTitle = new Label();
            pnlRight = new Panel();
            btnReset = new Button();
            btnConfirm = new Button();
            grpPayment = new GroupBox();
            btnShowQR = new Button();
            radTransfer = new RadioButton();
            radCash = new RadioButton();
            grpVoucher = new GroupBox();
            lblVoucherStatus = new Label();
            btnHuyVoucher = new Button();
            btnApDungVoucher = new Button();
            txtVoucherCode = new TextBox();
            pnlBillDetails = new Panel();
            lblSumTotal = new Label();
            divLine = new Panel();
            lblSumFood = new Label();
            lblSumVoucher = new Label();
            lblSumDiscount = new Label();
            lblSumTicketPrice = new Label();
            lblSumSeats = new Label();
            lblSumShow = new Label();
            lblSumMovie = new Label();
            lblBillTitle = new Label();
            pnlTop.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlLeft.SuspendLayout();
            pnlMovieMeta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPoster).BeginInit();
            pnlCenter.SuspendLayout();
            pnlSeatHeader.SuspendLayout();
            pnlLegend.SuspendLayout();
            pnlLegendAvailable.SuspendLayout();
            pnlLegendSelected.SuspendLayout();
            pnlLegendBooked.SuspendLayout();
            pnlRight.SuspendLayout();
            grpPayment.SuspendLayout();
            grpVoucher.SuspendLayout();
            pnlBillDetails.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblCashierInfo);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(15, 8, 15, 8);
            pnlTop.Size = new Size(1200, 50);
            pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 83, 45);
            lblTitle.Location = new Point(15, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(558, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🎟️ BÁN VÉ XEM PHIM & BẮP NƯỚC - NHH CINEMA";
            // 
            // lblCashierInfo
            // 
            lblCashierInfo.AutoSize = true;
            lblCashierInfo.Dock = DockStyle.Right;
            lblCashierInfo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCashierInfo.ForeColor = Color.FromArgb(16, 185, 129);
            lblCashierInfo.Location = new Point(1046, 8);
            lblCashierInfo.Name = "lblCashierInfo";
            lblCashierInfo.Size = new Size(139, 25);
            lblCashierInfo.TabIndex = 1;
            lblCashierInfo.Text = "👤 Nhân viên: ";
            lblCashierInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlBody
            // 
            pnlBody.ColumnCount = 3;
            pnlBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            pnlBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            pnlBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            pnlBody.Controls.Add(pnlLeft, 0, 0);
            pnlBody.Controls.Add(pnlCenter, 1, 0);
            pnlBody.Controls.Add(pnlRight, 2, 0);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 50);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(10);
            pnlBody.RowCount = 1;
            pnlBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlBody.Size = new Size(1200, 700);
            pnlBody.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.AutoScroll = true;
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(pnlFoodList);
            pnlLeft.Controls.Add(lblSec4);
            pnlLeft.Controls.Add(lblComboNote);
            pnlLeft.Controls.Add(cboCombo);
            pnlLeft.Controls.Add(lblSec3);
            pnlLeft.Controls.Add(lblRoomInfo);
            pnlLeft.Controls.Add(cboSuatChieu);
            pnlLeft.Controls.Add(lblSec2);
            pnlLeft.Controls.Add(pnlMovieMeta);
            pnlLeft.Controls.Add(cboPhim);
            pnlLeft.Controls.Add(lblSec1);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(14, 14);
            pnlLeft.Margin = new Padding(4);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(12);
            pnlLeft.Size = new Size(346, 672);
            pnlLeft.TabIndex = 0;
            // 
            // pnlFoodList
            // 
            pnlFoodList.AutoScroll = true;
            pnlFoodList.BackColor = Color.FromArgb(248, 250, 249);
            pnlFoodList.Dock = DockStyle.Top;
            pnlFoodList.Location = new Point(12, 437);
            pnlFoodList.Name = "pnlFoodList";
            pnlFoodList.Padding = new Padding(4);
            pnlFoodList.Size = new Size(322, 160);
            pnlFoodList.TabIndex = 10;
            // 
            // lblSec4
            // 
            lblSec4.Dock = DockStyle.Top;
            lblSec4.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSec4.ForeColor = Color.FromArgb(20, 83, 45);
            lblSec4.Location = new Point(12, 412);
            lblSec4.Name = "lblSec4";
            lblSec4.Size = new Size(322, 25);
            lblSec4.TabIndex = 9;
            lblSec4.Text = "4. CHỌN BẮP NƯỚC (F&B)";
            // 
            // lblComboNote
            // 
            lblComboNote.Dock = DockStyle.Top;
            lblComboNote.Font = new Font("Segoe UI", 8.5F);
            lblComboNote.ForeColor = Color.FromArgb(90, 115, 100);
            lblComboNote.Location = new Point(12, 377);
            lblComboNote.Name = "lblComboNote";
            lblComboNote.Size = new Size(322, 35);
            lblComboNote.TabIndex = 8;
            lblComboNote.Text = "Không áp dụng combo ưu đãi";
            // 
            // cboCombo
            // 
            cboCombo.Dock = DockStyle.Top;
            cboCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCombo.Font = new Font("Segoe UI", 10F);
            cboCombo.FormattingEnabled = true;
            cboCombo.Location = new Point(12, 346);
            cboCombo.Name = "cboCombo";
            cboCombo.Size = new Size(322, 31);
            cboCombo.TabIndex = 7;
            cboCombo.SelectedIndexChanged += CboCombo_SelectedIndexChanged;
            // 
            // lblSec3
            // 
            lblSec3.Dock = DockStyle.Top;
            lblSec3.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSec3.ForeColor = Color.FromArgb(20, 83, 45);
            lblSec3.Location = new Point(12, 321);
            lblSec3.Name = "lblSec3";
            lblSec3.Size = new Size(322, 25);
            lblSec3.TabIndex = 6;
            lblSec3.Text = "3. ƯU ĐÃI & COMBO VÉ";
            // 
            // lblRoomInfo
            // 
            lblRoomInfo.Dock = DockStyle.Top;
            lblRoomInfo.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblRoomInfo.ForeColor = Color.FromArgb(90, 115, 100);
            lblRoomInfo.Location = new Point(12, 299);
            lblRoomInfo.Name = "lblRoomInfo";
            lblRoomInfo.Size = new Size(322, 22);
            lblRoomInfo.TabIndex = 5;
            // 
            // cboSuatChieu
            // 
            cboSuatChieu.Dock = DockStyle.Top;
            cboSuatChieu.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSuatChieu.Font = new Font("Segoe UI", 10F);
            cboSuatChieu.FormattingEnabled = true;
            cboSuatChieu.Location = new Point(12, 268);
            cboSuatChieu.Name = "cboSuatChieu";
            cboSuatChieu.Size = new Size(322, 31);
            cboSuatChieu.TabIndex = 4;
            cboSuatChieu.SelectedIndexChanged += CboSuatChieu_SelectedIndexChanged;
            // 
            // lblSec2
            // 
            lblSec2.Dock = DockStyle.Top;
            lblSec2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSec2.ForeColor = Color.FromArgb(20, 83, 45);
            lblSec2.Location = new Point(12, 243);
            lblSec2.Name = "lblSec2";
            lblSec2.Size = new Size(322, 25);
            lblSec2.TabIndex = 3;
            lblSec2.Text = "2. CHỌN SUẤT CHIẾU";
            // 
            // pnlMovieMeta
            // 
            pnlMovieMeta.Controls.Add(lblMovieInfo);
            pnlMovieMeta.Controls.Add(picPoster);
            pnlMovieMeta.Dock = DockStyle.Top;
            pnlMovieMeta.Location = new Point(12, 68);
            pnlMovieMeta.Name = "pnlMovieMeta";
            pnlMovieMeta.Padding = new Padding(0, 6, 0, 6);
            pnlMovieMeta.Size = new Size(322, 175);
            pnlMovieMeta.TabIndex = 2;
            // 
            // lblMovieInfo
            // 
            lblMovieInfo.Dock = DockStyle.Fill;
            lblMovieInfo.Font = new Font("Segoe UI", 9F);
            lblMovieInfo.ForeColor = Color.FromArgb(20, 50, 35);
            lblMovieInfo.Location = new Point(110, 6);
            lblMovieInfo.Name = "lblMovieInfo";
            lblMovieInfo.Padding = new Padding(8, 0, 0, 0);
            lblMovieInfo.Size = new Size(212, 163);
            lblMovieInfo.TabIndex = 1;
            // 
            // picPoster
            // 
            picPoster.BackColor = Color.FromArgb(240, 245, 241);
            picPoster.BorderStyle = BorderStyle.FixedSingle;
            picPoster.Dock = DockStyle.Left;
            picPoster.Location = new Point(0, 6);
            picPoster.Name = "picPoster";
            picPoster.Size = new Size(110, 163);
            picPoster.SizeMode = PictureBoxSizeMode.Zoom;
            picPoster.TabIndex = 0;
            picPoster.TabStop = false;
            // 
            // cboPhim
            // 
            cboPhim.Dock = DockStyle.Top;
            cboPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhim.Font = new Font("Segoe UI", 10F);
            cboPhim.FormattingEnabled = true;
            cboPhim.Location = new Point(12, 37);
            cboPhim.Name = "cboPhim";
            cboPhim.Size = new Size(322, 31);
            cboPhim.TabIndex = 1;
            cboPhim.SelectedIndexChanged += CboPhim_SelectedIndexChanged;
            // 
            // lblSec1
            // 
            lblSec1.Dock = DockStyle.Top;
            lblSec1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSec1.ForeColor = Color.FromArgb(20, 83, 45);
            lblSec1.Location = new Point(12, 12);
            lblSec1.Name = "lblSec1";
            lblSec1.Size = new Size(322, 25);
            lblSec1.TabIndex = 0;
            lblSec1.Text = "1. CHỌN PHIM";
            // 
            // pnlCenter
            // 
            pnlCenter.BackColor = Color.White;
            pnlCenter.Controls.Add(pnlSeatMap);
            pnlCenter.Controls.Add(lblScreen);
            pnlCenter.Controls.Add(pnlSeatHeader);
            pnlCenter.Dock = DockStyle.Fill;
            pnlCenter.Location = new Point(368, 14);
            pnlCenter.Margin = new Padding(4);
            pnlCenter.Name = "pnlCenter";
            pnlCenter.Padding = new Padding(12);
            pnlCenter.Size = new Size(523, 672);
            pnlCenter.TabIndex = 1;
            // 
            // pnlSeatMap
            // 
            pnlSeatMap.AutoScroll = true;
            pnlSeatMap.BackColor = Color.FromArgb(248, 250, 249);
            pnlSeatMap.Dock = DockStyle.Fill;
            pnlSeatMap.Location = new Point(12, 105);
            pnlSeatMap.Name = "pnlSeatMap";
            pnlSeatMap.Padding = new Padding(15);
            pnlSeatMap.Size = new Size(499, 555);
            pnlSeatMap.TabIndex = 2;
            // 
            // lblScreen
            // 
            lblScreen.BackColor = Color.FromArgb(226, 237, 228);
            lblScreen.Dock = DockStyle.Top;
            lblScreen.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblScreen.ForeColor = Color.FromArgb(20, 83, 45);
            lblScreen.Location = new Point(12, 77);
            lblScreen.Name = "lblScreen";
            lblScreen.Size = new Size(499, 28);
            lblScreen.TabIndex = 1;
            lblScreen.Text = "📺 MÀN HÌNH CHIẾU";
            lblScreen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlSeatHeader
            // 
            pnlSeatHeader.Controls.Add(pnlLegend);
            pnlSeatHeader.Controls.Add(lblSeatMapTitle);
            pnlSeatHeader.Dock = DockStyle.Top;
            pnlSeatHeader.Location = new Point(12, 12);
            pnlSeatHeader.Name = "pnlSeatHeader";
            pnlSeatHeader.Size = new Size(499, 65);
            pnlSeatHeader.TabIndex = 0;
            // 
            // pnlLegend
            // 
            pnlLegend.Controls.Add(pnlLegendAvailable);
            pnlLegend.Controls.Add(pnlLegendSelected);
            pnlLegend.Controls.Add(pnlLegendBooked);
            pnlLegend.Dock = DockStyle.Top;
            pnlLegend.Location = new Point(0, 25);
            pnlLegend.Name = "pnlLegend";
            pnlLegend.Size = new Size(499, 35);
            pnlLegend.TabIndex = 1;
            // 
            // pnlLegendAvailable
            // 
            pnlLegendAvailable.Controls.Add(boxLegendAvailable);
            pnlLegendAvailable.Controls.Add(lblLegendAvailable);
            pnlLegendAvailable.Location = new Point(10, 5);
            pnlLegendAvailable.Name = "pnlLegendAvailable";
            pnlLegendAvailable.Size = new Size(100, 24);
            pnlLegendAvailable.TabIndex = 0;
            // 
            // boxLegendAvailable
            // 
            boxLegendAvailable.BackColor = Color.FromArgb(218, 230, 222);
            boxLegendAvailable.BorderStyle = BorderStyle.FixedSingle;
            boxLegendAvailable.Location = new Point(0, 4);
            boxLegendAvailable.Name = "boxLegendAvailable";
            boxLegendAvailable.Size = new Size(16, 16);
            boxLegendAvailable.TabIndex = 0;
            // 
            // lblLegendAvailable
            // 
            lblLegendAvailable.AutoSize = true;
            lblLegendAvailable.Font = new Font("Segoe UI", 8.5F);
            lblLegendAvailable.ForeColor = Color.FromArgb(20, 50, 35);
            lblLegendAvailable.Location = new Point(20, 4);
            lblLegendAvailable.Name = "lblLegendAvailable";
            lblLegendAvailable.Size = new Size(75, 20);
            lblLegendAvailable.TabIndex = 1;
            lblLegendAvailable.Text = "Ghế trống";
            // 
            // pnlLegendSelected
            // 
            pnlLegendSelected.Controls.Add(boxLegendSelected);
            pnlLegendSelected.Controls.Add(lblLegendSelected);
            pnlLegendSelected.Location = new Point(120, 5);
            pnlLegendSelected.Name = "pnlLegendSelected";
            pnlLegendSelected.Size = new Size(100, 24);
            pnlLegendSelected.TabIndex = 1;
            // 
            // boxLegendSelected
            // 
            boxLegendSelected.BackColor = Color.FromArgb(16, 185, 129);
            boxLegendSelected.BorderStyle = BorderStyle.FixedSingle;
            boxLegendSelected.Location = new Point(0, 4);
            boxLegendSelected.Name = "boxLegendSelected";
            boxLegendSelected.Size = new Size(16, 16);
            boxLegendSelected.TabIndex = 0;
            // 
            // lblLegendSelected
            // 
            lblLegendSelected.AutoSize = true;
            lblLegendSelected.Font = new Font("Segoe UI", 8.5F);
            lblLegendSelected.ForeColor = Color.FromArgb(20, 50, 35);
            lblLegendSelected.Location = new Point(20, 4);
            lblLegendSelected.Name = "lblLegendSelected";
            lblLegendSelected.Size = new Size(81, 20);
            lblLegendSelected.TabIndex = 1;
            lblLegendSelected.Text = "Đang chọn";
            // 
            // pnlLegendBooked
            // 
            pnlLegendBooked.Controls.Add(boxLegendBooked);
            pnlLegendBooked.Controls.Add(lblLegendBooked);
            pnlLegendBooked.Location = new Point(230, 5);
            pnlLegendBooked.Name = "pnlLegendBooked";
            pnlLegendBooked.Size = new Size(100, 24);
            pnlLegendBooked.TabIndex = 2;
            // 
            // boxLegendBooked
            // 
            boxLegendBooked.BackColor = Color.FromArgb(239, 68, 68);
            boxLegendBooked.BorderStyle = BorderStyle.FixedSingle;
            boxLegendBooked.Location = new Point(0, 4);
            boxLegendBooked.Name = "boxLegendBooked";
            boxLegendBooked.Size = new Size(16, 16);
            boxLegendBooked.TabIndex = 0;
            // 
            // lblLegendBooked
            // 
            lblLegendBooked.AutoSize = true;
            lblLegendBooked.Font = new Font("Segoe UI", 8.5F);
            lblLegendBooked.ForeColor = Color.FromArgb(20, 50, 35);
            lblLegendBooked.Location = new Point(20, 4);
            lblLegendBooked.Name = "lblLegendBooked";
            lblLegendBooked.Size = new Size(57, 20);
            lblLegendBooked.TabIndex = 1;
            lblLegendBooked.Text = "Đã bán";
            // 
            // lblSeatMapTitle
            // 
            lblSeatMapTitle.Dock = DockStyle.Top;
            lblSeatMapTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblSeatMapTitle.ForeColor = Color.FromArgb(20, 83, 45);
            lblSeatMapTitle.Location = new Point(0, 0);
            lblSeatMapTitle.Name = "lblSeatMapTitle";
            lblSeatMapTitle.Size = new Size(499, 25);
            lblSeatMapTitle.TabIndex = 0;
            lblSeatMapTitle.Text = "SƠ ĐỒ CHỌN GHẾ NGỒI";
            // 
            // pnlRight
            // 
            pnlRight.AutoScroll = true;
            pnlRight.BackColor = Color.White;
            pnlRight.Controls.Add(btnReset);
            pnlRight.Controls.Add(btnConfirm);
            pnlRight.Controls.Add(grpPayment);
            pnlRight.Controls.Add(grpVoucher);
            pnlRight.Controls.Add(pnlBillDetails);
            pnlRight.Controls.Add(lblBillTitle);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(899, 14);
            pnlRight.Margin = new Padding(4);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(12);
            pnlRight.Size = new Size(287, 672);
            pnlRight.TabIndex = 2;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(230, 238, 233);
            btnReset.Cursor = Cursors.Hand;
            btnReset.Dock = DockStyle.Top;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9.5F);
            btnReset.ForeColor = Color.FromArgb(20, 50, 35);
            btnReset.Location = new Point(12, 515);
            btnReset.Margin = new Padding(0, 8, 0, 0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(263, 36);
            btnReset.TabIndex = 4;
            btnReset.Text = "🔄 Làm mới / Hủy chọn";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += BtnReset_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(16, 163, 74);
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.Dock = DockStyle.Top;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(12, 467);
            btnConfirm.Margin = new Padding(0, 15, 0, 0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(263, 48);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "XÁC NHẬN BÁN VÉ";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += BtnConfirm_Click;
            // 
            // grpPayment
            // 
            grpPayment.Controls.Add(btnShowQR);
            grpPayment.Controls.Add(radTransfer);
            grpPayment.Controls.Add(radCash);
            grpPayment.Dock = DockStyle.Top;
            grpPayment.ForeColor = Color.FromArgb(20, 83, 45);
            grpPayment.Location = new Point(12, 357);
            grpPayment.Margin = new Padding(0, 10, 0, 0);
            grpPayment.Name = "grpPayment";
            grpPayment.Padding = new Padding(10);
            grpPayment.Size = new Size(263, 110);
            grpPayment.TabIndex = 2;
            grpPayment.TabStop = false;
            grpPayment.Text = " Phương thức thanh toán ";
            // 
            // btnShowQR
            // 
            btnShowQR.BackColor = Color.FromArgb(59, 130, 246);
            btnShowQR.Cursor = Cursors.Hand;
            btnShowQR.FlatAppearance.BorderSize = 0;
            btnShowQR.FlatStyle = FlatStyle.Flat;
            btnShowQR.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnShowQR.ForeColor = Color.White;
            btnShowQR.Location = new Point(160, 52);
            btnShowQR.Name = "btnShowQR";
            btnShowQR.Size = new Size(70, 26);
            btnShowQR.TabIndex = 2;
            btnShowQR.Text = "Mã QR";
            btnShowQR.UseVisualStyleBackColor = false;
            btnShowQR.Visible = false;
            btnShowQR.Click += BtnShowQR_Click;
            // 
            // radTransfer
            // 
            radTransfer.AutoSize = true;
            radTransfer.ForeColor = Color.FromArgb(20, 50, 35);
            radTransfer.Location = new Point(15, 55);
            radTransfer.Name = "radTransfer";
            radTransfer.Size = new Size(197, 27);
            radTransfer.TabIndex = 1;
            radTransfer.Text = "📱 Chuyển khoản QR";
            radTransfer.UseVisualStyleBackColor = true;
            radTransfer.CheckedChanged += RadTransfer_CheckedChanged;
            // 
            // radCash
            // 
            radCash.AutoSize = true;
            radCash.Checked = true;
            radCash.ForeColor = Color.FromArgb(20, 50, 35);
            radCash.Location = new Point(15, 25);
            radCash.Name = "radCash";
            radCash.Size = new Size(126, 27);
            radCash.TabIndex = 0;
            radCash.TabStop = true;
            radCash.Text = "💵 Tiền mặt";
            radCash.UseVisualStyleBackColor = true;
            // 
            // grpVoucher
            // 
            grpVoucher.Controls.Add(lblVoucherStatus);
            grpVoucher.Controls.Add(btnHuyVoucher);
            grpVoucher.Controls.Add(btnApDungVoucher);
            grpVoucher.Controls.Add(txtVoucherCode);
            grpVoucher.Dock = DockStyle.Top;
            grpVoucher.ForeColor = Color.FromArgb(20, 83, 45);
            grpVoucher.Location = new Point(12, 272);
            grpVoucher.Margin = new Padding(0, 8, 0, 0);
            grpVoucher.Name = "grpVoucher";
            grpVoucher.Padding = new Padding(10, 6, 10, 6);
            grpVoucher.Size = new Size(263, 85);
            grpVoucher.TabIndex = 2;
            grpVoucher.TabStop = false;
            grpVoucher.Text = " 🎟️ Mã Voucher Giảm Giá ";
            // 
            // lblVoucherStatus
            // 
            lblVoucherStatus.Font = new Font("Segoe UI", 8.25F);
            lblVoucherStatus.ForeColor = Color.FromArgb(90, 115, 100);
            lblVoucherStatus.Location = new Point(10, 57);
            lblVoucherStatus.Name = "lblVoucherStatus";
            lblVoucherStatus.Size = new Size(243, 20);
            lblVoucherStatus.TabIndex = 3;
            lblVoucherStatus.Text = "Nhập mã voucher để được giảm thêm";
            // 
            // btnHuyVoucher
            // 
            btnHuyVoucher.BackColor = Color.FromArgb(239, 68, 68);
            btnHuyVoucher.Cursor = Cursors.Hand;
            btnHuyVoucher.FlatAppearance.BorderSize = 0;
            btnHuyVoucher.FlatStyle = FlatStyle.Flat;
            btnHuyVoucher.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuyVoucher.ForeColor = Color.White;
            btnHuyVoucher.Location = new Point(176, 23);
            btnHuyVoucher.Name = "btnHuyVoucher";
            btnHuyVoucher.Size = new Size(77, 30);
            btnHuyVoucher.TabIndex = 2;
            btnHuyVoucher.Text = "✕ Bỏ";
            btnHuyVoucher.UseVisualStyleBackColor = false;
            btnHuyVoucher.Visible = false;
            btnHuyVoucher.Click += BtnHuyVoucher_Click;
            // 
            // btnApDungVoucher
            // 
            btnApDungVoucher.BackColor = Color.FromArgb(16, 185, 129);
            btnApDungVoucher.Cursor = Cursors.Hand;
            btnApDungVoucher.FlatAppearance.BorderSize = 0;
            btnApDungVoucher.FlatStyle = FlatStyle.Flat;
            btnApDungVoucher.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApDungVoucher.ForeColor = Color.White;
            btnApDungVoucher.Location = new Point(176, 23);
            btnApDungVoucher.Name = "btnApDungVoucher";
            btnApDungVoucher.Size = new Size(77, 30);
            btnApDungVoucher.TabIndex = 1;
            btnApDungVoucher.Text = "Áp dụng";
            btnApDungVoucher.UseVisualStyleBackColor = false;
            btnApDungVoucher.Click += BtnApDungVoucher_Click;
            // 
            // txtVoucherCode
            // 
            txtVoucherCode.BackColor = Color.White;
            txtVoucherCode.BorderStyle = BorderStyle.FixedSingle;
            txtVoucherCode.CharacterCasing = CharacterCasing.Upper;
            txtVoucherCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtVoucherCode.ForeColor = Color.FromArgb(20, 50, 35);
            txtVoucherCode.Location = new Point(10, 24);
            txtVoucherCode.MaxLength = 20;
            txtVoucherCode.Name = "txtVoucherCode";
            txtVoucherCode.PlaceholderText = "Nhập mã voucher...";
            txtVoucherCode.Size = new Size(160, 29);
            txtVoucherCode.TabIndex = 0;
            txtVoucherCode.KeyDown += TxtVoucherCode_KeyDown;
            // 
            // pnlBillDetails
            // 
            pnlBillDetails.BackColor = Color.FromArgb(248, 250, 249);
            pnlBillDetails.Controls.Add(lblSumTotal);
            pnlBillDetails.Controls.Add(divLine);
            pnlBillDetails.Controls.Add(lblSumFood);
            pnlBillDetails.Controls.Add(lblSumVoucher);
            pnlBillDetails.Controls.Add(lblSumDiscount);
            pnlBillDetails.Controls.Add(lblSumTicketPrice);
            pnlBillDetails.Controls.Add(lblSumSeats);
            pnlBillDetails.Controls.Add(lblSumShow);
            pnlBillDetails.Controls.Add(lblSumMovie);
            pnlBillDetails.Dock = DockStyle.Top;
            pnlBillDetails.Location = new Point(12, 42);
            pnlBillDetails.Name = "pnlBillDetails";
            pnlBillDetails.Padding = new Padding(10);
            pnlBillDetails.Size = new Size(263, 230);
            pnlBillDetails.TabIndex = 1;
            // 
            // lblSumTotal
            // 
            lblSumTotal.Dock = DockStyle.Top;
            lblSumTotal.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblSumTotal.ForeColor = Color.FromArgb(217, 119, 6);
            lblSumTotal.Location = new Point(10, 170);
            lblSumTotal.Name = "lblSumTotal";
            lblSumTotal.Size = new Size(243, 55);
            lblSumTotal.TabIndex = 7;
            lblSumTotal.Text = "TỔNG TIỀN:\r\n0 đ";
            lblSumTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // divLine
            // 
            divLine.BackColor = Color.FromArgb(218, 232, 222);
            divLine.Dock = DockStyle.Top;
            divLine.Location = new Point(10, 168);
            divLine.Margin = new Padding(0, 4, 0, 4);
            divLine.Name = "divLine";
            divLine.Size = new Size(243, 2);
            divLine.TabIndex = 6;
            // 
            // lblSumFood
            // 
            lblSumFood.Dock = DockStyle.Top;
            lblSumFood.Font = new Font("Segoe UI", 9F);
            lblSumFood.ForeColor = Color.FromArgb(20, 50, 35);
            lblSumFood.Location = new Point(10, 146);
            lblSumFood.Name = "lblSumFood";
            lblSumFood.Size = new Size(243, 22);
            lblSumFood.TabIndex = 5;
            lblSumFood.Text = "Tiền đồ ăn: 0 đ";
            // 
            // lblSumVoucher
            // 
            lblSumVoucher.Dock = DockStyle.Top;
            lblSumVoucher.Font = new Font("Segoe UI", 9F);
            lblSumVoucher.ForeColor = Color.FromArgb(16, 185, 129);
            lblSumVoucher.Location = new Point(10, 124);
            lblSumVoucher.Name = "lblSumVoucher";
            lblSumVoucher.Size = new Size(243, 22);
            lblSumVoucher.TabIndex = 8;
            lblSumVoucher.Text = "🎟️ Voucher: Chưa áp dụng";
            // 
            // lblSumDiscount
            // 
            lblSumDiscount.Dock = DockStyle.Top;
            lblSumDiscount.Font = new Font("Segoe UI", 9F);
            lblSumDiscount.ForeColor = Color.FromArgb(239, 68, 68);
            lblSumDiscount.Location = new Point(10, 102);
            lblSumDiscount.Name = "lblSumDiscount";
            lblSumDiscount.Size = new Size(243, 22);
            lblSumDiscount.TabIndex = 4;
            lblSumDiscount.Text = "Ưu đãi giảm: 0 đ";
            // 
            // lblSumTicketPrice
            // 
            lblSumTicketPrice.Dock = DockStyle.Top;
            lblSumTicketPrice.Font = new Font("Segoe UI", 9F);
            lblSumTicketPrice.ForeColor = Color.FromArgb(20, 50, 35);
            lblSumTicketPrice.Location = new Point(10, 80);
            lblSumTicketPrice.Name = "lblSumTicketPrice";
            lblSumTicketPrice.Size = new Size(243, 22);
            lblSumTicketPrice.TabIndex = 3;
            lblSumTicketPrice.Text = "Tiền vé: 0 đ";
            // 
            // lblSumSeats
            // 
            lblSumSeats.Dock = DockStyle.Top;
            lblSumSeats.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSumSeats.ForeColor = Color.FromArgb(20, 83, 45);
            lblSumSeats.Location = new Point(10, 56);
            lblSumSeats.Name = "lblSumSeats";
            lblSumSeats.Size = new Size(243, 24);
            lblSumSeats.TabIndex = 2;
            lblSumSeats.Text = "Ghế chọn: 0 ghế ()";
            // 
            // lblSumShow
            // 
            lblSumShow.Dock = DockStyle.Top;
            lblSumShow.Font = new Font("Segoe UI", 9F);
            lblSumShow.ForeColor = Color.FromArgb(90, 115, 100);
            lblSumShow.Location = new Point(10, 34);
            lblSumShow.Name = "lblSumShow";
            lblSumShow.Size = new Size(243, 22);
            lblSumShow.TabIndex = 1;
            lblSumShow.Text = "Suất chiếu: -";
            // 
            // lblSumMovie
            // 
            lblSumMovie.Dock = DockStyle.Top;
            lblSumMovie.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSumMovie.ForeColor = Color.FromArgb(20, 50, 35);
            lblSumMovie.Location = new Point(10, 10);
            lblSumMovie.Name = "lblSumMovie";
            lblSumMovie.Size = new Size(243, 24);
            lblSumMovie.TabIndex = 0;
            lblSumMovie.Text = "Phim: -";
            // 
            // lblBillTitle
            // 
            lblBillTitle.Dock = DockStyle.Top;
            lblBillTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblBillTitle.ForeColor = Color.FromArgb(20, 83, 45);
            lblBillTitle.Location = new Point(12, 12);
            lblBillTitle.Name = "lblBillTitle";
            lblBillTitle.Size = new Size(263, 30);
            lblBillTitle.TabIndex = 0;
            lblBillTitle.Text = "CHI TIẾT THANH TOÁN";
            // 
            // ucBanHang
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 245, 241);
            Controls.Add(pnlBody);
            Controls.Add(pnlTop);
            Font = new Font("Segoe UI", 10F);
            Name = "ucBanHang";
            Size = new Size(1200, 750);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlMovieMeta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPoster).EndInit();
            pnlCenter.ResumeLayout(false);
            pnlSeatHeader.ResumeLayout(false);
            pnlLegend.ResumeLayout(false);
            pnlLegendAvailable.ResumeLayout(false);
            pnlLegendAvailable.PerformLayout();
            pnlLegendSelected.ResumeLayout(false);
            pnlLegendSelected.PerformLayout();
            pnlLegendBooked.ResumeLayout(false);
            pnlLegendBooked.PerformLayout();
            pnlRight.ResumeLayout(false);
            grpPayment.ResumeLayout(false);
            grpPayment.PerformLayout();
            grpVoucher.ResumeLayout(false);
            grpVoucher.PerformLayout();
            pnlBillDetails.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCashierInfo;
        private System.Windows.Forms.TableLayoutPanel pnlBody;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblSec1;
        private System.Windows.Forms.ComboBox cboPhim;
        private System.Windows.Forms.Panel pnlMovieMeta;
        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.Label lblMovieInfo;
        private System.Windows.Forms.Label lblSec2;
        private System.Windows.Forms.ComboBox cboSuatChieu;
        private System.Windows.Forms.Label lblRoomInfo;
        private System.Windows.Forms.Label lblSec3;
        private System.Windows.Forms.ComboBox cboCombo;
        private System.Windows.Forms.Label lblComboNote;
        private System.Windows.Forms.Label lblSec4;
        private System.Windows.Forms.FlowLayoutPanel pnlFoodList;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Panel pnlSeatHeader;
        private System.Windows.Forms.Label lblSeatMapTitle;
        private System.Windows.Forms.Panel pnlLegend;
        private System.Windows.Forms.Panel pnlLegendAvailable;
        private System.Windows.Forms.Panel boxLegendAvailable;
        private System.Windows.Forms.Label lblLegendAvailable;
        private System.Windows.Forms.Panel pnlLegendSelected;
        private System.Windows.Forms.Panel boxLegendSelected;
        private System.Windows.Forms.Label lblLegendSelected;
        private System.Windows.Forms.Panel pnlLegendBooked;
        private System.Windows.Forms.Panel boxLegendBooked;
        private System.Windows.Forms.Label lblLegendBooked;
        private System.Windows.Forms.Label lblScreen;
        private System.Windows.Forms.Panel pnlSeatMap;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblBillTitle;
        private System.Windows.Forms.Panel pnlBillDetails;
        private System.Windows.Forms.Label lblSumMovie;
        private System.Windows.Forms.Label lblSumShow;
        private System.Windows.Forms.Label lblSumSeats;
        private System.Windows.Forms.Label lblSumTicketPrice;
        private System.Windows.Forms.Label lblSumDiscount;
        private System.Windows.Forms.Label lblSumFood;
        private System.Windows.Forms.Panel divLine;
        private System.Windows.Forms.Label lblSumTotal;
        private System.Windows.Forms.Label lblSumVoucher;
        private System.Windows.Forms.GroupBox grpVoucher;
        private System.Windows.Forms.TextBox txtVoucherCode;
        private System.Windows.Forms.Button btnApDungVoucher;
        private System.Windows.Forms.Button btnHuyVoucher;
        private System.Windows.Forms.Label lblVoucherStatus;
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.RadioButton radCash;
        private System.Windows.Forms.RadioButton radTransfer;
        private System.Windows.Forms.Button btnShowQR;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnReset;
    }
}
