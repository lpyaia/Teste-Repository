using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IUnidadeNegocioRepository<TEntity>
    {
        IEnumerable<TEntity> GetUnidadesNegocio(string cdUnidadeNegocio);

    }
}