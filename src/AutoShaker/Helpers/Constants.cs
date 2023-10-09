using System;
using System.Collections.Generic;

namespace AutoShaker.Helpers
{
	public static class Constants
	{
		#region Custom Field Properties

		private const string _customFieldPrefix = "Jag3Dagster.AutoShaker/";
		private static string CustomFieldPrefix => _customFieldPrefix;

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

			{ "259", "Cave" }, // Fiddlehead Fern

			{ "90", "Desert" }, // Cactus Fruit
			{ "88", "Desert" } // Coconut
		};

		[Flags]
		public enum Forageable : uint
		{
			// Spring
			Daffodil        = 0b_0000_0000_0000_0000_0000_0000_0000_0001, //          1
			Dandelion       = 0b_0000_0000_0000_0000_0000_0000_0000_0010, //          2
			Leek            = 0b_0000_0000_0000_0000_0000_0000_0000_0100, //          4
			WildHorseradish = 0b_0000_0000_0000_0000_0000_0000_0000_1000, //          8

			// Summer
			Grape           = 0b_0000_0000_0000_0000_0000_0000_0001_0000, //         16
			SpiceBerry      = 0b_0000_0000_0000_0000_0000_0000_0010_0000, //         32
			SweetPea        = 0b_0000_0000_0000_0000_0000_0000_0100_0000, //         64

			// Fall
			Hazelnut        = 0b_0000_0000_0000_0000_0000_0000_1000_0000, //        128
			WildPlum        = 0b_0000_0000_0000_0000_0000_0001_0000_0000, //        256

			// Winter
			Crocus          = 0b_0000_0000_0000_0000_0000_0010_0000_0000, //        512
			CrystalFruit    = 0b_0000_0000_0000_0000_0000_0100_0000_0000, //       1024
			Holly           = 0b_0000_0000_0000_0000_0000_1000_0000_0000, //       2048
			SnowYam         = 0b_0000_0000_0000_0000_0001_0000_0000_0000, //       4096
			WinterRoot      = 0b_0000_0000_0000_0000_0010_0000_0000_0000, //       8192

			// Mushrooms
			Chanterelle     = 0b_0000_0000_0000_0000_0100_0000_0000_0000, //      16384
			CommonMushroom  = 0b_0000_0000_0000_0000_1000_0000_0000_0000, //      32768
			MagmaCap        = 0b_0000_0000_0000_0001_0000_0000_0000_0000, //      65536
			Morel           = 0b_0000_0000_0000_0010_0000_0000_0000_0000, //     131072
			PurpleMushroom  = 0b_0000_0000_0000_0100_0000_0000_0000_0000, //     262144
			RedMushroom     = 0b_0000_0000_0000_1000_0000_0000_0000_0000, //     524288

			// Beach
			Clam            = 0b_0000_0000_0001_0000_0000_0000_0000_0000, //    1048576
			Cockle          = 0b_0000_0000_0010_0000_0000_0000_0000_0000, //    2097152
			Coral           = 0b_0000_0000_0100_0000_0000_0000_0000_0000, //    4194304
			Mussel          = 0b_0000_0000_1000_0000_0000_0000_0000_0000, //    8388608
			NautilusShell   = 0b_0000_0001_0000_0000_0000_0000_0000_0000, //   16777216
			Oyster          = 0b_0000_0010_0000_0000_0000_0000_0000_0000, //   33554432
			RainbowShell    = 0b_0000_0100_0000_0000_0000_0000_0000_0000, //   67108864
			SeaUrchin       = 0b_0000_1000_0000_0000_0000_0000_0000_0000, //  134217728
			Seaweed         = 0b_0001_0000_0000_0000_0000_0000_0000_0000, //  268435456

			// Cave
			FiddleheadFern  = 0b_0010_0000_0000_0000_0000_0000_0000_0000, //  536870912

			// Desert
			CactusFruit     = 0b_0100_0000_0000_0000_0000_0000_0000_0000, // 1073741824
			Coconut         = 0b_1000_0000_0000_0000_0000_0000_0000_0000  // 2147483648
		}

		public static readonly Dictionary<string, Forageable> ForageableLookup = new()
		{
			// Spring
			{ "(O)18", Forageable.Daffodil },
			{ "(O)22", Forageable.Dandelion },
			{ "(O)20", Forageable.Leek },
			{ "(O)16", Forageable.WildHorseradish },

			// Summer
			{ "(O)398", Forageable.Grape },
			{ "(O)396", Forageable.SpiceBerry },
			{ "(O)402", Forageable.SweetPea },

			// Fall
			{ "(O)408", Forageable.Hazelnut },
			{ "(O)406", Forageable.WildPlum },

			// Winter
			{ "(O)418", Forageable.Crocus },
			{ "(O)414", Forageable.CrystalFruit },
			{ "(O)283", Forageable.Holly },
			{ "(O)416", Forageable.SnowYam },
			{ "(O)412", Forageable.WinterRoot },

			// Mushrooms
			{ "(O)281", Forageable.Chanterelle },
			{ "(O)404", Forageable.CommonMushroom },
			{ "(O)851", Forageable.MagmaCap },
			{ "(O)257", Forageable.Morel },
			{ "(O)422", Forageable.PurpleMushroom },
			{ "(O)420", Forageable.RedMushroom },

			// Beach
			{ "(O)372", Forageable.Clam },
			{ "(O)718", Forageable.Cockle },
			{ "(O)393", Forageable.Coral },
			{ "(O)719", Forageable.Mussel },
			{ "(O)392", Forageable.NautilusShell },
			{ "(O)723", Forageable.Oyster },
			{ "(O)394", Forageable.RainbowShell },
			{ "(O)397", Forageable.SeaUrchin },
			{ "(O)152", Forageable.Seaweed },

			// Cave
			{ "(O)259", Forageable.FiddleheadFern },

			// Desert
			{ "(O)90", Forageable.CactusFruit },
			{ "(O)88", Forageable.Coconut }
		};

		//public static string AppleName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_AppleTrees());
		//public static string ApricotName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_ApricotTrees());
		//public static string BananaName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_BananaTrees());
		//public static string BlackberryName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_BlackberryBushes());
		//public static string CactusFruitName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_CactusFruits());
		//public static string ChanterelleName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_ChanterelleMushrooms());
		//public static string CherryName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_CherryTrees());
		//public static string ClamName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Clams());
		//public static string CockleName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Cockles());
		//public static string CoconutName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Coconuts());
		//public static string CommonMushroomName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_CommonMushrooms());
		//public static string CoralName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Coral());
		//public static string CrocusName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Crocuses());
		//public static string CrystalFruitName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_CrystalFruits());
		//public static string DaffodilName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Daffodils());
		//public static string DandelionName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Dandelions());
		//public static string FiddleheadFernName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_FiddleheadFerns());
		//public static string GingerName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_GingerRoots());
		//public static string GrapeName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Grapes());
		//public static string HazelnutName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Hazelnuts());
		//public static string HollyName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Holly());
		//public static string LeekName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Leeks());
		//public static string MagmaCapName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_MagmaCaps());
		//public static string MahoganyName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_MahoganyTrees());
		//public static string MangoName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_MangoTrees());
		//public static string MapleName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_MapleTrees());
		//public static string MorelName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_MorelMushrooms());
		//public static string MusselName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Mussels());
		//public static string NautilusShellName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_NautilusShells());
		//public static string OakName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_OakTrees());
		//public static string OrangeName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_OrangeTrees());
		//public static string OysterName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Oysters());
		//public static string PalmName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_PalmTrees());
		//public static string PeachName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_PeachTrees());
		//public static string PineName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_PineTrees());
		//public static string PomegranateName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_PomegranateTrees());
		//public static string PurpleMushroomName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_PurpleMushrooms());
		//public static string RainbowShellName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_RainbowShells());
		//public static string RedMushroomName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_RedMushrooms());
		//public static string SalmonberryName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_SalmonberryBushes());
		//public static string SeaUrchinName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_SeaUrchins());
		//public static string SeaweedName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_Seaweed());
		//public static string SnowYamName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_SnowYams());
		//public static string SpiceBerryName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_SpiceBerries());
		//public static string SpringOnionName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_SpringOnions());
		//public static string SweetPeaName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_SweetPeas());
		//public static string TeaName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_TeaBushes());
		//public static string WalnutName => I18n.Option_ToggleAction_Name(I18n.Action_Shake_Future(), I18n.Subject_WalnutBushes());
		//public static string WildHorseradishName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_WildHorseradishes());
		//public static string WildPlumName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_WildPlums());
		//public static string WinterRootName => I18n.Option_ToggleAction_Name(I18n.Action_Forage_Future(), I18n.Subject_WinterRoots());

		public static readonly Dictionary<string, string> ConfigNameLookup = new()
		{
			// Spring
			//{ "(O)18", DaffodilName },
			//{ "(O)22", DandelionName },
			//{ "(O)20", LeekName },
			//{ "(O)16", WildHorseradishName},

			//// Summer
			//{ "(O)398", GrapeName },
			//{ "(O)396", SpiceBerryName },
			//{ "(O)402", SweetPeaName },

			//// Fall
			//{ "(O)408", HazelnutName },
			//{ "(O)406", WildPlumName },

			//// Winter
			//{ "(O)418", CrocusName },
			//{ "(O)414", CrystalFruitName },
			//{ "(O)283", HollyName },
			//{ "(O)416", SnowYamName },
			//{ "(O)412", WinterRootName },

			//// Mushrooms
			//{ "(O)281", ChanterelleName },
			//{ "(O)404", CommonMushroomName },
			//{ "(O)851", MagmaCapName },
			//{ "(O)257", MorelName },
			//{ "(O)422", PurpleMushroomName },
			//{ "(O)420", RedMushroomName },

			//// Beach
			//{ "(O)372", ClamName },
			//{ "(O)718", CockleName },
			//{ "(O)393", CoralName },
			//{ "(O)719", MusselName },
			//{ "(O)392", NautilusShellName },
			//{ "(O)723", OysterName },
			//{ "(O)394", RainbowShellName },
			//{ "(O)397", SeaUrchinName },
			//{ "(O)152", SeaweedName },

			//// Cave
			//{ "(O)259", FiddleheadFernName },

			//// Desert
			//{ "(O)90", CactusFruitName },
			//{ "(O)88", CoconutName }
		};

		public static readonly Dictionary<string, string> SubjectNameLookup = new()
		{
			// Spring
			{ "(O)18", I18n.Subject_Daffodils() },
			{ "(O)22", I18n.Subject_Dandelions() },
			{ "(O)20", I18n.Subject_Leeks() },
			{ "(O)16", I18n.Subject_WildHorseradishes() },

			// Summer
			{ "(O)398", I18n.Subject_Grapes() },
			{ "(O)396", I18n.Subject_SpiceBerries() },
			{ "(O)402", I18n.Subject_SweetPeas() },

			// Fall
			{ "(O)408", I18n.Subject_Hazelnuts() },
			{ "(O)406", I18n.Subject_WildPlums() },

			// Winter
			{ "(O)418", I18n.Subject_Crocuses() },
			{ "(O)414", I18n.Subject_CrystalFruits() },
			{ "(O)283", I18n.Subject_Holly() },
			{ "(O)416", I18n.Subject_SnowYams() },
			{ "(O)412", I18n.Subject_WinterRoots() },

			// Mushrooms
			{ "(O)281", I18n.Subject_ChanterelleMushrooms() },
			{ "(O)404", I18n.Subject_CommonMushrooms() },
			{ "(O)851", I18n.Subject_MagmaCaps() },
			{ "(O)257", I18n.Subject_MorelMushrooms() },
			{ "(O)422", I18n.Subject_PurpleMushrooms() },
			{ "(O)420", I18n.Subject_RedMushrooms() },

			// Beach
			{ "(O)372", I18n.Subject_Clams() },
			{ "(O)718", I18n.Subject_Cockles() },
			{ "(O)393", I18n.Subject_Coral() },
			{ "(O)719", I18n.Subject_Mussels() },
			{ "(O)392", I18n.Subject_NautilusShells() },
			{ "(O)723", I18n.Subject_Oysters() },
			{ "(O)394", I18n.Subject_RainbowShells() },
			{ "(O)397", I18n.Subject_SeaUrchins() },
			{ "(O)152", I18n.Subject_Seaweed() },

			// Cave
			{ "(O)259", I18n.Subject_FiddleheadFerns() },

			// Desert
			{ "(O)90", I18n.Subject_CactusFruits() },
			{ "(O)88", I18n.Subject_Coconuts() }
		};
	}
}
