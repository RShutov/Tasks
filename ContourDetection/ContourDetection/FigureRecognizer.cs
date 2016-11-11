using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
	public class Circle
	{
		public int X;
		public int Y;
		public int R;
		public int dotCount;
		bool[] angles = new bool[360];
		public void addDot(int angle)
		{
			if(angles[angle])
			{
				angles[angle] = false;
				dotCount++;
			}
			
		}
	}

	static class FigureRecognizer
	{
		public static List<Circle> RecognizeCirclesFast(ref List<Point> points, int width, int height)
		{
			Circle[,,] circles = new Circle[width, height, 80];
			List<Circle> activeCicles = new List<Circle>();
			var i = 0;
			foreach(var point in points)
			{
				Debug.WriteLine($"Point {i} of {points.Count}");
				i++;
				for (int r = 20; r < 100; r++)
				{
					
					for (int fi = 0; fi < 360; fi += 1)
					{
						var x = point.X + r * Math.Cos(fi * Math.PI / 180);
						var y = point.Y + r * Math.Sin(fi * Math.PI / 180);
						if (
								x < 0 ||
								x > width - 1 ||
								y < 0 ||
								y > height - 1
							)
						{
							continue;
						}
						var curr = circles[(int)x, (int)y, r-20];
						if (Contains(point, (int)x, (int)y, width, height))
						{
							curr.addDot(fi);
							if (!activeCicles.Contains(curr))
							{
								activeCicles.Add(curr);
							}
						}
						
					}
				}
			}
			return activeCicles;
		}

		public static List<Tuple<int, int, int>> RecognizeCircles(ref bool[,] points, int width, int height)
		{
			
			List<Tuple<int, int, int>> circles = new List<Tuple<int, int, int>>();
			int maxR = Math.Min(width, height);
			for (int r = 20; r <= 100; r++)
			{
				Debug.WriteLine($"{r}\n");
				for (int x = 0; x < width - 1; x+=3)
				{
					for (int y = 0; y < height - 1; y+=3)
					{
						var count = 0;
						for(int fi = 0; fi < 360; fi += 1)
						{
							var componentX = x + r * Math.Cos(fi * Math.PI / 180);
							var componentY = y + r * Math.Sin(fi * Math.PI / 180);
							if(
								componentX < 0          || 
								componentX > width - 1  || 
								componentY < 0          ||
								componentY > height - 1
							){
								continue;
							}
							if (Contains(points, (int)componentX, (int)componentY, width, height))
							{
								count++;
							}
						}
						if((float)count / 360 >= 0.5)
						{
							circles.Add(new Tuple<int, int, int>(x, y, r));
						}
					}
				}
			}
			return circles;
		}

		private static bool Contains(Point point, int x, int y, int width, int height)
		{
			return Math.Abs(point.X - x) <= 2 && Math.Abs(point.Y - y) <= 2;
		}

		private static bool Contains(bool[,] points, int x, int y, int width, int height)
		{
			bool IsCircle = false;
			
			IsCircle |= points[x, y];
			/*if (y - 1 >= 0)
				IsCircle |= points[x, y - 1];
			if (y + 1 < height - 1)
				IsCircle |= points[x, y + 1];
			if (x + 1 < width - 1)
				IsCircle |= points[x + 1, y];
			if (x - 1 >= 0)
				IsCircle |= points[x - 1 , y];*/
			return IsCircle;
		}
	}
}
