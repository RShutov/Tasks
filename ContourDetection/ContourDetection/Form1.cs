using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContourDetection
{
	public partial class ConourDetectionForm : Form
	{
		

		public ConourDetectionForm()
		{
			InitializeComponent();

		}

		private void toolStripDropDownButton1_Click(object sender, EventArgs e)
		{

		}
		protected override void OnResize(EventArgs e)
		{
			if(pictureBox1.Image != null)
			{
				lock (this.pictureBox1.Image)
				{
					base.OnResize(e);
				}
			}	
		}

		private void DetectCirclesStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK) {
				var bmp = new Bitmap(Bitmap.FromFile(dialog.FileName));
				var newPicture = ContourDetector.DetectCircles(bmp);
				this.pictureBox1.Image = newPicture;
				//this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			}
		}

		private void DetectContourToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK) {
				var bmp = new Bitmap(Bitmap.FromFile(dialog.FileName));
				var newPicture = ContourDetector.DetectContour(bmp);
				this.pictureBox1.Image = newPicture;
				//this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			}
		}
	}
}
