using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Entity;
using HBSIS.Framework.Commons.Utils;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HBSIS.Framework.Data.Mongo
{
    public class MongoRepository<TEntity, TId> : Disposable, IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        public MongoRepository(IDataContext dataContext)
        {
            DataContext = dataContext as MongoDataContext;
            Session = DataContext?.Session;
        }

        protected MongoDataContext DataContext { get; private set; }

        protected IMongoDatabase Session { get; private set; }

        public IMongoCollection<TEntity> Collection
        {
            get { return Session.GetCollection<TEntity>(); }
        }

        public IQueryable<TEntity> Query
        {
            get { return Collection.AsQueryable(); }
        }

        public Type ElementType { get { return Query.ElementType; } }

        public Expression Expression { get { return Query.Expression; } }

        public IQueryProvider Provider { get { return Query.Provider; } }

        public void Delete(TEntity entity)
        {
            Collection.Delete<TEntity, TId>(entity);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Query.GetEnumerator();
        }

        public void Insert(TEntity entity)
        {
            Collection.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            Collection.Update<TEntity, TId>(entity);
        }

        public IEnumerable<TEntity> Select()
        {
            return Collection.Find<TEntity>(null).ToEnumerable<TEntity>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IQueryable<TEntity> GetQuery(IFetchStrategy<TEntity, TId> fetchStrategy)
        {
            return Query;
        }
    }
}