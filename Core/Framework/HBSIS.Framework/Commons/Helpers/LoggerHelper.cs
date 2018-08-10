using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Logging;
using HBSIS.Framework.Commons.Result;
using System;

namespace HBSIS.Framework.Commons.Helper
{
    public static class LoggerHelper
    {
        private static readonly object _lock = new object();
        private static ILogger _logger = null;

        public static ILogger Logger
        {
            get
            {
                lock (_lock)
                {
                    if (_logger == null)
                    {
                        var name = Configuration.Actual.GetAppName();
                        var config = Configuration.Actual.GetLog4NetConfigPath();

                        _logger = new Log4NetLogger(name, config);
                    }

                    return _logger;
                }
            }
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warning(string message)
        {
            Logger.Warn(message);
        }

        public static void Error(Exception ex)
        {
            if (ex == null) return;

            ex = ex.GetSqlException() ?? ex;
            Logger.Error(ex.Message, ex);
        }

        public static void Error(string message, Exception ex = null)
        {
            Logger.Error(message, ex);
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Log(LoggingType type, string message)
        {
            switch (type)
            {
                case LoggingType.Error:
                    Error(message);
                    break;

                case LoggingType.Warning:
                    Warning(message);
                    break;

                case LoggingType.Info:
                    Info(message);
                    break;

                case LoggingType.Debug:
                    Debug(message);
                    break;

                default:
                    break;
            }
        }

        public static void Log(Result.Result result)
        {
            switch (result.Status)
            {
                case ResultStatus.Warning:
                    Warning(result.MessageToString());
                    break;

                case ResultStatus.Error:
                    Error(result.MessageToString());
                    break;

                default:
                    Debug("Sucesso");
                    break;
            }
        }
    }
}