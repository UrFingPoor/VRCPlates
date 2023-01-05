using HarmonyLib;
using System;
using System.Reflection;
using VRC.Networking;

namespace SpacewareCheats.SDK.Patching.Patches
{
    public static class _Udon 
    {
        public static void InitUdon()
        {
            try
            {
                LethalPatches.Instance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), new HarmonyMethod(AccessTools.Method(typeof(_Udon), nameof(OnUdon))));
                LogHandler.Log(LogHandler.Colors.Green, "[Patch] Udon");
            }
            catch (Exception ex)
            {
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] Could not patch Udon failed\n");
            }
        }

        [Obfuscation(Exclude = true)]
        private static bool OnUdon(string __0, VRC.Player __1)
        {
            for (int i = 0; i < Main.Instance.OnUdonEventArray.Length; i++)
                if (!Main.Instance.OnUdonEventArray[i].OnUdon(__0, __1))
                    return false;
            return true;
        }
    }
}
