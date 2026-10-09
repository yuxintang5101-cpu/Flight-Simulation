#if UNITY_EDITOR_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if UNITY_EDITOR_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumWgs84Ellipsoid
    {
        public static partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(&returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public static partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(&longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public static partial Unity.Mathematics.double3 EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 earthCenteredEarthFixed)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(&earthCenteredEarthFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GetRadii(Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_ScaleToGeodeticSurface(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_GeodeticSurfaceNormal(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_LongitudeLatitudeHeightToEarthCenteredEarthFixed(Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumWgs84Ellipsoid_EarthCenteredEarthFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3* earthCenteredEarthFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
