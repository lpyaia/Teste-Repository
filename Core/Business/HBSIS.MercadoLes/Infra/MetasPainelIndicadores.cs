using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class MetasPainelIndicadores : DapperEntity<MetasPainelIndicadores>
    {
        public static string TableName
        {
            get
            {
                return "TB_METAS_PAINEL_INDICADORES";
            }
        }

        public MetasPainelIndicadores() { }

        #region Propriedades
        public int CdMetasPainelIndicadores { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public decimal VlMetaHomologacao { get; set; }
        public decimal VlMetaEfetividade { get; set; }
        public decimal VlMetaTemperaturaAprovada { get; set; }
        public decimal VlMetaAderencia { get; set; }
        public decimal VlMetaOTIF { get; set; }
        public decimal VlMetaReversao { get; set; }
        public decimal VlMetaRetorno { get; set; }
        public decimal VlMetaReentrega { get; set; }
        #endregion
    }
}
