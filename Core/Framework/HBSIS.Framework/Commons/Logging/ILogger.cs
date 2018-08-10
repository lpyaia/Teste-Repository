using System;

namespace HBSIS.Framework.Commons.Logging
{
    public interface ILogger
    {
        string Name { get; }

        void Debug(string message);

        void Info(string message);

        void Warn(string message);

        void Error(string message, Exception ex = null);

        void Log(LoggingType type, string message);
    }
}