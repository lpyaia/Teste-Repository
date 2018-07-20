using EasyNetQ.Consumer;
using HBSIS.Framework.Bus.Message;
using System;
using System.Collections.Generic;

namespace HBSIS.Framework.Bus.Bus
{
    public interface IBusContext : IDisposable
    {
        void Connect();

        void Enqueue<T>(string contextName, T message) where T : class, IBusMessage;

        void Receive<T>(string contextName, Action<T> action) where T : class, IBusMessage;

        void Receive(string contextName, Action<IHandlerRegistration> addHandlers);
    }
}