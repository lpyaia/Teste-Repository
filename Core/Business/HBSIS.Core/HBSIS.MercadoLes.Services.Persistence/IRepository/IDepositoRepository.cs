using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IDepositoRepository<TEntity>
    {
        IEnumerable<TEntity> GetDepositosComGeoCoordenadas(string cdUnidadeNegocio);
    }
}