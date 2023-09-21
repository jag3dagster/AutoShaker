using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShaker.Helpers
{
	public static class Constants
	{
		[Flags]
		public enum Forageable
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
			{ "(O)90", Forageable.CactusFruit }
		};

		//public static readonly Dictionary<string, Func<string>> ConfigNameLookup = new()
		//{
		//	// Spring
		//	{ "(O)18", I18n.PullDaffodils_Name },
		//	{ "(O)22", I18n.PullDandelions_Name },
		//	{ "(O)20", I18n.PullLeeks_Name },
		//	{ "(O)16", I18n.PullWildHorseradishes_Name },

		//	// Summer
		//	{ "(O)398", I18n.PullGrapes_Name },
		//	{ "(O)396", I18n.PullSpiceBerries_Name },
		//	{ "(O)402", I18n.PullSweetPeas_Name },

		//	// Fall
		//	{ "(O)408", I18n.PullHazelnuts_Name },
		//	{ "(O)406", I18n.PullWildPlums_Name },

		//	// Winter
		//	{ "(O)418", I18n.PullCrocuses_Name },
		//	{ "(O)414", I18n.PullCrystalFruits_Name },
		//	{ "(O)283", I18n.PullHolly_Name },
		//	{ "(O)416", I18n.DigSnowYams_Name },
		//	{ "(O)412", I18n.DigWinterRoots_Name },

		//	// Mushrooms
		//	{ "(O)281", I18n.PullChanterelles_Name },
		//	{ "(O)404", I18n.PullCommonMushrooms_Name },
		//	{ "(O)851", I18n.PullMagmaCaps_Name },
		//	{ "(O)257", I18n.PullMorels_Name },
		//	{ "(O)422", I18n.PullPurpleMushrooms_Name },
		//	{ "(O)420", I18n.PullDaffodils_Name },

		//	// Beach
		//	{ "(O)372", I18n.PullClams_Name },
		//	{ "(O)718", I18n.PullCockles_Name },
		//	{ "(O)393", I18n.PullCoral_Name },
		//	{ "(O)719", I18n.PullMussels_Name },
		//	{ "(O)392", I18n.PullNautilusShells_Name },
		//	{ "(O)723", I18n.PullOysters_Name },
		//	{ "(O)394", I18n.PullRainbowShells_Name },
		//	{ "(O)397", I18n.PullSeaUrchins_Name },
		//	{ "(O)152", I18n.PullSeaweed_Name },

		//	// Cave
		//	{ "(O)259", I18n.PullSeaweed_Name },

		//	// Desert
		//	{ "(O)90", I18n.PullCactusFruits_Name }
		//};
	}
}
