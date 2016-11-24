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
			Predictor p = new Predictor();
			var data = Reader.ReadWeatherFrom(@"..\..\Data\vlad_weather_data.csv");
			var trainingSet = data.FindAll(w => w.Date.Split(' ')[0].EndsWith("2016"));
			var dataSet = data.FindAll(w => !w.Date.Split(' ')[0].EndsWith("2016"));
			p.DataSet = dataSet;
			p.TrainingSet = trainingSet;
			p.FindAll();
		}
	}
}
