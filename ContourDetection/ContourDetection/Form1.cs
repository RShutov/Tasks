using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContourDetection
{
	public partial class ConourDetectionForm : Form
	{
		private int[][] SobelMatrix = 
		{
			new int[] { -1, -2, -1 },
			new int[] {  0,  0,  0 },
			new int[] {  1,  2,  1 },
		};

		private float getValue(int x, int y, Bitmap b)
		{
			float sum = 0;
			for (int i = 0; i < SobelMatrix.Length; i++) {
				for (int j = 0; j < SobelMatrix[0].Length; j++) {
					sum += SobelMatrix[i][j] * TryGetPixel(x - SobelMatrix.Length / 2 +i, y - SobelMatrix[0].Length / 2 +j, b);
				}
			}
			return sum;
		}

		private float TryGetPixel(int v1, int v2, Bitmap b)
		{
			if(v1 < 0 || v1 > b.Width-1 || v2 < 0 || v2 >b.Height-1) {
				return 0;
			}
			return b.GetPixel(v1, v2).GetBrightness();
		}

		Color HsvToRgb(double h, double S, double V)
		{
			int r;
			int g;
			int b;
			double H = h;
			while (H < 0) { H += 360; };
			while (H >= 360) { H -= 360; };
			double R, G, B;
			if (V <= 0) { R = G = B = 0; } else if (S <= 0) {
				R = G = B = V;
			} else {
				double hf = H / 60.0;
				int i = (int)Math.Floor(hf);
				double f = hf - i;
				double pv = V * (1 - S);
				double qv = V * (1 - S * f);
				double tv = V * (1 - S * (1 - f));
				switch (i) {

					// Red is the dominant color

					case 0:
						R = V;
						G = tv;
						B = pv;
						break;

					// Green is the dominant color

					case 1:
						R = qv;
						G = V;
						B = pv;
						break;
					case 2:
						R = pv;
						G = V;
						B = tv;
						break;

					// Blue is the dominant color

					case 3:
						R = pv;
						G = qv;
						B = V;
						break;
					case 4:
						R = tv;
						G = pv;
						B = V;
						break;

					// Red is the dominant color

					case 5:
						R = V;
						G = pv;
						B = qv;
						break;

					// Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

					case 6:
						R = V;
						G = tv;
						B = pv;
						break;
					case -1:
						R = V;
						G = pv;
						B = qv;
						break;

					// The color is not defined, we should throw an error.

					default:
						//LFATAL("i Value error in Pixel conversion, Value is %d", i);
						R = G = B = V; // Just pretend its black/white
						break;
				}
			}
			r = Clamp((int)(R * 255.0));
			g = Clamp((int)(G * 255.0));
			b = Clamp((int)(B * 255.0));
			return Color.FromArgb(r,g,b);
		}

		/// <summary>
		/// Clamp a value to 0-255
		/// </summary>
		int Clamp(int i)
		{
			if (i < 0) return 0;
			if (i > 255) return 255;
			return i;
		}

		public ConourDetectionForm()
		{
			InitializeComponent();

		}

		private void toolStripDropDownButton1_Click(object sender, EventArgs e)
		{

		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK) {
				var bmp = new Bitmap(Bitmap.FromFile(dialog.FileName));
				var newBmp = new Bitmap(bmp.Width, bmp.Height);
				for (int i = 0; i < bmp.Size.Width; i++) {
					for (int j = 0; j < bmp.Size.Height; j++) {
						var brightness = getValue(i, j, bmp);
						newBmp.SetPixel(i, j, HsvToRgb(0, 0, brightness));
					}
				}
				this.pictureBox1.Image = newBmp;
				this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			}
		}
	}
}
