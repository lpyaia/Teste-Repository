using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HBSIS.Framework.Data.Dapper
{
    public class DapperFactory : IFactory
    {
        private static readonly object _lock = new object();
        private const string DataContextCurrentKey = "FWK_DAPPER_DATA_CONTEXT_CURRENT";

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
                context = new DapperDataContext();

                _contexts[DataContextCurrentKey] = context;
                _contexts[keyContext] = context;
            }

            return context;
        }

        public static IDbConnection CreateConnection(string connectionStringName)
        {
            var connection = Configuration.Actual.GetSqlConnectionString(connectionStringName);
            var ret = new SqlConnection(connection);

            if (ret == null)
                throw new HBDataException("Dapper connection string not defined.");

            return ret;
        }
    }
}