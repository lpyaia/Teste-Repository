using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Entity;
using HBSIS.Framework.Commons.Utils;
using MongoDB.Driver;
using System;

namespace HBSIS.Framework.Data.Mongo
{
    public class MongoDataContext : Disposable, IDataContext<IMongoDatabase>, IDataRepositoryContext
    {
        public MongoDataContext(string name, IMongoDatabase session)
        {
            Name = name;
            Session = session;
        }

        public string Name { get; private set; }

        public bool IsClosed { get; private set; }

        public IMongoDatabase Session { get; private set; }

        public IRepository<TEntity, TId> GetRepository<TEntity, TId>()
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>
        {
            return new MongoRepository<TEntity, TId>(this);
        }

        public bool Commit()
        {
            return true;
        }
    }
}