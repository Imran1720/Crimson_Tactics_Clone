# Crimson_Tactics_Clone

Inspired by Crimson Tactics, this project was developed in Unity and focuses on essential tactical RPG features including pathfinding, grid-based mobility, and enemy AI on an isometric battlefield. With the use of specialized Unity Editor tools and clear, object-oriented C# programming, it displays key gameplay mechanisms seen in strategy role-playing games.

---

# 📌 Core Features Checklist
Each functionality below will be implemented in a separate branch. Once completed and tested, it will be checked off here.

### 1️⃣ Grid Generation
- [ ] 10x10 cube grid  
- [ ] Tile hover detection  
- [ ] UI display of tile position

---

### 2️⃣ Obstacle System
- [ ] Custom Editor Tool with toggleable grid  
- [ ] ScriptableObject stores obstacle data
- [ ] ObstacleManager renders red spheres

---

### 3️⃣ Player Pathfinding
- [ ] A* algorithm  
- [ ] Animated step-by-step movement  
- [ ] Obstacle-aware pathfinding

---

### 4️⃣ Enemy AI
- [ ] Enemy AI with interface  
- [ ] Moves to adjacent tile near player  
- [ ] Waits for player to move  

---

## 🛠️ Technologies
- Unity 2021.3+ (or newer)
- C#
- ScriptableObjects
- Custom Editor Tools
- UI Toolkit / TextMeshPro

---

## Project Architecture
/Assets
  /Resources
    /Art
      /Materials
      /Models
      /Textures
    /Prefabs
    /DataSO
  /Scripts
    /Grid
    /UI
    /Obstacles
    /Pathfinding
    /AI
    /Editor
/Scenes
