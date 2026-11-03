using System.Data;
namespace HeThongRapChieuPhim.UIHelpers;
public partial class MovieItemCard : UserControl
{
    private string _movieId = "";
    public event Action<string>? MovieSelected;
    public MovieItemCard()
    {
        InitializeComponent();
        poster.Click += SelectMovie; title.Click += SelectMovie; buy.Click += SelectMovie;
    }
    public void ApplyDesignTemplate(MovieItemCard template)
    {
        CopyAppearance(template, this);
    }

    private static void CopyAppearance(Control source, Control target)
    {
        target.Font = source.Font;
        target.BackColor = source.BackColor;
        target.ForeColor = source.ForeColor;
        target.Padding = source.Padding;
        target.Margin = source.Margin;
        target.MinimumSize = source.MinimumSize;
        target.MaximumSize = source.MaximumSize;
        target.Size = source.Size;
        foreach (Control child in source.Controls)
        {
            Control? copy = target.Controls[child.Name];
            if (copy == null) continue;
            CopyAppearance(child, copy);
            copy.Dock = child.Dock;
            copy.Anchor = child.Anchor;
            copy.Location = child.Location;
            if (child is Label sourceLabel && copy is Label label)
            {
                label.TextAlign = sourceLabel.TextAlign;
                label.AutoEllipsis = sourceLabel.AutoEllipsis;
            }
            if (child is Button sourceButton && copy is Button button)
            {
                button.Text = sourceButton.Text;
                button.FlatStyle = sourceButton.FlatStyle;
                button.FlatAppearance.BorderColor = sourceButton.FlatAppearance.BorderColor;
                button.FlatAppearance.BorderSize = sourceButton.FlatAppearance.BorderSize;
            }
            if (child is PictureBox sourcePicture && copy is PictureBox picture)
                picture.SizeMode = sourcePicture.SizeMode;
        }
    }
    private void SelectMovie(object? sender, EventArgs e) { if (_movieId.Length > 0) MovieSelected?.Invoke(_movieId); }
    public void SetMovie(DataRow movie)
    {
        _movieId = Convert.ToString(movie["MaPhim"]) ?? "";
        poster.Image?.Dispose();
        poster.Image = ImageAssetHelper.Load(Convert.ToString(movie["PosterPath"]));
        title.Text = Convert.ToString(movie["TenPhim"]);
        detail.Text = $"Thể loại: {movie["TheLoai"]}\nThời lượng: {movie["ThoiLuong"]} phút\nĐộ tuổi: {movie["DoTuoi"]}";
    }
    protected override void Dispose(bool disposing) { if (disposing) poster.Image?.Dispose(); base.Dispose(disposing); }
}
