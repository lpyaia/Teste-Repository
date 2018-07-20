using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class MotivoDevolucao: DapperEntity<MotivoDevolucao>
    {
        public static string TableName
        {
            get
            {
                return "TB_MOTIVO_DEVOLUCAO";
            }
        }

        #region Propriedades
        public long CdMotivoDevolucao { get; set; }
        public string DsMotivoDevolucao { get; set; }
        public bool IdAtivo { get; set; }
        public string DsCodigoExterno { get; set; }
        public int VlSequenciaExibicao { get; set; }
        public long CdResponsavelDevolucao { get; set; }
        public DateTime DtExclusao { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public string CdMotivoDevolucaoNegocio { get; set; }
        public bool IdExigeEvidencia { get; set; }
        public bool IdGeraRemuneracao { get; set; }
        public bool IdGeraRemuneracaoReentrega { get; set; }
        #endregion
    }
}
