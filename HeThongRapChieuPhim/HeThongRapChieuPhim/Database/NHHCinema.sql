

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
GO

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyRapChieuPhim')
BEGIN
    CREATE DATABASE QuanLyRapChieuPhim COLLATE Vietnamese_CI_AS;
END
GO

USE QuanLyRapChieuPhim;
GO

-- 1. BẢNG NHÂN VIÊN

IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL CREATE TABLE NhanVien (
    MaNV VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    SDT VARCHAR(15) NOT NULL,
    CCCD VARCHAR(20) NOT NULL,
    ChucVu NVARCHAR(50) NOT NULL DEFAULT N'Nhân viên'
);
GO

-- 1.1. DANH MỤC CA LÀM VÀ LỊCH PHÂN CA
IF OBJECT_ID(N'dbo.CaLamViec', N'U') IS NULL CREATE TABLE dbo.CaLamViec (
    MaCa TINYINT NOT NULL CONSTRAINT PK_CaLamViec PRIMARY KEY,
    TenCa NVARCHAR(40) NOT NULL,
    GioBatDau TIME(0) NOT NULL,
    GioKetThuc TIME(0) NOT NULL,
    CONSTRAINT CK_CaLamViec_ThoiGian_Valid CHECK (GioKetThuc > GioBatDau)
);
GO

IF OBJECT_ID(N'dbo.LichLamViec', N'U') IS NULL CREATE TABLE dbo.LichLamViec (
    MaPhanCa BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_LichLamViec PRIMARY KEY,
    MaNV VARCHAR(20) NOT NULL,
    TenDangNhapThuNgan VARCHAR(50) NULL,
    NgayLam DATE NOT NULL,
    MaCa TINYINT NOT NULL,
    TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_LichLamViec_TrangThai DEFAULT ('Scheduled'),
    CONSTRAINT FK_LichLamViec_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
    CONSTRAINT FK_LichLamViec_CaLamViec FOREIGN KEY (MaCa) REFERENCES dbo.CaLamViec(MaCa),
    CONSTRAINT CK_LichLamViec_TrangThai_Valid CHECK (TrangThai IN ('Scheduled', 'Completed', 'Cancelled'))
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.LichLamViec') AND name = N'UX_LichLamViec_Ngay_MaNV_Active')
    CREATE UNIQUE INDEX UX_LichLamViec_Ngay_MaNV_Active ON dbo.LichLamViec (NgayLam, MaNV)
    WHERE TrangThai IN ('Scheduled', 'Completed');
GO

IF NOT EXISTS (SELECT 1 FROM dbo.CaLamViec WHERE MaCa = 1)
    INSERT dbo.CaLamViec (MaCa, TenCa, GioBatDau, GioKetThuc) VALUES (1, N'Ca 1', '09:00', '13:00');
IF NOT EXISTS (SELECT 1 FROM dbo.CaLamViec WHERE MaCa = 2)
    INSERT dbo.CaLamViec (MaCa, TenCa, GioBatDau, GioKetThuc) VALUES (2, N'Ca 2', '13:00', '17:00');
IF NOT EXISTS (SELECT 1 FROM dbo.CaLamViec WHERE MaCa = 3)
    INSERT dbo.CaLamViec (MaCa, TenCa, GioBatDau, GioKetThuc) VALUES (3, N'Ca 3', '17:00', '21:00');
GO

-- 2. BẢNG TÀI KHOẢN
IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(255) NOT NULL,
    MaNV VARCHAR(20) NULL FOREIGN KEY REFERENCES NhanVien(MaNV) ON DELETE SET NULL,
    VaiTro NVARCHAR(50) NOT NULL, -- 'Admin' hoặc 'Thu ngân'
    TrangThai BIT NOT NULL DEFAULT 1,
    YeuCauDoiMatKhau BIT NOT NULL DEFAULT 0
);
GO

-- 3. BẢNG PHIM
IF OBJECT_ID(N'dbo.Phim', N'U') IS NULL CREATE TABLE Phim (
    MaPhim VARCHAR(20) PRIMARY KEY,
    TenPhim NVARCHAR(150) NOT NULL,
    TheLoai NVARCHAR(100) NOT NULL,
    NgonNgu NVARCHAR(50) NOT NULL,
    DoTuoi NVARCHAR(20) NOT NULL, -- 'P', 'K', 'T13', 'T16', 'T18'
    ThoiLuong INT NOT NULL DEFAULT 120,
    GiaPhim DECIMAL(18,0) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    PosterPath NVARCHAR(500) NULL,
    NgayBatDauChieu DATE NULL,
    NgayKetThucChieu DATE NULL,
    CONSTRAINT CK_Phim_ThoiLuong_Positive CHECK (ThoiLuong > 0),
    CONSTRAINT CK_Phim_GiaPhim_NonNegative CHECK (GiaPhim >= 0),
    CONSTRAINT CK_Phim_ThoiHanChieu_Valid CHECK (
        (NgayBatDauChieu IS NULL AND NgayKetThucChieu IS NULL) OR
        (NgayBatDauChieu IS NOT NULL AND NgayKetThucChieu IS NOT NULL AND NgayKetThucChieu >= NgayBatDauChieu)
    )
);
GO

-- 4. BẢNG PHÒNG CHIẾU
IF OBJECT_ID(N'dbo.PhongChieu', N'U') IS NULL CREATE TABLE PhongChieu (
    MaPhong VARCHAR(20) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    SoHangGhe INT NOT NULL,     -- Số hàng (Ví dụ: 6 hàng từ A đến F)
    SoGheMoiHang INT NOT NULL, -- Số ghế mỗi hàng (Ví dụ: 10 ghế từ 1 đến 10)
    CONSTRAINT CK_PhongChieu_SoHangGhe_Positive CHECK (SoHangGhe > 0),
    CONSTRAINT CK_PhongChieu_SoGheMoiHang_Positive CHECK (SoGheMoiHang > 0)
);
GO

-- 5. BẢNG ĐỒ ĂN
IF OBJECT_ID(N'dbo.DoAn', N'U') IS NULL CREATE TABLE DoAn (
    MaDoAn VARCHAR(20) PRIMARY KEY,
    TenDoAn NVARCHAR(100) NOT NULL,
    Loai NVARCHAR(50) NULL,
    Gia DECIMAL(18,0) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    HinhAnhPath NVARCHAR(500) NULL,
    CONSTRAINT CK_DoAn_Gia_NonNegative CHECK (Gia >= 0)
);
GO

-- 5.1. CỠ VÀ GIÁ BẮP NƯỚC
IF OBJECT_ID(N'dbo.DoAnKichCo', N'U') IS NULL CREATE TABLE dbo.DoAnKichCo (
    MaDoAn VARCHAR(20) NOT NULL,
    MaKichCo VARCHAR(10) NOT NULL,
    TenKichCo NVARCHAR(40) NOT NULL,
    DonGia DECIMAL(18,0) NOT NULL,
    CONSTRAINT PK_DoAnKichCo PRIMARY KEY (MaDoAn, MaKichCo),
    CONSTRAINT FK_DoAnKichCo_DoAn FOREIGN KEY (MaDoAn) REFERENCES dbo.DoAn(MaDoAn) ON DELETE CASCADE,
    CONSTRAINT CK_DoAnKichCo_DonGia_NonNegative CHECK (DonGia >= 0)
);
GO

-- 6. BẢNG SUẤT CHIẾU
IF OBJECT_ID(N'dbo.SuatChieu', N'U') IS NULL CREATE TABLE SuatChieu (
    MaSuatChieu VARCHAR(20) PRIMARY KEY,
    MaPhim VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Phim(MaPhim) ON DELETE CASCADE,
    MaPhong VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES PhongChieu(MaPhong) ON DELETE CASCADE,
    NgayChieu DATE NOT NULL,
    GioChieu TIME(0) NOT NULL,
    GiaSuatChieu DECIMAL(18,0) NULL,
    CONSTRAINT CK_SuatChieu_Gia_NonNegative CHECK (GiaSuatChieu IS NULL OR GiaSuatChieu >= 0)
);
GO

-- 7. BẢNG ƯU ĐÃI COMBO
IF OBJECT_ID(N'dbo.UuDaiCombo', N'U') IS NULL CREATE TABLE UuDaiCombo (
    MaCombo VARCHAR(20) PRIMARY KEY,
    TenCombo NVARCHAR(100) NOT NULL,
    SoLuongGhe INT NOT NULL,       -- Số ghế bắt buộc phải chọn để áp dụng
    PhanTramGiam DECIMAL(5,2) NOT NULL, -- % giảm giá vé phim (không giảm đồ ăn)
    MoTa NVARCHAR(255) NULL,
    CONSTRAINT CK_UuDaiCombo_SoLuongGhe_Positive CHECK (SoLuongGhe > 0),
    CONSTRAINT CK_UuDaiCombo_PhanTramGiam_Range CHECK (PhanTramGiam BETWEEN 0 AND 100)
);
GO

-- 7.1. BẢNG VOUCHER KHUYẾN MÃI
IF OBJECT_ID(N'dbo.Voucher', N'U') IS NULL CREATE TABLE Voucher (
    MaVoucher VARCHAR(20) PRIMARY KEY,
    TenVoucher NVARCHAR(100) NOT NULL,
    LoaiGiam NVARCHAR(50) NOT NULL, -- N'Giảm phần trăm', N'Giảm số tiền', N'Giảm số vé'
    GiaTriGiam DECIMAL(18,2) NOT NULL,
    HanSuDung DATE NOT NULL,
    MoTa NVARCHAR(255) NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Voucher_GiaTriGiam_Positive CHECK (GiaTriGiam > 0),
    CONSTRAINT CK_Voucher_LoaiGiam_Valid CHECK (
        (LoaiGiam IN (N'Giảm phần trăm (%)', N'Giảm phần trăm') AND GiaTriGiam <= 100) OR
        (LoaiGiam IN (N'Giảm số tiền (VNĐ)', N'Giảm số tiền') AND GiaTriGiam <= 10000000) OR
        (LoaiGiam IN (N'Giảm số vé (Vé)', N'Giảm số vé') AND GiaTriGiam <= 10 AND GiaTriGiam = FLOOR(GiaTriGiam))
    )
);
GO

-- 8. BẢNG HÓA ĐƠN
IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NULL CREATE TABLE HoaDon (
    MaHD VARCHAR(20) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV VARCHAR(20) NULL FOREIGN KEY REFERENCES NhanVien(MaNV),
    MaSuatChieu VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES SuatChieu(MaSuatChieu),
    MaCombo VARCHAR(20) NULL FOREIGN KEY REFERENCES UuDaiCombo(MaCombo),
    MaVoucher VARCHAR(20) NULL FOREIGN KEY REFERENCES Voucher(MaVoucher) ON DELETE SET NULL,
    DanhSachGhe NVARCHAR(255) NOT NULL,
    SoLuongVe INT NOT NULL,
    TienVe DECIMAL(18,0) NOT NULL,
    TienGiamGia DECIMAL(18,0) NOT NULL DEFAULT 0,
    TienDoAn DECIMAL(18,0) NOT NULL DEFAULT 0,
    TongTien DECIMAL(18,0) NOT NULL,
    PhuongThucTT NVARCHAR(50) NOT NULL, -- 'Tiền mặt' hoặc 'Chuyển khoản'
    GhiChu NVARCHAR(255) NULL,
    TenDangNhapNguoiBan VARCHAR(50) NULL,
    TrangThaiThanhToan VARCHAR(20) NOT NULL DEFAULT 'Paid',
    PayOSOrderCode BIGINT NULL,
    PayOSPaymentLinkId VARCHAR(64) NULL,
    CONSTRAINT UQ_HoaDon_MaHD_MaSuatChieu UNIQUE (MaHD, MaSuatChieu),
    CONSTRAINT CK_HoaDon_SoLuongVe_Positive CHECK (SoLuongVe > 0),
    CONSTRAINT CK_HoaDon_Amounts_NonNegative CHECK (TienVe >= 0 AND TienGiamGia >= 0 AND TienDoAn >= 0 AND TongTien >= 0),
    CONSTRAINT CK_HoaDon_TrangThaiThanhToan_Valid CHECK (TrangThaiThanhToan IN ('Paid', 'Pending', 'Expired', 'Cancelled'))
);
GO

-- 9. CHI TIẾT GHẾ CỦA HÓA ĐƠN
IF OBJECT_ID(N'dbo.ChiTietHoaDon_Ghe', N'U') IS NULL CREATE TABLE ChiTietHoaDon_Ghe (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MaHD VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
    MaSuatChieu VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES SuatChieu(MaSuatChieu),
    MaGhe VARCHAR(10) NOT NULL, -- Vd: 'A1', 'A2', 'B3'
    CONSTRAINT FK_ChiTietHoaDon_Ghe_HoaDon_Suat FOREIGN KEY (MaHD, MaSuatChieu)
        REFERENCES dbo.HoaDon(MaHD, MaSuatChieu)
);
GO

-- 10. CHI TIẾT ĐỒ ĂN CỦA HÓA ĐƠN
IF OBJECT_ID(N'dbo.ChiTietHoaDon_DoAn', N'U') IS NULL CREATE TABLE ChiTietHoaDon_DoAn (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MaHD VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
    MaDoAn VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES DoAn(MaDoAn),
    MaKichCo VARCHAR(10) NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,0) NOT NULL,
    ThanhTien DECIMAL(18,0) NOT NULL,
    CONSTRAINT FK_ChiTietHoaDon_DoAn_KichCo FOREIGN KEY (MaDoAn, MaKichCo)
        REFERENCES dbo.DoAnKichCo(MaDoAn, MaKichCo),
    CONSTRAINT CK_ChiTietHoaDon_DoAn_SoLuong_Positive CHECK (SoLuong > 0),
    CONSTRAINT CK_ChiTietHoaDon_DoAn_Amounts_NonNegative CHECK (DonGia >= 0 AND ThanhTien >= 0)
);
GO

-- ============================================================
IF OBJECT_ID(N'dbo.SeqHoaDon', N'SO') IS NULL
    EXEC(N'CREATE SEQUENCE dbo.SeqHoaDon AS BIGINT START WITH 1 INCREMENT BY 1');

IF OBJECT_ID(N'dbo.GheDatCho', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GheDatCho (
        MaSuatChieu VARCHAR(20) NOT NULL REFERENCES dbo.SuatChieu(MaSuatChieu),
        MaGhe VARCHAR(10) NOT NULL,
        MaHD VARCHAR(20) NOT NULL REFERENCES dbo.HoaDon(MaHD),
        HetHan DATETIME2 NULL,
        CONSTRAINT PK_GheDatCho PRIMARY KEY (MaSuatChieu, MaGhe),
        CONSTRAINT FK_GheDatCho_HoaDon_Suat FOREIGN KEY (MaHD, MaSuatChieu)
            REFERENCES dbo.HoaDon(MaHD, MaSuatChieu)
    );
END

IF OBJECT_ID(N'dbo.VoucherClaim', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.VoucherClaim (
        MaVoucher VARCHAR(20) NOT NULL PRIMARY KEY REFERENCES dbo.Voucher(MaVoucher),
        MaHD VARCHAR(20) NOT NULL REFERENCES dbo.HoaDon(MaHD),
        TrangThai VARCHAR(12) NOT NULL,
        CONSTRAINT CK_VoucherClaim_TrangThai_Valid CHECK (TrangThai IN ('Pending', 'Used')),
        HetHan DATETIME2 NULL
    );
END
GO



-- PHẦN MIGRATION: 20261014_RolesAndPasswordStorage.sql

/*
  Idempotent upgrade for the Admin/Cashier roles and password storage.
  Run against QuanLyRapChieuPhim. This migration does not drop tables or remove data.
  Legacy plaintext passwords are upgraded after the account's next successful login.
*/
USE QuanLyRapChieuPhim;
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


-- PHẦN MIGRATION: 20261014_SalesAndPayOsSafety.sql

/* Safe, repeatable schema update. Run in QuanLyRapChieuPhim; preserves existing invoices. */
USE QuanLyRapChieuPhim;
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


-- PHẦN MIGRATION: 20261015_SpecializedAdminRoles.sql

/* Expand the former Admin role into job-specific roles. Existing admin access is assigned to HR/account management. */
USE QuanLyRapChieuPhim;
GO

UPDATE dbo.TaiKhoan
SET VaiTro = 'AdminNhanSu'
WHERE VaiTro IN (N'Admin', N'Quản lý', N'Quan ly', N'Manager');
GO

UPDATE dbo.TaiKhoan
SET VaiTro = 'Cashier'
WHERE VaiTro IN (N'Thu ngân', N'Nhân viên', N'Nhan vien', N'Staff');
GO


-- PHẦN MIGRATION: 20261015_AdminTong.sql

/* Promote the existing admin login to the single all-pages administrator. Password and account data stay unchanged. */
USE QuanLyRapChieuPhim;
GO

UPDATE dbo.TaiKhoan
SET VaiTro = 'AdminTong'
WHERE TenDangNhap = 'admin' AND VaiTro = 'AdminNhanSu';
GO


-- PHẦN MIGRATION: 20261022_DataIntegrityAndTypedParameters.sql

/*
  Additive, repeatable integrity migration. Run after the four 20261014/20261015
  migrations. It never deletes, rewrites, or guesses at historical sales data.
  Any incompatible row is returned with its key and the migration is rolled back.
*/
USE QuanLyRapChieuPhim;
GO

SET QUOTED_IDENTIFIER ON;
GO
SET XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.Phim', N'U') IS NULL OR
       OBJECT_ID(N'dbo.PhongChieu', N'U') IS NULL OR
       OBJECT_ID(N'dbo.DoAn', N'U') IS NULL OR
       OBJECT_ID(N'dbo.SuatChieu', N'U') IS NULL OR
       OBJECT_ID(N'dbo.UuDaiCombo', N'U') IS NULL OR
       OBJECT_ID(N'dbo.Voucher', N'U') IS NULL OR
       OBJECT_ID(N'dbo.HoaDon', N'U') IS NULL OR
       OBJECT_ID(N'dbo.ChiTietHoaDon_Ghe', N'U') IS NULL OR
       OBJECT_ID(N'dbo.ChiTietHoaDon_DoAn', N'U') IS NULL OR
       OBJECT_ID(N'dbo.GheDatCho', N'U') IS NULL OR
       OBJECT_ID(N'dbo.VoucherClaim', N'U') IS NULL OR
       COL_LENGTH(N'dbo.TaiKhoan', N'YeuCauDoiMatKhau') IS NULL
        THROW 51020, 'Thiếu bảng/cột bắt buộc. Hãy chạy các migration 20261014/20261015 theo đúng thứ tự trước migration này.', 1;

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );
    END;

    DECLARE @Violations TABLE
    (
        ConstraintName NVARCHAR(128) NOT NULL,
        RecordKey NVARCHAR(200) NOT NULL,
        Details NVARCHAR(1000) NOT NULL
    );
    DECLARE @Review TABLE
    (
        MaHD VARCHAR(20) NOT NULL,
        Details NVARCHAR(1000) NOT NULL
    );

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_Phim_ThoiLuong_Positive', MaPhim, CONCAT(N'ThoiLuong=', ThoiLuong)
    FROM dbo.Phim WHERE ThoiLuong <= 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_Phim_GiaPhim_NonNegative', MaPhim, CONCAT(N'GiaPhim=', GiaPhim)
    FROM dbo.Phim WHERE GiaPhim < 0;

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_PhongChieu_SoHangGhe_Positive', MaPhong, CONCAT(N'SoHangGhe=', SoHangGhe)
    FROM dbo.PhongChieu WHERE SoHangGhe <= 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_PhongChieu_SoGheMoiHang_Positive', MaPhong, CONCAT(N'SoGheMoiHang=', SoGheMoiHang)
    FROM dbo.PhongChieu WHERE SoGheMoiHang <= 0;

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_DoAn_Gia_NonNegative', MaDoAn, CONCAT(N'Gia=', Gia)
    FROM dbo.DoAn WHERE Gia < 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_SuatChieu_Gia_NonNegative', MaSuatChieu, CONCAT(N'GiaSuatChieu=', GiaSuatChieu)
    FROM dbo.SuatChieu WHERE GiaSuatChieu < 0;

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_UuDaiCombo_SoLuongGhe_Positive', MaCombo, CONCAT(N'SoLuongGhe=', SoLuongGhe)
    FROM dbo.UuDaiCombo WHERE SoLuongGhe <= 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_UuDaiCombo_PhanTramGiam_Range', MaCombo, CONCAT(N'PhanTramGiam=', PhanTramGiam)
    FROM dbo.UuDaiCombo WHERE PhanTramGiam < 0 OR PhanTramGiam > 100;

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_Voucher_GiaTriGiam_Positive', MaVoucher, CONCAT(N'GiaTriGiam=', GiaTriGiam)
    FROM dbo.Voucher WHERE GiaTriGiam <= 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_Voucher_LoaiGiam_Valid', MaVoucher,
           CONCAT(N'LoaiGiam=', LoaiGiam, N'; GiaTriGiam=', GiaTriGiam)
    FROM dbo.Voucher
    WHERE NOT
    (
        (LoaiGiam IN (N'Giảm phần trăm (%)', N'Giảm phần trăm') AND GiaTriGiam > 0 AND GiaTriGiam <= 100) OR
        (LoaiGiam IN (N'Giảm số tiền (VNĐ)', N'Giảm số tiền') AND GiaTriGiam > 0 AND GiaTriGiam <= 10000000) OR
        (LoaiGiam IN (N'Giảm số vé (Vé)', N'Giảm số vé') AND GiaTriGiam > 0 AND GiaTriGiam <= 10 AND GiaTriGiam = FLOOR(GiaTriGiam))
    );

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_HoaDon_SoLuongVe_Positive', MaHD, CONCAT(N'SoLuongVe=', SoLuongVe)
    FROM dbo.HoaDon WHERE SoLuongVe <= 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_HoaDon_Amounts_NonNegative', MaHD,
           CONCAT(N'TienVe=', TienVe, N'; TienGiamGia=', TienGiamGia, N'; TienDoAn=', TienDoAn, N'; TongTien=', TongTien)
    FROM dbo.HoaDon WHERE TienVe < 0 OR TienGiamGia < 0 OR TienDoAn < 0 OR TongTien < 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_HoaDon_TrangThaiThanhToan_Valid', MaHD, CONCAT(N'TrangThaiThanhToan=', TrangThaiThanhToan)
    FROM dbo.HoaDon WHERE TrangThaiThanhToan NOT IN ('Paid', 'Pending', 'Expired', 'Cancelled');

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_ChiTietHoaDon_DoAn_SoLuong_Positive', CONCAT(MaHD, N'/', Id), CONCAT(N'SoLuong=', SoLuong)
    FROM dbo.ChiTietHoaDon_DoAn WHERE SoLuong <= 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_ChiTietHoaDon_DoAn_Amounts_NonNegative', CONCAT(MaHD, N'/', Id), CONCAT(N'DonGia=', DonGia, N'; ThanhTien=', ThanhTien)
    FROM dbo.ChiTietHoaDon_DoAn WHERE DonGia < 0 OR ThanhTien < 0;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'CK_VoucherClaim_TrangThai_Valid', MaVoucher, CONCAT(N'TrangThai=', TrangThai)
    FROM dbo.VoucherClaim WHERE TrangThai NOT IN ('Pending', 'Used');

    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'FK_ChiTietHoaDon_Ghe_HoaDon_Suat', ct.MaHD,
           CONCAT(N'Chi tiết ghế dùng suất ', ct.MaSuatChieu, N' nhưng hóa đơn dùng suất ', COALESCE(hd.MaSuatChieu, N'<không tồn tại>'), N'; ghế=', ct.MaGhe)
    FROM dbo.ChiTietHoaDon_Ghe ct
    LEFT JOIN dbo.HoaDon hd ON hd.MaHD = ct.MaHD
    WHERE hd.MaHD IS NULL OR ct.MaSuatChieu <> hd.MaSuatChieu;
    INSERT INTO @Violations (ConstraintName, RecordKey, Details)
    SELECT N'FK_GheDatCho_HoaDon_Suat', gd.MaHD,
           CONCAT(N'Ghế giữ chỗ dùng suất ', gd.MaSuatChieu, N' nhưng hóa đơn dùng suất ', COALESCE(hd.MaSuatChieu, N'<không tồn tại>'), N'; ghế=', gd.MaGhe)
    FROM dbo.GheDatCho gd
    LEFT JOIN dbo.HoaDon hd ON hd.MaHD = gd.MaHD
    WHERE hd.MaHD IS NULL OR gd.MaSuatChieu <> hd.MaSuatChieu;

    /* Report legacy invoice/detail differences without rewriting either representation. */
    INSERT INTO @Review (MaHD, Details)
    SELECT hd.MaHD, CONCAT(N'SoLuongVe=', hd.SoLuongVe, N'; số dòng ghế chi tiết=', COUNT(ct.Id))
    FROM dbo.HoaDon hd
    LEFT JOIN dbo.ChiTietHoaDon_Ghe ct ON ct.MaHD = hd.MaHD
    GROUP BY hd.MaHD, hd.SoLuongVe
    HAVING hd.SoLuongVe <> COUNT(ct.Id);

    INSERT INTO @Review (MaHD, Details)
    SELECT hd.MaHD, N'DanhSachGhe không khớp tập ghế trong ChiTietHoaDon_Ghe; dữ liệu lịch sử cần được xem xét thủ công.'
    FROM dbo.HoaDon hd
    WHERE EXISTS
    (
        SELECT LTRIM(RTRIM(value)) FROM STRING_SPLIT(hd.DanhSachGhe, ',')
        EXCEPT
        SELECT ct.MaGhe FROM dbo.ChiTietHoaDon_Ghe ct WHERE ct.MaHD = hd.MaHD
    ) OR EXISTS
    (
        SELECT ct.MaGhe FROM dbo.ChiTietHoaDon_Ghe ct WHERE ct.MaHD = hd.MaHD
        EXCEPT
        SELECT LTRIM(RTRIM(value)) FROM STRING_SPLIT(hd.DanhSachGhe, ',')
    );

    IF EXISTS (SELECT 1 FROM @Review)
        SELECT MaHD, Details FROM @Review ORDER BY MaHD, Details;

    IF EXISTS (SELECT 1 FROM @Violations)
    BEGIN
        SELECT ConstraintName, RecordKey, Details FROM @Violations ORDER BY ConstraintName, RecordKey;
        THROW 51021, 'Migration dừng: dữ liệu hiện có không tương thích. Hãy xem các mã bản ghi trong kết quả, sao lưu và xử lý thủ công rồi chạy lại.', 1;
    END;

    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.Phim') AND name = N'CK_Phim_ThoiLuong_Positive')
        ALTER TABLE dbo.Phim WITH CHECK ADD CONSTRAINT CK_Phim_ThoiLuong_Positive CHECK (ThoiLuong > 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.Phim') AND name = N'CK_Phim_GiaPhim_NonNegative')
        ALTER TABLE dbo.Phim WITH CHECK ADD CONSTRAINT CK_Phim_GiaPhim_NonNegative CHECK (GiaPhim >= 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.PhongChieu') AND name = N'CK_PhongChieu_SoHangGhe_Positive')
        ALTER TABLE dbo.PhongChieu WITH CHECK ADD CONSTRAINT CK_PhongChieu_SoHangGhe_Positive CHECK (SoHangGhe > 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.PhongChieu') AND name = N'CK_PhongChieu_SoGheMoiHang_Positive')
        ALTER TABLE dbo.PhongChieu WITH CHECK ADD CONSTRAINT CK_PhongChieu_SoGheMoiHang_Positive CHECK (SoGheMoiHang > 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.DoAn') AND name = N'CK_DoAn_Gia_NonNegative')
        ALTER TABLE dbo.DoAn WITH CHECK ADD CONSTRAINT CK_DoAn_Gia_NonNegative CHECK (Gia >= 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.SuatChieu') AND name = N'CK_SuatChieu_Gia_NonNegative')
        ALTER TABLE dbo.SuatChieu WITH CHECK ADD CONSTRAINT CK_SuatChieu_Gia_NonNegative CHECK (GiaSuatChieu IS NULL OR GiaSuatChieu >= 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.UuDaiCombo') AND name = N'CK_UuDaiCombo_SoLuongGhe_Positive')
        ALTER TABLE dbo.UuDaiCombo WITH CHECK ADD CONSTRAINT CK_UuDaiCombo_SoLuongGhe_Positive CHECK (SoLuongGhe > 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.UuDaiCombo') AND name = N'CK_UuDaiCombo_PhanTramGiam_Range')
        ALTER TABLE dbo.UuDaiCombo WITH CHECK ADD CONSTRAINT CK_UuDaiCombo_PhanTramGiam_Range CHECK (PhanTramGiam BETWEEN 0 AND 100);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.Voucher') AND name = N'CK_Voucher_GiaTriGiam_Positive')
        ALTER TABLE dbo.Voucher WITH CHECK ADD CONSTRAINT CK_Voucher_GiaTriGiam_Positive CHECK (GiaTriGiam > 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.Voucher') AND name = N'CK_Voucher_LoaiGiam_Valid')
        ALTER TABLE dbo.Voucher WITH CHECK ADD CONSTRAINT CK_Voucher_LoaiGiam_Valid CHECK (
            (LoaiGiam IN (N'Giảm phần trăm (%)', N'Giảm phần trăm') AND GiaTriGiam <= 100) OR
            (LoaiGiam IN (N'Giảm số tiền (VNĐ)', N'Giảm số tiền') AND GiaTriGiam <= 10000000) OR
            (LoaiGiam IN (N'Giảm số vé (Vé)', N'Giảm số vé') AND GiaTriGiam <= 10 AND GiaTriGiam = FLOOR(GiaTriGiam))
        );
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.HoaDon') AND name = N'CK_HoaDon_SoLuongVe_Positive')
        ALTER TABLE dbo.HoaDon WITH CHECK ADD CONSTRAINT CK_HoaDon_SoLuongVe_Positive CHECK (SoLuongVe > 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.HoaDon') AND name = N'CK_HoaDon_Amounts_NonNegative')
        ALTER TABLE dbo.HoaDon WITH CHECK ADD CONSTRAINT CK_HoaDon_Amounts_NonNegative CHECK (TienVe >= 0 AND TienGiamGia >= 0 AND TienDoAn >= 0 AND TongTien >= 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.HoaDon') AND name = N'CK_HoaDon_TrangThaiThanhToan_Valid')
        ALTER TABLE dbo.HoaDon WITH CHECK ADD CONSTRAINT CK_HoaDon_TrangThaiThanhToan_Valid CHECK (TrangThaiThanhToan IN ('Paid', 'Pending', 'Expired', 'Cancelled'));
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.ChiTietHoaDon_DoAn') AND name = N'CK_ChiTietHoaDon_DoAn_SoLuong_Positive')
        ALTER TABLE dbo.ChiTietHoaDon_DoAn WITH CHECK ADD CONSTRAINT CK_ChiTietHoaDon_DoAn_SoLuong_Positive CHECK (SoLuong > 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.ChiTietHoaDon_DoAn') AND name = N'CK_ChiTietHoaDon_DoAn_Amounts_NonNegative')
        ALTER TABLE dbo.ChiTietHoaDon_DoAn WITH CHECK ADD CONSTRAINT CK_ChiTietHoaDon_DoAn_Amounts_NonNegative CHECK (DonGia >= 0 AND ThanhTien >= 0);
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.VoucherClaim') AND name = N'CK_VoucherClaim_TrangThai_Valid')
        ALTER TABLE dbo.VoucherClaim WITH CHECK ADD CONSTRAINT CK_VoucherClaim_TrangThai_Valid CHECK (TrangThai IN ('Pending', 'Used'));

    IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID(N'dbo.HoaDon') AND name = N'UQ_HoaDon_MaHD_MaSuatChieu')
        ALTER TABLE dbo.HoaDon ADD CONSTRAINT UQ_HoaDon_MaHD_MaSuatChieu UNIQUE (MaHD, MaSuatChieu);
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID(N'dbo.ChiTietHoaDon_Ghe') AND name = N'FK_ChiTietHoaDon_Ghe_HoaDon_Suat')
        ALTER TABLE dbo.ChiTietHoaDon_Ghe WITH CHECK ADD CONSTRAINT FK_ChiTietHoaDon_Ghe_HoaDon_Suat
            FOREIGN KEY (MaHD, MaSuatChieu) REFERENCES dbo.HoaDon(MaHD, MaSuatChieu);
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID(N'dbo.GheDatCho') AND name = N'FK_GheDatCho_HoaDon_Suat')
        ALTER TABLE dbo.GheDatCho WITH CHECK ADD CONSTRAINT FK_GheDatCho_HoaDon_Suat
            FOREIGN KEY (MaHD, MaSuatChieu) REFERENCES dbo.HoaDon(MaHD, MaSuatChieu);

    /* Also repair trust/enablement if a constraint with our name was disabled manually. */
    ALTER TABLE dbo.Phim WITH CHECK CHECK CONSTRAINT CK_Phim_ThoiLuong_Positive, CK_Phim_GiaPhim_NonNegative;
    ALTER TABLE dbo.PhongChieu WITH CHECK CHECK CONSTRAINT CK_PhongChieu_SoHangGhe_Positive, CK_PhongChieu_SoGheMoiHang_Positive;
    ALTER TABLE dbo.DoAn WITH CHECK CHECK CONSTRAINT CK_DoAn_Gia_NonNegative;
    ALTER TABLE dbo.SuatChieu WITH CHECK CHECK CONSTRAINT CK_SuatChieu_Gia_NonNegative;
    ALTER TABLE dbo.UuDaiCombo WITH CHECK CHECK CONSTRAINT CK_UuDaiCombo_SoLuongGhe_Positive, CK_UuDaiCombo_PhanTramGiam_Range;
    ALTER TABLE dbo.Voucher WITH CHECK CHECK CONSTRAINT CK_Voucher_GiaTriGiam_Positive, CK_Voucher_LoaiGiam_Valid;
    ALTER TABLE dbo.HoaDon WITH CHECK CHECK CONSTRAINT CK_HoaDon_SoLuongVe_Positive, CK_HoaDon_Amounts_NonNegative, CK_HoaDon_TrangThaiThanhToan_Valid;
    ALTER TABLE dbo.ChiTietHoaDon_DoAn WITH CHECK CHECK CONSTRAINT CK_ChiTietHoaDon_DoAn_SoLuong_Positive, CK_ChiTietHoaDon_DoAn_Amounts_NonNegative;
    ALTER TABLE dbo.VoucherClaim WITH CHECK CHECK CONSTRAINT CK_VoucherClaim_TrangThai_Valid;
    ALTER TABLE dbo.ChiTietHoaDon_Ghe WITH CHECK CHECK CONSTRAINT FK_ChiTietHoaDon_Ghe_HoaDon_Suat;
    ALTER TABLE dbo.GheDatCho WITH CHECK CHECK CONSTRAINT FK_GheDatCho_HoaDon_Suat;

    /* Set this only once; rerunning this migration must not re-force a changed password. */
    IF NOT EXISTS (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId = '20261022_DataIntegrityAndTypedParameters')
    BEGIN
        UPDATE dbo.TaiKhoan
        SET YeuCauDoiMatKhau = 1
        WHERE TenDangNhap = 'admin';

        INSERT INTO dbo.SchemaMigration (MigrationId)
        VALUES ('20261022_DataIntegrityAndTypedParameters');
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO


-- PHẦN MIGRATION: 20261022_FoodSizesAndImages.sql

/*
  Additive migration for menu descriptions/images and size-specific prices.
  Historical food details keep their saved quantity and price; their size stays NULL
  because the old schema did not record one.
*/
USE QuanLyRapChieuPhim;
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


-- PHẦN MIGRATION: 20261023_MovieReleaseWindow.sql

/*
  Adds an inclusive cinema release window to each movie.
  Existing movie rows remain unrestricted (both dates NULL) so prior schedules are preserved.
  New movie records created through the application require both dates.
*/
USE QuanLyRapChieuPhim;
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


-- PHẦN MIGRATION: 20261023_CashierWorkShifts.sql

/*
  Adds three daily work shifts and employee shift assignments.
  Existing employee and sales records are not changed or removed.
  Run after 20261023_MovieReleaseWindow.sql on an existing database.
*/
USE QuanLyRapChieuPhim;
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


-- Migration giá vé riêng theo từng hàng ghế.
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


GO
-- CẬP NHẬT RÀNG BUỘC VOUCHER TIẾNG VIỆT
-- Run with UTF-8: sqlcmd -f 65001 -I -b -i <this file>
USE QuanLyRapChieuPhim;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
BEGIN TRANSACTION;
IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID('dbo.Voucher') AND name='CK_Voucher_LoaiGiam_Valid')
    ALTER TABLE dbo.Voucher DROP CONSTRAINT CK_Voucher_LoaiGiam_Valid;
ALTER TABLE dbo.Voucher WITH CHECK ADD CONSTRAINT CK_Voucher_LoaiGiam_Valid CHECK (
    (LoaiGiam IN (N'Giảm phần trăm (%)', N'Giảm phần trăm') AND GiaTriGiam > 0 AND GiaTriGiam <= 100) OR
    (LoaiGiam IN (N'Giảm số tiền (VNĐ)', N'Giảm số tiền') AND GiaTriGiam > 0 AND GiaTriGiam <= 10000000) OR
    (LoaiGiam IN (N'Giảm số vé (Vé)', N'Giảm số vé') AND GiaTriGiam > 0 AND GiaTriGiam <= 10 AND GiaTriGiam = FLOOR(GiaTriGiam))
);
COMMIT;

GO
-- CẤU HÌNH HÀNG GHẾ VIP VÀ PHẦN TRĂM TĂNG GIÁ
-- Optional manual installation; the application also creates this table on first use.
USE QuanLyRapChieuPhim;
GO
IF OBJECT_ID(N'dbo.PhongChieuVip',N'U') IS NULL
CREATE TABLE dbo.PhongChieuVip (
    MaPhong VARCHAR(20) NOT NULL PRIMARY KEY REFERENCES dbo.PhongChieu(MaPhong) ON DELETE CASCADE,
    HangVip VARCHAR(51) NOT NULL DEFAULT '',
    PhanTramTang DECIMAL(7,2) NOT NULL DEFAULT 0 CHECK (PhanTramTang BETWEEN 0 AND 500)
);
GO

GO
-- DỮ LIỆU KHỞI TẠO: DUY NHẤT TÀI KHOẢN ADMIN (CHỈ THÊM NẾU CHƯA CÓ)
-- ============================================================

IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaNV = 'QL01')
    INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, SDT, CCCD, ChucVu) VALUES
    ('QL01', N'Quản trị viên NHH', '1995-01-01', '0987654321', '001200000001', N'Quản lý');

-- Tài khoản quản lý duy nhất để người dùng tự trải nghiệm hệ thống
-- Tên đăng nhập: admin / Mật khẩu: admin
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'admin')
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNV, VaiTro, TrangThai, YeuCauDoiMatKhau) VALUES
    ('admin', 'admin', 'QL01', N'AdminTong', 1, 1);


-- PHẦN MIGRATION: 20261103_CashierAccountShiftAssignments.sql
-- Migration lịch sử: thêm tài khoản vào ca; migration tiếp theo điều chỉnh nhân viên theo từng ca.
USE QuanLyRapChieuPhim;
GO

SET XACT_ABORT ON;
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

    IF EXISTS
    (
        SELECT 1 FROM dbo.TaiKhoan
        WHERE VaiTro = 'Cashier' AND MaNV IS NOT NULL
        GROUP BY MaNV HAVING COUNT(*) > 1
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

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.TaiKhoan') AND name=N'UX_TaiKhoan_TenDangNhap_MaNV')
        CREATE UNIQUE INDEX UX_TaiKhoan_TenDangNhap_MaNV ON dbo.TaiKhoan (TenDangNhap, MaNV);

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.TaiKhoan') AND name=N'UX_TaiKhoan_Cashier_MaNV')
        CREATE UNIQUE INDEX UX_TaiKhoan_Cashier_MaNV ON dbo.TaiKhoan (MaNV)
            WHERE VaiTro = 'Cashier' AND MaNV IS NOT NULL;

    EXEC sys.sp_executesql N'
        UPDATE pc SET pc.TenDangNhapThuNgan=tk.TenDangNhap
        FROM dbo.LichLamViec pc
        INNER JOIN dbo.TaiKhoan tk ON tk.MaNV=pc.MaNV AND tk.VaiTro=''Cashier''
        WHERE pc.TenDangNhapThuNgan IS NULL;';

    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id=OBJECT_ID(N'dbo.LichLamViec') AND name=N'FK_LichLamViec_TaiKhoanNhanVien')
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.LichLamViec WITH CHECK ADD CONSTRAINT FK_LichLamViec_TaiKhoanNhanVien
                FOREIGN KEY (TenDangNhapThuNgan, MaNV) REFERENCES dbo.TaiKhoan (TenDangNhap, MaNV);';

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LichLamViec') AND name=N'UX_LichLamViec_Ngay_TaiKhoan_Active')
        EXEC sys.sp_executesql N'
            CREATE UNIQUE INDEX UX_LichLamViec_Ngay_TaiKhoan_Active
                ON dbo.LichLamViec (NgayLam, TenDangNhapThuNgan)
                WHERE TenDangNhapThuNgan IS NOT NULL AND TrangThai IN (''Scheduled'', ''Completed'');';

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );

    IF NOT EXISTS (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId='20261103_CashierAccountShiftAssignments')
        INSERT dbo.SchemaMigration (MigrationId) VALUES ('20261103_CashierAccountShiftAssignments');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO

-- PHẦN MIGRATION: 20261103_CashierShiftEmployees.sql
-- Tài khoản là quầy đăng nhập; nhân viên được chọn riêng cho từng ca.
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
        SELECT 1 FROM dbo.LichLamViec
        WHERE TenDangNhapThuNgan IS NOT NULL AND TrangThai IN ('Scheduled','Completed')
        GROUP BY NgayLam, MaCa, TenDangNhapThuNgan HAVING COUNT(*) > 1
    )
    BEGIN
        SELECT NgayLam, MaCa, TenDangNhapThuNgan, COUNT(*) AS SoLichTrung
        FROM dbo.LichLamViec
        WHERE TenDangNhapThuNgan IS NOT NULL AND TrangThai IN ('Scheduled','Completed')
        GROUP BY NgayLam, MaCa, TenDangNhapThuNgan HAVING COUNT(*) > 1;
        THROW 51322, N'Một tài khoản đang có nhiều nhân viên trong cùng ca. Hãy xử lý trước khi chạy lại migration.', 1;
    END;

    IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id=OBJECT_ID(N'dbo.LichLamViec') AND name=N'FK_LichLamViec_TaiKhoanNhanVien')
        ALTER TABLE dbo.LichLamViec DROP CONSTRAINT FK_LichLamViec_TaiKhoanNhanVien;
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LichLamViec') AND name=N'UX_LichLamViec_Ngay_TaiKhoan_Active')
        DROP INDEX UX_LichLamViec_Ngay_TaiKhoan_Active ON dbo.LichLamViec;
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.TaiKhoan') AND name=N'UX_TaiKhoan_Cashier_MaNV')
        DROP INDEX UX_TaiKhoan_Cashier_MaNV ON dbo.TaiKhoan;
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.TaiKhoan') AND name=N'UX_TaiKhoan_TenDangNhap_MaNV')
        DROP INDEX UX_TaiKhoan_TenDangNhap_MaNV ON dbo.TaiKhoan;

    UPDATE dbo.TaiKhoan SET MaNV=NULL WHERE VaiTro='Cashier' AND MaNV IS NOT NULL;

    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE parent_object_id=OBJECT_ID(N'dbo.LichLamViec') AND name=N'FK_LichLamViec_TaiKhoanThuNgan')
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.LichLamViec WITH CHECK ADD CONSTRAINT FK_LichLamViec_TaiKhoanThuNgan
                FOREIGN KEY (TenDangNhapThuNgan) REFERENCES dbo.TaiKhoan (TenDangNhap);';

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LichLamViec') AND name=N'UX_LichLamViec_Ngay_Ca_TaiKhoan_Active')
        EXEC sys.sp_executesql N'
            CREATE UNIQUE INDEX UX_LichLamViec_Ngay_Ca_TaiKhoan_Active
                ON dbo.LichLamViec (NgayLam, MaCa, TenDangNhapThuNgan)
                WHERE TenDangNhapThuNgan IS NOT NULL AND TrangThai IN (''Scheduled'', ''Completed'');';

    IF OBJECT_ID(N'dbo.SchemaMigration', N'U') IS NULL
        CREATE TABLE dbo.SchemaMigration
        (
            MigrationId VARCHAR(128) NOT NULL CONSTRAINT PK_SchemaMigration PRIMARY KEY,
            AppliedAt DATETIME2(0) NOT NULL CONSTRAINT DF_SchemaMigration_AppliedAt DEFAULT (SYSUTCDATETIME())
        );

    IF NOT EXISTS (SELECT 1 FROM dbo.SchemaMigration WHERE MigrationId='20261103_CashierShiftEmployees')
        INSERT dbo.SchemaMigration (MigrationId) VALUES ('20261103_CashierShiftEmployees');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO



GO
SELECT DB_NAME() AS DatabaseName;
SELECT name AS TableName FROM sys.tables WHERE is_ms_shipped = 0 ORDER BY name;
GO
