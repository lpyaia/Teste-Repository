using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;

namespace HBSIS.MercadoLes.Services.Commons.Base.Service
{
    public class BaseService<TMessage, TDto> : BaseService, IService<TMessage>
        where TMessage : SpecializedMessage<TMessage>
        where TDto : class, IDto
    {
        public virtual void StoreMessage(TMessage message)
        {
        }
    }
}