using System;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

using Constants = AutoShaker.Helpers.Constants;
using Forageable = AutoShaker.Helpers.Constants.Forageable;

namespace AutoShaker
{
	internal class ModConfig
	{
		private const int MinFruitsReady = 1;
		private const int MaxFruitsReady = 3;

		private int _fruitsReadyToShake;
		public uint ForageableToggles;

		#region General Properties

		public bool IsShakerActive { get; set; }
		public KeybindList ToggleShaker { get; set; } = new KeybindList();
		public bool UsePlayerMagnetism { get; set; }
		public int ShakeDistance { get; set; }

		public bool AnyShakeEnabled { get; set; }

		#endregion General Properties

		#region Seed Tree Properties

		public bool ShakeMahoganyTrees { get; set; }
		public bool ShakeMapleTrees { get; set; }
		public bool ShakeOakTrees { get; set; }
		public bool ShakePalmTrees { get; set; }
		public bool ShakePineTrees { get; set; }

		public bool AnySeedTreeEnabled { get; set; }

		#endregion Seed Tree Properties

		#region Fruit Tree Properties

		public int FruitsReadyToShake
		{
			get => _fruitsReadyToShake;
			set => _fruitsReadyToShake = Math.Clamp(value, MinFruitsReady, MaxFruitsReady);
		}
		public bool ShakeAppleTrees { get; set; }
		public bool ShakeApricotTrees { get; set; }
		public bool ShakeBananaTrees { get; set; }
		public bool ShakeCherryTrees { get; set; }
		public bool ShakeMangoTrees { get; set; }
		public bool ShakeOrangeTrees { get; set; }
		public bool ShakePeachTrees { get; set; }
		public bool ShakePomegranateTrees { get; set; }

		public bool AnyFruitTreeEnabled { get; set; }

		#endregion Fruit Tree Properties

		#region Bush Properties

		public bool ShakeSalmonberriesBushes { get; set; }
		public bool ShakeBlackberriesBushes { get; set; }
		public bool ShakeTeaBushes { get; set; }
		public bool ShakeWalnutBushes { get; set; }

		public bool AnyBushEnabled { get; set; }

		#endregion Bush Properties

		#region Forageable Properties

		#region Spring Forageable Properties

		private bool _pullDaffodils;
		public bool PullDaffodils
		{
			get => _pullDaffodils;
			set 
			{
				_pullDaffodils = value;
				UpdateForageableBit(Forageable.Daffodil, value);
			}
		}

		private bool _pullDandelions;
		public bool PullDandelions
		{
			get => _pullDandelions;
			set
			{
				_pullDandelions = value;
				UpdateForageableBit(Forageable.Dandelion, value);
			}
		}

		private bool _pullLeeks;
		public bool PullLeeks
		{
			get => _pullLeeks;
			set
			{
				_pullLeeks = value;
				UpdateForageableBit(Forageable.Leek, value);
			}
		}

		public bool PullSpringOnions { get; set; }

		private bool _pullWildHorseradishes;
		public bool PullWildHorseradishes
		{
			get => _pullWildHorseradishes;
			set
			{
				_pullWildHorseradishes = value;
				UpdateForageableBit(Forageable.WildHorseradish, value);
			}
		}

		#endregion Spring Forageable Properties

		#region Summer Forageable Properties

		private bool _pullGrapes;
		public bool PullGrapes
		{
			get => _pullGrapes;
			set
			{
				_pullGrapes = value;
				UpdateForageableBit(Forageable.Grape, value);
			}
		}

		private bool _pullSpiceBerries;
		public bool PullSpiceBerries
		{
			get => _pullSpiceBerries;
			set
			{
				_pullSpiceBerries = value;
				UpdateForageableBit(Forageable.SpiceBerry, value);
			}
		}

		private bool _pullSweetPeas;
		public bool PullSweetPeas
		{
			get => _pullSweetPeas;
			set
			{
				_pullSweetPeas = value;
				UpdateForageableBit(Forageable.SweetPea, value);
			}
		}

		#endregion Summer Forageable Properties

		#region Fall Forageable Properties

		private bool _pullHazelnuts;
		public bool PullHazelnuts
		{
			get => _pullHazelnuts;
			set
			{
				_pullHazelnuts = value;
				UpdateForageableBit(Forageable.Hazelnut, value);
			}
		}

		private bool _pullWildPlums;
		public bool PullWildPlums
		{
			get => _pullWildPlums;
			set
			{
				_pullWildPlums = value;
				UpdateForageableBit(Forageable.WildPlum, value);
			}
		}

		#endregion Fall Forageable Properties

		#region Winter Forageable Properties

		private bool _pullCrocuses;
		public bool PullCrocuses
		{
			get => _pullCrocuses;
			set
			{
				_pullCrocuses = value;
				UpdateForageableBit(Forageable.Crocus, value);
			}
		}


		private bool _pullCrystalFruits;
		public bool PullCrystalFruits
		{
			get => _pullCrystalFruits;
			set
			{
				_pullCrystalFruits = value;
				UpdateForageableBit(Forageable.CrystalFruit, value);
			}
		}

		private bool _pullHolly;
		public bool PullHolly
		{
			get => _pullHolly;
			set
			{
				_pullHolly = value;
				UpdateForageableBit(Forageable.Holly, value);
			}
		}

		private bool _digSnowYams;
		public bool DigSnowYams
		{
			get => _digSnowYams;
			set
			{
				_digSnowYams = value;
				UpdateForageableBit(Forageable.SnowYam, value);
			}
		}

		private bool _digWinterRoots;
		public bool DigWinterRoots
		{
			get => _digWinterRoots;
			set
			{
				_digWinterRoots = value;
				UpdateForageableBit(Forageable.WinterRoot, value);
			}
		}

		#endregion Winter Forageable Properties

		#region Mushroom Forageable Properties

		private bool _pullChanterelles;
		public bool PullChanterelles
		{
			get => _pullChanterelles;
			set
			{
				_pullChanterelles = value;
				UpdateForageableBit(Forageable.Chanterelle, value);
			}
		}

		private bool _pullCommonMushrooms;
		public bool PullCommonMushrooms
		{
			get => _pullCommonMushrooms;
			set
			{
				_pullCommonMushrooms = value;
				UpdateForageableBit(Forageable.CommonMushroom, value);
			}
		}

		private bool _pullMagmaCaps;
		public bool PullMagmaCaps
		{
			get => _pullMagmaCaps;
			set
			{
				_pullMagmaCaps = value;
				UpdateForageableBit(Forageable.MagmaCap, value);
			}
		}

		private bool _pullMorels;
		public bool PullMorels
		{
			get => _pullMorels;
			set
			{
				_pullMorels = value;
				UpdateForageableBit(Forageable.Morel, value);
			}
		}

		private bool _pullPurpleMushrooms;
		public bool PullPurpleMushrooms
		{
			get => _pullPurpleMushrooms;
			set
			{
				_pullPurpleMushrooms = value;
				UpdateForageableBit(Forageable.PurpleMushroom, value);
			}
		}

		private bool _pullRedMushrooms;
		public bool PullRedMushrooms
		{
			get => _pullRedMushrooms;
			set
			{
				_pullRedMushrooms = value;
				UpdateForageableBit(Forageable.RedMushroom, value);
			}
		}

		#endregion Mushroom Forageable Properties

		#region Beach Forageable Properties

		private bool _pullClams;
		public bool PullClams
		{
			get => _pullClams;
			set
			{
				_pullClams = value;
				UpdateForageableBit(Forageable.Clam, value);
			}
		}

		private bool _pullCockles;
		public bool PullCockles
		{
			get => _pullCockles;
			set
			{
				_pullCockles = value;
				UpdateForageableBit(Forageable.Cockle, value);
			}
		}

		private bool _pullCoral;
		public bool PullCoral
		{
			get => _pullCoral;
			set
			{
				_pullCoral = value;
				UpdateForageableBit(Forageable.Coral, value);
			}
		}

		private bool _pullMussels;
		public bool PullMussels
		{
			get => _pullMussels;
			set
			{
				_pullMussels = value;
				UpdateForageableBit(Forageable.Mussel, value);
			}
		}

		private bool _pullNautilusShells;
		public bool PullNautilusShells
		{
			get => _pullNautilusShells;
			set
			{
				_pullNautilusShells = value;
				UpdateForageableBit(Forageable.NautilusShell, value);
			}
		}

		private bool _pullOysters;
		public bool PullOysters
		{
			get => _pullOysters;
			set
			{
				_pullOysters = value;
				UpdateForageableBit(Forageable.Oyster, value);
			}
		}

		private bool _pullRainbowShells;
		public bool PullRainbowShells
		{
			get => _pullRainbowShells;
			set
			{
				_pullRainbowShells = value;
				UpdateForageableBit(Forageable.RainbowShell, value);
			}
		}

		private bool _pullSeaUrchins;
		public bool PullSeaUrchins
		{
			get => _pullSeaUrchins;
			set
			{
				_pullSeaUrchins = value;
				UpdateForageableBit(Forageable.SeaUrchin, value);
			}
		}

		private bool _pullSeaweed;
		public bool PullSeaweed
		{
			get => _pullSeaweed;
			set
			{
				_pullSeaweed = value;
				UpdateForageableBit(Forageable.Seaweed, value);
			}
		}

		#endregion Beach Forageable Properties

		#region Cave Forageable Properties

		private bool _pullFiddleheadFerns;
		public bool PullFiddleheadFerns
		{
			get => _pullFiddleheadFerns;
			set
			{
				_pullFiddleheadFerns = value;
				UpdateForageableBit(Forageable.FiddleheadFern, value);
			}
		}

		#endregion Cave Forageable Properties

		#region Desert Forageable Properties

		private bool _pullCactusFruits;
		public bool PullCactusFruits
		{
			get => _pullCactusFruits;
			set
			{
				_pullCactusFruits = value;
				UpdateForageableBit(Forageable.CactusFruit, value);
			}
		}

		private bool _harvestCoconuts;
		public bool HarvestCoconuts
		{
			get => _harvestCoconuts;
			set
			{
				_harvestCoconuts = value;
				UpdateForageableBit(Forageable.Coconut, value);
			}
		}

		#endregion Desert Forageable Properties

		#region Island Forageable Properties

		public bool DigGinger { get; set; }

		#endregion Island Forageable Properties

		public bool AnyForageablesEnabled { get; set; }

		#endregion Forageable Properties

		public void ResetToDefault()
		{
			IsShakerActive = true;
			ToggleShaker = new KeybindList(
				new Keybind(SButton.LeftAlt, SButton.H),
				new Keybind(SButton.RightAlt, SButton.H));

			UsePlayerMagnetism = false;
			ShakeDistance = 2;

			AnyShakeEnabled = true;

			// Seed Trees
			ShakeMahoganyTrees = true;
			ShakeMapleTrees = true;
			ShakeOakTrees = true;
			ShakePalmTrees = true;
			ShakePineTrees = true;

			AnySeedTreeEnabled = true;

			// Fruit Trees
			FruitsReadyToShake = MinFruitsReady;
			ShakeAppleTrees = true;
			ShakeApricotTrees = true;
			ShakeBananaTrees = true;
			ShakeCherryTrees = true;
			ShakeMangoTrees = true;
			ShakeOrangeTrees = true;
			ShakePeachTrees = true;
			ShakePomegranateTrees = true;

			AnyFruitTreeEnabled = true;

			// Bushes
			ShakeSalmonberriesBushes = true;
			ShakeBlackberriesBushes = true;
			ShakeTeaBushes = true;
			ShakeWalnutBushes = false;

			AnyBushEnabled = true;

			// Forageables
			// Spring
			PullDaffodils = false;
			PullDandelions = false;
			PullLeeks = false;
			PullSpringOnions = false;
			PullWildHorseradishes = false;

			// Summer
			PullGrapes = false;
			PullSpiceBerries = false;
			PullSweetPeas = false;

			// Fall
			PullHazelnuts = false;
			PullWildPlums = false;

			// Winter
			PullCrocuses = false;
			PullCrystalFruits = false;
			PullHolly = false;
			DigSnowYams = false;
			DigWinterRoots = false;

			// Mushrooms
			PullChanterelles = false;
			PullCommonMushrooms = false;
			PullMagmaCaps = false;
			PullMorels = false;
			PullPurpleMushrooms = false;
			PullRedMushrooms = false;

			// Beach
			PullClams = false;
			PullCockles = false;
			PullCoral = false;
			PullMussels = false;
			PullNautilusShells = false;
			PullOysters = false;
			PullRainbowShells = false;
			PullSeaUrchins = false;
			PullSeaweed = false;

			// Caves
			PullFiddleheadFerns = false;

			// Desert
			PullCactusFruits = false;

			// Island
			DigGinger = false;

			AnyForageablesEnabled = false;
			ForageableToggles = 0;
		}

		public ModConfig()
		{
			ResetToDefault();
		}

		public void RegisterModConfigMenu(IModHelper helper, IManifest manifest)
		{
			if (!helper.ModRegistry.IsLoaded("spacechase0.GenericModConfigMenu")) return;

			var gmcmApi = helper.ModRegistry.GetApi<IGenericModConfigMenu>("spacechase0.GenericModConfigMenu");
			if (gmcmApi == null) return;

			gmcmApi.Register(manifest, ResetToDefault, () => helper.WriteConfig(this));

			/* General */

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_General_Text);

			// IsShakerActive
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.IsShakerActive",
				name: I18n.Option_IsShakerActive_Name,
				tooltip: I18n.Option_IsShakerActive_Tooltip,
				getValue: () => IsShakerActive,
				setValue: val => IsShakerActive = val);

			// ToggleShaker
			gmcmApi.AddKeybindList(
				mod: manifest,
				fieldId: "AutoShaker.ToggleShaker",
				name: I18n.Option_ToggleShaker_Name,
				tooltip: I18n.Option_ToggleShaker_Tooltip,
				getValue: () => ToggleShaker,
				setValue: val => ToggleShaker = val);

			// UsePlayerMagnetism
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.UsePlayerMagnetism",
				name: I18n.Option_UsePlayerMagnetism_Name,
				tooltip: () => I18n.Option_UsePlayerMagnetism_Tooltip(I18n.Option_ShakeDistance_Name()),
				getValue: () => UsePlayerMagnetism,
				setValue: val => UsePlayerMagnetism = val);

			// ShakeDistance
			gmcmApi.AddNumberOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeDistance",
				name: I18n.Option_ShakeDistance_Name,
				tooltip: () => I18n.Option_ShakeDistance_Tooltip(I18n.Option_UsePlayerMagnetism_Name()),
				getValue: () => ShakeDistance,
				setValue: val => ShakeDistance = val);

			/* Page Links Section */

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.SeedTreesPage",
				text: I18n.Link_SeedTrees_Text);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.FruitTreesPage",
				text: I18n.Link_FruitTrees_Text);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.BushesPage",
				text: I18n.Link_Bushes_Text);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.ForageablesPage",
				text: I18n.Link_Forageables_Text);

			/* Seed Trees */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.SeedTreesPage",
				pageTitle: I18n.Page_SeedTrees_Title);

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_SeedTree_Text);

			// ShakeMahoganyTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeMahoganyTrees",
				name: () => Constants.MahoganyName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_MahoganyTrees(),
					I18n.Reward_MahoganySeeds()),
				getValue: () => ShakeMahoganyTrees,
				setValue: val =>
				{
					ShakeMahoganyTrees = val;
					UpdateEnabled();
				}); ;

			// ShakeMapleTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeMapleTrees",
				name: () => Constants.MapleName,
				tooltip:  () => I18n.Option_ToggleAction_Description_Reward_Note(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_MapleTrees(),
					I18n.Reward_MapleSeedsHazelnuts(),
					I18n.Note_ShakeMapleTrees()),
				getValue: () => ShakeMapleTrees,
				setValue: val =>
				{
					ShakeMapleTrees = val;
					UpdateEnabled();
				});

			// ShakeOakTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeOakTrees",
				name: () => Constants.OakName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_OakTrees(),
					I18n.Reward_Acorns()),
				getValue: () => ShakeOakTrees,
				setValue: val =>
				{
					ShakeOakTrees = val;
					UpdateEnabled();
				});

			// ShakePalmTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakePalmTrees",
				name: () => Constants.PalmName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward_Note(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_PalmTrees(),
					I18n.Reward_Coconuts(),
					I18n.Note_ShakePalmTrees()),
				getValue: () => ShakePalmTrees,
				setValue: val =>
				{
					ShakePalmTrees = val;
					UpdateEnabled();
				});

			// ShakePineTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakePineTrees",
				name: () => Constants.PineName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_PineTrees(),
					I18n.Reward_PineCones()),
				getValue: () => ShakePineTrees,
				setValue: val =>
				{
					ShakePineTrees = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.Link_BackToMain_Text);

			/* Fruit Trees */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.FruitTreesPage",
				pageTitle: I18n.Page_FruitTrees_Title);

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_FruitTrees_Text);

			// FruitsReadyToShake
			gmcmApi.AddNumberOption(
				mod: manifest,
				fieldId: "AutoShaker.FruitsReadyToShake",
				name: I18n.Option_FruitsReadyToShake_Name,
				tooltip: I18n.Option_FruitsReadyToShake_Tooltip,
				getValue: () => FruitsReadyToShake,
				setValue: val => FruitsReadyToShake = val,
				min: MinFruitsReady,
				max: MaxFruitsReady);

			// ShakeAppleTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeAppleTrees",
				name: () => Constants.AppleName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_AppleTrees(),
					I18n.Reward_Apples()),
				getValue: () => ShakeAppleTrees,
				setValue: val =>
				{
					ShakeAppleTrees = val;
					UpdateEnabled();
				});

			// ShakeApricotTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeApricotTrees",
				name: () => Constants.ApricotName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_ApricotTrees(),
					I18n.Reward_Apricots()),
				getValue: () => ShakeApricotTrees,
				setValue: val =>
				{
					ShakeApricotTrees = val;
					UpdateEnabled();
				});

			// ShakeBananaTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeBananaTrees",
				name: () => Constants.BananaName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_BananaTrees(),
					I18n.Reward_Bananas()),
				getValue: () => ShakeBananaTrees,
				setValue: val =>
				{
					ShakeBananaTrees = val;
					UpdateEnabled();
				});

			// ShakeCherryTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeCherryTrees",
				name: () => Constants.CherryName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_CherryTrees(),
					I18n.Reward_Cherries()),
				getValue: () => ShakeCherryTrees,
				setValue: val =>
				{
					ShakeCherryTrees = val;
					UpdateEnabled();
				});

			// ShakeMangoTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeMangoTrees",
				name: () => Constants.MangoName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_MangoTrees(),
					I18n.Reward_Mangos()),
				getValue: () => ShakeMangoTrees,
				setValue: val =>
				{
					ShakeMangoTrees = val;
					UpdateEnabled();
				});

			// ShakeOrangeTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeOrangeTrees",
				name: () => Constants.OrangeName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_OrangeTrees(),
					I18n.Reward_Oranges()),
				getValue: () => ShakeOrangeTrees,
				setValue: val =>
				{
					ShakeOrangeTrees = val;
					UpdateEnabled();
				});

			// ShakePeachTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakePeachTrees",
				name: () => Constants.PeachName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_PeachTrees(),
					I18n.Reward_Peaches()),
				getValue: () => ShakePeachTrees,
				setValue: val =>
				{
					ShakePeachTrees = val;
					UpdateEnabled();
				});

			// ShakePomegranateTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakePomegranateTrees",
				name: () => Constants.PomegranateName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_PomegranateTrees(),
					I18n.Reward_Pomegranates()),
				getValue: () => ShakePomegranateTrees,
				setValue: val =>
				{
					ShakePomegranateTrees = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.Link_BackToMain_Text);

			/* Bushes */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.BushesPage",
				pageTitle: I18n.Page_Bushes_Title);

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Bushes_Text);

			// ShakeSalmonberries
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeSalmonberries",
				name: () => Constants.SalmonberryName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_SalmonberryBushes(),
					I18n.Reward_Salmonberries()),
				getValue: () => ShakeSalmonberriesBushes,
				setValue: val =>
				{
					ShakeSalmonberriesBushes = val;
					UpdateEnabled();
				});

			// ShakeBlackberries
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeBlackberries",
				name: () => Constants.BlackberryName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_BlackberryBushes(),
					I18n.Reward_Blackberries()),
				getValue: () => ShakeBlackberriesBushes,
				setValue: val =>
				{
					ShakeBlackberriesBushes = val;
					UpdateEnabled();
				});

			// ShakeTeaBushes
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeTeaBushes",
				name: () => Constants.TeaName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_TeaBushes(),
					I18n.Reward_TeaLeaves()),
				getValue: () => ShakeTeaBushes,
				setValue: val =>
				{
					ShakeTeaBushes = val;
					UpdateEnabled();
				});

			// ShakeWalnutBushes
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeWalnutBushes",
				name: () => Constants.WalnutName,
				tooltip: () => I18n.Option_ToggleAction_Description_Reward_Note(
					I18n.Action_Shake_Future().ToLowerInvariant(),
					I18n.Subject_WalnutBushes(),
					I18n.Reward_GoldenWalnuts(),
					I18n.Note_ShakeWalnutBushes()),
				getValue: () => ShakeWalnutBushes,
				setValue: val =>
				{
					ShakeWalnutBushes = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.Link_BackToMain_Text);

			/* Forageables */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.ForageablesPage",
				pageTitle: I18n.Page_Forageables_Title);

			/* Spring */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Spring_Text);

			// PullDaffodils
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageDaffodils",
				name: () => Constants.DaffodilName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Daffodils()),
				getValue: () => PullDaffodils,
				setValue: val =>
				{
					PullDaffodils = val;
					UpdateEnabled();
				});

			// PullDandelions
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageDandelions",
				name: () => Constants.DandelionName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Dandelions()),
				getValue: () => PullDandelions,
				setValue: val =>
				{
					PullDandelions = val;
					UpdateEnabled();
				});

			// PullLeeks
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageLeeks",
				name: () => Constants.LeekName,
				tooltip:  () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Leeks()),
				getValue: () => PullLeeks,
				setValue: val =>
				{
					PullLeeks = val;
					UpdateEnabled();
				});

			// PullSpringOnions
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageSpringOnions",
				name: () => Constants.SpringOnionName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SpringOnions()),
				getValue: () => PullSpringOnions,
				setValue: val =>
				{
					PullSpringOnions = val;
					UpdateEnabled();
				});

			// PullWildHorseradishes
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageWildHorseradishes",
				name: () => Constants.WildHorseradishName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_WildHorseradishes()),
				getValue: () => PullWildHorseradishes,
				setValue: val =>
				{
					PullWildHorseradishes = val;
					UpdateEnabled();
				});

			/* Summer */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Summer_Text);

			// PullGrapes
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageGrapes",
				name: () => Constants.GrapeName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Grapes()),
				getValue: () => PullGrapes,
				setValue: val =>
				{
					PullGrapes = val;
					UpdateEnabled();
				});

			// PullSpiceBerries
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageSpiceBerries",
				name: () => Constants.SpiceBerryName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SpiceBerries()),
				getValue: () => PullSpiceBerries,
				setValue: val =>
				{
					PullSpiceBerries = val;
					UpdateEnabled();
				});

			// PullSweetPeas
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageSweetPeas",
				name: () => Constants.SweetPeaName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SweetPeas()),
				getValue: () => PullSweetPeas,
				setValue: val =>
				{
					PullSweetPeas = val;
					UpdateEnabled();
				});

			/* Fall */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Fall_Text);

			// PullHazelnuts
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageHazelnuts",
				name: () => Constants.HazelnutName,
				tooltip: () => I18n.Option_ToggleAction_Description_Note(
					I18n.Action_Forage_Future().ToLowerInvariant(),
					I18n.Subject_Hazelnuts(),
					I18n.Note_ForageHazelnuts()),
				getValue: () => PullHazelnuts,
				setValue: val =>
				{
					PullHazelnuts = val;
					UpdateEnabled();
				});

			// PullWildPlums
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageWildPlums",
				name: () => Constants.WildPlumName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_WildPlums()),
				getValue: () => PullWildPlums,
				setValue: val =>
				{
					PullWildPlums = val;
					UpdateEnabled();
				});

			/* Winter */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Winter_Text);

			// PullCrocuses
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageCrocuses",
				name: () => Constants.CrocusName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Crocuses()),
				getValue: () => PullCrocuses,
				setValue: val =>
				{
					PullCrocuses = val;
					UpdateEnabled();
				});

			// PullCrystalFruits
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageCrystalFruits",
				name: () => Constants.CrystalFruitName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_CrystalFruits()),
				getValue: () => PullCrystalFruits,
				setValue: val =>
				{
					PullCrystalFruits = val;
					UpdateEnabled();
				});

			// PullHolly
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageHolly",
				name: () => Constants.HollyName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Holly()),
				getValue: () => PullHolly,
				setValue: val =>
				{
					PullHolly = val;
					UpdateEnabled();
				});

			// DigSnowYams
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageSnowYams",
				name: () => Constants.SnowYamName,
				tooltip: () => I18n.Option_ToggleAction_Description_Note(
					I18n.Action_Forage_Future().ToLowerInvariant(),
					I18n.Subject_SnowYams(),
					I18n.Note_ArtifactSpot(I18n.Subject_SnowYam())),
				getValue: () => DigSnowYams,
				setValue: val =>
				{
					DigSnowYams = val;
					UpdateEnabled();
				});

			// DigWinterRoots
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageWinterRoots",
				name: () => Constants.WinterRootName,
				tooltip: () => I18n.Option_ToggleAction_Description_Note(
					I18n.Action_Forage_Future().ToLowerInvariant(),
					I18n.Subject_WinterRoots(),
					I18n.Note_ArtifactSpot(I18n.Subject_WinterRoot())),
				getValue: () => DigWinterRoots,
				setValue: val =>
				{
					DigWinterRoots = val;
					UpdateEnabled();
				});

			/* Mushrooms */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Mushrooms_Text);

			// PullChanterelles
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageChanterelles",
				name: () => Constants.ChanterelleName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_ChanterelleMushrooms()),
				getValue: () => PullChanterelles,
				setValue: val =>
				{
					PullChanterelles = val;
					UpdateEnabled();
				});

			// PullCommonMushrooms
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageCommonMushrooms",
				name: () => Constants.CommonMushroomName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_CommonMushrooms()),
				getValue: () => PullCommonMushrooms,
				setValue: val =>
				{
					PullCommonMushrooms = val;
					UpdateEnabled();
				});

			// PullMagmaCaps
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageMagmaCaps",
				name: () => Constants.MagmaCapName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_MagmaCaps()),
				getValue: () => PullMagmaCaps,
				setValue: val =>
				{
					PullMagmaCaps = val;
					UpdateEnabled();
				});

			// PullMorels
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageMorels",
				name: () => Constants.MorelName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_MorelMushrooms()),
				getValue: () => PullMorels,
				setValue: val =>
				{
					PullMorels = val;
					UpdateEnabled();
				});

			// PullPurpleMushrooms
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForagePurpleMushrooms",
				name: () => Constants.PurpleMushroomName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_PurpleMushrooms()),
				getValue: () => PullPurpleMushrooms,
				setValue: val =>
				{
					PullPurpleMushrooms = val;
					UpdateEnabled();
				});

			// PullRedMushrooms
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageRedMushrooms",
				name: () => Constants.RedMushroomName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_RedMushrooms()),
				getValue: () => PullRedMushrooms,
				setValue: val =>
				{
					PullRedMushrooms = val;
					UpdateEnabled();
				});

			/* Beach */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Beach_Text);

			// PullClams
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageClams",
				name: () => Constants.ClamName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Clams()),
				getValue: () => PullClams,
				setValue: val =>
				{
					PullClams = val;
					UpdateEnabled();
				});

			// PullCockles
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageCockles",
				name: () => Constants.CockleName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Cockles()),
				getValue: () => PullCockles,
				setValue: val =>
				{
					PullCockles = val;
					UpdateEnabled();
				});

			// PullCoral
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageCoral",
				name: () => Constants.CoralName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Coral()),
				getValue: () => PullCoral,
				setValue: val =>
				{
					PullCoral = val;
					UpdateEnabled();
				});

			// PullMussels
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageMussels",
				name: () => Constants.MusselName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Mussels()),
				getValue: () => PullMussels,
				setValue: val =>
				{
					PullMussels = val;
					UpdateEnabled();
				});

			// PullNautilusShells
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageNautilusShells",
				name: () => Constants.NautilusShellName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_NautilusShells()),
				getValue: () => PullNautilusShells,
				setValue: val =>
				{
					PullNautilusShells = val;
					UpdateEnabled();
				});

			// PullOysters
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageOysters",
				name: () => Constants.OysterName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Oysters()),
				getValue: () => PullOysters,
				setValue: val =>
				{
					PullOysters = val;
					UpdateEnabled();
				});

			// PullRainbowShells
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageRainbowShells",
				name: () => Constants.RainbowShellName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_RainbowShells()),
				getValue: () => PullRainbowShells,
				setValue: val =>
				{
					PullRainbowShells = val;
					UpdateEnabled();
				});

			// PullSeaUrchins
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageSeaUrchins",
				name: () => Constants.SeaUrchinName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SeaUrchins()),
				getValue: () => PullSeaUrchins,
				setValue: val =>
				{
					PullSeaUrchins = val;
					UpdateEnabled();
				});

			// PullSeaweed
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageSeaweed",
				name: () => Constants.SeaweedName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Seaweed()),
				getValue: () => PullSeaweed,
				setValue: val =>
				{
					PullSeaweed = val;
					UpdateEnabled();
				});

			/* Cave */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Cave_Text);

			// PullFiddleheadFerns
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageFiddleheadFerns",
				name: () => Constants.FiddleheadFernName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_FiddleheadFerns()),
				getValue: () => PullFiddleheadFerns,
				setValue: val =>
				{
					PullFiddleheadFerns = val;
					UpdateEnabled();
				});

			/* Desert */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Desert_Text);

			// PullCactusFruits
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageCactusFruits",
				name: () => Constants.CactusFruitName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_CactusFruits()),
				getValue: () => PullCactusFruits,
				setValue: val =>
				{
					PullCactusFruits = val;
					UpdateEnabled();
				});

			// HarvestCoconuts
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageCoconuts",
				name: () => Constants.CoconutName,
				tooltip:() => I18n.Option_ToggleAction_Description_Note(
					I18n.Action_Forage_Future().ToLowerInvariant(),
					I18n.Subject_Coconuts(),
					I18n.Note_ForageCoconuts()),
				getValue: () => HarvestCoconuts,
				setValue: val =>
				{
					HarvestCoconuts = val;
					UpdateEnabled();
				});

			/* Island */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_Island_Text);

			// $TODO: Toggle to require a hoe in the inventory

			// ForageGinger
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ForageGinger",
				name: () => Constants.GingerName,
				tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_GingerRoots()),
				getValue: () => DigGinger,
				setValue: val =>
				{
					DigGinger = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.Link_BackToMain_Text);
		}

		public void UpdateEnabled()
		{
			AnySeedTreeEnabled = ShakeMahoganyTrees
				|| ShakeMapleTrees
				|| ShakeOakTrees
				|| ShakePalmTrees
				|| ShakePineTrees;

			AnyFruitTreeEnabled = ShakeAppleTrees
				|| ShakeApricotTrees
				|| ShakeBananaTrees
				|| ShakeCherryTrees
				|| ShakeMangoTrees
				|| ShakeOrangeTrees
				|| ShakePeachTrees
				|| ShakePomegranateTrees;

			AnyBushEnabled = ShakeSalmonberriesBushes
				|| ShakeBlackberriesBushes
				|| ShakeTeaBushes
				|| ShakeWalnutBushes;

			AnyForageablesEnabled = ForageableToggles > 0;

			AnyShakeEnabled = AnySeedTreeEnabled || AnyFruitTreeEnabled || AnyBushEnabled || AnyForageablesEnabled;
		}

		private void UpdateForageableBit(Forageable forageble, bool enabled)
		{
			if (enabled)
			{
				ForageableToggles |= (uint)forageble;
			}
			else
			{
				ForageableToggles &= ~(uint)forageble;
			}
		}
	}

	public interface IGenericModConfigMenu
	{
		/*********
		** Methods
		*********/

		/// <summary>Register a mod whose config can be edited through the UI.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="reset">Reset the mod's config to its default values.</param>
		/// <param name="save">Save the mod's current config to the <c>config.json</c> file.</param>
		/// <param name="titleScreenOnly">Whether the options can only be edited from the title screen.</param>
		/// <remarks>Each mod can only be registered once, unless it's deleted via <see cref="Unregister"/> before calling this again.</remarks>
		void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);

		/****
		** Basic options
		****/

		/// <summary>Add a section title at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="text">The title text shown in the form.</param>
		/// <param name="tooltip">The tooltip text shown when the cursor hovers on the title, or <c>null</c> to disable the tooltip.</param>
		void AddSectionTitle(IManifest mod, Func<string> text, Func<string>? tooltip = null);

		/// <summary>Add a boolean option at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="getValue">Get the current value from the mod config.</param>
		/// <param name="setValue">Set a new value in the mod config.</param>
		/// <param name="name">The label text to show in the form.</param>
		/// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
		/// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
		void AddBoolOption(IManifest mod, Func<bool> getValue, Action<bool> setValue, Func<string> name, Func<string>? tooltip = null, string? fieldId = null);

		/// <summary>Add an integer option at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="getValue">Get the current value from the mod config.</param>
		/// <param name="setValue">Set a new value in the mod config.</param>
		/// <param name="name">The label text to show in the form.</param>
		/// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
		/// <param name="min">The minimum allowed value, or <c>null</c> to allow any.</param>
		/// <param name="max">The maximum allowed value, or <c>null</c> to allow any.</param>
		/// <param name="interval">The interval of values that can be selected.</param>
		/// <param name="formatValue">Get the display text to show for a value, or <c>null</c> to show the number as-is.</param>
		/// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
		void AddNumberOption(IManifest mod, Func<int> getValue, Action<int> setValue, Func<string> name, Func<string>? tooltip = null, int? min = null, int? max = null, int? interval = null, Func<int, string>? formatValue = null, string? fieldId = null);


		/// <summary>Add a key binding list at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="getValue">Get the current value from the mod config.</param>
		/// <param name="setValue">Set a new value in the mod config.</param>
		/// <param name="name">The label text to show in the form.</param>
		/// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
		/// <param name="fieldId">The unique field ID for use with <see cref="OnFieldChanged"/>, or <c>null</c> to auto-generate a randomized ID.</param>
		void AddKeybindList(IManifest mod, Func<KeybindList> getValue, Action<KeybindList> setValue, Func<string> name, Func<string>? tooltip = null, string? fieldId = null);

		/****
		** Multi-page management
		****/

		/// <summary>Start a new page in the mod's config UI, or switch to that page if it already exists. All options registered after this will be part of that page.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="pageId">The unique page ID.</param>
		/// <param name="pageTitle">The page title shown in its UI, or <c>null</c> to show the <paramref name="pageId"/> value.</param>
		/// <remarks>You must also call <see cref="AddPageLink"/> to make the page accessible. This is only needed to set up a multi-page config UI. If you don't call this method, all options will be part of the mod's main config UI instead.</remarks>
		void AddPage(IManifest mod, string pageId, Func<string>? pageTitle = null);

		/// <summary>Add a link to a page added via <see cref="AddPage"/> at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="pageId">The unique ID of the page to open when the link is clicked.</param>
		/// <param name="text">The link text shown in the form.</param>
		/// <param name="tooltip">The tooltip text shown when the cursor hovers on the link, or <c>null</c> to disable the tooltip.</param>
		void AddPageLink(IManifest mod, string pageId, Func<string> text, Func<string>? tooltip = null);

		/****
		** Advanced
		****/

		/// <summary>Register a method to notify when any option registered by this mod is edited through the config UI.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="onChange">The method to call with the option's unique field ID and new value.</param>
		/// <remarks>Options use a randomized ID by default; you'll likely want to specify the <c>fieldId</c> argument when adding options if you use this.</remarks>
		void OnFieldChanged(IManifest mod, Action<string, object> onChange);

		/// <summary>Get the currently-displayed mod config menu, if any.</summary>
		/// <param name="mod">The manifest of the mod whose config menu is being shown, or <c>null</c> if not applicable.</param>
		/// <param name="page">The page ID being shown for the current config menu, or <c>null</c> if not applicable. This may be <c>null</c> even if a mod config menu is shown (e.g. because the mod doesn't have pages).</param>
		/// <returns>Returns whether a mod config menu is being shown.</returns>
		bool TryGetCurrentMenu(out IManifest mod, out string page);
	}
}
