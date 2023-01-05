using HarmonyLib;
using Photon.Realtime;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace SpacewareCheats.SDK.Patching.Patches
{
    public static class _Logginsg 
    {
        public static void InitLoggsing()
        {
            try
            {
                LethalPatches.Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "Log" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(_Logging), nameof(Debug))));
                LethalPatches.Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogError" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(_Logging), nameof(DebugError))));
                LethalPatches.Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogWarning" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(_Logging), nameof(DebugWarning))));
                LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] Logger");
            }
            catch { LogHandler.Log(LogHandler.Colors.Red, "[Patch] [Error] Logger"); }          
        }
        [Obfuscation(Exclude = true)]
        private static bool DebugError(ref Il2CppSystem.Object __0)
        {
           // if (UnityLogger.Instance == null)
                return true;

           // if (UnityLogger.Instance.toggled)
             //   SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[UnityError] " + Il2CppSystem.Convert.ToString(__0), true, false);
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool DebugWarning(ref Il2CppSystem.Object __0)
        {
           // if (UnityLogger.Instance == null)
                return true;

           // if (UnityLogger.Instance.toggled)
                //SDK.LogHandler.Log(SDK.LogHandler.Colors.Yellow, "[UnityWarning] " + Il2CppSystem.Convert.ToString(__0), true, false);
            return true;
        }

        [Obfuscation(Exclude = true)]
        private static bool Debug(ref Il2CppSystem.Object __0)
        {
           // if (UnityLogger.Instance == null)
                return true;

            //if (UnityLogger.Instance.toggled)
            //    SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Unity] " + Il2CppSystem.Convert.ToString(__0), true, false);
            return true;
        }

        private static bool PlayerJoin(ref VRC.Player __0)
        {
            if (__0 == null) return true;
            if (MainUI.ESPStatus)
            {
                Module.Render.CapsuleEsp.WireframeESP(__0, true);
            }
            else
            {
                Module.Render.CapsuleEsp.WireframeESP(__0, false);
            }

            return true;
        }
      
    }
}
