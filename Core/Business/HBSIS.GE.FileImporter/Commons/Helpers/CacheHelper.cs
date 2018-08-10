using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Cache;
using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using HBSIS.GE.FileImporter.Services.Commons.Cache;
using HBSIS.GE.FileImporter.Services.Commons.Logging;
using HBSIS.GE.FileImporter.Services.Commons.Logging.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.GE.FileImporter.Services.Commons.Helpers
{
    public static class CacheHelper
    {
        public const string ContextName = "Cache";

        public static void Send(this IDto dto, ICallbackMessage callback = null)
        {
            if (dto == null) return;

            var message = dto.ToMessage();

            bool onCallback = callback != null && callback.Token != null;

            if (onCallback)
            {
                message.Token = callback.Token;
            }

            using (var ctx = BusFactory.CreateBusContext())
            {
                SendInternal(ctx, message, onCallback);
            }
        }

        public static void SendMany(this CacheCollection cache)
        {
            if (cache == null || cache.Count == 0) return;

            var messages = cache.ToMessages();

            using (var ctx = BusFactory.CreateBusContext())
            {
                foreach (var message in messages)
                {
                    SendInternal(ctx, message);
                }
            }
        }

        private static void SendInternal(this IBusContext ctx, CacheMessage message, bool onCallback = false)
        {
            if (onCallback)
            {
                message.Token = message.Token ?? message.RequestId.ToString();
                MessageLogger.Sended(message);
            }

            ctx.Enqueue(ContextName, message);
        }

        public static void TryEnqueue(this IBusContext ctx, CacheMessage message)
        {
            if (message == null) return;

            ctx.Enqueue(ContextName, message);
        }

        public static CacheCollectionDto ToDto(this CacheCollection cache)
        {
            if (cache == null || cache.Count == 0) return null;

            var dto = new CacheCollectionDto();
            dto.Dtos = new List<Tuple<string, string>>();

            foreach (var item in cache)
            {
                if (item != null)
                {
                    var typeName = item.GetType().AssemblyQualifiedName;
                    var content = JsonHelper.Serialize(item);

                    dto.Dtos.Add(Tuple.Create<string, string>(typeName, content));
                }
            }

            return dto;
        }

        #region CacheCollection

        public static List<T> GetAllOf<T>(this CacheCollection value)
        {
            if (value == null) return null;

            return value.OfType<T>().ToList();
        }

        public static T GetFirstOf<T>(this CacheCollection value)
        {
            if (value == null) return default(T);

            return value.OfType<T>().FirstOrDefault();
        }

        public static T GetLastOf<T>(this CacheCollection value)
        {
            if (value == null) return default(T);

            return value.OfType<T>().LastOrDefault();
        }

        #endregion CacheCollection
    }
}