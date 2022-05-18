using System;
using System.Diagnostics;
using Xamariners.Core.Common.Interfaces;

namespace Xamariners.Core.Common.Helpers
{
    public static class TraceHelpers
    {
        public static void WriteToTrace(string format, params object[] arg)
        {
#if TRACE
            Debug.WriteLine(format, arg);
#endif
        }

        public static void WriteToTrace(string arg)
        {
#if TRACE
            Debug.WriteLine(arg);
#endif
        }

        public static void WriteToTrace(Exception arg)
        {
#if TRACE
            Debug.WriteLine(arg);
#endif
        }
    }
}