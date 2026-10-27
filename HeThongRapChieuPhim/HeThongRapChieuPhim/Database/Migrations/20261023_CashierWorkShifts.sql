/*
  Adds three daily work shifts and employee shift assignments.
  Existing employee and sales records are not changed or removed.
  Run after 20261023_MovieReleaseWindow.sql on an existing database.
*/
USE NHHCinemaDB;
GO

SET XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL
        THROW 51050, 'Không tìm thấy dbo.NhanVien. Hãy chạy schema và các migration nền trước.', 1;

    IF OBJECT_ID(N'dbo.CaLamViec', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.CaLamViec
        (
            MaCa TINYINT NOT NULL CONSTRAINT PK_CaLamViec PRIMARY KEY,
            TenCa NVARCHAR(40) NOT NULL,
            GioBatDau TIME(0) NOT NULL,
            GioKetThuc TIME(0) NOT NULL,
            CONSTRAINT CK_CaLamViec_ThoiGian_Valid CHECK (GioKetThuc > GioBatDau)
        );
    END;

    IF OBJECT_ID(N'dbo.LichLamViec', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.LichLamViec
        (
            MaPhanCa BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_LichLamViec PRIMARY KEY,
            MaNV VARCHAR(20) NOT NULL,
            NgayLam DATE NOT NULL,
            MaCa TINYINT NOT NULL,
            TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_LichLamViec_TrangThai DEFAULT ('Scheduled'),
            CONSTRAINT FK_LichLamViec_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
            CONSTRAINT FK_LichLamViec_CaLamViec FOREIGN KEY (MaCa) REFERENCES dbo.CaLamViec(MaCa),
            CONSTRAINT CK_LichLamViec_TrangThai_Valid CHECK (TrangThai IN ('Scheduled', 'Completed', 'Cancelled'))
        );
    END;

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.LichLamViec') AND name = N'UX_LichLamViec_Ngay_MaNV_Active')
        CREATE UNIQUE INDEX UX_LichLamViec_Ngay_MaNV_Active ON dbo.LichLamViec (NgayLam, MaNV)
        WHERE TrangThai IN ('Scheduled', 'Completed');

    IF NOT EXISTS (SELECT 1 FROM dbo.CaLamViec WHERE MaCa = 1)
        INSERT dbo.CaLamViec (MaCa, TenCa, GioBatDau, GioKetThuc) VALUES (1, N'Ca 1', '09:00', '13:00');
    IF NOT EXISTS (SELECT 1 FROM dbo.CaLamViec WHERE MaCa = 2)
        INSERT dbo.CaLamViec (MaCa, TenCa, GioBatDau, GioKetThuc) VALUES (2, N'Ca 2', '13:00', '17:00');
    IF NOT EXISTS (SELECT 1 FROM dbo.CaLamViec WHERE MaCa = 3)
        INSERT dbo.CaLamViec (MaCa, TenCa, GioBatDau, GioKetThuc) VALUES (3, N'Ca 3', '17:00', '21:00');

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId = '20261023_CashierWorkShifts')
        INSERT dbo.SchemaMigration (MigrationId) VALUES ('20261023_CashierWorkShifts');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
