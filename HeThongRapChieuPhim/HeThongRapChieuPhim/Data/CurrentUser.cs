namespace HeThongRapChieuPhim.Data
{
    public static class CurrentUser
    {
        public static string TenDangNhap { get; set; } = string.Empty;
        public static string MaNV { get; set; } = string.Empty;
        public static string HoTen { get; set; } = string.Empty;
        public static string VaiTro { get; set; } = string.Empty;
        public static bool MustChangePassword { get; set; }

        public static bool IsAdmin => AccessRoles.IsAdmin(VaiTro);
        public static bool IsFilmAdmin => AccessRoles.Normalize(VaiTro) == AccessRoles.FilmAdmin;
        public static bool IsHumanResourcesAdmin => AccessRoles.Normalize(VaiTro) == AccessRoles.HumanResourcesAdmin;
        public static bool IsFoodAndPromotionsAdmin => AccessRoles.Normalize(VaiTro) == AccessRoles.FoodAndPromotionsAdmin;
        public static bool IsRevenueAdmin => AccessRoles.Normalize(VaiTro) == AccessRoles.RevenueAdmin;
        public static bool IsSuperAdmin => AccessRoles.IsSuperAdmin(VaiTro);
        public static bool IsCashier => AccessRoles.Normalize(VaiTro) == AccessRoles.Cashier;
        public static bool IsQuanLy => IsAdmin;

        public static void Clear()
        {
            TenDangNhap = string.Empty;
            MaNV = string.Empty;
            HoTen = string.Empty;
            VaiTro = string.Empty;
            MustChangePassword = false;
        }
    }
}
