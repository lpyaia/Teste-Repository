using System;

namespace HBSIS.Framework.Bus.Message
{
    public class GeneralMessage : IPublishMessage, ICallbackMessage
    {
        public GeneralMessage(string name)
        {
            RequestId = Guid.NewGuid();
            RequestName = name;
        }

        public Guid RequestId { get; set; }
        public string Token { get; set; }
        public string RequestName { get; set; }
        public string UserName { get; set; }
    }
}