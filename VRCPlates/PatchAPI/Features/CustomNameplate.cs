using System;
using TMPro;
using UnityEngine;
using VRC;

namespace VRCPlates
{
    class CustomNameplate : MonoBehaviour
    {
        public static bool PlateToggle = false;

        public VRC.Player player = new Player();
        private Transform PlateObg;
        private TextMeshProUGUI Nameplatext;
        private byte frames, ping;
        private int noUpdateCount = 0;

        public CustomNameplate(IntPtr ptr) : base(ptr)
        {

        }

        void Start()
        {
             PlayerNameplate namePlateManager = player._vrcplayer.field_Public_PlayerNameplate_0;
                Transform Plate = namePlateManager.field_Public_GameObject_5.transform;
                PlateObg = Instantiate(Plate, Plate);
                PlateObg.parent = namePlateManager.transform;
                PlateObg.parent = gameObject.transform.Find("Contents");
                PlateObg.name = "LethalPlates";
                PlateObg.gameObject.SetActive(true);
                PlateObg.localPosition = new Vector3(0f, 62f, 0f);
                PlateObg.localScale = new Vector3(1f, 1f, 2f);
                Nameplatext = PlateObg.Find("Trust Text").GetComponent<TextMeshProUGUI>();
                Nameplatext.color = Color.white;
                Nameplatext.fontStyle = FontStyles.Subscript;
                Nameplatext.isOverlay = true;
                PlateObg.Find("Trust Icon").gameObject.SetActive(false);
                PlateObg.Find("Performance Icon").gameObject.SetActive(false);
                PlateObg.Find("Performance Text").gameObject.SetActive(false);
                PlateObg.Find("Friend Anchor Stats").gameObject.SetActive(false);

            frames = player._playerNet.field_Private_Byte_0;
            ping = player._playerNet.field_Private_Byte_1;
            Nameplatext.text = "";
        }

        void Update()
        {
            if (frames == player._playerNet.field_Private_Byte_0 && ping == player._playerNet.field_Private_Byte_1) noUpdateCount++;
            else
            {
                noUpdateCount = 0;
            }
            frames = player._playerNet.field_Private_Byte_0;
            ping = player._playerNet.field_Private_Byte_1;

            var status = "<color=green>Stable</color>";
            
            if (noUpdateCount > 35)
                status = "<color=yellow>Lagging</color>";
            if (noUpdateCount > 375)
                status = "<color=red>Crashed</color>";

            try
            {
                Nameplatext.text = $"[<color=green>{ player.GetPlatform()}</color>] | [{status}]  |<color=white> FPS:</color> {player.GetFramesColord()} |<color=white> Ping</color>: {player.GetPingColord()}";
            }
            catch { }

        }
    }
}
