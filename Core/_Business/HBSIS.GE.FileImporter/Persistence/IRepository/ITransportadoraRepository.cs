using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface ITransportadoraRepository<TEntity>
    {
        TEntity Get(long cdRota);
    }
}