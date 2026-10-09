#if UNITY_EDITOR_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if UNITY_EDITOR_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTile
    {
        private static partial UnityEngine.Bounds getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4 ecefToLocalMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new UnityEngine.Bounds();
                DotNet_CesiumForUnity_Cesium3DTile_getBounds(pTile, pTileEllipsoid, &ecefToLocalMatrix, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTile_getBounds(System.IntPtr pTile, System.IntPtr pTileEllipsoid, Unity.Mathematics.double4x4* ecefToLocalMatrix, UnityEngine.Bounds* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
