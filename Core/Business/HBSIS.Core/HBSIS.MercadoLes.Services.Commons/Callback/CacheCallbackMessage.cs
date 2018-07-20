using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.MercadoLes.Services.Commons.Base.Message;

namespace HBSIS.MercadoLes.Services.Commons.Callback
{
    public class CacheCallbackMessage : BaseMessage<CacheCallbackMessage>
    {
        public string Key { get; set; }

        public StatusDto Action { get; set; }
    }
}