using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class SolicitacaoDescargaRepository : DapperRepository<SolicitacaoDescarga, Guid>, ISolicitacaoDescargaRepository<SolicitacaoDescarga>
    {

        public SolicitacaoDescargaRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<SolicitacaoDescarga> GetAll()
        {
            return base.GetAll("TB_SOLICITACAO_DESCARGA");
        }
        public SolicitacaoDescarga Get(long cdEntrega)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<SolicitacaoDescarga>("SELECT * FROM OPMDM.TB_SOLICITACAO_DESCARGA WHERE CdEntrega = @CdEntrega", new { CdEntrega = cdEntrega }).FirstOrDefault();
            }
        }
    }
}
