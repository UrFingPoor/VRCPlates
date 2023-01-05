using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using VRC.SDKBase;

namespace LethalClient.SDK.Patching.Patches
{
    public static class _OnEvent
    {
        public static List<int> blacklistedPlayers = new List<int>();
        public static void InitOnEvent()
        {
            try
            {
                //AlienPatch.Instance.Patch(AccessTools.Property(typeof(PhotonPeer), "RoundTripTime").GetMethod, null, new HarmonyMethod(typeof(_OnEvent), "Pings"));
                //AlienPatch.Instance.Patch(AccessTools.Property(typeof(Time), "smoothDeltaTime").GetMethod, null, new HarmonyMethod(typeof(_OnEvent), "Frames"));
                LethalPatches.Instance.Patch(typeof(LoadBalancingClient).GetMethod("OnEvent"), new HarmonyMethod(AccessTools.Method(typeof(_OnEvent), nameof(OnEvent)))); //method_Public_Virtual_New_Void_EventData_0"
                LethalPatches.Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "OnEvent", null, null), new HarmonyMethod(AccessTools.Method(typeof(_OnEvent), nameof(OnEvent))));
                LethalPatches.Instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0"), new HarmonyMethod(AccessTools.Method(typeof(_OnEvent), nameof(OnRPC))));
                LethalPatches.Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0", null, null), new HarmonyMethod(AccessTools.Method(typeof(_OnEvent), nameof(OpRaiseEvent))));
                LogHandler.Log("[Patch] Networking Patched!");
            }
            catch
            {
               LogHandler.Log("[Patch] Networking Patching Failed!");
            }
        }

        private static bool OnEvent(EventData __0)
        {
            if (__0 == null) return false;
            return true;
        }

        private static bool OpRaiseEvent(byte __0, Il2CppSystem.Object __1, RaiseEventOptions __2)
        {
            if (__0 == 0 || __1 == null || __2 == null) return false;
            return true;
        }

        private static bool OnRPC(ref VRC.Player __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4)
        {
            if (__0 == null || __1 == null || __3 == 0 || __4 == 0f) return false;
            return true;
        }
    }
}
