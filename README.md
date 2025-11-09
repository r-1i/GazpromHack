# 🎴 GazpromHack - Reigns-like Card Decision Game

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2022.3.46f1-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=c-sharp)
![Platform](https://img.shields.io/badge/Platform-WebGL-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Prototype-orange?style=for-the-badge)

**[🇷🇺 Русская версия](README_RU.md)**

*An interactive card game where every swipe shapes your destiny*

[📖 Overview](#-project-overview) • [🚀 Quick Start](#-how-to-run) • [⚙️ Architecture](#-architecture-and-design) • [🎯 Features](#-functionality) • [📈 Future Plans](#-future-improvements)

</div>

---

## 📖 Project Overview

<table>
<tr>
<td>

GazpromHack - Stories is a **decision-making card game** developed for the Gazprom Hackathon, inspired by the acclaimed mobile game "Reigns". Players navigate through stories of Gazprombank clients, making critical life decisions by swiping cards left or right.

Each decision impacts **four key life parameters**:
- 💗 **Mood** - Emotional well-being
- 👨‍👩‍👧 **Family** - Family relationships  
- 💰 **Money** - Financial status
- 📊 **Investments** - Long-term planning

</td>
<td width="40%">

### 🎮 Quick Facts

| | |
|---|---|
| **Type** | Mobile Card Game |
| **Genre** | Decision-Making, Narrative |
| **Platform** | Android, iOS |
| **Cards** | 54 unique scenarios |
| **Status** | ✅ Playable Demo |
| **Dev Time** | Hackathon (72h) |

</td>
</tr>
</table>

> 💡 **Core Concept**: Players experience financial decision-making through engaging narrative gameplay, making it both educational and entertaining.

---

## 🛠️ Tech Stack

<table>
<tr>
<td width="50%">

### 🎮 Game Engine
- **Unity 2022.3.46f1 LTS**
- **C#** Programming Language
- **Unity 2D Feature Set 2.0.1**

### 📦 Unity Packages
- **TextMesh Pro 3.0.6** - Typography
- **Unity UI (uGUI) 1.0.0** - Interface
- **Unity Timeline 1.7.6** - Animation

</td>
<td width="50%">

### 🔧 Third-Party Libraries
- **DOTween (Demigiant)** - Animation System
  - Smooth card transitions
  - Easing functions
  - Professional polish

### 🏗️ Architecture Patterns
- ✅ Singleton Pattern
- ✅ Event Bus Pattern
- ✅ Observer Pattern
- ✅ Factory Pattern

</td>
</tr>
</table>

---

## ⚙️ Architecture and Design

### 🎯 System Overview

```
┌─────────────────┐
│   JSON Cards    │ ← 54 Card Definitions
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│  CardSpawner    │ ← Deck Management
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ SwipeDetector   │ ← Player Interaction
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│   Event Bus     │ ← System Communication
└────────┬────────┘
         │
         ├─────────────┬──────────────┐
         ▼             ▼              ▼
┌──────────────┐ ┌──────────┐ ┌────────────────┐
│ Parameters   │ │   UI     │ │  Next Card     │
│   Update     │ │  Update  │ │   Addition     │
└──────────────┘ └──────────┘ └────────────────┘
```

### 🏛️ Core Components

<details>
<summary><b>1. 📡 Event-Driven Architecture</b></summary>

**Custom Event Bus System** for decoupled component communication:

```csharp
EventBus → EventBusHolder (Singleton)
    ↓
Events:
    - OnDestroyCardEvent (card swiped)
    - SetParametersEvent (parameter changes)
```

**Benefits:**
- ✅ Loose coupling between systems
- ✅ Easy feature additions
- ✅ Clear data flow
- ✅ Highly testable

</details>

<details>
<summary><b>2. 🎴 Card System Architecture</b></summary>

**Data Flow:**
```
JSON Files → Deserialization → SwipeDetector → Event Bus → Actions
```

**Card Structure:**
- Two choices per card (left/right swipe)
- Three possible outcomes (positive, negative, neutral)
- Configurable outcome probabilities
- Parameter change effects
- Branching narrative paths

</details>

<details>
<summary><b>3. 💾 Game State Management</b></summary>

**Persistent Settings:**
- `SceneLoadValues` survives scene transitions
- Multiple color schemes (Malevich/Gazprom)
- Debug mode with card ID display

**Parameter System:**
- Range: 0-100 for each parameter
- Real-time event-driven updates
- Smooth visual transitions

</details>

<details>
<summary><b>4. 🔄 Dynamic Deck Management</b></summary>

**Smart Card Spawning:**
- Initial deck from "starter" cards
- Dynamic card insertion at specific positions
- Prevents card repetition
- Fallback random generation (long game mode)

</details>

### 🎨 Design Patterns in Action

| Pattern | Implementation | Purpose |
|---------|---------------|----------|
| **Singleton** | `UIController`, `EventBusHolder` | Global manager access |
| **Observer** | Event Bus listeners | Parameter updates |
| **Factory** | Card instantiation from JSON | Dynamic content creation |
| **Strategy** | Outcome types (pos/neg/neu) | Flexible decision results |

### ⚖️ Technical Trade-offs

> 💭 **Hackathon Pragmatism**: Several design decisions prioritized rapid delivery

| Decision | Reason | Future Fix |
|----------|--------|------------|
| 🔴 Hardcoded IDs | Time constraints | Use ScriptableObjects |
| 🔴 Commented probability code | JSON generation issues | Refactor or remove |
| 🔴 No save system | Scope limitation | Implement PlayerPrefs |
| 🔴 Magic numbers | Rapid prototyping | Extract to constants |

---

## 🎯 Functionality

### 🎮 Core Game Loop

```
1. 🎴 Card Appears (DOTween animation)
2. 👆 Player Swipes (left/right decision)
3. 🎲 Outcome Resolves (probability-based)
4. 📊 Parameters Update (Event Bus)
5. ➕ Next Card Queued (dynamic deck)
6. 🔄 Loop Continues
```

### ✨ Feature Highlights

#### 1. 👆 Swipe Mechanics

<table>
<tr>
<td width="50%">

**Visual Feedback:**
- 🔄 Card rotation (±15°)
- 💬 Choice bubbles fade in
- ↩️ Smooth return if swipe too short
- 🚀 Flying exit animation

</td>
<td width="50%">

**Technical Details:**
- Threshold: 200px minimum
- Physics-based movement
- Touch & mouse compatible
- DOTween smooth easing

</td>
</tr>
</table>

#### 2. 📊 Parameter System

| Parameter | Icon | Color | Represents |
|-----------|------|-------|------------|
| Mood | 💗 | 🔴 Red | Emotional well-being |
| Family | 👨‍👩‍👧 | 🟡 Yellow | Family relationships |
| Money | 💰 | 🟢 Green | Financial status |
| Investments | 📊 | 🔵 Blue | Long-term planning |

**Mechanics:**
- ✅ 0-100 range with clamping
- ✅ Immediate visual updates
- ✅ Fill bar representation
- ✅ Color-coded feedback

#### 3. 📦 Content System

**54 JSON Cards** with rich data:

```json
{
  "id": 10,
  "is_start": true,
  "name": "Tutorial Card",
  "description": "Card situation...",
  "properties_yes": {
    "text": "Right choice",
    "pos": {
      "id_next": 300,
      "moves_next": 1,
      "chance": 0.7,
      "properties": {"mood": 5, "money": -10}
    }
  }
}
```

**Card Features:**
- ✅ Unique character scenarios
- ✅ Custom character sprites
- ✅ Branching narratives
- ✅ Parameter effects
- ✅ Probability-based outcomes

#### 4. 🎨 Visual Customization

**Two Distinct Themes:**
- 🎨 **Malevich** - Artistic suprematism style
- 🏢 **Gazprom** - Corporate brand colors

**Additional Options:**
- 🐛 Debug mode (show card IDs)
- 📱 Responsive UI (multiple resolutions)
- ♾️ Long game mode toggle

#### 5. 🔗 Special Features

| Feature | Description |
|---------|-------------|
| 🌐 **External Link** | Card ID -122 opens Gazprombank website |
| 🏁 **Multiple Endings** | Special IDs trigger different conclusions |
| 🎲 **Dynamic Content** | Fallback random generation mode |

---

## 🚧 Challenges and Limitations

### ✅ Technical Challenges Overcome

| Challenge | Solution |
|-----------|----------|
| ⚡ Event timing sync | DOTween callback integration |
| 🎴 Deck management | Custom insertion algorithm |
| 📱 UI responsiveness | Proportional sizing system |
| 🎬 Smooth animations | DOTween easing functions |

### ⚠️ Current Limitations

<details>
<summary><b>📋 Scope Limitations</b></summary>

- ❌ **No save system** - Game state resets
- ❌ **Limited endings** - Only deck depletion triggers end
- ❌ **No parameter endings** - Parameters tracked but don't end game
- ❌ **Single playthrough** - Limited replay value

</details>

<details>
<summary><b>💻 Technical Debt</b></summary>

```csharp
// TODO: These need addressing
const int MAGIC_NUMBER_122 = -122;  // Should be config
const int MAGIC_NUMBER_300 = -300;  // Should be config

// Commented code needs decision
// Either refactor or remove completely

// No unit tests
// Minimal error handling
```

</details>

<details>
<summary><b>📝 Content Limitations</b></summary>

- 🔢 **Fixed 54 cards** - No procedural generation
- 🌍 **Russian only** - No localization
- 🎨 **Limited sprites** - Optimized for size
- 🔇 **No audio** - Silent experience

</details>

### 🐛 Known Issues

- ⚠️ Probability distribution needs verification
- ⚠️ One-time shuffle at load
- ⚠️ No JSON validation for card references
- ⚠️ Extreme aspect ratios may need adjustment

---

## 🚀 How to Run

### 📋 Prerequisites

<table>
<tr>
<td width="50%">

**Required Software:**
- ✅ Unity Hub
- ✅ Unity 2022.3.46f1 LTS
- ✅ 4GB RAM minimum
- ✅ 2GB disk space

</td>
<td width="50%">

**Supported Platforms:**
- 🪟 Windows 10/11
- 🍎 macOS 10.14+
- 🐧 Ubuntu 20.04+

</td>
</tr>
</table>

### ⚙️ Installation Guide

**Step 1: Get the Project**
```bash
git clone https://github.com/r-1i/GazpromHack.git
cd GazpromHack
```

**Step 2: Open in Unity Hub**
1. Launch Unity Hub
2. Click **"Add" → "Add project from disk"**
3. Navigate to `GazpromHack` folder
4. Unity Hub auto-detects version
5. Install Unity 2022.3.46f1 if prompted
6. Open project

**Step 3: Verify Setup**
- ⏱️ Wait 2-5 minutes for import
- ✅ Check Console for errors
- ✅ Verify DOTween in `Assets/Plugins/Demigiant`

> 📦 **Missing DOTween?** Download free from Unity Asset Store

### ▶️ Running the Game

**Play in Editor:**
1. Open `Assets/Scenes/MainMenu.unity`
2. Press **Play** button (▶️)
3. Swipe cards with mouse

**Build for Mobile:**

<table>
<tr>
<td>

**🤖 Android**
```
File → Build Settings
Platform: Android
Texture: ASTC
→ Build and Run
```

</td>
<td>

**🍎 iOS**
```
File → Build Settings
Platform: iOS
→ Build
→ Open in Xcode
```

</td>
</tr>
</table>

### 🎛️ Configuration

**MainMenu Options:**
- 🎨 Color scheme selector
- 🐛 Card ID display toggle

**Advanced (Inspector):**
- ♾️ `longGame` boolean
- 📝 Edit JSON in `Assets/Resources/Cards/`

---

## 📂 Project Structure

```
GazpromHack/
│
├── 📁 Assets/
│   ├── 🖼️ Sprites/              # UI and character sprites
│   ├── 🎨 Font/                 # Typography assets
│   ├── 📦 Prefabs/              # Card prefab
│   ├── 🗂️ Resources/
│   │   ├── 🎴 Cards/            # 54 JSON card files
│   │   └── 🖼️ Images/           # Character sprites
│   │
│   ├── 🎬 Scenes/
│   │   ├── MainMenu.unity       # 🏠 Menu scene
│   │   └── SampleScene.unity    # 🎮 Game scene
│   │
│   ├── 📜 Scripts/
│   │   ├── CardJson.cs          # 📋 Data structures
│   │   ├── CardSpawner.cs       # 🎲 Deck logic
│   │   ├── SwipeDetector.cs     # 👆 Input handling
│   │   ├── ParametersListener.cs # 📊 Stats tracking
│   │   ├── UIController.cs      # 🎨 UI management
│   │   ├── MainMenu.cs          # 🏠 Menu logic
│   │   ├── SceneLoadValues.cs   # 💾 Persistence
│   │   └── EventBus/            # 📡 Event system
│   │
│   └── 🔌 Plugins/
│       └── Demigiant/           # DOTween library
│
├── 📦 Packages/                 # Unity packages
├── ⚙️ ProjectSettings/          # Unity config
└── 📖 README.md
```

---

## 📈 Future Improvements

### 🔴 High Priority

<table>
<tr>
<td width="33%">

#### 🎮 Game Design
- ✅ Win/Lose conditions
- ✅ Parameter-based endings
- ✅ Save/load system
- ✅ Tutorial system
- ✅ Multiple endings

</td>
<td width="33%">

#### 💻 Technical
- ✅ Refactor probability logic
- ✅ ScriptableObject configs
- ✅ Robust error handling
- ✅ Unit test coverage
- ✅ Performance profiling

</td>
<td width="33%">

#### 📝 Content
- ✅ 100+ cards
- ✅ Card categories
- ✅ Character arcs
- ✅ Seasonal events
- ✅ Story branches

</td>
</tr>
</table>

### 🟡 Medium Priority

<details>
<summary><b>✨ Polish & UX</b></summary>

**Audio System:**
- 🎵 Background music
- 🔊 Card swipe SFX
- 📊 Parameter change sounds
- 🌆 Ambient audio

**Visual Effects:**
- ✨ Particle effects
- 📳 Screen shake
- 💫 Glow effects
- 🎬 Animation variety

**Accessibility:**
- 📏 Text size options
- 🎨 Colorblind mode
- ⚙️ Difficulty settings

</details>

<details>
<summary><b>🌐 Social Features</b></summary>

- 🏆 Leaderboards
- 📅 Daily challenges
- 📱 Social media sharing
- 👥 Multiplayer mode (future)

</details>

### 🟢 Long-term Vision

| Category | Features |
|----------|----------|
| 🤖 **Advanced** | AI card generation, Mod support, Analytics |
| 🌍 **Platform** | Web version, Console ports, Steam release |
| 💼 **Business** | Bank integration, Financial education, Partnerships |

---

## 💡 Lessons Learned

### 🎯 What Went Well

| Success | Impact |
|---------|--------|
| ⚡ **Rapid Prototyping** | Shipped in hackathon timeframe |
| 🏗️ **Event Architecture** | Easy to extend features |
| 📋 **Data-Driven Design** | Non-programmers can add cards |
| ✨ **DOTween Integration** | Professional feel achieved |
| 📐 **Code Structure** | Maintainable despite speed |

### 🚀 Growth Areas

```
✅ Testing   → Start with unit tests from day 1
✅ Config    → Avoid hardcoded values early
✅ Git       → More frequent, descriptive commits
✅ Docs      → Code comments and diagrams
✅ Perf      → Mobile optimization from start
```

---

## 🎓 Conclusion

### 💼 For Potential Employers

<table>
<tr>
<td width="50%">

#### ✅ Demonstrated Skills

- 🎮 Unity 2022.3 LTS expertise
- 📝 Clean C# programming
- 🏛️ Event-driven architecture
- ✨ Professional animation (DOTween)
- 📊 Data-driven game design
- 🚀 Rapid prototyping ability

</td>
<td width="50%">

#### 🎯 Project Highlights

- ⏱️ 48-72h hackathon delivery
- 🎴 54 unique cards created
- 🎨 Multiple visual themes
- 📱 Mobile-first design
- 🏗️ Scalable architecture
- 📖 Comprehensive documentation

</td>
</tr>
</table>

> 💎 **Key Takeaway**: This project demonstrates the ability to deliver a polished, playable game prototype under hackathon constraints while maintaining clean architecture and professional code quality.

### 💬 Discussion Topics

Ready to discuss:
- 🏗️ Architecture decisions and trade-offs
- 📈 Scaling for production release  
- 🔧 Technical implementation details
- 👥 Team collaboration approaches
- 🎮 Game design methodology

---

## 📞 Contact

<div align="center">

[![GitHub](https://img.shields.io/badge/GitHub-r--1i-black?style=for-the-badge&logo=github)](https://github.com/r-1i)
[![Project](https://img.shields.io/badge/Project-GazpromHack-blue?style=for-the-badge&logo=unity)](https://github.com/r-1i/GazpromHack)

**[⬆ Back to Top](#-gazpromhack---reigns-like-card-decision-game)**

</div>

---

## 📊 Technical Specifications

<details>
<summary><b>🔧 Build Information</b></summary>

| Platform | Size | FPS Target | Load Time |
|----------|------|------------|-----------|
| 🤖 Android APK | ~30-40 MB | 60 FPS | <2s |
| 🍎 iOS IPA | ~35-45 MB | 60 FPS | <2s |
| 🪟 Windows | ~40-50 MB | 60 FPS | <2s |

**Performance Targets:**
- Mid-range mobile devices (2020+)
- <100ms touch response time
- Smooth 60 FPS gameplay

</details>

<details>
<summary><b>📦 Dependencies</b></summary>

**Required:**
- Unity 2022.3 LTS or higher
- DOTween (Free version)
- TextMesh Pro (included)

**Optional:**
- Unity Cloud Build
- Firebase Analytics (for production)

</details>

---

<div align="center">

**⭐ Star this project if you find it interesting!**

*Made with ❤️ during Gazprom Hackathon*

**Last Updated:** November 2025  
**Status:** ✅ Playable Prototype

</div>
