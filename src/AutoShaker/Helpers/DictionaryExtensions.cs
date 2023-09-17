using System;
using System.Collections.Generic;

namespace AutoShaker.Helpers
{
	public static class DictionaryExtensions
	{
		public static void AddOrIncrement<TKey>(this Dictionary<TKey, int> dict, TKey key) where TKey : IEquatable<TKey>
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
