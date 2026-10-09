#if UNITY_EDITOR_OSX
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
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
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
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
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
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
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
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
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
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
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
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
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
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
    public partial class CesiumPropertyTableProperty : System.IDisposable
    {
        internal class ImplementationHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public ImplementationHandle(CesiumPropertyTableProperty managed) : base(true)
            {
                SetHandle(DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(Reinterop.ObjectHandleUtility.CreateHandle(managed)));
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(this.handle);
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
        public partial bool GetBoolean(long featureID, bool defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetBoolean cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial sbyte GetSByte(long featureID, sbyte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetSByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial byte GetByte(long featureID, byte defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetByte cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial short GetInt16(long featureID, short defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ushort GetUInt16(long featureID, ushort defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt16 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial int GetInt32(long featureID, int defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial uint GetUInt32(long featureID, uint defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt32 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial long GetInt64(long featureID, long defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial ulong GetUInt64(long featureID, ulong defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt64 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial float GetFloat(long featureID, float defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial double GetDouble(long featureID, double defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        public partial Unity.Mathematics.int2 GetInt2(long featureID, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2 GetUInt2(long featureID, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2 GetFloat2(long featureID, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2 GetDouble2(long featureID, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3 GetInt3(long featureID, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3 GetUInt3(long featureID, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3 GetFloat3(long featureID, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3 GetDouble3(long featureID, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4 GetInt4(long featureID, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4 GetUInt4(long featureID, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4 GetFloat4(long featureID, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4 GetDouble4(long featureID, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int2x2 GetInt2x2(long featureID, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint2x2 GetUInt2x2(long featureID, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float2x2 GetFloat2x2(long featureID, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double2x2 GetDouble2x2(long featureID, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble2x2 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int3x3 GetInt3x3(long featureID, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint3x3 GetUInt3x3(long featureID, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float3x3 GetFloat3x3(long featureID, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double3x3 GetDouble3x3(long featureID, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble3x3 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.int4x4 GetInt4x4(long featureID, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.uint4x4 GetUInt4x4(long featureID, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetUInt4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.float4x4 GetFloat4x4(long featureID, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetFloat4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial Unity.Mathematics.double4x4 GetDouble4x4(long featureID, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetDouble4x4 cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        public partial string GetString(long featureID, string defaultValue)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetString cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumPropertyArray GetArray(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetArray cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumPropertyArray)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }
        public partial CesiumForUnity.CesiumMetadataValue GetRawValue(long featureID)
        {
            unsafe
            {
                if (this._implementation == null || this._implementation.IsInvalid)
                    throw new NotImplementedException("The native implementation is missing so GetRawValue cannot be invoked. This may be caused by a missing call to CreateImplementation in one of your constructors, or it may be that the entire native implementation shared library is missing or out of date.");
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(Reinterop.ObjectHandleUtility.CreateHandle(this), _implementation, featureID, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (CesiumForUnity.CesiumMetadataValue)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_CreateImplementation(System.IntPtr thiz);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_DestroyImplementation(System.IntPtr implementation);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetBoolean(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetSByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetByte(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt16(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt32(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt64(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble2x2(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble3x3(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetUInt4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetFloat4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetDouble4x4(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetString(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetArray(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumPropertyTableProperty_GetRawValue(System.IntPtr thiz, CesiumForUnity.CesiumPropertyTableProperty.ImplementationHandle implementation, long featureID, System.IntPtr* reinteropException);
    }
}
#endif
