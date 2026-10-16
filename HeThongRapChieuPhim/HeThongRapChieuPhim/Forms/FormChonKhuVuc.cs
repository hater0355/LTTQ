using HeThongRapChieuPhim.Data;

namespace HeThongRapChieuPhim.Forms
{
    public sealed partial class FormChonKhuVuc : Form
    {
        private readonly string _imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Beta_Cinemas_Home.png");

        public FormChonKhuVuc()
        {
            InitializeComponent();
            btnAdmin.Tag = AccessRoles.SuperAdmin;
            btnMovies.Tag = AccessRoles.FilmAdmin;
            btnPeople.Tag = AccessRoles.HumanResourcesAdmin;
            btnFood.Tag = AccessRoles.FoodAndPromotionsAdmin;
            btnRevenue.Tag = AccessRoles.RevenueAdmin;
            btnCashier.Tag = AccessRoles.Cashier;

            btnAdmin.Click += RoleSelected;
            btnMovies.Click += RoleSelected;
            btnPeople.Click += RoleSelected;
            btnFood.Click += RoleSelected;
            btnRevenue.Click += RoleSelected;
            btnCashier.Click += RoleSelected;
            btnExit.Click += (_, _) => Close();

            if (File.Exists(_imagePath))
            {
                using var stream = new FileStream(_imagePath, FileMode.Open, FileAccess.Read);
                using var source = Image.FromStream(stream);
                picHero.Image = new Bitmap(source);
            }
        }

        private void RoleSelected(object? sender, EventArgs e)
        {
            string role = sender is Control control
                ? Convert.ToString(control.Tag) ?? string.Empty
                : string.Empty;
            if (string.IsNullOrWhiteSpace(role)) return;

            using var login = new FormLogin(role);
            if (login.ShowDialog(this) != DialogResult.OK) return;

            Hide();
            try
            {
                using var main = new FormMain();
                main.ShowDialog(this);
            }
            finally
            {
                CurrentUser.Clear();
                Show();
                Activate();
            }
        }
    }
}
