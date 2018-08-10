using System;

namespace HBSIS.Framework.Bus.Message
{
    public interface IPublishMessage : IBusMessage
    {
        Guid RequestId { get; set; }
        string RequestName { get; set; }
        string UserName { get; set; }
    }
}