﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace AutoShaker
{
	internal class ModConfig
	{
		public bool IsShakerActive { get; set; }
		public SButton ToggleShaker { get; set; }
		public bool ShakeTrees { get; set; }
		public bool ShakeBushes { get; set; }
		public bool UsePlayerMagnetism { get; set; }
		public int ShakeDistance { get; set; }

		public void ResetToDefault()
		{
			IsShakerActive = true;
			ToggleShaker = SButton.H;

			ShakeTrees = true;
			ShakeBushes = true;

			UsePlayerMagnetism = false;
			ShakeDistance = 1;
		}

		public ModConfig()
		{
			ResetToDefault();
		}

		public void RegisterModConfigMenu(IModHelper helper, IManifest manifest)
		{
			if (!helper.ModRegistry.IsLoaded("spacechase0.GenericModConfigMenu")) return;

			var gmcmApi = helper.ModRegistry.GetApi<IGenericModConfigMenu>("spacechase0.GenericModConfigMenu");

			gmcmApi.RegisterModConfig(manifest, ResetToDefault, () => helper.WriteConfig(this));

			// IsShakerActive
			gmcmApi.RegisterSimpleOption(
				manifest,
				"Shaker Is Active",
				"Whether or not the AutoShaker mod is active.",
				() => IsShakerActive,
				(val) => IsShakerActive = val);

			// ToggleShaker
			gmcmApi.RegisterSimpleOption(
				manifest,
				"Toggle Shaker Keybind",
				"Keybinding to toggle the AutoShaker on and off.",
				() => ToggleShaker,
				(val) => ToggleShaker = val);

			// ShakeTrees
			gmcmApi.RegisterSimpleOption(
				manifest,
				"Shake Trees?",
				"Whether or not the AutoShaker will shake trees that you walk by.",
				() => ShakeTrees,
				(val) => ShakeTrees = val);

			// ShakeBushes
			gmcmApi.RegisterSimpleOption(
				manifest,
				"Shake Bushes?",
				"Whether or not the AutoShaker will shake bushes that you walk by.",
				() => ShakeBushes,
				(val) => ShakeBushes = val);

			// UsePlayerMagnetism
			gmcmApi.RegisterSimpleOption(
				manifest,
				"Use Player Magnetism Distance?",
				"Whether or not the AutoShaker will shake bushes at the same distance players can pick up items. Note: Overrides 'Shake Distance'",
				() => UsePlayerMagnetism,
				(val) => UsePlayerMagnetism = val);

			// ShakeDistance
			gmcmApi.RegisterSimpleOption(
				manifest,
				"Shake Distance",
				"Distance to shake bushes when not using 'Player Magnetism.'",
				() => ShakeDistance,
				(val) => ShakeDistance = val);
		}
	}

	public interface IGenericModConfigMenu
	{
		void RegisterModConfig(IManifest mod, Action revertToDefault, Action saveToFile);

		void RegisterLabel(IManifest mod, string labelName, string labelDesc);
		void RegisterSimpleOption(IManifest mod, string optionName, string optionDesc, Func<bool> optionGet, Action<bool> optionSet);
		void RegisterSimpleOption(IManifest mod, string optionName, string optionDesc, Func<int> optionGet, Action<int> optionSet);
		void RegisterSimpleOption(IManifest mod, string optionName, string optionDesc, Func<float> optionGet, Action<float> optionSet);
		void RegisterSimpleOption(IManifest mod, string optionName, string optionDesc, Func<string> optionGet, Action<string> optionSet);
		void RegisterSimpleOption(IManifest mod, string optionName, string optionDesc, Func<SButton> optionGet, Action<SButton> optionSet);

		void RegisterClampedOption(IManifest mod, string optionName, string optionDesc, Func<int> optionGet, Action<int> optionSet, int min, int max);
		void RegisterClampedOption(IManifest mod, string optionName, string optionDesc, Func<float> optionGet, Action<float> optionSet, float min, float max);

		void RegisterChoiceOption(IManifest mod, string optionName, string optionDesc, Func<string> optionGet, Action<string> optionSet, string[] choices);

		void RegisterComplexOption(IManifest mod, string optionName, string optionDesc,
			Func<Vector2, object, object> widgetUpdate,
			Func<SpriteBatch, Vector2, object, object> widgetDraw,
			Action<object> onSave);
	}
}