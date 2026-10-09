#if UNITY_EDITOR_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
#if UNITY_EDITOR_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileExcluder
    {
        internal partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_AddToTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileExcluder_RemoveFromTileset(System.IntPtr thiz, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
