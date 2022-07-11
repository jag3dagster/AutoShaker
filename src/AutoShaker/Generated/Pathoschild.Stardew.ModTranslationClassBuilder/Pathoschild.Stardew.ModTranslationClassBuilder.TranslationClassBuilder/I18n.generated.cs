#nullable enable
using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using StardewModdingAPI;

namespace AutoShaker
{
    /// <summary>Get translations from the mod's <c>i18n</c> folder.</summary>
    /// <remarks>This is auto-generated from the <c>i18n/default.json</c> file when the project is compiled.</remarks>
    [GeneratedCode("TextTemplatingFileGenerator", "1.0.0")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Deliberately named for consistency and to match translation conventions.")]
    internal static class I18n
    {
        /*********
        ** Fields
        *********/
        /// <summary>The mod's translation helper.</summary>
        private static ITranslationHelper? Translations;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="translations">The mod's translation helper.</param>
        public static void Init(ITranslationHelper translations)
        {
            I18n.Translations = translations;
        }

        /// <summary>Get a translation equivalent to "Shaker Is Active".</summary>
        public static string IsShakerActive_Name()
        {
            return I18n.GetByKey("IsShakerActive.Name");
        }

        /// <summary>Get a translation equivalent to "Whether or not the AutoShaker mod is active.".</summary>
        public static string IsShakerActive_Description()
        {
            return I18n.GetByKey("IsShakerActive.Description");
        }

        /// <summary>Get a translation equivalent to "Toggle Shaker Keybind".</summary>
        public static string ToggleShaker_Name()
        {
            return I18n.GetByKey("ToggleShaker.Name");
        }

        /// <summary>Get a translation equivalent to "Keybinding to toggle the AutoShaker on and off.".</summary>
        public static string ToggleShaker_Description()
        {
            return I18n.GetByKey("ToggleShaker.Description");
        }

        /// <summary>Get a translation equivalent to "Shake Regular Trees?".</summary>
        public static string ShakeRegularTrees_Name()
        {
            return I18n.GetByKey("ShakeRegularTrees.Name");
        }

        /// <summary>Get a translation equivalent to "Whether or not the AutoShaker will shake regular trees that you walk by for seeds.".</summary>
        public static string ShakeRegularTrees_Description()
        {
            return I18n.GetByKey("ShakeRegularTrees.Description");
        }

        /// <summary>Get a translation equivalent to "Shake Fruit Trees?".</summary>
        public static string ShakeFruitTrees_Name()
        {
            return I18n.GetByKey("ShakeFruitTrees.Name");
        }

        /// <summary>Get a translation equivalent to "Whether or not the AutoShaker will shake fruit trees that you walk by for fruit.".</summary>
        public static string ShakeFruitTrees_Description()
        {
            return I18n.GetByKey("ShakeFruitTrees.Description");
        }

        /// <summary>Get a translation equivalent to "Minimum Fruits Ready to Shake".</summary>
        public static string FruitsReadyToShake_Name()
        {
            return I18n.GetByKey("FruitsReadyToShake.Name");
        }

        /// <summary>Get a translation equivalent to "Minimum amount of fruits a Fruit Tree should have ready before the AutoShaker shakes the tree.".</summary>
        public static string FruitsReadyToShake_Description()
        {
            return I18n.GetByKey("FruitsReadyToShake.Description");
        }

        /// <summary>Get a translation equivalent to "Shake Tea Bushes?".</summary>
        public static string ShakeTeaBushes_Name()
        {
            return I18n.GetByKey("ShakeTeaBushes.Name");
        }

        /// <summary>Get a translation equivalent to "Whether or not the AutoShaker will shake tea bushes that you walk by for tea leaves.".</summary>
        public static string ShakeTeaBushes_Description()
        {
            return I18n.GetByKey("ShakeTeaBushes.Description");
        }

        /// <summary>Get a translation equivalent to "Shake Bushes?".</summary>
        public static string ShakeBushes_Name()
        {
            return I18n.GetByKey("ShakeBushes.Name");
        }

        /// <summary>Get a translation equivalent to "Whether or not the AutoShaker will shake bushes that you walk by.".</summary>
        public static string ShakeBushes_Description()
        {
            return I18n.GetByKey("ShakeBushes.Description");
        }

        /// <summary>Get a translation equivalent to "Use Player Magnetism Distance?".</summary>
        public static string UsePlayerMagnetism_Name()
        {
            return I18n.GetByKey("UsePlayerMagnetism.Name");
        }

        /// <summary>Get a translation equivalent to "Whether or not the AutoShaker will shake bushes at the same distance players can pick up items. Note: Overrides 'Shake Distance'".</summary>
        public static string UsePlayerMagnetism_Description()
        {
            return I18n.GetByKey("UsePlayerMagnetism.Description");
        }

        /// <summary>Get a translation equivalent to "Shake Distance".</summary>
        public static string ShakeDistance_Name()
        {
            return I18n.GetByKey("ShakeDistance.Name");
        }

        /// <summary>Get a translation equivalent to "Distance to shake bushes when not using 'Player Magnetism.'".</summary>
        public static string ShakeDistance_Description()
        {
            return I18n.GetByKey("ShakeDistance.Description");
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Get a translation by its key.</summary>
        /// <param name="key">The translation key.</param>
        /// <param name="tokens">An object containing token key/value pairs. This can be an anonymous object (like <c>new { value = 42, name = "Cranberries" }</c>), a dictionary, or a class instance.</param>
        private static Translation GetByKey(string key, object? tokens = null)
        {
            if (I18n.Translations == null)
                throw new InvalidOperationException($"You must call {nameof(I18n)}.{nameof(I18n.Init)} from the mod's entry method before reading translations.");
            return I18n.Translations.Get(key, tokens);
        }
    }
}

