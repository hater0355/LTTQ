using System;
using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucDanhMuc : UserControl
    {
        private Button? _activeTab = null;

        private ucNhanVien? _ucNhanVien;
        private ucPhim? _ucPhim;
        private ucDoAn? _ucDoAn;
        private ucPhongChieu? _ucPhongChieu;

        public ucDanhMuc() : this(0)
        {
        }

        public ucDanhMuc(int initialTab = 0)
        {
            InitializeComponent();
            btnNV.Visible = CurrentUser.IsHumanResourcesAdmin || CurrentUser.IsSuperAdmin;
            btnPhim.Visible = CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin;
            btnDoAn.Visible = CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin;
            btnPhong.Visible = CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin;
            SelectTab(initialTab);
        }

        private void BtnNV_Click(object? sender, EventArgs e) => SelectTab(0);
        private void BtnPhim_Click(object? sender, EventArgs e) => SelectTab(1);
        private void BtnDoAn_Click(object? sender, EventArgs e) => SelectTab(2);
        private void BtnPhong_Click(object? sender, EventArgs e) => SelectTab(3);

        public void SelectTab(int index)
        {
            Button[] tabButtons = new[] { btnNV, btnPhim, btnDoAn, btnPhong };
            if (index < 0 || index >= tabButtons.Length || !tabButtons[index].Visible)
                index = Array.FindIndex(tabButtons, button => button.Visible);
            if (index < 0) return;
            Button targetBtn = tabButtons[index];

            if (_activeTab != null)
            {
                _activeTab.BackColor = Color.Transparent;
                _activeTab.ForeColor = Color.FromArgb(60, 95, 75);
            }

            _activeTab = targetBtn;
            _activeTab.BackColor = Color.White;
            _activeTab.ForeColor = ThemeHelper.PrimaryDark;

            pnlBody.SuspendLayout();
            pnlBody.Controls.Clear();

            UserControl uc = index switch
            {
                0 => (_ucNhanVien ??= new ucNhanVien()),
                1 => (_ucPhim ??= new ucPhim()),
                2 => (_ucDoAn ??= new ucDoAn()),
                3 => (_ucPhongChieu ??= new ucPhongChieu()),
                _ => (_ucNhanVien ??= new ucNhanVien())
            };

            uc.Dock = DockStyle.Fill;
            pnlBody.Controls.Add(uc);
            pnlBody.ResumeLayout();
        }
    }
}
