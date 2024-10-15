using Microsoft.VisualBasic;
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
        private Button loadImageButton;
        private Button saveImageButton;

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

            backgroundButton = CreateButton("Background", backgroundButton_Click);
            clearButton = CreateButton("Clear", clearButton_Click);
            showButton = CreateButton("Open", showButton_Click);
            cropButton = CreateButton("Cut", CropButton_Click);
            loadImageButton = CreateButton("Open Link", LoadImageFromUrl);
            saveImageButton = CreateButton("Save", SaveImage);

            SetParams();
            SetControls();
        }

        private void SetParams()
        {
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnCount = 4; 
            tableLayoutPanel1.RowCount = 2; 
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 93F)); // % for height
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7F)); //

            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;

            checkBox1.Text = "Stretch";
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
        }

        private void SetControls()
        {
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.SetColumnSpan(pictureBox1, 4); 

            tableLayoutPanel1.Controls.Add(checkBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(clearButton, 0, 1);
            tableLayoutPanel1.Controls.Add(backgroundButton, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 3, 1);

            // right side btns
            flowLayoutPanel1.Controls.Add(showButton);
            flowLayoutPanel1.Controls.Add(loadImageButton);
            flowLayoutPanel1.Controls.Add(cropButton);
            flowLayoutPanel1.Controls.Add(saveImageButton);

            Controls.Add(tableLayoutPanel1);
        }

        // methods for buttons

        private void SaveImage(object? sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                }
            }
        }

        private void LoadImageFromUrl(object? sender, EventArgs e)
        {
            string inputlink = Interaction.InputBox("Sisesta link pildile", "Lingilt alla laadida");
            try
            {
                pictureBox1.Load(inputlink);
            }
            catch
            {
                MessageBox.Show("Viga pildi laadimisel URL-ist.");
            }
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

        private void CropImage(Rectangle cropArea)
        {
            if (pictureBox1.Image != null && pictureBox1.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                // old image size
                Bitmap oldImage = (Bitmap)pictureBox1.Image;
                int originalWidth = oldImage.Width;
                int originalHeight = oldImage.Height;

                // current image size
                int displayedWidth = pictureBox1.ClientSize.Width;
                int displayedHeight = pictureBox1.ClientSize.Height;

                // scaling changes
                float scaleX = (float)originalWidth / displayedWidth;
                float scaleY = (float)originalHeight / displayedHeight;

                // new image coordinates
                Rectangle adjustedCropArea = new Rectangle(
                    (int)(cropArea.X * scaleX),
                    (int)(cropArea.Y * scaleY),
                    (int)(cropArea.Width * scaleX),
                    (int)(cropArea.Height * scaleY));

                // cut image
                Bitmap newImage = oldImage.Clone(adjustedCropArea, oldImage.PixelFormat);
                oldImage.Dispose();
                pictureBox1.Image = newImage;
            }
            // default
            else if (pictureBox1.Image != null)
            {    
                Bitmap oldImage = (Bitmap)pictureBox1.Image;
                Bitmap newImage = oldImage.Clone(cropArea, oldImage.PixelFormat);
                oldImage.Dispose();
                pictureBox1.Image = newImage;
            }
        }

        // ----------------------------------------------------------------------------
        // crop methods
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
            if (e.Button == MouseButtons.Left)
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

        private void CropButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.MouseDown += PictureBox1_MouseDown;
                pictureBox1.MouseMove += PictureBox1_MouseMove;
                pictureBox1.MouseUp += PictureBox1_MouseUp;
                pictureBox1.Paint += PictureBox1_Paint;
            }
        }

        private void ResetCrop()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.MouseDown -= PictureBox1_MouseDown;
                pictureBox1.MouseMove -= PictureBox1_MouseMove;
                pictureBox1.MouseUp -= PictureBox1_MouseUp;
                pictureBox1.Paint -= PictureBox1_Paint;

                cropRectangle = Rectangle.Empty;
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Red, cropRectangle);
        }

        //--------------------------


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
