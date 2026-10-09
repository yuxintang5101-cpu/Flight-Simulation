#if UNITY_EDITOR_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
#if UNITY_EDITOR_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    public partial class CesiumMetadataValue
    {
        internal static partial bool ConvertToBoolean(CesiumForUnity.CesiumMetadataValue value, bool defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue ? (byte)1 : (byte)0, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        internal static partial sbyte ConvertToSByte(CesiumForUnity.CesiumMetadataValue value, sbyte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial byte ConvertToByte(CesiumForUnity.CesiumMetadataValue value, byte defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial short ConvertToInt16(CesiumForUnity.CesiumMetadataValue value, short defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ushort ConvertToUInt16(CesiumForUnity.CesiumMetadataValue value, ushort defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial int ConvertToInt32(CesiumForUnity.CesiumMetadataValue value, int defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial uint ConvertToUInt32(CesiumForUnity.CesiumMetadataValue value, uint defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial long ConvertToInt64(CesiumForUnity.CesiumMetadataValue value, long defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial ulong ConvertToUInt64(CesiumForUnity.CesiumMetadataValue value, ulong defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial float ConvertToFloat(CesiumForUnity.CesiumMetadataValue value, float defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial double ConvertToDouble(CesiumForUnity.CesiumMetadataValue value, double defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(Reinterop.ObjectHandleUtility.CreateHandle(value), defaultValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result;
            }
        }
        internal static partial Unity.Mathematics.int2 ConvertToInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2 ConvertToUInt2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2 ConvertToFloat2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2 ConvertToDouble2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3 ConvertToInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3 ConvertToUInt3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3 ConvertToFloat3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3 ConvertToDouble3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4 ConvertToInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4 ConvertToUInt4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4 ConvertToFloat4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4 ConvertToDouble4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int2x2 ConvertToInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint2x2 ConvertToUInt2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float2x2 ConvertToFloat2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double2x2 ConvertToDouble2x2(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double2x2 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double2x2();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int3x3 ConvertToInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint3x3 ConvertToUInt3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float3x3 ConvertToFloat3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double3x3 ConvertToDouble3x3(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double3x3 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double3x3();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.int4x4 ConvertToInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.int4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.int4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.uint4x4 ConvertToUInt4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.uint4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.uint4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.float4x4 ConvertToFloat4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.float4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.float4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial Unity.Mathematics.double4x4 ConvertToDouble4x4(CesiumForUnity.CesiumMetadataValue value, Unity.Mathematics.double4x4 defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var returnValue = new Unity.Mathematics.double4x4();
                DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(Reinterop.ObjectHandleUtility.CreateHandle(value), &defaultValue, &returnValue, &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return returnValue;
            }
        }
        internal static partial string ConvertToString(CesiumForUnity.CesiumMetadataValue value, string defaultValue)
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(Reinterop.ObjectHandleUtility.CreateHandle(value), Reinterop.ObjectHandleUtility.CreateHandle(defaultValue), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return (string)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(result)!;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToBoolean(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern sbyte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToSByte(System.IntPtr value, sbyte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToByte(System.IntPtr value, byte defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern short DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt16(System.IntPtr value, short defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ushort DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt16(System.IntPtr value, ushort defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern int DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt32(System.IntPtr value, int defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern uint DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt32(System.IntPtr value, uint defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern long DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt64(System.IntPtr value, long defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern ulong DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt64(System.IntPtr value, ulong defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern float DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat(System.IntPtr value, float defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern double DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble(System.IntPtr value, double defaultValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2(System.IntPtr value, Unity.Mathematics.int2* defaultValue, Unity.Mathematics.int2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2(System.IntPtr value, Unity.Mathematics.uint2* defaultValue, Unity.Mathematics.uint2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2(System.IntPtr value, Unity.Mathematics.float2* defaultValue, Unity.Mathematics.float2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2(System.IntPtr value, Unity.Mathematics.double2* defaultValue, Unity.Mathematics.double2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3(System.IntPtr value, Unity.Mathematics.int3* defaultValue, Unity.Mathematics.int3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3(System.IntPtr value, Unity.Mathematics.uint3* defaultValue, Unity.Mathematics.uint3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3(System.IntPtr value, Unity.Mathematics.float3* defaultValue, Unity.Mathematics.float3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3(System.IntPtr value, Unity.Mathematics.double3* defaultValue, Unity.Mathematics.double3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4(System.IntPtr value, Unity.Mathematics.int4* defaultValue, Unity.Mathematics.int4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4(System.IntPtr value, Unity.Mathematics.uint4* defaultValue, Unity.Mathematics.uint4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4(System.IntPtr value, Unity.Mathematics.float4* defaultValue, Unity.Mathematics.float4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4(System.IntPtr value, Unity.Mathematics.double4* defaultValue, Unity.Mathematics.double4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt2x2(System.IntPtr value, Unity.Mathematics.int2x2* defaultValue, Unity.Mathematics.int2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt2x2(System.IntPtr value, Unity.Mathematics.uint2x2* defaultValue, Unity.Mathematics.uint2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat2x2(System.IntPtr value, Unity.Mathematics.float2x2* defaultValue, Unity.Mathematics.float2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble2x2(System.IntPtr value, Unity.Mathematics.double2x2* defaultValue, Unity.Mathematics.double2x2* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt3x3(System.IntPtr value, Unity.Mathematics.int3x3* defaultValue, Unity.Mathematics.int3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt3x3(System.IntPtr value, Unity.Mathematics.uint3x3* defaultValue, Unity.Mathematics.uint3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat3x3(System.IntPtr value, Unity.Mathematics.float3x3* defaultValue, Unity.Mathematics.float3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble3x3(System.IntPtr value, Unity.Mathematics.double3x3* defaultValue, Unity.Mathematics.double3x3* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToInt4x4(System.IntPtr value, Unity.Mathematics.int4x4* defaultValue, Unity.Mathematics.int4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToUInt4x4(System.IntPtr value, Unity.Mathematics.uint4x4* defaultValue, Unity.Mathematics.uint4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToFloat4x4(System.IntPtr value, Unity.Mathematics.float4x4* defaultValue, Unity.Mathematics.float4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern void DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToDouble4x4(System.IntPtr value, Unity.Mathematics.double4x4* defaultValue, Unity.Mathematics.double4x4* pReturnValue, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern System.IntPtr DotNet_CesiumForUnity_CesiumMetadataValue_ConvertToString(System.IntPtr value, System.IntPtr defaultValue, System.IntPtr* reinteropException);
    }
}
#endif
