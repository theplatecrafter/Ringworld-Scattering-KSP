using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
namespace NivenRingworld.Extensions
{
    internal static class ScatteringScreenCopy
    {
        private sealed class Buffers { internal CommandBuffer Copy,Release;internal bool Manual; }
        private static readonly Dictionary<Camera,Buffers> cameras=new Dictionary<Camera,Buffers>();
        private static bool enabled;
        private static bool visibilityResolved;private static Func<Camera,bool> waterVisible;
        private static bool Visible(Camera camera)
        {
            if(!visibilityResolved)
            {
                visibilityResolved=true;
                var type=typeof(RingworldFlight).Assembly.GetType("NivenRingworld.RingWaterVisibility",false);
                var method=type==null?null:type.GetMethod("Visible",BindingFlags.NonPublic|BindingFlags.Static);
                if(method!=null)waterVisible=(Func<Camera,bool>)Delegate.CreateDelegate(typeof(Func<Camera,bool>),method);
            }
            // Older supported hosts retain their full scene copy behaviour.
            return waterVisible==null||waterVisible(camera);
        }
        private static readonly int texture=Shader.PropertyToID("_RingWaterBackground");
        internal static void Enable(bool value)
        {
            if(enabled==value)return;
            enabled=value;
            if(value)Camera.onPreCull+=Prepare;
            else {Camera.onPreCull-=Prepare;foreach(var c in new List<Camera>(cameras.Keys))Detach(c);}
        }
        private static void Prepare(Camera camera)
        {
            if(camera==null)return;
            Buffers existing;if(cameras.TryGetValue(camera,out existing)&&existing.Manual)return;
            if((camera.cullingMask&(1<<15))!=0&&Visible(camera))Attach(camera,false);else Detach(camera);
        }
        internal static void Attach(Camera camera)
        {Attach(camera,true);}
        private static void Attach(Camera camera,bool manual)
        {
            camera.depthTextureMode|=DepthTextureMode.Depth;
            Buffers previous;if(cameras.TryGetValue(camera,out previous)){previous.Manual|=manual;return;}
            var b=new Buffers{Copy=new CommandBuffer{name="Ringworld Water scene copy"},Release=new CommandBuffer{name="Ringworld Water release"},Manual=manual};
            b.Copy.GetTemporaryRT(texture,-1,-1,0,FilterMode.Bilinear,camera.allowHDR?RenderTextureFormat.DefaultHDR:RenderTextureFormat.Default);
            b.Copy.Blit(BuiltinRenderTextureType.CameraTarget,texture);
            b.Copy.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);
            b.Release.ReleaseTemporaryRT(texture);
            camera.AddCommandBuffer(CameraEvent.BeforeForwardAlpha,b.Copy);
            camera.AddCommandBuffer(CameraEvent.AfterForwardAlpha,b.Release);cameras.Add(camera,b);
        }
        internal static void Detach(Camera camera)
        {
            Buffers b;if(!cameras.TryGetValue(camera,out b))return;
            if(camera!=null){camera.RemoveCommandBuffer(CameraEvent.BeforeForwardAlpha,b.Copy);camera.RemoveCommandBuffer(CameraEvent.AfterForwardAlpha,b.Release);}
            b.Copy.Release();b.Release.Release();cameras.Remove(camera);
        }
    }
}
