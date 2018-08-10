using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons.Utils;
using System;

namespace HBSIS.Framework.Bus.EasyNetQRabbit
{
    public abstract class BaseConsumer<TMessage> : Disposable, IConsumer<TMessage>
        where TMessage : class, IBusMessage
    {
        public DateTime LastConsumming { get; private set; }

        public string ContextName { get; private set; }

        protected IBusContext Bus { get; private set; }

        protected BaseConsumer(string contextName)
        {
            ContextName = contextName;
            Bus = new BusContext();
        }

        public abstract void Consume(TMessage message);

        private void ConsumeInternal(TMessage message)
        {
            LastConsumming = DateTime.UtcNow;
            Consume(message);
        }

        public void Connect()
        {
            Bus.Connect();
            Bus.Receive<TMessage>(ContextName, Consume);
        }

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                Bus.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion IDisposable
    }
}