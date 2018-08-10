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
    public class UnidadeNegocioRepository : DapperRepository<UnidadeNegocio, Guid>, IUnidadeNegocioRepository<UnidadeNegocio>
    {

        public UnidadeNegocioRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<UnidadeNegocio> GetAll()
        {
            return base.GetAll("TB_UNIDADE_NEGOCIO");
        }

        public IEnumerable<UnidadeNegocio> GetUnidadesNegocio(string cdUnidadeNegocio)
        {
            using (var dapperConnection = AbreConexao())
            {
                var unidadesNegocio = dapperConnection
                    .Query<UnidadeNegocio>("SELECT * FROM OPMDM.TB_UNIDADE_NEGOCIO WHERE CdUnidadeNegocio = @CdUnidadeNegocio", 
                    new { CdUnidadeNegocio = cdUnidadeNegocio });

                return unidadesNegocio;
            }
        }
    }
}
