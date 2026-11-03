using HeThongRapChieuPhim.UserControls;
namespace HeThongRapChieuPhim.Forms;
public partial class FormBooking : Form
{
    [System.ComponentModel.Category("Layout")]
    [System.ComponentModel.DefaultValue(typeof(Size), "600, 710")]
    public Size PaymentClientSize { get; set; } = new(600, 710);
    [System.ComponentModel.Category("Layout")]
    [System.ComponentModel.DefaultValue(typeof(Size), "1050, 710")]
    public Size SelectionClientSize { get; set; } = new(1050, 710);
    [System.ComponentModel.Category("Layout")]
    [System.ComponentModel.DefaultValue(typeof(Size), "600, 710")]
    public Size FoodClientSize { get; set; } = new(600, 710);
    public ucBanHang Booking => bookingControl;
    public FormBooking()
    {
        InitializeComponent();
        bookingControl.BookingCancelled += (_, _) => Close();
        bookingControl.BookingCompleted += (_, _) => { DialogResult = DialogResult.OK; Close(); };
        bookingControl.BookingStepChanged += (_, _) => ResizeForStep();
    }

    private void ResizeForStep()
    {
        var center = new Point(Left + Width / 2, Top + Height / 2);
                ClientSize = bookingControl.CurrentBookingStep switch
        {
            2 => FoodClientSize,
            3 => PaymentClientSize,
            _ => SelectionClientSize
        };
        var area = Screen.FromControl(this).WorkingArea;
        Location = new Point(Math.Clamp(center.X - Width / 2, area.Left, Math.Max(area.Left, area.Right - Width)),
            Math.Clamp(center.Y - Height / 2, area.Top, Math.Max(area.Top, area.Bottom - Height)));
    }
}
