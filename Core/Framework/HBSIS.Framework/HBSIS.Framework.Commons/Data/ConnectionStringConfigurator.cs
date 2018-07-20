using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Exceptions;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace HBSIS.Framework.Commons.Data
{
    public class ConnectionStringConfigurator : XmlConfigurator<ConnectionStringSettingsCollection>
    {
        protected string ConnectionStringsKey = "connectionStrings";
        protected string NameKey = "name";
        protected string ConnectionStringKey = "connectionString";
        protected string ProviderNameKey = "providerName";

        public ConnectionStringConfigurator(string fileName = null)
            : base(nameof(ConnectionStringConfigurator), fileName)
        {
        }

        protected override ConnectionStringSettingsCollection CreateModel(XDocument document)
        {
            var ret = new ConnectionStringSettingsCollection();

            var items = (from lv1 in document.Descendants(ConnectionStringsKey)
                         select lv1.Elements()).FirstOrDefault();

            foreach (var item in items.ToList())
            {
                var conn = new ConnectionStringSettings();
                conn.Name = item.Attribute(NameKey)?.Value;
                conn.ConnectionString = item.Attribute(ConnectionStringKey)?.Value;
                conn.ProviderName = item.Attribute(ProviderNameKey)?.Value;

                ret.Add(conn);
            }

            return ret;
        }

        public static string GetValueOrDefault(string name, string defaultValue)
        {
            string conn = null;

            var configurator = new ConnectionStringConfigurator().GetCurrent();

            if (configurator != null)
                conn = configurator[name]?.ConnectionString;

            if (conn == null)
                conn = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;

            return conn ?? defaultValue;
        }

        public static string GetConnectionString(string name = null)
        {
            var conn = ConnectionStringConfigurator.GetValueOrDefault(name, "");

            if (string.IsNullOrWhiteSpace(conn))
                throw new HBDataException("ConnectionString not defined.");

            return conn;
        }
    }
}