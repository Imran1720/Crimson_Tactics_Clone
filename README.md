# Crimson_Tactics_Clone

Inspired by Crimson Tactics, this project was developed in Unity and focuses on essential tactical RPG features including pathfinding, grid-based mobility, and enemy AI on an isometric battlefield. With the use of specialized Unity Editor tools and clear, object-oriented C# programming, it displays key gameplay mechanisms seen in strategy role-playing games.

---
## Demo

[![Watch the video](https://github.com/user-attachments/assets/fe0cb925-3403-4e1c-a9fc-bc2e46de69ec)](https://youtu.be/UebnLKq9Z7I)


Click the image above to watch the demo video.


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
- [x] ObstacleManager renders Rocks(replacing with red Sphere)

---

### 3️⃣ Player Pathfinding
- [X] Pathfinding algorithm  
- [X] Animated movement  
- [ ] Obstacle-aware pathfinding

---

### 4️⃣ Enemy AI
- [x] Enemy AI with interface  
- [x] Moves to adjacent tile near player  
- [x] Waits for player to move  

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
│  ├──Animations
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
│  ├── Pathfinding/
│  ├── ScriptableObjects/
│  ├── UI/
│  └── Utils/
└── Scenes/
      └──MainScene.unity
