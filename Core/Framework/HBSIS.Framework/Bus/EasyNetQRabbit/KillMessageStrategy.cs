using EasyNetQ;
using EasyNetQ.Consumer;
using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Exceptions;
using HBSIS.Framework.Commons.Helper;
using System;
using System.Collections.Generic;

namespace HBSIS.Framework.Bus.EasyNetQRabbit
{
    public class KillMessageStrategy : DefaultConsumerErrorStrategy
    {
        private Dictionary<string, int> Keys = new Dictionary<string, int>();

        public KillMessageStrategy(IConnectionFactory connectionFactory, ISerializer serializer, IEasyNetQLogger logger, IConventions conventions, ITypeNameSerializer typeNameSerializer, IErrorMessageSerializer errorMessageSerializer)
                : base(connectionFactory, serializer, logger, conventions, typeNameSerializer, errorMessageSerializer)
        {
        }

        public override AckStrategy HandleConsumerError(ConsumerExecutionContext context, Exception exception)
        {
            var ex = exception.InnerException;

            if(ex.GetType() == typeof(HBFlowException)) return AckStrategies.NackWithRequeue;

            var count = 0;
            var id = context.Properties.CorrelationId;

            if (!Keys.ContainsKey(id))
            {
                Keys.Add(id, 0);
            }

            Keys.TryGetValue(id, out count);

            if (count == 0)
            {
                LoggerHelper.Error(ex);
            }

            count++;
            Keys[id] = count;

            return AckStrategies.NackWithRequeue;
        }
    }
}