using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Helper;
using System.Linq;
using System.Xml.Linq;

namespace HBSIS.MercadoLes.Commons.Integration.Config
{
    public class IntegrationConfigurator<T> : XmlConfigurator<IntegrationConfigCollection<T>>
        where T : class, IIntegrationConfig, new()
    {
        protected string IntegrationsKey = "integrations";
        protected string IntegrationKey = "integration";
        protected string ConfigKey = "config";
        protected string NameKey = "name";
        protected string UserKey = "userName";
        protected string PasswordKey = "password";
        protected string UrlKey = "url";
        protected string Url2Key = "url2";
        protected string IntervalKey = "interval";
        protected string EnabledKey = "enabled";
        protected string AttemptsKey = "attempts";
        protected string Token = "token";
        protected string ExtensionDataKey = "extensionData";

        public IntegrationConfigurator(string name, string fileName = null)
            : base($"IntegrationConfigurator_{name}", fileName)
        {
            Name = name;
        }

        protected string Name { get; }

        protected override IntegrationConfigCollection<T> CreateModel(XDocument document)
        {
            var ret = new IntegrationConfigCollection<T>();

            var integration = (from lv1 in document.Descendants(IntegrationsKey)
                               select lv1)
                               .FirstOrDefault()
                               .Elements()
                               .Where(x => x.Attribute(NameKey)?.Value?.ToUpper() == Name?.ToUpper())
                               .FirstOrDefault();

            if (integration != null)
            {
                ret.Name = integration.Attribute(NameKey)?.Value;
                ret.Interval = ConvertHelper.ToInt(integration.Attribute(IntervalKey)?.Value ?? "5");

                foreach (var item in integration.Elements())
                {
                    var config = GetConfig(item);

                    if (config != null)
                        ret.Add(config);
                }
            }

            return ret;
        }

        protected virtual T GetConfig(XElement item)
        {
            var config = new T();

            config.Name = item.Element(NameKey)?.Value;
            config.UserName = item.Element(UserKey)?.Value;
            config.Password = item.Element(PasswordKey)?.Value;
            config.Url = item.Element(UrlKey)?.Value;
            config.Url2 = item.Element(Url2Key)?.Value;
            config.Token = item.Element(Token)?.Value;
            config.ExtensionData = item.Element(ExtensionDataKey)?.Value;

            var interval = item.Element(IntervalKey)?.Value;
            if (interval != null)
                config.Interval = ConvertHelper.ToInt(interval);

            var enabled = item.Element(EnabledKey)?.Value;
            if (enabled != null)
                config.Enabled = ConvertHelper.ToBoolean(enabled);

            var attempts = item.Element(AttemptsKey)?.Value;
            if (attempts != null)
                config.Attempts = ConvertHelper.ToInt(attempts);

            return config;
        }
    }
}