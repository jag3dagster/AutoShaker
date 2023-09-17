using System;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace AutoShaker
{
	internal class ModConfig
	{
		private const int MinFruitsReady = 1;
		private const int MaxFruitsReady = 3;

		private int _fruitsReadyToShake;

		// General
		public bool IsShakerActive { get; set; }
		public KeybindList ToggleShaker { get; set; }
		public bool UsePlayerMagnetism { get; set; }
		public int ShakeDistance { get; set; }

		public bool AnyShakeEnabled { get; set; }

		// Regular Trees
		public bool ShakeMahoganyTrees { get; set; }
		public bool ShakeMapleTrees { get; set; }
		public bool ShakeOakTrees { get; set; }
		public bool ShakePalmTrees { get; set; }
		public bool ShakePineTrees { get; set; }

		public bool AnyRegularTreeEnabled { get; set; }

		// Fruit Trees
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

		// Bushes
		public bool ShakeSalmonberriesBushes { get; set; }
		public bool ShakeBlackberriesBushes { get; set; }
		public bool ShakeTeaBushes { get; set; }
		public bool ShakeWalnutBushes { get; set; }

		public bool AnyBushEnabled { get; set; }

		// Forageables
		// Spring
		public bool PullDaffodils { get; set; }
		public bool PullDandelions { get; set; }
		public bool PullLeeks { get; set; }
		public bool PullSpringOnions { get; set; }
		public bool PullWildHorseradishes { get; set; }

		// Summer
		public bool PullGrapes { get; set; }
		public bool PullSpiceBerries { get; set; }
		public bool PullSweetPeas { get; set; }

		// Fall
		public bool PullHazelnuts { get; set; }
		public bool PullWildPlums { get; set; }

		// Winter
		public bool PullCrocuses { get; set; }
		public bool PullCrystalFruits { get; set; }
		public bool PullHolly { get; set; }
		public bool DigSnowYams { get; set; }
		public bool DigWinterRoots { get; set; }

		// Mushrooms
		public bool PullChanterelles { get; set; }
		public bool PullCommonMushrooms { get; set; }
		public bool PullMagmaCaps { get; set; }
		public bool PullMorels { get; set; }
		public bool PullPurpleMushrooms { get; set; }
		public bool PullRedMushrooms { get; set; }
		
		// Beach
		public bool PullClams { get; set; }
		public bool PullCockles { get; set; }
		public bool PullCoral { get; set; }
		public bool PullMussels { get; set; }
		public bool PullNautilusShells { get; set; }
		public bool PullOysters { get; set; }
		public bool PullRainbowShells { get; set; }
		public bool PullSeaUrchins { get; set; }
		public bool PullSeaweed { get; set; }

		// Cave
		public bool PullFiddleheadFerns { get; set; }

		// Desert
		public bool PullCactusFruits { get; set; }

		// Island
		public bool DigGinger { get; set; }

		public bool AnyForageablesEnabled { get; set; }

		public void ResetToDefault()
		{
			IsShakerActive = true;
			ToggleShaker = new KeybindList(
				new Keybind(SButton.LeftAlt, SButton.H),
				new Keybind(SButton.RightAlt, SButton.H));

			UsePlayerMagnetism = false;
			ShakeDistance = 2;

			AnyShakeEnabled = true;

			// Regular Trees
			ShakeMahoganyTrees = true;
			ShakeMapleTrees = true;
			ShakeOakTrees = true;
			ShakePalmTrees = true;
			ShakePineTrees = true;

			AnyRegularTreeEnabled = true;

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
		}

		public ModConfig()
		{
			ResetToDefault();
		}

		public void RegisterModConfigMenu(IModHelper helper, IManifest manifest)
		{
			if (!helper.ModRegistry.IsLoaded("spacechase0.GenericModConfigMenu")) return;

			var gmcmApi = helper.ModRegistry.GetApi<IGenericModConfigMenu>("spacechase0.GenericModConfigMenu");

			gmcmApi.Register(manifest, ResetToDefault, () => helper.WriteConfig(this));
			//gmcmApi.OnFieldChanged(manifest, OnOptionChanged);

			/* General */

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.GeneralSection_Text);

			// IsShakerActive
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.IsShakerActive",
				name: I18n.IsShakerActive_Name,
				tooltip: I18n.IsShakerActive_Tooltip,
				getValue: () => IsShakerActive,
				setValue: val => IsShakerActive = val);

			// ToggleShaker
			gmcmApi.AddKeybindList(
				mod: manifest,
				fieldId: "AutoShaker.ToggleShaker",
				name: I18n.ToggleShaker_Name,
				tooltip: I18n.ToggleShaker_Tooltip ,
				getValue: () => ToggleShaker,
				setValue: val => ToggleShaker = val);

			// UsePlayerMagnetism
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.UsePlayerMagnetism",
				name: I18n.UsePlayerMagnetism_Name,
				tooltip: () => I18n.UsePlayerMagnetism_Tooltip(I18n.ShakeDistance_Name()),
				getValue: () => UsePlayerMagnetism,
				setValue: val => UsePlayerMagnetism = val);

			// ShakeDistance
			gmcmApi.AddNumberOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeDistance",
				name: I18n.ShakeDistance_Name,
				tooltip: () => I18n.ShakeDistance_Tooltip(I18n.UsePlayerMagnetism_Name()),
				getValue: () => ShakeDistance,
				setValue: val => ShakeDistance = val);

			/* Page Links Section */

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.RegularTreesPage",
				text: I18n.LinkSection_Link_RegularTrees);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.FruitTreesPage",
				text: I18n.LinkSection_Link_FruitTrees);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.BushesPage",
				text: I18n.LinkSection_Link_Bushes);

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "AutoShaker.ForageablesPage",
				text: I18n.LinkSection_Link_Forageables);

			/* Regular Trees */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.RegularTreesPage",
				pageTitle: I18n.RegularTreePage_Title);

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.RegularTreeSection_Text);

			// ShakeMahoganyTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeMahoganyTrees",
				name: I18n.ShakeMahoganyTrees_Name,
				tooltip: I18n.ShakeMahoganyTrees_Tooltip,
				getValue: () => ShakeMahoganyTrees,
				setValue: val =>
				{
					ShakeMahoganyTrees = val;
					UpdateEnabled();
				});

			// ShakeMapleTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeMapleTrees",
				name: I18n.ShakeMapleTrees_Name,
				tooltip: I18n.ShakeMapleTrees_Tooltip,
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
				name: I18n.ShakeOakTrees_Name,
				tooltip: I18n.ShakeOakTrees_Tooltip,
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
				name: I18n.ShakePalmTrees_Name,
				tooltip: I18n.ShakePalmTrees_Tooltip,
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
				name: I18n.ShakePineTrees_Name,
				tooltip: I18n.ShakePineTrees_Tooltip,
				getValue: () => ShakePineTrees,
				setValue: val =>
				{
					ShakePineTrees = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.LinkSection_Link_BackToMain);

			/* Fruit Trees */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.FruitTreesPage",
				pageTitle: I18n.FruitTreePage_Title);

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.FruitTreeSection_Text); ;

			// FruitsReadyToShake
			gmcmApi.AddNumberOption(
				mod: manifest,
				fieldId: "AutoShaker.FruitsReadyToShake",
				name: I18n.FruitsReadyToShake_Name,
				tooltip: I18n.FruitsReadyToShake_Tooltip,
				getValue: () => FruitsReadyToShake,
				setValue: val => FruitsReadyToShake = val,
				min: MinFruitsReady,
				max: MaxFruitsReady);

			// ShakeAppleTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeAppleTrees",
				name: I18n.ShakeAppleTrees_Name,
				tooltip: I18n.ShakeAppleTrees_Tooltip,
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
				name: I18n.ShakeApricotTrees_Name,
				tooltip: I18n.ShakeApricotTrees_Tooltip,
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
				name: I18n.ShakeBananaTrees_Name,
				tooltip: I18n.ShakeBananaTrees_Tooltip,
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
				name: I18n.ShakeCherryTrees_Name,
				tooltip: I18n.ShakeCherryTrees_Tooltip,
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
				name: I18n.ShakeMangoTrees_Name,
				tooltip: I18n.ShakeMangoTrees_Tooltip,
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
				name: I18n.ShakeOrangeTrees_Name,
				tooltip: I18n.ShakeOrangeTrees_Tooltip,
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
				name: I18n.ShakePeachTrees_Name,
				tooltip: I18n.ShakePeachTrees_Tooltip,
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
				name: I18n.ShakePomegranateTrees_Name,
				tooltip: I18n.ShakePomegranateTrees_Tooltip,
				getValue: () => ShakePomegranateTrees,
				setValue: val =>
				{
					ShakePomegranateTrees = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.LinkSection_Link_BackToMain);

			/* Bushes */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.BushesPage",
				pageTitle: I18n.BushesPage_Title);

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.BushesSection_Text);

			// ShakeSalmonberries
			gmcmApi.AddBoolOption(
				mod: manifest,
				fieldId: "AutoShaker.ShakeSalmonberries",
				name: I18n.ShakeSalmonberries_Name,
				tooltip: I18n.ShakeSalmonberries_Tooltip,
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
				name: I18n.ShakeBlackberries_Name,
				tooltip: I18n.ShakeBlackberries_Tooltip,
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
				name: I18n.ShakeTeaBushes_Name,
				tooltip: I18n.ShakeTeaBushes_Tooltip,
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
				name: I18n.ShakeWalnutBushes_Name,
				tooltip: I18n.ShakeWalnutBushes_Tooltip,
				getValue: () => ShakeWalnutBushes,
				setValue: val =>
				{
					ShakeWalnutBushes = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.LinkSection_Link_BackToMain);

			/* Forageables */

			gmcmApi.AddPage(
				mod: manifest,
				pageId: "AutoShaker.ForageablesPage",
				pageTitle: I18n.ForageablesPage_Title);

			/* Spring */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.SpringSection_Text);

			// PullDaffodils
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullDaffodils_Name,
				tooltip: I18n.PullDaffodils_Tooltip,
				getValue: () => PullDaffodils,
				setValue: val =>
				{
					PullDaffodils = val;
					UpdateEnabled();
				});

			// PullDandelions
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullDandelions_Name,
				tooltip: I18n.PullDandelions_Tooltip,
				getValue: () => PullDandelions,
				setValue: val =>
				{
					PullDandelions = val;
					UpdateEnabled();
				});

			// PullLeeks
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullLeeks_Name,
				tooltip: I18n.PullLeeks_Tooltip,
				getValue: () => PullLeeks,
				setValue: val =>
				{
					PullLeeks = val;
					UpdateEnabled();
				});

			// PullSpringOnions
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullSpringOnions_Name,
				tooltip: I18n.PullSpringOnions_Tooltip,
				getValue: () => PullSpringOnions,
				setValue: val =>
				{
					PullSpringOnions = val;
					UpdateEnabled();
				});

			// PullWildHorseradishes
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullWildHorseradishes_Name,
				tooltip: I18n.PullWildHorseradishes_Tooltip,
				getValue: () => PullWildHorseradishes,
				setValue: val =>
				{
					PullWildHorseradishes = val;
					UpdateEnabled();
				});

			/* Summer */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.SummerSection_Text);

			// PullGrapes
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullGrapes_Name,
				tooltip: I18n.PullGrapes_Tooltip,
				getValue: () => PullGrapes,
				setValue: val =>
				{
					PullGrapes = val;
					UpdateEnabled();
				});

			// PullSpiceBerries
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullSpiceBerries_Name,
				tooltip: I18n.PullSpiceBerries_Tooltip,
				getValue: () => PullSpiceBerries,
				setValue: val =>
				{
					PullSpiceBerries = val;
					UpdateEnabled();
				});

			// PullSweetPeas
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullSweetPeas_Name,
				tooltip: I18n.PullSweetPeas_Tooltip,
				getValue: () => PullSweetPeas,
				setValue: val =>
				{
					PullSweetPeas = val;
					UpdateEnabled();
				});

			/* Fall */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.FallSection_Text);

			// PullHazelnuts
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullHazelnuts_Name,
				tooltip: I18n.PullHazelnuts_Tooltip,
				getValue: () => PullHazelnuts,
				setValue: val =>
				{
					PullHazelnuts = val;
					UpdateEnabled();
				});

			// PullWildPlums
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullWildPlums_Name,
				tooltip: I18n.PullWildPlums_Tooltip,
				getValue: () => PullWildPlums,
				setValue: val =>
				{
					PullWildPlums = val;
					UpdateEnabled();
				});

			/* Winter */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.WinterSection_Text);

			// PullCrocuses
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullCrocuses_Name,
				tooltip: I18n.PullCrocuses_Tooltip,
				getValue: () => PullCrocuses,
				setValue: val =>
				{
					PullCrocuses = val;
					UpdateEnabled();
				});

			// PullCrystalFruits
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullCrystalFruits_Name,
				tooltip: I18n.PullCrystalFruits_Tooltip,
				getValue: () => PullCrystalFruits,
				setValue: val =>
				{
					PullCrystalFruits = val;
					UpdateEnabled();
				});

			// PullHolly
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullHolly_Name,
				tooltip: I18n.PullHolly_Tooltip,
				getValue: () => PullHolly,
				setValue: val =>
				{
					PullHolly = val;
					UpdateEnabled();
				});

			// DigSnowYams
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.DigSnowYams_Name,
				tooltip: I18n.DigSnowYams_Tooltip,
				getValue: () => DigSnowYams,
				setValue: val =>
				{
					DigSnowYams = val;
					UpdateEnabled();
				});

			// DigWinterRoots
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.DigWinterRoots_Name,
				tooltip: I18n.DigWinterRoots_Tooltip,
				getValue: () => DigWinterRoots,
				setValue: val =>
				{
					DigWinterRoots = val;
					UpdateEnabled();
				});

			/* Mushrooms */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.MushroomSection_Text);

			// PullChanterelles
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullChanterelles_Name,
				tooltip: I18n.PullChanterelles_Tooltip,
				getValue: () => PullChanterelles,
				setValue: val =>
				{
					PullChanterelles = val;
					UpdateEnabled();
				});

			// PullCommonMushrooms
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullCommonMushrooms_Name,
				tooltip: I18n.PullCommonMushrooms_Tooltip,
				getValue: () => PullCommonMushrooms,
				setValue: val =>
				{
					PullCommonMushrooms = val;
					UpdateEnabled();
				});

			// PullMagmaCaps
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullMagmaCaps_Name,
				tooltip: I18n.PullMagmaCaps_Tooltip,
				getValue: () => PullMagmaCaps,
				setValue: val =>
				{
					PullMagmaCaps = val;
					UpdateEnabled();
				});

			// PullMorels
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullMorels_Name,
				tooltip: I18n.PullMorels_Tooltip,
				getValue: () => PullMorels,
				setValue: val =>
				{
					PullMorels = val;
					UpdateEnabled();
				});

			// PullPurpleMushrooms
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullPurpleMushrooms_Name,
				tooltip: I18n.PullPurpleMushrooms_Tooltip,
				getValue: () => PullPurpleMushrooms,
				setValue: val =>
				{
					PullPurpleMushrooms = val;
					UpdateEnabled();
				});

			// PullRedMushrooms
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullRedMushrooms_Name,
				tooltip: I18n.PullRedMushrooms_Tooltip,
				getValue: () => PullRedMushrooms,
				setValue: val =>
				{
					PullRedMushrooms = val;
					UpdateEnabled();
				});

			/* Beach */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.BeachSection_Text);

			// PullClams
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullClams_Name,
				tooltip: I18n.PullClams_Tooltip,
				getValue: () => PullClams,
				setValue: val =>
				{
					PullClams = val;
					UpdateEnabled();
				});

			// PullCockles
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullCockles_Name,
				tooltip: I18n.PullCockles_Tooltip,
				getValue: () => PullCockles,
				setValue: val =>
				{
					PullCockles = val;
					UpdateEnabled();
				});

			// PullCoral
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullCoral_Name,
				tooltip: I18n.PullCoral_Tooltip,
				getValue: () => PullCoral,
				setValue: val =>
				{
					PullCoral = val;
					UpdateEnabled();
				});

			// PullMussels
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullMussels_Name,
				tooltip: I18n.PullMussels_Tooltip,
				getValue: () => PullMussels,
				setValue: val =>
				{
					PullMussels = val;
					UpdateEnabled();
				});

			// PullNautilusShells
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullNautilusShells_Name,
				tooltip: I18n.PullNautilusShells_Tooltip,
				getValue: () => PullNautilusShells,
				setValue: val =>
				{
					PullNautilusShells = val;
					UpdateEnabled();
				});

			// PullOysters
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullOysters_Name,
				tooltip: I18n.PullOysters_Tooltip,
				getValue: () => PullOysters,
				setValue: val =>
				{
					PullOysters = val;
					UpdateEnabled();
				});

			// PullRainbowShells
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullRainbowShells_Name,
				tooltip: I18n.PullRainbowShells_Tooltip,
				getValue: () => PullRainbowShells,
				setValue: val =>
				{
					PullRainbowShells = val;
					UpdateEnabled();
				});

			// PullSeaUrchins
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullSeaUrchins_Name,
				tooltip: I18n.PullSeaUrchins_Tooltip,
				getValue: () => PullSeaUrchins,
				setValue: val =>
				{
					PullSeaUrchins = val;
					UpdateEnabled();
				});

			// PullSeaweed
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullSeaweed_Name,
				tooltip: I18n.PullSeaweed_Tooltip,
				getValue: () => PullSeaweed,
				setValue: val =>
				{
					PullSeaweed = val;
					UpdateEnabled();
				});

			/* Cave */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.CaveSection_Text);

			// PullfiddleheadFerns
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullFiddleheadFerns_Name,
				tooltip: I18n.PullFiddleheadFerns_Tooltip,
				getValue: () => PullFiddleheadFerns,
				setValue: val =>
				{
					PullFiddleheadFerns = val;
					UpdateEnabled();
				});

			/* Desert */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.DesertSection_Text);

			// PullCactusFruits
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.PullCactusFruits_Name,
				tooltip: I18n.PullCactusFruits_Tooltip,
				getValue: () => PullCactusFruits,
				setValue: val =>
				{
					PullCactusFruits = val;
					UpdateEnabled();
				});

			/* Island */
			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.DesertSection_Text);

			// DigGinger
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.DigGinger_Name,
				tooltip: I18n.DigGinger_Tooltip,
				getValue: () => DigGinger,
				setValue: val =>
				{
					DigGinger = val;
					UpdateEnabled();
				});

			gmcmApi.AddPageLink(
				mod: manifest,
				pageId: "",
				text: I18n.LinkSection_Link_BackToMain);
		}

		public void UpdateEnabled()
		{
			AnyRegularTreeEnabled = ShakeMahoganyTrees
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

			AnyForageablesEnabled = PullDaffodils
				|| PullDandelions
				|| PullLeeks
				|| PullSpringOnions
				|| PullWildHorseradishes
				|| PullGrapes
				|| PullSpiceBerries
				|| PullSweetPeas
				|| PullHazelnuts
				|| PullWildPlums
				|| PullCrocuses
				|| PullCrystalFruits
				|| PullHolly
				|| DigSnowYams
				|| DigWinterRoots
				|| PullChanterelles
				|| PullCommonMushrooms
				|| PullMagmaCaps
				|| PullMorels
				|| PullPurpleMushrooms
				|| PullRedMushrooms
				|| PullClams
				|| PullCockles
				|| PullCoral
				|| PullMussels
				|| PullNautilusShells
				|| PullOysters
				|| PullRainbowShells
				|| PullSeaUrchins
				|| PullSeaweed
				|| PullFiddleheadFerns
				|| DigGinger;

			AnyShakeEnabled = AnyRegularTreeEnabled || AnyFruitTreeEnabled || AnyBushEnabled || AnyForageablesEnabled;
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
		void AddPage(IManifest mod, string pageId, Func<string> pageTitle = null);

		/// <summary>Add a link to a page added via <see cref="AddPage"/> at the current position in the form.</summary>
		/// <param name="mod">The mod's manifest.</param>
		/// <param name="pageId">The unique ID of the page to open when the link is clicked.</param>
		/// <param name="text">The link text shown in the form.</param>
		/// <param name="tooltip">The tooltip text shown when the cursor hovers on the link, or <c>null</c> to disable the tooltip.</param>
		void AddPageLink(IManifest mod, string pageId, Func<string> text, Func<string> tooltip = null);

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
