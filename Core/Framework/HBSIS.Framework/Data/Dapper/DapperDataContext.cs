using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Entity;
using HBSIS.Framework.Commons.Utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HBSIS.Framework.Data.Dapper
{
    public class DapperDataContext : Disposable, IDataContext, IDataRepositoryContext
    {
        public string Name { get; private set; }

        public bool IsClosed { get; private set; }

        public string ConnectionString { get; set; }

        public DapperDataContext()
        {
            ConnectionString = Configuration.Actual.GetSqlConnectionString();
        }

        public IRepository<TEntity, TId> GetRepository<TEntity, TId>()
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            return new DapperRepository<TEntity, TId>(ConnectionString);
        }

        public bool Commit()
        {
            return true;
        }
    }
}