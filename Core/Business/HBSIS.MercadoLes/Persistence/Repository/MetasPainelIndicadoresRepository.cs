using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.MercadoLes.Persistence.IRepository;

namespace HBSIS.MercadoLes.Persistence.Repository
{
    public class MetasPainelIndicadoresRepository : DapperRepository<MetasPainelIndicadores, Guid>, IMetasPainelIndicadoresRepository<MetasPainelIndicadores>
    {

        public MetasPainelIndicadoresRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<MetasPainelIndicadores> GetAll()
        {
            return base.GetAll("TB_METAS_PAINEL_INDICADORES");
        }
        

        public MetasPainelIndicadores GetByUnidadeNegocio(string cdUnidadeNegocio)
        {
            Dictionary<long, Rota> rota = new Dictionary<long, Rota>();

            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                MetasPainelIndicadores result = dapperConnection.Query<MetasPainelIndicadores>(
                    @"SELECT * FROM OPMDM.TB_METAS_PAINEL_INDICADORES WHERE CdUnidadeNegocio = @CdUnidadeNegocio",
                    new { CdUnidadeNegocio = cdUnidadeNegocio }).FirstOrDefault();

                return result;
            }
        }
    }
}
