using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Context;
using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using HBSIS.Framework.Data.Mongo;
using System.Linq;
using System.Reflection;

namespace HBSIS.Framework.Commons.Config
{
    public static class ConfigurationExtensions
    {
        public const string DataFactoryKey = "FWK_DATA_FACTORY";
        public static string MongoDataFactoryKey = "FWK_MONGO_DATA_FACTORY";
        public const string ContextPersister = "FWK_CONTEXT_PERSISTER";
        public const string AppNameKey = "FWK_COMMONS_APPNAME";
        public const string Log4NetKey = "FWK_LOG_LOG4NET";
        public const string FileImporterPath = "FWK_FILEIMPORTER_PATH";
        public const string EndpointWebServiceGE = "FWK_ENDPOINTWSGE";
        private const string SentFilesPath = "FWK_SENTFILES_PATH";
        private const string SQLConnectionStringKey = "FWK_DATA_SQL_CONNECTIONSTRING";
        private const string BusFactoryKey = "FWK_BUS_FACTORY";
        private const string MongoMapClassKey = "FWK_DATA_MONGO_MAPCLASS";
        private const string MongoConnectionStringKey = "FWK_DATA_MONGO_CONNECTIONSTRING";
        
        public static IConfiguration UseAppName(this IConfiguration configuration, string name)
        {
            if (configuration == null) return configuration;

            configuration.Put(AppNameKey, name);
            return configuration;
        }

        public static IConfiguration UseDataFactory<T>(this IConfiguration configuration)
            where T : class, IFactory
        {
            if (configuration == null) return configuration;

            configuration.Put(DataFactoryKey, typeof(T).AssemblyQualifiedName);
            return configuration;
        }

        public static IConfiguration UseMongoDataFactory<T>(this IConfiguration configuration)
            where T : class, IFactory
        {
            if (configuration == null) return configuration;

            configuration.Put(MongoDataFactoryKey, typeof(T));
            return configuration;
        }

        public static string GetDataFactoryTypeName(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.Get<string>(DataFactoryKey);
        }

        public static IConfiguration UseContextPersister<T>(this IConfiguration configuration)
          where T : class, IApplicationContext
        {
            if (configuration == null) return null;

            configuration.Put(ContextPersister, typeof(T).AssemblyQualifiedName);

            return configuration;
        }

        public static IConfiguration UseThreadContextPersister(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.UseContextPersister<ThreadContext>();
        }

        public static IConfiguration UseSingletonContextPersister(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.UseContextPersister<SingletonContext>();
        }

        public static IConfiguration UseSqlConnectionString(this IConfiguration configuration, string connectionStringName)
        {
            if (configuration == null) return configuration;

            configuration.Put(SQLConnectionStringKey, connectionStringName);
            return configuration;
        }

        public static IConfiguration UseLog4Net(this IConfiguration configuration)
        {
            if (configuration == null) return configuration;

            configuration.Put(Log4NetKey, Configurator.GetLogPathName(""));
            return configuration;
        }

        public static string GetContextPersisterTypeName(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.Get<string>(ContextPersister);
        }

        public static string GetAppName(this IConfiguration configuration)
        {
            var defaultName = "Undefined";

            if (configuration == null) return defaultName;

            var name = configuration.Get<string>(AppNameKey);

            if (string.IsNullOrWhiteSpace(defaultName))
                name = Assembly.GetEntryAssembly()?.GetName().Name.Split('.').LastOrDefault();

            return name ?? defaultName;
        }

        public static string GetSqlConnectionStringName(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.Get<string>(SQLConnectionStringKey);
        }

        public static string GetSqlConnectionString(this IConfiguration configuration, string name = null)
        {
            name = name ?? GetSqlConnectionStringName(configuration) ?? "SqlDB";
            return ConnectionStringConfigurator.GetConnectionString(name);
        }

        public static IConfiguration GetFileImporterPath(this IConfiguration configuration)
        {
            configuration.Put(FileImporterPath, AppSettingConfigurator.GetValueOrDefault("files"));
            return configuration;
        }

        public static IConfiguration GetEndointWebServiceGE(this IConfiguration configuration)
        {
            configuration.Put(EndpointWebServiceGE, AppSettingConfigurator.GetValueOrDefault("endpointWebServiceGE"));
            return configuration;
        }

        public static IConfiguration GetSentFilesPath(this IConfiguration configuration)
        {
            configuration.Put(SentFilesPath, AppSettingConfigurator.GetValueOrDefault("sentFiles"));
            return configuration;
        }

        public static string GetLog4NetConfigPath(this IConfiguration configuration)
        {
            return configuration.Get<string>(Log4NetKey);
        }

        public static int GetJobInterval(this IConfiguration configuration, string jobName)
        {
            return Configurator.GetJobInterval(jobName);
        }

        internal static IConfiguration UseBusFactory<T>(this IConfiguration configuration)
            where T : BusFactory
        {
            if (configuration == null) return configuration;

            configuration.Put(BusFactoryKey, typeof(T).AssemblyQualifiedName);
            return configuration;
        }

        public static IConfiguration UseBusMockFactory(this IConfiguration configuration)
        {
            if (configuration == null) return configuration;

            return configuration.UseBusFactory<Bus.Mock.MockBusFactory>();
        }

        public static IConfiguration UseBusEasyNetQFactory(this IConfiguration configuration)
        {
            if (configuration == null) return configuration;

            return configuration.UseBusFactory<HBSIS.Framework.Bus.EasyNetQRabbit.BusEasyNetQFactory>();
        }

        public static string GetBusFactoryTypeName(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.Get<string>(BusFactoryKey);
        }

        public static string GetRabbitAddress(this IConfiguration configuration)
        {
            return new ConnectionBusConfigurator().GetCurrent()?.Address;
        }

        public static string GetRabbitUser(this IConfiguration configuration)
        {
            return new ConnectionBusConfigurator().GetCurrent()?.User;
        }

        public static string GetRabbitPassword(this IConfiguration configuration)
        {
            return new ConnectionBusConfigurator().GetCurrent()?.Password;
        }

        public static string GetRabbitVirtualHost(this IConfiguration configuration)
        {
            return new ConnectionBusConfigurator().GetCurrent()?.Vhost;
        }

        public static string GetCacherPath(this IConfiguration configuration)
        {
            return AppSettingConfigurator.GetValueOrDefault("mov3r:CacherPath");
        }

        public static IConfiguration UseDataDapperFactory(this IConfiguration configuration)
        {
            if (configuration == null) return configuration;

            return configuration.UseDataFactory<DapperFactory>();
        }

        public static IConfiguration UseDataMongoFactory(this IConfiguration configuration)
        {
            if (configuration == null) return configuration;

            return configuration.UseMongoDataFactory<MongoFactory>();
        }

        public static IConfiguration UseMongoMapClass<T>(this IConfiguration configuration)
        where T : MongoMap
        {
            if (configuration == null) return configuration;

            configuration.Put(MongoMapClassKey, typeof(T).AssemblyQualifiedName);
            return configuration;
        }

        public static IConfiguration UseMongoConnectionString(this IConfiguration configuration, string connectionStringName)
        {
            if (configuration == null) return configuration;

            configuration.Put(MongoConnectionStringKey, connectionStringName);
            return configuration;
        }

        public static string GetMongoMappingTypeName(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.Get<string>(MongoMapClassKey);
        }

        public static string GetMongoConnectionStringName(this IConfiguration configuration)
        {
            if (configuration == null) return null;

            return configuration.Get<string>(MongoConnectionStringKey);
        }

        public static string GetMongoConnectionString(this IConfiguration configuration, string name = null)
        {
            name = name ?? GetMongoConnectionStringName(configuration) ?? "MongoDB";
            return ConnectionStringConfigurator.GetConnectionString(name);
        }

        public static MongoFactory GetMongoFactory(this IConfiguration configuration)
        {
            var type = configuration.Get<System.Type>(MongoDataFactoryKey);
            var mongoFactory = System.Activator.CreateInstance(type);

            return mongoFactory as MongoFactory;
        }
        public static IConfiguration UseJobLog4Net(this IConfiguration configuration)
        {
            if (configuration == null) return configuration;

            configuration.Put(Log4NetKey, Configurator.GetLogPathName("log4net.Job.Config"));
            return configuration;
        }

        public static IConfiguration UseServiceLog4Net(this IConfiguration configuration)
        {
            if (configuration == null) return configuration;

            configuration.Put(Log4NetKey, Configurator.GetLogPathName("log4net.Service.Config"));
            return configuration;
        }

    }
}