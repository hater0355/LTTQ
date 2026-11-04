/*
  HISTORICAL migration: it introduced cashier account values on shift rows,
  but also temporarily linked each account to one employee. The intended
  model is per-shift staffing; always run
  20261103_CashierShiftEmployees.sql after this migration. The full
  NHHcinema.sql setup script already applies both migrations in order.
*/
USE QuanLyRapChieuPhim;
GO

SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL
       OR OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL
       OR OBJECT_ID(N'dbo.LichLamViec', N'U') IS NULL
        THROW 51140, N'Thiếu bảng TaiKhoan, NhanVien hoặc LichLamViec. Hãy chạy schema và migration lịch ca trước.', 1;

    IF COL_LENGTH(N'dbo.LichLamViec', N'TenDangNhapThuNgan') IS NULL
        ALTER TABLE dbo.LichLamViec ADD TenDangNhapThuNgan VARCHAR(50) NULL;

    -- Do not guess which account should own an employee with duplicate cashier links.
    IF EXISTS
    (
        SELECT 1
        FROM dbo.TaiKhoan
        WHERE VaiTro = 'Cashier' AND MaNV IS NOT NULL
        GROUP BY MaNV
        HAVING COUNT(*) > 1
    )
    BEGIN
        SELECT MaNV AS [Mã nhân viên], TenDangNhap AS [Tài khoản thu ngân bị trùng]
        FROM dbo.TaiKhoan
        WHERE VaiTro = 'Cashier' AND MaNV IN
        (
            SELECT MaNV FROM dbo.TaiKhoan
            WHERE VaiTro = 'Cashier' AND MaNV IS NOT NULL
            GROUP BY MaNV HAVING COUNT(*) > 1
        )
        ORDER BY MaNV, TenDangNhap;
        THROW 51141, N'Một nhân viên đang được gắn với nhiều tài khoản thu ngân. Hãy xử lý danh sách ở trên rồi chạy lại migration.', 1;
    END;

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.TaiKhoan') AND name = N'UX_TaiKhoan_TenDangNhap_MaNV'
    )
        CREATE UNIQUE INDEX UX_TaiKhoan_TenDangNhap_MaNV
            ON dbo.TaiKhoan (TenDangNhap, MaNV);

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.TaiKhoan') AND name = N'UX_TaiKhoan_Cashier_MaNV'
    )
        CREATE UNIQUE INDEX UX_TaiKhoan_Cashier_MaNV
            ON dbo.TaiKhoan (MaNV)
            WHERE VaiTro = 'Cashier' AND MaNV IS NOT NULL;

    -- Only backfill when the database already contains a definitive account-to-employee link.
    EXEC sys.sp_executesql N'
        UPDATE pc
           SET pc.TenDangNhapThuNgan = tk.TenDangNhap
        FROM dbo.LichLamViec pc
        INNER JOIN dbo.TaiKhoan tk ON tk.MaNV = pc.MaNV AND tk.VaiTro = ''Cashier''
        WHERE pc.TenDangNhapThuNgan IS NULL;';

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.foreign_keys
        WHERE parent_object_id = OBJECT_ID(N'dbo.LichLamViec')
          AND name = N'FK_LichLamViec_TaiKhoanNhanVien'
    )
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.LichLamViec WITH CHECK
                ADD CONSTRAINT FK_LichLamViec_TaiKhoanNhanVien
                FOREIGN KEY (TenDangNhapThuNgan, MaNV)
                REFERENCES dbo.TaiKhoan (TenDangNhap, MaNV);';

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.LichLamViec')
          AND name = N'UX_LichLamViec_Ngay_TaiKhoan_Active'
    )
        EXEC sys.sp_executesql N'
            CREATE UNIQUE INDEX UX_LichLamViec_Ngay_TaiKhoan_Active
                ON dbo.LichLamViec (NgayLam, TenDangNhapThuNgan)
                WHERE TenDangNhapThuNgan IS NOT NULL
                  AND TrangThai IN (''Scheduled'', ''Completed'');';

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );
    END;

    IF NOT EXISTS
        (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId = '20261103_CashierAccountShiftAssignments')
        INSERT dbo.SchemaMigration (MigrationId)
        VALUES ('20261103_CashierAccountShiftAssignments');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
