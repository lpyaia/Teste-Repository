using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class SolicitacaoDescarga: DapperEntity<SolicitacaoDescarga>
    {
        public static string TableName
        {
            get
            {
                return "TB_SOLICITACAO_DESCARGA";
            }
        }

        #region Propriedades
        public long CdEntrega { get; set; }
        public DateTime DtSolicitacao { get; set; }
        public DateTime DtSolicitacaoMobile { get; set; }
        public decimal VlPreAprovado { get; set; }
        public decimal VlCalculado { get; set; }
        public decimal VlAprovado { get; set; }
        public int CdAprovador { get; set; }
        public DateTime DtAprovacao { get; set; }
        public bool IdAprovado { get; set; }
        public int IdSenha { get; set; }
        public int IdContraSenha { get; set; }
        public bool IdTemDevolucao { get; set; }
        public bool IdInconsistenciaValor { get; set; }
        #endregion
    }
}
