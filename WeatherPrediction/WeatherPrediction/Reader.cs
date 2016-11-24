using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction
{
	public static class Reader
	{
		public static List<WeatherModel> ReadWeatherFrom(string path)
		{
			List<WeatherModel> weatherSet = new List<WeatherModel>();
			using (TextFieldParser parser = new TextFieldParser(path))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				int i = 0;
				string[] header = parser.ReadFields();
				while (!parser.EndOfData)
				{
					if (i == DataConsts.RowCount) break;
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
					weatherSet.Add(new WeatherModel(modelFields));
					/*
					if (model.Date.Split(' ')[0].EndsWith("2016"))
					{
						TrainingSet.Add(model);
					}
					else
					{
						DataSet.Add(model);
					}
					*/
					if(DataConsts.Echo) {
						Console.SetCursorPosition(0, Console.CursorTop >= 1 ? Console.CursorTop - 1 : Console.CursorTop);
						Console.WriteLine($"Items total {i}");
					}
						i++;
				}

			}
			return weatherSet;
		}
	}
}
