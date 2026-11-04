USE QuanLyRapChieuPhim;
GO

/*
    Dữ liệu phim và poster do người dùng cung cấp.
    Chạy sau Database/NHHcinema.sql bằng sqlcmd với mã hóa UTF-8, ví dụ:
    sqlcmd -S . -E -f 65001 -b -i "Database/Seeds/20261104_Seed_MoviePosters.sql"
    Chạy lại được: cập nhật phim đã tồn tại theo tên, chỉ thêm phim còn thiếu.
    Hai cột ngày giới hạn chiếu để NULL để các phim trong danh mục vẫn có thể
    được xếp suất chiếu; ngày khởi chiếu được ghi trong mô tả.
*/
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

DECLARE @Movies TABLE
(
    MaPhim VARCHAR(20) NOT NULL,
    TenPhim NVARCHAR(150) NOT NULL,
    TheLoai NVARCHAR(100) NOT NULL,
    NgonNgu NVARCHAR(50) NOT NULL,
    DoTuoi NVARCHAR(20) NOT NULL,
    ThoiLuong INT NOT NULL,
    GiaPhim DECIMAL(18,0) NOT NULL,
    PosterPath NVARCHAR(500) NOT NULL,
    MoTa NVARCHAR(MAX) NOT NULL
);

INSERT INTO @Movies (MaPhim, TenPhim, TheLoai, NgonNgu, DoTuoi, ThoiLuong, GiaPhim, PosterPath, MoTa)
VALUES
('MOV26_TBN', N'TRẠI BUÔN NGƯỜI', N'Hành động, Tội phạm, Tâm lý', N'Tiếng Việt', N'T18 (18+)', 135, 80000,
 N'Assets/Movies/Poster_TraiBuonNguoi.png',
 N'Khởi chiếu 25/09/2026. Ny, một chàng trai miền Tây, tìm cách cứu em gái bị dụ dỗ vào đường dây buôn người. Anh và người bạn thân lại bị bắt giữ trong một khu trại tội phạm xuyên biên giới, nơi nhiều nạn nhân bị ép làm việc và sống trong sợ hãi. Ny gặp những con người cùng cảnh ngộ cùng các trinh sát đang hoạt động bí mật; họ âm thầm tìm cơ hội trốn thoát và vạch ra đường dây phía sau. Giữa những hiểm nguy, bộ phim theo chân cuộc đấu tranh sinh tồn, tình thân và khát vọng tự do của những người bị lừa bởi lời hứa “việc nhẹ, lương cao”.'),
('MOV26_TAI', N'TÀI', N'Hành động, Tâm lý, Gia đình', N'Tiếng Việt', N'T16 (16+)', 101, 80000,
 N'Assets/Movies/Poster_Tai.png',
 N'Khởi chiếu 06/03/2026. Tài là người đàn ông miền Tây bị cuốn vào một biến cố khiến những lựa chọn của anh đặt cả gia đình trước nguy hiểm. Khi các mối quan hệ và bí mật trong quá khứ dần lộ diện, Tài phải đối mặt với ranh giới mong manh giữa đúng và sai, giữa mong muốn bảo vệ người thân và cái giá của con đường mình đang đi. Phim kết hợp những cuộc truy đuổi, xung đột căng thẳng với các khoảnh khắc hài hước và tình cảm gia đình, đồng thời đặt ra câu hỏi liệu lòng hiếu thảo có thể biện minh cho mọi quyết định hay không.'),
('MOV25_CVNG', N'CỤC VÀNG CỦA NGOẠI', N'Gia đình, Tâm lý', N'Tiếng Việt', N'T13 (13+)', 119, 80000,
 N'Assets/Movies/Poster_CucVangCuaNgoai.png',
 N'Khởi chiếu 17/10/2025. Trong một xóm nhỏ chan chứa tình người, bà Hậu trở thành chỗ dựa của đứa cháu ngoại sau khi con gái rời nhà. Cuộc sống còn nhiều thiếu thốn và những va chạm trong gia đình khiến bà phải gồng gánh không ít lo toan, nhưng tình thương bà dành cho cháu vẫn luôn nguyên vẹn. Qua những bữa cơm, lời nhắc nhở, lần đưa đón và sự đùm bọc của hàng xóm, bộ phim kể một câu chuyện gần gũi về ký ức tuổi thơ, tình bà cháu và cách các thế hệ học cách thấu hiểu, bao dung cho nhau.'),
('MOV25_LM8', N'LẬT MẶT 8: VÒNG TAY NẮNG', N'Gia đình, Tâm lý, Âm nhạc', N'Tiếng Việt', N'T13 (13+)', 135, 80000,
 N'Assets/Movies/Poster_LatMat8_VongTayNang.png',
 N'Khởi chiếu 30/04/2025; suất chiếu sớm từ 27/04/2025. Tâm cùng nhóm bạn trẻ ở miền Trung nuôi ước mơ trở thành nghệ sĩ và được biểu diễn trên sân khấu lớn. Cơ hội đến gần cũng là lúc Tâm va chạm với cha mình, ông Phước, người mong con tập trung học hành và có một tương lai ổn định. Từ góc nhìn của con cái, cha mẹ và ông bà, phim khắc họa những bất đồng trong gia đình, nỗi lo phía sau sự nghiêm khắc và nghị lực theo đuổi đam mê. Hành trình của nhóm bạn đặt ra câu hỏi làm sao để người trẻ được lựa chọn tương lai mà vẫn tìm thấy sự nâng đỡ từ gia đình.'),
('MOV25_DUOC', N'ĐIỀU ƯỚC CUỐI CÙNG', N'Hài, Gia đình, Tình bạn', N'Tiếng Việt', N'T16 (16+)', 114, 80000,
 N'Assets/Movies/Poster_DieuUocCuoiCung.png',
 N'Khởi chiếu 04/07/2025. Khi biết mình mắc bệnh ALS và thời gian không còn nhiều, Hoàng tâm sự với hai người bạn thân là Thy và Long về điều ước cuối cùng của mình. Hai người bạn quyết tâm giúp Hoàng thực hiện mong muốn ấy, mở ra chuyến đi đầy những tình huống dở khóc dở cười, hiểu lầm và thử thách. Đằng sau chất hài hước là câu chuyện về tình bạn lâu năm, sự quan tâm của gia đình và cách mỗi người đối diện với một ước muốn khó nói thành lời. Bộ phim đặt sự trưởng thành và tình yêu thương vào trung tâm của hành trình.'),
('MOV24_CTBL', N'CÔNG TỬ BẠC LIÊU', N'Tâm lý, Hài, Lịch sử', N'Tiếng Việt', N'T13 (13+)', 113, 80000,
 N'Assets/Movies/Poster_CongTuBacLieu.png',
 N'Khởi chiếu 06/12/2024. Lấy cảm hứng từ những giai thoại về cậu Ba Trần Trinh Huy ở Nam Bộ đầu thế kỷ XX, phim theo chân Ba Hơn, người con được nuông chiều của một gia đình giàu có, sau khi trở về từ Pháp. Lối sống phóng túng và những màn tiêu tiền gây xôn xao khiến anh thường xuyên va chạm với người cha nghiêm khắc, đồng thời buộc anh suy nghĩ về trách nhiệm với gia đình và con đường trưởng thành của mình. Những giai thoại như đốt tiền nấu chè, đời sống sân khấu và khát vọng khẳng định bản thân được kể lại trong bối cảnh Nam Kỳ xưa.'),
('MOV26_NHSNH', N'NGHỈ HÈ SỢ NGHỈ HƯU', N'Hài, Gia đình, Tâm lý', N'Tiếng Việt', N'T16 (16+)', 117, 80000,
 N'Assets/Movies/Poster_NghiHeSoNghiHuu.png',
 N'Khởi chiếu 21/08/2026. Trí Bình, một chàng trai Gen Z, về quê nghỉ hè cùng ông nội là ông Thời, cựu chiến binh sống khép kín và nghiêm nghị. Những biểu hiện kỳ lạ của ông cùng các lời đồn trong vùng khiến Trí Bình vừa tò mò vừa lo sợ, từ đó kéo cậu vào hành trình tìm hiểu chuyện gì đang xảy ra trong gia đình. Khi hai ông cháu dần đối diện với những bí mật và ký ức cũ, khoảng cách giữa lối sống hiện đại của người trẻ và những trải nghiệm của thế hệ trước cũng được thu hẹp. Phim đan xen tiếng cười, không khí bí ẩn và tình cảm gia đình, gợi nhắc về sự hy sinh và giá trị của hòa bình.');

IF EXISTS
(
    SELECT 1
    FROM @Movies AS source
    INNER JOIN dbo.Phim AS target ON target.MaPhim = source.MaPhim
    WHERE target.TenPhim <> source.TenPhim
)
    THROW 51060, N'Mã phim trong seed đã được dùng cho một phim khác. Hãy đổi mã MOV... trong script trước khi chạy.', 1;

UPDATE target
SET target.TenPhim = source.TenPhim,
    target.TheLoai = source.TheLoai,
    target.NgonNgu = source.NgonNgu,
    target.DoTuoi = source.DoTuoi,
    target.ThoiLuong = source.ThoiLuong,
    target.GiaPhim = source.GiaPhim,
    target.PosterPath = source.PosterPath,
    target.MoTa = source.MoTa,
    target.NgayBatDauChieu = NULL,
    target.NgayKetThucChieu = NULL
FROM dbo.Phim AS target
INNER JOIN @Movies AS source ON source.TenPhim = target.TenPhim;

INSERT INTO dbo.Phim
    (MaPhim, TenPhim, TheLoai, NgonNgu, DoTuoi, ThoiLuong, GiaPhim, PosterPath, MoTa, NgayBatDauChieu, NgayKetThucChieu)
SELECT source.MaPhim, source.TenPhim, source.TheLoai, source.NgonNgu, source.DoTuoi,
       source.ThoiLuong, source.GiaPhim, source.PosterPath, source.MoTa, NULL, NULL
FROM @Movies AS source
WHERE NOT EXISTS (SELECT 1 FROM dbo.Phim AS target WHERE target.TenPhim = source.TenPhim);

COMMIT TRANSACTION;
GO
