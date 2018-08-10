using System;
using System.ServiceModel;

namespace HBSIS.MercadoLes.Commons.Helpers
{
    public class ChannelFactoryHelper
    {
        private static BasicHttpBinding DefaultBinding
        {
            get
            {
                return new BasicHttpBinding()
                {
                    MaxReceivedMessageSize = 2147483647,
                    OpenTimeout = new TimeSpan(0, 3, 0),
                    CloseTimeout = new TimeSpan(0, 3, 0),
                    ReceiveTimeout = new TimeSpan(0, 3, 0),
                    SendTimeout = new TimeSpan(0, 3, 0)
                };
            }
        }

        public static T CreateChannel<T>(string url)
        {
            var endpoint = new EndpointAddress(url);
            var channelFactory = new ChannelFactory<T>(DefaultBinding, endpoint);

            return channelFactory.CreateChannel();
        }

        public static T CreateChannel<T>(string url, string userName = null, string password = null)
        {
            var binding = DefaultBinding;
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Basic;

            var endpoint = new EndpointAddress(url);
            var channelFactory = new ChannelFactory<T>(binding, endpoint);

            if (userName != null)
            {
                channelFactory.Credentials.UserName.UserName = userName;
                channelFactory.Credentials.UserName.Password = password;
            }

            return channelFactory.CreateChannel();
        }
    }
}