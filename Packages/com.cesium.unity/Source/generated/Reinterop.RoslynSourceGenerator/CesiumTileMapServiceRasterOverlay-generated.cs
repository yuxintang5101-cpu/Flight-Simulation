#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
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
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
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
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
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
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
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
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
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
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
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
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
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
    public partial class CesiumTileMapServiceRasterOverlay : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumTileMapServiceRasterOverlay managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(this.handle);
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
        public CesiumTileMapServiceRasterOverlay()
        {
            CreateImplementation();
        }
        protected override partial void AddToTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so AddToTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        protected override partial void RemoveFromTileset(CesiumForUnity.Cesium3DTileset tileset)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RemoveFromTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(tileset), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_AddToTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumTileMapServiceRasterOverlay_RemoveFromTileset(System.IntPtr thiz, CesiumForUnity.CesiumTileMapServiceRasterOverlay.ImplementationHandle implementation, System.IntPtr tileset, System.IntPtr* reinteropException);
    }
}
#endif
