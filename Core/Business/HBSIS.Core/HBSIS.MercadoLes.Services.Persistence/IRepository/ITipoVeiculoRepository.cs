using HBSIS.MercadoLes.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface ITipoVeiculoRepository<TEntity>
    {
        IEnumerable<TipoVeiculo> GetTipoVeiculo(long cdPlacaVeiculo);
    }
}
