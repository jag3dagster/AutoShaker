# Configuration Values

## General Options
### IsShakerActive
- Whether or not the AutoShaker will shake trees and bushes and forage for objects
- *Default* - **true**
### ToggleShakerKeybind
- Which button(s) will toggle the AutoShaker on and off when pressed while holding either Alt key
- *Default* - **LeftAlt + H**, **RightAlt + H**
### UsePlayerMagnetism
- Whether or not to use the player's current magnetism distance when looking for things to shake or forage
- *Default* - **false**
- *Note* - This overrides the (#ShakeDistance) config value
### ShakeDistance
- Distance, in tiles, that the AutoShaker will look for things to shake or forage
- *Default* - **2**
- *Note* - This value is ignored if (#UsePlayerMagnetism) is set to **true**
### RequireHoe
- Whether or not a player is required to have a hoe in their inventory to dig up Ginger roots, Snow Yams, and Winter Roots
- *Default* - **true**

## Seed Tree Toggles
### ShakeMahoganyTrees
- Whether or not to shake Mahogany trees for Mahogany Seeds
- *Default* - **true**
### ShakeMapleTrees
- Whether or not to shake Maple trees for Maple Seeds and Hazelnuts
- *Default* - **true**
- *Note* - This is separate from foraged Hazelnuts
### ShakeOakTrees
- Whether or not to shake Oak trees for Acorns
- *Default* - **true**
### ShakePalmTrees
- Whether or not to shake Palm trees for Coconuts
- *Default* - **true**
- *Note* - This is separate from foraged Coconuts
### ShakePineTrees
- Whether or not to shake Pine trees for Pine Cones
- *Default* - **true**

## Fruit Tree Toggles
### FruitsReadyToShake
- Minimum number of fruits to be ready before shaking fruit trees
- *Default* - **1**
- *Minimum* - *1*
- *Maximum* - *3*
### ShakeAppleTrees
- Whether or not to shake Apple trees for Apples
- *Default* - **true**
### ShakeApricotTrees
- Whether or not to shake Apricot trees for Apricots
- *Default* - **true**
### ShakeBananaTrees
- Whether or not to shake Banana trees for Bananas
- *Default* - **true**
### ShakeCherryTrees
- Whether or not to shake Cherry trees for Cherries
- *Default* - **true**
### ShakeMangoTrees
- Whether or not to shake Mango trees for Mangos
- *Default* - **true**
### ShakeOrangeTrees
- Whether or not to shake Orange trees for Oranges
- *Default* - **true**
### ShakePeachTrees
- Whether or not to shake Peach trees for Peaches
- *Default* - **true**
### ShakePomegranateTrees
- Whether or not to shake Pomegranate trees for Pomegranates
- *Default* - **true**

## Bush Toggles
### ShakeSalmonberryBushes
- Whether or not to shake Salmonberry bushes for Salmonberries
- *Default* - **true**
### ShakeBlackberryBushes
- Whether or not to shake Blackberry bushes for Blackberries
- *Default* - **true**
### ShakeTeaBushes
- Whether or not to shake Tea bushes for Tea Leaves
- *Default* - **true**
### ShakeWalnutBushes
- Whether or not to shake Walnut bushes for Golden Walnuts
- *Default* - **false**

## Forageable Toggles
### Spring Forageables
#### ForageDaffodils
- Whether or not to forage Daffodils
- *Default* - **false**
#### ForageDandelions
- Whether or not to forage Dandelions
- *Default* - **false**
#### ForageLeeks
- Whether or not to forage Leeks
- *Default* - **false**
#### ForageSpringOnions
- Whether or not to forage Spring Onions
- *Default* - **false**
#### ForageWildHorseradishes
- Whether or not to forage Wild Horseradishes
- *Default* - **false**
### Summer Forageables
#### ForageGrapes
- Whether or not to forage Grapes
- *Default* - **false**
#### ForageSpiceBerries
- Whether or not to forage Spice Berries
- *Default* - **false**
#### ForageSweetPeas
- Whether or not to forage Sweet Peas
- *Default* - **false**
### Fall Forageables
#### ForageHazelnuts
- Whether or not to forage Hazelnuts
- *Default* - **false**
- *Note* - This is separate from Hazelnuts that come from shaking Maple trees
#### ForageWildPlums
- Whether or not to forage Wild Plums
- *Default* - **false**
### Winter Forageables
#### ForageCrocuses
- Whether or not to forage Crocuses
- *Default* - **false**
#### ForageCrystalFruits
- Whether or not to forage Crystal Fruits
- *Default* - **false**
#### ForageHolly
- Whether or not to forage Holly
- *Default* - **false**
#### ForageSnowYams
- Whether or not to forage Snow Yams
- *Default* - **false**
- *Note* - This checks Artifact Spots, a.k.a 'worms', 'stems', or 'twigs', and will only dig up the spot if it holds a Snow Yam
#### ForageWinterRoots
- Whether or not to forage Winter Roots
- *Default* - **false**
- *Note* - This checks Artifact Spots, a.k.a 'worms', 'stems', or 'twigs', and will only dig up the spot if it holds a Winter Root
### Mushroom Forageables
#### ForageChanterelles
- Whether or not to forage Chanterelle mushrooms
- *Default* - **false**
#### ForageCommonMushrooms
- Whether or not to forage Common Mushrooms
- *Default* - **false**
#### ForageMagmaCaps
- Whether or not to forage Magma Caps
- *Default* - **false**
#### ForageMorels
- Whether or not to forage Morels
- *Default* - **false**
#### ForagePurpleMushrooms
- Whether or not to forage Purple Mushrooms
- *Default* - **false**
#### ForageRedMushrooms
- Whether or not to forage Red Mushrooms
- *Default* - **false**
### Beach Forageables
#### ForageClams
- Whether or not to forage Clams
- *Default* - **false**
#### ForageCockles
- Whether or not to forage Cockles
- *Default* - **false**
#### ForageCoral
- Whether or not to forage Coral
- *Default* - **false**
#### ForageMussels
- Whether or not to forage Mussels
- *Default* - **false**
#### ForageNautilusShells
- Whether or not to forage Nautilus Shells
- *Default* - **false**
#### ForageOysters
- Whether or not to forage Oysters
- *Default* - **false**
#### ForageRainbowShells
- Whether or not to forage Rainbow Shells
- *Default* - **false**
#### ForageSeaUrchins
- Whether or not to forage Sea Urchins
- *Default* - **false**
#### ForageSeaweed
- Whether or not to forage Seaweed
- *Default* - **false**
### Cave Forageables
#### ForageFiddleheadFerns
- Whether or not to forage Fiddlehead Ferns
- *Default* - **false**
### Desert Forageables
#### ForageCactusFruits
- Whether or not to forage Cactus Fruits
- *Default* - **false**
#### ForageCoconuts
- Whether or not to forage Coconuts
- *Default* - **false**
- *Note* - This is separate from Coconuts that come from shaking Palm trees
### Island Forageables
#### ForageGinger
- Whether or not to forage Ginger roots
- *Default* - **false**
