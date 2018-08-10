using HBSIS.GE.FileImporter.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface ITipoVeiculoRepository<TEntity>
    {
        IEnumerable<TipoVeiculo> GetTipoVeiculo(long cdPlacaVeiculo);
    }
}
