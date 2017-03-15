using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Recognizers
{
	public class LineRecognizer
	{
		/// <summary>
		/// Clip line to the bounds
		/// </summary>
		/// <param name="rad"></param>
		/// <param name="angle"></param>
		/// <param name="h"></param>
		/// <param name="w"></param>
		/// <returns></returns>
		private static Line ClipToBounds(double rad, double angle, double h, double w)
		{
			double x1 = 0;
			double x2 = 0;
			double y1 = 0;
			double y2 = 0;
			var phi = angle * Math.PI / 180;

			// rad = Math.Cos(phi) * x + Math.Sin(phi) * y
			// left side
			if (rad / Math.Sin(phi) < 0) {
				y1 = 0;
				x1 = rad / Math.Cos(phi);
			}

			if (rad / Math.Sin(phi) > h) {
				x1 = (rad - Math.Sin(phi) * h) / Math.Cos(phi);
				y1 = h;
			}

			// right side
			if ((rad - Math.Cos(phi) * w) / Math.Sin(phi) < 0) {
				x2 = rad / Math.Cos(phi);
				y2 = 0;
			}

			if ((rad - Math.Cos(phi) * w) / Math.Sin(phi) > h) {
				x2 = (rad - Math.Sin(phi) * h) / Math.Cos(phi);
				y2 = h;
			}


			// top Side
			if ((rad - Math.Sin(phi) * h) / Math.Cos(phi) < 0) {
				x1 = 0;
				y1 = rad / Math.Sin(phi);
			}

			if ((rad - Math.Sin(phi) * h) / Math.Cos(phi) > w) {
				x2 = w;
				y2 = (rad - Math.Cos(phi) * w) / Math.Sin(phi);
			}

			// bottom Side
			if (rad / Math.Cos(phi) < 0) {
				x1 = 0;
				y1 = rad / Math.Sin(phi);
			}

			if (rad / Math.Cos(phi) > w) {
				x2 = w;
				y2 = (rad - Math.Cos(phi) * w) / Math.Sin(phi);
			}

			return new Line(x1, y1, x2, y2);

		}

		public static List<Line> RecognizeLines(ref List<Point> points, int width, int height)
		{
			var threshold = width * 0.30;
			var startTime = DateTime.Now;
			var epsilon = 0.0000001;

			List<Line> lines = new List<Line>();

			// Calculate max possible radius
			int maxRad = (int)Math.Sqrt(height * height + width * width);

			// Added lines buffer
			bool[,] added = new bool[360, 2 * maxRad];
			var i = 0;

			Dictionary<int, Dictionary<int, int>> accum = new Dictionary<int, Dictionary<int, int>>();
			foreach (var point in points) {
				i++;

				for (int angle = -90; angle < 90; angle += 1) {
					var phi = angle * Math.PI / 180;
					var rad = point.X * Math.Cos(phi) + point.Y * Math.Sin(phi);
					var radKey = (int)rad;
					if (radKey == 0) continue;
					if (added[angle + 90, radKey + maxRad]) continue;

					if (!accum.ContainsKey(angle)) {
						accum.Add(angle, new Dictionary<int, int>());
					}

					if (!accum[angle].ContainsKey(radKey)) {
						accum[angle].Add(radKey, 0);
					}
					accum[angle][radKey]++;
					if (accum[angle][radKey] > threshold && !added[angle + 90, radKey + maxRad]) {
						var sin = Math.Sin(phi);
						var tan = Math.Tan(phi);
						sin = sin == 0 ? epsilon : sin;
						if (double.IsPositiveInfinity(tan)) {
							tan = double.MaxValue;
						}
						else if (double.IsNegativeInfinity(tan)) {
							tan = -double.MaxValue;
						}
						else if (tan == 0) {
							tan = epsilon;
						}
						var x1 = 0;
						var y1 = (rad) / sin;
						var x2 = width;
						var y2 = (rad) / sin - width * (1 / tan);
						if (angle == 0) {
							lines.Add(new Line(Math.Abs(rad), 0, Math.Abs(rad), height));
						}
						else if (angle == -90 || angle == 90) {
							lines.Add(new Line(0, Math.Abs(rad), width, Math.Abs(rad)));
						}
						else {
							lines.Add(ClipToBounds(rad, angle, height, width));
						}
						added[angle + 90, radKey + maxRad] = true;
					}
				}
			}

			Console.WriteLine("[Lines Recognize] Elapsed time:" + (DateTime.Now - startTime).ToString());
			return lines;
		}
	}
}
