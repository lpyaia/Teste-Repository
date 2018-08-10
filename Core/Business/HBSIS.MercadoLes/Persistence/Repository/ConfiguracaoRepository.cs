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
    public class ConfiguracaoRepository : DapperRepository<Configuracao, Guid>, IConfiguracaoRepository<Configuracao>
    {

        public ConfiguracaoRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Configuracao> GetAll()
        {
            return base.GetAll("TB_CONFIGURACAO");
        }
    }
}
