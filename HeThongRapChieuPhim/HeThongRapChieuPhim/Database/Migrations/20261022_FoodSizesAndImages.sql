/*
  Additive migration for menu descriptions/images and size-specific prices.
  Historical food details keep their saved quantity and price; their size stays NULL
  because the old schema did not record one.
*/
USE NHHCinemaDB;
GO

SET XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.DoAn', N'U') IS NULL OR OBJECT_ID(N'dbo.ChiTietHoaDon_DoAn', N'U') IS NULL
        THROW 51030, 'Thiếu bảng DoAn hoặc ChiTietHoaDon_DoAn. Hãy chạy schema và migration bán hàng trước.', 1;

    IF COL_LENGTH(N'dbo.DoAn', N'MoTa') IS NULL
        ALTER TABLE dbo.DoAn ADD MoTa NVARCHAR(255) NULL;
    IF COL_LENGTH(N'dbo.DoAn', N'HinhAnhPath') IS NULL
        ALTER TABLE dbo.DoAn ADD HinhAnhPath NVARCHAR(500) NULL;
    IF COL_LENGTH(N'dbo.ChiTietHoaDon_DoAn', N'MaKichCo') IS NULL
        ALTER TABLE dbo.ChiTietHoaDon_DoAn ADD MaKichCo VARCHAR(10) NULL;

    IF OBJECT_ID(N'dbo.DoAnKichCo', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.DoAnKichCo
        (
            MaDoAn VARCHAR(20) NOT NULL,
            MaKichCo VARCHAR(10) NOT NULL,
            TenKichCo NVARCHAR(40) NOT NULL,
            DonGia DECIMAL(18,0) NOT NULL,
            CONSTRAINT PK_DoAnKichCo PRIMARY KEY (MaDoAn, MaKichCo),
            CONSTRAINT FK_DoAnKichCo_DoAn FOREIGN KEY (MaDoAn) REFERENCES dbo.DoAn(MaDoAn) ON DELETE CASCADE,
            CONSTRAINT CK_DoAnKichCo_DonGia_NonNegative CHECK (DonGia >= 0)
        );
    END;

    /* Gia remains the medium-size price. Smaller/larger defaults are rounded to the nearest 1,000 VND. */
    INSERT dbo.DoAnKichCo (MaDoAn, MaKichCo, TenKichCo, DonGia)
    SELECT da.MaDoAn, 'S', N'Cỡ nhỏ', CONVERT(DECIMAL(18,0), ROUND(da.Gia * 0.8 / 1000.0, 0) * 1000)
    FROM dbo.DoAn da
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DoAnKichCo kc WHERE kc.MaDoAn=da.MaDoAn AND kc.MaKichCo='S');

    INSERT dbo.DoAnKichCo (MaDoAn, MaKichCo, TenKichCo, DonGia)
    SELECT da.MaDoAn, 'M', N'Cỡ vừa', da.Gia
    FROM dbo.DoAn da
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DoAnKichCo kc WHERE kc.MaDoAn=da.MaDoAn AND kc.MaKichCo='M');

    INSERT dbo.DoAnKichCo (MaDoAn, MaKichCo, TenKichCo, DonGia)
    SELECT da.MaDoAn, 'L', N'Cỡ lớn', CONVERT(DECIMAL(18,0), ROUND(da.Gia * 1.2 / 1000.0, 0) * 1000)
    FROM dbo.DoAn da
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DoAnKichCo kc WHERE kc.MaDoAn=da.MaDoAn AND kc.MaKichCo='L');

    IF EXISTS
    (
        SELECT 1 FROM dbo.ChiTietHoaDon_DoAn ct
        LEFT JOIN dbo.DoAnKichCo kc ON kc.MaDoAn=ct.MaDoAn AND kc.MaKichCo=ct.MaKichCo
        WHERE ct.MaKichCo IS NOT NULL AND kc.MaDoAn IS NULL
    )
    BEGIN
        SELECT ct.Id, ct.MaHD, ct.MaDoAn, ct.MaKichCo
        FROM dbo.ChiTietHoaDon_DoAn ct
        LEFT JOIN dbo.DoAnKichCo kc ON kc.MaDoAn=ct.MaDoAn AND kc.MaKichCo=ct.MaKichCo
        WHERE ct.MaKichCo IS NOT NULL AND kc.MaDoAn IS NULL;
        THROW 51031, 'Có chi tiết hóa đơn tham chiếu cỡ món chưa tồn tại. Migration đã dừng, không sửa lịch sử.', 1;
    END;

    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id=OBJECT_ID(N'dbo.ChiTietHoaDon_DoAn') AND name=N'FK_ChiTietHoaDon_DoAn_KichCo')
        ALTER TABLE dbo.ChiTietHoaDon_DoAn WITH CHECK ADD CONSTRAINT FK_ChiTietHoaDon_DoAn_KichCo
            FOREIGN KEY (MaDoAn, MaKichCo) REFERENCES dbo.DoAnKichCo(MaDoAn, MaKichCo);

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId='20261022_FoodSizesAndImages')
        INSERT dbo.SchemaMigration (MigrationId) VALUES ('20261022_FoodSizesAndImages');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
