using HBSIS.MercadoLes.Services.Commons.Enums;
using System;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Commons.Logging.Message
{
    public class LogMessage
    {
        public LogMessage()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }     
        public List<MessageDetail> Messages { get; set; } = new List<MessageDetail>();

        public class MessageDetail
        {
            public Guid RequestId { get; set; }
            public StatusMessage Status { get; set; }
            public string Name { get; set; }
            public string Message { get; set; }
            public string Sender { get; set; }
            public string Consumer { get; set; }
            public string Error { get; set; }
            public DateTime Date { get; set; }
        }
    }
}