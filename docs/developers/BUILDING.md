# Building

Use .NET SDK with net472 reference assemblies and a legally installed KSP 1.12.5. The default layout places this repository beside `Ring World KSP` and `template_instance`. Build the base first, then run `./build.ps1 -Install`. Override `-RingworldRoot` and `-KspRoot` for another layout. To package, use `./build.ps1 -Package`.

Shader sources are under tools/VisualShaders. Use Unity 2019.4.18f1 and `./build-visuals.ps1` to rebuild the included Direct3D 11 bundle. Other graphics APIs need separately built and tested bundles; native Linux/OpenGL is not certified. Windows KSP through Proton is a separate compatibility path.

The initial extension interface is a version-paired first-party ABI in the base `Extensions/ExtensionProviders.cs`. The base discovers providers without a hard DLL dependency; friend assembly access lets the extension read the same geometry/settings rather than reimplement physics. Version 1.0.0 is paired with base 1.1.5. Updating the base's internal interface requires rebuilding/testing these extensions; do not claim arbitrary future-version compatibility. General third-party gameplay integration should use the public RingworldSurfaceApi instead.

Never commit game assemblies, test saves, third-party mods, Unity caches or authentication credentials. Releases contain only this extension and documentation.

## Validation and release

After `./build.ps1 -Package`, run `python tools/verify_release.py`. It compares the ZIP against the compiled DLL and distributable assets, checks its dependency metadata, and rejects bundled game assemblies or dependencies.

The base repository's `tools/test-extension-matrix.ps1` exercises base-only, each extension separately, and both installed. It runs shader probes at small resolutions and the combined flight test with the Slow preset. These checks do not replace visual review on other graphics hardware. Full scenes above Slow are not part of the laptop test matrix.

The provider interface is internal and version paired; preserve the exact base dependency until a compatible new version is tested. Do not make saved gameplay depend on this rendering package.
