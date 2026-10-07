# CKAN metadata

## Development and release ownership

The [workspace workflow](../../../AGENTS.md) governs local development. The development assistant edits ordinary files, builds and tests locally, and packages ZIPs only when requested. All Git operations, GitHub/SpaceDock publication, and NetKAN pull requests are handled by the project owner. Publishing steps below are instructions for the owner, not authorization for automated publication.

Each component's `RELEASE-NOTES.md` must report required dependencies and version constraints, optional/suggested integrations, configuration-provider conflicts, installation layout, and validation limits for the version being prepared. Configuration release notes must distinguish the requirements of all four packs.

The proposed identifier is `RingworldScattering`. Submit the sibling `NetKAN/NetKAN/RingworldScattering.netkan` to the NetKAN maintainers after the GitHub release is available. The file uses the SpaceDock listing as its download source and installs only `GameData/RingworldScattering`.

Version 1.0.1 requires **NivenRingworld >= 1.1.7** because it uses the base rendering interface. Harmony is required through the base dependency. Cyla and the other Ringworld extension are optional. No dependency is bundled in the ZIP.

The base's metadata suggests this extension; a suggestion is not a requirement. Coordinate the two recipes when submitting them so an unindexed optional identifier does not delay the base update. Publishing the repository or attaching metadata to a release does not automatically add a mod to CKAN.

The author handles submission. Users can install manually until the entry is indexed, preserving the same GameData directory structure.
