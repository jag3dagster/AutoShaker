using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoShaker.Helpers;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.GameData.Locations;
using StardewValley.Internal;
using StardewValley.TerrainFeatures;

using Constants = AutoShaker.Helpers.Constants;
using Object = StardewValley.Object;

namespace AutoShaker
{
	/// <summary>
	/// The mod entry point.
	/// </summary>
	public class ModEntry : Mod
	{
		static readonly string disabledConfigString = "{0} are not being interacted with due to the [{1}] config option being disabled.";
		static readonly string eodStatMessage = Environment.NewLine + "\t[{0}] {1} shaken";

		private ModConfig _config = new();

		private Vector2 previousTilePosition;

		private readonly HashSet<TerrainFeature> _ignoredFeatures = new();
		private readonly HashSet<TerrainFeature> _interactedFeatures = new();

		private readonly Dictionary<Vector2, string> _forageablePredictions = new();
		private readonly Dictionary<string, Dictionary<string, int>> _trackingCounts = new()
		{
			{ "Seed Trees", new() },
			{ "Fruit Trees", new() },
			{ "Bushes", new() },
			{ "Forageables", new() }
		};


		private int _forageablesCount;

		/// <summary>
		/// The mod entry point, called after the mod is first loaded.
		/// </summary>
		/// <param name="helper">Provides simplified APIs for writing mods.</param>
		public override void Entry(IModHelper helper)
		{
			I18n.Init(helper.Translation);

			_config = helper.ReadConfig<ModConfig>();
			_config.UpdateEnabled();
			helper.WriteConfig(_config);

			helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
			helper.Events.GameLoop.DayStarted += OnDayStarted;
			helper.Events.GameLoop.DayEnding += OnDayEnding;
			helper.Events.Input.ButtonsChanged += OnButtonsChanged;
			helper.Events.Player.Warped += OnPlayerWarped;
			helper.Events.GameLoop.GameLaunched += (_,_) => _config.RegisterModConfigMenu(helper, ModManifest);
		}

		private void OnPlayerWarped(object? sender, WarpedEventArgs e)
		{
			_forageablePredictions.Clear();

			var mapLoc = e.NewLocation;
			var objsPairs = mapLoc.Objects.Pairs;

			foreach (var objPair in objsPairs)
			{
				var vec = objPair.Key;
				var x = (int)vec.X;
				var y = (int)vec.Y;
				var obj = objPair.Value;

				if (obj.QualifiedItemId == "(O)590")
				{
					var random = Utility.CreateDaySaveRandom(x * 2000, y);
					var dict = Game1.content.Load<Dictionary<string, LocationData>>("Data\\Locations");
					var locData = mapLoc.GetData();
					var context = new ItemQueryContext(mapLoc, Game1.player, random);
					IEnumerable<ArtifactSpotDropData> enumerable = dict["Default"].ArtifactSpots;

					if (locData != null && locData.ArtifactSpots?.Count > 0)
					{
						enumerable = enumerable.Concat(locData.ArtifactSpots);
					}

					enumerable = enumerable.OrderBy((ArtifactSpotDropData p) => p.Precedence);
					foreach (var drop in enumerable)
					{
						if (!random.NextBool(drop.Chance) || (drop.Condition != null && !GameStateQuery.CheckConditions(drop.Condition, mapLoc, Game1.player, null, null, random)))
						{
							continue;
						}

						var item = ItemQueryResolver.TryResolveRandomItem(drop, context, avoidRepeat: false, null, null, null, delegate (string query, string error)
						{
							Monitor.Log($"Error on query resolve.", LogLevel.Debug);
						});

						if (item == null)
						{
							continue;
						}

						Monitor.Log($"[{x}, {y}] {item.QualifiedItemId}", LogLevel.Info);

						// Snow Yam or Winter Root
						if (item.QualifiedItemId == "(O)416" || item.QualifiedItemId == "(O)412")
						{
							_forageablePredictions.Add(vec, item.QualifiedItemId);
						}

						if (!drop.ContinueOnDrop)
						{
							break;
						}
					}
				}
			}
		}

		private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
		{
			if (!Context.IsWorldReady || !Context.IsPlayerFree) return;
			if (!_config.IsShakerActive || !_config.AnyShakeEnabled) return;
			if (Game1.currentLocation == null || Game1.player == null) return;
			if (Game1.player.Tile.Equals(previousTilePosition)) return;
			if (Game1.CurrentEvent != null && (!Game1.CurrentEvent.playerControlSequence || !Game1.CurrentEvent.canPlayerUseTool())) return;
			if (Game1.player.currentLocation.terrainFeatures.Count() == 0
				&& Game1.player.currentLocation.largeTerrainFeatures.Count == 0
				&& Game1.player.currentLocation.Objects.Count() == 0) return;

			previousTilePosition = Game1.player.Tile;
			var playerTileLocationPoint = Game1.player.TilePoint;
			var playerMagnetism = Game1.player.GetAppliedMagneticRadius();
			var radius = _config.UsePlayerMagnetism ? playerMagnetism / Game1.tileSize : _config.ShakeDistance;

			if (_config.AnyRegularTreeEnabled || _config.AnyFruitTreeEnabled || _config.AnyBushEnabled || _config.AnyForageablesEnabled)
			{
				foreach (var vec in GetTilesToCheck(playerTileLocationPoint, radius))
				{
					// Regular Trees, Fruit Trees, Bushes, Spring Onions, Ginger
					if (Game1.currentLocation.terrainFeatures.TryGetValue(vec, out var feature)
						&& feature is Tree or FruitTree or Bush or HoeDirt
						&& !_ignoredFeatures.Contains(feature)
						&& !_interactedFeatures.Contains(feature))
					{
						var featureTileLocation = feature.Tile;
						var toIgnore = false;

						switch (feature)
						{
							// Tree Cases
							case Tree treeFeature:
								if (!_config.AnyRegularTreeEnabled) continue;
								if (treeFeature.stump.Value) toIgnore = true;
								if (treeFeature.growthStage.Value < 5 || !treeFeature.hasSeed.Value) toIgnore = true;
								if (Game1.player.ForagingLevel < 1) toIgnore = true;

								if (!treeFeature.isActionable())
								{
									Monitor.Log($"Tree of type [{treeFeature.treeType.Value}] not shaken because the game deemed it was not actionable. It is likely tapped or not mature yet.", LogLevel.Trace);
									toIgnore = true;
								}

								if (toIgnore)
								{
									_ignoredFeatures.Add(treeFeature);
									continue;
								}

								switch (treeFeature.treeType.Value)
								{
									// Oak Tree
									case "1":
									case "4": // Winter
										if (!_config.ShakeOakTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Oak trees", I18n.ShakeOakTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Seed Trees"].AddOrIncrement("Oak trees");
										break;

									// Maple Tree
									case "2":
									case "5": // Winter
										if (!_config.ShakeMapleTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Maple trees", I18n.ShakeMapleTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Seed Trees"].AddOrIncrement("Maple trees");
										break;

									// Pine Tree
									case "3":
										if (!_config.ShakePineTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Pine trees", I18n.ShakePineTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Seed Trees"].AddOrIncrement("Pine trees");
										break;

									// Mahogany Tree
									case "8":
										if (!_config.ShakeMahoganyTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Mahogany trees", I18n.ShakeMahoganyTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Seed Trees"].AddOrIncrement("Mahogany trees");
										break;

									// Palm Tree
									case "6": // Desert
									case "9": // Island
										if (!_config.ShakePalmTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Palm trees", I18n.ShakePalmTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Seed Trees"].AddOrIncrement("Palm trees");
										break;

									default:
										Monitor.Log($"Unknown Tree type: [{treeFeature.treeType.Value}]", LogLevel.Warn);
										_ignoredFeatures.Add(treeFeature);
										continue;
								}

								treeFeature.performUseAction(featureTileLocation);
								_interactedFeatures.Add(treeFeature);
								break;

							// Fruit Tree Cases
							case FruitTree fruitTree:
								if (!_config.AnyFruitTreeEnabled) continue;
								if (fruitTree.stump.Value) toIgnore = true;
								if (fruitTree.growthStage.Value < 4) toIgnore = true;

								if (fruitTree.fruit.Count < _config.FruitsReadyToShake)
								{
									Monitor.LogOnce($"Fruit trees will not be shaken until they have the mnumber of fruits available specified by the [{I18n.FruitsReadyToShake_Name()}] config option. Expected Number of Fruits: [{_config.FruitsReadyToShake}]", LogLevel.Debug);
									toIgnore = true;
								}

								if (!fruitTree.isActionable())
								{
									Monitor.Log($"A fruit tree of type [{fruitTree.treeId.Value}] was marked as not actionable. This shouldn't be possible.", LogLevel.Warn);
									Monitor.Log($"Type: [{fruitTree.treeId.Value}]; Location: [{fruitTree.Location.Name}]; Tile Location: [{fruitTree.Tile}]; Fruit Count: [{fruitTree.fruit.Count}]; Fruit Indices: [{String.Join(",", fruitTree.fruit)}]", LogLevel.Debug);
								}

								if (toIgnore)
								{
									_ignoredFeatures.Add(fruitTree);
									continue;
								}

								switch (fruitTree.treeId.Value)
								{
									// Cherry Tree
									case "0":
										if (!_config.ShakeCherryTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Cherry trees", I18n.ShakeCherryTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Cherry trees");
										break;

									// Apricot Tree
									case "1":
										if (!_config.ShakeApricotTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Apricot trees", I18n.ShakeApricotTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Apricot trees");
										break;

									// Orange Tree
									case "2":
										if (!_config.ShakeOrangeTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Orange trees", I18n.ShakeOrangeTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Orange trees");
										break;

									// Peach Tree
									case "3":
										if (!_config.ShakePeachTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Peach trees", I18n.ShakePeachTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Peach trees");
										break;

									// Pomegranate Tree
									case "4":
										if (!_config.ShakePomegranateTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Pomegranate trees", I18n.ShakePomegranateTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Pomegranate trees");
										break;

									// Apple Tree
									case "5":
										if (!_config.ShakeAppleTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Apple trees", I18n.ShakeAppleTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Apple trees");
										break;

									// Banana Tree
									case "7":
										if (!_config.ShakeBananaTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Banana trees", I18n.ShakeBananaTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Banana trees");
										break;

									// Mango Tree
									case "8":
										if (!_config.ShakeMangoTrees)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Mango trees", I18n.ShakeMangoTrees_Name()), LogLevel.Debug);
											continue;
										}

										_trackingCounts["Fruit Trees"].AddOrIncrement("Mango trees");
										break;

									default:
										Monitor.Log($"Unknown Fruit Tree type: [{fruitTree.treeId.Value}]", LogLevel.Warn);
										_ignoredFeatures.Add(fruitTree);
										continue;
								}

								fruitTree.performUseAction(featureTileLocation);
								_interactedFeatures.Add(fruitTree);
								break;

							// Bush Cases
							case Bush bushFeature:
								if (!CheckBush(bushFeature)) continue;

								bushFeature.performUseAction(featureTileLocation);
								_interactedFeatures.Add(bushFeature);
								break;

							// Forageable Cases
							case HoeDirt hoeDirtFeature:
								if (!_config.PullSpringOnions && !_config.DigGinger) continue;
								if (hoeDirtFeature.crop == null || !hoeDirtFeature.crop.forageCrop.Value || hoeDirtFeature.crop.whichForageCrop.Value.IsNullOrEmpty()) toIgnore = true;

								if (toIgnore)
								{
									Monitor.LogOnce($"Ignored {hoeDirtFeature.crop?.indexOfHarvest.Value ?? "empty hoe dirt"}", LogLevel.Debug);
									_ignoredFeatures.Add(hoeDirtFeature);
									continue;
								}

								var whichCrop = hoeDirtFeature.crop?.whichForageCrop.Value ?? "-1";
								Vector2 tile;
								switch (whichCrop)
								{
									// Spring Onion
									case "1":
										if (!_config.PullSpringOnions)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Spring Onions", I18n.PullSpringOnions_Name()), LogLevel.Debug);
											continue;
										}

										tile = hoeDirtFeature.Tile;
										var x = (int)tile.X;
										var y = (int)tile.Y;

										ForageItem(ItemRegistry.Create<Object>("(O)399"), tile, Utility.CreateDaySaveRandom(x * 1000, y * 2000), 3);
										hoeDirtFeature.destroyCrop(false);
										Game1.playSound("harvest");

										_trackingCounts["Forageables"].AddOrIncrement("Spring Onions");
										break;

									// Ginger
									case "2":
										if (!_config.DigGinger)
										{
											Monitor.LogOnce(String.Format(disabledConfigString, "Ginger Roots", I18n.DigGinger_Name()), LogLevel.Debug);
											continue;
										}

										tile = hoeDirtFeature.Tile;

										hoeDirtFeature.crop?.hitWithHoe((int)tile.X, (int)tile.Y, hoeDirtFeature.Location, hoeDirtFeature);
										hoeDirtFeature.destroyCrop(false);

										_trackingCounts["Forageables"].AddOrIncrement("Ginger roots");
										break;

									default:
										Monitor.Log($"No good case: {whichCrop}");
										continue;
								}

								_forageablesCount += 1;
								_interactedFeatures.Add(hoeDirtFeature);
								break;

							// This should never happen
							default:
								Monitor.Log("I am an unknown terrain feature, ignore me I guess...", LogLevel.Debug);
								break;
						}
					}

					// Forageables (except Spring Onions, Ginger)
					if (_config.AnyForageablesEnabled && Game1.currentLocation.Objects.TryGetValue(vec, out var obj))
					{
						if (obj.isForage() && obj.IsSpawnedObject && !obj.questItem.Value)
						{
							if ((_config.ForageableToggles & (int)Constants.ForageableLookup[obj.QualifiedItemId]) > 0)
							{
								ForageItem(obj, vec, Utility.CreateDaySaveRandom(vec.X, vec.Y * 777f), 7, true);

								Game1.player.currentLocation.removeObject(vec, showDestroyedObject: false);
								Game1.playSound("harvest");

								_trackingCounts["Forageables"].AddOrIncrement(obj.DisplayName);
								_forageablesCount += 1;
							}
							else
							{
								Monitor.LogOnce(String.Format(disabledConfigString, obj.DisplayName, Constants.ConfigNameLookup[obj.QualifiedItemId]()), LogLevel.Debug);
								continue;
							}
						}
						else if (obj.QualifiedItemId == "(O)590")
						{
							if (_forageablePredictions.ContainsKey(vec) && (_config.ForageableToggles & (int)Constants.ForageableLookup[_forageablePredictions[vec]]) > 0)
							{
								Game1.currentLocation.digUpArtifactSpot((int)vec.X, (int)vec.Y, Game1.player);

								if (!Game1.currentLocation.terrainFeatures.ContainsKey(vec))
								{
									Game1.currentLocation.makeHoeDirt(vec, ignoreChecks: true);
								}

								Game1.currentLocation.playSound("hoeHit");
								Game1.currentLocation.removeObject(vec, false);

								_forageablesCount += 1;
							}
							else if (_forageablePredictions.ContainsKey(vec))
							{
								Monitor.LogOnce(String.Format(disabledConfigString, obj.DisplayName, Constants.ConfigNameLookup[_forageablePredictions[vec]]()), LogLevel.Debug);
								continue;
							}
						}
					}
				}

			}

			if (_config.AnyBushEnabled)
			{
				foreach (var feature in Game1.player.currentLocation.largeTerrainFeatures)
				{
					if (feature is not Bush bush) continue;

					var location = bush.Tile;

					if (!IsInRange(playerTileLocationPoint, location, radius)
						|| _interactedFeatures.Contains(bush) || _ignoredFeatures.Contains(bush))
					{
						continue;
					}

					if (CheckBush(bush))
					{
						bush.performUseAction(location);
						_interactedFeatures.Add(feature);
					}
				}
			}
		}

		private void OnDayStarted(object? sender, DayStartedEventArgs e)
		{
			previousTilePosition = Game1.player.Tile;
		}

		// $TODO - Update to I18n.
		private void OnDayEnding(object? sender, DayEndingEventArgs e)
		{
			StringBuilder statMessage = new($"{Environment.NewLine}{Utility.getDateString()}:{Environment.NewLine}");
			statMessage.AppendLine($"[{_interactedFeatures.Count + _forageablesCount}] Total Interactions");

			foreach (var category in _trackingCounts)
			{
				if (category.Value.Count > 0)
				{
					statMessage.AppendLine($"{category.Key}:");

					foreach (var interactable in category.Value)
					{
						if (interactable.Value <= 0) Monitor.Log($"Invalid forageable value for {interactable.Key}: {interactable.Value}. How did we get here?", LogLevel.Warn);
						statMessage.AppendLine(String.Format(eodStatMessage, interactable.Value, interactable.Key));
					}
				}

				category.Value.Clear();
			}

			Monitor.Log(statMessage.ToString(), LogLevel.Info);

			// Reset
			Monitor.Log("Resetting daily counts...", LogLevel.Trace);

			_ignoredFeatures.Clear();
			_interactedFeatures.Clear();
			_forageablesCount = 0;
		}

		private void OnButtonsChanged(object? sender, ButtonsChangedEventArgs e)
		{
			if (Game1.activeClickableMenu == null)
			{
				if (_config.ToggleShaker.JustPressed())
				{
					_config.IsShakerActive = !_config.IsShakerActive;
					Task.Run(() => Helper.WriteConfig(_config)).ContinueWith((t) =>
						this.Monitor.Log(t.Status == TaskStatus.RanToCompletion
							? "Config saved successfully!"
							: $"Saving config unsuccessful {t.Status}"));

					var message = "AutoShaker has been " + (_config.IsShakerActive ? "ACTIVATED" : "DEACTIVATED");

					Monitor.Log(message, LogLevel.Info);
					Game1.addHUDMessage(new HUDMessage(message));
				}
			}
		}

		private bool CheckBush(Bush bush)
		{
			var toIgnore = false;

			if (!_config.AnyBushEnabled) return false;
			if (bush.townBush.Value) toIgnore = true;
			if (!bush.inBloom()) toIgnore = true;
			if (bush.tileSheetOffset.Value != 1) toIgnore = true;

			if (!bush.isActionable())
			{
				Monitor.Log($"A bush feature of size [{bush.size.Value}] was marked as not actionable. This shouldn't be possible.", LogLevel.Warn);
				Monitor.Log($"Size: [{bush.size.Value}]; Location: [{bush.Location.Name}]; Tile Location: [{bush.Tile}]; Town Bush: [{bush.townBush.Value}]", LogLevel.Debug);
			}

			if (toIgnore)
			{
				_ignoredFeatures.Add(bush);
				return false;
			}

			switch (bush.size.Value)
			{
				// Forageable Bushes
				case 0:
				case 1:
				case 2:
					var season = Game1.currentSeason;

					if (!season.Equals("spring") && !season.Equals("fall")) return false;

					if (season.Equals("spring") && !_config.ShakeSalmonberriesBushes)
					{
						Monitor.LogOnce(String.Format(disabledConfigString, "Salmonberry bushes", I18n.ShakeSalmonberries_Name()), LogLevel.Debug);
						return false;
					}

					if (season.Equals("fall") && !_config.ShakeBlackberriesBushes)
					{
						Monitor.LogOnce(String.Format(disabledConfigString, "Blackberry bushes", I18n.ShakeBlackberries_Name()), LogLevel.Debug);
						return false;
					}

					_trackingCounts["Bushes"].AddOrIncrement(season.Equals("spring") ? "Salmonberry bushes" : "Blackberry bushes");
					break;

				// Tea Bushes
				case 3:
					if (!_config.ShakeTeaBushes)
					{
						Monitor.LogOnce(String.Format(disabledConfigString, "Tea bushes", I18n.ShakeTeaBushes_Name()), LogLevel.Debug);
						return false;
					}

					_trackingCounts["Bushes"].AddOrIncrement("Tea bushes");
					break;

				// Walnut Bushes
				case 4:
					if (!_config.ShakeWalnutBushes)
					{
						Monitor.LogOnce(String.Format(disabledConfigString, "Walnut bushes", I18n.ShakeWalnutBushes_Name()), LogLevel.Debug);
						return false;
					}

					_trackingCounts["Bushes"].AddOrIncrement("Walnut bushes");
					break;

				default:
					Monitor.Log($"Unknown Bush size: [{bush.size.Value}]", LogLevel.Warn);
					_ignoredFeatures.Add(bush);
					return false;
			}

			return true;
		}

		private static void ForageItem(Object obj, Vector2 vec, Random random, int xpGained = 0, bool checkGatherer = false)
		{
			var foragingLevel = Game1.player.ForagingLevel;
			var professions = Game1.player.professions;

			if (professions.Contains(16))
			{
				obj.Quality = 4;
			}
			else if (random.NextDouble() < (double)(foragingLevel / 30f))
			{
				obj.Quality = 2;
			}
			else if (random.NextDouble() < (double)(foragingLevel / 15f))
			{
				obj.Quality = 1;
			}

			vec *= 64.0f;

			Game1.player.gainExperience(2, xpGained);
			Game1.createItemDebris(obj.getOne(), vec, -1, null, -1);

			if (checkGatherer && professions.Contains(13) && random.NextDouble() < 0.2)
			{
				Game1.player.gainExperience(2, xpGained);
				Game1.createItemDebris(obj.getOne(), vec, -1, null, -1);
			}
		}

		private static bool IsInRange(Point playerLocation, Vector2 bushLocation, int radius)
			=> Math.Abs(bushLocation.X - playerLocation.X) <= radius && Math.Abs(bushLocation.Y - playerLocation.Y) <= radius;

		private static IEnumerable<Vector2> GetTilesToCheck(Point playerLocation, int radius)
		{
			for (int x = Math.Max(playerLocation.X - radius, 0); x <= playerLocation.X + radius; x++)
				for (int y = Math.Max(playerLocation.Y - radius, 0); y <= playerLocation.Y + radius; y++)
					yield return new Vector2(x, y);

			yield break;
		}
	}
}
