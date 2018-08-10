using System.Collections.Generic;

namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IEntregaRepository<TEntity>
    {
        IEnumerable<TEntity> GetByRota(long cdRota);
    }
}