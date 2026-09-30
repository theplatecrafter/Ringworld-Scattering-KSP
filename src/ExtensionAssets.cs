using System.IO;
using UnityEngine;
namespace NivenRingworld.Extensions {
 internal static class ExtensionAssets {
  static AssetBundle bundle;
  internal static AssetBundle Acquire(){if(bundle==null)bundle=AssetBundle.LoadFromFile(Path.Combine(KSPUtil.ApplicationRootPath,"GameData","RingworldScattering","Assets","ringworldscattering"));return bundle;}
  // Bundle lives for the KSP process: several ring/camera clients share its assets.
  internal static void Release(){}
  internal static Shader Shader(string name){var b=Acquire();return b==null?null:b.LoadAsset<Shader>("Assets/Shaders/"+name+".shader");}
 }
}
