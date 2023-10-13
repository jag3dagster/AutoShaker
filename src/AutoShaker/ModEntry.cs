using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.GameData.Locations;
using StardewValley.Internal;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using AutoShaker.Helpers;

using Constants = AutoShaker.Helpers.Constants;
using Object = StardewValley.Object;
using StardewValley.GameData.FruitTrees;
using StardewValley.GameData.Objects;
using StardewValley.GameData.WildTrees;
using AutoShaker.Classes;

namespace AutoShaker
{
	/// <summary>
	/// The mod entry point.
	/// </summary>
	public class ModEntry : Mod
	{
		private const string BushKey = "Key.Bushes";
		private const string ForageableKey = "Key.Forageables";
		private const string FruitTreeKey = "Key.FruitTrees";
		private const string WildTreeKey = "Key.WildTrees";

		private ModConfig _config;

		private bool _gameStarted;
		private Vector2 _previousTilePosition;

		//private readonly HashSet<TerrainFeature> _ignoredFeatures = new();
		//private readonly HashSet<TerrainFeature> _interactedFeatures = new();

		private readonly List<string> _overrideItemIds;
		private readonly List<string> _ignoreItemIds;

		private readonly ForageableItemTracker _forageableTracker;

		private readonly Dictionary<Vector2, string> _artifactPredictions;

		private readonly Dictionary<string, Dictionary<string, int>> _trackingCounts;

		#region Cache Properties

		private Dictionary<string, FruitTreeData> _fruitTreeCache = new();
		private Dictionary<string, FruitTreeData> FruitTreeCache
		{
			get => _fruitTreeCache;
			set
			{
				_fruitTreeCache = value;
				ParseAssets(value);
			}
		}

		private Dictionary<string, LocationData> _locationCache = new();
		private Dictionary<string, LocationData> LocationCache
		{
			get => _locationCache;
			set
			{
				_locationCache = value;
				ParseAssets(value);
			}
		}

		private Dictionary<string, ObjectData> _objectCache = new();
		private Dictionary<string, ObjectData> ObjectCache
		{
			get => _objectCache;
			set
			{
				_objectCache = value;
				ParseAssets(value);
			}
		}

		private Dictionary<string, WildTreeData> _wildTreeCache = new();
		private Dictionary<string, WildTreeData> WildTreeCache
		{
			get => _wildTreeCache;
			set
			{
				_wildTreeCache = value;
				ParseAssets(value);
			}
		}

		#endregion Cache Properties

		private DateTime _nextErrorMessage;

		public ModEntry()
		{
			_config = new();
			_gameStarted = false;

			_overrideItemIds = new()
			{
				"(O)152", // Seaweed
				"(O)296", // Salmonberry
				"(O)416", // Snow Yam
				"(O)851"  // Magma Cap
			};

			_ignoreItemIds = new()
			{
				"(O)78" // Cave Carrot
			};

			_forageableTracker = ForageableItemTracker.Instance;
			_artifactPredictions = new();

			_trackingCounts = new()
			{
				{ BushKey, new() },
				{ ForageableKey, new() },
				{ FruitTreeKey, new() },
				{ WildTreeKey, new() }
			};

			_nextErrorMessage = DateTime.MinValue;
		}

		/// <summary>
		/// The mod entry point, called after the mod is first loaded.
		/// </summary>
		/// <param name="helper">Provides simplified APIs for writing mods.</param>
		public override void Entry(IModHelper helper)
		{
			I18n.Init(helper.Translation);

			_config = helper.ReadConfig<ModConfig>();
			_config.UpdateEnabled(Helper);

			helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
			helper.Events.GameLoop.DayStarted += OnDayStarted;
			helper.Events.GameLoop.DayEnding += OnDayEnding;
			helper.Events.Input.ButtonsChanged += OnButtonsChanged;
			helper.Events.Player.Warped += OnPlayerWarped;
			helper.Events.GameLoop.GameLaunched += OnGameLaunched;
			helper.Events.Content.AssetReady += OnAssetReady;
			helper.Events.Content.AssetRequested += OnAssetRequested;
		}

		private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
		{
			var assetName = e.Name.BaseName;

			if (assetName.Equals(Constants.FruitTreeAssetName))
			{
				e.Edit(EditFruitTrees);
			}
			else if (assetName.Equals(Constants.ObjectsAssetName))
			{
				e.Edit(EditObjects);
			}
			else if (assetName.Equals(Constants.WildTreeAssetName))
			{
				e.Edit(EditWildTrees);
			}
		}

		private void OnAssetReady(object? sender, AssetReadyEventArgs e)
		{
			if (!_gameStarted) return;

			var name = e.Name.BaseName;

			if (name.Equals(Constants.FruitTreeAssetName))
			{
				FruitTreeCache = Game1.content.Load<Dictionary<string, FruitTreeData>>(Constants.FruitTreeAssetName);
			}
			else if (name.Equals(Constants.LocationsAssetName))
			{
				LocationCache = Game1.content.Load<Dictionary<string, LocationData>>(Constants.LocationsAssetName);
			}
			else if (name.Equals(Constants.ObjectsAssetName))
			{
				ObjectCache = Game1.content.Load<Dictionary<string, ObjectData>>(Constants.ObjectsAssetName);
			}
			else if (name.Equals(Constants.WildTreeAssetName))
			{
				WildTreeCache = Game1.content.Load<Dictionary<string, WildTreeData>>(Constants.WildTreeAssetName);
			}
		}

		private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
		{
			FruitTreeCache = Game1.content.Load<Dictionary<string, FruitTreeData>>(Constants.FruitTreeAssetName);
			WildTreeCache = Game1.content.Load<Dictionary<string, WildTreeData>>(Constants.WildTreeAssetName);
			ObjectCache = Game1.content.Load<Dictionary<string, ObjectData>>(Constants.ObjectsAssetName);
			LocationCache = Game1.content.Load<Dictionary<string, LocationData>>(Constants.LocationsAssetName);

			// $TODO - Figure out how to register / update GMCM
			_config.RegisterModConfigMenu(Helper, ModManifest);
			_config.UpdateEnabled(Helper);

			_gameStarted = true;
		}

		private void OnPlayerWarped(object? sender, WarpedEventArgs e)
		{
			_artifactPredictions.Clear();

			var mapLoc = e.NewLocation;
			var objsPairs = mapLoc.Objects.Pairs;

			foreach (var objPair in objsPairs)
			{
				var vec = objPair.Key;
				var x = (int)vec.X;
				var y = (int)vec.Y;
				var obj = objPair.Value;

				// $NOTE - Sourced from GameLocation.digUpArtifactSpot
				if (obj.QualifiedItemId == "(O)590")
				{
					var random = Utility.CreateDaySaveRandom(x * 2000, y);
					var locData = mapLoc.GetData();
					var context = new ItemQueryContext(mapLoc, Game1.player, random);
					IEnumerable<ArtifactSpotDropData> artifactSpotDrops = LocationCache["Default"].ArtifactSpots;

					if (locData != null && locData.ArtifactSpots?.Count > 0)
					{
						artifactSpotDrops = artifactSpotDrops.Concat(locData.ArtifactSpots);
					}

					artifactSpotDrops = artifactSpotDrops.OrderBy(p => p.Precedence);
					foreach (var drop in artifactSpotDrops)
					{
						if (!random.NextBool(drop.Chance) || (drop.Condition != null && !GameStateQuery.CheckConditions(drop.Condition, mapLoc, Game1.player, null, null, random)))
						{
							continue;
						}

						var item = ItemQueryResolver.TryResolveRandomItem(drop, context, avoidRepeat: false, null, null, null, delegate (string query, string error)
						{
							Monitor.Log($"Error on query resolve: {mapLoc.NameOrUniqueName} ({drop.ItemId}): {query}{Environment.NewLine}{error}", LogLevel.Debug);
						});

						if (item == null)
						{
							continue;
						}

						Monitor.Log($"[{x}, {y}] {item.QualifiedItemId}", LogLevel.Info);

						if (_forageableTracker.ArtifactForageables.Any(i => i.QualifiedItemId.Equals(item.QualifiedItemId)))
						{
							_artifactPredictions.Add(vec, item.QualifiedItemId);
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
			if (!Context.IsPlayerFree) return;
			if (Game1.currentLocation == null || Game1.player == null) return;
			if (Game1.player.Tile.Equals(_previousTilePosition)) return;

			_previousTilePosition = Game1.player.Tile;
			var playerTileLocationPoint = Game1.player.TilePoint;
			var playerMagnetism = Game1.player.GetAppliedMagneticRadius();
			var radius = _config.UsePlayerMagnetism ? playerMagnetism / Game1.tileSize : _config.ShakeDistance;

			foreach (var vec in GetTilesToCheck(playerTileLocationPoint, radius))
			{
				if (Game1.currentLocation.terrainFeatures.TryGetValue(vec, out var feature))
				{
					if (feature is Tree tree)
					{
						if (tree.stump.Value) continue;
						if (tree.wasShakenToday.Value) continue;
						if (tree.growthStage.Value < 5 || !tree.hasSeed.Value) continue;
						if (!Game1.IsMultiplayer && Game1.player.ForagingLevel < 1) continue;
						if (!tree.isActionable()) continue;

						var itemIds = tree.GetSeedAndSeedItemIds();
						if (_forageableTracker.WildTreeForageables.Any(i => itemIds.Contains(i.QualifiedItemId) && i.IsEnabled))
						{
							tree.performUseAction(tree.Tile);
							Monitor.Log($"Tree shaken: {string.Join(", ", itemIds)}", LogLevel.Debug);
						}
						else
						{
							Monitor.Log($"Tree not shaken: {string.Join(",", itemIds)}", LogLevel.Debug);
						}
					}
					else if (feature is FruitTree fruitTree)
					{
						if (fruitTree.stump.Value) continue;
						if (fruitTree.growthStage.Value < 4) continue;

						var fruitCount = fruitTree.fruit.Count;
						if (fruitCount <= 0 || fruitCount < _config.FruitsReadyToShake) continue;

						var itemIds = fruitTree.GetFruitItemIds();
						if (_forageableTracker.FruitTreeForageables.Any(i => itemIds.Contains(i.QualifiedItemId) && i.IsEnabled))
						{
							fruitTree.performUseAction(fruitTree.Tile);
							Monitor.Log($"Fruit Tree shaken: {string.Join(", ", itemIds)}", LogLevel.Debug);
						}
						else
						{
							Monitor.Log($"Fruit Tree not shaken: {string.Join(",", itemIds)}", LogLevel.Debug);
						}
					}
					else if (feature is Bush bush)
					{
						if (!CheckBush(bush)) continue;

						bush.performUseAction(bush.Tile);
					}
					else if (feature is HoeDirt hoeDirt)
					{

					}
				}

				if (Game1.currentLocation.Objects.TryGetValue(vec, out var obj))
				{
					// Forageable Items
					if (obj.isForage() && obj.IsSpawnedObject && !obj.questItem.Value)
					{
						if (_forageableTracker.ObjectForageables.Any(i => i.QualifiedItemId.Equals(obj.QualifiedItemId) && i.IsEnabled))
						{
							ForageItem(obj, vec, Utility.CreateDaySaveRandom(vec.X, vec.Y * 777f), 7, true);

							Game1.player.currentLocation.removeObject(vec, false);
							Game1.playSound("harvest");
						}
					}
					// Artifact Spot
					else if (obj.QualifiedItemId.Equals("(O)590") && _artifactPredictions.ContainsKey(vec))
					{
						var prediction = _artifactPredictions[vec];
						if (_forageableTracker.ArtifactForageables.Any(i => i.QualifiedItemId.Equals(prediction) && i.IsEnabled))
						{
							Game1.currentLocation.digUpArtifactSpot((int)vec.X, (int)vec.Y, Game1.player);

							if (!Game1.currentLocation.terrainFeatures.ContainsKey(vec))
							{
								Game1.currentLocation.makeHoeDirt(vec, ignoreChecks: true);
							}

							Game1.currentLocation.playSound("hoeHit");
							Game1.currentLocation.removeObject(vec, false);
						}
					}
				}
			}
		}

		//					if ((_config.ForageableToggles & (uint)Constants.ForageableLookup[predictedId]) > 0)
		//					{
		//						Game1.currentLocation.digUpArtifactSpot((int)vec.X, (int)vec.Y, Game1.player);

		//						if (!Game1.currentLocation.terrainFeatures.ContainsKey(vec))
		//						{
		//							Game1.currentLocation.makeHoeDirt(vec, ignoreChecks: true);
		//						}

		//						Game1.currentLocation.playSound("hoeHit");
		//						Game1.currentLocation.removeObject(vec, false);

		//						_trackingCounts[ForageableKey].AddOrIncrement(Constants.SubjectNameLookup[predictedId]);
		//					}
		//					else if (_forageablePredictions.ContainsKey(vec))
		//					{
		//						Monitor.LogOnce(I18n.Log_DisabledConfig(obj.DisplayName, Constants.ConfigNameLookup[predictedId]), LogLevel.Debug);
		//						continue;
		//					}

		//private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
		//{
		//	if (!Context.IsWorldReady || !Context.IsPlayerFree) return;
		//	if (!_config.IsShakerActive || !_config.AnyShakeEnabled) return;
		//	if (Game1.currentLocation == null || Game1.player == null) return;
		//	if (Game1.player.Tile.Equals(previousTilePosition)) return;
		//	if (Game1.CurrentEvent != null && (!Game1.CurrentEvent.playerControlSequence || !Game1.CurrentEvent.canPlayerUseTool())) return;
		//	if (Game1.player.currentLocation.terrainFeatures.Count() == 0
		//		&& Game1.player.currentLocation.largeTerrainFeatures.Count == 0
		//		&& Game1.player.currentLocation.Objects.Count() == 0) return;

		//	previousTilePosition = Game1.player.Tile;
		//	var playerTileLocationPoint = Game1.player.TilePoint;
		//	var playerMagnetism = Game1.player.GetAppliedMagneticRadius();
		//	var radius = _config.UsePlayerMagnetism ? playerMagnetism / Game1.tileSize : _config.ShakeDistance;

		//	if (_config.AnySeedTreeEnabled || _config.AnyFruitTreeEnabled || _config.AnyBushEnabled || _config.AnyForageablesEnabled)
		//	{
		//		foreach (var vec in GetTilesToCheck(playerTileLocationPoint, radius))
		//		{
		//			// Seed Trees, Fruit Trees, Bushes, Spring Onions, Ginger
		//			if (Game1.currentLocation.terrainFeatures.TryGetValue(vec, out var feature)
		//				&& feature is Tree or FruitTree or Bush or HoeDirt
		//				&& !_ignoredFeatures.Contains(feature)
		//				&& !_interactedFeatures.Contains(feature))
		//			{
		//				var featureTileLocation = feature.Tile;
		//				var toIgnore = false;

		//				switch (feature)
		//				{
		//					// Tree Cases
		//					case Tree treeFeature:
		//						if (!_config.AnySeedTreeEnabled) continue;
		//						if (treeFeature.stump.Value) toIgnore = true;
		//						if (treeFeature.growthStage.Value < 5 || !treeFeature.hasSeed.Value) toIgnore = true;
		//						if (Game1.player.ForagingLevel < 1) toIgnore = true;

		//						if (!treeFeature.isActionable())
		//						{
		//							Monitor.Log($"Tree of type [{treeFeature.treeType.Value}] not shaken because the game deemed it was not actionable. It is likely tapped or not mature yet.", LogLevel.Trace);
		//							toIgnore = true;
		//						}

		//						if (toIgnore)
		//						{
		//							_ignoredFeatures.Add(treeFeature);
		//							continue;
		//						}

		//						//treeFeature.GetData()

		//						switch (treeFeature.treeType.Value)
		//						{
		//							// Oak Tree
		//							case "1":
		//							case "4": // Winter
		//								if (!_config.ShakeOakTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_OakTrees(), Constants.OakName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[SeedTreeKey].AddOrIncrement(I18n.Subject_OakTrees());
		//								break;

		//							// Maple Tree
		//							case "2":
		//							case "5": // Winter
		//								if (!_config.ShakeMapleTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_MapleTrees(), Constants.MapleName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[SeedTreeKey].AddOrIncrement(I18n.Subject_MapleTrees());
		//								break;

		//							// Pine Tree
		//							case "3":
		//								if (!_config.ShakePineTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_PineTrees(), Constants.PineName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[SeedTreeKey].AddOrIncrement(I18n.Subject_PineTrees());
		//								break;

		//							// Mahogany Tree
		//							case "8":
		//								if (!_config.ShakeMahoganyTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_MahoganyTrees(), Constants.MahoganyName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[SeedTreeKey].AddOrIncrement(I18n.Subject_MahoganyTrees());
		//								break;

		//							// Palm Tree
		//							case "6": // Desert
		//							case "9": // Island
		//								if (!_config.ShakePalmTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_PalmTrees(), Constants.PalmName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[SeedTreeKey].AddOrIncrement(I18n.Subject_PalmTrees());
		//								break;

		//							default:
		//								Monitor.Log($"Unknown Tree type: [{treeFeature.treeType.Value}]", LogLevel.Warn);
		//								_ignoredFeatures.Add(treeFeature);
		//								continue;
		//						}

		//						treeFeature.performUseAction(featureTileLocation);
		//						_interactedFeatures.Add(treeFeature);
		//						break;

		//					// Fruit Tree Cases
		//					case FruitTree fruitTree:
		//						if (!_config.AnyFruitTreeEnabled) continue;
		//						if (fruitTree.stump.Value) toIgnore = true;
		//						if (fruitTree.growthStage.Value < 4) toIgnore = true;

		//						if (fruitTree.fruit.Count < _config.FruitsReadyToShake)
		//						{
		//							Monitor.LogOnce(I18n.Log_FruitNotReady(_config.FruitsReadyToShake, I18n.Option_FruitsReadyToShake_Name()), LogLevel.Debug);
		//							toIgnore = true;
		//						}

		//						if (!fruitTree.isActionable())
		//						{
		//							Monitor.Log($"A fruit tree of type [{fruitTree.treeId.Value}] was marked as not actionable. This shouldn't be possible.", LogLevel.Warn);
		//							Monitor.Log($"Type: [{fruitTree.treeId.Value}]; Location: [{fruitTree.Location.Name}]; Tile Location: [{fruitTree.Tile}]; Fruit Count: [{fruitTree.fruit.Count}]; Fruit Indices: [{String.Join(",", fruitTree.fruit)}]", LogLevel.Debug);
		//						}

		//						if (toIgnore)
		//						{
		//							_ignoredFeatures.Add(fruitTree);
		//							continue;
		//						}

		//						switch (fruitTree.treeId.Value)
		//						{
		//							// Cherry Tree
		//							case "0":
		//								if (!_config.ShakeCherryTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_CherryTrees(), Constants.CherryName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_CherryTrees());
		//								break;

		//							// Apricot Tree
		//							case "1":
		//								if (!_config.ShakeApricotTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_ApricotTrees(), Constants.ApricotName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_ApricotTrees());
		//								break;

		//							// Orange Tree
		//							case "2":
		//								if (!_config.ShakeOrangeTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_OrangeTrees(), Constants.OrangeName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_OrangeTrees());
		//								break;

		//							// Peach Tree
		//							case "3":
		//								if (!_config.ShakePeachTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_PeachTrees(), Constants.PeachName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_PeachTrees());
		//								break;

		//							// Pomegranate Tree
		//							case "4":
		//								if (!_config.ShakePomegranateTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_PomegranateTrees(), Constants.PomegranateName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_PomegranateTrees());
		//								break;

		//							// Apple Tree
		//							case "5":
		//								if (!_config.ShakeAppleTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_AppleTrees(), Constants.AppleName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_AppleTrees());
		//								break;

		//							// Banana Tree
		//							case "7":
		//								if (!_config.ShakeBananaTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_BananaTrees(), Constants.BananaName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_BananaTrees());
		//								break;

		//							// Mango Tree
		//							case "8":
		//								if (!_config.ShakeMangoTrees)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_MangoTrees(), Constants.MangoName), LogLevel.Debug);
		//									continue;
		//								}

		//								_trackingCounts[FruitTreeKey].AddOrIncrement(I18n.Subject_MangoTrees());
		//								break;

		//							default:
		//								Monitor.Log($"Unknown Fruit Tree type: [{fruitTree.treeId.Value}]", LogLevel.Warn);
		//								_ignoredFeatures.Add(fruitTree);
		//								continue;
		//						}

		//						fruitTree.performUseAction(featureTileLocation);
		//						_interactedFeatures.Add(fruitTree);
		//						break;

		//					// Bush Cases
		//					case Bush bushFeature:
		//						if (!CheckBush(bushFeature)) continue;

		//						bushFeature.performUseAction(featureTileLocation);
		//						_interactedFeatures.Add(bushFeature);
		//						break;

		//					// Forageable Cases
		//					case HoeDirt hoeDirtFeature:
		//						if (!_config.ForageSpringOnions && !_config.ForageGinger) continue;
		//						if (hoeDirtFeature.crop == null || !hoeDirtFeature.crop.forageCrop.Value || hoeDirtFeature.crop.whichForageCrop.Value.IsNullOrEmpty()) toIgnore = true;

		//						if (toIgnore)
		//						{
		//							Monitor.LogOnce($"Ignored {hoeDirtFeature.crop?.indexOfHarvest.Value ?? "empty hoe dirt"}", LogLevel.Debug);
		//							_ignoredFeatures.Add(hoeDirtFeature);
		//							continue;
		//						}

		//						var whichCrop = hoeDirtFeature.crop?.whichForageCrop.Value ?? "-1";
		//						Vector2 tile;
		//						switch (whichCrop)
		//						{
		//							// Spring Onion
		//							case "1":
		//								if (!_config.ForageSpringOnions)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_SpringOnions(), Constants.SpringOnionName), LogLevel.Debug);
		//									continue;
		//								}

		//								tile = hoeDirtFeature.Tile;
		//								var x = (int)tile.X;
		//								var y = (int)tile.Y;

		//								ForageItem(ItemRegistry.Create<Object>("(O)399"), tile, Utility.CreateDaySaveRandom(x * 1000, y * 2000), 3);
		//								hoeDirtFeature.destroyCrop(false);
		//								Game1.playSound("harvest");

		//								_trackingCounts[ForageableKey].AddOrIncrement(I18n.Subject_SpringOnions());
		//								break;

		//							// Ginger
		//							case "2":
		//								if (!_config.ForageGinger)
		//								{
		//									Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_GingerRoots(), Constants.GingerName), LogLevel.Debug);
		//									continue;
		//								}

		//								if (_config.RequireHoe && !Game1.player.Items.Any(i => i is Hoe))
		//								{
		//									if (_nextErrorMessage < DateTime.UtcNow)
		//									{
		//										Game1.addHUDMessage(new HUDMessage(I18n.Message_MissingHoe(I18n.Subject_GingerRoots()), HUDMessage.error_type));
		//										_nextErrorMessage = DateTime.UtcNow.AddSeconds(10);
		//									}

		//									Monitor.LogOnce(I18n.Log_MissingHoe(I18n.Subject_GingerRoots(), I18n.Option_RequireHoe_Name(" ")), LogLevel.Debug);
		//									continue;
		//								}

		//								tile = hoeDirtFeature.Tile;

		//								hoeDirtFeature.crop?.hitWithHoe((int)tile.X, (int)tile.Y, hoeDirtFeature.Location, hoeDirtFeature);
		//								hoeDirtFeature.destroyCrop(false);

		//								_trackingCounts[ForageableKey].AddOrIncrement(I18n.Subject_GingerRoots());
		//								break;

		//							default:
		//								Monitor.Log($"No good case: {whichCrop}");
		//								continue;
		//						}

		//						_interactedFeatures.Add(hoeDirtFeature);
		//						break;

		//					// This should never happen
		//					default:
		//						Monitor.Log("I am an unknown terrain feature, ignore me I guess...", LogLevel.Debug);
		//						break;
		//				}
		//			}

		//			// Forageables (except Spring Onions, Ginger)
		//			if (_config.AnyForageablesEnabled && Game1.currentLocation.Objects.TryGetValue(vec, out var obj))
		//			{
		//				if (obj.isForage() && obj.IsSpawnedObject && !obj.questItem.Value)
		//				{
		//					if ((_config.ForageableToggles & (uint)Constants.ForageableLookup[obj.QualifiedItemId]) > 0)
		//					{
		//						ForageItem(obj, vec, Utility.CreateDaySaveRandom(vec.X, vec.Y * 777f), 7, true);

		//						Game1.player.currentLocation.removeObject(vec, showDestroyedObject: false);
		//						Game1.playSound("harvest");

		//						_trackingCounts[ForageableKey].AddOrIncrement(Constants.SubjectNameLookup[obj.QualifiedItemId]);
		//					}
		//					else
		//					{
		//						Monitor.LogOnce(I18n.Log_DisabledConfig(obj.DisplayName, Constants.ConfigNameLookup[obj.QualifiedItemId]), LogLevel.Debug);
		//						continue;
		//					}
		//				}
		//				else if (obj.QualifiedItemId == "(O)590")
		//				{
		//					if (!_forageablePredictions.ContainsKey(vec)) continue;

		//					var predictedId = _forageablePredictions[vec];

		//					if (_config.RequireHoe && !Game1.player.Items.Any(i => i is Hoe))
		//					{
		//						if (_nextErrorMessage < DateTime.UtcNow)
		//						{
		//							Game1.addHUDMessage(new HUDMessage(I18n.Message_MissingHoe(Constants.SubjectNameLookup[predictedId]), HUDMessage.error_type));
		//							_nextErrorMessage = DateTime.UtcNow.AddSeconds(10);
		//						}

		//						Monitor.LogOnce(I18n.Log_MissingHoe(Constants.SubjectNameLookup[predictedId], I18n.Option_RequireHoe_Name(" ")), LogLevel.Debug);
		//						continue;
		//					}

		//					if ((_config.ForageableToggles & (uint)Constants.ForageableLookup[predictedId]) > 0)
		//					{
		//						Game1.currentLocation.digUpArtifactSpot((int)vec.X, (int)vec.Y, Game1.player);

		//						if (!Game1.currentLocation.terrainFeatures.ContainsKey(vec))
		//						{
		//							Game1.currentLocation.makeHoeDirt(vec, ignoreChecks: true);
		//						}

		//						Game1.currentLocation.playSound("hoeHit");
		//						Game1.currentLocation.removeObject(vec, false);

		//						_trackingCounts[ForageableKey].AddOrIncrement(Constants.SubjectNameLookup[predictedId]);
		//					}
		//					else if (_forageablePredictions.ContainsKey(vec))
		//					{
		//						Monitor.LogOnce(I18n.Log_DisabledConfig(obj.DisplayName, Constants.ConfigNameLookup[predictedId]), LogLevel.Debug);
		//						continue;
		//					}
		//				}
		//			}
		//		}

		//	}

		//	if (_config.AnyBushEnabled)
		//	{
		//		foreach (var feature in Game1.player.currentLocation.largeTerrainFeatures)
		//		{
		//			if (feature is not Bush bush) continue;

		//			var location = bush.Tile;

		//			if (!IsInRange(playerTileLocationPoint, location, radius)
		//				|| _interactedFeatures.Contains(bush) || _ignoredFeatures.Contains(bush))
		//			{
		//				continue;
		//			}

		//			if (CheckBush(bush))
		//			{
		//				bush.performUseAction(location);
		//				_interactedFeatures.Add(feature);
		//			}
		//		}
		//	}
		//}

		private void OnDayStarted(object? sender, DayStartedEventArgs e)
		{
			_previousTilePosition = Game1.player.Tile;
		}

		private void OnDayEnding(object? sender, DayEndingEventArgs e)
		{
			StringBuilder statMessage = new($"{Environment.NewLine}{Utility.getDateString()}:{Environment.NewLine}");
			statMessage.AppendLine(I18n.Log_Eod_TotalStat(_trackingCounts.SumAll()));

			foreach (var category in _trackingCounts)
			{
				if (category.Value.Count > 0)
				{
					statMessage.AppendLine($"[{category.Value.SumAll()}] {category.Key}:");

					foreach (var interactable in category.Value)
					{
						if (interactable.Value <= 0)
						{
							Monitor.Log($"Invalid forageable value for {interactable.Key}: {interactable.Value}. How did we get here?", LogLevel.Warn);
							continue;
						}

						statMessage.AppendLine(I18n.Log_Eod_Stat(interactable.Value, interactable.Key));
					}
				}

				category.Value.Clear();
			}

			Monitor.Log(statMessage.ToString(), LogLevel.Info);

			// Reset
			Monitor.Log("Resetting daily counts...", LogLevel.Trace);

			//_ignoredFeatures.Clear();
			//_interactedFeatures.Clear();
		}

		private void OnButtonsChanged(object? sender, ButtonsChangedEventArgs e)
		{
			if (Game1.activeClickableMenu == null)
			{
				if (_config.ToggleShakerKeybind.JustPressed())
				{
					_config.IsShakerActive = !_config.IsShakerActive;
					Task.Run(() => Helper.WriteConfig(_config)).ContinueWith((t) =>
						Monitor.Log(t.Status == TaskStatus.RanToCompletion ? "Config saved successfully!" : $"Saving config unsuccessful {t.Status}", LogLevel.Debug));

					Func<string> state = _config.IsShakerActive ? I18n.State_Activated : I18n.State_Deactivated;
					var message = I18n.Message_AutoShakerToggled(state());

					Monitor.Log(message, LogLevel.Info);
					Game1.addHUDMessage(new HUDMessage(message) { noIcon = true });
				}
			}
		}

		private bool CheckBush(Bush bush)
		{
			var toIgnore = false;

			//if (!_config.AnyBushEnabled) return false;
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
				//_ignoredFeatures.Add(bush);
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

					//if (season.Equals("spring") && !_config.ShakeSalmonberryBushes)
					//{
					//	//Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_SalmonberryBushes(), Constants.SalmonberryName), LogLevel.Debug);
					//	return false;
					//}

					//if (season.Equals("fall") && !_config.ShakeBlackberryBushes)
					//{
					//	//Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_BlackberryBushes(), Constants.BlackberryName), LogLevel.Debug);
					//	return false;
					//}

					_trackingCounts[BushKey].AddOrIncrement(season.Equals("spring") ? I18n.Subject_SalmonberryBushes() : I18n.Subject_BlackberryBushes());
					break;

				// Tea Bushes
				case 3:
					//if (!_config.ShakeTeaBushes)
					//{
					//	//Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_TeaBushes(), Constants.TeaName), LogLevel.Debug);
					//	return false;
					//}

					_trackingCounts[BushKey].AddOrIncrement(I18n.Subject_TeaBushes());
					break;

				// Walnut Bushes
				case 4:
					//if (!_config.ShakeWalnutBushes)
					//{
					//	//Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_WalnutBushes(), Constants.WalnutName), LogLevel.Debug);
					//	return false;
					//}

					_trackingCounts[BushKey].AddOrIncrement(I18n.Subject_WalnutBushes());
					break;

				default:
					Monitor.Log($"Unknown Bush size: [{bush.size.Value}]", LogLevel.Warn);
					//_ignoredFeatures.Add(bush);
					return false;
			}

			return true;
		}

		private void ParseAssets<T>(Dictionary<string, T> data)
		{
			if (data is Dictionary<string, FruitTreeData> fruitTreeData)
			{
				_forageableTracker.FruitTreeForageables.Clear();
				_forageableTracker.FruitTreeForageables.AddRange(ForageableItem.Parse(fruitTreeData));
				_forageableTracker.FruitTreeForageables.SortByDisplayName();
			}
			else if (data is Dictionary<string, LocationData> locationData)
			{
				if (ObjectCache == null ||  ObjectCache.Count == 0)
				{
					ObjectCache = Game1.content.Load<Dictionary<string, ObjectData>>(Constants.ObjectsAssetName);
				}

				_forageableTracker.ArtifactForageables.Clear();
				_forageableTracker.ArtifactForageables.AddRange(ForageableItem.Parse(ObjectCache, locationData));
				_forageableTracker.ArtifactForageables.SortByDisplayName();
			}
			else if (data is Dictionary<string, ObjectData> objectData)
			{
				_forageableTracker.ObjectForageables.Clear();
				_forageableTracker.ObjectForageables.AddRange(ForageableItem.Parse(objectData));
				_forageableTracker.ObjectForageables.SortByDisplayName();

				if (LocationCache != null && LocationCache.Count > 0)
				{
					_forageableTracker.ArtifactForageables.Clear();
					_forageableTracker.ArtifactForageables.AddRange(ForageableItem.Parse(objectData, LocationCache));
					_forageableTracker.ArtifactForageables.SortByDisplayName();
				}
			}
			else if (data is Dictionary<string, WildTreeData> wildTreeData)
			{
				_forageableTracker.WildTreeForageables.Clear();
				_forageableTracker.WildTreeForageables.AddRange(ForageableItem.Parse(wildTreeData));
				_forageableTracker.WildTreeForageables.SortByDisplayName();
			}

			if (_config != null && _gameStarted)
			{
				_config.UpdateEnabled(Helper);
			}
		}

		private static void EditFruitTrees(IAssetData asset)
		{
			var fruitTreeData = asset.AsDictionary<string, FruitTreeData>();
			foreach (var fruitTree in fruitTreeData.Data)
			{
				fruitTree.Value.CustomFields.AddOrUpdate(Constants.CustomFieldForageableKey, "true");
			}
		}

		private void EditObjects(IAssetData asset)
		{
			var objectData = asset.AsDictionary<string, ObjectData>();
			foreach (var obj in objectData.Data)
			{
				if (_ignoreItemIds.Any(i => obj.Key.Equals(i.Substring(3)))) continue;

				if ((obj.Value.ContextTags?.Contains("forage_item") ?? false)
					|| _overrideItemIds.Any(i => obj.Key.Equals(i.Substring(3))))
				{
					obj.Value.CustomFields.AddOrUpdate(Constants.CustomFieldForageableKey, "true");

					if (Constants.KnownCategoryLookup.TryGetValue(obj.Key, out var value))
					{
						obj.Value.CustomFields.AddOrUpdate(Constants.CustomFieldCategoryKey, value);
					}
				}
			}
		}

		private static void EditWildTrees(IAssetData asset)
		{
			var wildTreeData = asset.AsDictionary<string, WildTreeData>();
			foreach (var wildTree in wildTreeData.Data)
			{
				// Just say no to mushroom trees
				if (wildTree.Key.Equals("7")) continue;

				wildTree.Value.CustomFields.AddOrUpdate(Constants.CustomFieldForageableKey, "true");
			}
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
