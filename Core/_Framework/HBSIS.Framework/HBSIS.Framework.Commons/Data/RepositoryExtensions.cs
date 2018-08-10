using HBSIS.Framework.Commons.Entity;
using System;
using System.Linq.Expressions;

namespace HBSIS.Framework.Commons.Data
{
    public static class RepositoryExtensions
    {
        public static IFetchStrategy<TEntity, TId> Include<TEntity, TId>(this IQueryableRepository<TEntity, TId> repository, Expression<Func<TEntity, object>> selector)
            where TEntity : IEntity<TId>
            where TId : struct, IEquatable<TId>
        {
            var fs = new GenericFetchStrategy<TEntity, TId>(repository);
            return fs.ThenInclude(selector);
        }
    }
}