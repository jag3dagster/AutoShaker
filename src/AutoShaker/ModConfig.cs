using System;
using System.Collections.Generic;
using System.Linq;
using AutoShaker.Classes;
using AutoShaker.Helpers;
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

		private bool _isRegistered = false;

		#region General Properties

		public bool IsShakerActive { get; set; }
		public KeybindList ToggleShakerKeybind { get; set; } = new KeybindList();
		public bool UsePlayerMagnetism { get; set; }
		public int ShakeDistance { get; set; }
		public bool RequireHoe { get; set; }

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

		public bool ShakeSalmonberryBushes { get; set; }
		public bool ShakeBlackberryBushes { get; set; }
		public bool ShakeTeaBushes { get; set; }
		public bool ShakeWalnutBushes { get; set; }

		public bool AnyBushEnabled { get; set; }

		#endregion Bush Properties

		#region Forageable Properties

		#region Spring Forageable Properties

		private bool _forageDaffodils;
		public bool ForageDaffodils
		{
			get => _forageDaffodils;
			set 
			{
				_forageDaffodils = value;
				UpdateForageableBit(Forageable.Daffodil, value);
			}
		}

		private bool _forageDandelions;
		public bool ForageDandelions
		{
			get => _forageDandelions;
			set
			{
				_forageDandelions = value;
				UpdateForageableBit(Forageable.Dandelion, value);
			}
		}

		private bool _forageLeeks;
		public bool ForageLeeks
		{
			get => _forageLeeks;
			set
			{
				_forageLeeks = value;
				UpdateForageableBit(Forageable.Leek, value);
			}
		}

		public bool ForageSpringOnions { get; set; }

		private bool _forageWildHorseradishes;
		public bool ForageWildHorseradishes
		{
			get => _forageWildHorseradishes;
			set
			{
				_forageWildHorseradishes = value;
				UpdateForageableBit(Forageable.WildHorseradish, value);
			}
		}

		#endregion Spring Forageable Properties

		#region Summer Forageable Properties

		private bool _forageGrapes;
		public bool ForageGrapes
		{
			get => _forageGrapes;
			set
			{
				_forageGrapes = value;
				UpdateForageableBit(Forageable.Grape, value);
			}
		}

		private bool _forageSpiceBerries;
		public bool ForageSpiceBerries
		{
			get => _forageSpiceBerries;
			set
			{
				_forageSpiceBerries = value;
				UpdateForageableBit(Forageable.SpiceBerry, value);
			}
		}

		private bool _forageSweetPeas;
		public bool ForageSweetPeas
		{
			get => _forageSweetPeas;
			set
			{
				_forageSweetPeas = value;
				UpdateForageableBit(Forageable.SweetPea, value);
			}
		}

		#endregion Summer Forageable Properties

		#region Fall Forageable Properties

		private bool _forageHazelnuts;
		public bool ForageHazelnuts
		{
			get => _forageHazelnuts;
			set
			{
				_forageHazelnuts = value;
				UpdateForageableBit(Forageable.Hazelnut, value);
			}
		}

		private bool _forageWildPlums;
		public bool ForageWildPlums
		{
			get => _forageWildPlums;
			set
			{
				_forageWildPlums = value;
				UpdateForageableBit(Forageable.WildPlum, value);
			}
		}

		#endregion Fall Forageable Properties

		#region Winter Forageable Properties

		private bool _forageCrocuses;
		public bool ForageCrocuses
		{
			get => _forageCrocuses;
			set
			{
				_forageCrocuses = value;
				UpdateForageableBit(Forageable.Crocus, value);
			}
		}


		private bool _forageCrystalFruits;
		public bool ForageCrystalFruits
		{
			get => _forageCrystalFruits;
			set
			{
				_forageCrystalFruits = value;
				UpdateForageableBit(Forageable.CrystalFruit, value);
			}
		}

		private bool _forageHolly;
		public bool ForageHolly
		{
			get => _forageHolly;
			set
			{
				_forageHolly = value;
				UpdateForageableBit(Forageable.Holly, value);
			}
		}

		private bool _forageSnowYams;
		public bool ForageSnowYams
		{
			get => _forageSnowYams;
			set
			{
				_forageSnowYams = value;
				UpdateForageableBit(Forageable.SnowYam, value);
			}
		}

		private bool _forageWinterRoots;
		public bool ForageWinterRoots
		{
			get => _forageWinterRoots;
			set
			{
				_forageWinterRoots = value;
				UpdateForageableBit(Forageable.WinterRoot, value);
			}
		}

		#endregion Winter Forageable Properties

		#region Mushroom Forageable Properties

		private bool _forageChanterelles;
		public bool ForageChanterelles
		{
			get => _forageChanterelles;
			set
			{
				_forageChanterelles = value;
				UpdateForageableBit(Forageable.Chanterelle, value);
			}
		}

		private bool _forageCommonMushrooms;
		public bool ForageCommonMushrooms
		{
			get => _forageCommonMushrooms;
			set
			{
				_forageCommonMushrooms = value;
				UpdateForageableBit(Forageable.CommonMushroom, value);
			}
		}

		private bool _forageMagmaCaps;
		public bool ForageMagmaCaps
		{
			get => _forageMagmaCaps;
			set
			{
				_forageMagmaCaps = value;
				UpdateForageableBit(Forageable.MagmaCap, value);
			}
		}

		private bool _forageMorels;
		public bool ForageMorels
		{
			get => _forageMorels;
			set
			{
				_forageMorels = value;
				UpdateForageableBit(Forageable.Morel, value);
			}
		}

		private bool _foragePurpleMushrooms;
		public bool ForagePurpleMushrooms
		{
			get => _foragePurpleMushrooms;
			set
			{
				_foragePurpleMushrooms = value;
				UpdateForageableBit(Forageable.PurpleMushroom, value);
			}
		}

		private bool _forageRedMushrooms;
		public bool ForageRedMushrooms
		{
			get => _forageRedMushrooms;
			set
			{
				_forageRedMushrooms = value;
				UpdateForageableBit(Forageable.RedMushroom, value);
			}
		}

		#endregion Mushroom Forageable Properties

		#region Beach Forageable Properties

		private bool _forageClams;
		public bool ForageClams
		{
			get => _forageClams;
			set
			{
				_forageClams = value;
				UpdateForageableBit(Forageable.Clam, value);
			}
		}

		private bool _forageCockles;
		public bool ForageCockles
		{
			get => _forageCockles;
			set
			{
				_forageCockles = value;
				UpdateForageableBit(Forageable.Cockle, value);
			}
		}

		private bool _forageCoral;
		public bool ForageCoral
		{
			get => _forageCoral;
			set
			{
				_forageCoral = value;
				UpdateForageableBit(Forageable.Coral, value);
			}
		}

		private bool _forageMussels;
		public bool ForageMussels
		{
			get => _forageMussels;
			set
			{
				_forageMussels = value;
				UpdateForageableBit(Forageable.Mussel, value);
			}
		}

		private bool _forageNautilusShells;
		public bool ForageNautilusShells
		{
			get => _forageNautilusShells;
			set
			{
				_forageNautilusShells = value;
				UpdateForageableBit(Forageable.NautilusShell, value);
			}
		}

		private bool _forageOysters;
		public bool ForageOysters
		{
			get => _forageOysters;
			set
			{
				_forageOysters = value;
				UpdateForageableBit(Forageable.Oyster, value);
			}
		}

		private bool _forageRainbowShells;
		public bool ForageRainbowShells
		{
			get => _forageRainbowShells;
			set
			{
				_forageRainbowShells = value;
				UpdateForageableBit(Forageable.RainbowShell, value);
			}
		}

		private bool _forageSeaUrchins;
		public bool ForageSeaUrchins
		{
			get => _forageSeaUrchins;
			set
			{
				_forageSeaUrchins = value;
				UpdateForageableBit(Forageable.SeaUrchin, value);
			}
		}

		private bool _forageSeaweed;
		public bool ForageSeaweed
		{
			get => _forageSeaweed;
			set
			{
				_forageSeaweed = value;
				UpdateForageableBit(Forageable.Seaweed, value);
			}
		}

		#endregion Beach Forageable Properties

		#region Cave Forageable Properties

		private bool _forageFiddleheadFerns;
		public bool ForageFiddleheadFerns
		{
			get => _forageFiddleheadFerns;
			set
			{
				_forageFiddleheadFerns = value;
				UpdateForageableBit(Forageable.FiddleheadFern, value);
			}
		}

		#endregion Cave Forageable Properties

		#region Desert Forageable Properties

		private bool _forageCactusFruits;
		public bool ForageCactusFruits
		{
			get => _forageCactusFruits;
			set
			{
				_forageCactusFruits = value;
				UpdateForageableBit(Forageable.CactusFruit, value);
			}
		}

		private bool _forageCoconuts;
		public bool ForageCoconuts
		{
			get => _forageCoconuts;
			set
			{
				_forageCoconuts = value;
				UpdateForageableBit(Forageable.Coconut, value);
			}
		}

		#endregion Desert Forageable Properties

		#region Island Forageable Properties

		public bool ForageGinger { get; set; }

		#endregion Island Forageable Properties

		public bool AnyForageablesEnabled { get; set; }

		#endregion Forageable Properties

		public void ResetToDefault()
		{
			IsShakerActive = true;
			ToggleShakerKeybind = new KeybindList(
				new Keybind(SButton.LeftAlt, SButton.H),
				new Keybind(SButton.RightAlt, SButton.H));

			UsePlayerMagnetism = false;
			ShakeDistance = 2;
			RequireHoe = true;

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
			ShakeSalmonberryBushes = true;
			ShakeBlackberryBushes = true;
			ShakeTeaBushes = true;
			ShakeWalnutBushes = false;

			AnyBushEnabled = true;

			// Forageables
			// Spring
			ForageDaffodils = false;
			ForageDandelions = false;
			ForageLeeks = false;
			ForageSpringOnions = false;
			ForageWildHorseradishes = false;

			// Summer
			ForageGrapes = false;
			ForageSpiceBerries = false;
			ForageSweetPeas = false;

			// Fall
			ForageHazelnuts = false;
			ForageWildPlums = false;

			// Winter
			ForageCrocuses = false;
			ForageCrystalFruits = false;
			ForageHolly = false;
			ForageSnowYams = false;
			ForageWinterRoots = false;

			// Mushrooms
			ForageChanterelles = false;
			ForageCommonMushrooms = false;
			ForageMagmaCaps = false;
			ForageMorels = false;
			ForagePurpleMushrooms = false;
			ForageRedMushrooms = false;

			// Beach
			ForageClams = false;
			ForageCockles = false;
			ForageCoral = false;
			ForageMussels = false;
			ForageNautilusShells = false;
			ForageOysters = false;
			ForageRainbowShells = false;
			ForageSeaUrchins = false;
			ForageSeaweed = false;

			// Caves
			ForageFiddleheadFerns = false;

			// Desert
			ForageCactusFruits = false;

			// Island
			ForageGinger = false;

			AnyForageablesEnabled = false;
			ForageableToggles = 0;
		}

		public ModConfig()
		{
			ResetToDefault();
		}

		public void RegisterModConfigMenu(
			IModHelper helper,
			IManifest manifest,
			List<ForageableItem> artifactForageables,
			List<ForageableItem> fruitTreeForageables,
			List<ForageableItem> objectForageables,
			List<ForageableItem> wildTreeForageables)
		{
			if (!helper.ModRegistry.IsLoaded("spacechase0.GenericModConfigMenu")) return;

			var gmcmApi = helper.ModRegistry.GetApi<IGenericModConfigMenu>("spacechase0.GenericModConfigMenu");
			if (gmcmApi == null) return;

			if (_isRegistered)
			{
				gmcmApi.Unregister(manifest);
			}

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
				getValue: () => ToggleShakerKeybind,
				setValue: val => ToggleShakerKeybind = val);

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

			// RequireHoe
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.RequireHoe",
				name: () => I18n.Option_RequireHoe_Name(Environment.NewLine),
				tooltip: I18n.Option_RequireHoe_Tooltip,
				getValue: () => RequireHoe,
				setValue: val => RequireHoe = val);

			/* Page Links Section */

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.WildTreesPage",
				text: I18n.Link_WildTrees_Text);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.FruitTreesPage",
				text: I18n.Link_FruitTrees_Text);

			//gmcmApi.AddPageLink(
			//	mod: manifest,
			//	pageId: "AutoShaker.BushesPage",
			//	text: I18n.Link_Bushes_Text);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.ForageablesPage",
				text: I18n.Link_Forageables_Text);

			/* Wild Trees */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.WildTreesPage",
				pageTitle: I18n.Page_WildTrees_Title);

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.Section_WildTree_Text);

			gmcmApi.AddParagraph(
				mod: manifest,
				text: I18n.Page_WildTrees_Description);

			foreach (var item in wildTreeForageables)
			{
				gmcmApi.AddBoolOption(
					mod: manifest,
					name: () => I18n.Option_ToggleAction_Name(item.DisplayName),
					getValue: () => item.IsEnabled,
					setValue: val =>
					{
						item.IsEnabled = val;
						// $TODO - UpdatedEnabled()
					});
			}

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

			gmcmApi.AddParagraph(
				mod: manifest,
				text: I18n.Page_WildTrees_Description);

			foreach (var item in fruitTreeForageables)
			{
				gmcmApi.AddBoolOption(
					mod: manifest,
					name: () => I18n.Option_ToggleAction_Name(item.DisplayName),
					getValue: () => item.IsEnabled,
					setValue: val =>
					{
						item.IsEnabled = val;
						// $TODO - UpdateEnabled();
					});
			}

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.Link_BackToMain_Text);

			/* Bushes */

			//gmcmApi.AddPage(
			//	mod: manifest,
			//	pageId: "AutoShaker.BushesPage",
			//	pageTitle: I18n.Page_Bushes_Title);

			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Bushes_Text);

			//// ShakeSalmonberries
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ShakeSalmonberries",
			//	name: () => Constants.SalmonberryName,
			//	tooltip: () => I18n.Option_ToggleAction_Description_Reward(
			//		I18n.Action_Shake_Future().ToLowerInvariant(),
			//		I18n.Subject_SalmonberryBushes(),
			//		I18n.Reward_Salmonberries()),
			//	getValue: () => ShakeSalmonberryBushes,
			//	setValue: val =>
			//	{
			//		ShakeSalmonberryBushes = val;
			//		UpdateEnabled();
			//	});

			//// ShakeBlackberries
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ShakeBlackberries",
			//	name: () => Constants.BlackberryName,
			//	tooltip: () => I18n.Option_ToggleAction_Description_Reward(
			//		I18n.Action_Shake_Future().ToLowerInvariant(),
			//		I18n.Subject_BlackberryBushes(),
			//		I18n.Reward_Blackberries()),
			//	getValue: () => ShakeBlackberryBushes,
			//	setValue: val =>
			//	{
			//		ShakeBlackberryBushes = val;
			//		UpdateEnabled();
			//	});

			//// ShakeTeaBushes
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ShakeTeaBushes",
			//	name: () => Constants.TeaName,
			//	tooltip: () => I18n.Option_ToggleAction_Description_Reward(
			//		I18n.Action_Shake_Future().ToLowerInvariant(),
			//		I18n.Subject_TeaBushes(),
			//		I18n.Reward_TeaLeaves()),
			//	getValue: () => ShakeTeaBushes,
			//	setValue: val =>
			//	{
			//		ShakeTeaBushes = val;
			//		UpdateEnabled();
			//	});

			//// ShakeWalnutBushes
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ShakeWalnutBushes",
			//	name: () => Constants.WalnutName,
			//	tooltip: () => I18n.Option_ToggleAction_Description_Reward_Note(
			//		I18n.Action_Shake_Future().ToLowerInvariant(),
			//		I18n.Subject_WalnutBushes(),
			//		I18n.Reward_GoldenWalnuts(),
			//		I18n.Note_ShakeWalnutBushes()),
			//	getValue: () => ShakeWalnutBushes,
			//	setValue: val =>
			//	{
			//		ShakeWalnutBushes = val;
			//		UpdateEnabled();
			//	});

			//gmcmApi.AddPageLink(
			//	mod: manifest,
			//	pageId: "",
			//	text: I18n.Link_BackToMain_Text);

			/* Forageables */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.ForageablesPage",
				pageTitle: I18n.Page_Forageables_Title);

			gmcmApi.AddParagraph(
				mod: manifest,
				text: I18n.Page_Forageables_Description);

			var groupedItems = objectForageables
				.GroupBy(f =>
				{
					if (!(f.CustomFields?.TryGetValue(Constants.CustomFieldCategoryKey, out var category) ?? false))
					{
						category = "Other";
					}

					return category;
				})
				.OrderBy(g => g.Key, new CategoryComparer())
				.Select(g => g.ToList())
				.ToList();

			foreach (var group in groupedItems)
			{
				if (!group.First().CustomFields.TryGetValue(Constants.CustomFieldCategoryKey, out var category))
				{
					category = "Other";
				}

				gmcmApi.AddSectionTitle(
					mod: manifest,
					text: () => category);

				foreach (var item in group)
				{
					gmcmApi.AddBoolOption(
						mod: manifest,
						name: () => I18n.Option_ToggleAction_Name(item.DisplayName),
						getValue: () => item.IsEnabled,
						setValue: val =>
						{
							item.IsEnabled = val;
							// $TODO - UpdatedEnabled();
						});
				}
			}

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.Link_BackToMain_Text);

			///* Spring */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Spring_Text);

			//// PullDaffodils
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageDaffodils",
			//	name: () => Constants.DaffodilName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Daffodils()),
			//	getValue: () => ForageDaffodils,
			//	setValue: val =>
			//	{
			//		ForageDaffodils = val;
			//		UpdateEnabled();
			//	});

			//// PullDandelions
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageDandelions",
			//	name: () => Constants.DandelionName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Dandelions()),
			//	getValue: () => ForageDandelions,
			//	setValue: val =>
			//	{
			//		ForageDandelions = val;
			//		UpdateEnabled();
			//	});

			//// PullLeeks
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageLeeks",
			//	name: () => Constants.LeekName,
			//	tooltip:  () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Leeks()),
			//	getValue: () => ForageLeeks,
			//	setValue: val =>
			//	{
			//		ForageLeeks = val;
			//		UpdateEnabled();
			//	});

			//// PullSpringOnions
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageSpringOnions",
			//	name: () => Constants.SpringOnionName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SpringOnions()),
			//	getValue: () => ForageSpringOnions,
			//	setValue: val =>
			//	{
			//		ForageSpringOnions = val;
			//		UpdateEnabled();
			//	});

			//// PullWildHorseradishes
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageWildHorseradishes",
			//	name: () => Constants.WildHorseradishName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_WildHorseradishes()),
			//	getValue: () => ForageWildHorseradishes,
			//	setValue: val =>
			//	{
			//		ForageWildHorseradishes = val;
			//		UpdateEnabled();
			//	});

			///* Summer */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Summer_Text);

			//// PullGrapes
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageGrapes",
			//	name: () => Constants.GrapeName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Grapes()),
			//	getValue: () => ForageGrapes,
			//	setValue: val =>
			//	{
			//		ForageGrapes = val;
			//		UpdateEnabled();
			//	});

			//// PullSpiceBerries
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageSpiceBerries",
			//	name: () => Constants.SpiceBerryName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SpiceBerries()),
			//	getValue: () => ForageSpiceBerries,
			//	setValue: val =>
			//	{
			//		ForageSpiceBerries = val;
			//		UpdateEnabled();
			//	});

			//// PullSweetPeas
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageSweetPeas",
			//	name: () => Constants.SweetPeaName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SweetPeas()),
			//	getValue: () => ForageSweetPeas,
			//	setValue: val =>
			//	{
			//		ForageSweetPeas = val;
			//		UpdateEnabled();
			//	});

			///* Fall */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Fall_Text);

			//// PullHazelnuts
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageHazelnuts",
			//	name: () => Constants.HazelnutName,
			//	tooltip: () => I18n.Option_ToggleAction_Description_Note(
			//		I18n.Action_Forage_Future().ToLowerInvariant(),
			//		I18n.Subject_Hazelnuts(),
			//		I18n.Note_ForageHazelnuts()),
			//	getValue: () => ForageHazelnuts,
			//	setValue: val =>
			//	{
			//		ForageHazelnuts = val;
			//		UpdateEnabled();
			//	});

			//// PullWildPlums
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageWildPlums",
			//	name: () => Constants.WildPlumName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_WildPlums()),
			//	getValue: () => ForageWildPlums,
			//	setValue: val =>
			//	{
			//		ForageWildPlums = val;
			//		UpdateEnabled();
			//	});

			///* Winter */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Winter_Text);

			//// PullCrocuses
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageCrocuses",
			//	name: () => Constants.CrocusName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Crocuses()),
			//	getValue: () => ForageCrocuses,
			//	setValue: val =>
			//	{
			//		ForageCrocuses = val;
			//		UpdateEnabled();
			//	});

			//// PullCrystalFruits
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageCrystalFruits",
			//	name: () => Constants.CrystalFruitName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_CrystalFruits()),
			//	getValue: () => ForageCrystalFruits,
			//	setValue: val =>
			//	{
			//		ForageCrystalFruits = val;
			//		UpdateEnabled();
			//	});

			//// PullHolly
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageHolly",
			//	name: () => Constants.HollyName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Holly()),
			//	getValue: () => ForageHolly,
			//	setValue: val =>
			//	{
			//		ForageHolly = val;
			//		UpdateEnabled();
			//	});

			//// DigSnowYams
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageSnowYams",
			//	name: () => Constants.SnowYamName,
			//	tooltip: () => I18n.Option_ToggleAction_Description_Note(
			//		I18n.Action_Forage_Future().ToLowerInvariant(),
			//		I18n.Subject_SnowYams(),
			//		I18n.Note_ArtifactSpot(I18n.Subject_SnowYam())),
			//	getValue: () => ForageSnowYams,
			//	setValue: val =>
			//	{
			//		ForageSnowYams = val;
			//		UpdateEnabled();
			//	});

			//// DigWinterRoots
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageWinterRoots",
			//	name: () => Constants.WinterRootName,
			//	tooltip: () => I18n.Option_ToggleAction_Description_Note(
			//		I18n.Action_Forage_Future().ToLowerInvariant(),
			//		I18n.Subject_WinterRoots(),
			//		I18n.Note_ArtifactSpot(I18n.Subject_WinterRoot())),
			//	getValue: () => ForageWinterRoots,
			//	setValue: val =>
			//	{
			//		ForageWinterRoots = val;
			//		UpdateEnabled();
			//	});

			///* Mushrooms */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Mushrooms_Text);

			//// PullChanterelles
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageChanterelles",
			//	name: () => Constants.ChanterelleName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_ChanterelleMushrooms()),
			//	getValue: () => ForageChanterelles,
			//	setValue: val =>
			//	{
			//		ForageChanterelles = val;
			//		UpdateEnabled();
			//	});

			//// PullCommonMushrooms
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageCommonMushrooms",
			//	name: () => Constants.CommonMushroomName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_CommonMushrooms()),
			//	getValue: () => ForageCommonMushrooms,
			//	setValue: val =>
			//	{
			//		ForageCommonMushrooms = val;
			//		UpdateEnabled();
			//	});

			//// PullMagmaCaps
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageMagmaCaps",
			//	name: () => Constants.MagmaCapName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_MagmaCaps()),
			//	getValue: () => ForageMagmaCaps,
			//	setValue: val =>
			//	{
			//		ForageMagmaCaps = val;
			//		UpdateEnabled();
			//	});

			//// PullMorels
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageMorels",
			//	name: () => Constants.MorelName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_MorelMushrooms()),
			//	getValue: () => ForageMorels,
			//	setValue: val =>
			//	{
			//		ForageMorels = val;
			//		UpdateEnabled();
			//	});

			//// PullPurpleMushrooms
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForagePurpleMushrooms",
			//	name: () => Constants.PurpleMushroomName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_PurpleMushrooms()),
			//	getValue: () => ForagePurpleMushrooms,
			//	setValue: val =>
			//	{
			//		ForagePurpleMushrooms = val;
			//		UpdateEnabled();
			//	});

			//// PullRedMushrooms
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageRedMushrooms",
			//	name: () => Constants.RedMushroomName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_RedMushrooms()),
			//	getValue: () => ForageRedMushrooms,
			//	setValue: val =>
			//	{
			//		ForageRedMushrooms = val;
			//		UpdateEnabled();
			//	});

			///* Beach */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Beach_Text);

			//// PullClams
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageClams",
			//	name: () => Constants.ClamName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Clams()),
			//	getValue: () => ForageClams,
			//	setValue: val =>
			//	{
			//		ForageClams = val;
			//		UpdateEnabled();
			//	});

			//// PullCockles
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageCockles",
			//	name: () => Constants.CockleName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Cockles()),
			//	getValue: () => ForageCockles,
			//	setValue: val =>
			//	{
			//		ForageCockles = val;
			//		UpdateEnabled();
			//	});

			//// PullCoral
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageCoral",
			//	name: () => Constants.CoralName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Coral()),
			//	getValue: () => ForageCoral,
			//	setValue: val =>
			//	{
			//		ForageCoral = val;
			//		UpdateEnabled();
			//	});

			//// PullMussels
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageMussels",
			//	name: () => Constants.MusselName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Mussels()),
			//	getValue: () => ForageMussels,
			//	setValue: val =>
			//	{
			//		ForageMussels = val;
			//		UpdateEnabled();
			//	});

			//// PullNautilusShells
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageNautilusShells",
			//	name: () => Constants.NautilusShellName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_NautilusShells()),
			//	getValue: () => ForageNautilusShells,
			//	setValue: val =>
			//	{
			//		ForageNautilusShells = val;
			//		UpdateEnabled();
			//	});

			//// PullOysters
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageOysters",
			//	name: () => Constants.OysterName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Oysters()),
			//	getValue: () => ForageOysters,
			//	setValue: val =>
			//	{
			//		ForageOysters = val;
			//		UpdateEnabled();
			//	});

			//// PullRainbowShells
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageRainbowShells",
			//	name: () => Constants.RainbowShellName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_RainbowShells()),
			//	getValue: () => ForageRainbowShells,
			//	setValue: val =>
			//	{
			//		ForageRainbowShells = val;
			//		UpdateEnabled();
			//	});

			//// PullSeaUrchins
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageSeaUrchins",
			//	name: () => Constants.SeaUrchinName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_SeaUrchins()),
			//	getValue: () => ForageSeaUrchins,
			//	setValue: val =>
			//	{
			//		ForageSeaUrchins = val;
			//		UpdateEnabled();
			//	});

			//// PullSeaweed
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageSeaweed",
			//	name: () => Constants.SeaweedName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_Seaweed()),
			//	getValue: () => ForageSeaweed,
			//	setValue: val =>
			//	{
			//		ForageSeaweed = val;
			//		UpdateEnabled();
			//	});

			///* Cave */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Cave_Text);

			//// PullFiddleheadFerns
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageFiddleheadFerns",
			//	name: () => Constants.FiddleheadFernName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_FiddleheadFerns()),
			//	getValue: () => ForageFiddleheadFerns,
			//	setValue: val =>
			//	{
			//		ForageFiddleheadFerns = val;
			//		UpdateEnabled();
			//	});

			///* Desert */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Desert_Text);

			//// PullCactusFruits
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageCactusFruits",
			//	name: () => Constants.CactusFruitName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_CactusFruits()),
			//	getValue: () => ForageCactusFruits,
			//	setValue: val =>
			//	{
			//		ForageCactusFruits = val;
			//		UpdateEnabled();
			//	});

			//// HarvestCoconuts
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageCoconuts",
			//	name: () => Constants.CoconutName,
			//	tooltip:() => I18n.Option_ToggleAction_Description_Note(
			//		I18n.Action_Forage_Future().ToLowerInvariant(),
			//		I18n.Subject_Coconuts(),
			//		I18n.Note_ForageCoconuts()),
			//	getValue: () => ForageCoconuts,
			//	setValue: val =>
			//	{
			//		ForageCoconuts = val;
			//		UpdateEnabled();
			//	});

			///* Island */
			//gmcmApi.AddSectionTitle(
			//	mod: manifest,
			//	text: I18n.Section_Island_Text);

			//// ForageGinger
			//gmcmApi.AddBoolOption(
			//	mod: manifest,
			//	fieldId: "AutoShaker.ForageGinger",
			//	name: () => Constants.GingerName,
			//	tooltip: () => I18n.Option_ToggleAction_Description(I18n.Action_Forage_Future().ToLowerInvariant(), I18n.Subject_GingerRoots()),
			//	getValue: () => ForageGinger,
			//	setValue: val =>
			//	{
			//		ForageGinger = val;
			//		UpdateEnabled();
			//	});
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

			AnyBushEnabled = ShakeSalmonberryBushes
				|| ShakeBlackberryBushes
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

		/// <summary>Add a paragraph of text at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="text">The paragraph text to display.</param>
		void AddParagraph(IManifest mod, Func<string> text);

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

		/// <summary>Remove a mod from the config UI and delete all its options and pages.</summary>
		/// <param name="mod">The mod's manifest.</param>
		void Unregister(IManifest mod);
	}
}
