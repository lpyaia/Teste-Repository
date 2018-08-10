using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using log4net.Appender;
using log4net.Core;

namespace HBSIS.MercadoLes.Commons.Logging
{
    public class LogEventRabbitAppender : AppenderSkeleton
    {
        public const string ContextName = "LogEvent";

        private static readonly object _lock = new object();
        private static bool _busCreated = false;
        private static IBusContext _busContext;

        public static IBusContext BusContext
        {
            get
            {
                lock (_lock)
                {
                    if (!_busCreated)
                    {
                        _busCreated = true;
                        _busContext = BusFactory.TryCreateBusContext();
                    }

                    return _busContext;
                }
            }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var e = new LogEvent();
            e.Date = loggingEvent.TimeStamp;
            e.Thread = loggingEvent.ThreadName;
            e.Level = loggingEvent.Level.ToString();
            e.Logger = loggingEvent.LoggerName;
            e.Message = loggingEvent.RenderedMessage;
            e.Exception = loggingEvent.GetExceptionString();

            BusContext?.Enqueue(ContextName, e);
        }
    }
}