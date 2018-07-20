using EasyNetQ.Consumer;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons;
using System;

namespace HBSIS.Framework.Bus.EasyNetQRabbit
{
    public class SingleConsumer<TMessage> : BaseConsumer<TMessage>, Bus.IConsumer
        where TMessage : SpecializedMessage<TMessage>
    {
        private readonly IService<TMessage> _service;

        public SingleConsumer(string contextName, IService<TMessage> service)
           : base(contextName)
        {
            this._service = service;
        }

        public override void Consume(TMessage message)
        {
            _service.StoreMessage(message);
        }
    }

   
}