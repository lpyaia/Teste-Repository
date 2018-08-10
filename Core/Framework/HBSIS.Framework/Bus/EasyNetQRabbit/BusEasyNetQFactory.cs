using EasyNetQ;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Commons.Config;

namespace HBSIS.Framework.Bus.EasyNetQRabbit
{
    public class BusEasyNetQFactory : Bus.BusFactory
    {
        public override IBusContext CreateContext()
        {
            return new BusContext();
        }

        internal static IBus CreateBus()
        {
            var address = Configuration.Actual.GetRabbitAddress();
            var user = Configuration.Actual.GetRabbitUser();
            var password = Configuration.Actual.GetRabbitPassword();
            var virtualHost = Configuration.Actual.GetRabbitVirtualHost();

            if (string.IsNullOrEmpty(virtualHost))
                return RabbitFactory.CreateBus(address, user, password);
            else
                return RabbitFactory.CreateBus(address, virtualHost, user, password);
        }
    }
}