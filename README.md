# 2D Dark Souls-Inspired Player Controller

This Unity-based project is a tribute to timing-focused combat systems seen in games like Dark Souls and For Honor. The player controller includes core mechanics for a 2D action game prototype that can be extended into a full platformer, reaction-based mobile game, or local co-op experience.

## Mechanics Implemented

- **Movement**: Walk left and right
- **Dodging**: Roll in both directions
- **Jumping**: Enables dodging and platform navigation
- **Sword Attacks**: Simple melee strikes
- **Parrying**: Timed blocks to deflect enemy attacks
- **Damage Reception**: Hit reaction included (no gameplay penalty in this version)

## Design Focus

### Hitboxes

Each hitbox was carefully placed to contribute meaningfully to gameplay. This helps simulate precision-based combat where every frame and pixel counts.

### Timing

Inspired by Souls-like games and titles like For Honor, the parry window and combat flow rely on frame-accurate timing. The player must time rolls, parries, and strikes to maintain control of combat.

### Scalability

This system can serve as the foundation for:
- A fast-paced reaction mobile game
- A side-scrolling couch co-op campaign
- AI-based training modules for parrying and timing practice

## Why I Built This

This project began as a collaboration with my housemate, who was studying computer science but unfamiliar with game development. I used this opportunity to introduce Unity fundamentals and escalate the lessons into a full, playable character controller.

It allowed us to bridge theory and practice, bringing code to life through interactive design.

## Project Structure

- `Player Scripts/`  
  Contains all core mechanics including movement, parrying, and combat

- `AI Scripts/`  
  Placeholder for future AI behavior and training

- `Prefabs/`  
  May include a prebuilt controller from the Unity Asset Store (not authored by me)

- `Hero Knight Asset`  
  Free asset from the Unity Asset Store used for player visuals and animations

## Disclaimer

- All custom logic and scripting are authored by me, located in the `Player Scripts` and `AI Scripts` folders
- The character visuals and animations are from the free "Hero Knight" asset pack on the Unity Asset Store
- A third-party prefab controller may exist in the prefab list. It is not my original work

## How to Run

1. Open the project in Unity (recommended version: 2021.3 or later)
2. Load the main scene
3. Press Play to test mechanics
4. Inspect `PlayerController` and related scripts to explore the logic

## Next Steps

- Add enemy AI for training and combat scenarios
- Implement health and stamina systems
- Create level progression and environmental hazards
- Explore online co-op or local multiplayer features

## Contact

For updates or collaboration:  
https://linktr.ee/teerasakmairoddee
