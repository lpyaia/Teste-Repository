using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IMotivoDevolucaoRepository<TEntity>
    {
        TEntity Get(long cdMotivoDevolucao);
    }
}