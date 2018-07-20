using Dapper;
using HBSIS.Framework.Data.Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class TipoVeiculoRepository : DapperRepository<TipoVeiculo, Guid>, ITipoVeiculoRepository<TipoVeiculo>
    {
        public TipoVeiculoRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }
        public IEnumerable<TipoVeiculo> GetTipoVeiculo(long cdTipoVeiculo)
        {
            using (var dapperConnection = AbreConexao())
            {
                var tipoVeiculos = dapperConnection
                    .Query<TipoVeiculo>("SELECT * FROM OPMDM.TB_TIPO_VEICULO WHERE CdTipoVeiculo = @CdTipoVeiculo",
                    new { CdTipoVeiculo = cdTipoVeiculo });

                return tipoVeiculos;
            }
        }
    }
}
