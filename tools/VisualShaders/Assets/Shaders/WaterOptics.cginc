// Shared water medium for surface-to-scene and submerged-camera paths.
float3 WaterTransmission(float path) { return exp(-float3(.025,.009,.004)*max(0,path)); }
float WaterShafts(float path,float depth,float2 chart,float3 ray,float3 sun,float time,float quality)
{
 if(quality<3)return 0;
 int steps=quality>=4?16:8;float result=0;
 [loop]for(int j=0;j<16;j++)
 {
  if(j>=steps)break;
  float t=path*(j+.5)/steps;
  float localDepth=max(0,depth-ray.z*t);
  float2 q=chart+ray.xy*t+sun.xy*localDepth/max(.15,sun.z);
  float pattern=pow(saturate(.5+.25*sin(q.x*.31+time)+.25*sin(q.y*.27-time*.73)),4);
  result+=pattern*exp(-localDepth*.015-t*.009)/steps;
 }
 return result;
}
float3 WaterScatter(float depth,float mu,float daylight,float shafts)
{
 float light=daylight*exp(-max(0,depth)*.015);
 return float3(.015,.26,.31)*(.06+light*(.65+pow(saturate(mu),24)*.6+shafts*2));
}
