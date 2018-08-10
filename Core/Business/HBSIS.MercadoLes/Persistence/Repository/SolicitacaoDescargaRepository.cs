using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using HBSIS.MercadoLes.Persistence.IRepository;

namespace HBSIS.MercadoLes.Persistence.Repository
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
