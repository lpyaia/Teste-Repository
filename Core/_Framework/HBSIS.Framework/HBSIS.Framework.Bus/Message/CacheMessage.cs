using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;

namespace HBSIS.Framework.Bus.Message
{
    public class CacheMessage : GeneralMessage, ICacheMessage
    {
        public CacheMessage(IBusMessage contentMessage)
            : base(contentMessage?.GetType().Name)
        {
            Content = contentMessage?.JsonSerialize();
            ContentType = contentMessage?.GetType().Name;
        }

        public string ContentType { get; set; }
        public string Content { get; set; }
    }
}