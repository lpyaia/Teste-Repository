using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Cache;
using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;

namespace HBSIS.MercadoLes.Services.Commons.Base.Cache
{
    public class Cacher<T> : ICacher
        where T : class, IDto
    {
        public virtual void StoreCache(CacheMessage message)
        {
            if (message == null) return;

            var dto = message.Content.JsonDeserialize<T>();

            if (dto == null) return;

            Process(dto);
        }

        public virtual void StoreCache(IDto dto)
        {
            if (dto == null) return;

            Process(dto as T);
        }

        protected virtual void Process(T dto)
        {
        }

        public static void Store(T dto)
        {
            //var cacher = Cachers.Actual[typeof(T).Name];
            //cacher?.StoreCache(dto);
        }
    }
}