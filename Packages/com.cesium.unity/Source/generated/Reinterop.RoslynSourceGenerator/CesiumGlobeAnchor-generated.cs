#if UNITY_EDITOR_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
#if UNITY_EDITOR_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGlobeAnchor
    {
        private partial void SetNewLocalToGlobeFixedMatrix(Unity.Mathematics.double4x4 newLocalToGlobeFixedMatrix)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(Reinterop.ObjectHandleUtility.CreateHandle(this), &newLocalToGlobeFixedMatrix, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void SetNewLocalToGlobeFixedMatrixFromTransform()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial Unity.Mathematics.quaternion GetLocalToEastUpNorthRotation()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.quaternion();
                DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial void SetLocalToEastUpNorthRotation(Unity.Mathematics.quaternion newRotation)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(Reinterop.ObjectHandleUtility.CreateHandle(this), &newRotation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrix(System.IntPtr thiz, Unity.Mathematics.double4x4* newLocalToGlobeFixedMatrix, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetNewLocalToGlobeFixedMatrixFromTransform(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_GetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGlobeAnchor_SetLocalToEastUpNorthRotation(System.IntPtr thiz, Unity.Mathematics.quaternion* newRotation, System.IntPtr* reinteropException);
    }
}
#endif
