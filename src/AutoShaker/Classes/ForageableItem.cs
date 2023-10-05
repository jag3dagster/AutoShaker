using System.Collections.Generic;
using System.Threading;
using StardewValley;
using StardewValley.GameData;
using StardewValley.GameData.FruitTrees;
using StardewValley.GameData.Locations;
using StardewValley.GameData.Objects;
using StardewValley.GameData.WildTrees;
using StardewValley.ItemTypeDefinitions;

namespace AutoShaker.Classes
{
	public class ForageableItem
	{
		private readonly string _qualifiedItemId;
		public string QualifiedItemId => _qualifiedItemId;

		private readonly string _displayName;
		public string DisplayName => TokenParser.ParseText(_displayName);

		private bool _isEnabled;
		public bool IsEnabled
		{
			get => _isEnabled;
			set => _isEnabled = value;
		}

		public ForageableItem(string itemId, string displayName, bool enabled = false)
		{
			_qualifiedItemId = itemId;
			_displayName = displayName;
			_isEnabled = enabled;
		}

		public ForageableItem(ParsedItemData data, bool enabled = false)
			: this(data.QualifiedItemId, data.DisplayName, enabled)
		{ }

		public static IEnumerable<ForageableItem> Parse(IDictionary<string, FruitTreeData> data)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				foreach (var fruit in kvp.Value.Fruit)
				{
					forageItems.Add(new ForageableItem(ItemRegistry.GetData(fruit.ItemId)));
				}
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(Dictionary<string, WildTreeData> data)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				// Mushroom trees aren't real. They can't hurt you
				if (kvp.Key == "7") continue;

				forageItems.Add(new ForageableItem(ItemRegistry.GetData(kvp.Value.SeedItemId)));

				var dropItems = kvp.Value.SeedDropItems;
				if (dropItems != null)
				{
					foreach (var seedDrop in kvp.Value.SeedDropItems)
					{
						forageItems.Add(new ForageableItem(ItemRegistry.GetData(seedDrop.ItemId)));
					}
				}
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(IDictionary<string, ObjectData> data, List<string> overrideItemIds)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				var qualifiedItemId = "(O)" + kvp.Key;

				if (kvp.Value.ContextTags == null
					&& !overrideItemIds.Contains(qualifiedItemId))
				{
					continue;
				}

				if ((kvp.Value.ContextTags?.Contains("forage_item") ?? false)
					|| overrideItemIds.Contains(qualifiedItemId))
				{
					forageItems.Add(new ForageableItem(qualifiedItemId, kvp.Value.DisplayName, true));
				}
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(IDictionary<string, ObjectData> oData, Dictionary<string, LocationData> lData, List<string> overrideItemIds)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in lData)
			{
				var artifactSpots = kvp.Value.ArtifactSpots;
				if (artifactSpots == null || artifactSpots.Count <= 0) continue;

				foreach (var artifact in artifactSpots)
				{
					List<string> itemIds;
					if (artifact.RandomItemId != null)
					{
						itemIds = artifact.RandomItemId;
					}
					else if (artifact.ItemId != null)
					{
						itemIds = new() { artifact.ItemId };
					}
					else
					{
						continue;
					}

					foreach (var itemId in itemIds)
					{
						var artifactId = itemId.Substring(itemId.IndexOf(')') + 1);
						if (!oData.ContainsKey(artifactId)) continue;

						var objData = oData[artifactId];
						if (objData == null) continue;
						if (objData.ContextTags == null
							&& !overrideItemIds.Contains(itemId))
						{
							continue;
						}

						if ((objData.ContextTags?.Contains("forage_item") ?? false)
							|| overrideItemIds.Contains(itemId))
						{
							forageItems.Add(new ForageableItem(itemId, objData.DisplayName, true));
						}
					}
				}
			}

			return forageItems;
		}
	}
}
