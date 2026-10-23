using System.Drawing;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.UIHelpers;

public sealed record FoodSizeOption(string MaKichCo, string TenKichCo, decimal DonGia)
{
    public override string ToString() => TenKichCo;
}

public sealed record FoodMenuItem(
    string MaDoAn,
    string TenDoAn,
    string MoTa,
    string HinhAnhPath,
    IReadOnlyList<FoodSizeOption> KichCo);

public sealed class FoodQuantityChangedEventArgs(FoodMenuItem item, FoodSizeOption size, int delta) : EventArgs
{
    public FoodMenuItem Item { get; } = item;
    public FoodSizeOption Size { get; } = size;
    public int Delta { get; } = delta;
}

public sealed class FoodItemCard : UserControl
{
    private readonly ComboBox _sizeSelector = new();
    private readonly Label _priceLabel = new();
    private readonly Label _quantityLabel = new();
    private readonly Button _removeButton = new();
    private readonly Button _addButton = new();

    public FoodMenuItem Item { get; }
    public FoodSizeOption? SelectedSize => _sizeSelector.SelectedItem as FoodSizeOption;

    public event EventHandler<FoodQuantityChangedEventArgs>? QuantityChanged;
    public event EventHandler? SelectedSizeChanged;

    public FoodItemCard(FoodMenuItem item)
    {
        Item = item;
        Size = new Size(296, 128);
        MinimumSize = new Size(270, 128);
        Margin = new Padding(2, 2, 2, 8);
        Padding = new Padding(0);
        BackColor = Color.White;
        BorderStyle = BorderStyle.FixedSingle;

        var poster = new PictureBox
        {
            Location = new Point(7, 8),
            Size = new Size(78, 78),
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.FromArgb(248, 250, 249),
            BorderStyle = BorderStyle.FixedSingle,
            Image = ImageAssetHelper.Load(item.HinhAnhPath)
        };

        var nameLabel = new Label
        {
            Location = new Point(94, 7),
            Size = new Size(190, 22),
            Text = item.TenDoAn,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            ForeColor = Color.FromArgb(20, 83, 45),
            AutoEllipsis = true
        };

        var descriptionLabel = new Label
        {
            Location = new Point(94, 29),
            Size = new Size(190, 34),
            Text = string.IsNullOrWhiteSpace(item.MoTa) ? "Bắp nước tươi ngon, dùng trong rạp." : item.MoTa,
            Font = new Font("Segoe UI", 8f),
            ForeColor = Color.FromArgb(90, 115, 100),
            AutoEllipsis = true
        };

        _sizeSelector.DropDownStyle = ComboBoxStyle.DropDownList;
        _sizeSelector.Font = new Font("Segoe UI", 8.5f);
        _sizeSelector.Location = new Point(94, 66);
        _sizeSelector.Size = new Size(130, 25);
        foreach (var size in item.KichCo) _sizeSelector.Items.Add(size);
        _sizeSelector.SelectedIndexChanged += (_, _) =>
        {
            UpdatePrice();
            SelectedSizeChanged?.Invoke(this, EventArgs.Empty);
        };
        if (_sizeSelector.Items.Count > 0) _sizeSelector.SelectedIndex = 0;

        _priceLabel.Location = new Point(224, 67);
        _priceLabel.Size = new Size(65, 22);
        _priceLabel.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        _priceLabel.ForeColor = Color.FromArgb(180, 83, 9);
        _priceLabel.TextAlign = ContentAlignment.MiddleRight;

        _removeButton.Location = new Point(94, 97);
        _removeButton.Size = new Size(32, 24);
        _removeButton.Text = "−";
        _removeButton.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        _removeButton.FlatStyle = FlatStyle.Flat;
        _removeButton.FlatAppearance.BorderColor = Color.FromArgb(210, 220, 214);
        _removeButton.Click += (_, _) => RaiseQuantityChange(-1);

        _quantityLabel.Location = new Point(130, 99);
        _quantityLabel.Size = new Size(88, 20);
        _quantityLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
        _quantityLabel.ForeColor = Color.FromArgb(20, 50, 35);
        _quantityLabel.TextAlign = ContentAlignment.MiddleCenter;
        _quantityLabel.Text = "Đã chọn: 0";

        _addButton.Location = new Point(222, 97);
        _addButton.Size = new Size(67, 24);
        _addButton.Text = "+ Thêm";
        _addButton.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
        _addButton.BackColor = Color.FromArgb(16, 163, 74);
        _addButton.ForeColor = Color.White;
        _addButton.FlatStyle = FlatStyle.Flat;
        _addButton.FlatAppearance.BorderSize = 0;
        _addButton.Click += (_, _) => RaiseQuantityChange(1);

        Controls.Add(poster);
        Controls.Add(nameLabel);
        Controls.Add(descriptionLabel);
        Controls.Add(_sizeSelector);
        Controls.Add(_priceLabel);
        Controls.Add(_removeButton);
        Controls.Add(_quantityLabel);
        Controls.Add(_addButton);
        UpdatePrice();
        SetSelectedQuantity(0);
    }

    public void SetSelectedQuantity(int quantity)
    {
        _quantityLabel.Text = $"Đã chọn: {quantity}";
        _removeButton.Enabled = quantity > 0;
        _addButton.Enabled = quantity < 20 && SelectedSize is not null;
    }

    private void UpdatePrice()
    {
        _priceLabel.Text = SelectedSize is { } size ? $"{size.DonGia:N0} đ" : "—";
        _addButton.Enabled = SelectedSize is not null;
    }

    private void RaiseQuantityChange(int delta)
    {
        if (SelectedSize is { } size)
            QuantityChanged?.Invoke(this, new FoodQuantityChangedEventArgs(Item, size, delta));
    }
}
