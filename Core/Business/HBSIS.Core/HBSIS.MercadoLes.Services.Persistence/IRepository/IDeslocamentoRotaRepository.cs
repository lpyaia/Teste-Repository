using HBSIS.MercadoLes.Infra.Entities;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IDeslocamentoRotaRepository<TEntity>
    {
        List<Deslocamento> GetDeslocamentosPIM(long cdRota);
    }
}