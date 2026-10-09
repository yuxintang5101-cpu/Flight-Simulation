#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class Cesium3DTileset : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(Cesium3DTileset managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(this.handle);
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
        public Cesium3DTileset()
        {
            CreateImplementation();
        }
        public partial float ComputeLoadProgress()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ComputeLoadProgress cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial void RecreateTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so RecreateTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void FocusTileset()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so FocusTileset cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult> SampleHeightMostDetailed(params Unity.Mathematics.double3[] longitudeLatitudeHeightPositions)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SampleHeightMostDetailed cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, Reinterop.ObjectHandleUtility.CreateHandle(longitudeLatitudeHeightPositions), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (System.Threading.Tasks.Task<CesiumForUnity.CesiumSampleHeightResult>)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        private partial void SetShowCreditsOnScreen(bool value)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetShowCreditsOnScreen cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, value ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Start()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Start cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Start(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void Update()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so Update cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_Update(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnValidate()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnValidate cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnEnable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnEnable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        private partial void OnDisable()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so OnDisable cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        internal partial void UpdateOverlayMaterialKeys()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so UpdateOverlayMaterialKeys cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_Cesium3DTileset_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_Cesium3DTileset_ComputeLoadProgress(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_RecreateTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_FocusTileset(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_Cesium3DTileset_SampleHeightMostDetailed(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr longitudeLatitudeHeightPositions, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_SetShowCreditsOnScreen(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, byte value, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Start(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_Update(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnValidate(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnEnable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_OnDisable(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_Cesium3DTileset_UpdateOverlayMaterialKeys(System.IntPtr thiz, CesiumForUnity.Cesium3DTileset.ImplementationHandle implementation, System.IntPtr* reinteropException);
    }
}
#endif
