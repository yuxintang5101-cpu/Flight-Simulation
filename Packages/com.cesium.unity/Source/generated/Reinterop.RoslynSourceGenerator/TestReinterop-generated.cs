#if UNITY_EDITOR_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("__Internal", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
#if UNITY_EDITOR_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
using System;
using System.Runtime.InteropServices;

namespace CesiumForUnity
{
    internal partial class TestReinterop
    {
        public partial bool CallThrowAnExceptionFromCppAndCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool CallThrowAnExceptionFromCppAndDontCatchIt()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowCppStdException()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }
        public partial bool ThrowOtherCppExceptionType()
        {
            unsafe
            {
                Reinterop.ReinteropInitializer.Initialize();
                System.IntPtr reinteropException = System.IntPtr.Zero;
                var result = DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(Reinterop.ObjectHandleUtility.CreateHandle(this), &reinteropException);
                if (reinteropException != IntPtr.Zero) throw (System.Exception)Reinterop.ObjectHandleUtility.GetObjectAndFreeHandle(reinteropException);
                return result != 0;
            }
        }

        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_CallThrowAnExceptionFromCppAndDontCatchIt(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowCppStdException(System.IntPtr thiz, System.IntPtr* reinteropException);
        [DllImport("CesiumForUnityNative", CallingConvention=CallingConvention.Cdecl)]
        private static unsafe extern byte DotNet_CesiumForUnity_TestReinterop_ThrowOtherCppExceptionType(System.IntPtr thiz, System.IntPtr* reinteropException);
    }
}
#endif
