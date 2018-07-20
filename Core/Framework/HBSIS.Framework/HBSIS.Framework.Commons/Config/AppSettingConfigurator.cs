using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace HBSIS.Framework.Commons.Config
{
    public class AppSettingConfigurator : XmlConfigurator<NameValueCollection>
    {
        protected string AppSettingsKey = "appSettings";
        protected string NameKey = "key";
        protected string ValueKey = "value";

        public AppSettingConfigurator()
            : base(nameof(AppSettingConfigurator))
        {
        }

        protected override NameValueCollection CreateModel(XDocument document)
        {
            var ret = new NameValueCollection();

            var items = (from lv1 in document.Descendants(AppSettingsKey)
                         select lv1.Elements()).FirstOrDefault();

            foreach (var item in items.ToList())
            {
                var value = item.Attribute(NameKey)?.Value;
                var name = item.Attribute(ValueKey)?.Value;

                ret.Add(value, name);
            }

            return ret;
        }

        public static string GetValueOrDefault(string key, string defaultValue = null)
        {
            string ret = null;

            var configurator = new AppSettingConfigurator();

            if (configurator != null)
                ret = configurator.GetCurrent()?.Get(key);

            if (ret == null)
                ret = ConfigurationManager.AppSettings[key];

            return !string.IsNullOrEmpty(ret) ? ret : defaultValue;
        }
    }
}