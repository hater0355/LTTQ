# HƯỚNG DẪN SỬ DỤNG & CÀI ĐẶT HỆ THỐNG RẠP CHIẾU PHIM NHH CINEMA
**Bài tập lớn môn: Lập trình trực quan (C# Windows Forms App)**

---

## 1. TỔNG QUAN HỆ THỐNG
- **Tên hệ thống:** Quản lý rạp chiếu phim **NHH Cinema**
- **Nền tảng:** C# Windows Forms App (.NET, Visual Studio)
- **Cơ sở dữ liệu:** Microsoft SQL Server (`NHHCinemaDB`)
- **Hai đối tượng sử dụng:**
  1. **Admin:** Dashboard, danh mục, cài đặt và quản lý tài khoản thu ngân.
  2. **Thu ngân:** Chỉ truy cập bán vé/bắp nước và tra cứu hóa đơn.

---

## 2. TÀI KHOẢN ĐĂNG NHẬP
- Trên database mới, script tạo sẵn một tài khoản Admin mẫu để đăng nhập lần đầu:

| Vai trò | Tên đăng nhập | Mật khẩu | Ghi chú |
| :--- | :--- | :--- | :--- |
| **👑 Admin tổng** | `admin` | `admin` | Toàn quyền tất cả chức năng; tự động băm bảo mật sau lần đăng nhập đầu tiên |
| **👑 Admin tổng** | `admintong` | `admin123` | Tài khoản Admin tổng dự phòng |

*(Màn hình Đăng nhập có nút bấm tiện lợi **"👑 Tự điền: admin / admin"** để tự động điền tài khoản).*
- Sau khi đăng nhập Admin, mở **Tài Khoản Thu Ngân** để tạo tài khoản độc lập hoặc đặt lại mật khẩu. Mật khẩu phải có ít nhất 6 ký tự.
- Hồ sơ nhân viên được quản lý riêng trong **Danh Mục > Nhân Viên** và không tự tạo tài khoản đăng nhập.

---

## 3. CƠ SỞ DỮ LIỆU SQL SERVER
- Script SQL hoàn chỉnh được đặt tại: `Database/NHHCinema.sql`.
- Đã được khởi tạo và chạy thành công trên máy tính của bạn với database `NHHCinemaDB`.
- Nếu cần chạy lại trên máy khác:
  1. Mở **SQL Server Management Studio (SSMS)**.
  2. Mở file `Database\NHHCinema.sql`.
  3. Chạy script khởi tạo một lần trên database mới/trống. Với database đang có dữ liệu, chạy `Database/Migrations/20261014_RolesAndPasswordStorage.sql`; không chạy lại script khởi tạo vì script đó tạo lại các bảng.
- Chuỗi kết nối nằm tại class `Classes/DataProcesser.cs`:
  ```csharp
  Server=.;Database=NHHCinemaDB;Integrated Security=True;TrustServerCertificate=True;
  ```

---

## 4. CHI TIẾT CÁC TÍNH NĂNG VÀ NGHIỆP VỤ

### 4.1. Giao diện Admin & Thu ngân
- **Khi đăng nhập tài khoản Admin (`admin` / `admin` trên database mới):**
  - Mở trực tiếp vào **📊 Trang Chính (Dashboard)** với các biểu đồ doanh thu.
  - Trên màn hình có cả **Thanh Tab nhanh phía trên** và **Menu Sidebar bên trái** được phân nhóm trực quan:
    1. **📊 TỔNG QUAN DOANH THU:** Mở Dashboard xem doanh thu tổng, biểu đồ các tháng, biểu đồ phim.
    2. **📁 QUẢN LÝ DANH MỤC:** Gồm 4 trang con (Nhân viên, Phim, Đồ ăn, Phòng chiếu) có thể chuyển nhanh bằng Tab phía trên.
    3. **⚙️ CÀI ĐẶT HỆ THỐNG:** Gồm 3 trang con (Cài suất chiếu, Cài ưu đãi combo, Cài đặt voucher).

    5. **🔐 TÀI KHOẢN THU NGÂN:** Tạo tài khoản và đặt lại mật khẩu.
- **Khi đăng nhập bằng tài khoản Thu ngân do Admin tạo:**
  - Hệ thống tự động chuyển thẳng vào phân hệ **💼 Kinh doanh: Bán hàng** và **Hóa đơn**. Không hiển thị Dashboard, Danh mục hay Cài đặt của Quản lý.

### 4.2. Trang chính (Dashboard - Quản lý)
- **Thẻ KPI tổng quan:** Tổng doanh thu (VNĐ), Doanh thu tháng này, Tổng số vé đã bán, Số lượng phim đang chiếu.
- **Biểu đồ doanh thu các tháng:** Biểu đồ cột đồ họa mượt mà, hiển thị số tiền từng tháng.
- **Biểu đồ doanh thu theo phim:** Biểu đồ tròn/doughnut thể hiện tỷ trọng doanh thu giữa các bộ phim.
- **Bảng xếp hạng phim:** Danh sách phim ăn khách sắp xếp theo doanh thu và số vé đã bán.

### 4.3. Quản lý Danh mục (4 trang con)
- **Nhân viên:** Quản lý Mã NV, Họ Tên, Ngày sinh, Số ĐT, CCCD, Chức vụ. Tài khoản đăng nhập được quản lý độc lập.
- **Phim & Poster:** Mã phim, Tên phim, Thể loại, Ngôn ngữ, Độ tuổi (P, K, T13, T16, T18), Thời lượng, Giá vé gốc, Chọn ảnh poster lưu vào thư mục `Assets/`.
- **Đồ ăn (F&B):** Mã đồ ăn, Tên món, Phân loại, Đơn giá.
- **Phòng chiếu:** Mã phòng, Tên phòng, Số hàng ghế (A-Z), Số ghế mỗi hàng (1-N), xem trước sơ đồ ghế.

### 4.4. Cài đặt Hệ thống (3 trang con)
- **Cài suất chiếu:** Gắn phòng chiếu cố định vào phim, ngày chiếu và giờ chiếu. Khi khách chọn phim bên màn hình Bán hàng sẽ tự động hiện giờ chiếu & phòng tương ứng.
- **Cài ưu đãi combo:** Mã combo, Tên combo, Số lượng ghế bắt buộc, % Giảm giá vé phim. Bắt buộc khách chọn đúng số lượng ghế và giảm giá chỉ tính trên giá vé phim (không giảm đồ ăn).
- **Cài đặt voucher:** Quản lý mã khuyến mãi/voucher rạp chiếu phim với đầy đủ tiêu chí:
  - **Mã voucher:** Hỗ trợ nhập mã tùy ý hoặc nút tạo mã tự động ngẫu nhiên (VD: `VC7482`).
  - **Tên voucher:** Tên chiến dịch ưu đãi (VD: *Tri ân khách hàng thân thiết*).
  - **Hạn sử dụng:** Chọn ngày hết hạn qua DateTimePicker trực quan.
  - **Hình thức giảm giá:** 
    + *Giảm phần trăm (%):* Tự động giới hạn giá trị từ 1% đến 100%.
    + *Giảm số tiền (VNĐ):* Giảm tiền trực tiếp (VD: 20.000, 50.000 VNĐ).
    + *Giảm số vé:* Tặng/giảm trực tiếp theo số vé (VD: 1 vé, 2 vé).
  - **Mô tả voucher:** Ghi chú điều kiện áp dụng hoặc chương trình khuyến mãi.
  - **Trạng thái & Tìm kiếm:** Tự động nhận diện trạng thái (🟢 Còn hiệu lực, 🔴 Đã khóa, ⌛ Hết hạn), bộ lọc tìm kiếm nhanh theo mã hoặc tên voucher.

### 4.5. Phân hệ Kinh doanh: Bán hàng (Bán vé & Bắp nước)
- **Góc trên màn hình:** Hiển thị rõ tên và mã nhân viên đang trực ca bán hàng.
- **Chọn phim:** Khi chọn phim, lập tức hiển thị Poster to rõ nét, thể loại, độ tuổi và giá vé.
- **Chọn suất chiếu:** Lập tức nạp phòng chiếu và sơ đồ ghế tương ứng.
- **Sơ đồ chọn ghế tương tác (Interactive Seat Map):**
  - Ghế đã được người khác mua: **Màu đỏ**, vô hiệu hóa (không thể click).
  - Ghế trống: **Màu xanh xám**, click vào chuyển sang **Màu xanh lá** (Đang chọn).
  - Ràng buộc combo: Nếu chọn combo yêu cầu N ghế, hệ thống nhắc nhở và quản lý chặt chẽ số lượng ghế.
- **Chọn đồ ăn (F&B):** Tăng giảm số lượng bắp nước linh hoạt.
- **Áp dụng Mã Voucher Khuyến Mãi (Mới):**
  - Ô nhập mã voucher trực tiếp tại khung thanh toán: nhập mã và nhấn Enter hoặc bấm **"Áp dụng"**.
  - **Kiểm tra tính hợp lệ:**
    + *Nhập sai mã:* Báo lỗi mã không tồn tại trong hệ thống, yêu cầu kiểm tra và nhập lại.
    + *Nhập mã đã dùng / đã khóa:* Thông báo voucher này đã được sử dụng hoặc đang bị khóa, không cho phép dùng lại.
    + *Nhập mã hết hạn:* Thông báo ngày hết hạn cụ thể, yêu cầu nhập mã khác.
    + *Nhập đúng mã:* Hệ thống tự động tính mức giảm theo đúng loại (giảm % vé, giảm số tiền VNĐ hoặc giảm số vé miễn phí) và cập nhật ngay vào tổng thanh toán.
  - **Tự động chuyển trạng thái khi thanh toán:** Khi bấm **"Xác nhận bán vé"** thành công, trạng thái của voucher trong CSDL lập tức được đổi sang **`Đã sử dụng / Đã khóa` (TrangThai = 0)**, ngăn chặn hoàn toàn việc tái sử dụng.
- **Tính tiền tự động:**
  - `Tiền vé gốc = Số ghế * Giá vé phim`
  - `Tiền giảm combo = Tiền vé gốc * % giảm combo`
  - `Tiền giảm voucher = Tính theo hình thức ưu đãi của voucher`
  - `Tiền đồ ăn = Tổng các món F&B`
  - `Tổng thanh toán = (Tiền vé gốc + Tiền đồ ăn) - (Tiền giảm combo + Tiền giảm voucher)`
- **Phương thức thanh toán:**
  - Tiền mặt.
  - Chuyển khoản: Tự động sinh mã **VietQR Code chuẩn** theo đúng tổng tiền thực tế sau khi đã giảm trừ voucher.
- **Xác nhận & In hóa đơn:**
  - Bấm xác nhận -> Lưu hóa đơn vào SQL Server (các ghế vừa mua lập tức chuyển sang trạng thái đã bán, voucher chuyển sang trạng thái đã sử dụng).
  - Hiện cửa sổ Hóa đơn xem trước và hỗ trợ **In hóa đơn**.

### 4.9. Phân hệ Kinh doanh: Quản lý Hóa đơn
- Hiển thị danh sách hóa đơn đã bán kèm đầy đủ thông tin: Mã HĐ, Ngày giờ xuất, Mã NV bán, Tên NV bán, Tên phim, Phòng chiếu, Suất chiếu, Ghế ngồi, Tổng tiền, Hình thức thanh toán.
- Bộ lọc thông minh theo từ khóa và theo khoảng thời gian (Từ ngày - Đến ngày).
- Nút **"Xem / In Lại Hóa Đơn"** và nút **"Hủy Hóa Đơn"**.

---

## 5. CẤU TRÚC THƯ MỤC PROJECT
```
HeThongRapChieuPhim/
├── Assets/                        # Ảnh logo, poster phim
│   ├── logo.jpg
│   ├── P01.jpg
│   └── P03.jpg
├── Database/
│   ├── NHHCinema.sql              # Script khởi tạo database mới
│   └── Migrations/                # Script nâng cấp database đang dùng
├── Data/
│   └── CurrentUser.cs             # Quản lý phiên đăng nhập
├── Classes/
│   ├── DataProcesser.cs           # Truy cập SQL Server bằng ADO.NET
│   └── Function.cs                # Đăng nhập, phân quyền, nghiệp vụ dùng chung

├── Forms/
│   ├── FormLogin.cs               # Màn hình Đăng nhập
│   ├── FormMain.cs                # Màn hình Chính (Menu điều hướng theo quyền)
│   ├── FormInHoaDon.cs            # In & xem trước hóa đơn
│   └── FormQRPayment.cs           # Cửa sổ thanh toán VietQR
├── UserControls/
│   ├── ucDashboard.cs             # Trang chính & Biểu đồ doanh thu
│   ├── ucNhanVien.cs              # Quản lý hồ sơ nhân viên
│   ├── ucTaiKhoanThuNgan.cs       # Tạo tài khoản thu ngân/đặt lại mật khẩu
│   ├── ucPhim.cs                  # Quản lý phim & Poster
│   ├── ucDoAn.cs                  # Quản lý thực đơn F&B
│   ├── ucPhongChieu.cs            # Quản lý phòng chiếu & sơ đồ ghế
│   ├── ucCaiSuatChieu.cs          # Cài đặt suất chiếu
│   ├── ucCaiUuDai.cs              # Cài đặt ưu đãi combo
│   ├── ucBanHang.cs               # Bán vé, sơ đồ ghế, QR, in hóa đơn
│   └── ucHoaDon.cs                # Lịch sử hóa đơn
├── UIHelpers/
│   ├── QrCodeHelper.cs            # Tạo mã QR Code
│   └── ThemeHelper.cs             # Bảng màu Cinema, style DataGridView
└── Program.cs                     # Điểm khởi chạy ứng dụng
```
