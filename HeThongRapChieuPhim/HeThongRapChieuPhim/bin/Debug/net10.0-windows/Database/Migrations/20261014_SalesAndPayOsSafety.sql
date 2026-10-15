/* Safe, repeatable schema update. Run in NHHCinemaDB; preserves existing invoices. */
USE NHHCinemaDB;
GO

/* Required when creating the filtered PayOS order-code index from sqlcmd. */
SET QUOTED_IDENTIFIER ON;
GO

IF COL_LENGTH(N'dbo.TaiKhoan', N'YeuCauDoiMatKhau') IS NULL
    ALTER TABLE dbo.TaiKhoan ADD YeuCauDoiMatKhau BIT NOT NULL
        CONSTRAINT DF_TaiKhoan_YeuCauDoiMatKhau DEFAULT (0) WITH VALUES;
GO

IF COL_LENGTH(N'dbo.TaiKhoan', N'MatKhau') IS NOT NULL
    ALTER TABLE dbo.TaiKhoan ALTER COLUMN MatKhau NVARCHAR(255) NOT NULL;
GO

UPDATE dbo.TaiKhoan
SET YeuCauDoiMatKhau = 1
WHERE TenDangNhap = 'admin' AND VaiTro = N'Admin' AND MatKhau = N'admin';
GO

IF COL_LENGTH(N'dbo.HoaDon', N'TenDangNhapNguoiBan') IS NULL
    ALTER TABLE dbo.HoaDon ADD TenDangNhapNguoiBan VARCHAR(50) NULL;
IF COL_LENGTH(N'dbo.HoaDon', N'TrangThaiThanhToan') IS NULL
    ALTER TABLE dbo.HoaDon ADD TrangThaiThanhToan VARCHAR(20) NOT NULL
        CONSTRAINT DF_HoaDon_TrangThaiThanhToan DEFAULT ('Paid') WITH VALUES;
IF COL_LENGTH(N'dbo.HoaDon', N'PayOSOrderCode') IS NULL
    ALTER TABLE dbo.HoaDon ADD PayOSOrderCode BIGINT NULL;
IF COL_LENGTH(N'dbo.HoaDon', N'PayOSPaymentLinkId') IS NULL
    ALTER TABLE dbo.HoaDon ADD PayOSPaymentLinkId VARCHAR(64) NULL;
GO

UPDATE hd
SET TenDangNhapNguoiBan = account.TenDangNhap
FROM dbo.HoaDon hd
OUTER APPLY (
    SELECT TOP (1) tk.TenDangNhap
    FROM dbo.TaiKhoan tk
    WHERE tk.MaNV = hd.MaNV
    ORDER BY CASE WHEN tk.VaiTro IN (N'Thu ngân', N'Cashier') THEN 0 ELSE 1 END, tk.TenDangNhap
) account
WHERE hd.TenDangNhapNguoiBan IS NULL AND account.TenDangNhap IS NOT NULL;
GO

IF OBJECT_ID(N'dbo.SeqHoaDon', N'SO') IS NULL
    EXEC(N'CREATE SEQUENCE dbo.SeqHoaDon AS BIGINT START WITH 1 INCREMENT BY 1');
GO

IF OBJECT_ID(N'dbo.GheDatCho', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GheDatCho (
        MaSuatChieu VARCHAR(20) NOT NULL REFERENCES dbo.SuatChieu(MaSuatChieu),
        MaGhe VARCHAR(10) NOT NULL,
        MaHD VARCHAR(20) NOT NULL REFERENCES dbo.HoaDon(MaHD),
        HetHan DATETIME2 NULL,
        CONSTRAINT PK_GheDatCho PRIMARY KEY (MaSuatChieu, MaGhe)
    );
END;
GO

/* Legacy duplicate sales stay in history; one row per physical seat blocks future double booking. */
;WITH SeatOwner AS (
    SELECT ct.MaSuatChieu, ct.MaGhe, ct.MaHD,
           ROW_NUMBER() OVER (PARTITION BY ct.MaSuatChieu, ct.MaGhe ORDER BY hd.NgayLap DESC, ct.MaHD DESC) AS rn
    FROM dbo.ChiTietHoaDon_Ghe ct
    JOIN dbo.HoaDon hd ON hd.MaHD = ct.MaHD
    WHERE hd.TrangThaiThanhToan = 'Paid'
)
INSERT INTO dbo.GheDatCho (MaSuatChieu, MaGhe, MaHD, HetHan)
SELECT s.MaSuatChieu, s.MaGhe, s.MaHD, NULL
FROM SeatOwner s
WHERE s.rn = 1
  AND NOT EXISTS (SELECT 1 FROM dbo.GheDatCho g WHERE g.MaSuatChieu = s.MaSuatChieu AND g.MaGhe = s.MaGhe);
GO

IF OBJECT_ID(N'dbo.VoucherClaim', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.VoucherClaim (
        MaVoucher VARCHAR(20) NOT NULL PRIMARY KEY REFERENCES dbo.Voucher(MaVoucher),
        MaHD VARCHAR(20) NOT NULL REFERENCES dbo.HoaDon(MaHD),
        TrangThai VARCHAR(12) NOT NULL CHECK (TrangThai IN ('Pending', 'Used')),
        HetHan DATETIME2 NULL
    );
END;
GO

;WITH VoucherOwner AS (
    SELECT hd.MaVoucher, hd.MaHD,
           ROW_NUMBER() OVER (PARTITION BY hd.MaVoucher ORDER BY hd.NgayLap DESC, hd.MaHD DESC) AS rn
    FROM dbo.HoaDon hd
    WHERE hd.MaVoucher IS NOT NULL AND hd.TrangThaiThanhToan = 'Paid'
)
INSERT INTO dbo.VoucherClaim (MaVoucher, MaHD, TrangThai, HetHan)
SELECT v.MaVoucher, v.MaHD, 'Used', NULL
FROM VoucherOwner v
WHERE v.rn = 1
  AND NOT EXISTS (SELECT 1 FROM dbo.VoucherClaim c WHERE c.MaVoucher = v.MaVoucher);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.HoaDon') AND name = N'UX_HoaDon_PayOSOrderCode')
    CREATE UNIQUE INDEX UX_HoaDon_PayOSOrderCode ON dbo.HoaDon(PayOSOrderCode) WHERE PayOSOrderCode IS NOT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.GheDatCho') AND name = N'IX_GheDatCho_MaHD')
    CREATE INDEX IX_GheDatCho_MaHD ON dbo.GheDatCho(MaHD);
GO
