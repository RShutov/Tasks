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
		static void Main(string[] args)
		{
			int count = 150;
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
					if (model.Date.Year == 2016) {
						trainingSet.Add(model);
						Console.WriteLine(model.ToString());
					} else {
						dataSet.Add(model);
					}
					i++;
				}
			}
		}
	}
}
