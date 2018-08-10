using HBSIS.Framework.Commons.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HBSIS.Framework.Commons.Data
{
    public class GenericFetchStrategy<TEntity, TId> : IFetchStrategy<TEntity, TId>
        where TEntity : IEntity<TId>
        where TId : struct, IEquatable<TId>
    {
        private readonly IList<Expression<Func<TEntity, object>>> properties;
        private readonly IQueryableRepository<TEntity, TId> repository;

        public GenericFetchStrategy(IQueryableRepository<TEntity, TId> repository)
        {
            properties = new List<Expression<Func<TEntity, object>>>();
            this.repository = repository;
        }

        public IEnumerable<Expression<Func<TEntity, object>>> IncludePaths { get { return properties; } }

        public IQueryable<TEntity> GetQuery()
        {
            return repository.GetQuery(this);
        }

        public IFetchStrategy<TEntity, TId> ThenInclude(Expression<Func<TEntity, object>> path)
        {
            properties.Add(path);
            return this;
        }
    }
}