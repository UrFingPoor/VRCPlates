using HarmonyLib;
using MelonLoader;
using System;

namespace VRCPlates.Patches
{
    class _OnPlayer 
    {
        public static void InitOnPlayer()
        {
            try
            {
                VRCPlatesPatches.Instance.Patch(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_0)), new HarmonyMethod(AccessTools.Method(typeof(_OnPlayer), nameof(OnPlayerJoin))));
                VRCPlatesPatches.Instance.Patch(typeof(NetworkManager).GetMethod(nameof(NetworkManager.Method_Public_Void_Player_2)), new HarmonyMethod(AccessTools.Method(typeof(_OnPlayer), nameof(OnPlayerLeave))));
                MelonLogger.Msg("[Patch] OnPlayer Patched!");
            }
            catch (Exception ex)
            {
                MelonLogger.Msg("[Patch] Patching OnPlayer Failed!\n" + ex);
            }
        }


        public static void OnPlayerJoin(ref VRC.Player __0)
        {
            if (__0.prop_APIUser_0 == null) return;
            VRCPlateNameplates.UpdatePlayerNameplates(__0);
            return;
        }

        public static void OnPlayerLeave(ref VRC.Player __0)
        {
            if (__0.prop_APIUser_0 == null) return;
            VRCPlateNameplates.UpdatePlayerNameplates(__0);
            return;
        }

    }
        
}
