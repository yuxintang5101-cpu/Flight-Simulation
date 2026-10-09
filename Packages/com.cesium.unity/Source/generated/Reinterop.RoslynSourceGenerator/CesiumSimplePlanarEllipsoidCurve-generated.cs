#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
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
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
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
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
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
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
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
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
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
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
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
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
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
    public partial class CesiumSimplePlanarEllipsoidCurve : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumSimplePlanarEllipsoidCurve managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(this.handle);
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
        public partial Unity.Mathematics.double3 GetPosition(double percentage, double additionalHeight)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPosition cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, percentage, additionalHeight, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        private partial bool CreateFromCenteredFixed(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceEcef, Unity.Mathematics.double3 destinationEcef)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromCenteredFixed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceEcef, &destinationEcef, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private partial bool CreateFromLongitudeLatitudeHeight(CesiumForUnity.CesiumEllipsoid ellipsoid, Unity.Mathematics.double3 sourceLlh, Unity.Mathematics.double3 destinationLlh)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so CreateFromLongitudeLatitudeHeight cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(ellipsoid), &sourceLlh, &destinationLlh, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_GetPosition(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, double percentage, double additionalHeight, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromCenteredFixed(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceEcef, Unity.Mathematics.double3* destinationEcef, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumSimplePlanarEllipsoidCurve_CreateFromLongitudeLatitudeHeight(System.IntPtr thiz, CesiumForUnity.CesiumSimplePlanarEllipsoidCurve.ImplementationHandle implementation, System.IntPtr ellipsoid, Unity.Mathematics.double3* sourceLlh, Unity.Mathematics.double3* destinationLlh, System.IntPtr* reinteropException);
    }
}
#endif
