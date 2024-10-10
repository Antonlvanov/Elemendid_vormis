using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Elemendid_vormis_TARpv23
{
    public partial class Pildi_vaatamine : Form
    {
        public Pildi_vaatamine(int w, int h)
        {
            InitializeComponent();
            this.Height = 600;
            this.Width = 1000;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void backgroundColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDialog1.Color;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JPEG Files|*.jpg|PNG Files|*.png|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Load(openFileDialog.FileName);
                }
            }

        }

        private void Stretch_CheckedChanged(object sender, EventArgs e)
        {
            if (Stretch.Checked)
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
