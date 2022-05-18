using System;
using Xamariners.Core.Common;

namespace Xamariners.Core.Interface
{
    public interface ILogger
    {
        void LogException(Exception exception);

        void LogInfo(string message);

        void LogException(Exception exception, string message);

        void LogAction(Exception exception, Logger.LogType logType = Logger.LogType.ERROR, string message = null);


    }
}