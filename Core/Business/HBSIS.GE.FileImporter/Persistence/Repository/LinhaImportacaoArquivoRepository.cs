using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;
using Dapper.Contrib.Extensions;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class LinhaImportacaoArquivoRepository : DapperRepository<LinhaImportacaoArquivo, Guid>, ILinhaImportacaoArquivoRepository<LinhaImportacaoArquivo>
    {
        public LinhaImportacaoArquivoRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<LinhaImportacaoArquivo> GetAll()
        {
            return base.GetAll("OPMDM.TB_LINHA_IMPORTACAO_ARQUIVO");
        }

        public IEnumerable<LinhaImportacaoArquivo> GetLinhas(string nomeArquivo)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<LinhaImportacaoArquivo>(@"
                    SELECT * FROM OPMDM.TB_LINHA_IMPORTACAO_ARQUIVO 
                    WHERE DsNomeArquivo = @DsNomeArquivo",
                    new { DsNomeArquivo = nomeArquivo });
            }
        }
    }
}
