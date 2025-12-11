# Creative-Technology

Assets/
│
├── Scripts/
│   ├── PlayerInteractor.cs
│   ├── PanelButton.cs
│   ├── PanelSlider.cs
│   ├── ControlPanelManager.cs
│   ├── LaserShooter.cs
│   ├── Target.cs
│   ├── TargetSpawner.cs
│   ├── ScoreManager.cs
│   ├── GameTimer.cs
│   └── IInteractable.cs
│
├── Materials/
│   ├── Neon Materials
│   ├── Transparent Glass Materials
│   └── Neon Glow Materials
│
├── Prefabs/
│   ├── Target.prefab
│   ├── Control Panel Buttons
│   ├── Volume Slider Handle
│   └── Light Bulb Particles
│
├── Audio/
│   ├── BackgroundMusic.mp3
│   └── LaserShot.wav
│
├── Scenes/
│   ├── LunarArena.unity
│   └── (Optional) TestScene.unity
│
└── UI/
    ├── ScoreText
    ├── TimerText
    └── CountdownText



| Action                       | Key        |
| ---------------------------- | ---------- |
| Move                         | WASD       |
| Look                         | Mouse      |
| Shoot Laser                  | Left Click |
| Interact with Buttons/Slider | E          |
| Lock/Unlock Mouse            | Esc        |



Gameplay Flow

Approach the control panel

Press Start Game to begin

Countdown plays: 3 → 2 → 1 → GO!

Targets spawn around the arena

Shoot targets using the laser

Score increases per hit

When time runs out, targets disappear

Score is finalised and saved as high score

🛠️ System Functionality
Laser Shooting System

Shoots a raycast from the laser origin

Displays a glowing line using LineRenderer

Plays firing sound

Destroys targets on hit

Interactive Control Panel

Music Toggle Button

Lights Toggle Button

Start Game Button (starts timer + spawner)

Volume Slider with glow feedback

Target System

Random neon color

Random size

Emission glow highlight when aimed at

Particle burst on destruction

Timer & Score System

30-second training session

Score resets each game

High score saved using PlayerPrefs

Countdown animation

💡 Project Requirements
Unity Version

✔ Recommended: Unity 2021 LTS or later
(The project also works in many 2022 versions.)
