using System.Collections.Generic;
using StardewValley.TerrainFeatures;

namespace AutoShaker.Helpers
{
	public static class TreeExtensions
	{
		public static List<string> GetSeedAndSeedItemIds(this Tree tree)
		{
			var itemIds = new List<string>();
			var treeData = tree.GetData();

			if (treeData.SeedItemId != null)
			{
				itemIds.Add(treeData.SeedItemId);
			}

			if (treeData.SeedDropItems != null)
			{
				foreach (var item in treeData.SeedDropItems)
				{
					if (item?.ItemId != null)
					{
						itemIds.Add(item.ItemId);
					}
				}
			}

			return itemIds;
		}
	}
}
