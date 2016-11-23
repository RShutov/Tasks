using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction
{
	class Program
	{
		public WeatherModel findMin(WeatherModel item, ref List<WeatherModel> dataSet)
		{
			double min = double.MaxValue;
			WeatherModel origin = null;
			foreach(var elem in dataSet)
			{
				var diff = WeatherModel.ToNormal(item, elem);
				if(diff < min)
				{
					min = diff;
					origin = elem;
				}
			}
			return origin;
		}

		static void Main(string[] args)
		{
			int count = 6000;
			int i = 0;
			List<WeatherModel> trainingSet = new List<WeatherModel>();
			List<WeatherModel> dataSet = new List<WeatherModel>();
			using (TextFieldParser parser = new TextFieldParser(@"..\..\Data\vlad_weather_data.csv"))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				string[] header = parser.ReadFields();
				while (!parser.EndOfData)
				{
					if (i == count) break;
					string[] fields = parser.ReadFields();
					string[] modelFields = {
						fields[0],
						fields[1],
						fields[2],
						fields[3],
						fields[4],
						fields[5],
						fields[7],
						fields[22],
						fields[23],
					};
					var model = new WeatherModel(modelFields);
					
					if (model.Date.Split(' ')[0].EndsWith("2016")) {
						trainingSet.Add(model);
					} else {
						dataSet.Add(model);
					}

					Console.SetCursorPosition(0, Console.CursorTop >=3? Console.CursorTop - 3: Console.CursorTop);
					Console.WriteLine($"Items total {i}");
					Console.WriteLine($"Training items {trainingSet.Count}");
					Console.WriteLine($"Data items {dataSet.Count}");
					i++;
				}
				Console.WriteLine();
				Console.WriteLine();
				int errorCount = 0;
				foreach(var item in trainingSet)
				{
					double min = double.MaxValue;
					WeatherModel origin = null;
					double diff = 0;
					foreach (var elem in dataSet)
					{
						diff = WeatherModel.ToNormal(item, elem);
						if (diff < min)
						{
							min = diff;
							origin = elem;
						}
					}
					if(origin.RRR == 0 && item.RRR != 0 || origin.RRR != 0 && item.RRR == 0)
					{
						errorCount++;
					}
					Console.SetCursorPosition(0, Console.CursorTop - 2);
					Console.WriteLine($"ErrorCount {errorCount} of total {trainingSet.Count}");
					Console.WriteLine($"Prediction precision {(int)((1 - (float)errorCount / trainingSet.Count) * 100 )}" + "%");
				}
				
				//var elem = findMin(trainingSet[15], ref dataSet);

			}
		}
	}
}
