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
			using (TextFieldParser parser = new TextFieldParser(@"..\..\Data\vlad_weather_data.csv"))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				string[] header = parser.ReadFields();
				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();
					foreach (string field in fields)
					{
					}
				}
			}
		}
	}
}
