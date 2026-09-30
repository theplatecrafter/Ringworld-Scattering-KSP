using UnityEngine;
using Ringworld.Core;
namespace NivenRingworld.Extensions {
 internal sealed class ScatteringProvider : IScatteringProvider {
  public AssetBundle Assets {get{return ExtensionAssets.Acquire();}}
  public Shader WaterShader(bool refraction){return ExtensionAssets.Shader(refraction?"RingWaterRefraction":"RingWater");}
  public IDistantAtmosphere Create(Transform parent,Settings settings){var bundle=ExtensionAssets.Acquire();return bundle==null?null:new DistantAtmosphere(parent,settings,bundle);}
  public void UpdateWater(Settings settings,Material material,Shader fallback,DVec observer,Vector3d star){WaterController.Update(settings,material,fallback,observer,star);}
  public void ScreenCopy(bool value){ScatteringScreenCopy.Enable(value);}
  public void Underwater(bool value){UnderwaterEffect.Enable(value);}
  public void Attach(Camera camera){ScatteringScreenCopy.Attach(camera);}
  public void Detach(Camera camera){ScatteringScreenCopy.Detach(camera);}
 }
}
