using HBSIS.MercadoLes.Commons.Base.Message;
using System;

namespace HBSIS.MercadoLes.Messages.Message
{
    public class IntegracaoSapBrfMessage : BaseMessage<IntegracaoSapBrfMessage>
    {
        public long CdRota { get; set; }

        public static IntegracaoSapBrfMessage Get(long cdRota)
        {
            var message = new IntegracaoSapBrfMessage();
            message.CdRota = cdRota;
            
            return message;
        }
    }
}