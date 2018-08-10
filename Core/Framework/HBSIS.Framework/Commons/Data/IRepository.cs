using HBSIS.Framework.Commons.Entity;
using System;
using System.Collections.Generic;

namespace HBSIS.Framework.Commons.Data
{
    public interface IRepository<TEntity, TId> : IQueryableRepository<TEntity, TId>, IDisposable
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}