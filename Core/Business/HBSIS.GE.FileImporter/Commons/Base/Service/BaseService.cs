using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.GE.FileImporter.Services.Commons.Base.Message;
using HBSIS.GE.FileImporter.Services.Commons.Cache;
using HBSIS.GE.FileImporter.Services.Commons.Helpers;
using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Commons.Base.Service
{
    public class BaseService
    {
        public CacheCollection Caches { get; private set; } = CacheCollection.Empty;
        public MessageCollection Messages { get; private set; } = MessageCollection.Empty;

        #region Messages

        protected void SendMessages(ICallbackMessage callback)
        {
            foreach (var message in Messages)
            {
                bool onCallback = callback != null && callback.Token != null;

                if (onCallback)
                {
                    message.Token = callback.Token;
                }

                message.Send(onCallback: onCallback);
            }
        }

        protected void Message(ISpecializedMessage message)
        {
            Messages.Enqueue(message);
        }

        protected void Message(IEnumerable<ISpecializedMessage> messages)
        {
            foreach (var message in messages)
            {
                Message(message);
            }
        }

        #endregion Messages

        #region Caches

        protected void ClearMessages()
        {
            Messages = MessageCollection.Empty;
        }

        protected void Cache(IDto dto)
        {
            Caches.Enqueue(dto);
        }

        protected void Cache(CacheCollection collection)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    Cache(item);
                }
            }
        }

        protected void ClearCache()
        {
            Caches = CacheCollection.Empty;
        }

        #endregion Caches
    }
}