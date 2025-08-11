# Crimson_Tactics_Clone

Inspired by Crimson Tactics, this project was developed in Unity and focuses on essential tactical RPG features including pathfinding, grid-based mobility, and enemy AI on an isometric battlefield. With the use of specialized Unity Editor tools and clear, object-oriented C# programming, it displays key gameplay mechanisms seen in strategy role-playing games.

---

# 📌 Core Features Checklist
Each functionality below will be implemented in a separate branch. Once completed and tested, it will be checked off here.

### 1️⃣ Grid Generation
- [x] 10x10 cube grid  
- [x] Tile hover detection  
- [x] UI display of tile position

---

### 2️⃣ Obstacle System
- [x] Custom Editor Tool with toggleable grid  
- [x] ScriptableObject stores obstacle data
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

## Project Folder Architecture
```plaintext
Assets/
├── Resources/
│  ├── Art/
│  │  ├── Materials/
│  │  ├── Models/
│  │  └── Textures/
│  ├── Prefabs/
│  └── DataSO/
├── Scripts/
│  ├── AI/
│  ├── Core/
│  ├── Custom Data/
│  ├── Editor/
│  ├── Event/
│  ├── Grid/
│  ├── Obstacles/
│  ├── Pathfinding/
│  ├── ScriptableObjects/
│  ├── UI/
│  └── Utils/
└── Scenes/
<<<<<<< HEAD
<<<<<<< HEAD
      └──MainScene.unity
=======
      └──MainScene.unity
>>>>>>> 18b3fdbd464ee33a4bf673da07d944e31183048f
=======
      └──MainScene.unity
>>>>>>> 18b3fdbd464ee33a4bf673da07d944e31183048f
