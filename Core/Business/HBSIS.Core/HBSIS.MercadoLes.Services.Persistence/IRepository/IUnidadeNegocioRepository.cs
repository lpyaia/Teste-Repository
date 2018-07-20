using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IUnidadeNegocioRepository<TEntity>
    {
        IEnumerable<TEntity> GetUnidadesNegocio(string cdUnidadeNegocio);

    }
}