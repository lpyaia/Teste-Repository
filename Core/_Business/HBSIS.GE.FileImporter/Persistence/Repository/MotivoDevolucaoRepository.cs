using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class MotivoDevolucaoRepository : DapperRepository<MotivoDevolucao, Guid>, IMotivoDevolucaoRepository<MotivoDevolucao>
    {

        public MotivoDevolucaoRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<MotivoDevolucao> GetAll()
        {
            return base.GetAll("TB_MOTIVO_DESCARGA");
        }

        public MotivoDevolucao Get(long cdMotivoDevolucao)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<MotivoDevolucao>("SELECT * FROM OPMDM.TB_MOTIVO_DEVOLUCAO WHERE CdMotivoDevolucao = @CdMotivoDevolucao",
                    new { CdMotivoDevolucao = cdMotivoDevolucao }).FirstOrDefault();
            }
        }
    }
}
