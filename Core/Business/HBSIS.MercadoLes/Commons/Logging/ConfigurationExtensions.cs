using HBSIS.Framework.Commons.Config;

namespace HBSIS.MercadoLes.Commons.Logging
{
    public static class ConfigurationExtensions
    {
        public const string MessageConnectionStringKey = "FWK_LES_LOG_CONNECTIONSTRING";
        public const string ConnectionStringName = "hbsis.les-log";

        public static IConfiguration UseLogConnectionString(this IConfiguration configuration, string connectionStringName)
        {
            if (configuration == null) return configuration;

            configuration.Put(MessageConnectionStringKey, connectionStringName);
            return configuration;
        }

        public static string GetLogConnectionStringName(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            var cnName = configuration.Get<string>(MessageConnectionStringKey);

            if (string.IsNullOrWhiteSpace(cnName))
            {
                return ConnectionStringName;
            }

            return cnName;
        }
    }
}