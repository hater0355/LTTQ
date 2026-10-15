using System;
using System.Drawing;
using System.Windows.Forms;
using HeThongRapChieuPhim.Data;
using HeThongRapChieuPhim.UIHelpers;

namespace HeThongRapChieuPhim.UserControls
{
    public partial class ucCaiDat : UserControl
    {
        private Button? _activeTab = null;
        private Button[] _tabButtons = Array.Empty<Button>();

        private ucCaiSuatChieu? _ucCaiSuatChieu;
        private ucCaiUuDai? _ucCaiUuDai;
        private ucCaiVoucher? _ucCaiVoucher;

        public ucCaiDat() : this(0)
        {
        }

        public ucCaiDat(int initialTab)
        {
            InitializeComponent();
            _tabButtons = new[] { btnSuatChieu, btnCombo, btnVoucher };
            btnSuatChieu.Visible = CurrentUser.IsFilmAdmin || CurrentUser.IsSuperAdmin;
            btnCombo.Visible = CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin;
            btnVoucher.Visible = CurrentUser.IsFoodAndPromotionsAdmin || CurrentUser.IsSuperAdmin;
            SelectTab(initialTab);
        }

        private void BtnSuatChieu_Click(object? sender, EventArgs e)
        {
            SelectTab(0);
        }

        private void BtnCombo_Click(object? sender, EventArgs e)
        {
            SelectTab(1);
        }

        private void BtnVoucher_Click(object? sender, EventArgs e)
        {
            SelectTab(2);
        }

        public void SelectTab(int index)
        {
            if (_tabButtons == null || _tabButtons.Length == 0) return;
            if (index < 0 || index >= _tabButtons.Length || !_tabButtons[index].Visible)
                index = Array.FindIndex(_tabButtons, button => button.Visible);
            if (index < 0) return;

            var targetBtn = _tabButtons[index];
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

            UserControl uc;
            if (index == 2)
            {
                if (_ucCaiVoucher == null)
                    _ucCaiVoucher = new ucCaiVoucher();
                else
                    _ucCaiVoucher.RefreshData();
                uc = _ucCaiVoucher;
            }
            else if (index == 1)
            {
                if (_ucCaiUuDai == null)
                    _ucCaiUuDai = new ucCaiUuDai();
                else
                    _ucCaiUuDai.RefreshData();
                uc = _ucCaiUuDai;
            }
            else
            {
                if (_ucCaiSuatChieu == null)
                    _ucCaiSuatChieu = new ucCaiSuatChieu();
                else
                    _ucCaiSuatChieu.RefreshData();
                uc = _ucCaiSuatChieu;
            }

            uc.Dock = DockStyle.Fill;
            pnlBody.Controls.Add(uc);
            pnlBody.ResumeLayout();
        }
    }
}
