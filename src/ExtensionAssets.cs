using System.IO;
using System.Collections.Generic;
using UnityEngine;
namespace NivenRingworld.Extensions {
 internal static class ExtensionAssets {
  static AssetBundle bundle;
  static readonly Dictionary<string,Shader> shaders=new Dictionary<string,Shader>();
  internal static AssetBundle Acquire(){if(bundle==null)bundle=AssetBundle.LoadFromFile(Path.Combine(KSPUtil.ApplicationRootPath,"GameData","RingworldScattering","Assets","ringworldscattering"));return bundle;}
  // Bundle lives for the KSP process: several ring/camera clients share its assets.
  internal static void Release(){}
  internal static Shader Shader(string name){Shader shader;if(shaders.TryGetValue(name,out shader))return shader;var b=Acquire();if(b==null)return null;shader=b.LoadAsset<Shader>("Assets/Shaders/"+name+".shader");shaders[name]=shader;return shader;}
 }
}
