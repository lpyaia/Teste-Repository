using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IDepositoRepository<TEntity>
    {
        IEnumerable<TEntity> GetDepositosComGeoCoordenadas(string cdUnidadeNegocio);
    }
}