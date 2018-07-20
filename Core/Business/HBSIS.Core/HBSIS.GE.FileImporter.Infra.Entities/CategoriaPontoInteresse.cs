using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class CategoriaPontoInteresse : DapperEntity<CategoriaPontoInteresse>
    {
        public static string TableName
        {
            get
            {
                return "TB_CATEGORIA_PONTO_INTERESSE";
            }
        }

        #region Propriedades
        public long CdCategoriaPontoInteresse { get; set; }
        public string DsCategoriaPontoInteresse { get; set; }
        public bool IdInternoSistema { get; set; }
        public string DsImagemPin { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        #endregion
    }
}
