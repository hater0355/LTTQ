namespace HeThongRapChieuPhim.Data
{
    public static class AccessRoles
    {
        public const string FilmAdmin = "AdminPhim";
        public const string HumanResourcesAdmin = "AdminNhanSu";
        public const string FoodAndPromotionsAdmin = "AdminDoAn";
        public const string RevenueAdmin = "AdminDoanhThu";
        public const string SuperAdmin = "AdminTong";
        public const string Cashier = "Cashier";

        public static readonly string[] ManagedRoles =
        {
            FilmAdmin, HumanResourcesAdmin, FoodAndPromotionsAdmin, RevenueAdmin, Cashier
        };

        public static bool IsAdmin(string role) => role is FilmAdmin or HumanResourcesAdmin
            or FoodAndPromotionsAdmin or RevenueAdmin or SuperAdmin or "Admin";

        public static bool IsSuperAdmin(string role) => Normalize(role) == SuperAdmin;

        public static string Normalize(string role)
        {
            string value = role.Trim();
            if (value.Equals(FilmAdmin, StringComparison.OrdinalIgnoreCase)) return FilmAdmin;
            if (value.Equals(HumanResourcesAdmin, StringComparison.OrdinalIgnoreCase)) return HumanResourcesAdmin;
            if (value.Equals(FoodAndPromotionsAdmin, StringComparison.OrdinalIgnoreCase)) return FoodAndPromotionsAdmin;
            if (value.Equals(RevenueAdmin, StringComparison.OrdinalIgnoreCase)) return RevenueAdmin;
            if (value.Equals(SuperAdmin, StringComparison.OrdinalIgnoreCase)) return SuperAdmin;
            if (value.Equals(Cashier, StringComparison.OrdinalIgnoreCase)
                || value.Equals("Thu ngân", StringComparison.OrdinalIgnoreCase)
                || value.Equals("Nhân viên", StringComparison.OrdinalIgnoreCase)) return Cashier;
            if (value.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                || value.Contains("Quản lý", StringComparison.OrdinalIgnoreCase)
                || value.Contains("Quan ly", StringComparison.OrdinalIgnoreCase)
                || value.Equals("Manager", StringComparison.OrdinalIgnoreCase)) return HumanResourcesAdmin;
            return string.Empty;
        }

        public static string DisplayName(string role) => Normalize(role) switch
        {
            FilmAdmin => "Quản trị phim & suất chiếu",
            HumanResourcesAdmin => "Quản trị nhân sự",
            FoodAndPromotionsAdmin => "Quản trị đồ ăn & ưu đãi",
            RevenueAdmin => "Quản trị doanh thu",
            SuperAdmin => "Admin tổng",
            Cashier => "Thu ngân",
            _ => "Chưa phân quyền"
        };

        public static bool IsAllowedToManageAccounts(string role) =>
            Normalize(role) is HumanResourcesAdmin or SuperAdmin;
    }
}
