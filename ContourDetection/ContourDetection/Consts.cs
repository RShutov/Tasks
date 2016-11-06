using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
	public static class Consts
	{
		public static bool IsAdapt = true;
		public static bool IsOriginalValue = false;
		public static float filter = 0.1f;
		public static int step = 4;
		public static int thresholdWidnowSize = 10;

		public static  int[][] SobelMatrix =
		{
			new int[] { -1, -2, -1 },
			new int[] {  0,  0,  0 },
			new int[] {  1,  2,  1 },
		};

		public static  int[][] SobelMatrixTransporant =
		{
			new int[] { 1,  0, -1 },
			new int[] {  2,  0,  -2 },
			new int[] {  1,  0,  -1 },
		};
	}
}
