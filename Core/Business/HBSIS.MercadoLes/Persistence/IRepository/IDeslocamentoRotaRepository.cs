using HBSIS.MercadoLes.Infra;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IDeslocamentoRotaRepository<TEntity>
    {
        List<Deslocamento> GetDeslocamentosPIM(long cdRota);
    }
}