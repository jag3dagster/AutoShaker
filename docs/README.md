**AutoShaker** is an open-source mod for [Stardew Valley](https://stardewvalley.net) that allows players to automatically shake trees and bushes simply by moving nearby to them.

## Documentation
### Overview
This mod checks for:
* Bushes that are currently blooming with berries
* Fruit trees that currently have fruit on them
* Trees that have a seed available to be shaken down

NOTE: This includes trees with hazelnuts, coconuts, and golden coconuts

### Config
* IsShakerActive
    * Whether or not the AutoShaker will shake bushes / trees
    * *Default* - true
* ToggleShaker
    * Which button will toggle the AutoShaker on and off when pressed while holding either Alt key
    * *Default* - H
* ShakeTrees
    * Whether or not the AutoShaker should try and shake trees
    * *Default* - true
* ShakeBushes
    * Whether or not the AutoShaker should try and shake bushes
    * *Default* - true
* UsePlayerMagnetism
    * Whether or not to use the players current magnetism distance when looking for things to shake
    * This overrides the *ShakeDistance* config value
    * *Default* - false
* ShakeDistance
    * Distance in tiles that the AutoShaker will look for things to shake
    * This value is ignored if *UsePlayerMagnetism* is set to **true**
    * *Default* - 1


### Install
1. Install the latest version of [SMAPI](https://smapi.io)
    1. [Nexus Mirror](https://www.nexusmods.com/stardewvalley/mods/2400)
    2. [GitHub Mirror](https://github.com/Pathoschild/SMAPI/releases)
2. *OPTIONAL* Install the latest version of [Generic Mod Config Menu](https://spacechase0.com/mods/stardew-valley/generic-mod-config-menu/)
    1. [Nexus Mirror](https://www.nexusmods.com/stardewvalley/mods/5098)
3. Install this mod by unzipping the mod folder into 'Stardew Valley/Mods'
4. Launch teh game using SMAPI

### Compatibility
* Compatible with...
    - Stardew Valley 1.5 or later
    - SMAPI 3.0.0 or later
* No known mod conflicts
    - If you find one please feel free to notify me here or on the [Nexus Mod](https://www.nexusmods.com/stardewvalley/mods/7736) site

## Limitations
### Solo + Multiplayer
* Any bushes that have the potential to have berries on them will be shaken during berry seasons whether or not they have berries on them

## Releases
Releases can be found on [GitHub](https://github.com/jag3dagster/AutoShaker/releases) and on the [Nexus Mod](https://www.nexusmods.com/stardewvalley/mods/7736) site
### 1.0.0
* Initial release
* Allows players to automatically shake trees and bushes by moving nearby to them
* Working as of Stardew Valley 1.5.3
