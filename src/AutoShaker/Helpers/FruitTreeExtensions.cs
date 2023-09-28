using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShaker.Helpers
{
	public static class FruitTreeExtensions
	{
		public static List<string> GetFruitItemIds(this FruitTree fruitTree)
		{
			var itemIds = new List<string>();

			foreach (var fruit in fruitTree.fruit)
			{
				itemIds.Add(fruit.QualifiedItemId);
			}

			return itemIds;
		}
	}
}
