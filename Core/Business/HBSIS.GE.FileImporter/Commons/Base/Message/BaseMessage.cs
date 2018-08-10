using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.GE.FileImporter.Services.Commons.Config;

namespace HBSIS.GE.FileImporter.Services.Commons.Base.Message
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