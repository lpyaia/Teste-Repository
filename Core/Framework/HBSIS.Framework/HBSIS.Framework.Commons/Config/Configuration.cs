using HBSIS.Framework.Data.Mongo;

namespace HBSIS.Framework.Commons.Config
{
    public static class Configuration
    {
        private static readonly object _lock = new object();
        internal static IConfiguration _config;

        public static IConfiguration Actual
        {
            get
            {
                lock (_lock)
                {
                    return _config;
                }
            }
            internal set
            {
                lock (_lock)
                {
                    _config = value;
                }
            }
        }

        public static IConfiguration UseStaticDictionary()
        {
            return new StaticDictionaryConfiguration();
        }
    }
}