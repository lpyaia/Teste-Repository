using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.EasyNetQRabbit;
using HBSIS.Framework.Bus.Message;
using HBSIS.MercadoLes.Services.Commons.Cache;
using HBSIS.MercadoLes.Services.Commons.Helpers;
using System;
//using Topshelf;

namespace HBSIS.MercadoLes.Services.Commons.Base.ServiceControl
{
    public class ConsumerServiceControl : CustomServiceControl
    {
        private readonly IConsumer consumer;

        public ConsumerServiceControl(IConsumer consumer)
        {
            this.consumer = consumer;
        }

        public override bool Start()
        {
            bool result = true;

            try
            {
                consumer?.Connect();
            }

            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public override bool Stop()
        {
            consumer?.Dispose();
            return true;
        }

        #region Create

        public static ConsumerServiceControl Create(string contextName, Action<EasyNetQ.Consumer.IHandlerRegistration> addHandlers)
        {
            var consumer = new MultipleConsumer(contextName, addHandlers);
            return new ConsumerServiceControl(consumer);
        }

        public static ConsumerServiceControl Create<TService, TMessage>(string contextName = null)
            where TService : IService<TMessage>, new()
            where TMessage : SpecializedMessage<TMessage>, new()
        {
            var service = new TService();
            contextName = contextName ?? new TMessage().ContextName;

            return Create(new SingleConsumer<TMessage>(contextName, service));
        }

        public static ConsumerServiceControl Create<TService, TMessage>(TService service, string contextName = null)
           where TService : IService<TMessage>
           where TMessage : SpecializedMessage<TMessage>, new()
        {
            contextName = contextName ?? new TMessage().ContextName;

            return Create(new SingleConsumer<TMessage>(contextName, service));
        }

        public static ConsumerServiceControl Create(IConsumer consumer)
        {
            return new ConsumerServiceControl(consumer);
        }

        public static ConsumerServiceControl CreateCache(string contextName = null)
        {
            contextName = contextName ?? CacheHelper.ContextName;
            var consumer = new CacheConsumer(contextName);
            return Create(consumer);
        }

        #endregion Create
    }
}