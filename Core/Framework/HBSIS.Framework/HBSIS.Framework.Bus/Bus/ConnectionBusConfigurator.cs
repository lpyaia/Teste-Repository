using HBSIS.Framework.Commons.Config;
using System.Linq;
using System.Xml.Linq;

namespace HBSIS.Framework.Bus.Bus
{
    public class ConnectionBusConfigurator : XmlConfigurator<ConnectionBusConfig>
    {
        protected string ConnectionBusKey = "connectionBus";
        protected string AddressKey = "address";
        protected string UserKey = "user";
        protected string PasswordKey = "password";
        protected string VhostKey = "vhost";

        public ConnectionBusConfigurator(string fileName = null)
            : base(nameof(ConnectionBusConfigurator), fileName)
        {
        }

        protected override ConnectionBusConfig CreateModel(XDocument document)
        {
            var ret = new ConnectionBusConfig();

            var item = (from lv1 in document.Descendants(ConnectionBusKey)
                        select lv1).FirstOrDefault();

            if (item != null)
            {
                ret.Address = item.Element(AddressKey)?.Value;
                ret.User = item.Element(UserKey)?.Value;
                ret.Password = item.Element(PasswordKey)?.Value;
                ret.Vhost = item.Element(VhostKey)?.Value;
            }

            return ret;
        }
    }
}