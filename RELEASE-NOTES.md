# Release notes

## Unreleased ? map-camera alignment

The full-ring atmosphere uploads camera-relative geometry immediately before each scaled-space camera render. Ring coordinates remain in double precision until upload, and grazing optical distance uses the relative position directly. This addresses camera-motion lag and precision differences between the atmosphere and the base ring surface in Map View and Tracking Station.

Requires a matching base-mod build containing `CameraRelativeRingMesh`; the released base 1.1.7 binary does not contain that helper. The release owner must assign the next base/extension versions and require that new base version in NetKAN before publishing this extension. KSP 1.12.5 and Harmony through the base remain required. Cyla and Ringworld Clouds remain optional; no dependencies are bundled. Installation remains `GameData/RingworldScattering`, extracted from the KSP root. No artifact or publication has been made for these changes.

Validation: the extension and shader bundle compile. Runtime checks are tracked in the base project's `docs/developers/MAP-RENDERING.md`.

## 1.0.1 — October 5, 2026

Requires **NivenRingworld >= 1.1.7**. Dependencies are installed separately. CKAN metadata is maintained in the dedicated NetKAN checkout.

- Water wave and underwater scattering axes follow the inclination of the ring. Uses the inclined ring geometry in base 1.1.7.
- Water and underwater illumination use the associated star's actual direction. The distant atmosphere receives physical eclipses from celestial bodies, other ring hulls, rim walls and enabled panels, through the base mod's shared eclipse data texture.

The distant atmosphere respects the base mod's per-ring day/night-panel toggle. This requires base 1.1.7 or newer.

## 1.0.0 ? September 30, 2026

Enhanced water with waves and sun glints, depth-limited refraction/transmission, underwater absorption and approximate light shafts, plus a separately switchable full-ring atmosphere approximation.

This is the first standalone release, extracted from Niven Ringworld's development renderer. Requires **NivenRingworld 1.1.5** and KSP 1.12.5. Harmony is required by the base. Cyla and the other Ringworld extension are optional. Dependencies are installed separately and never bundled.

Extract the ZIP into the KSP root, merging GameData. The base remains usable without this extension. Settings and presets stay in the Ringworld control panel; removing the extension does not remove terrain, water physics or saved vessels.

### Validation and limits

Base-only, each-extension and combined installation tests passed on Windows/Direct3D 11. Full flight scenes used Slow on the development laptop; higher shader modes used small GPU probes. Combined tests passed photo capture/restoration. This is not certification for every GPU or graphics API.

The renderer is original Ringworld code, not an EVE/Scatterer port. See the settings guide for approximations and missing features. CKAN metadata is included in the repository and attached to this release for submission; an attached recipe does not itself establish CKAN availability.
