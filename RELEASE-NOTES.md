# Release notes

## 1.0.3 — artifacts prepared 15 October 2026

Local NetKAN follow-up: this component's requirements stay unchanged after checking the released host SDKs. Clouds 1.0.2 compiles with base 1.1.5 and 1.1.9; Scattering 1.0.3 compiles with base 1.1.8 and 1.1.9. The optional water-visibility hook retains older-host fallback. See the base [metadata audit](../Ring%20World%20KSP/docs/developers/NETKAN-COMPATIBILITY.md) for targeted runtime evidence. The already prepared ZIPs remain unchanged; the Parallax recipe separately gains explicit texture-loader/stock-scatter requirements.


[Player-facing release notes](docs/releases/v1.0.3.md). Local preparation only; the owner handles publication.

Requires NivenRingworld >=1.1.8 and its transitive requirements; host 1.1.10 enables the optional reflected water-visibility hook, with previous-copy fallback on older supported hosts. No upstream Scatterer dependency. Clouds, Parallax and Cyla optional. Install into GameData/RingworldScattering.

No dependency/minimum-version change from the previous component release. No DLL/type rename, save-format change, terrain-generator change or reset. Existing ring identities, world parameters, resident poses, progress and explicit settings are retained. Stable assembly identities are preserved while file/product versions advance. Standalone archives contain only their own mod and documentation. No requirement or install-path change is needed in this component's NetKAN recipe.

Release validation (15 October 2026): fresh `StartupCompatibility-release-1.1.10-retained.log` passed retained formats 3/4/pre-1.1.8 format 5, repeated migration/options/generator/pose/progress preservation, legacy resident restore/resave and real Flight Map -> Tracking Station -> Space Center transitions with KSPCF. Original retained files were hash-protected. Historical scenario data and a synthetic legacy resident are complementary coverage; this does not certify every old craft/mod binary combination. Current renderer source also passed `FramePerformance-overcast-followup-high.log`, `ParallaxPhoto-performance-4.log` and `Issue7Performance-rendering-final-9.log` before the version-only preparation. Core/placement suites: 143,203/18,050. No serialization or preset-budget changes. Normal DLLs omit test harnesses; archive checks verify versions, CRCs, hashes, paths and dependency exclusions.

### water rendering performance (15 October 2026)

- Skip automatic scene-colour copies when no live water renderer can enter that camera's frustum. The new base host supplies visibility; older supported hosts retain the previous copying behaviour. Explicit camera attachments remain force-enabled for photo/probe callers, including non-flight layers.
- Cache shader assets and time-independent wave-spectrum coefficients, while updating phases, sunlight and weather normally. Reuse identical terrain samples between medium detection and underwater rendering within a frame. Full-ring atmosphere updates return early when that layer is disabled.
- The base host reduces full-ring atmosphere vertex preparation using a composed double-precision transform and conservative analytic bounds. Above/below-water scattering, refraction, waves, opacity and physics settings are retained.

Required NivenRingworld minimum remains 1.1.8; Harmony/ModuleManager and existing configuration/provider requirements are supplied through the base. No upstream Scatterer dependency, DLL rename, new external dependency, metadata edit or save-format change. Development profiling references are absent from normal extension binaries. This release packages the current normal DLL and assets; older ZIPs are preserved.

Native checks passed automatic copy attachment/removal for visible/off-frustum/disabled/staged water, explicit probe-camera attachment, the refraction shader at qualities 1/3, deep-column absorption and the quality-4 scattering toggle in a small GPU probe. These are presentation checks, not new physics changes or a desktop-preset certification. The paired whole-ocean workload measured 3.15→3.34 FPS; underwater measured 3.02 FPS without an original baseline. See the base [performance report](../Ring%20World%20KSP/docs/developers/FRAME-PERFORMANCE-20261015.md) for variability and limits.

## 1.0.2 - artifacts prepared October 9, 2026

Coordinated with NivenRingworld >= 1.1.8. Includes camera-relative full-ring atmosphere alignment and exterior-hull occlusion, plus the current water refraction/absorption, above/below-water scattering and underwater light implementation. Source/shader validation history follows below. No upstream Scatterer dependency or asset bundle is included.

Required: NivenRingworld min_version 1.1.8; KSP 1.12.5 Windows x64 / D3D11. The base supplies Harmony2 >=2.2.1.0, ModuleManager and a selected config. No additional required mod. RingworldClouds, Cyla and RingworldParallax are optional. Dependency change from 1.0.1: raise minimum base from 1.1.7 to 1.1.8 for the new camera/exterior API. Owner should update NetKAN with min_version, not an exact-version dependency; metadata is unchanged locally.

Standalone archive merges GameData/RingworldScattering at the KSP root and includes documentation. No dependencies bundled; no publication. Existing ring geometry, water levels and save format are unchanged. Higher-preset desktop performance remains unverified.

## Unreleased - manual modpack inclusion

The current local Scattering DLL/shaders are included with the matching base, Clouds and full-size interstellar config in the sibling `Ringworld Modpacks` artifact. Harmony2, ModuleManager and Kopernicus's required stack are bundled only in that explicitly requested combined pack. Standalone Scattering packaging remains dependency-free. External dependencies and component versions are unchanged; no NetKAN edit or publication. See the modpack release notes for assembled-pack validation.

## Unreleased - exterior hull occlusion

The full-ring atmosphere respects the base mod's double-precision exterior-camera classification. A camera below the scrith hull within its width no longer sees the interior atmosphere overlay. Views into the habitat through an open rim retain the existing ray/hull visibility tests.

Install this updated shader bundle with the matching development base DLL and visual bundle. Released base 1.1.7 does not provide the exterior flag; this correction belongs to the next coordinated base/extension release. KSP 1.12.5 and Harmony through the base remain required; Cyla and Ringworld Clouds remain optional. There are no new dependencies, NetKAN changes, packaged archives or publications. Installation remains `GameData/RingworldScattering`.

Local runtime validation on October 8 passed the full-ring atmosphere's interior-visible/exterior-hidden GPU probe and an actual scaled-space exterior camera view. The latter retained dark scrith with no bright habitat layers. Flight checks used Slow on Windows/Direct3D 11; see the base mod's terrain texture validation for the diagnostic log and scope.

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
