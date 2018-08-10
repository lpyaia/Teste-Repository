using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class TipoVeiculo : DapperEntity<TipoVeiculo>
    {
        public static string TableName
        {
            get
            {
                return "TB_TIPO_VEICULO";
            }
        }

        #region Propriedades
        public long CdTipoVeiculo { get; set; }
        public long CdExterno { get; set; }
        public string DsTipo { get; set; }
        public decimal VlPercentualMetaTemperatura { get; set; }
        public string DsGrupoRisco { get; set; }
        #endregion
    }
}
