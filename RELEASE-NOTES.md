# Release notes

## Unreleased development

- Water wave and underwater scattering axes follow the inclination of the ring. Requires the matching development base build with inclined ring geometry.
- Water and underwater illumination use the associated star's actual direction. The distant atmosphere receives physical eclipses from celestial bodies, other ring hulls, rim walls and enabled panels, through the base mod's shared eclipse data texture.

The distant atmosphere respects the base mod's per-ring day/night-panel toggle. This requires the matching development base build; it is not included in the published 1.0.0 package.

## 1.0.0 ? September 30, 2026

Enhanced water with waves and sun glints, depth-limited refraction/transmission, underwater absorption and approximate light shafts, plus a separately switchable full-ring atmosphere approximation.

This is the first standalone release, extracted from Niven Ringworld's development renderer. Requires **NivenRingworld 1.1.5** and KSP 1.12.5. Harmony is required by the base. Cyla and the other Ringworld extension are optional. Dependencies are installed separately and never bundled.

Extract the ZIP into the KSP root, merging GameData. The base remains usable without this extension. Settings and presets stay in the Ringworld control panel; removing the extension does not remove terrain, water physics or saved vessels.

### Validation and limits

Base-only, each-extension and combined installation tests passed on Windows/Direct3D 11. Full flight scenes used Slow on the development laptop; higher shader modes used small GPU probes. Combined tests passed photo capture/restoration. This is not certification for every GPU or graphics API.

The renderer is original Ringworld code, not an EVE/Scatterer port. See the settings guide for approximations and missing features. CKAN metadata is included in the repository and attached to this release for submission; an attached recipe does not itself establish CKAN availability.
