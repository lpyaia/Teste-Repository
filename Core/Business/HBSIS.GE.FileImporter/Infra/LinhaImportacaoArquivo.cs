using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities
{
    public class LinhaImportacaoArquivo: DapperEntity<LinhaImportacaoArquivo>
    {
        public static string TableName
        {
            get
            {
                return "OPMDM.TB_LINHA_IMPORTACAO_ARQUIVO";
            }
        }

        #region Propriedades
        public long CdLinhaImportacao { get; set; }
        public int IdTipoImportacao { get; set; }
        public string DsNomeArquivo { get; set; }
        public int VlNumeroLinha { get; set; }
        public int VlTotalLinhasArquivo { get; set; }
        public DateTime DtInclusao { get; set; }
        public string DsConteudoLinha { get; set; }
        public bool IdErroImportacao { get; set; }
        #endregion
    }
}
