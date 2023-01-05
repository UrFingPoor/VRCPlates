//using AmplitudeSDKWrapper;
//using Area51.Module.Settings.Logging;
//using Area51.SDK;
//using ExitGames.Client.Photon;
//using HarmonyLib;
//using MelonLoader;
//using Photon.Realtime;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading;
//using UnhollowerBaseLib;
//using UnityEngine;
//using VRC.Core;
//using VRC.Networking;
//using VRC.SDKBase;

//namespace Area51
//{
//    class OLD
//    {
//    }
//}
//    //    public new static HarmonyLib.Harmony Harmony { get; private set; }
//    //    private static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("Area 51");
//    //    private static string newHWID = "";
//    //    public static List<int> blacklistedPlayers = new List<int>();

//    //    private static HarmonyMethod GetLocalPatch(Type type, string methodName)
//    //    {
//    //        return new HarmonyMethod(type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
//    //    }



//    //    public static void Init()
//    //    {

//    //        try
//    //        {
//    //            Instance.Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(FakeHWID))));
//    //            SafetyPatch();
//    //            Instance.Patch(typeof(AmplitudeWrapper).GetMethod("PostEvents"), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(VoidPatch))));
//    //            Instance.Patch(typeof(AmplitudeWrapper).GetMethods().First((MethodInfo x) => x.Name == "LogEvent" && x.GetParameters().Length == 4), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(VoidPatch))));
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] Analystics", false, false);
//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] Could not patch Analystics failed\n" + ex, false, false);
//    //        }
//    //        try
//    //        {
//    //            Instance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(OnUdon))));
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] Udon", false, false);
//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] Could not patch Udon failed\n" + ex, false, false);
//    //        }
//    //        try
//    //        {
//    //            Instance.Patch(typeof(LoadBalancingClient).GetMethod("OnEvent"), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(OnEvent))));
//    //            Instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0"), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(OnRPC))));
//    //            Instance.Patch(AccessTools.Method(typeof(LoadBalancingClient), "Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0", null, null), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(OpRaiseEvent))));

//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] Networking", false, false);
//    //        }
//    //        catch
//    //        {
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] [Error] Networking", false, false);
//    //        }

//    //        try
//    //        {
//    //            Instance.Patch(typeof(VRC.Core.AssetManagement).GetMethod("Method_Public_Static_Object_Object_Boolean_Boolean_Boolean_0"), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(OnAvatarAssetBundleLoad))));

//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] AssetBundle", false, false);
//    //        }
//    //        catch
//    //        {
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] [Error] AssetBundle", false, false);
//    //        }

//    //        try
//    //        {
//    //            Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "Log" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(Debug))));
//    //            Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogError" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(DebugError))));
//    //            Instance.Patch(typeof(Debug).GetMethods().First(x => x.Name == "LogWarning" && x.GetParameters().Length == 1), new HarmonyMethod(AccessTools.Method(typeof(OLD), nameof(DebugWarning))));

//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] Logger", false, false);
//    //        }
//    //        catch
//    //        {
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] [Error] Logger", false, false);
//    //        }
//    //        try
//    //        {
            
//    //            Instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod("Awake"), null, new HarmonyMethod(AccessTools.Method(typeof(Main), ("OnUIInit"))));
//    //        }
//    //        catch
//    //        {

//    //        }
//    //        while (NetworkManager.field_Internal_Static_NetworkManager_0 == null)
//    //        {
//    //            Thread.Sleep(25);
//    //        }

//    //        VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_ = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;
//    //        VRCEventDelegate<VRC.Player> field_Internal_VRCEventDelegate_1_Player_2 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;
//    //        field_Internal_VRCEventDelegate_1_Player_.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(OLD.OnPlayerJoin));
//    //        field_Internal_VRCEventDelegate_1_Player_2.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<VRC.Player>(OLD.OnPlayerLeave));

//    //        SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] All Patching Procedures Are Complete, Now Starting Client", false, false);
//    //    }


    
//    //    private static void OnPlayerJoin(VRC.Player player)
//    //    {
//    //        if (player == PlayerWrapper.LocalPlayer) { WorldWrapper.Init(); }
//    //        for (int i = 0; i < Main.Instance.OnPlayerJoinEventArray.Length; i++)
//    //            Main.Instance.OnPlayerJoinEventArray[i].OnPlayerJoin(player);
//    //        if (PlayerWrapper.PlayersActorID.ContainsKey(player.GetActorNumber()))
//    //        {
//    //            PlayerWrapper.PlayersActorID.Remove(player.GetActorNumber());
//    //            PlayerWrapper.PlayersActorID.Add(player.GetActorNumber(), player);
//    //        }
//    //        else
//    //        {
//    //            PlayerWrapper.PlayersActorID.Add(player.GetActorNumber(), player);
//    //        }

//    //    }



//    //    private static void OnPlayerLeave(VRC.Player player)
//    //    {
//    //        if (player == null)
//    //        {
//    //            return;
//    //        }
//    //        for (int i = 0; i < Main.Instance.OnPlayerLeaveEventArray.Length; i++)
//    //            Main.Instance.OnPlayerLeaveEventArray[i].PlayerLeave(player);
//    //        PlayerWrapper.PlayersActorID.Remove(player.GetActorNumber());
//    //    }


//    //    [Obfuscation(Exclude = true)]
//    //    private static bool DebugError(ref Il2CppSystem.Object __0)
//    //    {
//    //        if (UnityLogger.Instance == null)
//    //            return true;

//    //        if (UnityLogger.Instance.toggled)
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[UnityError] " + Il2CppSystem.Convert.ToString(__0), false, false);
//    //        return true;
//    //    }



//    //    [Obfuscation(Exclude = true)]
//    //    private static bool OnAvatarAssetBundleLoad(ref UnityEngine.Object __0)
//    //    {
//    //        GameObject gameObject = __0.TryCast<GameObject>();
//    //        if (gameObject == null)
//    //        {
//    //            return true;
//    //        }
//    //        if (!gameObject.name.ToLower().Contains("avatar"))
//    //        {
//    //            return true;
//    //        }
//    //        string avatarId = gameObject.GetComponent<PipelineManager>().blueprintId;
//    //        for (int i = 0; i < Main.Instance.OnAssetBundleLoadEventArray.Length; i++)
//    //            if (!Main.Instance.OnAssetBundleLoadEventArray[i].OnAvatarAssetBundleLoad(gameObject, avatarId))
//    //                return false;

//    //        return true;
//    //    }

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool DebugWarning(ref Il2CppSystem.Object __0)
//    //    {
//    //        if (UnityLogger.Instance == null)
//    //            return true;

//    //        if (UnityLogger.Instance.toggled)
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Yellow, "[UnityWarning] " + Il2CppSystem.Convert.ToString(__0), false, false);
//    //        return true;
//    //    }

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool Debug(ref Il2CppSystem.Object __0)
//    //    {
//    //        if (UnityLogger.Instance == null)
//    //            return true;

//    //        if (UnityLogger.Instance.toggled)
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Unity] " + Il2CppSystem.Convert.ToString(__0), false, false);
//    //        return true;
//    //    }


//    //    [Obfuscation(Exclude = true)]
//    //    private static bool OnUdon(string __0, VRC.Player __1)
//    //    {
//    //        for (int i = 0; i < Main.Instance.OnUdonEventArray.Length; i++)
//    //            if (!Main.Instance.OnUdonEventArray[i].OnUdon(__0, __1))
//    //                return false;
//    //        return true;
//    //    }

      
//    //    [Obfuscation(Exclude = true)]
//    //    private static bool FakeHWID(ref string __result)
//    //    {
//    //        if (OLD.newHWID == "")
//    //        {
//    //            newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
//    //            {
//    //                new System.Random().Next(0, 9),
//    //                new System.Random().Next(0, 9),
//    //                new System.Random().Next(0, 9),
//    //                new System.Random().Next(0, 9),
//    //                new System.Random().Next(0, 9),
//    //                new System.Random().Next(0, 9),
//    //                new System.Random().Next(0, 9)
//    //            }))).Select(delegate (byte x)
//    //            {
//    //                byte b = x;
//    //                return b.ToString("x2");
//    //            }).Aggregate((string x, string y) => x + y);
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"[Spoofer] HWID New: {OLD.newHWID}", false, false);
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"[Spoofer] PC Name New: {SystemInfo.deviceName}", false, false);
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"[Spoofer] Model New: {SystemInfo.deviceModel}", false, false);
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"[Spoofer] PBU New: {SystemInfo.graphicsDeviceName}", false, false);
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"[Spoofer] CPU New: {SystemInfo.processorType}", false, false);
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"[Spoofer] PBU ID New: {SystemInfo.graphicsDeviceID.ToString()}", false, false);
//    //            SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, $"[Spoofer] OS New:{SystemInfo.operatingSystem}", false, false);
//    //        }
//    //        __result = newHWID;
//    //        return false;
//    //    }


//    //    public unsafe static void SafetyPatch()
//    //    {
//    //        IntPtr intPtr2 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceModel");
//    //        MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr2)), AccessTools.Method(typeof(OLD), "FakeModel", null, null).MethodHandle.GetFunctionPointer());
//    //        IntPtr intPtr3 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceName");
//    //        MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr3)), AccessTools.Method(typeof(OLD), "FakeName", null, null).MethodHandle.GetFunctionPointer());
//    //        IntPtr intPtr4 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceName");
//    //        MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr4)), AccessTools.Method(typeof(OLD), "FakeGBU", null, null).MethodHandle.GetFunctionPointer());
//    //        IntPtr intPtr5 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceID");
//    //        MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr5)), AccessTools.Method(typeof(OLD), "FakeGBUID", null, null).MethodHandle.GetFunctionPointer());
//    //        IntPtr intPtr6 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetProcessorType");
//    //        MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr6)), AccessTools.Method(typeof(OLD), "FakeProcessor", null, null).MethodHandle.GetFunctionPointer());
//    //        IntPtr intPtr7 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetOperatingSystem");
//    //        MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr7)), AccessTools.Method(typeof(OLD), "FakeOS", null, null).MethodHandle.GetFunctionPointer());
//    //    }

//    //    public static IntPtr FakeModel() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(OLD.Motherboards[new System.Random().Next(0, OLD.Motherboards.Length)])).Pointer;
//    //    public static IntPtr FakeName() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp("DESKTOP-" + Misc.RandomString(7))).Pointer;
//    //    public static IntPtr FakeGBU() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(OLD.PBU[new System.Random().Next(0, OLD.PBU.Length)])).Pointer;
//    //    public static IntPtr FakeGBUID() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Misc.RandomString(12))).Pointer;
//    //    public static IntPtr FakeProcessor() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(OLD.CPU[new System.Random().Next(0, OLD.CPU.Length)])).Pointer;
//    //    public static IntPtr FakeOS() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(OLD.OS[new System.Random().Next(0, OLD.OS.Length)])).Pointer;

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool OnEvent(EventData __0)
//    //    {
//    //        if (__0 == null)
//    //            return false;
//    //        if (blacklistedPlayers.Contains(__0.Sender))
//    //            return false;

//    //        for (int i = 0; i < Main.Instance.OnEventEventArray.Length; i++)
//    //            if (!Main.Instance.OnEventEventArray[i].OnEvent(__0))
//    //                return false;
//    //        return true;
//    //    }

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool OpRaiseEvent(byte __0, Il2CppSystem.Object __1, RaiseEventOptions __2)
//    //    {
//    //        for (int i = 0; i < Main.Instance.OnSendOPEventArray.Length; i++)
//    //            if (!Main.Instance.OnSendOPEventArray[i].OnSendOP(__0, ref __1, ref __2))
//    //                return false;

//    //        return true;
//    //    }

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool OnRPC(VRC.Player __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4)
//    //    {
//    //        for (int i = 0; i < Main.Instance.OnRPCEventArray.Length; i++)
//    //            if (!Main.Instance.OnRPCEventArray[i].OnRPC(__0, __1, __2, __3, __4))
//    //                return false;

//    //        return true;
//    //    }

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool VoidPatch()
//    //    {
//    //        return false;
//    //    }

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool VoidPatchTrue(bool __result)
//    //    {
//    //        __result = true;
//    //        return false;
//    //    }

//    //    [Obfuscation(Exclude = true)]
//    //    private static bool VoidPatchFalse(bool __result)
//    //    {
//    //        __result = false;
//    //        return false;
//    //    }

//    //    private static string[] PBU = new string[]
//    //    {
//    //        "MSI Radeon RX 6900 XT GAMING Z TRIO 16GB",
//    //        "Gigabyte Radeon RX 6700 XT Gaming OC 12GB",
//    //        "ASUS DUAL GeForce RTX 2060 6GB OC EVO",
//    //        "Palit GeForce GTX 1050 Ti StormX 4GB",
//    //        "MSI GeForce GTX 1650 D6 Ventus XS OCV2 12GB OC",
//    //        "ASUS GOLD20TH GTX 980 Ti Platinum",
//    //        "ASUS TUF GeForce RTX 3060 12GB OC Gaming",
//    //        "NVIDIA Quadro RTX 4000 8GB",
//    //        "NVIDIA GeForce GTX 1080 Ti",
//    //        "NVIDIA GeForce GTX 1080",
//    //        "NVIDIA GeForce GTX 1070",
//    //        "NVIDIA GeForce GTX 1060 6GB",
//    //        "NVIDIA GeForce GTX 980 Ti"
//    //    };
//    //    private static string[] CPU = new string[]
//    //    {
//    //        "AMD Ryzen 5 3600",
//    //        "AMD Ryzen 7 3700X",
//    //        "AMD Ryzen 7 5800X",
//    //        "AMD Ryzen 9 5900X",
//    //        "AMD Ryzen 9 3900X 12-Core Processor",
//    //        "INTEL CORE I9-10900K",
//    //        "INTEL CORE I7-10700K",
//    //        "INTEL CORE I9-9900K",
//    //        "Intel Core i7-8700K"
//    //    };
//    //    private static string[] Motherboards = new string[]
//    //    {
//    //        "B550 AORUS PRO (Gigabyte Technology Co., Ltd.)",
//    //        "Gigabyte B450M DS3H",
//    //        "Asus AM4 TUF Gaming X570-Plus",
//    //        "ASRock Z370 Taichi"
//    //    };
//    //    private static string[] OS = new string[]
//    //    {
//    //        "Windows 10  (10.0.0) 64bit",
//    //        "Windows 10  (10.0.0) 32bit",
//    //        "Windows 8  (10.0.0) 64bit",
//    //        "Windows 8  (10.0.0) 32bit",
//    //        "Windows 7  (10.0.0) 64bit",
//    //        "Windows 7  (10.0.0) 32bit",
//    //        "Windows Vista 64bit",
//    //        "Windows Vista 32bit",
//    //        "Windows XP 64bit",
//    //        "Windows XP 32bit"
//    //    };

     

//    //    //private static bool VRCNetworkingClientOnPhotonEvent(EventData eventData)
//    //    //{
//    //    //    for (int i = 0; i < Main.Instance.onVrcEventArray.Length; i++)
//    //    //        if (!Main.Instance.onVrcEventArray[i].VRCNetworkingClientOnPhotonEvent(eventData))
//    //    //            return false;
//    //    //    return true;
//    //    //}

//    //    //private static bool OnEventPatch(LoadBalancingClient loadBalancingClient, EventData eventData)
//    //    //{
//    //    //    for (int i = 0; i < Main.Instance.onPhotonEventArray.Length; i++)
//    //    //        if (!Main.Instance.onPhotonEventArray[i].OnEventPatch(loadBalancingClient, eventData))
//    //    //            return false;
//    //    //    return true;
//    //    //}
//    //}
