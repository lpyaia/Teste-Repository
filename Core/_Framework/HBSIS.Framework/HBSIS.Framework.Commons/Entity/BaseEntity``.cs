using HBSIS.Framework.Data;
using HBSIS.Framework.Commons.Exceptions;
using System;
using System.Linq;
using HBSIS.Framework.Commons.Data;

namespace HBSIS.Framework.Commons.Entity
{
    public abstract class BaseEntity<TEntity, TId> : BaseEntity<TId>, IActiveEntity
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        protected IDataRepositoryContext DataContext
        {
            get
            {
                var context = GetDataContext();

                if (context == null)
                    throw new HBDataException("DataContext not implement IDataRepositoryContext");

                return context;
            }
        }

        protected IRepository<TEntity, TId> Repository
        {
            get { return DataContext.GetRepository<TEntity, TId>(); }
        }

        protected static IDataRepositoryContext CurrentDataContext
        {
            get
            {
                return (Activator.CreateInstance<TEntity>() as BaseEntity<TEntity, TId>).DataContext;
            }
        }

        public static TEntity Get(TId id)
        {
            return CurrentDataContext.GetRepository<TEntity, TId>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public static IQueryableRepository<TEntity, TId> List
        {
            get
            {
                return CurrentDataContext.GetRepository<TEntity, TId>();
            }
        }

        protected virtual IDataRepositoryContext GetDataContext()
        {
            return FactoryProvider.CurrentFactory.CurrentDataContext as IDataRepositoryContext;
        }

        public virtual void Insert()
        {
            Repository.Insert(this as TEntity);
        }

        public virtual void Update()
        {
            Repository.Update(this as TEntity);
        }

        public virtual void Delete()
        {
            Repository.Delete(this as TEntity);
        }

        public void InsertOrUpdate()
        {
            var any = Repository.Any(x => x.Id.Equals(this.Id));

            if (any)
                Update();
            else
                Insert();
        }
    }
}