# Project Description: Camel Invaders

**Camel Invaders – 2D Space Shooter Game developed in Unity**

**Description:**
I designed and implemented a 2D Space Shooter game (inspired by *Space Invaders*) using the Unity game engine and C#. The project highlights the ability to write modular, scalable code and utilize established design patterns to solve specific problems in game development.

**Technical Aspects and Achievements:**

* **Software Architecture:**
* Implementation of the **Singleton** pattern for the `GameMaster` class to centralize game state management (score, enemy waves, win/loss conditions).
* Utilization of **ScriptableObjects** to define configuration data for enemy waves (`WaveScriptableObject`), allowing designers to adjust difficulty and level structure directly within the editor without modifying code.

* **Object-Oriented Programming (OOP) and Modularity:**
* Extensive use of **Interfaces** (`IDamageable`, `IBuffable`) to create a generic interaction system between entities. This allows various objects (player, enemies) to react to damage or buffs in a polymorphic manner, reducing tight coupling.
* Organization of code into **Namespaces** (`CamelInvaders.GameMaster`, `CamelInvaders.Entity`, etc.) for a clear structure and maintenance of the separation of concerns.

* **Gameplay and Mechanics:**
* Development of the player controller (`Player.cs`) managing input, movement physics, and combat systems.
* Implementation of a **Regenerating Shield** system and Health Management.
* Diversified weapon system (Rapid-fire lasers, Missiles).
* Dynamic **Enemy Spawning** logic and group movement behavior for enemies.

* **UI and Feedback:**
* Integration of UI systems (Health Bars, Shield Bars, menus) and their real-time synchronization with the internal game state.
* Audio system management for sound effects and feedback.

**Technologies:** C#, Unity 2022+, Git.
