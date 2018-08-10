using HBSIS.Framework.Commons.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HBSIS.Framework.Commons.Data
{
    public interface IFetchStrategy<TEntity, TId>
        where TEntity : IEntity<TId>
        where TId : IEquatable<TId>
    {
        IEnumerable<Expression<Func<TEntity, object>>> IncludePaths { get; }

        IFetchStrategy<TEntity, TId> ThenInclude(Expression<Func<TEntity, object>> path);

        IQueryable<TEntity> GetQuery();
    }
}