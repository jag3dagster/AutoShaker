using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SpaceShared.APIs;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.TerrainFeatures;

namespace AutoShaker
{
	/// <summary>
	/// The mod entry point.
	/// </summary>
	public class ModEntry : Mod
	{
		private const int SingleTileDistance = 64;

		private ModConfig _config;

		private readonly List<Bush> _shakenBushes = new List<Bush>();

		private int _treesShaken = 0;
		private int _fruitTressShaken = 0;

		/// <summary>
		/// The mod entry point, called after the mod is first loaded.
		/// </summary>
		/// <param name="helper">Provides simplified APIs for writing mods.</param>
		public override void Entry(IModHelper helper)
		{
			_config = helper.ReadConfig<ModConfig>();

			helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
			helper.Events.GameLoop.DayEnding += this.OnDayEnding;
			helper.Events.Input.ButtonPressed += this.OnButtonPressed;
			helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
		}

		private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
		{
			if (!Game1.hasLoadedGame) return;
			if (!_config.IsShakerActive) return;
			if (Game1.currentLocation == null || Game1.player == null) return;
			if (Game1.CurrentEvent != null && (!Game1.CurrentEvent.playerControlSequence || !Game1.CurrentEvent.canPlayerUseTool())) return;
			if (Game1.player.currentLocation.terrainFeatures.ToList().Count == 0 &&
				Game1.player.currentLocation.largeTerrainFeatures.ToList().Count == 0) return;

			
			var playerTileLocationPoint = Game1.player.getTileLocationPoint();
			var playerMagnetism = Game1.player.GetAppliedMagneticRadius();

			if (_config.ShakeTrees)
			{
				var trees = Game1.player.currentLocation.terrainFeatures.Pairs
					.Select(p => p.Value)
					.Where(v => v is Tree || v is FruitTree);

				foreach (var tree in trees)
				{
					var featureTileLocation = tree.currentTileLocation;

					if (!IsInShakeRange(playerTileLocationPoint, featureTileLocation, playerMagnetism)) continue;

					switch (tree)
					{
						// Tree cases
						case Tree treeFeature when treeFeature.stump:
							continue;
						case Tree treeFeature when !treeFeature.hasSeed:
							continue;
						case Tree treeFeature when !treeFeature.isActionable():
							continue;
						case Tree _ when Game1.player.ForagingLevel < 1:
							continue;
						case Tree treeFeature:
							treeFeature.performUseAction(featureTileLocation, Game1.player.currentLocation);
							_treesShaken += 1;
							break;

						// Fruit Tree cases
						case FruitTree fruitTree when fruitTree.stump:
							continue;
						case FruitTree fruitTree when fruitTree.fruitsOnTree.Value <= 0:
							continue;
						case FruitTree fruitTree when !fruitTree.isActionable():
							continue;
						case FruitTree fruitTree:
							fruitTree.performUseAction(featureTileLocation, Game1.player.currentLocation);
							_fruitTressShaken += 1;
							break;

						// This should never happen
						default:
							Monitor.Log("I am an unknown terrain feature, ignore me I guess...", LogLevel.Debug);
							break;
					}
				}
			}

			if (_config.ShakeBushes)
			{
				var bushes = Game1.player.currentLocation.largeTerrainFeatures.Where(feature => feature is Bush);

				foreach (var bush in bushes)
				{
					var location = bush.tilePosition;

					if (_shakenBushes.Contains(bush)) continue;
					if (!IsInShakeRange(playerTileLocationPoint, location, playerMagnetism)) continue;

					switch (bush)
					{
						// Bush cases
						case Bush bushFeature when bushFeature.townBush:
							continue;
						case Bush bushFeature when !bushFeature.isActionable():
							continue;
						case Bush bushFeature when !bushFeature.inBloom(Game1.CurrentSeasonDisplayName, Game1.dayOfMonth):
							continue;
						case Bush bushFeature:
							bushFeature.performUseAction(location, Game1.player.currentLocation);
							_shakenBushes.Add(bushFeature);
							break;

						// This should never happen
						default:
							Monitor.Log("I am an unknown large terrain feature, ignore me I guess...", LogLevel.Debug);
							break;
					}
				}
			}
		}

		private void OnDayEnding(object sender, DayEndingEventArgs e)
		{
			var statMessage = $"{Game1.CurrentSeasonDisplayName}, day {Game1.dayOfMonth} stats: ";

			if (_treesShaken == 0 && _fruitTressShaken == 0 && _shakenBushes.Count == 0)
			{
				statMessage += "Nothing shaken today.";
			}
			else
			{
				statMessage += $"[{_treesShaken}] Trees shaken; [{_fruitTressShaken}] Fruit Trees shaken; [{_shakenBushes.Count}] Bushes shaken";

				Monitor.Log("Resetting daily counts...");
				_shakenBushes.Clear();
				_treesShaken = 0;
				_fruitTressShaken = 0;
			}

			Monitor.Log(statMessage, LogLevel.Info);
		}

		private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
		{
			if ((Helper.Input.IsDown(SButton.LeftAlt) || Helper.Input.IsDown(SButton.RightAlt)) && e.Button == SButton.H)
			{
				_config.IsShakerActive = !_config.IsShakerActive;
				Helper.WriteConfig(_config);

				var message = "AutoShaker has been " + (_config.IsShakerActive ? "ACTIVATED" : "DEACTIVATED");

				Monitor.Log(message, LogLevel.Info);
				Game1.addHUDMessage(new HUDMessage(message, null));
			}
		}

		private bool IsInShakeRange(Point playerLocation, Vector2 bushLocation, int playerMagnetism)
		{
			var pickUpDistance = _config.ShakeDistance;

			if (_config.UsePlayerMagnetism)
			{
				pickUpDistance = (int)Math.Floor((double)playerMagnetism / (double)SingleTileDistance);
			}

			var inRange = Math.Abs(bushLocation.X - playerLocation.X) <= pickUpDistance &&
							Math.Abs(bushLocation.Y - playerLocation.Y) <= pickUpDistance;

			return inRange;
		}

		private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
		{
			var gmcmApi = this.Helper.ModRegistry.GetApi<GenericModConfigMenuAPI>("spacechase0.GenericModConfigMenu");

			if (gmcmApi != null)
			{
				gmcmApi.RegisterModConfig(ModManifest, () => _config = new ModConfig(), () => Helper.WriteConfig(_config));

				gmcmApi.RegisterSimpleOption(ModManifest, "Shaker Is Active", "Whether or not the AutoShaker mod is active.", () => _config.IsShakerActive, (val) => _config.IsShakerActive = val);
				gmcmApi.RegisterSimpleOption(ModManifest, "Toggle Shaker Keybind", "Keybinding to toggle the AutoShaker on and off.", () => _config.ToggleShaker, (val) => _config.ToggleShaker = val);
				gmcmApi.RegisterSimpleOption(ModManifest, "Shake Trees?", "Whether or not the AutoShaker will shake trees that you walk by.", () => _config.ShakeTrees, (val) => _config.ShakeTrees = val);
				gmcmApi.RegisterSimpleOption(ModManifest, "Shake Bushes?", "Whether or not the AutoShaker will shake bushes that you walk by.", () => _config.ShakeBushes, (val) => _config.ShakeBushes = val);
				gmcmApi.RegisterSimpleOption(ModManifest, "Use Player Magnetism Distance?", "Whether or not the AutoShaker will shake bushes at the same distance players can pick up items. Note: Overrides 'Shake Distance'", () => _config.UsePlayerMagnetism, (val) => _config.UsePlayerMagnetism = val);
				gmcmApi.RegisterSimpleOption(ModManifest, "Shake Distance", "Distance to shake bushes when not using 'Player Magnetism.'", () => _config.ShakeDistance, (val) => _config.ShakeDistance = val);
			}
		}
	}
}
