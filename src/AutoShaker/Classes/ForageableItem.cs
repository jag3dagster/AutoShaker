using System.Collections.Generic;
using System.Threading;
using AutoShaker.Helpers;
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
		private readonly string _itemId;
		public string ItemId => _itemId;

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

		public ForageableItem(string itemId, string qualifiedItemId, string displayName, bool enabled = false)
		{
			_itemId = itemId;
			_qualifiedItemId = qualifiedItemId;
			_displayName = displayName;
			_isEnabled = enabled;
		}

		public ForageableItem(ParsedItemData data, bool enabled = false)
			: this(data.ItemId, data.QualifiedItemId, data.DisplayName, enabled)
		{ }

		public static IEnumerable<ForageableItem> Parse(IDictionary<string, FruitTreeData> data)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				foreach (var fruit in kvp.Value.Fruit)
				{
					if (kvp.Value.CustomFields == null || !kvp.Value.CustomFields.ContainsKey(Constants.CustomFieldKey)) continue;

					forageItems.Add(new ForageableItem(ItemRegistry.GetData(fruit.ItemId), true));
				}
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(IDictionary<string, WildTreeData> data)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				if (kvp.Value.CustomFields == null|| !kvp.Value.CustomFields.ContainsKey(Constants.CustomFieldKey)) continue;

				forageItems.Add(new ForageableItem(ItemRegistry.GetData(kvp.Value.SeedItemId), true));
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(IDictionary<string, ObjectData> data)
		{
			var forageItems = new List<ForageableItem>();

			foreach (var kvp in data)
			{
				if (kvp.Value.CustomFields == null || !kvp.Value.CustomFields.ContainsKey(Constants.CustomFieldKey)) continue;

				var qualifiedItemId = "(O)" + kvp.Key;
				forageItems.Add(new ForageableItem(ItemRegistry.GetData(qualifiedItemId), true));
			}

			return forageItems;
		}

		public static IEnumerable<ForageableItem> Parse(IDictionary<string, ObjectData> oData, IDictionary<string, LocationData> lData)
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
						if (objData == null || objData.CustomFields == null || !objData.CustomFields.ContainsKey(Constants.CustomFieldKey)) continue;
						
						
						forageItems.Add(new ForageableItem(ItemRegistry.GetData(itemId), true));
					}
				}
			}

			return forageItems;
		}
	}
}
