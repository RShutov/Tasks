using Recognizers;
using System;
using System.Drawing;
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

		private void DetectLinesStripMenuItem_Click(object sender, EventArgs e)
		{

			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK) {
				var bmp = new Bitmap(Bitmap.FromFile(dialog.FileName));
				var newPicture = FigureRecognizer.DetectLines(bmp, Consts.ThresholdParams);
				this.pictureBox1.Image = newPicture;
				this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			}
		}

		private void DetectContourToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK) {
				var bmp = new Bitmap(Bitmap.FromFile(dialog.FileName));
				var newPicture = FigureRecognizer.DetectContour(bmp, Consts.ThresholdParams);
				this.pictureBox1.Image = newPicture;
				this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			}
		}
	}
}
