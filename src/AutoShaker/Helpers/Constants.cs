using System.Collections.Generic;

namespace AutoShaker.Helpers
{
	public static class Constants
	{
		#region Custom Field Properties

		private const string _customFieldPrefix = "Jag3Dagster.AutoShaker/";

		private const string _customFieldForageableKey = _customFieldPrefix + "Forageable";
		public static string CustomFieldForageableKey => _customFieldForageableKey;

		private const string _customFieldCategoryKey = _customFieldPrefix + "Category";
		public static string CustomFieldCategoryKey => _customFieldCategoryKey;

		#endregion Custom field Properties

		#region Asset Properties

		private const string _fruitTreeAssetName = "Data/FruitTrees";
		public static string FruitTreeAssetName => _fruitTreeAssetName;

		private const string _locationsAssetName = "Data/Locations";
		public static string LocationsAssetName => _locationsAssetName;

		private const string _objectsAssetName = "Data/Objects";
		public static string ObjectsAssetName => _objectsAssetName;

		private const string _wildTreeAssetName = "Data/WildTrees";
		public static string WildTreeAssetName => _wildTreeAssetName;

		#endregion Asset Properties

		public static readonly Dictionary<string, string> KnownCategoryLookup = new()
		{
			{ "18", "Spring" }, // Daffodil
			{ "22", "Spring" }, // Dandelion
			{ "20", "Spring" }, // Leek
			{ "16", "Spring" }, // Wild Horseradish

			{ "259", "Summer" }, // Fiddlehead Fern
			{ "398", "Summer" }, // Grapes
			{ "396", "Summer" }, // Spice Berry
			{ "402", "Summer" }, // Sweet Pea

			{ "408", "Fall" }, // Hazelnut
			{ "406", "Fall" }, // Wild Plum

			{ "418", "Winter" }, // Crocus
			{ "414", "Winter" }, // Crystal Fruit
			{ "283", "Winter" }, // Holly
			{ "416", "Winter" }, // Snow Yam
			{ "412", "Winter" }, // Winter Root

			{ "281", "Mushrooms" }, // Chanterelle
			{ "404", "Mushrooms" }, // Common Mushroom
			{ "851", "Mushrooms" }, // Magma Cap
			{ "257", "Mushrooms" }, // Morel
			{ "422", "Mushrooms" }, // Purple Mushroom
			{ "420", "Mushrooms" }, // Red Mushroom

			{ "372", "Beach" }, // Clam
			{ "718", "Beach" }, // Cockle
			{ "393", "Beach" }, // Coral
			{ "719", "Beach" }, // Mussel
			{ "392", "Beach" }, // Nautilus Shell
			{ "723", "Beach" }, // Oyster
			{ "394", "Beach" }, // Rainbow Shell
			{ "397", "Beach" }, // Sea Urchin
			{ "152", "Beach" }, // Seaweed

			{ "410", "Cave" }, // Blackberry
			{ "296", "Cave" }, // Salmonberry

			{ "90", "Desert" }, // Cactus Fruit
			{ "88", "Desert" }, // Coconut

			{ "829", "Special" }, // Ginger
			{ "399", "Special" } // Spring Onion
		};

		public static string BlackberryName => I18n.Option_ToggleAction_Name(I18n.Subject_BlackberryBushes());
		public static string SalmonberryName => I18n.Option_ToggleAction_Name(I18n.Subject_SalmonberryBushes());
		public static string TeaName => I18n.Option_ToggleAction_Name(I18n.Subject_TeaBushes());
		public static string WalnutName => I18n.Option_ToggleAction_Name(I18n.Subject_WalnutBushes());
	}
}
