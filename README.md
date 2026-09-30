# Ringworld Scattering

Enhanced water surfaces, underwater scattering and distant full-ring atmosphere for Niven's Ringworld.

This optional extension requires **Niven Ringworld 1.1.5** and KSP 1.12.5. Install Harmony 2 (2.2.1.0 or newer) as required by the base mod. No dependencies are bundled. The base Ringworld mod works without this extension; installing it does not change terrain, water levels, gravity, science or saved vessels.

## Installation

1. Install [Niven Ringworld](https://github.com/theplatecrafter/Ring-World-KSP-mod/releases) and its required dependencies.
2. Download this extension's ZIP from [Releases](https://github.com/theplatecrafter/Ringworld-Scattering-KSP/releases).
3. Extract into the **KSP installation directory**, merging GameData. The result must be `GameData/RingworldScattering/Plugins`, not `GameData/GameData`.
4. Restart KSP. The Ringworld settings panel reports the installed extensions. Rendering switches and quality remain configurable per ring.

Offline documentation is installed under `GameData/RingworldScattering/Documentation`, so it does not overwrite the base mod's documents.

## Documentation

- [Rendering and settings](docs/guides/SETTINGS.md)
- [Building and contributing](docs/developers/BUILDING.md)
- [Release notes](RELEASE-NOTES.md)
- [Credits](CREDITS.md) and [license](LICENSE)

Report bugs with KSP.log, the base and extension versions, graphics API/GPU and reproduction steps in [Issues](https://github.com/theplatecrafter/Ringworld-Scattering-KSP/issues). Remove private save data before posting logs.

## Compatibility

Cyla is optional and independent: it can provide the local atmospheric rendering alongside this extension. Ringworld Clouds and Ringworld Scattering can be installed independently or together. This is original Ringworld-specific rendering, not EVE or Scatterer transplanted onto a planet. Installing those planetary mods does not provide their planetary effects on the ring.

## Uninstalling

Close KSP and remove only `GameData/RingworldScattering`. The base mod retains the ring and its gameplay. Back up saves before changing any mod installation.
