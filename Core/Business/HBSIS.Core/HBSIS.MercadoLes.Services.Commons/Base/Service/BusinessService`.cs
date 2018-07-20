using HBSIS.MercadoLes.Services.Commons.Base.Message;
using HBSIS.MercadoLes.Services.Commons.Cache;
using HBSIS.MercadoLes.Services.Commons.Helpers;

namespace HBSIS.MercadoLes.Services.Commons.Base.Service
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