using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IBaldeioEntregaRepository<TEntity>
    {
        IEnumerable<TEntity> GetBaldeiosMultiTransporteByRotaDestino(long cdRota);

        TEntity GetByRota(long cdRota);
    }
}