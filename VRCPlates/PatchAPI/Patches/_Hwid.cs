using System;
using System.Linq;
using System.Reflection;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;

namespace VRCPlates.SDK
{
    class _Hwid //knah HWID Patch https://github.com/knah/ML-UniversalMods/tree/main/HWIDPatch
    {
        private static Il2CppSystem.Object ourGeneratedHwidString;

        public static unsafe void InitHwidPatch()
        {
            try
            {
                var category = MelonPreferences.CreateCategory("LethalHWID", "HWID Patch");
                var hwidEntry = category.CreateEntry("HWID", "", is_hidden: true);

                var newId = hwidEntry.Value;
                if (newId.Length != SystemInfo.deviceUniqueIdentifier.Length)
                {
                    var random = new System.Random(Environment.TickCount);
                    var bytes = new byte[SystemInfo.deviceUniqueIdentifier.Length / 2];
                    random.NextBytes(bytes);
                    newId = string.Join("", bytes.Select(it => it.ToString("x2")));
                    MelonLogger.Msg("Generated and saved a new HWID");
                    hwidEntry.Value = newId;
                    category.SaveToFile(false);
                }

                ourGeneratedHwidString = new Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(newId));

                var icallName = "UnityEngine.SystemInfo::GetDeviceUniqueIdentifier";
                var icallAddress = IL2CPP.il2cpp_resolve_icall(icallName);
                if (icallAddress == IntPtr.Zero)
                {
                    MelonLogger.Error("Can't resolve the icall, not patching");
                    return;
                }

                MelonUtils.NativeHookAttach((IntPtr)(&icallAddress), typeof(_Hwid).GetMethod(nameof(GetDeviceIdPatch), BindingFlags.Static | BindingFlags.NonPublic)!.MethodHandle.GetFunctionPointer());
                MelonLogger.Msg("[Patch] HWID Patched!; below Id's should match:");
                MelonLogger.Msg($"[Patch] Current: {SystemInfo.deviceUniqueIdentifier}");
                MelonLogger.Msg($"[Patch] Target:  {newId}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
            }
        }
        private static IntPtr GetDeviceIdPatch() => ourGeneratedHwidString.Pointer;
    }
}
