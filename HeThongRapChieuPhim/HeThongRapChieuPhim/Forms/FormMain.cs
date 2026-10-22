using System;
using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;
using HeThongRapChieuPhim.UserControls;

namespace HeThongRapChieuPhim.Forms
{
    public partial class FormMain : Form
    {
        // UserControls
        private ucDashboard? _ucDashboard;
        private ucBanHang? _ucBanHang;
        private ucHoaDon? _ucHoaDon;
        private ucTaiKhoanThuNgan? _ucTaiKhoanThuNgan;

        // Individual category & settings screens
        private ucPhim? _ucPhim;
        private ucPhongChieu? _ucPhongChieu;
        private ucNhanVien? _ucNhanVien;
        private ucDoAn? _ucDoAn;
        private ucCaiSuatChieu? _ucCaiSuatChieu;
        private ucCaiUuDai? _ucCaiUuDai;
        private ucCaiVoucher? _ucCaiVoucher;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object? sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignTime(this)) return;

            if (!string.IsNullOrEmpty(CurrentUser.HoTen))
            {
                lblStatusUser.Text = $"Người dùng: {CurrentUser.HoTen} ({AccessRoles.DisplayName(CurrentUser.VaiTro)})";
            }
            else
            {
                lblStatusUser.Text = "Người dùng: Chưa đăng nhập";
            }

            ApplyRolePermissions();
            timerClock.Start();
            UpdateClock();

            // Default screen based on role
            if (CurrentUser.IsSuperAdmin) ShowDashboard();
            else if (CurrentUser.IsFilmAdmin) ShowPhim();
            else if (CurrentUser.IsHumanResourcesAdmin) ShowNhanVien();
            else if (CurrentUser.IsFoodAndPromotionsAdmin) ShowDoAn();
            else if (CurrentUser.IsRevenueAdmin) ShowDashboard();
            else ShowBanHang();
        }

        private void ApplyRolePermissions()
        {
            bool isSuper = CurrentUser.IsSuperAdmin;

            // Phim, phòng chiếu, suất chiếu (Quản trị phim hoặc Admin tổng)
            menuPhim.Visible = isSuper || CurrentUser.IsFilmAdmin;
            menuPhongChieu.Visible = isSuper || CurrentUser.IsFilmAdmin;
            menuSuatChieu.Visible = isSuper || CurrentUser.IsFilmAdmin;

            // Bán vé, hóa đơn (Thu ngân, Doanh thu hoặc Admin tổng)
            menuBanHang.Visible = isSuper || CurrentUser.IsCashier;
            menuHoaDon.Visible = isSuper || CurrentUser.IsCashier || CurrentUser.IsRevenueAdmin;

            // Đồ ăn, combo, voucher (Quản trị đồ ăn & ưu đãi hoặc Admin tổng)
            menuDoAn.Visible = isSuper || CurrentUser.IsFoodAndPromotionsAdmin;
            menuCombo.Visible = isSuper || CurrentUser.IsFoodAndPromotionsAdmin;
            menuVoucher.Visible = isSuper || CurrentUser.IsFoodAndPromotionsAdmin;

            // Nhân sự & tài khoản (Admin nhân sự hoặc Admin tổng)
            menuNhanVien.Visible = isSuper || CurrentUser.IsHumanResourcesAdmin;
            menuTaiKhoanThuNgan.Visible = isSuper || CurrentUser.IsHumanResourcesAdmin;

            // Báo cáo doanh thu (Admin doanh thu hoặc Admin tổng)
            menuDoanhThu.Visible = isSuper || CurrentUser.IsRevenueAdmin;
        }

        private void TimerClock_Tick(object? sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblStatusClock.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void SwitchView(UserControl uc, string title)
        {
            lblStatusView.Text = $"Chức năng: {title}";
            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            uc.AutoScroll = true;
            uc.AutoScrollMinSize = new Size(1100, 720);
            pnlContent.Controls.Add(uc);
            pnlContent.ResumeLayout();
        }

        public void ShowDashboard()
        {
            if (!RequireRole(CurrentUser.IsRevenueAdmin || CurrentUser.IsSuperAdmin, "Admin Doanh thu hoặc Admin tổng")) return;
            _ucDashboard ??= new ucDashboard();
            SwitchView(_ucDashboard, "Báo cáo doanh thu tổng quan");
            _ucDashboard.LoadDashboardData();
        }

        public void ShowPhim()
        {
            if (!RequireRole(CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin, "Admin Phim & Rạp hoặc Admin tổng")) return;
            _ucPhim ??= new ucPhim();
            SwitchView(_ucPhim, "Danh mục phim chiếu rạp");
        }

        public void ShowPhongChieu()
        {
            if (!RequireRole(CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin, "Admin Phim & Rạp hoặc Admin tổng")) return;
            _ucPhongChieu ??= new ucPhongChieu();
            SwitchView(_ucPhongChieu, "Danh mục phòng chiếu");
        }

        public void ShowNhanVien()
        {
            if (!RequireRole(CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin, "Admin Nhân sự hoặc Admin tổng")) return;
            _ucNhanVien ??= new ucNhanVien();
            SwitchView(_ucNhanVien, "Danh mục nhân viên");
        }

        public void ShowDoAn()
        {
            if (!RequireRole(CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin, "Admin Đồ ăn & Ưu đãi hoặc Admin tổng")) return;
            _ucDoAn ??= new ucDoAn();
            SwitchView(_ucDoAn, "Danh mục đồ ăn & nước uống");
        }

        public void ShowSuatChieu()
        {
            if (!RequireRole(CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin, "Admin Phim & Rạp hoặc Admin tổng")) return;
            _ucCaiSuatChieu ??= new ucCaiSuatChieu();
            SwitchView(_ucCaiSuatChieu, "Cài đặt lịch chiếu & suất chiếu");
        }

        public void ShowCombo()
        {
            if (!RequireRole(CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin, "Admin Đồ ăn & Ưu đãi hoặc Admin tổng")) return;
            _ucCaiUuDai ??= new ucCaiUuDai();
            SwitchView(_ucCaiUuDai, "Cài đặt ưu đãi combo");
        }

        public void ShowVoucher()
        {
            if (!RequireRole(CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin, "Admin Đồ ăn & Ưu đãi hoặc Admin tổng")) return;
            _ucCaiVoucher ??= new ucCaiVoucher();
            SwitchView(_ucCaiVoucher, "Cài đặt Voucher");
        }

        public void ShowBanHang()
        {
            if (!CurrentUser.IsCashier && !CurrentUser.IsSuperAdmin) return;
            _ucBanHang ??= new ucBanHang();
            SwitchView(_ucBanHang, "Bán vé xem phim & Bắp nước");
            _ucBanHang.LoadData();
        }

        public void ShowHoaDon()
        {
            if (!CurrentUser.IsCashier && !CurrentUser.IsRevenueAdmin && !CurrentUser.IsSuperAdmin) return;
            _ucHoaDon ??= new ucHoaDon();
            SwitchView(_ucHoaDon, "Quản lý & tra cứu hóa đơn");
            _ucHoaDon.LoadHoaDon();
        }

        public void ShowTaiKhoanThuNgan()
        {
            if (!RequireRole(CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin, "Admin Nhân sự hoặc Admin tổng")) return;
            _ucTaiKhoanThuNgan ??= new ucTaiKhoanThuNgan();
            SwitchView(_ucTaiKhoanThuNgan, "Quản lý tài khoản thu ngân");
        }

        public void ShowDanhMuc(int tabIndex = 0)
        {
            switch (tabIndex)
            {
                case 0: ShowNhanVien(); break;
                case 1: ShowPhim(); break;
                case 2: ShowDoAn(); break;
                case 3: ShowPhongChieu(); break;
                default: ShowPhim(); break;
            }
        }

        public void ShowCaiDat(int tabIndex = 0)
        {
            switch (tabIndex)
            {
                case 0: ShowSuatChieu(); break;
                case 1: ShowCombo(); break;
                case 2: ShowVoucher(); break;
                default: ShowSuatChieu(); break;
            }
        }

        private bool RequireRole(bool allowed, string requiredRole)
        {
            if (allowed) return true;
            MessageBox.Show($"Chức năng này chỉ dành cho {requiredRole}.", "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void MenuDoiMatKhau_Click(object? sender, EventArgs e)
        {
            using var dlg = new FormDoiMatKhau();
            dlg.ShowDialog(this);
        }

        private void MenuDangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CurrentUser.Clear();
                Close();
            }
        }

        private void MenuThoat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MenuPhim_Click(object? sender, EventArgs e) => ShowPhim();
        private void MenuPhongChieu_Click(object? sender, EventArgs e) => ShowPhongChieu();
        private void MenuSuatChieu_Click(object? sender, EventArgs e) => ShowSuatChieu();
        private void MenuBanHang_Click(object? sender, EventArgs e) => ShowBanHang();
        private void MenuHoaDon_Click(object? sender, EventArgs e) => ShowHoaDon();
        private void MenuDoAn_Click(object? sender, EventArgs e) => ShowDoAn();
        private void MenuCombo_Click(object? sender, EventArgs e) => ShowCombo();
        private void MenuVoucher_Click(object? sender, EventArgs e) => ShowVoucher();
        private void MenuNhanVien_Click(object? sender, EventArgs e) => ShowNhanVien();
        private void MenuTaiKhoanThuNgan_Click(object? sender, EventArgs e) => ShowTaiKhoanThuNgan();
        private void MenuDoanhThu_Click(object? sender, EventArgs e) => ShowDashboard();
    }
}
