using Microsoft.Xna.Framework.Content;
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

		public ModConfig()
		{
			this.IsShakerActive = true;
			this.ToggleShaker = SButton.H;

			this.ShakeTrees = true;
			this.ShakeBushes = true;

			this.UsePlayerMagnetism = false;
			this.ShakeDistance = 1;
		}
	}
}
