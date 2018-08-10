using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Management.Client;
using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Exceptions;

namespace HBSIS.Framework.Bus.EasyNetQRabbit
{
    public static class RabbitFactory
    {
        private static readonly object _lock = new object();
        private static IManagementClient _client = null;

        public static IManagementClient Client
        {
            get
            {
                lock (_lock)
                {
                    if (_client == null)
                        _client = CreateClient();

                    return _client;
                }
            }
        }

        public static IManagementClient CreateClient()
        {
            var address = Configuration.Actual.GetRabbitAddress();
            var user = Configuration.Actual.GetRabbitUser();
            var password = Configuration.Actual.GetRabbitPassword();

            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(password))
                throw new HBBusException("RabbitParameters not defined.");

            return CreateClient(address, user, password);
        }

        public static IManagementClient CreateClient(string address, string user, string password)
        {
            return new ManagementClient(address, user, password);
        }

        public static IBus CreateBus(string address, string user, string password)
        {
            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
                throw new HBBusException("RabbitParameters not defined.");

            return CreateBusInternal($"host={address};username={user};password={password}");
        }

        public static IBus CreateBus(string address, string virtualHost, string user, string password)
        {
            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
                throw new HBBusException("RabbitParameters not defined.");

            return CreateBusInternal($"host={address};virtualHost={virtualHost};username={user};password={password}");
        }

        public static IBus CreateBus(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new HBBusException("RabbitConnectionString not defined.");

            return CreateBusInternal(connectionString);
        }

        private static IBus CreateBusInternal(string connectionString)
        {
            return RabbitHutch.CreateBus(connectionString, serviceRegister => serviceRegister.Register<IConsumerErrorStrategy, KillMessageStrategy>());
        }
    }
}