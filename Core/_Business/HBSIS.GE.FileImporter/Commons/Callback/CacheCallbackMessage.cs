using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.GE.FileImporter.Services.Commons.Base.Message;

namespace HBSIS.GE.FileImporter.Services.Commons.Callback
{
    public class CacheCallbackMessage : BaseMessage<CacheCallbackMessage>
    {
        public string Key { get; set; }

        public StatusDto Action { get; set; }
    }
}