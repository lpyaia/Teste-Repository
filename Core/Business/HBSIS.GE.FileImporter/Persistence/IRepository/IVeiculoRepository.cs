using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IVeiculoRepository<TEntity>
    {
        IEnumerable<TEntity> GetVeiculos(string cdPlacaVeiculo);

    }
}
