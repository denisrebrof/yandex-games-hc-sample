# yandex-games-hc-sample
Hypercasual (and not only) Unity WebGL game template for common web game platforms.

# Supported Platforms
* Crazy Games
* Poki Games
* Yandex Games
* VK Games

Planned to add support:
* Kongregate
* another platforms ...

# Features:
* Common Game Mechanics:
  * Balance
  * Daily reward
  * Hints
  * Levels
  * Purchases
  * Sound enabled setting
* Common browser games platforms actions
* * Happy Time (on CrazyGames, Poki)
* * Yandex Games - metrika hit
* * Record Sharing (only VK support now)
* Analytics
* Ads
* Localization (RU & EN)
* Setup Wizard (WIP)
* Platform selection Editor Tooling

# Main Dependencies:
* UniRx
* Zenject
* GameAnalytics

# Modules list (folders in "Scripts"):
|Name|Description|
|---|---|
|Ads|Contains Ad Navigator with set of decorators (stats, platform-dependent stuff, etc.).<br />Only Interstital Ad is supported for now, WIP Rewarded Video support.|
|Analytics|Contains Analytics adapter - you can replace default GameAnalytics adapter with another.|
|Balance|Contains only single currency "Coins" with use cases & reactive repository + ui components.|
|Daily Reward|Coins reward support only|
|Gameplay|Useful utility MonoBehaviours + WIP Player Input System to correct touch input handling on mobile devices|
|Hints|Localized hints UI system|
|Levels|Levels list ui, levels repository with corresponding prefabs, level prefabs|
|Localization|Get Language from platform support, ui components|
|Purchases|Single purchase by coins/rewarded video/level completion, repo & use cases|
|Rewarded Video|WIP, should be moved to "Ads"|
|SDK|Platform - related stuff, switch platform editor tools, common mechincs like HappyTime or Game State|
|Social|Record Sharing (only VK support now)|
|Sound|Sound enabled setting by AudioMixed|
|Utils|Editor utils - SetupWizard, DefineSymbols utils, utility MonoBehaviours (may be moved into Gameplay)|
