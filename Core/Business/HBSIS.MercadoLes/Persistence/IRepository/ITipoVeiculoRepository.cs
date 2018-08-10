using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface ITipoVeiculoRepository<TEntity>
    {
        IEnumerable<TipoVeiculo> GetTipoVeiculo(long cdPlacaVeiculo);
    }
}
