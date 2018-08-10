using System.Collections.Generic;

namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IDepositoRepository<TEntity>
    {
        IEnumerable<TEntity> GetDepositosComGeoCoordenadas(string cdUnidadeNegocio);
    }
}