using log4net;
using log4net.Repository;
using System;
using System.IO;
using System.Xml;

namespace HBSIS.Framework.Commons.Logging
{
    public class Log4NetLogger : ILogger
    {
        private ILog _logger = null;
        private ILoggerRepository _loggerRepository = null;

        public Log4NetLogger(string name, string log4netConfigPath)
        {
            Name = name;
            log4net.GlobalContext.Properties["LogName"] = Name;

            if (_loggerRepository == null)
            {
                _loggerRepository = LogManager.CreateRepository(Name, typeof(log4net.Repository.Hierarchy.Hierarchy));
                log4net.Config.XmlConfigurator.Configure(_loggerRepository, ParseLog4NetConfigFile(log4netConfigPath));
            }

            _logger = LogManager.GetLogger(Name, typeof(log4net.Repository.Hierarchy.Hierarchy));
        }

        private XmlElement ParseLog4NetConfigFile(string fileName)
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(fileName));
            return log4netConfig["log4net"];
        }

        public string Name { get; }

        public void Info(string message)
        {
            message = FormatMessage(message);
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            message = FormatMessage(message);
            _logger.Warn(message);
        }

        public void Error(string message, Exception ex = null)
        {
            message = FormatMessage(message);
            _logger.Error(message, ex);
        }

        public void Debug(string message)
        {
            message = FormatMessage(message);
            _logger.Debug(message);
        }

        public void Log(LoggingType type, string message)
        {
            switch (type)
            {
                case LoggingType.Error:
                    Error(message);
                    break;

                case LoggingType.Warning:
                    Warn(message);
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

        private string FormatMessage(string message)
        {
            return $"[{Name}] - {message}";
        }
    }
}