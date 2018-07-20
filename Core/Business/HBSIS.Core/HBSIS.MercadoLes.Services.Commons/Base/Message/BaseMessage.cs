using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.MercadoLes.Services.Commons.Config;

namespace HBSIS.MercadoLes.Services.Commons.Base.Message
{
    public class BaseMessage<T> : SpecializedMessage<T>
        where T : BaseMessage<T>
    {
        public BaseMessage()
        {
            UserName = GlobalSettings.CurrentUserName;
        }
    }
}