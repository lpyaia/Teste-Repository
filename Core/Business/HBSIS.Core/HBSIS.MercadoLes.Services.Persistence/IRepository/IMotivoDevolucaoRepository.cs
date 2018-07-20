using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IMotivoDevolucaoRepository<TEntity>
    {
        TEntity Get(long cdMotivoDevolucao);
    }
}