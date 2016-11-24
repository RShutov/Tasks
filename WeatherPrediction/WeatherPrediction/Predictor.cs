using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction
{
	public class Predictor
	{
		public List<WeatherModel> TrainingSet { get; set; }
		public List<WeatherModel> DataSet { get; set; }

		private WeatherModel findMin(WeatherModel item, ref List<WeatherModel> dataSet)
		{
			double min = double.MaxValue;
			WeatherModel origin = null;
			foreach (var elem in dataSet)
			{
				var diff = WeatherModel.ToNormal(item, elem);
				if (diff < min)
				{
					min = diff;
					origin = elem;
				}
			}
			return origin;
		}

		public void FindAll()
		{
			Console.WriteLine();
			Console.WriteLine();
			int errorCount = 0;
			foreach (var item in TrainingSet)
			{
				double min = double.MaxValue;
				WeatherModel origin = null;
				double diff = 0;
				foreach (var elem in DataSet)
				{
					diff = WeatherModel.ToNormal(item, elem);
					if (diff < min) {
						min = diff;
						origin = elem;
					}
				}
				if (origin == null) continue;
				if (origin.RRR == 0 && item.RRR != 0 || origin.RRR != 0 && item.RRR == 0) {
					errorCount++;
				}
				Console.SetCursorPosition(0, Console.CursorTop - 2);
				Console.WriteLine($"ErrorCount {errorCount} of total {TrainingSet.Count}");
				Console.WriteLine($"Prediction precision {(int)((1 - (float)errorCount / TrainingSet.Count) * 100)}" + "%");
			}
		}


		public Predictor()
		{
			TrainingSet = new List<WeatherModel>();
			DataSet = new List<WeatherModel>();
		}
	}
}
