# Troubleshooting

## The extension is not listed in Ringworld settings

Confirm that the base is version 1.1.5, this extension is version 1.0.0, and its DLL is in `GameData/RingworldScattering/Plugins`. An extra `GameData/GameData` directory prevents assets from loading. Restart KSP after changing DLLs. Keep only one copy of each mod.

## Rendering is missing or performs poorly

Check the extension switch and quality settings in the Ringworld panel. Presets deliberately reduce effects on slower hardware; installation alone does not force maximum quality. Use Slow or lower on a laptop. Change one setting at a time, then apply it. Photo mode can use a different preset and restores the previous settings on exit.

The release rendering target is Windows KSP 1.12.5 with Direct3D 11. Other graphics APIs are not certified by this release. Native Linux and Proton/Wine need separate validation. A successful shader load alone does not establish visual correctness on every GPU.

## Reporting a problem

Provide the base and extension versions, KSP.log, GPU and graphics API, quality preset, location, and steps that reproduce it. Mention whether it also happens with the other extension removed. Screenshots help with visual faults; a craft file helps with vessel-specific faults. Remove personal information before posting logs.

Water physics, terrain, science, and saves belong to the base Ringworld mod. Removing this rendering extension does not remove a ring or change its mean water level.
