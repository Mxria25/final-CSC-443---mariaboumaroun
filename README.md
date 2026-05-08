Final CSC 443 - Spring 2026 : Infinite Runner Project

Name: Maria Bou Maroun
ID: 20231119
Course: CSC 443
Project: Infinite Runner Extension

This project extends the Infinite Runner game provided in class into a more complete playable experience inspired by Subway Surfers.

The base project already included:
- Infinite chunk-based world generation
- 3-lane movement system
- Jump mechanic
- Follow camera
- Object pooling system

The project was expanded by adding gameplay systems, UI, menus, obstacles, train-top movement, collectible coins, and additional environment elements.



Implemented Core Features

1. Collision & Game Over
- Added obstacle collision detection using trigger colliders.
- The player dies upon colliding with obstacles such as trains and roadblocks.
- A Game Over panel appears when the player dies.

2. Restart System
- Pressing the `R` key reloads the scene and starts a fresh run.
- Restart works both during gameplay and after Game Over.

3. Score HUD
- The player’s distance score is displayed during gameplay.
- The final score is shown on the Game Over screen.
- High score is also displayed.



Chosen Extensions

B — Main Menu Scene
Implemented a complete Main Menu scene containing:
- Start Game button
- Quit button

The menu loads the gameplay scene using SceneManager.

C — Pause Menu
Implemented a Pause system:
- Press `ESC` to pause/unpause the game.
- Pause panel contains:
  - Resume button
  - Return to Main Menu button

Time scale is stopped while paused.



Additional Features

Train Roof Mechanic
- The player can jump and run on top of trains.
- Separate colliders were used for:
  - Train body collision
  - Walkable train roof

Coins System
- Random coin spawning inside chunks.
- Coins can appear:
  - On the floor
  - On top of trains
  - In jump positions
- Coin counter UI added during gameplay.
- Final coin count shown on the Game Over screen.

Jump Audio
- Added a jump sound effect when the player jumps.

Environment Improvements
- Added side walls to improve scene visuals.



Controls

- A → Move Left
- D → Move Right
- W → Jump
- ESC → Pause / Resume
- R → Restart



Known Issues

- Some obstacle/chunk combinations may still require minor balancing adjustments.



Assets Used

- Unity Asset Store train models
- Unity Asset Store roadblock models
- Unity Asset Store coin assets
- Unity Asset Store jump audio