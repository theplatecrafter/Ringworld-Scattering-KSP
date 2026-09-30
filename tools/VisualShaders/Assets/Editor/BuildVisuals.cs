using System;using System.IO;using UnityEditor;using UnityEngine;
public static class BuildVisuals {
 public static void Run(){
  string output=Path.GetFullPath(Path.Combine(Application.dataPath,"../../../GameData/RingworldScattering/Assets"));Directory.CreateDirectory(output);
  var bundle=new AssetBundleBuild{assetBundleName="ringworldscattering",assetNames=new[]{"Assets/Shaders/RingWater.shader","Assets/Shaders/RingWaterRefraction.shader","Assets/Shaders/RingUnderwater.shader","Assets/Shaders/FullRingAtmosphere.shader"}};
  EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone,BuildTarget.StandaloneWindows64);
  PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows64,new[]{UnityEngine.Rendering.GraphicsDeviceType.Direct3D11});
  if(BuildPipeline.BuildAssetBundles(output,new[]{bundle},BuildAssetBundleOptions.ForceRebuildAssetBundle|BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.StandaloneWindows64)==null)throw new Exception("Shader build failed");
  Debug.Log("RINGWORLD VISUAL BUNDLE BUILT: "+output);
 }
}
