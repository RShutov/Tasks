using ImageRecognizeHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ImageRecognizeHelper
{
	public class ThresholdParams
	{
		public bool IsAdapt { get; set; }
		public float Filter { get; set; }
		public int Step { get; set; }
		public int WidnowSize { get; set; }
	}

	public class ThresholdAdapter
	{
		private static bool AdaptThreshold(
			ref byte[] bitmap, 
			int x,
			int y,
			ThresholdParams param,
			double threshold,
			int width,
			int height
		) {

			var originalCoord = (y * width + x) * param.Step;
			if (x - param.WidnowSize / 2 < 0 ||
				x + param.WidnowSize / 2 > width - 1 ||
				y - param.WidnowSize / 2 < 0 ||
				y + param.WidnowSize / 2 > height - 1
			) {
				var v = Color.FromArgb(
								bitmap[originalCoord + 3],
								bitmap[originalCoord + 2],
								bitmap[originalCoord + 1],
								bitmap[originalCoord]
							).GetBrightness();
				v = v >= threshold ? v : 0;
				return v == 1 ? true : false;
			}

			float max = float.MinValue;
			float min = float.MaxValue;
			float originalColor = 0;

			for (int i = 0; i < param.WidnowSize; i++) {
				for (int j = 0; j < param.WidnowSize; j++) {
					var coord = ((y - param.WidnowSize / 2 + j) * width +
						(x - param.WidnowSize / 2 + i)) * param.Step;
					if (coord == originalCoord) {
						originalColor = Color.FromArgb(
							bitmap[coord + 3],
							bitmap[coord + 2],
							bitmap[coord + 1],
							bitmap[coord]
							).GetBrightness();
					}
					var value = Color.FromArgb(
						bitmap[coord + 3],
						bitmap[coord + 2],
						bitmap[coord + 1],
						bitmap[coord]
					).GetBrightness();
					if (value > max) {
						max = value;
					}
					if (value < min) {
						min = value;
					}
				}
			}
			float mean = (max + min) / 2;
			float val = originalColor - mean;
			if (val < threshold) {
				val = 0;
			}
			return val == 0 ? false : true;
		}

		public static List<Point> Adapt(Bitmap bitmap, ThresholdParams param,/* int step = 4, int size = 5,*/ double threshold = 0.1)
		{
			var startTime = DateTime.Now;
			var newTarget = new Bitmap(bitmap.Width, bitmap.Height);
			BitmapData bitmapData1 = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
			System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
			byte[] bitmapData = new byte[bitmapData1.Stride * bitmapData1.Height];
			System.Runtime.InteropServices.Marshal.Copy(bitmapData1.Scan0, bitmapData, 0, bitmapData.Length);
			bitmap.UnlockBits(bitmapData1);
			List<Point> points = new List<Point>();
			for (int i = 0; i < bitmap.Size.Width; i++) {
				for (int j = 0; j < bitmap.Size.Height; j++) {
					if (AdaptThreshold(ref bitmapData,i, j, param, threshold, bitmap.Width, bitmap.Height))
						points.Add(new Point(i, j));
				}
			}
			Console.WriteLine("[Threshold] Elapsed time:" + (DateTime.Now - startTime).ToString());
			return points;
		}
	}
}
