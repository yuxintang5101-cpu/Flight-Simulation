#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
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
    public partial class CesiumGeoJsonObject : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumGeoJsonObject managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(this.handle);
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
        public partial CesiumForUnity.CesiumGeoJsonObjectType GetObjectType()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectType cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial bool IsValid()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so IsValid cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature GetObjectAsFeature()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeature cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonFeature[] GetObjectAsFeatureCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsFeatureCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonFeature[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial Unity.Mathematics.double3 GetObjectAsPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3[] GetObjectAsMultiPoint()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPoint cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (Unity.Mathematics.double3[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString GetObjectAsLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonLineString[] GetObjectAsMultiLineString()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiLineString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonLineString[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon GetObjectAsPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonPolygon[] GetObjectAsMultiPolygon()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsMultiPolygon cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonPolygon[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumGeoJsonObject[] GetObjectAsGeometryCollection()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetObjectAsGeometryCollection cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumGeoJsonObject[])Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial bool HasStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so HasStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial CesiumForUnity.CesiumVectorStyle GetStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new CesiumForUnity.CesiumVectorStyle();
                DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial void SetStyle(CesiumForUnity.CesiumVectorStyle style)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so SetStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &style, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }
        public partial void ClearStyle()
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so ClearStyle cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern CesiumForUnity.CesiumGeoJsonObjectType DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectType(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_IsValid(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeature(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsFeatureCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPoint(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiLineString(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsMultiPolygon(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumGeoJsonObject_GetObjectAsGeometryCollection(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumGeoJsonObject_HasStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_GetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_SetStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, CesiumForUnity.CesiumVectorStyle* style, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumGeoJsonObject_ClearStyle(System.IntPtr thiz, CesiumForUnity.CesiumGeoJsonObject.ImplementationHandle implementation, System.IntPtr* reinteropException);
    }
}
#endif
