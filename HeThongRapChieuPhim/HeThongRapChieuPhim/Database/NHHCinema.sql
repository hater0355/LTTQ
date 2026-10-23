-- ============================================================
-- HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM NHH CINEMA
-- CƠ SỞ DỮ LIỆU SQL SERVER (AN TOÀN, KHÔNG XÓA BẢNG/DỮ LIỆU KHI CHẠY LẠI)
-- ============================================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'NHHCinemaDB')
BEGIN
    CREATE DATABASE NHHCinemaDB COLLATE Vietnamese_CI_AS;
END
GO

USE NHHCinemaDB;
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
    CONSTRAINT CK_Phim_ThoiLuong_Positive CHECK (ThoiLuong > 0),
    CONSTRAINT CK_Phim_GiaPhim_NonNegative CHECK (GiaPhim >= 0)
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
-- DỮ LIỆU KHỞI TẠO: DUY NHẤT 1 TÀI KHOẢN QUẢN LÝ (ADMIN)
-- ============================================================

IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaNV = 'QL01')
    INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, SDT, CCCD, ChucVu) VALUES
    ('QL01', N'Quản trị viên NHH', '1995-01-01', '0987654321', '001200000001', N'Quản lý');

-- Tài khoản quản lý duy nhất để người dùng tự trải nghiệm hệ thống
-- Tên đăng nhập: admin / Mật khẩu: admin
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'admin')
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNV, VaiTro, TrangThai, YeuCauDoiMatKhau) VALUES
    ('admin', 'admin', 'QL01', N'AdminTong', 1, 1);

IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'admintong')
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNV, VaiTro, TrangThai, YeuCauDoiMatKhau) VALUES
    ('admintong', 'admin123', 'QL01', N'AdminTong', 1, 1);

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

