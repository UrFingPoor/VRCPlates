using HarmonyLib;
using MelonLoader;
using System;
using System.Reflection;
using VRC.Networking;

namespace LethalClient.SDK.Patching.Patches
{
    public static class _Udon 
    {
        public static  bool DisableUdon = false;
        public static void InitUdon()
        {
            try
            {
                LethalPatches.Instance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), new HarmonyMethod(AccessTools.Method(typeof(_Udon), nameof(OnUdon))));
                LethalPatches.Instance.Patch(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_0)), new HarmonyMethod(AccessTools.Method(typeof(_Udon), nameof(OnPlayerJoin))));
                LethalPatches.Instance.Patch(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_2)), new HarmonyMethod(AccessTools.Method(typeof(_Udon), nameof(OnPlayerLeave))));
                LogHandler.Log("[Patch] UDON Patched!");
            }
            catch (Exception ex)
            {
                LogHandler.Log("[Patch] Patching UDON Failed!\n" + ex);
            }
        }

        [Obfuscation(Exclude = true)]
        private static bool OnUdon(string __0, VRC.Player __1)
        {
            if (DisableUdon)
            {
                if (__1.field_Private_APIUser_0.id == Utils.LocalPlayer.prop_APIUser_0.id)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return true;
        }

        public static void OnPlayerJoin(ref VRC.Player __0)
        {
            if (Features.LethalNameplates.toggled)
            {
                if (__0.prop_APIUser_0 == null) return;
                Features.LethalNameplates.UpdatePlayerNameplates(__0);
            }
            return;
        }

        public static void OnPlayerLeave(ref VRC.Player __0)
        {
            if (Features.LethalNameplates.toggled)
            {
                if (__0.prop_APIUser_0 == null) return;
                Features.LethalNameplates.UpdatePlayerNameplates(__0);
            }
            return;
        }
    }
}
