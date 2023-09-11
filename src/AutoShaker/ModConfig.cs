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

		// Regular Trees
		public bool ShakeOakTrees { get; set; }
		public bool ShakeMapleTrees { get; set; }
		public bool ShakePineTrees { get; set; }
		public bool ShakeMahoganyTrees { get; set; }
		public bool ShakePalmTrees { get; set; }

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

		// Bushes
		public bool ShakeSalmonberries { get; set; }
		public bool ShakeBlackberries { get; set; }
		public bool ShakeTeaBushes { get; set; }
		public bool ShakeWalnutBushes { get; set; }

		public void ResetToDefault()
		{
			IsShakerActive = true;
			ToggleShaker = new KeybindList(
				new Keybind(SButton.LeftAlt, SButton.H),
				new Keybind(SButton.RightAlt, SButton.H));

			UsePlayerMagnetism = false;
			ShakeDistance = 2;

			// Regular Trees
			ShakeOakTrees = true;
			ShakeMapleTrees = true;
			ShakePineTrees = true;
			ShakeMahoganyTrees = true;
			ShakePalmTrees = true;

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

			// Bushes
			ShakeSalmonberries = true;
			ShakeBlackberries = true;
			ShakeTeaBushes = true;
			ShakeWalnutBushes = false;
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

			/* General */

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.GeneralSection_Name,
				tooltip: null);

			// IsShakerActive
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.IsShakerActive_Name,
				tooltip: I18n.IsShakerActive_Description,
				getValue: () => IsShakerActive,
				setValue: val => IsShakerActive = val);

			// ToggleShaker
			gmcmApi.AddKeybindList(
				mod: manifest,
				name: I18n.ToggleShaker_Name,
				tooltip: I18n.ToggleShaker_Description ,
				getValue: () => ToggleShaker,
				setValue: val => ToggleShaker = val);

			// UsePlayerMagnetism
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.UsePlayerMagnetism_Name,
				tooltip: I18n.UsePlayerMagnetism_Description,
				getValue: () => UsePlayerMagnetism,
				setValue: val => UsePlayerMagnetism = val);

			// ShakeDistance
			gmcmApi.AddNumberOption(
				mod: manifest,
				name: I18n.ShakeDistance_Name,
				tooltip: I18n.ShakeDistance_Description,
				getValue: () => ShakeDistance,
				setValue: val => ShakeDistance = val);

			/* Regular Trees */

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.RegularTreeSection_Name,
				tooltip: null);

			// ShakeMahoganyTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeMahoganyTrees_Name,
				tooltip: I18n.ShakeMahoganyTrees_Description,
				getValue: () => ShakeMahoganyTrees,
				setValue: val => ShakeMahoganyTrees = val);

			// ShakeMapleTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeMapleTrees_Name,
				tooltip: I18n.ShakeMapleTrees_Description,
				getValue: () => ShakeMapleTrees,
				setValue: val => ShakeMapleTrees = val);

			// ShakeOakTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeOakTrees_Name,
				tooltip: I18n.ShakeOakTrees_Description,
				getValue: () => ShakeOakTrees,
				setValue: val => ShakeOakTrees = val);

			// ShakePalmTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakePalmTrees_Name,
				tooltip: I18n.ShakePalmTrees_Description,
				getValue: () => ShakePalmTrees,
				setValue: val => ShakePalmTrees = val);

			// ShakePineTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakePineTrees_Name,
				tooltip: I18n.ShakePineTrees_Description,
				getValue: () => ShakePineTrees,
				setValue: val => ShakePineTrees = val);

			/* Fruit Trees */

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.FruitTreeSection_Name,
				tooltip: null); ;

			// FruitsReadyToShake
			gmcmApi.AddNumberOption(
				mod: manifest,
				name: I18n.FruitsReadyToShake_Name,
				tooltip: I18n.FruitsReadyToShake_Description,
				getValue: () => FruitsReadyToShake,
				setValue: val => FruitsReadyToShake = val,
				min: MinFruitsReady,
				max: MaxFruitsReady);

			// ShakeAppleTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeAppleTrees_Name,
				tooltip: I18n.ShakeAppleTrees_Description,
				getValue: () => ShakeAppleTrees,
				setValue: val => ShakeAppleTrees = val);

			// ShakeApricotTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeApricotTrees_Name,
				tooltip: I18n.ShakeApricotTrees_Description,
				getValue: () => ShakeApricotTrees,
				setValue: val => ShakeApricotTrees = val);

			// ShakeBananaTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeBananaTrees_Name,
				tooltip: I18n.ShakeBananaTrees_Description,
				getValue: () => ShakeBananaTrees,
				setValue: val => ShakeBananaTrees = val);

			// ShakeCherryTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeCherryTrees_Name,
				tooltip: I18n.ShakeCherryTrees_Description,
				getValue: () => ShakeCherryTrees,
				setValue: val => ShakeCherryTrees = val);

			// ShakeMangoTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeMangoTrees_Name,
				tooltip: I18n.ShakeMangoTrees_Description,
				getValue: () => ShakeMangoTrees,
				setValue: val => ShakeMangoTrees = val);

			// ShakeOrangeTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeOrangeTrees_Name,
				tooltip: I18n.ShakeOrangeTrees_Description,
				getValue: () => ShakeOrangeTrees,
				setValue: val => ShakeOrangeTrees = val);

			// ShakePeachTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakePeachTrees_Name,
				tooltip: I18n.ShakePeachTrees_Description,
				getValue: () => ShakePeachTrees,
				setValue: val => ShakePeachTrees = val);

			// ShakePomegranateTrees
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakePomegranateTrees_Name,
				tooltip: I18n.ShakePomegranateTrees_Description,
				getValue: () => ShakePomegranateTrees,
				setValue: val => ShakePomegranateTrees = val);

			/* Bushes */

			gmcmApi.AddSectionTitle(
				mod: manifest,
				text: I18n.BushesSection_Name,
				tooltip: null);

			// ShakeSalmonberries
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeSalmonberries_Name,
				tooltip: I18n.ShakeSalmonberries_Description,
				getValue: () => ShakeSalmonberries,
				setValue: val => ShakeSalmonberries = val);

			// ShakeBlackberries
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeBlackberries_Name,
				tooltip: I18n.ShakeBlackberries_Description,
				getValue: () => ShakeBlackberries,
				setValue: val => ShakeBlackberries = val);

			// ShakeTeaBushes
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeTeaBushes_Name,
				tooltip: I18n.ShakeTeaBushes_Description,
				getValue: () => ShakeTeaBushes,
				setValue: val => ShakeTeaBushes = val);

			// ShakeWalnutBushes
			gmcmApi.AddBoolOption(
				mod: manifest,
				name: I18n.ShakeWalnutBushes_Name,
				tooltip: I18n.ShakeWalnutBushes_Description,
				getValue: () => ShakeWalnutBushes,
				setValue: val => ShakeWalnutBushes = val);
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
	}
}
