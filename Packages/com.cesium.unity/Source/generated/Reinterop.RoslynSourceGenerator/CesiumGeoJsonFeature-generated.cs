#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonFeature : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonFeature managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonFeatureIdType GetIdType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetIdAsString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial long GetIdAsInteger()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetIdAsInteger cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial string GetPropertiesAsJson()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetPropertiesAsJson cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial string GetStringProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStringProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial double GetNumericProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetNumericProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool HasProperty(string propertyName)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasProperty cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(propertyName), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetGeometry()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetGeometry cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonFeature_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonFeatureIdType DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetIdAsInteger(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetPropertiesAsJson(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetStringProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetNumericProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonFeature_HasProperty(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr propertyName, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonFeature_GetGeometry(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonFeature.ImplementationHandle implementation, System.IntPtr* reinteropException);
    }
}
#endif
