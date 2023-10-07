using AutoShaker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShaker.Helpers
{
	public static class ListExtensions
	{
		public static void AddDistinct(this List<ForageableItem> list, ForageableItem item)
		{
			if (list.Any(i => i.QualifiedItemId.Equals(item.QualifiedItemId))) return;

			list.Add(item);
		}

		public static void SortByDisplayName(this List<ForageableItem> items)
		{
			items.Sort((x, y) => string.CompareOrdinal(x.DisplayName, y.DisplayName));
		}
	}
}
