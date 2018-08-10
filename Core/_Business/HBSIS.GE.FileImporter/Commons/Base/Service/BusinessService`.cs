using HBSIS.GE.FileImporter.Services.Commons.Base.Message;
using HBSIS.GE.FileImporter.Services.Commons.Cache;
using HBSIS.GE.FileImporter.Services.Commons.Helpers;

namespace HBSIS.GE.FileImporter.Services.Commons.Base.Service
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