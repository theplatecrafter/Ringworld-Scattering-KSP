using System;
using Ringworld.Core;
using UnityEngine;
namespace NivenRingworld.Extensions
{
    // Optional presentation only. Mean water level, buoyancy and science belong
    // to the host; disabling this module must never change a saved vessel.
    internal static class WaterController
    {
        private static readonly Vector4[] spectrum=new Vector4[12];
        private static readonly double[] kxValues=new double[12],kyValues=new double[12],speeds=new double[12],offsets=new double[12],amplitudes=new double[12];
        private static Settings spectrumSettings;private static int spectrumSeed;private static double spectrumGravity;
        private static void PrepareSpectrum(Settings settings)
        {
            int seed=settings.Geometry.P.Seed;double gravity=settings.Geometry.P.Gravity;
            if(spectrumSettings==settings&&spectrumSeed==seed&&spectrumGravity==gravity)return;
            spectrumSettings=settings;spectrumSeed=seed;spectrumGravity=gravity;
            for(int i=0;i<12;i++)
            {
                double k=2*Math.PI/(210*Math.Pow(.59,i)),direction=.5+(settings.Terrain.Scatter(i,0,1907)-.5)*2.6;
                kxValues[i]=k*Math.Cos(direction);kyValues[i]=k*Math.Sin(direction);speeds[i]=Math.Sqrt(gravity*k);
                offsets[i]=settings.Terrain.Scatter(i,0,1913)*Math.PI*2;amplitudes[i]=.45*Math.Exp(-i*.42);
            }
        }
        internal static void Update(Settings settings,Material material,Shader fallback,DVec observer,Vector3d star)
        {
#if RINGWORLD_FRAME_PROFILING
            long profile=RingPerformance.Begin();
            try{UpdateSurface(settings,material,fallback,observer,star);}finally{RingPerformance.End(RingPerformance.Section.WaterSurface,profile);}
#else
            UpdateSurface(settings,material,fallback,observer,star);
#endif
        }
        private static void UpdateSurface(Settings settings,Material material,Shader fallback,DVec observer,Vector3d star)
        {
            RingPoint p=settings.Geometry.Coordinates(observer);
            var f=RingworldFlight.Instance;int waterQuality=settings.WaterExtension?settings.WaterQuality:0;
            bool refracting=false;
            if(f!=null&&f.visuals!=null)
            {
                var shader=f.visuals.WaterShader(waterQuality>=3);
                if(shader==null||!shader.isSupported)shader=f.visuals.WaterShader();
                if(shader!=null&&shader.isSupported)
                {
                    material.shader=shader;refracting=shader.name=="NivenRingworld/WaterRefraction";
                    material.SetVector("_WaveCamera",(Vector3)(star+ConvertVector.Ksp(observer)));
                    material.SetVector("_WaveAlong",ConvertVector.Unity(settings.Geometry.AlongDirection(observer)));
                    material.SetVector("_WaveAcross",ConvertVector.Unity(settings.Geometry.Axis));material.SetVector("_WaveUp",ConvertVector.Unity(settings.Geometry.Up(observer)));
                    double time=Planetarium.GetUniversalTime();
                    material.SetFloat("_WaterScattering",settings.WaterScattering?1:0);
                    material.SetVector("_WaterOpticsChart",new Vector4((float)RingGeometry.Wrap(p.Along,Math.PI*200),(float)RingGeometry.Wrap(p.Across,Math.PI*200),(float)RingGeometry.Wrap(time*.35,Math.PI*200),0));
                    var weather=settings.Weather(p.Along,p.Across,time);
                    PrepareSpectrum(settings);
                    int count=waterQuality>=4?12:waterQuality>=3?9:waterQuality>=2?6:3;
                    for(int i=0;i<12;i++)
                    {
                        double phaseValue=kxValues[i]*p.Along+kyValues[i]*p.Across-speeds[i]*time+offsets[i];
                        spectrum[i]=new Vector4((float)kxValues[i],(float)kyValues[i],(float)RingGeometry.Wrap(phaseValue,Math.PI*2),(float)amplitudes[i]);
                    }
                    material.SetVectorArray("_SeaWaves",spectrum);material.SetFloat("_SeaCount",count);
                    material.SetFloat("_SeaWind",(float)(.4+weather.Storm*.6));
                    material.SetVector("_WaveSun",ConvertVector.Unity(RingLighting.Direction(settings,observer,time)));

                    material.SetVector("_Wave",new Vector4(0,0,0,waterQuality>=2?(float)settings.WaveHeight:0));
                    material.SetVector("_WavePhase",new Vector4(Phase(p.Along*.037+p.Across*.012-time*1.1),Phase(-p.Along*.016+p.Across*.029-time*.8),Phase(p.Along*.063-p.Across*.054-time*1.7),0));
                    material.SetVector("_RipplePhase",new Vector4(Phase(p.Along*1.7+p.Across*.64-time*2),Phase(p.Across*1.3-p.Along*.92+time*1.6),Phase(p.Along*5+p.Across*3.1+time*2.4),Phase(p.Across*4.2-p.Along*3.7-time*2.1)));
                    material.SetVector("_NoiseOffset",new Vector4((float)RingGeometry.Wrap(p.Along,65536),(float)RingGeometry.Wrap(p.Across,65536),(float)RingGeometry.Wrap(time*.15,65536),0));
                    material.SetFloat("_WaterLight",(float)settings.Geometry.Daylight(p.Along,time,p.Across,p.Altitude));material.SetFloat("_WaterQuality",waterQuality);
                }
                else material.shader=fallback;
            }
            else material.shader=fallback;
            ScatteringScreenCopy.Enable(refracting);
            UnderwaterEffect.Enable(settings.WaterExtension&&waterQuality>0);
        }
        private static float Phase(double value){return (float)RingGeometry.Wrap(value,Math.PI*2);}
    }
}
