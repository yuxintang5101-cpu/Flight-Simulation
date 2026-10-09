#if UNITY_EDITOR_OSX
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
#if !UNITY_EDITOR && UNITY_IOS
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
#if UNITY_EDITOR_WIN
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
#if !UNITY_EDITOR && UNITY_WSA
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
namespace Reinterop
{
    [Reinterop]
    internal class ReinteropException : System.Exception
    {
        public ReinteropException(string message) : base(message) {}

        internal static void ExposeToCPP()
        {
            ReinteropException e = new ReinteropException("message");
            string s = e.Message;
        }
    }
}
#endif
