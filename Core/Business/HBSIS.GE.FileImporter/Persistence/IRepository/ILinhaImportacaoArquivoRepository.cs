using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface ILinhaImportacaoArquivoRepository<TEntity>
    {
        IEnumerable<TEntity> GetLinhas(string nomeArquivo);
    }
}