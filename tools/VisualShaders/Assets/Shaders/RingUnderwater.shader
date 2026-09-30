Shader "NivenRingworld/Underwater"
{
 Properties { _MainTex ("Scene", 2D) = "white" {} }
 SubShader { Cull Off ZWrite Off ZTest Always
 Pass { CGPROGRAM
 #pragma vertex vert_img
 #pragma fragment frag
 #pragma target 3.0
 #include "UnityCG.cginc"
 #include "WaterOptics.cginc"
 sampler2D _MainTex;
 UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
 float4 _WaterOptics; // camera depth, daylight, quality, time
 float2 _WaterChart;float3 _WaterAlong,_WaterAcross;
 float3 _WaterUp,_WaterSun,_ViewForward,_ViewRight,_ViewUp;
 float4 frag(v2f_img i):SV_Target
 {
  float4 scene=tex2D(_MainTex,i.uv);
  #ifdef UNITY_COLORSPACE_GAMMA
   scene.rgb=GammaToLinearSpace(scene.rgb);
  #endif
  float3 ray=normalize(_ViewForward+_ViewRight*(i.uv.x*2-1)+_ViewUp*(i.uv.y*2-1));
  float eye=LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv));
  float distanceInWater=min(300,eye/max(.05,dot(ray,normalize(_ViewForward))));
  float up=dot(ray,_WaterUp);
  if(up>0)distanceInWater=min(distanceInWater,_WaterOptics.x/max(.001,up));
  distanceInWater=max(0,distanceInWater);
  float3 transmittance=WaterTransmission(distanceInWater);
  float3 chartRay=float3(dot(ray,_WaterAlong),dot(ray,_WaterAcross),up);
  float3 chartSun=float3(dot(_WaterSun,_WaterAlong),dot(_WaterSun,_WaterAcross),dot(_WaterSun,_WaterUp));
  float shafts=WaterShafts(distanceInWater,_WaterOptics.x,_WaterChart,chartRay,chartSun,_WaterOptics.w,_WaterOptics.z);
  float3 scatter=WaterScatter(_WaterOptics.x,dot(ray,_WaterSun),_WaterOptics.y,shafts);
  float wet=smoothstep(0,.35,_WaterOptics.x);
  float3 color=lerp(scene.rgb,scene.rgb*transmittance+scatter*(1-transmittance),wet);
  #ifdef UNITY_COLORSPACE_GAMMA
   color=LinearToGammaSpace(color);
  #endif
  return float4(color,scene.a);
 }
 ENDCG }
 }
}
