using HarmonyLib;
using System;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.Core;

namespace SpacewareCheats.SDK.Patching.Patches
{
    public static class _AvatarAssetBundleLoad 
    {
        public static void InitAOnAssetBundleLoad()
        {
            try
            {
                LethalPatches.Instance.Patch(typeof(AssetManagement).GetMethod("Method_Public_Static_Object_Object_Boolean_Boolean_Boolean_0"), new HarmonyMethod(AccessTools.Method(typeof(_AvatarAssetBundleLoad), nameof(OnAvatarAssetBundleLoad))));
               // AlienPatch.Instance.Patch(typeof(AssetManagement).GetMethod("Method_Public_Static_Object_Object_Vector3_Quaternion_Boolean_Boolean_Boolean_0"), new HarmonyMethod(AccessTools.Method(typeof(_AvatarAssetBundleLoad), nameof(OnAssetRequestDownload))));

                SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] AssetBundle");
            }
            catch
            {
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] [Error] AssetBundle");
            }
        }


 
        //public static bool OnAssetRequestDownload(IntPtr AssetPointer, Vector3 Position, Quaternion Rotation, byte AllowCustomShader, byte IsUI, byte Validate, IntPtr NativeMethodPointer)
        //{
        //    return true;
        //}

        [Obfuscation(Exclude = true)]
        private static bool OnAvatarAssetBundleLoad(ref UnityEngine.Object __0)
        {
            try
            {
                GameObject gameObject = __0.TryCast<GameObject>();
                if (gameObject == null)
                {
                    return true;
                }
                if (!gameObject.name.ToLower().Contains("avatar"))
                {
                    return true;
                }
                string avatarId = gameObject.GetComponent<PipelineManager>().blueprintId;
                for (int i = 0; i < Main.Instance.OnAssetBundleLoadEventArray.Length; i++)
                    if (!Main.Instance.OnAssetBundleLoadEventArray[i].OnAvatarAssetBundleLoad(gameObject, avatarId))
                        return false;
            }
            catch {  }
            return true;
        }

    }
}
