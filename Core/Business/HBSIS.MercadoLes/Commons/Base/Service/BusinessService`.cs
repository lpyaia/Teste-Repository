using HBSIS.MercadoLes.Commons.Base.Message;
using HBSIS.MercadoLes.Commons.Cache;
using HBSIS.MercadoLes.Commons.Helpers;

namespace HBSIS.MercadoLes.Commons.Base.Service
{
    public abstract class BusinessService<TMessage> : BusinessService<TMessage, CacheCollectionDto>
        where TMessage : BaseMessage<TMessage>
    {
        protected override CacheCollectionDto ProcessDto(CacheCollection cache)
        {
            return cache.ToDto();
        }
    }
}