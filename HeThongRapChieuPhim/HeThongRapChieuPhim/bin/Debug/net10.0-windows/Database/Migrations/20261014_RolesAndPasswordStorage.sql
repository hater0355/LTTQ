/*
  Idempotent upgrade for the Admin/Cashier roles and password storage.
  Run against NHHCinemaDB. This migration does not drop tables or remove data.
  Legacy plaintext passwords are upgraded after the account's next successful login.
*/
USE NHHCinemaDB;
GO

IF COL_LENGTH(N'dbo.TaiKhoan', N'MatKhau') IS NOT NULL
    ALTER TABLE dbo.TaiKhoan ALTER COLUMN MatKhau NVARCHAR(255) NOT NULL;
GO

UPDATE dbo.TaiKhoan SET VaiTro = N'Admin'
WHERE VaiTro IN (N'Quản lý', N'Quan ly', N'Manager');
GO

UPDATE dbo.TaiKhoan SET VaiTro = N'Thu ngân'
WHERE VaiTro IN (N'Nhân viên', N'Nhan vien', N'Staff', N'Cashier');
GO
