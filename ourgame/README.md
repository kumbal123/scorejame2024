## Project structure:
# Classes 
TileBase.cs - base parent class used for tiles that have some effect when stepped on
-> TileUpgrade.cs - can be used to level up an upgrade when stepped on.

EnemyBase - base parent class used for enemy Nodes.

Upgrades are implemented using two base parent classes:
- UpgradeData, that contains the upgrade's data such as name, stats and level up changes.
- UpgradeNode, which is the actual node that is spawned in-game to attack enemies, etc.

UpgradeData is a script in "res://src/upgrades/dataScript/"
> The script is then used to create a Resource (.tres) file, which you can find in "res://resources/upgrades/"
> This resource file is then assigned to the TileUpgrade and UpgradeNode Nodes so they can pull the necessary data from it.


# Collision layers:
1 - Player PhysicsBody (CharacterBody2D)
	- Used by TileLava to detect PlayerCharacter entering lava to begin damage tick.

2 - Player HitBox (Area2D)
	- Used by PlayerCharacter to find out what damaging areas it is inside so it can take damage on the next damage tick.

4 - Enemy PhysicsBody (CharacterBody2D)
	- Used by Upgrades to find and damage EnemyBase.