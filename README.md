# Cube-Invasion
## 1 Project overview
### 1.1 Introduction:

This project started out as a personal for-fun project in which I wanted to mimic the movement found in World of Warcraft, since I find it really satisfying. The built-in Unity Input.GetAxis() method has a kind of wind-up to it, which I didn’t like. After hours of fiddling around with alternatives I realised that Unity already has a method called Input.GetAxisRaw(), which does exactly the same thing as what I was looking for.

In the meantime while testing out my own movement implementation I had, however, begun experimenting a lot with rigidbodies, and the project evolved into a kind of mini-game where evil boxes with ill intent fall from the sky, and you have to keep them away from you with laser-powered "explosions".

### 1.2 Project elements: 
The project only has one scene, the hierarchy of which contains the following parts:
- A GameController GameObject holding a GameController script.
- A Player GameObject.
- A Platform on which the game takes place.
- A Container for holding the enemy cubes instantiated during runtime.
- A Canvas with in-game UI containing a health bar, a killscore and a points score.
- A Canvas which becomes active once the game ends, containing a small message, your final score, and a retry button.


The project contains four scripts:
- **GameController** - which is responsible for spawning enemies at a changing pace, updating the health bar, keeping track of player health, and updating the score.
- **PlayerMovement** - which is responsible for the movement of the player in the game controlled by WASD and the arrow-keys, and the camera, which rotates horizontally around the player, controlled by moving the mouse when moving left or right.
- **EnemyBehavior** - This is the script attached to the Enemy prefab. It first assigns a colour (red (gradient), black or green) to the cube and has the cube behave differently based on this colour. The red cubes are light and fast (the redder the faster). The black cubes are semi-heavy, medium-sized, and jump around. The green cubes are very heavy, large, and slow.
- **LaserAim** - This script is attached to a LaserController GameObject, a child of the Player object. This script includes a Raycast which shoots out in front of the player and returns information about the enemy hit. If enemies are hit by the ray (this happens when the player looks at an enemy and the enemy is within range) the player is able to shoot using the Left mouse button. This then applies an explosion force to the Rigidbody of the enemy hit as well as those of nearby enemies in a radius around the hit. The LaserController object also has a LineRenderer attached. This is used to visualise the shot.

## 2 Time schedule & Resources
### 2.1 Time schedule

Activity | Approx time spent |
--- | --- |
Player movement experimentation | 2.50 hrs |
Final movement and camera movement implementation|1.00 hrs|
Player jump and isGrounded check|1.50 hrs|
Enemy move towards player|1.00 hrs|
Implementing laser shoot function|1.00 hrs|
Randomise Red Enemy colour and speed|1.50 hrs|
Implementing and tuning explosions|2.00 hrs|
Implementing enemy spawn controller|2.00 hrs|
Implementing green cube and making it heavier and have drag|0.50 hrs|
Implementing black cube and its jump functionality|1.50 hrs|
Finalising enemy cube behaviour|1.50 hrs|
Implementing AimController|2.50 hrs|
Implementing point system|0.50 hrs|
Implementing health bar|1.00 hrs|
Implementing and fine tuning player death|2.00 hrs|
Adding end-game screen|1.50 hrs|
Adding restart button|0.25 hrs|
Playtesting throughout project|0.75 hrs|
**Approx total:**|**24.50 hrs**|


### 2.2 Resources used (in order)
- Book: [Unity in Action](https://www.manning.com/books/unity-in-action-second-edition) by [Joe Hocking](https://www.newarteest.com/)
- Unity Forum Question: [Move Player without acceleration](https://answers.unity.com/questions/1643729/move-player-without-acceleration.html)
- Youtube tutorial: [Move Camera Around Object in Unity](https://youtu.be/zVX9-c_aZVg)
- YouTube tutorial: [How to Check if Your Player is GROUNDED Unity Tutorial](https://www.youtube.com/watch?v=j41UHuA1bLM)
- Unity Documentation: [Cursor.visible](https://docs.unity3d.com/ScriptReference/Cursor-visible.html)
- Unity Documentation: [Application.Quit](https://docs.unity3d.com/ScriptReference/Application.Quit.html)
- Unity Documentation: [Material.SetColor](https://docs.unity3d.com/ScriptReference/Material.SetColor.html)
- Unity Forum Question: [Change GameObject color by C# script](https://forum.unity.com/threads/change-gameobject-color-by-c-script.923672/)
- Unity Forum Question: [Compare Tag on Collision (Collider.CompareTag)](https://answers.unity.com/questions/1272543/compare-tag-on-collision-collidercomparetag.html)
- YouTube tutorial: [Shooting Laser using Raycast and LineRenderer | Unity Game Engine](https://www.youtube.com/watch?v=YjpKxjzwado)
- YouTube tutorial: [Unity Explosion Force/knockback - Making a bomb](https://www.youtube.com/watch?v=SKdM2ERWy8U)
- Unity Documentation: [Rigidbody.AddExplosionForce](https://docs.unity3d.com/ScriptReference/Rigidbody.AddExplosionForce.html)
- Unity Forum Question: [number of colliders inside a trigger.](https://answers.unity.com/questions/1742827/number-of-colliders-inside-a-triggernumber-of-coll.html)
- Unity Forum Question: [Convert from local position to world position](https://answers.unity.com/questions/147184/convert-from-local-position-to-world-position.html)
- Unity Forum Question: [Random.randomBoolean function!](https://forum.unity.com/threads/random-randomboolean-function.83220/)
- Unity Forum Question: [Public class not showing in inspector.](https://forum.unity.com/threads/public-class-not-showing-in-inspector.919190/)
- YouTube tutorial: [Create a HEALTH BAR, Volume slider and LOADING BAR! | Unity UI Tutorial](https://www.youtube.com/watch?v=tWUyEfD0kV0&t=311s)
- Unity Forum Question: [UI Relative to screen size](https://answers.unity.com/questions/1178324/ui-relative-to-screen-size.html)
- Stackoverflow Question: [How to display Table in README.md file in Github?](https://stackoverflow.com/questions/39378020/how-to-display-table-in-readme-md-file-in-github)
- GitHub Documentation: [Basic writing and formatting syntax](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax)
