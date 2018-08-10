using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.GE.FileImporter.Services.Commons.Base.Message;
using HBSIS.GE.FileImporter.Services.Commons.Logging;
using HBSIS.GE.FileImporter.Services.Commons.Logging.Message;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.GE.FileImporter.Services.Commons.Helpers
{
    public static class MessageHelper
    {
        public static void Send<T>(this T message, bool onCallback = false, string contextName = null)
             where T : class, ISpecializedMessage
        {
            if (message == null) return;

            using (var ctx = BusFactory.CreateBusContext())
            {
                SendInternal(ctx, message, onCallback, contextName);
            }
        }

        public static void SendMany<T>(this List<T> messages, bool onCallback = false, string contextName = null)
            where T : class, ISpecializedMessage
        {
            if (messages == null || messages.Count == 0) return;

            using (var ctx = BusFactory.CreateBusContext())
            {
                foreach (var message in messages)
                {
                    SendInternal(ctx, message, onCallback, contextName);
                }
            }
        }

        public static void Send<T>(this IBusContext ctx, T message, bool onCallback = false, string contextName = null)
            where T : class, ISpecializedMessage
        {
            if (message == null) return;

            SendInternal(ctx, message, onCallback, contextName);
        }

        private static void SendInternal<T>(this IBusContext ctx, T message, bool onCallback = false, string contextName = null)
             where T : class, ISpecializedMessage
        {
            contextName = contextName ?? message.ContextName;

            if (onCallback)
            {
                message.Token = message.Token ?? message.RequestId.ToString();
                MessageLogger.Sended(message);
            }

            ctx.Enqueue(contextName, message);
        }

        #region MessageCollection

        public static List<T> GetAllOf<T>(this MessageCollection value)
        {
            if (value == null) return null;

            return value.OfType<T>().ToList();
        }

        public static T GetFirstOf<T>(this MessageCollection value)
        {
            if (value == null) return default(T);

            return value.OfType<T>().FirstOrDefault();
        }

        public static T GetLastOf<T>(this MessageCollection value)
        {
            if (value == null) return default(T);

            return value.OfType<T>().LastOrDefault();
        }

        #endregion MessageCollection
    }
}