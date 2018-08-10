using HBSIS.Framework.Commons.Entity;
using System;
using System.Linq;

namespace HBSIS.Framework.Commons.Data
{
    public interface IQueryableRepository<TEntity, TId> : IQueryable<TEntity>
        where TEntity : IEntity<TId>
        where TId : IEquatable<TId>
    {
        IQueryable<TEntity> GetQuery(IFetchStrategy<TEntity, TId> fetchStrategy);
    }
}