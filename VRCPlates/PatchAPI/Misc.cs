using UnityEngine;
using VRC;

namespace VRCPlates
{
    static class Misc
    {
        public static float GetFrames(this Player player) => (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : -1f;
        public static short GetPing(this Player player) => player._playerNet.field_Private_Int16_0;

        public static string GetFramesColord(this Player player)
        {
            float fps = player.GetFrames();
            if (fps > 80)
                return "<color=green>" + fps + "</color>";
            else if (fps > 30)
                return "<color=yellow>" + fps + "</color>";
            else
                return "<color=red>" + fps + "</color>";
        }

        public static string GetPingColord(this Player player)
        {
            short ping = player.GetPing();
            if (ping > 150)
                return "<color=red>" + ping + "</color>";
            else if (ping > 75)
                return "<color=yellow>" + ping + "</color>";
            else
                return "<color=green>" + ping + "</color>";
        }

        public static string GetPlatform(this Player player)
        {
            if (player.prop_APIUser_0.IsOnMobile)
            {
                return "<color=green>Q</color>";
            }
            else if (player.prop_VRCPlayerApi_0.IsUserInVR())
            {
                return "<color=#CE00D5>VR</color>";
            }
            else
            {
                return "<color=grey>PC</color>";
            }
        }
    }
}
