#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonDocument : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonDocument managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(this.handle);
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
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject GetRootObject()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRootObject cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        internal partial bool ParseInternal(string geoJsonString)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ParseInternal cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(geoJsonString), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        private static partial void LoadFromUrl(string url, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(Reinterop.ObjectHandleUtility.CreateHandle(url), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private static partial void LoadFromCesiumIon(long ionAssetId, string ionAccessToken, string ionApiUrl, System.Action<CesiumForUnity.CesiumGeoJsonDocument> callback)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(ionAssetId, Reinterop.ObjectHandleUtility.CreateHandle(ionAccessToken), Reinterop.ObjectHandleUtility.CreateHandle(ionApiUrl), Reinterop.ObjectHandleUtility.CreateHandle(callback), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonDocument_GetRootObject(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonDocument_ParseInternal(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonDocument.ImplementationHandle implementation, System.IntPtr geoJsonString, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromUrl(System.IntPtr url, System.IntPtr callback, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonDocument_LoadFromCesiumIon(long ionAssetId, System.IntPtr ionAccessToken, System.IntPtr ionApiUrl, System.IntPtr callback, System.IntPtr* reinteropException);
    }
}
#endif
