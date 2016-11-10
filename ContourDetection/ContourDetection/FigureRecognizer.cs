using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
	static class FigureRecognizer
	{
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
