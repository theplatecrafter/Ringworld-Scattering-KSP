"""Validate the extension archive against the build and distributable assets."""
from pathlib import Path
import hashlib, json, zipfile
import yaml
root=Path(__file__).resolve().parents[1]
mod="RingworldScattering"
release=json.loads((root/"GameData"/mod/(mod+".version")).read_text(encoding="utf-8"))["VERSION"]
version=".".join(str(release[k]) for k in ("MAJOR","MINOR","PATCH"))
archive=root/"artifacts"/(mod+"-"+version+".zip")
with zipfile.ZipFile(archive) as z:
    assert z.testzip() is None, "ZIP CRC failure"
    names={n.replace("\\","/"):n for n in z.namelist()}
    assert {n.split("/")[1] for n in names if n.startswith("GameData/")}=={mod}, "Bundled dependency or wrong install root"
    for p in (root/"GameData"/mod).rglob("*"):
        if p.is_file():
            key=p.relative_to(root).as_posix()
            assert z.read(names[key])==p.read_bytes(), "Asset mismatch: "+key
    dll="Ringworld.Scattering.dll"
    data=z.read(names["GameData/"+mod+"/Plugins/"+dll])
    assert data==(root/"src/bin/Release/net472"/dll).read_bytes(), "DLL mismatch"
    assert b"SmokeTest" not in data, "Test code in archive"
    for key in names:
        assert not any(x in key.lower() for x in ("assembly-csharp", "unityengine", "persistent.sfs", "template_instance", ".csproj")), key
    for doc in ("README.md","RELEASE-NOTES.md","LICENSE","CREDITS.md","THIRD-PARTY-NOTICES.md","docs/README.md"):
        assert z.read(names["GameData/"+mod+"/Documentation/"+doc])==(root/doc).read_bytes(), "Document mismatch: "+doc
    recipes=list(yaml.safe_load_all((root.parent/"NetKAN"/"NetKAN"/(mod+".netkan")).read_text(encoding="utf-8")))
    assert recipes, "Missing NetKAN source recipes"
    for metadata in recipes:
        assert metadata["identifier"] == mod
        assert any(d["name"]=="NivenRingworld" and d.get("min_version")=="1.1.7" for d in metadata["depends"])

digest=hashlib.sha256(archive.read_bytes()).hexdigest()
assert (archive.with_suffix(".zip.sha256")).read_text(encoding="utf-8-sig").split()[0]==digest
print("PASS:", archive.name, "CRC, assets, DLL, docs, dependencies and checksum")
