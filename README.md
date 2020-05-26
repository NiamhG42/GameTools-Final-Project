# GameTools-Final-Project
Final Project (and supporting documentation) for Game Tools 2020

### Log

* Made the city bigger (more buildings)

* Created "racetrack" path through city

* Divided the city up with roads

* Made the racetrack more visually distinct from normal roads

* Randomised the building heights

* Building blocks now check to see if they're completely surrounded

* If a block is surrounded, its mesh doesn't render

* Designed and added new materials:
  - Wood (Yellow, White, Green)
  - Basic Tile (Yellow, White, Green)
  - "Daisy" themed tiles
  - "Sunflower" themed tiles
  - "Poppy" themed tiles
  - Racetrack tile
  - Glass

* Created perlin noise texture for land outside city

* Created terrain assets, randomly distributed outside city 

* Randomised terrain object sizes

* Ensured 2D terrain objects are always facing towards the centre of the city

* Made 2D Tilemap Mountains around edge of scene

* Modelled a glass dome to go around the city

* Changed car controls (Now Add Force)

* Changed how camera works

* Fixed issue where the player "car" clips through objects

* Made a simple menu screen and a screen to explain the game's controls

* Changed layout of racetrack (corners were too tight and hard to turn)

* Game now has a timer and displays the race's current time (UI)

* Made rough version of "PointsBoosters" that the player can drive through to subtract from final time

* Points now display (UI)

* Registers start and end of the race

* Registers different laps

* Displays different laps

* Implemented countdown before the start of the race

* Records final score and compares it with previous final scores

* Sorts high scores into a leaderboard

* Displays rudimentary leaderboard in the menu screens

* Implemented basic teleportation 

* Created cave floor (perlin noise used to generate terrain heights)

* Fixed UI scaling issues (now adjusts to screen size)

* Added crystal texture to cave floor

* Created cave walls

* Giant crystal columns added to cave
  - Randomly selects between different formations
  - Places columns each time a game starts (code)
  - Doesn't use same formation twice in a row

* Now instantiating "PointsBoosters" in caves via code

* Now adding "PointsBoosters" to list in RaceManager when instantiated

* Cave walls and floor are inactive when not racing through them

* Placed Start/Finish Line, "PointsBoosters" and teleporters where they should be

* "PointBoosters" properly modelled, lit and sized 

* UI now announces start of race, laps and end of race

* Displays Time, Score, Points + Leaderboard at the end of the game

* Can now restart race or return to menu at the end of the game

* Highlights your score in red if you managed to get in 1st, 2nd or 3rd place

* Basic teleporter/portal animations implemented

* Simple version of bouncy mushrooms implemented (physics working)

* Modelled mushrooms and imported. Implemented finished version of bouncy mushrooms.

* Placed all mushrooms where they should be.

* Fixed issue with some building parts turning invisible when they weren't supposed to.

* Added Sound Effects

* Correct Finish Line models implemented

* Final version of teleporter/portal animations drawn and implemented

* Implemented menu background art

* Fixed exploit so the player has to actually do a lap for it to be counted