using HBSIS.Framework.Bus.Message;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.Framework.Bus.Mock
{
    public class MockBusQueues
    {
        private static Dictionary<string, Queue<IBusMessage>> queueCollection = new Dictionary<string, Queue<IBusMessage>>();

        private static Queue<IBusMessage> GetQueue(string queueName)
        {
            if (queueCollection.ContainsKey(queueName))
            {
                return queueCollection[queueName];
            }

            var queue = new Queue<IBusMessage>();
            queueCollection.Add(queueName, queue);

            return queueCollection[queueName];
        }

        public static void Clear(string queueName = null)
        {
            if (queueName == null)
            {
                queueCollection.Clear();
            }
            else
            {
                var mocks = GetQueue(queueName);
                mocks.Clear();
            }
        }

        public static void Enqueue(ISpecializedMessage message)
        {
            var contextName = message.ContextName;
            EnqueueInternal(contextName, message);
        }

        public static void Enqueue(string contextName, IBusMessage message)
        {
            EnqueueInternal(contextName, message);
        }

        private static void EnqueueInternal(string contextName, IBusMessage message)
        {
            var queues = GetQueue(contextName);
            queues.Enqueue(message);
        }

        public static IEnumerable<TMessage> Gets<TMessage>(string contextName)
          where TMessage : IBusMessage
        {
            var values = GetQueue(contextName);
            return values.OfType<TMessage>();
        }

        public static List<TMessage> GetAll<TMessage>(string contextName = null)
          where TMessage : SpecializedMessage<TMessage>, new()
        {
            contextName = contextName ?? new TMessage().ContextName;
            return Gets<TMessage>(contextName).ToList();
        }

        public static TMessage GetFirst<TMessage>(string contextName = null)
           where TMessage : SpecializedMessage<TMessage>, new()
        {
            contextName = contextName ?? new TMessage().ContextName;
            var values = GetAll<TMessage>(contextName);

            return values.FirstOrDefault();
        }

        public static TMessage GetLast<TMessage>(string contextName = null)
             where TMessage : SpecializedMessage<TMessage>, new()
        {
            contextName = contextName ?? new TMessage().ContextName;
            var values = GetAll<TMessage>(contextName);

            return values.LastOrDefault();
        }
    }
}