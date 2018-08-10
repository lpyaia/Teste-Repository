using System.Collections.Generic;

namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IMotivoDevolucaoRepository<TEntity>
    {
        TEntity Get(long cdMotivoDevolucao);
    }
}