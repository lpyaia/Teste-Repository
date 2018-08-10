using HBSIS.Framework.Commons.Entity;
using System;

namespace HBSIS.Framework.Commons.Data
{
    public interface IDataContext : IDisposable
    {
        string Name { get; }

        bool IsClosed { get; }
    }

    public interface IDataContext<T> : IDataContext
    {
        T Session { get; }
    }

    public interface IDataRepositoryContext : IDataContext
    {
        IRepository<TEntity, TId> GetRepository<TEntity, TId>()
            where TEntity : class, IEntity<TId>
            where TId : IEquatable<TId>;
    }
}