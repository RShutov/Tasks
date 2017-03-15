using ImageRecognizeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
	public static class Consts
	{
		public static ThresholdParams ThresholdParams = new ThresholdParams {
			IsAdapt = true,
			Filter = 0.1f,
			Step = 4,
			WidnowSize = 5,
		};

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
