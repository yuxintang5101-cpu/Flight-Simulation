#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if UNITY_EDITOR_WIN
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumEllipsoid : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumEllipsoid managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(this.handle);
                return true;
            }
        }

        [System.NonSerialized]
        private ImplementationHandle _implementation = null;

        internal ImplementationHandle NativeImplementation
        {
            get { return _implementation; }
        }

        private void CreateImplementation()
        {
            Reinterop.ReinteropInitializer.Initialize();
            System.Diagnostics.Debug.Assert(this._implementation == null, "Implementation is already created. Be sure to call CreateImplementation only once.");
            this._implementation = new ImplementationHandle(this);
        }
        protected void DisposeImplementation()
        {
            if (this._implementation != null && !this._implementation.IsInvalid)
                this._implementation.Dispose();
            this._implementation = null;
        }
        public void Dispose()
        {
            
            this.DisposeImplementation();
        }
        public CesiumEllipsoid()
        {
            CreateImplementation();
        }
        public partial Unity.Mathematics.double3 GetRadii()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetRadii(Unity.Mathematics.double3 newRadii)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetRadii cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &newRadii, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial Unity.Mathematics.double3? ScaleToGeodeticSurface(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ScaleToGeodeticSurface cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                var result = DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result == 1 ? returnValue : null;
            }
        }
        public partial Unity.Mathematics.double3 GeodeticSurfaceNormal(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GeodeticSurfaceNormal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 LongitudeLatitudeHeightToCenteredFixed(Unity.Mathematics.double3 longitudeLatitudeHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so LongitudeLatitudeHeightToCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &longitudeLatitudeHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 CenteredFixedToLongitudeLatitudeHeight(Unity.Mathematics.double3 ellipsoidCenteredEllipsoidFixed)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CenteredFixedToLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &ellipsoidCenteredEllipsoidFixed, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumEllipsoid_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumEllipsoid_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_SetRadii(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* newRadii, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumEllipsoid_ScaleToGeodeticSurface(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_GeodeticSurfaceNormal(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_LongitudeLatitudeHeightToCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* longitudeLatitudeHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumEllipsoid_CenteredFixedToLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumEllipsoid.ImplementationHandle implementation, Unity.Mathematics.double3* ellipsoidCenteredEllipsoidFixed, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
    }
}
#endif
