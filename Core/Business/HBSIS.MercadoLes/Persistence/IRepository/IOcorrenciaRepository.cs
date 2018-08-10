using System.Collections.Generic;

namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IOcorrenciaRepository<TEntity>
    {
        IEnumerable<TEntity> GetOcorrenciasCompletas(long cdRota);
        IEnumerable<TEntity> GetOcorrencias(long cdRota, string descricaoCategoriaPontoInteresse);
        IEnumerable<TEntity> GetOcorrencias(long cdRota);

    }
}