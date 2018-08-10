using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities
{
    public class Cliente: DapperEntity<Cliente>
    {
        public static string TableName
        {
            get
            {
                return "TB_CLIENTE";
            }
        }

        #region Propriedades
        public long CdCliente { get; set; }
        public long CdPontoInteresse { get; set; }
        public string NmCliente { get; set; }
        public string DsEndereco { get; set; }
        public string NmBairro { get; set; }
        public string NmCidade { get; set; }
        public string NmEstado { get; set; }
        public string NmContato { get; set; }
        public string NrTelefoneContato { get; set; }
        public string NrCelularContato { get; set; }
        public bool IdAtivo { get; set; }
        public int IdTipoLocal { get; set; }
        public DateTime DtInicioExpediente { get; set; }
        public DateTime DtFimExpediente { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public bool IdEnviarNotificacaoSms { get; set; }
        public long CdUsuario { get; set; }
        public bool IdUtilizaAplicativoAcompanhamento { get; set; }
        public DateTime DtInicioExpedienteAlternativo { get; set; }
        public DateTime DtFimExpedienteAlternativo { get; set; }
        public decimal VlAprovado { get; set; }
        public int IdTipoValorDescarga { get; set; }
        public string CdClienteNegocio { get; set; }
        public int IdTipoCliente { get; set; }
        public string NmDocumento { get; set; }
        public string DsObservacao { get; set; }
        public int IdDiasRestricao { get; set; }
        public DateTime TempoAtendimentoEntrega { get; set; }
        public DateTime TempoTratativaDevEntrega { get; set; }
        public int IdPotencialCliente { get; set; }
        #endregion
    }
}
