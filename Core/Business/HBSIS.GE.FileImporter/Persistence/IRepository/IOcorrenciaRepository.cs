using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IOcorrenciaRepository<TEntity>
    {
        IEnumerable<TEntity> GetOcorrenciasCompletas(long cdRota);
        IEnumerable<TEntity> GetOcorrencias(long cdRota, string descricaoCategoriaPontoInteresse);
        IEnumerable<TEntity> GetOcorrencias(long cdRota);

    }
}