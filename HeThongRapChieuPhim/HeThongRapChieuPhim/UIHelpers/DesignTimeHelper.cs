using System.ComponentModel;
using System.Windows.Forms;

namespace HeThongRapChieuPhim.UIHelpers;

internal static class DesignTimeHelper
{
    public static bool IsDesignTime(Control control) =>
        LicenseManager.UsageMode == LicenseUsageMode.Designtime || control.Site?.DesignMode == true;
}
