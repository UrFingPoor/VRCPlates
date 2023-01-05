using System;
using VRC;

namespace VRCPlates
{
    public class VRCPlateNameplates
    {
        public static bool toggled = false;
        public static void InitPlayerNameplates()
        {
            for (int i = 0; i < PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().Length; i++)
            {
                Player player = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray()[i];
                CustomNameplate nameplate = player._vrcplayer.field_Public_PlayerNameplate_0.gameObject.AddComponent<CustomNameplate>();
                nameplate.player = player;              
                if (i >= PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().Length) { break; }
            }
        }

        public static void UpdatePlayerNameplates(Player player)
        {
            if (player is null) throw new ArgumentNullException(nameof(player));

            for (int i = 0; i < PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().Length; i++)
            {
                player = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray()[i];
                CustomNameplate nameplate = player._vrcplayer.field_Public_PlayerNameplate_0.gameObject.AddComponent<CustomNameplate>();
                nameplate.player = player;
                if (i >= PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().Length) { break; }
            }
        }
    }
}

