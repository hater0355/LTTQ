namespace HeThongRapChieuPhim.UIHelpers;

public partial class ShowtimeOption : UserControl
{
    private Color _availableBackColor;
    private Color _availableForeColor;
    [System.ComponentModel.Category("Appearance")]
    [System.ComponentModel.DefaultValue(typeof(Color), "0, 150, 130")]
    public Color SelectedBackColor { get; set; } = Color.FromArgb(0, 150, 130);
    [System.ComponentModel.Category("Appearance")]
    [System.ComponentModel.DefaultValue(typeof(Color), "White")]
    public Color SelectedForeColor { get; set; } = Color.White;
    public event EventHandler? Chosen;
    public ShowtimeOption()
    {
        InitializeComponent();
        _availableBackColor = selectButton.BackColor;
        _availableForeColor = selectButton.ForeColor;
        selectButton.Click += (_, _) => Chosen?.Invoke(this, EventArgs.Empty);
    }
    public void Bind(string id, string time, string room, string date, bool showRoom)
    {
        Tag = id;
        selectButton.Tag = id;
        selectButton.Text = showRoom ? $"{time}\n{room}" : time;
        selectButton.AccessibleName = $"{date} {time} - {room}";
        // Keep the Designer size for every room filter.
    }
    public void SetSelected(bool selected)
    {
        selectButton.BackColor = selected ? SelectedBackColor : _availableBackColor;
        selectButton.ForeColor = selected ? SelectedForeColor : _availableForeColor;
    }
}
