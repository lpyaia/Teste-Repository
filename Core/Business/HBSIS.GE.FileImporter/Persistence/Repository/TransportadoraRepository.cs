using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class TransportadoraRepository : DapperRepository<Transportadora, Guid>, ITransportadoraRepository<Transportadora>
    {

        public TransportadoraRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Transportadora> GetAll()
        {
            return base.GetAll("TB_TRANSPORTADORA");
        }

        public Transportadora Get(long cdTransportadora)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<Transportadora>("SELECT * FROM OPMDM.TB_TRANSPORTADORA WHERE CdTransportadora = @CdTransportadora", 
                    new { CdTransportadora = cdTransportadora }).FirstOrDefault();
            }
        }
    }
}
