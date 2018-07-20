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
