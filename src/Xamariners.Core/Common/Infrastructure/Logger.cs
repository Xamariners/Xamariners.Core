using System;
using PCLAppConfig;
using Xamariners.Core.Configuration;
using Xamariners.Core.Interface;

namespace Xamariners.Core.Common
{
    public abstract class Logger : ILogger
    {
        protected string _appName;

        protected string _appConfig;

        public enum LogType
        {
            ERROR,
            WARNING,
            INFO,
            NAVIGATE
        }
        
        public Logger()
        {
            _appName = ConfigurationManager.AppSettings["app.name"];
            _appConfig = ConfigurationManager.AppSettings["app.config"];
        }

        public void LogException(Exception exception, string message)
        {
            LogAction(exception, LogType.ERROR, message);
        }

        public abstract void LogAction(Exception exception, LogType logType = LogType.ERROR, string message = null);

        public void LogException(Exception exception)
        {
            LogAction(exception);
        }

        public void LogInfo(string message)
        {
            var exception = new Exception(message);
            LogAction(exception, LogType.INFO, message);

        }
    }
}
