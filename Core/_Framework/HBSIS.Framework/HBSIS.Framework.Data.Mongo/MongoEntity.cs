using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Entity;
using MongoDB.Driver;
using System;

namespace HBSIS.Framework.Data.Mongo
{
    public class MongoEntity<T> : BaseEntity<T, Guid>
        where T : class, IEntity<Guid>
    {
        public MongoEntity()
        {
            Id = Guid.NewGuid();
        }

        protected override IDataRepositoryContext GetDataContext()
        {
            return FactoryProvider.GetFactory<MongoFactory>().CurrentDataContext as IDataRepositoryContext;
        }

        public static void Delete(Guid id)
        {
            var session = (CurrentDataContext as MongoDataContext).Session;
            session.GetCollection<T>().Delete(id);
        }

        public void Update(UpdateDefinition<T> update)
        {
            var session = (CurrentDataContext as MongoDataContext).Session;
            session.GetCollection<T>().Update(this as T, update);
        }
    }
}