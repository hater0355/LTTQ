using System.Drawing;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.UIHelpers;

public partial class FoodItemCard : UserControl
{


    public FoodMenuItem Item { get; } = new("", "Tên món", "", "", 0);


    public event EventHandler<FoodQuantityChangedEventArgs>? QuantityChanged;


    public FoodItemCard()
    {
        InitializeComponent();
        _removeButton.Click += (_, _) => RaiseQuantityChange(-1);
        _addButton.Click += (_, _) => RaiseQuantityChange(1);
        SetSelectedQuantity(0);
    }
    public FoodItemCard(FoodMenuItem item) : this()
    {
        Item = item;
        poster.Image = ImageAssetHelper.Load(item.HinhAnhPath);
        nameLabel.Text = item.TenDoAn;
        descriptionLabel.Text = string.IsNullOrWhiteSpace(item.MoTa) ? "Bắp nước tươi ngon, dùng trong rạp." : item.MoTa;
        UpdatePrice();
    }
    protected override void Dispose(bool disposing) { if (disposing) poster.Image?.Dispose(); base.Dispose(disposing); }

    public void SetSelectedQuantity(int quantity)
    {
        _quantityLabel.Text = $"Đã chọn: {quantity}";
        _removeButton.Enabled = quantity > 0;
        _addButton.Enabled = quantity < 20;
    }

    private void UpdatePrice()
    {
        _priceLabel.Text = $"{Item.DonGia:N0} đ";
        _addButton.Enabled = true;
    }

    private void RaiseQuantityChange(int delta)
    {
        QuantityChanged?.Invoke(this, new FoodQuantityChangedEventArgs(Item, delta));
    }
}
