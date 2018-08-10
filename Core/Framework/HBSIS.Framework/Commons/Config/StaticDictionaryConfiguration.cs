using System.Collections.Generic;

namespace HBSIS.Framework.Commons.Config
{
    public class StaticDictionaryConfiguration : IConfiguration
    {
        private Dictionary<string, object> all = new Dictionary<string, object>();

        public T Get<T>(string key)
        {
            if (all.ContainsKey(key))
            {
                return (T)all[key];
            }

            return default(T);
        }

        public void Put<T>(string key, T value)
        {
            if (all.ContainsKey(key))
            {
                all[key] = value;
                return;
            }

            all.Add(key, value);
        }

        public void Configure()
        {
            Configuration.Actual = this as IConfiguration;
        }
    }
}