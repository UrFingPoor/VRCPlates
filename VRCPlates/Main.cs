using System;
using System.Threading.Tasks;
using MelonLoader;
using UnhollowerRuntimeLib;
using VRCPlates.Patches;

[assembly: MelonInfo(typeof(VRCPlates.Main), "VRCPlates", "1.0.0", "Joshua")]
[assembly: MelonColor(ConsoleColor.Magenta)]
[assembly: MelonGame("VRChat", "VRChat")]

namespace VRCPlates
{
     class Main : MelonMod
     {

        public override void OnInitializeMelon()
        {
            ClassInjector.RegisterTypeInIl2Cpp<CustomNameplate>();
            Task.Run(() => { VRCPlatesPatches.InitPatches(); });
        }
     }
}
