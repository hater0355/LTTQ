using System.Data;
using HeThongRapChieuPhim.Classes;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls;

public partial class ucChonPhim : UserControl
{
    [System.ComponentModel.Category("Layout")]
    [System.ComponentModel.DefaultValue(true)]
    public bool AutoFitCards { get; set; } = true;
    public event Action<string>? MovieSelected;

    public ucChonPhim()
    {
        InitializeComponent();
        _movies.SizeChanged += (_, _) => ResizeCards();
        previousMovies.Click += (_, _) => ScrollMovies(-1);
        nextMovies.Click += (_, _) => ScrollMovies(1);
        _movies.Scroll += (_, _) => UpdateNavigation();
        _movies.Layout += (_, _) => UpdateNavigation();
        _movies.MouseWheel += (_, e) =>
        {
            if (e is HandledMouseEventArgs handled) handled.Handled = true;
            ScrollMovies(e.Delta > 0 ? -1 : 1);
        };
    }

    public void LoadMovies()
    {
        ClearCards();
        try
        {
            var movies = Function.Query(@"SELECT p.MaPhim,p.TenPhim,p.TheLoai,p.ThoiLuong,p.DoTuoi,p.PosterPath
                FROM dbo.Phim p WHERE EXISTS (
                    SELECT 1 FROM dbo.SuatChieu sc WHERE sc.MaPhim=p.MaPhim
                    AND (p.NgayBatDauChieu IS NULL OR sc.NgayChieu>=p.NgayBatDauChieu)
                    AND (p.NgayKetThucChieu IS NULL OR sc.NgayChieu<=p.NgayKetThucChieu)
                    AND (sc.NgayChieu>CONVERT(date,GETDATE()) OR
                         (sc.NgayChieu=CONVERT(date,GETDATE()) AND sc.GioChieu>CONVERT(time,GETDATE())))
                ) ORDER BY p.TenPhim");
            foreach (DataRow movie in movies.Rows) AddCard(movie);
            _status.Text = movies.Rows.Count == 0 ? "Chưa có phim có suất chiếu sắp tới." : $"{movies.Rows.Count} phim có suất chiếu • Chọn phim để tiếp tục chọn suất và ghế";
        }
        catch (Exception)
        {
            _status.Text = "Không tải được danh mục phim. Vui lòng mở lại mục Bán vé để thử lại.";
        }
        ResizeCards();
    }

    private void AddCard(DataRow movie)
    {
        var card = new MovieItemCard();
        card.ApplyDesignTemplate(previewMovie);
        card.SetMovie(movie);
        card.MovieSelected += id => MovieSelected?.Invoke(id);
        _movies.Controls.Add(card);
    }

    private void ResizeCards()
    {
        int available = Math.Max(240, _movies.ClientSize.Width - _movies.Padding.Horizontal);
        int columns = Math.Max(1, available / 284);
        int width = Math.Max(210, available / columns - 24);
        int height = Math.Max(200, (_movies.ClientSize.Height - _movies.Padding.Vertical
            - SystemInformation.HorizontalScrollBarHeight) / 2 - 24);
        _movies.SuspendLayout();
        try
        {
            for (int index = 0; index < _movies.Controls.Count; index++)
            {
                Control card = _movies.Controls[index];
                if (AutoFitCards) card.Size = new Size(width, height);
                _movies.SetFlowBreak(card, index % 2 == 1);
            }
        }
        finally { _movies.ResumeLayout(true); }
        UpdateNavigation();
    }

    private void ScrollMovies(int direction)
    {
        int current = -_movies.AutoScrollPosition.X;
        int page = Math.Max(1, _movies.ClientSize.Width - _movies.Padding.Horizontal);
        int maximum = Math.Max(0, _movies.DisplayRectangle.Width - _movies.ClientSize.Width);
        _movies.AutoScrollPosition = new Point(Math.Clamp(current + direction * page, 0, maximum), 0);
        UpdateNavigation();
    }

    private void UpdateNavigation()
    {
        int current = -_movies.AutoScrollPosition.X;
        int maximum = Math.Max(0, _movies.DisplayRectangle.Width - _movies.ClientSize.Width);
        previousMovies.Enabled = current > 0;
        nextMovies.Enabled = current < maximum;
    }

    private void ClearCards()
    {
        foreach (Control card in _movies.Controls.Cast<Control>().ToArray())
        {
            
            if (card == previewMovie) _movies.Controls.Remove(card);
            else card.Dispose();
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { ClearCards(); previewMovie.Dispose(); }
        base.Dispose(disposing);
    }
}
