using EasyNetQ.Consumer;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;
using System;

namespace HBSIS.Framework.Bus.Mock
{
    public class MockBusContext : IBusContext
    {
        public string ContextName { get; set; }

        public virtual void Connect()
        {
        }

        public void Dispose()
        {
        }

        public void Enqueue<T>(string contextName, T message)
            where T : class, IBusMessage
        {
            contextName = contextName ?? ContextName;
            MockBusQueues.Enqueue(contextName, message);
        }

        public void Receive(string contextName, Action<IHandlerRegistration> addHandlers)
        {
        }

        public void Receive<T>(string contextName, Action<T> action)
            where T : class, IBusMessage
        {
        }
    }
}