# GitHub Copilot Instructions for [XND] Tiny Tweaks (Continued) Mod

## Overview and Purpose
"[XND] Tiny Tweaks (Continued)" is a RimWorld mod that aggregates a series of small but significant quality-of-life improvements and adjustments aimed at refining the gameplay experience. The mod is inspired by other comprehensive modification packs like the 4M Mehni Misc Modifications and the TD Enhancement Pack. It focuses on enhancing multiple facets of the game with features that are customizable through the mod settings, allowing players to tailor their experience as desired.

## Key Features and Systems
- **Animal Medical Alerts**: Ensures bonded animals under threat receive timely alerts, preventing unnoticed critical conditions.
- **Caravan Food Restrictions**: Provides the ability to set specific food restrictions for caravans, enhancing resource management during trips.
- **AutoOwl Schedule Adjustments**: Automatically sets Night Owl character timetables to a 11 AM - 7 PM schedule, avoiding mood debuffs associated with incorrect sleep timings.
- **Medical Bed Alerts**: Keeps 'Colonist needs treatment' alerts visible even when colonists are in medical beds.
- **Item Label Capitalization**: Adjusts mod-added item labels to match vanilla RimWorld capitalization standards.
- **Melee Weapon AP Scaling Fix**: Corrects the spearhead attack AP calculations, ensuring compatibility with weapon materials' damage multipliers.
- **Delayed Skill Decay**: Implements a delay in skill decay when substantial experience is gained, smoothing out skill leveling progression.

Each feature can be toggled on or off via the mod settings menu, providing a customizable experience.

## Coding Patterns and Conventions
- **C# Style and Practices**: The code follows standard C# conventions including the use of PascalCase for class names and methods, and camelCase for local variables.
- **Static and Instance Classes**: The mod employs both static and instance class structures depending on whether shared state or instance behavior is required.
- **Method Segmentation**: Effective separation of logic within methods to adhere to single-responsibility principles.
- **Commenting and Documentation**: Classes and methods are documented, facilitating understanding and collaboration.

## XML Integration
- Utilizes XML for various configuration aspects of the mod, like defining new items, traits, and game settings.
- XML files are kept organized and are used in conjunction with C# to bind settings from the UI to behavior in the game.

## Harmony Patching
- The mod is powered by the Harmony patching library, allowing for dynamic alterations of existing base game methods without direct modification.
- Harmony patches are encapsulated in a dedicated `HarmonyPatches.cs` file.
- Patches target specific game methods to inject additional logic, ensuring minimal conflicts with other mods.

## Suggestions for Copilot
- **Feature Expansion**: When auto-suggesting new features, consider proposing additional configurable elements within existing systems or suggesting acts for broader systems integration.
- **Code Efficiency**: Encourage refactoring suggestions that enhance performance, such as optimizing loop structures or minimizing redundant operations.
- **Debugging Assistance**: Suggest logging patterns or debug outputs in instances where feature logic might introduce errors.
- **Compatibility Enhancements**: Recommend saving and loading strategies that mitigate potential issues when integrating new mods or removing existing ones.

By adhering to these guidelines, GitHub Copilot can be leveraged effectively to assist in maintaining and expanding the [XND] Tiny Tweaks (Continued) mod, ensuring it remains a robust and highly favored addition to the RimWorld community.
