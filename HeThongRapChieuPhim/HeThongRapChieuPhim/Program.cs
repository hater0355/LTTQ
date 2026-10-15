using System.Windows.Forms;

namespace HeThongRapChieuPhim
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Forms.FormChonKhuVuc());
        }
    }
}
