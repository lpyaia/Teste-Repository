using EasyNetQ.Consumer;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Commons.Utils;
using System;

namespace HBSIS.Framework.Bus.EasyNetQRabbit
{
    public class MultipleConsumer : Disposable, Bus.IConsumer
    {
        public string ContextName { get; private set; }

        protected IBusContext Bus { get; private set; }

        private Action<IHandlerRegistration> AddHandlers { get; }

        public MultipleConsumer(string contextName, Action<IHandlerRegistration> addHandlers)
        {
            ContextName = contextName;
            AddHandlers = addHandlers;
            Bus = new BusContext();
        }

        public void Connect()
        {
            Bus.Connect();
            Bus.Receive(ContextName, AddHandlers);
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