using System.Collections.Generic;
using System.Linq;
using AutoShaker.Classes;

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

		public static bool IsNullOrEmpty<T>(this List<T> list)
		{
			if (list == null) return false;

			return !list.Any();
		}

		public static bool TryGetItem(this List<ForageableItem> items, string itemId, out ForageableItem? item)
		{
			item = null;

			if (items == null || itemId == null)
			{
				return false;
			}

			foreach (var fItem in items)
			{
				if (fItem.QualifiedItemId.IEquals(itemId))
				{
					item = fItem;
					return true;
				}
			}

			return false;
		}
	}
}
