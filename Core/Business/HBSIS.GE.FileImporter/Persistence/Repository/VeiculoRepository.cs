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
    public class VeiculoRepository : DapperRepository<Veiculo, Guid>, IVeiculoRepository<Veiculo>
    {

        public VeiculoRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Veiculo> GetAll()
        {
            return base.GetAll("TB_VEICULO");
        }
      
        public IEnumerable<Veiculo> GetVeiculos(string cdPlacaVeiculo)
        {
            using (var dapperConnection = AbreConexao())
            {
                var veiculos = dapperConnection
                    .Query<Veiculo>("SELECT * FROM OPMDM.TB_VEICULO WHERE CdPlacaVeiculo = @CdPlacaVeiculo",
                    new { CdPlacaVeiculo = cdPlacaVeiculo });                             

                return veiculos;
            }
        }

     
    }
}
