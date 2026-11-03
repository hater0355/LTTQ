namespace HeThongRapChieuPhim.UserControls
{
    partial class ucChonPhim
    {
        private void InitializeComponent()
        {
            _movies = new FlowLayoutPanel();
            previewMovie = new HeThongRapChieuPhim.UIHelpers.MovieItemCard();
            _status = new Label();
            header = new Label();
            movieNavigation = new TableLayoutPanel();
            previousMovies = new Button();
            nextMovies = new Button();
            _movies.SuspendLayout();
            SuspendLayout();
            // 
            // _movies
            // 
            _movies.AutoScroll = true;
            _movies.WrapContents = true;
            _movies.FlowDirection = FlowDirection.TopDown;
            _movies.Controls.Add(previewMovie);
            _movies.Dock = DockStyle.Fill;
            _movies.Location = new Point(0, 96);
            _movies.Name = "_movies";
            _movies.Padding = new Padding(12);
            _movies.Size = new Size(1050, 624);
            _movies.TabIndex = 0;
            // 
            // previewMovie
            // 
            previewMovie.BackColor = Color.White;
            previewMovie.Location = new Point(24, 24);
            previewMovie.Margin = new Padding(12);
            previewMovie.Name = "previewMovie";
            previewMovie.Padding = new Padding(10);
            previewMovie.Size = new Size(260, 474);
            previewMovie.TabIndex = 0;
            // 
            // _status
            // 
            _status.Dock = DockStyle.Top;
            _status.Location = new Point(0, 64);
            _status.Name = "_status";
            _status.Size = new Size(1050, 32);
            _status.TabIndex = 1;
            _status.Text = "Danh sách phim có suất chiếu";
            // 
            // header
            // 
            header.Dock = DockStyle.Top;
            header.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            header.ForeColor = Color.FromArgb(0, 51, 102);
            header.Location = new Point(0, 0);
            header.Name = "header";
            header.Padding = new Padding(24, 20, 0, 0);
            header.Size = new Size(1050, 64);
            header.TabIndex = 2;
            header.Text = "CHỌN PHIM ĐỂ MUA VÉ";
            // Horizontal movie strip with quiet navigation at either edge.
            movieNavigation.Name = "movieNavigation";
            movieNavigation.Dock = DockStyle.Fill;
            movieNavigation.ColumnCount = 3;
            movieNavigation.RowCount = 1;
            movieNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 42F));
            movieNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            movieNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 42F));
            movieNavigation.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            movieNavigation.Controls.Add(previousMovies, 0, 0);
            movieNavigation.Controls.Add(_movies, 1, 0);
            movieNavigation.Controls.Add(nextMovies, 2, 0);
            previousMovies.Name = "previousMovies";
            previousMovies.Text = "❮";
            previousMovies.AccessibleName = "Xem các phim phía trước";
            previousMovies.Anchor = AnchorStyles.None;
            previousMovies.Size = new Size(36, 64);
            previousMovies.FlatStyle = FlatStyle.Flat;
            previousMovies.FlatAppearance.BorderSize = 0;
            previousMovies.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 232, 240);
            previousMovies.ForeColor = Color.FromArgb(155, 170, 188);
            previousMovies.Font = new Font("Segoe UI", 20F);
            previousMovies.Cursor = Cursors.Hand;
            nextMovies.Name = "nextMovies";
            nextMovies.Text = "❯";
            nextMovies.AccessibleName = "Xem các phim tiếp theo";
            nextMovies.Anchor = AnchorStyles.None;
            nextMovies.Size = new Size(36, 64);
            nextMovies.FlatStyle = FlatStyle.Flat;
            nextMovies.FlatAppearance.BorderSize = 0;
            nextMovies.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 232, 240);
            nextMovies.ForeColor = Color.FromArgb(155, 170, 188);
            nextMovies.Font = new Font("Segoe UI", 20F);
            nextMovies.Cursor = Cursors.Hand;
            // 
            // ucChonPhim
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(movieNavigation);
            Controls.Add(_status);
            Controls.Add(header);
            Font = new Font("Segoe UI", 10F);
            Name = "ucChonPhim";
            Size = new Size(1050, 720);
            _movies.ResumeLayout(false);
            ResumeLayout(false);
        }
        internal FlowLayoutPanel _movies;
        internal TableLayoutPanel movieNavigation;
        internal Button previousMovies;
        internal Button nextMovies;
        internal Label _status;
        internal Label header;
        internal HeThongRapChieuPhim.UIHelpers.MovieItemCard previewMovie;

    }
}
