using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface ITransportadoraRepository<TEntity>
    {
        TEntity Get(long cdRota);
    }
}