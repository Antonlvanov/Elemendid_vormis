using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public class Pildi_vaatamine : Form
    {
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button backgroundButton;
        private Button clearButton;
        private Button showButton;
        private OpenFileDialog openFileDialog1;
        private ColorDialog colorDialog1;
        private Button cropButton;

        private Point startPoint;
        private Rectangle cropRectangle;

        public Pildi_vaatamine()
        {
            this.Text = "Pildi vaatamine";
            this.Width = 800;
            this.Height = 600;

            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            openFileDialog1 = new OpenFileDialog();
            colorDialog1 = new ColorDialog();

            backgroundButton = CreateButton("Set the background color", backgroundButton_Click);
            clearButton = CreateButton("Clear the picture", clearButton_Click);
            showButton = CreateButton("Show a picture", showButton_Click);
            cropButton = CreateButton("Crop Image", CropButton_Click);

            SetParams();
            SetControls();
        }

        private Button CreateButton(string text, EventHandler onClick)
        {
            var button = new Button
            {
                Text = text,
                AutoSize = true
            };
            button.Click += onClick;
            return button;
        }

        private void SetParams()
        {
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));

            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;

            checkBox1.Text = "Stretch";
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        }

        private void SetControls()
        {
            flowLayoutPanel1.Controls.Add(showButton);
            flowLayoutPanel1.Controls.Add(clearButton);
            flowLayoutPanel1.Controls.Add(backgroundButton);
            flowLayoutPanel1.Controls.Add(cropButton);

            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.SetColumnSpan(pictureBox1, 2);
            tableLayoutPanel1.Controls.Add(checkBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 1);

            Controls.Add(tableLayoutPanel1);
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                cropRectangle = new Rectangle(startPoint, new Size(0, 0));
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && pictureBox1.Image != null)
            {
                Point currentPoint = e.Location;
                cropRectangle = new Rectangle(Math.Min(startPoint.X, currentPoint.X),
                                              Math.Min(startPoint.Y, currentPoint.Y),
                                              Math.Abs(startPoint.X - currentPoint.X),
                                              Math.Abs(startPoint.Y - currentPoint.Y));

                pictureBox1.Invalidate();
                pictureBox1.Update();
                pictureBox1.Paint += PictureBox1_Paint;
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (cropRectangle.Width > 0 && cropRectangle.Height > 0)
            {
                CropImage(cropRectangle);
                ResetCrop();
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (cropRectangle != null && cropRectangle.Width > 0 && cropRectangle.Height > 0)
            {
                e.Graphics.DrawRectangle(Pens.Red, cropRectangle);
            }
        }

        private void CropImage(Rectangle cropArea)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap oldImage = (Bitmap)pictureBox1.Image;
                Bitmap newImage = oldImage.Clone(cropArea, oldImage.PixelFormat);
                oldImage.Dispose();
                pictureBox1.Image = newImage;
            }
        }

        private void CropButton_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.Paint += PictureBox1_Paint;
        }

        private void ResetCrop()
        {
            pictureBox1.MouseDown -= PictureBox1_MouseDown;
            pictureBox1.MouseMove -= PictureBox1_MouseMove;
            pictureBox1.MouseUp -= PictureBox1_MouseUp;
            pictureBox1.Paint -= PictureBox1_Paint;

            cropRectangle = Rectangle.Empty;
            pictureBox1.Invalidate(); 
        }

        private void LoadImageFromUrl(string url)
        {
            try
            {
                pictureBox1.Load(url);
            }
            catch
            {
                MessageBox.Show("Ошибка загрузки изображения с URL.");
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png;*.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog1.Title = "Select an Image File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }
    }
}
