using HBSIS.Framework.Commons.Entity;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HBSIS.Framework.Data.Mongo
{
    public static class MongoHelper
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase session)
        {
            var attrs = typeof(T).GetCustomAttributes(typeof(MongoCollectionNameAttribute), false).OfType<MongoCollectionNameAttribute>().FirstOrDefault();
            var collectionName = attrs?.Name ?? typeof(T).Name;

            return session.GetCollection<T>(collectionName);
        }

        #region GetQueryable

        public static IQueryable<T> GetQueryable<T>(this IMongoDatabase session)
        {
            return session.GetCollection<T>().AsQueryable();
        }

        public static IQueryable<T> GetQueryable<T>(this IMongoDatabase session, string name)
        {
            return session.GetCollection<T>(name).AsQueryable();
        }

        #endregion GetQueryable

        #region GetOrDefault

        public static T GetOrDefault<T>(this IMongoCollection<T> session, Expression<Func<T, bool>> filter)
        {
            return session.Find(filter).FirstOrDefault();
        }

        public static T GetOrDefault<T>(this IMongoCollection<T> session, FilterDefinition<T> filter)
        {
            return session.Find(filter).FirstOrDefault();
        }

        #endregion GetOrDefault

        #region Insert

        public static void Insert<T>(this IMongoCollection<T> collection, T value)
             where T : class
        {
            collection.InsertOneAsync(value).Wait();
        }

        #endregion Insert

        #region InsertOrUpdate

        public static void InsertOrUpdate<T>(this IMongoCollection<T> collection, T value)
            where T : class, IEntity<Guid>
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            InsertOrUpdate(collection, filter, value);
        }

        public static void InsertOrUpdate<T>(this IMongoCollection<T> collection, FilterDefinition<T> filter, T value)
            where T : class
        {
            collection.FindOneAndReplaceAsync(filter, value, new FindOneAndReplaceOptions<T, T>() { IsUpsert = true }).Wait();
        }

        public static void InsertOrUpdate<T>(this IMongoCollection<T> collection, T value, UpdateDefinition<T> update)
            where T : class, IEntity<Guid>
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            InsertOrUpdate(collection, filter, update);
        }

        public static void InsertOrUpdate<T>(this IMongoCollection<T> collection, FilterDefinition<T> filter, UpdateDefinition<T> update)
            where T : class
        {
            collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<T, T>() { IsUpsert = true }).Wait();
        }

        #endregion InsertOrUpdate

        #region Update

        public static void Update<T>(this IMongoCollection<T> collection, T value)
            where T : class, IEntity<Guid>
        {
            Update<T, Guid>(collection, value);
        }

        public static void Update<TEntity, TId>(this IMongoCollection<TEntity> collection, TEntity value)
          where TEntity : class, IEntity<TId>
          where TId : IEquatable<TId>
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, value.Id);
            collection.FindOneAndReplaceAsync(filter, value).Wait();
        }

        public static void Update<T>(this IMongoCollection<T> collection, T value, UpdateDefinition<T> update)
            where T : class, IEntity<Guid>
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            Update(collection, filter, update);
        }

        public static void Update<T>(this IMongoCollection<T> collection, FilterDefinition<T> filter, UpdateDefinition<T> update)
            where T : class
        {
            collection.FindOneAndUpdateAsync(filter, update).Wait();
        }

        #endregion Update

        #region Delete

        public static void Delete<T>(this IMongoCollection<T> collection, T value)
            where T : class, IEntity<Guid>
        {
            Delete<T, Guid>(collection, value);
        }

        public static void Delete<TEntity, TId>(this IMongoCollection<TEntity> collection, TEntity value)
          where TEntity : class, IEntity<TId>
          where TId : IEquatable<TId>
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, value.Id);
            Delete(collection, filter);
        }

        public static void Delete<T>(this IMongoCollection<T> collection, Guid Id)
         where T : class, IEntity<Guid>
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, Id);
            Delete(collection, filter);
        }

        public static void Delete<T>(this IMongoCollection<T> collection, FilterDefinition<T> filter)
            where T : class
        {
            collection.FindOneAndDeleteAsync(filter).Wait();
        }

        #endregion Delete
    }
}