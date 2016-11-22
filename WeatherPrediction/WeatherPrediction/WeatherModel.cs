using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction
{
	class WeatherModel
	{
		/// <summary>DoWork is a method in the TestClass class.
		/// <para>ДАта и время</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Температура воздуха</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double T { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Атмосферное давление на уровне станции</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double Po { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Атмосферное давление, приведенное к среднему уровню моря</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double P { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Барическая тенденция: изменение атмосферного давления за последние три часа </para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double Pa { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Относительная влажность на высоте 2м над поверхностью земли</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double U { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Cкорость ветра на высоте 10-12 метров над земной поверхностью, осредненная за 
		/// 10-минутный период, непосредственно предшествовавший сроку наблюдения (метры в секунду)</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double Ef { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Температура точки росы на высоте 2м над поверхностью земли</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double Td { get; set; }

		/// <summary>DoWork is a method in the TestClass class.
		/// <para>Количество выпавших осадков</para>
		/// <seealso cref="WeatherPrediction.WeatherModel"/>
		/// </summary>
		public double RRR { get; set; }

		public static string DateFmt = "dd.MM.yyyy HH:mm";

		public WeatherModel(string[] data)
		{
			try {
				Date = DateTime.ParseExact(
					data[0], DateFmt, 
					System.Globalization.CultureInfo.InvariantCulture);
				T = TryParse(data[1]);
				Po  = TryParse(data[2]);
				P   = TryParse(data[3]);
				Pa  = TryParse(data[4]);
				U   = TryParse(data[5]);
				Ef  = TryParse(data[6]);
				Td  = TryParse(data[7]);
				RRR = TryParse(data[8]);
			} catch(IndexOutOfRangeException) {
				throw new Exception("wronng number params");
			}
		}

		private double TryParse(string value)
		{
			try {
				return Convert.ToDouble(value, CultureInfo.InvariantCulture);
			} catch (FormatException) {
				return 0;
			}
		}

		public override string ToString()
		{
			return string.Join(" ", new string[]{
				Date.ToString(DateFmt),
				T.ToString(),
				Po.ToString(),
				Pa.ToString(),
				U.ToString(),
				Ef.ToString(),
				Td.ToString(),
				RRR.ToString(),
			});
		}
	}
}
