USE QuanLyRapChieuPhim;
GO

SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.GiaVeHangGhe', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.GiaVeHangGhe
        (
            MaSuatChieu VARCHAR(20) NOT NULL,
            HangGhe VARCHAR(10) NOT NULL,
            GiaVe DECIMAL(18,0) NOT NULL,
            CONSTRAINT PK_GiaVeHangGhe PRIMARY KEY (MaSuatChieu, HangGhe),
            CONSTRAINT FK_GiaVeHangGhe_SuatChieu FOREIGN KEY (MaSuatChieu)
                REFERENCES dbo.SuatChieu(MaSuatChieu) ON DELETE CASCADE,
            CONSTRAINT CK_GiaVeHangGhe_GiaNonNegative CHECK (GiaVe >= 0)
        );
    END;

    IF COL_LENGTH(N'dbo.ChiTietHoaDon_Ghe', N'GiaVe') IS NULL
        ALTER TABLE dbo.ChiTietHoaDon_Ghe
            ADD GiaVe DECIMAL(18,0) NOT NULL
                CONSTRAINT DF_ChiTietHoaDon_Ghe_GiaVe DEFAULT (0);

    -- Existing invoices used a single fare, so preserve their historical per-seat amount.
    EXEC sys.sp_executesql N'
        UPDATE ct
           SET ct.GiaVe = CASE WHEN hd.SoLuongVe > 0
                               THEN ROUND(hd.TienVe / hd.SoLuongVe, 0)
                               ELSE 0 END
        FROM dbo.ChiTietHoaDon_Ghe ct
        JOIN dbo.HoaDon hd ON hd.MaHD = ct.MaHD
        WHERE ct.GiaVe = 0;';

    ;WITH SeatRows AS
    (
        SELECT CONVERT(INT, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1) AS RowIndex
        FROM sys.all_objects
    )
    INSERT dbo.GiaVeHangGhe (MaSuatChieu, HangGhe, GiaVe)
    SELECT sc.MaSuatChieu,
           CONVERT(VARCHAR(10), CHAR(ASCII('A') + sr.RowIndex)),
           CONVERT(DECIMAL(18,0), ROUND(
               ISNULL(sc.GiaSuatChieu, p.GiaPhim) *
               CASE WHEN pc.SoHangGhe <= 1 THEN 1.0
                    ELSE 0.8 + 0.4 * sr.RowIndex / (pc.SoHangGhe - 1.0) END, 0))
    FROM dbo.SuatChieu sc
    JOIN dbo.PhongChieu pc ON pc.MaPhong = sc.MaPhong
    JOIN dbo.Phim p ON p.MaPhim = sc.MaPhim
    JOIN SeatRows sr ON sr.RowIndex < pc.SoHangGhe AND sr.RowIndex < 26
    WHERE NOT EXISTS
    (
        SELECT 1 FROM dbo.GiaVeHangGhe gv
        WHERE gv.MaSuatChieu = sc.MaSuatChieu
          AND gv.HangGhe = CONVERT(VARCHAR(10), CHAR(ASCII('A') + sr.RowIndex))
    );

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId = '20261029_RowTicketPricing')
        INSERT dbo.SchemaMigration (MigrationId) VALUES ('20261029_RowTicketPricing');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
