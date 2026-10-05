using UnityEngine;
namespace NivenRingworld.Extensions
{
    // Camera medium effect, independent of water tile bounds. Only active below
    // sampled mean water level; visual only, no change to buoyancy/collision.
    public sealed class UnderwaterEffect : MonoBehaviour
    {
        private Material material;private AssetBundle bundle;private Camera view;
        internal static void Enable(bool value)
        {
            var camera=FlightCamera.fetch==null?null:FlightCamera.fetch.mainCamera;if(camera==null)return;
            // Avoid a full-screen copy above water, especially on low presets.
            var flight=RingworldFlight.Instance;
            if(value&&flight!=null&&flight.FrameInUse&&!MapView.MapIsEnabled)
            {
                var point=ConvertVector.Core((Vector3d)camera.transform.position-flight.Center);
                var c=flight.Settings.Geometry.Coordinates(point);var t=flight.Settings.Terrain.Sample(c.Along,c.Across);
                value=t.Wet&&c.Altitude<t.WaterHeight&&c.Altitude>=t.Height-2;
            }
            else value=false;
            var effect=camera.GetComponent<UnderwaterEffect>();
            if(value&&effect==null)effect=camera.gameObject.AddComponent<UnderwaterEffect>();
            if(effect!=null)effect.enabled=value;
        }
        private void Awake(){view=GetComponent<Camera>();view.depthTextureMode|=DepthTextureMode.Depth;}
        private void OnRenderImage(RenderTexture source,RenderTexture destination)
        {
            var f=RingworldFlight.Instance;
            if(f==null||!f.FrameInUse||MapView.MapIsEnabled||!f.Settings.WaterExtension||f.Settings.WaterQuality<1){Graphics.Blit(source,destination);return;}
            var s=f.Settings;var point=ConvertVector.Core((Vector3d)view.transform.position-f.Center);
            var c=s.Geometry.Coordinates(point);var terrain=s.Terrain.Sample(c.Along,c.Across);
            double depth=terrain.WaterHeight-c.Altitude;
            if(!terrain.Wet||depth<=0||c.Altitude<terrain.Height-2){Graphics.Blit(source,destination);return;}
            if(material==null)
            {
                bundle=ExtensionAssets.Acquire();var shader=bundle==null?null:bundle.LoadAsset<Shader>("Assets/Shaders/RingUnderwater.shader");
                if(shader!=null&&shader.isSupported)material=new Material(shader);
                else {if(bundle!=null){ExtensionAssets.Release();bundle=null;}Graphics.Blit(source,destination);return;}
            }
            double time=Planetarium.GetUniversalTime();float tangent=Mathf.Tan(view.fieldOfView*Mathf.Deg2Rad*.5f);
            material.SetVector("_WaterOptics",new Vector4((float)depth,(float)s.Geometry.Daylight(c.Along,time,c.Across,c.Altitude),s.WaterScattering?s.WaterQuality:0,(float)Ringworld.Core.RingGeometry.Wrap(time*.35,System.Math.PI*200)));
            material.SetVector("_WaterChart",new Vector4((float)Ringworld.Core.RingGeometry.Wrap(c.Along,System.Math.PI*200),(float)Ringworld.Core.RingGeometry.Wrap(c.Across,System.Math.PI*200),0,0));
            material.SetVector("_WaterAlong",ConvertVector.Unity(s.Geometry.AlongDirection(point)));material.SetVector("_WaterAcross",ConvertVector.Unity(s.Geometry.Axis));
            material.SetVector("_WaterUp",ConvertVector.Unity(s.Geometry.Up(point)));
            material.SetVector("_WaterSun",ConvertVector.Unity(RingLighting.Direction(s,point,time)));
            material.SetVector("_ViewForward",view.transform.forward);material.SetVector("_ViewRight",view.transform.right*tangent*view.aspect);material.SetVector("_ViewUp",view.transform.up*tangent);
            Graphics.Blit(source,destination,material);
        }
        private void OnDestroy(){if(material!=null)Destroy(material);if(bundle!=null)ExtensionAssets.Release();}
    }
}
