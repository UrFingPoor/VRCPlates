using System;
using System.Reflection;
using VRCPlates.SDK;

namespace VRCPlates.Patches
{
    class VRCPlatesPatches
    {
        public static HarmonyLib.Harmony Harmony { get; set; }
        public static HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("VRCPlates");

        [Obsolete]
        public static Harmony.HarmonyMethod GetLocalPatch(Type type, string methodName)
        {
            return new Harmony.HarmonyMethod(type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
        }
        public static void InitPatches()
        {
            try
            {
                MelonLoader.MelonLogger.Msg("[Patch] Starting Patches....");
                _Hwid.InitHwidPatch();
                _OnPlayer.InitOnPlayer();
            }
            catch (Exception ERR) { MelonLoader.MelonLogger.Msg(ERR.Message); }
            
        }
    }
}
