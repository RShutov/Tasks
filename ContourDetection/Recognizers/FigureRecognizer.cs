using ImageRecognizeHelper;
using ImageRecognizeHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Recognizers;
using System.Drawing;

namespace Recognizers
{
	public static class FigureRecognizer
	{
		private static List<Point> DetectContourPoints(Bitmap target, ThresholdParams param)
		{
			BitmapData targetData1 = target.LockBits(new Rectangle(0, 0, target.Width, target.Height), ImageLockMode.ReadWrite, target.PixelFormat);
			byte[] targetData = new byte[targetData1.Stride * targetData1.Height];
			Marshal.Copy(targetData1.Scan0, targetData, 0, targetData.Length);
			target.UnlockBits(targetData1);
			byte[] newTargetData = new byte[targetData.Length];
			bool[,] mask = new bool[target.Size.Width, target.Size.Height];
			List<Point> points = new List<Point>();
			if (param.IsAdapt) {
				points = ThresholdAdapter.Adapt(target, param);
			} 
			return points;
		}

		public static Bitmap DetectLines(Bitmap target, ThresholdParams param)
		{
			var newTarget = new Bitmap(target.Width, target.Height);
			var points = DetectContourPoints(target, param);
			BitmapData targetData1 = target.LockBits(
				new Rectangle(0, 0, target.Width, target.Height), 
				ImageLockMode.ReadWrite, target.PixelFormat);
		
			byte[] targetData = new byte[targetData1.Stride * targetData1.Height];
			byte[] newTargetData = new byte[targetData.Length];
			var lines = LineRecognizer.RecognizeLines(ref points, target.Size.Width, target.Size.Height);
			newTarget = new Bitmap(target.Width, target.Height, targetData1.Stride,
				target.PixelFormat,
				Marshal.UnsafeAddrOfPinnedArrayElement(newTargetData, 0));
			var graphics = Graphics.FromImage(newTarget);

			foreach (var elem in points) {
				newTarget.SetPixel(elem.X, elem.Y, Color.Black);
			}

			foreach (var e in lines) {
				DrawLine(e, graphics);
			}
			target.UnlockBits(targetData1);
			return newTarget;
		}

		public static Bitmap DetectContour(Bitmap target, ThresholdParams param)
		{
			var points = DetectContourPoints(target, param);
			var newTarget = new Bitmap(target.Width, target.Height);
			foreach (var elem in points) {
				newTarget.SetPixel(elem.X, elem.Y, Color.Black);
			}
			return newTarget;
		}

		private static void DrawCircle(int x, int y, int r, Graphics g)
		{
			g.DrawEllipse(new Pen(Color.Red), x - r, y - r, 2 * r, 2 * r);
		}

		private static void DrawLine(Line line, Graphics g)
		{
			g.DrawLine(new Pen(Color.Red), line.Start.X, line.Start.Y, line.End.X, line.End.Y);
		}
	}
}
