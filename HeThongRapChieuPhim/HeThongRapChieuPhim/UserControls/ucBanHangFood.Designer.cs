namespace HeThongRapChieuPhim.UserControls
{
 partial class ucBanHangFood
 {
  private void InitializeComponent()
  {
   flpFoodItems = new FlowLayoutPanel();
   lblFoodHint = new Label();
   lblSec4 = new Label();
   lblFoodEmpty = new Label();
   SuspendLayout();
            this.Name = "tabFood";
            flpFoodItems.Name = "flpFoodItems";
            lblFoodHint.Name = "lblFoodHint";
            lblSec4.Name = "lblSec4";
            lblFoodEmpty.Name = "lblFoodEmpty";
            lblFoodEmpty.AutoSize = false;
            lblFoodEmpty.Size = new Size(280, 52);
            lblFoodEmpty.Text = "Chưa có món nào. Hãy thêm món trong phần quản lý đồ ăn.";
            this.Controls.Add(flpFoodItems);
            this.Controls.Add(lblFoodHint);
            this.Controls.Add(lblSec4);
            this.Name = "tabFood";
            this.Padding = new Padding(8);
            flpFoodItems.AutoScroll = true;
            flpFoodItems.Dock = DockStyle.Fill;
            flpFoodItems.FlowDirection = FlowDirection.TopDown;
            flpFoodItems.Location = new Point(8, 64);
            flpFoodItems.Name = "flpFoodItems";
            flpFoodItems.Padding = new Padding(10, 8, 10, 8);
            flpFoodItems.Size = new Size(510, 543);
            flpFoodItems.TabIndex = 10;
            flpFoodItems.WrapContents = false;
            lblFoodHint.Dock = DockStyle.Top;
            lblFoodHint.Font = new Font("Segoe UI", 9F);
            lblFoodHint.ForeColor = Color.FromArgb(90, 115, 100);
            lblFoodHint.Location = new Point(8, 38);
            lblFoodHint.Name = "lblFoodHint";
            lblFoodHint.Size = new Size(510, 26);
            lblFoodHint.TabIndex = 11;
            lblFoodHint.Text = "Xem giá rồi nhấn + Thêm. Số lượng đã chọn được giữ khi quay lại chọn ghế.";
            lblSec4.Dock = DockStyle.Top;
            lblSec4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSec4.ForeColor = Color.FromArgb(20, 83, 45);
            lblSec4.Location = new Point(8, 8);
            lblSec4.Name = "lblSec4";
            lblSec4.Size = new Size(510, 30);
            lblSec4.TabIndex = 9;
            lblSec4.Text = "ĐỒ ĂN & THỨC UỐNG";
   AutoScaleMode = AutoScaleMode.Font;
   Font = new Font("Segoe UI", 10F);
   BackColor = Color.White;
   Name = "ucBanHangFood";
   Size = new Size(600, 560);
   ResumeLayout(false);
  }
  internal FlowLayoutPanel flpFoodItems;
  internal Label lblFoodHint;
  internal Label lblSec4;
  internal Label lblFoodEmpty;
 }
}