namespace HeThongRapChieuPhim.UIHelpers
{
    partial class ShowtimeOption
    {
        private void InitializeComponent()
        {
            selectButton = new Button();
            SuspendLayout();
            selectButton.Name = "selectButton";
            selectButton.Dock = DockStyle.Fill;
            selectButton.Text = "19:00";
            selectButton.FlatStyle = FlatStyle.Flat;
            selectButton.FlatAppearance.BorderColor = Color.FromArgb(205, 205, 192);
            selectButton.BackColor = Color.FromArgb(255, 254, 245);
            selectButton.Font = new Font("Segoe UI", 10F);
            selectButton.Cursor = Cursors.Hand;
            Controls.Add(selectButton);
            Name = "ShowtimeOption";
            Size = new Size(112, 48);
            Margin = new Padding(0, 0, 6, 6);
            ResumeLayout(false);
        }
        internal Button selectButton;
    }
}
