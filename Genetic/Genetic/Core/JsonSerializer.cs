using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using System.Web.UI;
using System.Web.Script.Serialization;

namespace Genetic.Core
{
	class JsonSerializer
	{
		public static T FromJson<T>(string jsonString)
		{
			return  (T)(new JavaScriptSerializer().Deserialize(jsonString, typeof(T)));
		}

		public static string ToJson(object obj)
		{
			return new JavaScriptSerializer().Serialize(obj);
		}
	}
}
