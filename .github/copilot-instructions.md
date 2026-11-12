# GitHub Copilot Instructions for [XND] Tiny Tweaks (Continued)

## Mod Overview and Purpose

[XND] Tiny Tweaks (Continued) is a RimWorld mod that provides a collection of small improvements to enhance the gameplay experience. The mod aims to address various aspects of the game, such as animal medical alerts, caravan food restrictions, and delayed skill decay, among others. All features are fully configurable through mod settings, allowing players to enable or disable specific tweaks based on their preferences.

## Key Features and Systems

### Animal Medical Alerts
- Ensures that bonded animals receive timely medical attention, preventing them from bleeding out unnoticed.

### Caravan Food Restrictions
- Allows players to assign specific food restrictions to their caravans, ensuring that the correct food is available for the journey.

### Night Owls Timetable
- Automatically sets the timetables for night owls to the correct 11 AM to 7 PM sleeping schedule, reducing mood debuffs.

### Medical Bed Alerts
- Prevents the hiding of 'Colonist needs treatment' alerts when a colonist is in a medical bed.

### Vanilla-style Label Capitalization
- Applies vanilla-style label capitalization to mod-added items, ensuring consistency with the game's naming conventions.

### Melee Weapon AP Scaling
- Fixes the issue where spearhead attack AP ignores the weapon's material damage multipliers.

### Delayed Skill Decay
- Delays skill decay when a substantial amount of experience is gained, providing smoother skill progression.

## Coding Patterns and Conventions

### Coding Patterns
- **Modular Design**: Each feature is encapsulated within its own class or module, making the codebase easier to maintain and extend.
- **Harmony Patching**: The mod utilizes the Harmony Patch Library to modify vanilla game behavior without altering the original codebase.

### Coding Conventions
- **Namespace**: All code is placed within the `TinyTweaks` namespace.
- **Naming Conventions**: Class names are PascalCase, while method and variable names are camelCase.
- **Comments**: Extensive comments are provided to explain complex logic and the purpose of each feature.
- **Error Handling**: Proper error handling is implemented to ensure the mod's stability and reliability.

## XML Integration

[XND] Tiny Tweaks (Continued) does not currently utilize any XML files for its definitions. However, the mod is designed to be easily extendable, and future updates may incorporate XML integration for additional customization.

## Harmony Patching

The mod relies heavily on Harmony patching to modify vanilla game behavior. Here are some key examples of Harmony patches used in the mod:

- **Alert_ColonistNeedsTend_NeedingColonists**: Modifies the `AllowIfMedical` method to ensure that colonist needs treatment alerts are not hidden when in a medical bed.
- **CompLaunchable_TryLaunch**: Postfix patch to modify the behavior of the `TryLaunch` method in the `CompLaunchable` class.
- **CompTerrainPump_CompTickRare**: Postfix and prefix patches to modify the `CompTickRare` method in the `CompTerrainPump` class.

## Suggestions for Copilot

### General Suggestions
- **Feature Requests**: If you have any feature requests or ideas for improvements, please feel free to suggest them in the GitHub repository.
- **Bug Reports**: If you encounter any bugs or issues, please report them using the Log Uploader and provide as much detail as possible.
- **Code Contributions**: Contributions are welcome! If you have any improvements or new features, feel free to submit a pull request.

### Specific Suggestions for Copilot
- **Harmony Patching**: When generating Harmony patches, ensure that the patches are correctly applied to the target methods and that they do not conflict with other mods.
- **Modular Design**: When implementing new features, follow the modular design pattern to ensure that the code is easy to maintain and extend.
- **Configuration Settings**: When adding new features, ensure that they can be easily enabled or disabled through the mod settings.

By following these guidelines and suggestions, you can contribute to the continued development and improvement of [XND] Tiny Tweaks (Continued).

Thank you for using [XND] Tiny Tweaks (Continued) and for any feedback or contributions you may provide!

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

