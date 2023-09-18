# Technical Documentation

This resource serves as a guide to the game's architecture, code structure, and development tools. 

## Code Overview

The Code is divided into following sections:
### 1. Dungeons
Following scripts control procedural dungeon generation, dungeon room management and spawning items in rooms.

1. #### ```DungeonGenerator```
   The "DungeonGenerator" script procedurally generates interconnected dungeon rooms in the game using a BFS algorithm. The BFS based algorithm provides interesting and balanced room generation. Unlike DFS based approach, which may generate long horizontal or vertical strips of interconnected rooms.

    - ```GenerateMap()```: Initiates room generation using BFS, considering room types and boundaries.

    - ```InstantiateRooms()```: Instantiates room prefabs based on room references, setting room types.

    - ```ValidateCoordinates()```: Checks if coordinates are within valid world boundaries.

2. #### ```DungeonRoomManager```
   The script manages individual rooms in the Machina Mausoleum game. These rooms have various attributes, including enemy and object spawning, door management, and room type classification.

    - ```HandleDoors()```: Debugging function for marking rooms as cleaned.

    - ```SetActiveEntrances(bool[] sidesToActivate)```: Controls room connectivity by activating entrances and blocks.

3. #### ```EnemySpawner```
    The script handles enemy spawns in dungeons. The script randomizes enemy locations and tracking their numbers for room progression.

4. #### ```EnemySpawnPoint```
    The script randomly selects and instantiates enemy types from an array of types at the spawn point's location.
5. #### ```ItemSpawner```
    The script handles the activation of a random selection of item spawn points, providing variety in item placement.
6. #### ```ItemSpawnPoint```
    The script randomly selects and potentially spawns items based on a specified spawn percentage, adding variety to item placement.
7. #### ```NavmeshBaker```
   The script initiates asynchronous NavMesh baking with a specified delay
8. #### ```ObjectSpawnManager```
   The ```ObjectSpawner``` class handles the spawning of various in-game objects, including elevator structures and key sctructures.

9.  #### ```ObjectSpawnPoint```
    The script serves as base for instantiating given structures like elevators, key structures and obstacles.


### 2. Enemies
Following scripts manage enemy movement, attacks, managin health and pathfinding.
1. #### ```AxeSpawner```
   The script spawns axes evenly distributed in a circular pattern and calculates the rotational differences between them.

2. #### ```BotAxeManager```
   The script controls the rotation speed of a bot axe. The script rotates the axe and deals damage to the player upon collision.

3. #### ```EnemyHealthHandler```
   The script controls enemy health, damage,  damage flashes, disabling melee axes upon death, and triggering death animations and behavior for both melee and ranged enemies.

4. #### ```EnemyMovement```
   The script controls enemy movement and states, including navigation on a ```NavMesh``` (see **NavMeshPlus:** [NavMeshPlus Docs](https://github.com/h8man/NavMeshPlus)) and state-specific logic for movement.

5. #### ```EnemyWeaponManager```
   The script handles selecting a gun, preparing for shooting, detecting the player, and managing shooting cooldowns.

6. #### ```ShootingEnemyMovement```
   The class is an extension of the ```EnemyMovement```class, specifically designed for enemies with ranged attacks.

### 3. EnvObjects
Following scripts manage item pickups and other item interactions.
1. #### ```AmmoPickup```
   The script represents an ammo item that the player can pick up to receive additional ammunition for their weapons.

2. #### ```CratePickup```
   The script represents a crate object that can be opened by the player to reveal and collect various items, including guns and other pickups.
3. #### ```DoorManager```
   The script handles dungeon door "close" and "open" animations according to room state.

4. #### ```ElevatorManager```
   The script is responsible for managing the behavior of elevators in the game, including their animation and interactions with the player.

5. #### ```HeartPickup```
   The class is an extension of the ```KeyPickup```class. It is responsible for heart pickups and healing player.

6. #### ```ItemMover```
   The script creates a hovering motion effect for GameObject it is attached to.

7. #### ```KeyPickup```
   The script is responsible for handling the behavior of key pickups and pickups in general.

8. #### ```WeaponPickup```
    The class is an extension of the ```CratePickup```. Handles behavior of weapon pickups in the game, allowing players to equip new weapons and associated ammo.

### 4. Player
These scripts are responsible for player movement, health management and weapon management.
1. #### ```PlayerMovement```
   The script controls the movement of the player character in the game, including basic movement, dashing, animation, and flip direction based on mouse position.
2. #### ```PlayerWeaponManager```
   The script manages the player's equipped guns, allowing them to switch between weapons, add new guns to their inventory, and save/load gun data between game sessions.

    - ```ScrollThroughGuns()```: This method enables the player to switch between equipped guns using the mouse scroll wheel, with checks for scrolling up and down and audio feedback when changing weapons.

   - ```SwitchWeapon(int indexChange)```: This method switches between guns by changing the visibility of the currently equipped gun and updating the current gun reference.

   - ```AddFromGunReferences(string nameToAdd)```: This method adds guns to the player's inventory based on predefined gun pairs by iterating through the available gun pairs and checking for a matching gun name.

### 5. Projectiles
This category handles projectile behavior.
1. #### ```Projectile```
   The script handles collision detection with other game objects and deals damage based on its initiator, which can be either the player or an enemy.

### 6. UI
Following scripts are used for managing all UI components of the game - displaying player health and gun information, managing paused and death screen and taking care of menu and settings scene. 
1. #### ```ButtonSoundManager```
   The script manages button sound effects in the game. 
2. #### ```DeathMenuManager```
   The script manages the death menu, which is displayed when the player character dies. It provides functionality for displaying a random death caption and the player's score, as well as an option to restart the game.

3. #### ```ElevatorHelper```
   The script checks if the player has entered the trigger zone of the elevator and whether the player has obtained the key to activate the elevator.

4. #### ```GunWindow```
   The script displays the player's current weapon and its ammo information in a UI element.

5. #### ```HealthUIManager```
   The script is responsible for managing the UI elements that represent the player's health using heart icons.

6. #### ```HeartUI```
   The script dynamically changes the UI sprite of the heart icon to represent different states of the player's health.

7. #### ```KeyUIComp```
   The script displays whether the player has acquired the elevator key.

8. #### ```LayerAnnouncer```
   The script displays number of current level at the start of level.

10. #### ```MainMenuManager```
   The script manages interactions and transitions within the main menu.

11.  #### ```PauseMenuManager```
   The script allows the player to pause and resume the game, exit to the main menu, and exit the game entirely.

11. #### ```ScoreCounterUI```
    The script displays players score as the amount of killed enemies.

12. #### ```CameraController```
    The script handles camera movement to smoothly follow the player.

13. #### ```TransitionManager```
    The script is responsible for displaying transition between scenes and levels.

### 7. Utils
This section is dedicated to scripts helping save, load and manage universal data and shared prefab behaviors.
1. #### ```DamageFlasher```
   The script is used to make the parent GameObject (Player or Enemy) "flash" briefly with a specified material when it takes damage. 

2. #### ```GameStateManager```
   Static class for storing information about game being paused.

3. #### ```LevelManager```
   The script provides a centralized way to manage level loading, scene transitions, and resetting game state. It also offers an event system to notify other parts of the game when a level change occurs.

4. #### ```OptionManager```
    Static class for storing information about sound options handled in ```OptionsMenu``` scene.

5. #### ```OptionsSceneManager```
   The script is responsible for managing the ```OptionsMenu``` scene interactions. 
   
6. #### ```PlayerSFXManager```
    The script is responsible for managing and playing sound effects related to player.

7. #### ```PlayerStateTracker```
    The static class contains fields and methods that manage player health, the player's current weapons, and game events.
    - ```OnDamageTaken```: A delegate event to handle damage taken by the player. It's triggered when the player takes damage.

    - ```PlayerDied```: A delegate event to handle the player's death. It's triggered when the player character dies.
  
    - ```GunInfo```: A struct that holds information about a gun, including its name (gunName) and remaining ammo (ammo). Used for saving and loading guns.

8. #### ```SoundManager```
   The script handles playing both background music (themes) and sound effects, with the ability to control their playback based on game settings and events.

### 8. Weapons
This section is dedicated to managing gun equips and shooting.
1. #### ```Gun```
   The script handles rotation towards a target position and flipping sprite based on the target's position relative to the gun.

2. #### ```GunPickupSpawner```
   The script is responsible for spawning weapon pickups with a specified amount of ammo.

3. #### ```GunShooting```
   The script handles functionality for shooting mechanics, handling different shooting modes (single and automatic), ammunition management, accuracy, and damage calculation. 

## Libraries
The project uses open source GitHub repository [NavMeshPlus](https://github.com/h8man/NavMeshPlus). It's essential to follow the documentation and guidelines provided by the asset's creators on their GitHub page.

## Used Software
In this project, [Unity](https://unity.com/) was used as the game engine of choice. All visual assets were crafted using [Aseprite](https://www.aseprite.org/).

## Commit Guidelines
This project follows [Conventional Commits](https://gist.github.com/qoomon/5dfcdf8eec66a051ecd85625518cfd13).


## Assets
1. **Visual assets**
   - Every visual element in the project was  handcrafted by the developer.
2. **SFX**
   - Special thanks to:
      - Trevor Lentz
      - Jesús Lastra


