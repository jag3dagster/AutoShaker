using System.Collections.Generic;
using System.Linq;
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

		public ForageableItem(ISpawnItemData data, bool enabled = false)
		{
			_qualifiedItemId = data.ItemId;
			_displayName = data.ObjectDisplayName;
			_isEnabled = enabled;
		}

		public ForageableItem(ParsedItemData data)
		{
			_qualifiedItemId = data.QualifiedItemId;
			_displayName = data.DisplayName;
			_isEnabled = true;
		}

		public ForageableItem(string itemId, string displayName, bool enabled = false)
		{
			_qualifiedItemId = itemId;
			_displayName = displayName;
			_isEnabled = enabled;
		}

		public static IEnumerable<ForageableItem> Parse(Dictionary<string, FruitTreeData> data)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				foreach (var fruit in kvp.Value.Fruit)
				{
					forageItems.Add(new ForageableItem(fruit, true));
				}
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(Dictionary<string, WildTreeData> data)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				forageItems.Add(new ForageableItem(ItemRegistry.GetData(kvp.Value.SeedItemId)));

				var dropItems = kvp.Value.SeedDropItems;
				if (dropItems != null)
				{
					foreach (var seedDrop in kvp.Value.SeedDropItems)
					{
						forageItems.Add(new ForageableItem(seedDrop));
					}
				}
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(Dictionary<string, ObjectData> oData, Dictionary<string, LocationData> lData)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in lData)
			{
				var artifactSpots = kvp.Value.ArtifactSpots;
				if (artifactSpots == null || artifactSpots.Count <= 0) continue;

				foreach (var artifact in artifactSpots)
				{
					List<string> itemIds = null;
					if (artifact.ItemId == null && artifact.RandomItemId != null)
					{
						itemIds = artifact.RandomItemId;
					}
					else if (artifact.ItemId != null)
					{
						itemIds = new() { artifact.ItemId };
					}

					foreach (var itemId in itemIds)
					{
						var artifactId = itemId.Substring(itemId.IndexOf(')') + 1);
						if (!oData.ContainsKey(artifactId)) continue;

						var objData = oData[artifactId];
						if (objData?.ContextTags == null) continue;

						if (objData.ContextTags.Contains("forage_item"))
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
