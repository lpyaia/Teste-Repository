using HBSIS.Framework.Bus.Message;
using System;

namespace HBSIS.Framework.Bus.Bus
{
    public interface IConsumer : IDisposable
    {
        //DateTime LastConsumming { get; }

        void Connect();
    }

    public interface IConsumer<TMessage> : IConsumer
        where TMessage : IBusMessage
    {
        void Consume(TMessage message);
    }
}