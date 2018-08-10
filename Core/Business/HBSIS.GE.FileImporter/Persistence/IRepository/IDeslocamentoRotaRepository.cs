using HBSIS.GE.FileImporter.Infra.Entities;
using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IDeslocamentoRotaRepository<TEntity>
    {
        List<Deslocamento> GetDeslocamentosPIM(long cdRota);
    }
}