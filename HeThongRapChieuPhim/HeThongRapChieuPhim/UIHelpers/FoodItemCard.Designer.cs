namespace HeThongRapChieuPhim.UIHelpers
{
    partial class FoodItemCard
    {
        private void InitializeComponent()
        {
            poster = new PictureBox();
            poster.Name = "poster";
            nameLabel = new Label();
            nameLabel.Name = "nameLabel";
            descriptionLabel = new Label();
            descriptionLabel.Name = "descriptionLabel";
            _priceLabel = new Label();
            _priceLabel.Name = "_priceLabel";
            _quantityLabel = new Label();
            _quantityLabel.Name = "_quantityLabel";
            _removeButton = new Button();
            _removeButton.Name = "_removeButton";
            _addButton = new Button();
            _addButton.Name = "_addButton";
            SuspendLayout();
            Size = new Size(296, 116);
            MinimumSize = new Size(296, 116);
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(2, 2, 2, 8);
            poster.Location = new Point(7, 8);
            poster.Size = new Size(78, 78);
            this.Controls.Add(poster);
            nameLabel.Location = new Point(94, 7);
            nameLabel.Size = new Size(190, 22);
            nameLabel.Text = "Tên món";
            this.Controls.Add(nameLabel);
            descriptionLabel.Location = new Point(94, 29);
            descriptionLabel.Size = new Size(190, 20);
            descriptionLabel.Text = "Mô tả món";
            this.Controls.Add(descriptionLabel);
            _priceLabel.Location = new Point(94, 51);
            _priceLabel.Size = new Size(195, 22);
            _priceLabel.Text = "0 đ";
            this.Controls.Add(_priceLabel);
            _quantityLabel.Location = new Point(180, 78);
            _quantityLabel.Size = new Size(106, 30);
            _quantityLabel.Text = "Đã chọn: 0";
            this.Controls.Add(_quantityLabel);
            _removeButton.Location = new Point(94, 78);
            _removeButton.Size = new Size(36, 30);
            _removeButton.Text = "−";
            this.Controls.Add(_removeButton);
            _addButton.Location = new Point(136, 78);
            _addButton.Size = new Size(36, 30);
            _addButton.Text = "+";
            this.Controls.Add(_addButton);
            poster.SizeMode = PictureBoxSizeMode.Zoom;
            nameLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            nameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _priceLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _quantityLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _quantityLabel.TextAlign = ContentAlignment.MiddleCenter;
            _removeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _addButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _removeButton.FlatStyle = FlatStyle.Flat;
            _removeButton.FlatAppearance.BorderSize = 0;
            _removeButton.BackColor = Color.FromArgb(220, 53, 69);
            _removeButton.ForeColor = Color.White;
            _removeButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _removeButton.Cursor = Cursors.Hand;
            _removeButton.UseVisualStyleBackColor = false;
            _removeButton.AccessibleName = "Giảm số lượng";
            _addButton.FlatStyle = FlatStyle.Flat;
            _addButton.FlatAppearance.BorderSize = 0;
            _addButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _addButton.Cursor = Cursors.Hand;
            _addButton.UseVisualStyleBackColor = false;
            _addButton.AccessibleName = "Tăng số lượng";
            _addButton.BackColor = Color.FromArgb(16, 163, 74);
            _addButton.ForeColor = Color.White;

            Name = "FoodItemCard";
            ResumeLayout(false);
            PerformLayout();
        }
        internal PictureBox poster;
        internal Label nameLabel;
        internal Label descriptionLabel;
        internal Label _priceLabel;
        internal Label _quantityLabel;
        internal Button _removeButton;
        internal Button _addButton;

    }
}
