using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IEntregaRepository<TEntity>
    {
        IEnumerable<TEntity> GetByRota(long cdRota);
    }
}