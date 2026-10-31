/* Expand the former Admin role into job-specific roles. Existing admin access is assigned to HR/account management. */
USE NHHCinemaDB;
GO

UPDATE dbo.TaiKhoan
SET VaiTro = 'AdminNhanSu'
WHERE VaiTro IN (N'Admin', N'Quản lý', N'Quan ly', N'Manager');
GO

UPDATE dbo.TaiKhoan
SET VaiTro = 'Cashier'
WHERE VaiTro IN (N'Thu ngân', N'Nhân viên', N'Nhan vien', N'Staff');
GO
