using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Recognizers
{

	public class Line
	{
		public Line(double x1, double y1, double x2, double y2)
		{
			Start = new Point((int)x1, (int)y1);
			End = new Point((int)x2, (int)y2);
		}

		public Point Start { get; set; }
		public Point End { get; set; }
	}
}
