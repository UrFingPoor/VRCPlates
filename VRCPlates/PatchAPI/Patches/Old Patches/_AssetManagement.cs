using SpacewareCheats.SDK.Patching;
using Harmony; 
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC.Core;

namespace SpacewareCheats.SDK.PatchAPI.Patches
{
    public static class _AssetManagement  //fish copy 
    {
        public readonly struct AvatarManagerCookie : IDisposable
        {
            public static VRCAvatarManager CurrentManager;
            public readonly VRCAvatarManager myLastManager;

            public AvatarManagerCookie(VRCAvatarManager avatarManager)
            {
                myLastManager = CurrentManager;
                CurrentManager = avatarManager;
            }

            public void Dispose()
            {
                CurrentManager = myLastManager;
            }
        }
        public static readonly List<object> antiGCList = new List<object>();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void VoidDelegate(IntPtr thisPtr, IntPtr nativeMethodInfo);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr ObjectInstantiateDelegate(IntPtr assetPtr, Vector3 pos, Quaternion rot, byte allowCustomShaders, byte isUI, byte validate, IntPtr nativeMethodPointer);
        public static void InitObjectInstantiatedPatch()
        {
            try
            {
                List<MethodInfo> onObjectInstantiatedMethods = typeof(AssetManagement).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Where(it => it.Name.StartsWith("Method_Public_Static_Object_Object_Vector3_Quaternion_Boolean_Boolean_Boolean_0") && it.GetParameters().Length == 6).ToList();
                foreach (MethodInfo onObjectInstantiatedMethod in onObjectInstantiatedMethods)
                {
                    unsafe
                    {
                        IntPtr originalMethodPointer = *(IntPtr*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(onObjectInstantiatedMethod).GetValue(null);

                        ObjectInstantiateDelegate originalInstantiateDelegate = null;

                        ObjectInstantiateDelegate replacement = (assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer) =>
                            OnObjectInstantiatedPatch(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer, originalInstantiateDelegate);

                        antiGCList.Add(replacement);

                        MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPointer), Marshal.GetFunctionPointerForDelegate(replacement));

                        originalInstantiateDelegate = Marshal.GetDelegateForFunctionPointer<ObjectInstantiateDelegate>(originalMethodPointer);
                    }
                }
                foreach (Type nestedType in typeof(VRCAvatarManager).GetNestedTypes())
                {
                    MethodInfo moveNext = nestedType.GetMethod("MoveNext");

                    if (moveNext == null)
                    {
                        continue;
                    }

                    PropertyInfo avatarManagerField = nestedType.GetProperties().SingleOrDefault(it => it.PropertyType == typeof(VRCAvatarManager));

                    if (avatarManagerField == null)
                    {
                        continue;
                    }

                    int fieldOffset = (int)IL2CPP.il2cpp_field_get_offset((IntPtr)UnhollowerUtils.GetIl2CppFieldInfoPointerFieldForGeneratedFieldAccessor(avatarManagerField.GetMethod).GetValue(null));

                    unsafe
                    {
                        IntPtr originalMethodPointer = *(IntPtr*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(moveNext).GetValue(null);

                        originalMethodPointer = XrefScannerLowLevel.JumpTargets(originalMethodPointer).First();

                        VoidDelegate originalDelegate = null;

                        void TaskMoveNextPatch(IntPtr taskPtr, IntPtr nativeMethodInfo)
                        {
                            IntPtr avatarManager = *(IntPtr*)(taskPtr + fieldOffset - 16);

                            using (new AvatarManagerCookie(new VRCAvatarManager(avatarManager)))
                            {
                                originalDelegate(taskPtr, nativeMethodInfo);
                            }
                        }

                        VoidDelegate patchDelegate = new VoidDelegate(TaskMoveNextPatch);
                        antiGCList.Add(patchDelegate);

                        MelonUtils.NativeHookAttach((IntPtr)(&originalMethodPointer), Marshal.GetFunctionPointerForDelegate(patchDelegate));
                        originalDelegate = Marshal.GetDelegateForFunctionPointer<VoidDelegate>(originalMethodPointer);
                    }
                }
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Green, "[Patch] AssetManagement");
            }
            catch
            {
                SDK.LogHandler.Log(SDK.LogHandler.Colors.Red, "[Patch] [Error] AssetManagement");
            }
        }

        public static IntPtr OnObjectInstantiatedPatch(IntPtr assetPtr, Vector3 pos, Quaternion rot, byte allowCustomShaders, byte isUI, byte validate, IntPtr nativeMethodPointer, ObjectInstantiateDelegate originalInstantiateDelegate)
        {
            if (assetPtr == IntPtr.Zero)
            {
                return originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);
            }
            GameObject potentialAvatar = new UnityEngine.Object(assetPtr).TryCast<GameObject>();
            if (potentialAvatar == null)
            {
                return originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);
            }
            if (potentialAvatar.name.StartsWith("UserUi") == true ||
               potentialAvatar.name.StartsWith("WorldUi") == true ||
               potentialAvatar.name.StartsWith("AvatarUi") == true ||
               potentialAvatar.name.StartsWith("Holoport") == true)
            {
                return originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);
            }
            bool containsPrefabName = potentialAvatar.name.StartsWith("prefab");
            IntPtr result = originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);
            for (int i = 0; i < Main.Instance.OnObjectInstantiatedArray.Length; i++)
                if (Main.Instance.OnObjectInstantiatedArray[i].OnObjectInstantiatedPatch(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer, originalInstantiateDelegate))
                    return result;
            return result;
        }
    }
}
