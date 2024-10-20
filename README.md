# **Goblin Hunter** 🗡️🎮

## **Trailer**
<p align="center">
  <a href="https://youtu.be/1g0OP0_okLE">
    <img src="https://img.youtube.com/vi/1g0OP0_okLE/0.jpg" alt="Goblin Hunter Trailer" />
  </a>
</p>

## **About the Game**
**Goblin Hunter** is a first-person dungeon crawler developed using Unity and C#, drawing inspiration from classic games like *Doom (1993)*. With a 2.5D visual style and 8-directional sprites, this game offers a nostalgic, yet modern, experience. The core feature of **Goblin Hunter** is the procedural map generation and the camera techniques, making each dungeon exploration unique. The game is designed for educational purposes and not for commercial release.

## **Key Features**
- **Procedural Dungeon Generation**: Custom Depth First Search algorithm ensures a unique dungeon layout with dynamic enemy and item placements for every playthrough.
- **State Machines**: AI and player animations are controlled via custom state machines integrated with Unity's framework for smooth behavior transitions.
- **Save and Load System**: Progress is saved and loaded through JSON serialization, allowing players to pick up where they left off.
- **Custom Shaders**: Handcrafted shaders built with Unity to maintain a consistent aesthetic.
- **Terrain Design**: Utilizes [Unity Terrain Tools](https://assetstore.unity.com/packages/tools/terrain/terrain-tools-64852#releases) and Blender for creating immersive level environments.
- **Multiple Weapons & Interactables**: Equip your player with different weapons like swords, axes, and daggers, while also interacting with keys, doors, and healing items.

## **How to Play**
1. **Exploring the Dungeon**: Traverse the procedurally generated dungeon and engage in combat with various enemies.
2. **Combat System**: Choose from a variety of weapons (sword, daggers, axe) and fight enemies using strategic combat moves.
3. **AI Encounters**: Face enemies with unique AI behaviors powered by state machines.
4. **Saving & Loading**: Save your progress at any point and load your saved game to continue your adventure.

## **Technologies Used**
- **Unity**: For overall game development.
- **C#**: Main programming language for scripting.
- **Blender**: Used to create 3d models like the cave and others.
- **Unity Terrain Tools**: For level and environment design like the forest.
- **JSON Serialization**: Save and load player stats and game state.

## **Project Structure**
Here's an overview of the key files and scripts in the project:
```
📦Scripts
 ┣ 📂Animation
 ┃ ┣ 📜AnimationAspectManager.cs
 ┃ ┣ 📜AnimationStateManager.cs
 ┃ ┣ 📜BaseAnimationState.cs
 ┣ 📂Dungeon
 ┃ ┣ 📜DungeonGenerator.cs
 ┃ ┣ 📜RoomBehaviour.cs
 ┣ 📂Effect
 ┃ ┣ 📜CameraFade.cs
 ┃ ┣ 📜Effects.cs
 ┣ 📂Enemy
 ┃ ┣ 📂EnemyAnimationStates
 ┃ ┃ ┣ 📜EnemyAttackState.cs
 ┃ ┃ ┣ 📜EnemyHitState.cs
 ┃ ┃ ┣ 📜EnemyIdleState.cs
 ┃ ┃ ┣ 📜EnemyToDeathState.cs
 ┃ ┃ ┣ 📜EnemyWalkState.cs
 ┃ ┣ 📂EnemyStates
 ┃ ┃ ┣ 📜EnemyAttackLogicState.cs
 ┃ ┃ ┣ 📜EnemyChaseState.cs
 ┃ ┃ ┣ 📜EnemyDeathState.cs
 ┃ ┃ ┣ 📜EnemyPatrolState.cs
 ┃ ┣ 📜EnemyAnimationFSM.cs
 ┃ ┣ 📜EnemyController.cs
 ┃ ┣ 📜EnemyStateMachine.cs
 ┣ 📂Interactables
 ┃ ┣ 📂InteractionEvent
 ┃ ┃ ┣ 📜FamilyCookedEvent.cs
 ┃ ┃ ┣ 📜InteractionEvent.cs
 ┃ ┃ ┣ 📜ScreamEvent.cs
 ┃ ┣ 📜AreaChangeInteractable.cs
 ┃ ┣ 📜AxeInteractable.cs
 ┃ ┣ 📜DaggersInteractable.cs
 ┃ ┣ 📜HealingInteractable.cs
 ┃ ┣ 📜Interactable.cs
 ┃ ┣ 📜KeyInteractable.cs
 ┃ ┣ 📜OpenCloseObject.cs
 ┃ ┣ 📜SwordInteractable.cs
 ┣ 📂Menu
 ┃ ┣ 📜Credits.cs
 ┃ ┣ 📜CreditsMenu.cs
 ┃ ┣ 📜EquipMenu.cs
 ┃ ┣ 📜LoadMenu.cs
 ┃ ┣ 📜MainMenu.cs
 ┃ ┣ 📜MenuInitializer.cs
 ┃ ┣ 📜NewGameMenu.cs
 ┃ ┣ 📜OptionsMenu.cs
 ┃ ┣ 📜PauseMenu.cs
 ┃ ┣ 📜SaveSlot.cs
 ┣ 📂Player
 ┃ ┣ 📂PlayerStates
 ┃ ┃ ┣ 📜AttackState.cs
 ┃ ┃ ┣ 📜DeathState.cs
 ┃ ┃ ┣ 📜EquipState.cs
 ┃ ┃ ┣ 📜HitState.cs
 ┃ ┃ ┣ 📜IdleState.cs
 ┃ ┃ ┣ 📜UnequipState.cs
 ┃ ┣ 📜Player.cs
 ┃ ┣ 📜PlayerAnimationFSM.cs
 ┃ ┣ 📜PlayerController.cs
 ┃ ┣ 📜PlayerInteract.cs
 ┃ ┣ 📜PlayerLogic.cs
 ┃ ┣ 📜PlayerRotate.cs
 ┃ ┣ 📜PlayerRotateSmooth.cs
 ┃ ┣ 📜PlayerSFX.cs
 ┃ ┣ 📜PlayerUI.cs
 ┃ ┣ 📜PlayerVFX.cs
 ┣ 📂SaveSystem
 ┃ ┣ 📂Data
 ┃ ┃ ┣ 📜AreaData.cs
 ┃ ┃ ┣ 📜EnemyData.cs
 ┃ ┃ ┣ 📜GameData.cs
 ┃ ┃ ┗ 📜PlayerData.cs
 ┃ ┣ 📜GameRuntimeManager.cs
 ┃ ┣ 📜PlayerProfile.cs
 ┃ ┣ 📜SaveFinder.cs
 ┃ ┣ 📜SaveManager.cs
 ┣ 📂States
 ┃ ┣ 📜BaseState.cs
 ┃ ┣ 📜StateManager.cs
 ┣ 📂Weapon
 ┃ ┣ 📜Axe.cs
 ┃ ┣ 📜Daggers.cs
 ┃ ┣ 📜Hands.cs
 ┃ ┣ 📜Sword.cs
 ┃ ┣ 📜Weapon.cs
 ┃ ┣ 📜WeaponManager.cs
 ┣ 📜EventManager.cs
 ┣ 📜GameIntro.cs
 ┣ 📜Hittable.cs
 ┣ 📜OutllneEffect.cs
 ┣ 📜SpriteBillboard.cs
 ┗ 📜Utils.cs

```

## Contributors
* [Dimitris Pararas](https://github.com/dimparar)
* [Vaggelis Leftakis](https://github.com/vleft02)
* [Panagiotis Triantafillidis](https://github.com/Panattack)

## More info

To find more information about the implementation of the project, you can double click this [link](../GoblinHunter/blob/main/Goblin_Hunter_Report.pdf) to check the report.

