using System;
using System.Collections.Generic;

namespace AutoShaker.Helpers
{
	public static class DictionaryExtensions
	{
		public static int SumAll(this Dictionary<string, int> dict)
		{
			var sum = 0;

			foreach (var kvp in dict)
			{
				sum += kvp.Value;
			}

			return sum;
		}

		public static int SumAll(this Dictionary<string, Dictionary<string, int>> dict)
		{
			var sum = 0;

			foreach (var kvp in dict)
			{
				if (kvp.Value == null) continue;

				sum += kvp.Value.SumAll();
			}

			return sum;
		}

		public static void AddOrIncrement<TKey>(this Dictionary<TKey, int> dict, TKey key) where TKey : notnull
		{
			if (dict.ContainsKey(key))
			{
				dict[key] += 1;
			}
			else
			{
				dict.Add(key, 1);
			}
		}
	}
}
