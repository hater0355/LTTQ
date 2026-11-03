namespace HeThongRapChieuPhim.UIHelpers
{
    partial class MovieItemCard
    {
        private void InitializeComponent()
        {
            poster = new PictureBox();
            poster.Name = "poster";
            title = new Label();
            title.Name = "title";
            detail = new Label();
            detail.Name = "detail";
            buy = new Button();
            buy.Name = "buy";
            SuspendLayout();
            Size = new Size(260, 474);
            Margin = new Padding(12);
            Padding = new Padding(10);
            BackColor = Color.White;
            poster.Dock = DockStyle.Fill;
            poster.SizeMode = PictureBoxSizeMode.Zoom;
            poster.BackColor = Color.FromArgb(231, 236, 241);
            title.Dock = DockStyle.Bottom;
            title.Height = 54;
            title.Text = "Tên phim";
            title.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            title.AutoEllipsis = true;
            detail.Dock = DockStyle.Bottom;
            detail.Height = 68;
            detail.Text = "Thể loại • Thời lượng • Độ tuổi";
            buy.Dock = DockStyle.Bottom;
            buy.Height = 42;
            buy.Text = "MUA VÉ";
            buy.BackColor = Color.FromArgb(0, 83, 150);
            buy.ForeColor = Color.White;
            this.Controls.Add(poster);
            this.Controls.Add(title);
            this.Controls.Add(detail);
            this.Controls.Add(buy);

            Name = "MovieItemCard";
            ResumeLayout(false);
            PerformLayout();
        }
        internal PictureBox poster;
        internal Label title;
        internal Label detail;
        internal Button buy;

    }
}
