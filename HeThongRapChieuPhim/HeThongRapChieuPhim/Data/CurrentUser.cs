namespace HeThongRapChieuPhim.Data
{
    public static class CurrentUser
    {
        public static string TenDangNhap { get; set; } = string.Empty;
        public static string MaNV { get; set; } = string.Empty;
        private static string _hoTen = string.Empty;
        public static string HoTen
        {
            get => _hoTen;
            set => _hoTen = RepairDisplayName(value ?? string.Empty);
        }

        private static string RepairDisplayName(string value)
        {
            // Recover UTF-8 names previously imported as Windows-1252.
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var legacy = System.Text.Encoding.GetEncoding(1252,
                System.Text.EncoderFallback.ExceptionFallback, System.Text.DecoderFallback.ExceptionFallback);
            var utf8 = new System.Text.UTF8Encoding(false, true);
            for (int i = 0; i < 2 && (value.Contains('Ã') || value.Contains('Â') || value.Contains('á')); i++)
            {
                try
                {
                    string decoded = utf8.GetString(legacy.GetBytes(value));
                    if (decoded == value) break;
                    value = decoded;
                }
                catch (System.Text.EncoderFallbackException) { break; }
                catch (System.Text.DecoderFallbackException) { break; }
            }
            return value;
        }
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
