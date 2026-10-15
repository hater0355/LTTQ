using System.Data;
using System.Security.Cryptography;
using HeThongRapChieuPhim.Data;

namespace HeThongRapChieuPhim.Classes
{
    /// <summary>Business operations and shared application functions.</summary>
    public static class Function
    {
        private const int HashIterations = 210_000;
        private const string HashPrefix = "PBKDF2$";

        // Transitional facade for existing screens; all ADO.NET work is in DataProcesser.
        public static bool TestConnection(out string error) => DataProcesser.TestConnection(out error);
        public static DataTable Query(string sql, DbArg[]? parameters = null) =>
            DataProcesser.Query(sql, parameters ?? Array.Empty<DbArg>());
        public static int Execute(string sql, DbArg[]? parameters = null) =>
            DataProcesser.Execute(sql, parameters ?? Array.Empty<DbArg>());
        public static void ExecuteTransaction(IReadOnlyList<(string Sql, DbArg[] Parameters)> statements) =>
            DataProcesser.ExecuteTransaction(statements);
        public static object? Scalar(string sql, DbArg[]? parameters = null) =>
            DataProcesser.Scalar(sql, parameters ?? Array.Empty<DbArg>());

        public static bool Authenticate(string username, string password, out string error)
        {
            error = string.Empty;
            var rows = DataProcesser.Query(@"
                SELECT tk.TenDangNhap, tk.MatKhau, tk.VaiTro, tk.TrangThai,
                       tk.YeuCauDoiMatKhau,
                       ISNULL(tk.MaNV, '') AS MaNV,
                       ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen
                FROM TaiKhoan tk LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE tk.TenDangNhap = @user",
                new DbArg("@user", username));

            if (rows.Rows.Count == 0) { error = "Tên đăng nhập không tồn tại!"; return false; }
            var row = rows.Rows[0];
            if (!Convert.ToBoolean(row["TrangThai"])) { error = "Tài khoản của bạn đang bị khóa!"; return false; }

            string stored = Convert.ToString(row["MatKhau"]) ?? string.Empty;
            if (!VerifyPassword(password, stored, out bool legacyPassword))
            {
                error = "Mật khẩu không chính xác!";
                return false;
            }

            string role = AccessRoles.Normalize(Convert.ToString(row["VaiTro"]) ?? string.Empty);
            if (role.Length == 0)
            {
                error = "Tài khoản chưa được gán vai trò hợp lệ.";
                return false;
            }

            if (legacyPassword)
            {
                DataProcesser.Execute("UPDATE TaiKhoan SET MatKhau = @password WHERE TenDangNhap = @user",
                    new DbArg("@password", HashPassword(password)), new DbArg("@user", username));
            }

            CurrentUser.TenDangNhap = Convert.ToString(row["TenDangNhap"]) ?? username;
            CurrentUser.MaNV = Convert.ToString(row["MaNV"]) ?? string.Empty;
            CurrentUser.HoTen = Convert.ToString(row["HoTen"]) ?? username;
            CurrentUser.VaiTro = role;
            CurrentUser.MustChangePassword = Convert.ToBoolean(row["YeuCauDoiMatKhau"]);
            return true;
        }

        public static bool ChangeCurrentPassword(string newPassword, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(CurrentUser.TenDangNhap)) { error = "Phiên đăng nhập không hợp lệ."; return false; }
            if (newPassword.Length < 8) { error = "Mật khẩu mới phải có ít nhất 8 ký tự."; return false; }
            int affected = DataProcesser.Execute(@"
                UPDATE TaiKhoan SET MatKhau = @password, YeuCauDoiMatKhau = 0
                WHERE TenDangNhap = @user AND TrangThai = 1",
                new DbArg("@password", HashPassword(newPassword)), new DbArg("@user", CurrentUser.TenDangNhap));
            if (affected != 1) { error = "Không thể cập nhật mật khẩu tài khoản."; return false; }
            CurrentUser.MustChangePassword = false;
            return true;
        }

        public static bool CreateManagedAccount(string username, string password, string role, out string error)
        {
            error = string.Empty;
            if (!AccessRoles.IsAllowedToManageAccounts(CurrentUser.VaiTro))
            {
                error = "Chỉ Admin Nhân sự được tạo tài khoản.";
                return false;
            }
            role = AccessRoles.Normalize(role);
            if (!AccessRoles.ManagedRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            {
                error = "Vai trò tài khoản không hợp lệ.";
                return false;
            }
            username = username.Trim();
            if (username.Length == 0 || password.Length < 6)
            {
                error = "Tên đăng nhập không được để trống; mật khẩu phải có ít nhất 6 ký tự.";
                return false;
            }

            try
            {
                DataProcesser.Execute(@"
                    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, YeuCauDoiMatKhau)
                    VALUES (@user, @password, @role, NULL, 1, 1)",
                    new DbArg("@user", username), new DbArg("@password", HashPassword(password)), new DbArg("@role", role));
                return true;
            }
            catch (DuplicateRecordException)
            {
                error = "Tên đăng nhập đã tồn tại.";
                return false;
            }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        public static bool ChangeManagedAccountPassword(string username, string newPassword, out string error)
        {
            error = string.Empty;
            if (!AccessRoles.IsAllowedToManageAccounts(CurrentUser.VaiTro))
            {
                error = "Chỉ Admin Nhân sự được đổi mật khẩu tài khoản.";
                return false;
            }
            if (newPassword.Length < 6) { error = "Mật khẩu phải có ít nhất 6 ký tự."; return false; }

            int affected = DataProcesser.Execute(@"
                UPDATE TaiKhoan SET MatKhau = @password, YeuCauDoiMatKhau = 1
                WHERE TenDangNhap = @user AND VaiTro IN ('AdminTong','AdminPhim','AdminNhanSu','AdminDoAn','AdminDoanhThu','Cashier')",
                new DbArg("@password", HashPassword(newPassword)), new DbArg("@user", username));
            if (affected == 0) error = "Không tìm thấy tài khoản cần đổi mật khẩu.";
            return affected > 0;
        }

        public static DataTable GetManagedAccounts()
        {
            if (!AccessRoles.IsAllowedToManageAccounts(CurrentUser.VaiTro))
                throw new UnauthorizedAccessException("Chỉ Admin Nhân sự được quản lý tài khoản.");
            var accounts = DataProcesser.Query(@"
                SELECT TenDangNhap AS [Tên đăng nhập], VaiTro AS [Mã vai trò],
                       CASE WHEN TrangThai = 1 THEN N'Đang hoạt động' ELSE N'Đã khóa' END AS [Trạng thái]
                FROM TaiKhoan WHERE VaiTro IN ('AdminTong','AdminPhim','AdminNhanSu','AdminDoAn','AdminDoanhThu','Cashier')
                ORDER BY VaiTro, TenDangNhap");
            accounts.Columns.Add("Vai trò", typeof(string));
            foreach (DataRow row in accounts.Rows)
                row["Vai trò"] = AccessRoles.DisplayName(Convert.ToString(row["Mã vai trò"]) ?? string.Empty);
            accounts.Columns.Remove("Mã vai trò");
            return accounts;
        }

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashIterations, HashAlgorithmName.SHA256, 32);
            return $"{HashPrefix}{HashIterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        private static bool VerifyPassword(string password, string stored, out bool legacy)
        {
            legacy = !stored.StartsWith(HashPrefix, StringComparison.Ordinal);
            if (legacy) return CryptographicOperations.FixedTimeEquals(
                System.Text.Encoding.UTF8.GetBytes(password), System.Text.Encoding.UTF8.GetBytes(stored));
            try
            {
                string[] parts = stored.Split('$');
                int iterations = int.Parse(parts[1]);
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expected = Convert.FromBase64String(parts[3]);
                byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);
                return CryptographicOperations.FixedTimeEquals(actual, expected);
            }
            catch { return false; }
        }
    }
}
