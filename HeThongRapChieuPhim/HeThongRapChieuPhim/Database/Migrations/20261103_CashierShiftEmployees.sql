/*
  A cashier login represents a register/account. The responsible employee is
  selected separately for each work-shift row. One account may therefore have
  different employees on different shifts, with no account-count limit.
  Existing employee and account values on shift rows are preserved.
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
        THROW 51320, N'Thiếu bảng TaiKhoan, NhanVien hoặc LichLamViec. Hãy chạy schema và migration lịch ca trước.', 1;

    IF COL_LENGTH(N'dbo.LichLamViec', N'TenDangNhapThuNgan') IS NULL
        ALTER TABLE dbo.LichLamViec ADD TenDangNhapThuNgan VARCHAR(50) NULL;

    -- Report any legacy orphaned account references rather than dropping shift history.
    IF EXISTS
    (
        SELECT 1 FROM dbo.LichLamViec pc
        WHERE pc.TenDangNhapThuNgan IS NOT NULL
          AND NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan tk WHERE tk.TenDangNhap=pc.TenDangNhapThuNgan)
    )
    BEGIN
        SELECT pc.MaPhanCa, pc.NgayLam, pc.MaCa, pc.TenDangNhapThuNgan, pc.MaNV
        FROM dbo.LichLamViec pc
        WHERE pc.TenDangNhapThuNgan IS NOT NULL
          AND NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan tk WHERE tk.TenDangNhap=pc.TenDangNhapThuNgan)
        ORDER BY pc.NgayLam, pc.MaCa, pc.MaPhanCa;
        THROW 51321, N'Có lịch ca tham chiếu tài khoản không tồn tại. Hãy xử lý các mã lịch ở trên rồi chạy lại.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.LichLamViec
        WHERE TenDangNhapThuNgan IS NOT NULL AND TrangThai IN ('Scheduled','Completed')
        GROUP BY NgayLam, MaCa, TenDangNhapThuNgan
        HAVING COUNT(*) > 1
    )
    BEGIN
        SELECT NgayLam, MaCa, TenDangNhapThuNgan, COUNT(*) AS SoLichTrung
        FROM dbo.LichLamViec
        WHERE TenDangNhapThuNgan IS NOT NULL AND TrangThai IN ('Scheduled','Completed')
        GROUP BY NgayLam, MaCa, TenDangNhapThuNgan
        HAVING COUNT(*) > 1;
        THROW 51322, N'Một tài khoản đang có nhiều nhân viên trong cùng ca. Hãy xử lý trước khi chạy lại migration.', 1;
    END;

    -- Remove the former fixed account-to-employee model.
    IF EXISTS
    (
        SELECT 1 FROM sys.foreign_keys
        WHERE parent_object_id=OBJECT_ID(N'dbo.LichLamViec')
          AND name=N'FK_LichLamViec_TaiKhoanNhanVien'
    )
        ALTER TABLE dbo.LichLamViec DROP CONSTRAINT FK_LichLamViec_TaiKhoanNhanVien;

    IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LichLamViec') AND name=N'UX_LichLamViec_Ngay_TaiKhoan_Active')
        DROP INDEX UX_LichLamViec_Ngay_TaiKhoan_Active ON dbo.LichLamViec;
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.TaiKhoan') AND name=N'UX_TaiKhoan_Cashier_MaNV')
        DROP INDEX UX_TaiKhoan_Cashier_MaNV ON dbo.TaiKhoan;
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.TaiKhoan') AND name=N'UX_TaiKhoan_TenDangNhap_MaNV')
        DROP INDEX UX_TaiKhoan_TenDangNhap_MaNV ON dbo.TaiKhoan;

    -- Employee ownership belongs to each shift, not to the cashier login.
    UPDATE dbo.TaiKhoan SET MaNV=NULL WHERE VaiTro='Cashier' AND MaNV IS NOT NULL;

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.foreign_keys
        WHERE parent_object_id=OBJECT_ID(N'dbo.LichLamViec')
          AND name=N'FK_LichLamViec_TaiKhoanThuNgan'
    )
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.LichLamViec WITH CHECK
                ADD CONSTRAINT FK_LichLamViec_TaiKhoanThuNgan
                FOREIGN KEY (TenDangNhapThuNgan)
                REFERENCES dbo.TaiKhoan (TenDangNhap);';

    IF NOT EXISTS
    (
        SELECT 1 FROM sys.indexes
        WHERE object_id=OBJECT_ID(N'dbo.LichLamViec')
          AND name=N'UX_LichLamViec_Ngay_Ca_TaiKhoan_Active'
    )
        EXEC sys.sp_executesql N'
            CREATE UNIQUE INDEX UX_LichLamViec_Ngay_Ca_TaiKhoan_Active
                ON dbo.LichLamViec (NgayLam, MaCa, TenDangNhapThuNgan)
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
        (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId='20261103_CashierShiftEmployees')
        INSERT dbo.SchemaMigration (MigrationId)
        VALUES ('20261103_CashierShiftEmployees');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
