using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace HBSIS.Framework.Data.Mongo
{
    public class MongoFactory : IFactory
    {
        private static readonly object _lock = new object();
        private const string DataContextCurrentKey = "FWK_MONGO_DATA_CONTEXT_CURRENT";

        private static Dictionary<string, object> _contexts = new Dictionary<string, object>();

        public IDataContext GetDataContext(string connectionStringName = null)
        {
            lock (_lock)
            {
                return GetOrCreateDataContext(connectionStringName);
            }
        }

        public IDataContext CurrentDataContext
        {
            get
            {
                lock (_lock)
                {
                    var context = _contexts.ContainsKey(DataContextCurrentKey) ? _contexts[DataContextCurrentKey] as IDataContext : null;

                    if (context == null || context.IsClosed)
                    {
                        context = GetOrCreateDataContext();
                    }

                    return context;
                }
            }
        }

        private IDataContext GetOrCreateDataContext(string connectionStringName = null)
        {
            var name = connectionStringName ?? "default";
            var keyContext = $"{DataContextCurrentKey}_{name}";

            var context = _contexts.ContainsKey(keyContext) ? _contexts[keyContext] as IDataContext : null;

            if (context == null)
            {
                context = new MongoDataContext(name, CreateSession(connectionStringName));

                _contexts[DataContextCurrentKey] = context;
                _contexts[keyContext] = context;
            }

            return context;
        }

        public static IMongoDatabase Session
        {
            get
            {
                lock (_lock)
                {
                    return (FactoryProvider.GetFactory<MongoFactory>().CurrentDataContext as MongoDataContext).Session;
                }
            }
        }

        public static IMongoDatabase CreateSession(string connectionStringName)
        {
            var connection = Configuration.Actual.GetMongoConnectionString(connectionStringName);
            var ret = CreateDatabase(connection);

            if (ret == null)
                throw new HBDataException("MongoDatabase not defined.");

            return ret;
        }

        #region Create

        public static IMongoDatabase CreateDatabase(string connectionString)
        {
            MongoMapping();

            var mongoUrl = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(MongoClientSettings.FromUrl(mongoUrl.ToMongoUrl()));
            var ret = client.GetDatabase(mongoUrl.DatabaseName);

            return ret;
        }

        private static void MongoMapping()
        {
            var mapTypeName = Configuration.Actual.GetMongoMappingTypeName();

            if (mapTypeName == null)
            {
                new MongoMap().Map();
            }
            else
            {
                var mapType = Type.GetType(mapTypeName);

                if (mapType != null)
                {
                    var mapper = Activator.CreateInstance(mapType) as MongoMap;
                    mapper?.Map();
                }
            }
        }

        #endregion Create
    }
}