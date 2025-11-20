# 🎴 GazpromHack - Card Game with Decision Making

<div align="center">

[![Play Game](https://img.shields.io/badge/🎮_PLAY_GAME_NOW-4CAF50?style=for-the-badge&logoColor=white)](https://r-1i.github.io/GazpromHackathon-Stories-Builds/)
![Unity](https://img.shields.io/badge/Unity-2022.3.46f1-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=c-sharp)
![Platform](https://img.shields.io/badge/Platform-WebGL-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Prototype-orange?style=for-the-badge)

**[🇬🇧 English Version](README.md)**

*Interactive card game where every swipe shapes your destiny*

[📖 Overview] • [🚀 Quick Start] • [⚙️ Architecture] • [🎯 Features] • [📈 Plans]

</div>

---

## 📖 Project Overview

<table>
<tr>
<td>

Stories — is a **decision-making game**, created for Gazprom hackathon, inspired by the popular mobile game "Reigns". Players go through stories of Gazprombank clients, making important life decisions by swiping cards left or right.

Each decision affects **four key life parameters**:
- 💗 **Mood** - Emotional wellbeing
- 👨‍👩‍👧 **Family** - Family relationships
- 💰 **Money** - Financial state
- 📊 **Investments** - Long-term planning

</td>
<td width="40%">

### 🎮 Brief Data

| | |
|---|---|
| **Type** | Mobile card game |
| **Genre** | Decision simulator, Narrative |
| **Platform** | WebGL |
| **Cards** | 54 unique scenarios |
| **Status** | ✅ Playable demo |
| **Development** | 48-72h |

</td>
</tr>
</table>

> 💡 **Main Idea**: Players master financial decisions through engaging gameplay — it's simultaneously learning and entertainment.

---

## 🛠️ Technology Stack

<table>
<tr>
<td width="50%">

### 🎮 Game Engine
- **Unity 2022.3.46f1 LTS**
- Programming language **C#**
- **Unity 2D Feature Set 2.0.1**

### 📦 Unity Packages
- **TextMesh Pro 3.0.6** - Typography
- **Unity UI (uGUI) 1.0.0** - Interface
- **Unity Timeline 1.7.6** - Animation

</td>
<td width="50%">

### 🔧 Third-party Libraries
- **DOTween (Demigiant)** - Animation system
  - Smooth card transitions
  - Easing functions

### 🏗️ Architectural Patterns
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
│   JSON cards    │ ← 54 card definitions
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│  CardSpawner    │ ← Deck management
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ SwipeDetector   │ ← Player interaction
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│   Event Bus     │ ← System communication
└────────┬────────┘
         │
         ├─────────────┬──────────────┐
         ▼             ▼              ▼
┌──────────────┐ ┌──────────┐ ┌────────────────┐
│ Parameter    │ │ UI       │ │ Adding         │
│ update       │ │ update   │ │ next card      │
└──────────────┘ └──────────┘ └────────────────┘
```

### 🏛️ Core Components

<details>
<summary><b>1. 📡 Event-Driven Architecture</b></summary>

**Custom Event Bus system** for decoupled component communication:

```csharp
EventBus → EventBusHolder (Singleton)
    ↓
Events:
    - OnDestroyCardEvent (card swipe)
    - SetParametersEvent (parameter change)
```

**Advantages:**
- ✅ Weak system coupling
- ✅ Easy feature addition
- ✅ Clear data flow
- ✅ High testability

</details>

<details>
<summary><b>2. 🎴 Card System Architecture</b></summary>

**Data Flow:**
```
JSON files → Deserialization → SwipeDetector → Event Bus → Actions
```

**Card Structure:**
- Two choices per card (swipe left/right)
- Three possible outcomes (positive, negative, neutral)
- Customizable outcome probabilities
- Parameter change effects
- Branching narrative paths

</details>

<details>
<summary><b>3. 💾 Game State Management</b></summary>

**Persistent Settings:**
- `SceneLoadValues` persists between scenes
- Multiple color schemes (Malevich/Gazprom)
- Debug mode with card ID display

**Parameter System:**
- Range: 0-100 for each parameter
- Real-time updates through events
- Smooth visual transitions

</details>

<details>
<summary><b>4. 🔄 Dynamic Deck Management</b></summary>

**Smart Card Spawning:**
- Initial deck from "starting" cards
- Dynamic card insertion at specific positions
- Card repetition prevention
- Fallback random generation (long game mode)

</details>

### 🎨 Design Patterns in Action

| Pattern | Implementation | Purpose |
|---------|------------|-----------|
| **Singleton** | `UIController`, `EventBusHolder` | Global manager access |
| **Observer** | Event Bus listeners | Parameter updates |
| **Factory** | Card creation from JSON | Dynamic content creation |
| **Strategy** | Outcome types (pos/neg/neu) | Flexible decision results |

---

## 🎯 Features

### 🎮 Core Game Loop

```
1. 🎴 Card appears (DOTween animation)
2. 👆 Player swipes (left/right decision)
3. 🎲 Outcome resolves (probability-based)
4. 📊 Parameters update (Event Bus)
5. ➕ Next card added (dynamic deck)
6. 🔄 Loop continues
```

### ✨ Key Features

#### 1. 👆 Swipe Mechanics

<table>
<tr>
<td width="50%">

**Visual Feedback:**
- 🔄 Card rotation (±15°)
- 💬 Choice bubble popup
- ↩️ Smooth return on short swipe
- 🚀 Fly-out animation

</td>
<td width="50%">

**Technical Details:**
- Threshold: minimum 200px
- Physical movement
- Touch and mouse compatibility
- Smooth DOTween easing

</td>
</tr>
</table>

#### 2. 📊 Parameter System

| Parameter | Icon | Color | Meaning |
|----------|--------|------|----------|
| Mood | 💗 | 🔴 Red | Emotional wellbeing |
| Family | 👨‍👩‍👧 | 🟡 Yellow | Family relationships |
| Money | 💰 | 🟢 Green | Financial state |
| Investments | 📊 | 🔵 Blue | Long-term planning |

**Mechanics:**
- ✅ Range 0-100 with clamping
- ✅ Immediate visual update
- ✅ Fill bar representation
- ✅ Color feedback

#### 3. 📦 Content System

**54 cards in JSON** with rich data:

```json
{
  "id": 10,
  "is_start": true,
  "name": "Tutorial card",
  "description": "Situation on card...",
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

**Card Capabilities:**
- ✅ Unique character scenarios
- ✅ Custom character sprites
- ✅ Branching narratives
- ✅ Parameter effects
- ✅ Probability-based outcomes

#### 4. 🎨 Visual Customization

**Two Separate Themes:**
- 🎨 **Malevich** - Suprematism art style
- 🏢 **Gazprom** - Corporate brand colors

**Additional Options:**
- 🐛 Debug mode (show card IDs)
- 📱 Adaptive UI (multiple resolutions)
- ♾️ Long game mode toggle

#### 5. 🔗 Special Features

| Feature | Description |
|---------|----------|
| 🌐 **External Link** | Card with ID -122 opens Gazprombank website |
| 🏁 **Multiple Endings** | Special IDs trigger different conclusions |
| 🎲 **Dynamic Content** | Fallback random generation mode |

---

## 🚧 Challenges and Limitations

### ✅ Overcome Technical Challenges

| Challenge | Solution |
|-------|---------|
| ⚡ Event synchronization | DOTween callback integration |
| 🎴 Deck management | Custom insertion algorithm |
| 📱 UI responsiveness | Proportional scaling system |
| 🎬 Smooth animations | DOTween easing functions |

<details>
<summary><b>📝 Content Limitations</b></summary>

- 🌍 **Russian only** - No localization
- 🎨 **Limited sprites** - Size optimization

</details>


## 🚀 How to Run

### 📋 Prerequisites

<table>
<tr>
<td width="50%">

**Required Software:**
- ✅ Unity Hub
- ✅ Unity 2022.3.46f1 LTS
- ✅ Minimum 4GB RAM
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

### ⚙️ Installation Instructions

**Step 1: Clone Project**
```bash
git clone https://github.com/r-1i/GazpromHack.git
cd GazpromHack
```

**Step 2: Open in Unity Hub**
1. Launch Unity Hub
2. Click **"Add" → "Add project from disk"**
3. Navigate to `GazpromHack` folder
4. Unity Hub will automatically detect version
5. Install Unity 2022.3.46f1 if prompted
6. Open project

**Step 3: Check Setup**
- ⏱️ Wait 2-5 minutes for import
- ✅ Check Console for errors
- ✅ Ensure DOTween in `Assets/Plugins/Demigiant`

> 📦 **Missing DOTween?** Download free from Unity Asset Store

### ▶️ Running the Game

**Game in Editor:**
1. Open `Assets/Scenes/MainMenu`
2. Press **Play** button (▶️)
3. Swipe cards with mouse


### 🎛️ Configuration

**MainMenu Options:**
- 🎨 Color scheme selection
- 🐛 Card ID display toggle

**Advanced (Inspector):**
- ♾️ Boolean `longGame`
- 📝 JSON editing in `Assets/Resources/Cards/`

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
│   │   ├── ParametersListener.cs # 📊 Stat tracking
│   │   ├── UIController.cs      # 🎨 UI management
│   │   ├── MainMenu.cs          # 🏠 Menu logic
│   │   ├── SceneLoadValues.cs   # 💾 Persistence
│   │   └── EventBus/            # 📡 Event system
│   │
│   └── 🔌 Plugins/
│       └── Demigiant/           # DOTween library
│
├── 📦 Packages/                 # Unity packages
├── ⚙️ ProjectSettings/          # Unity configuration
└── 📖 README.md
```

---

## 📈 Future Improvements

### 🔴 High Priority

<table>
<tr>
<td width="33%">

#### 🎮 Game Design
- ✅ Win/loss conditions
- ✅ Parameter-based endings
- ✅ Save/load system
- ✅ Tutorial system
- ✅ Multiple endings

</td>
<td width="33%">

#### 💻 Technical
- ✅ Probability logic refactoring
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
<summary><b>✨ Polish and UX</b></summary>

**Audio System:**
- 🎵 Background music
- 🔊 Card swipe sounds
- 📊 Parameter change sounds
- 🌆 Ambient audio

**Visual Effects:**
- ✨ Particle systems
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

</details>

### 🟢 Long-term Vision

| Category | Features |
|-----------|---------|
| 🤖 **Advanced** | AI card generation, Mod support, Analytics |
| 🌍 **Platforms** | Web version, Console porting, Steam release |
| 💼 **Business** | Bank integration, Financial education, Partnerships |

---

## 💡 Lessons Learned

### 🎯 What Went Well

| Success | Impact |
|-------|---------|
| ⚡ **Rapid Prototyping** | Delivered within hackathon deadline |
| 🏗️ **Event Architecture** | Easy feature expansion |
| 📋 **Data-Driven Design** | Non-programmers can add cards |
| ✨ **DOTween Integration** | Professional look achieved |
| 📐 **Code Structure** | Maintainable code despite speed |

### 🚀 Growth Areas

```
✅ Testing     → Start with unit tests from day one
✅ Configuration → Avoid hardcoding from the start
✅ Git          → More frequent, descriptive commits
✅ Documentation → Code comments and diagrams
✅ Performance  → Mobile optimization from start
```

---

## 🎓 Conclusion

</table>

> 💎 **Key Takeaway**: This project demonstrates the ability to create a polished, playable game prototype under hackathon conditions while maintaining clean architecture and code quality.

### 💬 Discussion Topics

Ready to discuss:
- 🏗️ Architectural decisions and tradeoffs
- 📈 Scaling for production release
- 🔧 Technical implementation details
- 👥 Team collaboration approaches

---

## 📞 Contacts

<div align="center">

[![GitHub](https://img.shields.io/badge/GitHub-r--1i-black?style=for-the-badge&logo=github)](https://github.com/r-1i)
[![Project](https://img.shields.io/badge/Project-GazpromHack-blue?style=for-the-badge&logo=unity)](https://github.com/r-1i/GazpromHack)

**[⬆ Back to Top](#-gazpromhack---card-game-with-decision-making)**

</div>

---

## 📊 Technical Specifications

<details>
<summary><b>📦 Dependencies</b></summary>

**Required:**
- Unity 2022.3 LTS or higher
- DOTween (free version)
- TextMesh Pro (included)

</details>

---

<div align="center">

**⭐ Star this project if it interests you!**

*Made with ❤️ during Gazprom hackathon*

**Last Update:** November 2025  
**Status:** ✅ Playable prototype

</div>
