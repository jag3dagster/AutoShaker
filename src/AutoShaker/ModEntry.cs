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
using StardewValley.GameData.FruitTrees;
using StardewValley.GameData.Locations;
using StardewValley.GameData.Objects;
using StardewValley.GameData.WildTrees;
using StardewValley.Internal;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using AutoShaker.Classes;
using AutoShaker.Helpers;

using Constants = AutoShaker.Helpers.Constants;
using Object = StardewValley.Object;

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
					switch (feature)
					{
						case Tree tree:
							if (tree.stump.Value) continue;
							if (tree.wasShakenToday.Value) continue;
							if (tree.growthStage.Value < 5 || !tree.hasSeed.Value) continue;
							if (!Game1.IsMultiplayer && Game1.player.ForagingLevel < 1) continue;
							if (!tree.isActionable()) continue;

							var seedItemIds = tree.GetSeedAndSeedItemIds();
							if (_forageableTracker.WildTreeForageables.Any(i => seedItemIds.Contains(i.QualifiedItemId) && i.IsEnabled))
							{
								tree.performUseAction(tree.Tile);
								Monitor.Log($"Tree shaken: {string.Join(", ", seedItemIds)}", LogLevel.Debug);

								foreach (var id in seedItemIds)
								{
									var name = id;

									if (_forageableTracker.WildTreeForageables.TryGetItem(id, out var wtItem))
									{
										name = wtItem?.DisplayName ?? id;
									}

									_trackingCounts[Helper.Translation.Get(WildTreeKey)].AddOrIncrement(name);
								}
							}
							else
							{
								Monitor.Log($"Tree not shaken: {string.Join(",", seedItemIds)}", LogLevel.Debug);
							}

							break;

						case FruitTree fruitTree:
							if (fruitTree.stump.Value) continue;
							if (fruitTree.growthStage.Value < 4) continue;

							var fruitCount = fruitTree.fruit.Count;
							if (fruitCount <= 0 || fruitCount < _config.FruitsReadyToShake) continue;

							var fruitItemIds = fruitTree.GetFruitItemIds();
							if (_forageableTracker.FruitTreeForageables.Any(i => fruitItemIds.Contains(i.QualifiedItemId) && i.IsEnabled))
							{
								fruitTree.performUseAction(fruitTree.Tile);
								Monitor.Log($"Fruit Tree shaken: {string.Join(", ", fruitItemIds)}", LogLevel.Debug);

								foreach (var id in fruitItemIds)
								{
									var name = id;

									if (_forageableTracker.FruitTreeForageables.TryGetItem(id, out var ftItem))
									{
										name = ftItem?.DisplayName ?? id;
									}

									_trackingCounts[Helper.Translation.Get(FruitTreeKey)].AddOrIncrement(name);
								}
							}
							else
							{
								Monitor.Log($"Fruit Tree not shaken: {string.Join(",", fruitItemIds)}", LogLevel.Debug);
							}

							break;

						case Bush bush:
							if (!CheckBush(bush)) continue;

							bush.performUseAction(bush.Tile);

							break;

						case HoeDirt hoeDirt:
							if (!(hoeDirt.crop?.forageCrop.Value ?? false) || (hoeDirt.crop?.whichForageCrop.Value.IsNullOrEmpty() ?? true)) continue;
							if (!_forageableTracker.ObjectForageables.Any(i =>
							{
								if (i.IsEnabled && i.CustomFields.TryGetValue(Constants.CustomFieldCategoryKey, out var category))
								{
									return category == "Special";
								}

								return false;
							})) continue;

							Vector2 tile;
							var whichCrop = hoeDirt.crop.whichForageCrop.Value;

							switch (whichCrop)
							{
								case Crop.forageCrop_springOnionID:
									var springOnion = _forageableTracker.ObjectForageables.FirstOrDefault(i => i.QualifiedItemId == "(O)399");

									if (springOnion != default(ForageableItem) && springOnion.IsEnabled)
									{
										tile = hoeDirt.Tile;
										var x = (int)tile.X;
										var y = (int)tile.Y;

										ForageItem(ItemRegistry.Create<Object>("(O)399"), tile, Utility.CreateDaySaveRandom(x * 1000, y * 2000), 3);
										hoeDirt.destroyCrop(false);
										Game1.playSound("harvest");

										_trackingCounts[ForageableKey].AddOrIncrement(springOnion.DisplayName);
									}

									break;

								case Crop.forageCrop_gingerID:
									var ginger = _forageableTracker.ObjectForageables.FirstOrDefault(i => i.QualifiedItemId == "(O)829");

									if (ginger != default(ForageableItem) && ginger.IsEnabled)
									{
										if (_config.RequireHoe && !Game1.player.Items.Any(i => i is Hoe))
										{
											if (_nextErrorMessage < DateTime.UtcNow)
											{
												Game1.addHUDMessage(new HUDMessage(I18n.Message_MissingHoe(I18n.Subject_GingerRoots()), HUDMessage.error_type));
												_nextErrorMessage = DateTime.UtcNow.AddSeconds(10);
											}

											Monitor.LogOnce(I18n.Log_MissingHoe(I18n.Subject_GingerRoots(), I18n.Option_RequireHoe_Name(" ")), LogLevel.Debug);
											continue;
										}

										tile = hoeDirt.Tile;
										hoeDirt.crop?.hitWithHoe((int)tile.X, (int)tile.Y, hoeDirt.Location, hoeDirt);
										hoeDirt.destroyCrop(false);

										_trackingCounts[ForageableKey].AddOrIncrement(ginger.DisplayName);
									}

									break;

								default:
									Monitor.Log($"No good case: {whichCrop}");
									break;
							}

							break;
					}
				}

				if (Game1.currentLocation.Objects.TryGetValue(vec, out var obj))
				{
					// Forageable Items
					if (obj.isForage() && obj.IsSpawnedObject && !obj.questItem.Value)
					{
						if (_forageableTracker.ObjectForageables.TryGetItem(obj.QualifiedItemId, out var objItem) && (objItem?.IsEnabled ?? false))
						{
							ForageItem(obj, vec, Utility.CreateDaySaveRandom(vec.X, vec.Y * 777f), 7, true);

							Game1.player.currentLocation.removeObject(vec, false);
							Game1.playSound("harvest");

							_trackingCounts[ForageableKey].AddOrIncrement(objItem.DisplayName);
						}
					}
					// Artifact Spot
					else if (obj.QualifiedItemId.Equals("(O)590") && _artifactPredictions.ContainsKey(vec))
					{
						var prediction = _artifactPredictions[vec];

						if (_forageableTracker.ArtifactForageables.TryGetItem(prediction, out var objItem) && (objItem?.IsEnabled ??  false))
						{
							Game1.currentLocation.digUpArtifactSpot((int)vec.X, (int)vec.Y, Game1.player);

							if (!Game1.currentLocation.terrainFeatures.ContainsKey(vec))
							{
								Game1.currentLocation.makeHoeDirt(vec, ignoreChecks: true);
							}

							Game1.currentLocation.playSound("hoeHit");
							Game1.currentLocation.removeObject(vec, false);

							_trackingCounts[ForageableKey].AddOrIncrement(objItem.DisplayName);
						}
					}
				}

				var largeTerrainFeature = Game1.currentLocation.getLargeTerrainFeatureAt((int)vec.X, (int)vec.Y);
				if (largeTerrainFeature != null && largeTerrainFeature is Bush largeBush)
				{
					if (CheckBush(largeBush))
					{
						largeBush.performUseAction(vec);
					}
				}
			}
		}

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
					statMessage.AppendLine($"[{category.Value.SumAll()}] {Helper.Translation.Get(category.Key)}:");

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
			if (!_config.AnyBushEnabled()) return false;
			if (bush.townBush.Value) return false;
			if (!bush.inBloom()) return false;
			if (bush.tileSheetOffset.Value != 1) return false;

			if (!bush.isActionable())
			{
				Monitor.Log($"A bush feature of size [{bush.size.Value}] was marked as not actionable. This shouldn't be possible.", LogLevel.Warn);
				Monitor.Log($"Size: [{bush.size.Value}]; Location: [{bush.Location.Name}]; Tile Location: [{bush.Tile}]; Town Bush: [{bush.townBush.Value}]", LogLevel.Debug);
			}

			switch (bush.size.Value)
			{
				// Forageable Bushes
				case 0:
				case 1:
				case 2:
					var season = Game1.currentSeason;

					if (!season.IEquals("spring") && !season.IEquals("fall")) return false;

					if (season.IEquals("spring") && !_config.GetSalmonberryBushesEnabled())
					{
						Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_SalmonberryBushes(), Constants.SalmonberryName), LogLevel.Debug);
						return false;
					}

					if (season.IEquals("spring") && !_config.GetBlackberryBushesEnabled())
					{
						Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_BlackberryBushes(), Constants.BlackberryName), LogLevel.Debug);
						return false;
					}

					var subjectName = season.IEquals("spring")
						? I18n.Subject_SalmonberryBushes()
						: I18n.Subject_BlackberryBushes();

					_trackingCounts[BushKey].AddOrIncrement(subjectName);
					break;

				// Tea Bushes
				case 3:
					if (!_config.GetTeaBushesEnabled())
					{
						Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_TeaBushes(), Constants.TeaName), LogLevel.Debug);
						return false;
					}

					_trackingCounts[BushKey].AddOrIncrement(I18n.Subject_TeaBushes());
					break;

				// Walnut Bushes
				case 4:
					if (!_config.GetWalnutBushesEnabled())
					{
						Monitor.LogOnce(I18n.Log_DisabledConfig(I18n.Subject_WalnutBushes(), Constants.WalnutName), LogLevel.Debug);
						return false;
					}

					_trackingCounts[BushKey].AddOrIncrement(I18n.Subject_WalnutBushes());
					break;

				default:
					Monitor.Log($"Unknown Bush size: [{bush.size.Value}]", LogLevel.Warn);
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
				_config.RegisterModConfigMenu(Helper, ModManifest);
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

		private static IEnumerable<Vector2> GetTilesToCheck(Point playerLocation, int radius)
		{
			for (int x = Math.Max(playerLocation.X - radius, 0); x <= playerLocation.X + radius; x++)
				for (int y = Math.Max(playerLocation.Y - radius, 0); y <= playerLocation.Y + radius; y++)
					yield return new Vector2(x, y);

			yield break;
		}
	}
}
