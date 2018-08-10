using System.Collections.Generic;

namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface ITransportadoraRepository<TEntity>
    {
        TEntity Get(long cdRota);
    }
}