using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Entity;
using System;

namespace HBSIS.Framework.Data.Dapper
{
    public class DapperEntity<T> : BaseEntity<T, Guid>
        where T : class, IEntity<Guid>
    {
        public DapperEntity()
        {
            Id = Guid.NewGuid();
        }

        protected override IDataRepositoryContext GetDataContext()
        {
            return FactoryProvider.GetFactory<DapperFactory>().CurrentDataContext as IDataRepositoryContext;
        }

        public static void Delete(Guid id)
        {

        }

        public void Update(T update)
        {

        }
    }
}