/*
  Adds an inclusive cinema release window to each movie.
  Existing movie rows remain unrestricted (both dates NULL) so prior schedules are preserved.
  New movie records created through the application require both dates.
*/
USE NHHCinemaDB;
GO

SET XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.Phim', N'U') IS NULL
        THROW 51040, 'Không tìm thấy bảng dbo.Phim. Hãy chạy schema và các migration nền trước.', 1;

    IF COL_LENGTH(N'dbo.Phim', N'NgayBatDauChieu') IS NULL
        ALTER TABLE dbo.Phim ADD NgayBatDauChieu DATE NULL;
    IF COL_LENGTH(N'dbo.Phim', N'NgayKetThucChieu') IS NULL
        ALTER TABLE dbo.Phim ADD NgayKetThucChieu DATE NULL;

    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.Phim') AND name=N'CK_Phim_ThoiHanChieu_Valid')
        ALTER TABLE dbo.Phim WITH CHECK ADD CONSTRAINT CK_Phim_ThoiHanChieu_Valid CHECK (
            (NgayBatDauChieu IS NULL AND NgayKetThucChieu IS NULL) OR
            (NgayBatDauChieu IS NOT NULL AND NgayKetThucChieu IS NOT NULL AND NgayKetThucChieu >= NgayBatDauChieu)
        );

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId='20261023_MovieReleaseWindow')
        INSERT dbo.SchemaMigration (MigrationId) VALUES ('20261023_MovieReleaseWindow');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO
