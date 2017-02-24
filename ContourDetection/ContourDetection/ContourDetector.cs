using ImageRecognizeHelper;
using ImageRecognizeHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
	public static class ContourDetector
	{
		
		private static bool AdaptThreshold(ref byte[] b, int x, int y, ref byte[] newB, int width, int height)
		{
			
			var originalCoord = (y * width + x) * Consts.step;
			if (x - Consts.thresholdWidnowSize / 2 < 0 ||
				x + Consts.thresholdWidnowSize / 2 > width - 1 ||
				y - Consts.thresholdWidnowSize / 2 < 0 ||
				y + Consts.thresholdWidnowSize / 2 > height - 1
			)
			{
				var v = Color.FromArgb(
								b[originalCoord + 3],
								b[originalCoord + 2],
								b[originalCoord + 1],
								b[originalCoord]
							).GetBrightness();
				v = v >= Consts.filter ? v : 0;
				if (!Consts.IsOriginalValue && v !=0)
				{
					v = 1;
				}				
				HsvToRgb(0, 0, v, ref newB, x, y, width, height);
				return v == 1? true : false;
			}
			float max = float.MinValue;
			float min = float.MaxValue;
			float originalColor = 0;
			for (int i = 0; i < Consts.thresholdWidnowSize; i++)
			{
				for (int j = 0; j < Consts.thresholdWidnowSize; j++)
				{
					var coord = ((y - Consts.thresholdWidnowSize / 2 + j) * width + 
						(x - Consts.thresholdWidnowSize / 2 + i)) * Consts.step;
					if(coord == originalCoord)
					{
					originalColor = Color.FromArgb(
						b[coord + 3],
						b[coord + 2],
						b[coord + 1],
						b[coord]
						).GetBrightness();
					}
					var value = Color.FromArgb(
						b[coord + 3],
						b[coord + 2],
						b[coord + 1],
						b[coord]
					).GetBrightness();
					if (value > max)
					{
						max = value;
					}
					if (value < min)
					{
						min = value;
					}
				}
			}
			float mean = (max + min) / 2;
			float val = originalColor - mean;
			if(val < Consts.filter) {
				val = 0;
			}
			if (!Consts.IsOriginalValue && val != 0) {
				val = 1;
			}
			HsvToRgb(0, 0, val, ref newB, x, y, width, height);
			return val == 1 ? true : false;
		}

		public static List<Point> DetectContourPoints(Bitmap target)
		{
			BitmapData targetData1 = target.LockBits(new Rectangle(0, 0, target.Width, target.Height), ImageLockMode.ReadWrite, target.PixelFormat);
			byte[] targetData = new byte[targetData1.Stride * targetData1.Height];
			Marshal.Copy(targetData1.Scan0, targetData, 0, targetData.Length);
			target.UnlockBits(targetData1);
			byte[] newTargetData = new byte[targetData.Length];
			bool[,] mask = new bool[target.Size.Width, target.Size.Height];
			List<Point> points = new List<Point>();
			if (Consts.IsAdapt) {
				points = ThresholdAdapter.Adapt(target, Consts.step, Consts.thresholdWidnowSize, Consts.filter);
			}
			else {
				for (int i = 0; i < target.Size.Width; i++) {
					for (int j = 0; j < target.Size.Height; j++) {
						var brightness = GetValue(i, j, ref targetData, target.Width, target.Height);
						if (brightness == 1)
							points.Add(new Point(i, j));
						mask[i, j] = brightness == 1 ? true : false;
						HsvToRgb(0, 0, brightness, ref newTargetData, i, j, target.Width, target.Height);
					}
				}
			}
			return points;
		}

		public static Bitmap DetectCircles(Bitmap target)
		{
			var newTarget = new Bitmap(target.Width, target.Height);
			var points = DetectContourPoints(target);
			BitmapData targetData1 = target.LockBits(
				new Rectangle(0, 0, target.Width, target.Height), 
				ImageLockMode.ReadWrite, target.PixelFormat);
		
			byte[] targetData = new byte[targetData1.Stride * targetData1.Height];
			byte[] newTargetData = new byte[targetData.Length];
			var circles = FigureRecognizer.RecognizeCirclesFast(ref points, target.Size.Width, target.Size.Height);
			newTarget = new Bitmap(target.Width, target.Height, targetData1.Stride,
				target.PixelFormat,
				Marshal.UnsafeAddrOfPinnedArrayElement(newTargetData, 0));
			var graphics = Graphics.FromImage(newTarget);

			foreach (var elem in points) {
				newTarget.SetPixel(elem.X, elem.Y, Color.Black);
			}

			foreach (var elem in circles) {
				if ((float)elem.dotCount / 360 > 0.8)
					DrawCircle(elem.X, elem.Y, elem.R, graphics);
			}
			target.UnlockBits(targetData1);
			return newTarget;
		}

		public static Bitmap DetectContour(Bitmap target)
		{
			var points = DetectContourPoints(target);
			var newTarget = new Bitmap(target.Width, target.Height);
			foreach (var elem in points) {
				newTarget.SetPixel(elem.X, elem.Y, Color.Black);
			}
			return newTarget;
		}

		private static void DrawCircle(int x, int y, int r, Graphics g)
		{
			g.DrawEllipse(new Pen(Color.Red), x - r, y - r,
					 2*r, 2*r);
		}

		private static float GetValue(int x, int y, ref byte[] b, int width, int height)
		{
			float sumX = 0;
			float sumY = 0;
			if (x - Consts.SobelMatrix.Length / 2 < 0 || 
				x + Consts.SobelMatrix.Length / 2  > width - 1 || 
				y - Consts.SobelMatrix.Length / 2 < 0 || 
				y + Consts.SobelMatrix.Length / 2 > height - 1
			){
				return TryGetPixel(x, y, ref b, width);
			}
			for (int i = 0; i < Consts.SobelMatrix.Length; i++)
			{
				for (int j = 0; j < Consts.SobelMatrix[0].Length; j++)
				{
					sumY += Consts.SobelMatrix[i][j] * 
						TryGetPixel(
							x - Consts.SobelMatrix.Length / 2 + i, 
							y - Consts.SobelMatrix[0].Length / 2 + j, 
							ref b,
							width);
					sumX += Consts.SobelMatrixTransporant[i][j] * 
						TryGetPixel(
							x - Consts.SobelMatrix.Length / 2 + i, 
							y - Consts.SobelMatrix[0].Length / 2 + j, 
							ref b,
							width);
				}
			}
			var val = (float)Math.Sqrt(sumY * sumY + sumX * sumX);
			return val >= Consts.filter ? 1 : 0;
		}

		private static float TryGetPixel(int v1, int v2, ref byte[] b, int width)
		{
			var coord = (v2 * width  +  v1) * Consts.step;
			return Color.FromArgb(
				b[coord + 3],
				b[coord + 2],
				b[coord + 1],
				b[coord]			
			).GetBrightness();
		}

		private static void HsvToRgb(double h, double S, double V, ref byte[] source, int x, int y, int width, int height)
		{
			int r;
			int g;
			int b;
			double H = h;
			while (H < 0) { H += 360; };
			while (H >= 360) { H -= 360; };
			double R, G, B;
			if (V <= 0) { R = G = B = 0; }
			else if (S <= 0)
			{
				R = G = B = V;
			}
			else
			{
				double hf = H / 60.0;
				int i = (int)Math.Floor(hf);
				double f = hf - i;
				double pv = V * (1 - S);
				double qv = V * (1 - S * f);
				double tv = V * (1 - S * (1 - f));
				switch (i)
				{

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
			r = ImageHelpers.Clamp((int)(R * 255.0));
			g = ImageHelpers.Clamp((int)(G * 255.0));
			b = ImageHelpers.Clamp((int)(B * 255.0));
			var coord = (y * width + x) * Consts.step;
			source[coord + 3] = 255;
			source[coord] = (byte)r;
			source[coord + 1] = (byte)g;
			source[coord  + 2] = (byte)b; 
		}
	}
}
