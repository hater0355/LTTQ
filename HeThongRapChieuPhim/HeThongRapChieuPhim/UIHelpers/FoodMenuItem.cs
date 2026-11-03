namespace HeThongRapChieuPhim.UIHelpers;

public sealed record FoodMenuItem(string MaDoAn, string TenDoAn, string MoTa, string HinhAnhPath, decimal DonGia);

public sealed class FoodQuantityChangedEventArgs(FoodMenuItem item, int delta) : EventArgs
{
    public FoodMenuItem Item { get; } = item;
    public int Delta { get; } = delta;
}
