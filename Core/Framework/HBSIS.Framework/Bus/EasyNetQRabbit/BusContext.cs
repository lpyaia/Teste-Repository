using EasyNetQ;
using EasyNetQ.Consumer;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons.Exceptions;
using HBSIS.Framework.Commons.Utils;
using System;

namespace HBSIS.Framework.Bus.EasyNetQRabbit
{
    public class BusContext : Disposable, IBusContext
    {
        protected IBus Bus { get; private set; }

        public void Connect()
        {
            Bus = BusEasyNetQFactory.CreateBus();

            if (Bus == null)
                throw new HBBusException("Bus not defined.");
        }

        public void Enqueue<T>(string contextName, T message)
            where T : class, IBusMessage
        {
            Bus.Send(contextName, message);
        }

        public virtual void Receive<T>(string contextName, Action<T> action)
            where T : class, IBusMessage
        {
            var queue = Bus.Advanced.QueueDeclare(contextName);
            Bus.Advanced.Consume(queue, x => x.Add<T>((message, info) => action(message.Body)).ThrowOnNoMatchingHandler = false);
        }

        public virtual void Receive(string contextName, Action<IHandlerRegistration> addHandlers)
        {
            var queue = Bus.Advanced.QueueDeclare(contextName);
            Bus.Advanced.Consume(queue, addHandlers);
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