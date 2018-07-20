using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Result;
using HBSIS.MercadoLes.Services.Commons.Base.Message;

namespace HBSIS.MercadoLes.Services.Commons.Callback
{
    public class GenericCallbackMessage : BaseMessage<GenericCallbackMessage>
    {
        public string Key { get; set; }
        public ResultStatus Status { get; set; }

        public string Message { get; set; }
    }
}