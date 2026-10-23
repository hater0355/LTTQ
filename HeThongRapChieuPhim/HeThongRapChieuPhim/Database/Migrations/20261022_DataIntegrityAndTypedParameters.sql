/*
  Additive, repeatable integrity migration. Run after the four 20261014/20261015
  migrations. It never deletes, rewrites, or guesses at historical sales data.
  Any incompatible row is returned with its key and the migration is rolled back.
*/
USE NHHCinemaDB;
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
        WHERE TenDangNhap IN ('admin', 'admintong');

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
