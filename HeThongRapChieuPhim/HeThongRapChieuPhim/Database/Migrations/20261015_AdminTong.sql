/* Promote the existing admin login to the single all-pages administrator. Password and account data stay unchanged. */
USE NHHCinemaDB;
GO

UPDATE dbo.TaiKhoan
SET VaiTro = 'AdminTong'
WHERE TenDangNhap = 'admin' AND VaiTro = 'AdminNhanSu';
GO
