# Water and atmosphere settings

Open the Ringworld panel, choose **Settings**, find **Ringworld extensions**, and enable or disable **Ringworld Water: enhanced surface rendering**. Click **Apply settings**, then save the game to retain the choice. Each ring stores its own choice. Quality presets change water quality but preserve the extension switch.

| Water quality | Rendering |
| --- | --- |
| Extension disabled / Flat | Basic translucent, depth-tinted water |
| Ripples | Animated normals, approximate sky reflection, underwater absorption and haze |
| Waves | Visual waves, finer ripples and shoreline foam |
| Detailed | Screen refraction, oblique-path absorption, sun glitter and 8-sample underwater light shafts |
| Ultra | Detailed rendering with finer surface detail and 16-sample underwater light shafts |

Use Ripples or lower on a laptop. Detailed and Ultra copy the visible screen once per camera render for the water refraction pass; they cost more even if only a small patch of water is visible. Photo mode can temporarily use a higher quality preset without changing the saved extension switch.

Close-range water already has terrain and collision beneath it. Transparent local LOD water now retains a separate seabed mesh rather than replacing the ground with its water surface. Far scaled-space water remains an opaque approximation. Ocean basin shapes and mean water levels are unchanged, preserving existing landing and swimming positions.

Water remains swimmable and buoyant when the extension is disabled. Wave crests are visual; physics uses the mean water level. Terrain generation, science and saves belong to the main mod.

This is original Ringworld rendering, not Scatterer running on the ring. Reflections currently approximate the sky; ships, buildings and the distant ring are not reflected. Refraction samples the current camera image: it cannot reveal objects outside the image, and displaced samples can show foreground-edge artifacts. Underwater haze uses camera-to-scene distance and stops at the mean water surface. Detailed/Ultra add approximate surface-modulated sunlight shafts, dimmed by depth and panel night. These are not ray-traced rays or shadows cast by vessels and terrain. Object reflections remain planned work; refraction is validated against the available scene depth.

### Scattering across the water surface

Detailed and Ultra integrate the submerged part of the view ray even while the camera is above water. Scene depth limits absorption to the first opaque submerged object or seabed; a shallow object is no longer attenuated using the entire depth of a deep ocean. Clear shallow water transmits more of the scene, while long underwater paths progressively lose contrast and red light. Deep oceans are not expected to reveal their entire floor.

**Settings -> Ringworld extensions -> Water light shafts (above and below surface)** controls the extra sampled illumination. It requires enhanced water and Detailed/Ultra water quality; Strong and higher presets enable it, lower presets disable it. Disabling shafts retains basic absorption and scattered water colour. Save the game after applying changes to retain the setting. Below-surface camera scattering and above-surface transmission share the same water-medium model. The expensive camera postprocess only runs while submerged; above-water scattering runs on visible water pixels.

The depth path uses opaque objects that participate in Unity's depth pass. Transparent objects or materials without a depth/shadow pass may not supply a usable endpoint. Refraction remains a screen-space approximation, and shafts are not shadows cast by scene geometry.


## Distant atmosphere

Enable Full-ring atmosphere in Ringworld settings to add a lightweight optical-column approximation visible in map, space and distant landed views. Local Cyla or Original still handles the nearby sky. Disable it independently of water. This is not full-ring physical multiple-scattering ray tracing.
