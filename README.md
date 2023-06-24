# Asteroids - robust

See in-development goals and design spec for the project in [this public gist](https://gist.github.com/keerthik/d174140ede5c2a0c2bd8787af13761ad). As the project develops and solidifies, more information will move into this readme.

## Worklog
Unofficial work log. Can be used as a more detailed commit log history (when it's accurate).

### Done
- Install Unity 2023.1.0b1 (latest beta at the time)
- Install Visual Studio Code (free)
- Set up VSCode for Unity [following official guide](https://code.visualstudio.com/docs/other/unity)[1]
- Install latest [Blender](https://www.blender.org/)
- Create 3D model for player space ship in Blender following Colby's [space ship tutorial](https://www.youtube.com/watch?v=jo7FZBf4VkM)
- Set up git repository
- Script basic 4-control movement for player: accelerate, decelerate, turn left, turn right
- Script momentum and angular momentum for player
- Basic replay system
- Add coordinator for menu state vs game state

### TODO
- Add menu navigation to replay system
- Clean up timing system concept

## Notes
[1] VSCode's Unity package is no longer supported, but it is [still available on github](https://github.com/Unity-Technologies/com.unity.ide.vscode/tree/next/master/Packages/com.unity.ide.vscode)