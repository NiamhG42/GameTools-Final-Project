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