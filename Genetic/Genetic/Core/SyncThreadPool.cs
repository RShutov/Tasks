using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic
{
	class SyncronizedThreadPool<TKey, TValue, TParam>
	{
		List<Task<Tuple<TKey, TValue>>> pool;
		Func<TValue, TParam, Tuple<TKey, TValue>> keyFunc;
		Func<TValue, TValue> valueFunc;
		TParam param;

		public delegate void OnAll(Tuple<TKey, TValue> value);

		public void add(TValue value)
		{
			pool.Add(Task<Tuple<TKey, TValue>>.Factory.StartNew(() => keyFunc.Invoke(valueFunc.Invoke(value), param)));
		}

		public SyncronizedThreadPool(Func<TValue,TParam, Tuple<TKey, TValue>> keyFunc, Func<TValue, TValue> valueFunc, TParam param)
		{
			this.param = param;
			this.keyFunc = keyFunc;
			this.valueFunc = valueFunc;
			pool = new List<Task<Tuple<TKey, TValue>>>();
		}

		public Tuple<TKey, TValue> start()
		{
			var result = Task.WhenAll(pool.ToArray());
			pool.Clear();
			return result.Result.Last();
		}
	}
}
