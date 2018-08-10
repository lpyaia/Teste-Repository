using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class UnidadeNegocio : DapperEntity<UnidadeNegocio>
    {
        public static string TableName
        {
            get
            {
                return "TB_UNIDADE_NEGOCIO";
            }
        }

        public UnidadeNegocio()
        {
        }

        #region Propriedades
        public string CdUnidadeNegocio { get; set; }
        public long CdEmpresa { get; set; }
        public string NmUnidadeNegocio { get; set; }
        public string NrCnpj { get; set; }
        public bool IdAtivo { get; set; }
        public bool IdUtilizarRoteirizador { get; set; }
        public short IdNivelTratamentoDevolucao { get; set; }
        public bool IdEnviarNotificacaoClienteSms { get; set; }
        public string DsTextoPadraoSms { get; set; }
        public string DsOperacaoEnvioSms { get; set; }
        public short IdTipoTratamentoItem { get; set; }
        public bool IdUsaGPSFinalizarRota { get; set; }
        public int QtLimiteEntregaAS { get; set; }
        public bool IdAtivarRecebimentoEntrega { get; set; }
        public bool IdGerenciamentoRisco { get; set; }
        public bool IdUtilizaBkTravas { get; set; }
        public bool IdExibirSenhaMotorista { get; set; }
        public bool IdUsaDescarga { get; set; }
        public bool IdExibirDevolucaoQuantidadeMotorista { get; set; }
        public bool IdUtilizarEvidenciaPDV { get; set; }
        public string DsSenhaBkTrava { get; set; }
        public bool IdHabilitarIntegracaoFoxtrot { get; set; }
        public string DsIntegragracaoFoxtrotApiKey { get; set; }
        public bool IdHabilitarIntegracaoRoteirizadorHBSis { get; set; }
        public bool IdPermitirReversaoMobileSemAcaoCentral { get; set; }
        public string DsTokenSeguro { get; set; }
        public string DsProjetoRisco { get; set; }
        public bool IdUtilizaRisco { get; set; }
        public long CdGamification { get; set; }
        public bool IdReenvioSMS { get; set; }
        public int QtMinutosNoRaio { get; set; }
        public int QtHoraMaxPermanenciaCarreta { get; set; }
        public DateTime? DtHoraLimRestanteRetorno { get; set; }
        public decimal? QtQuilometroRestante { get; set; }
        public double? VlMaxDistanciaKmDesvioPIM { get; set; }
        public double? VlMaxPorcentagemDesvioPIM { get; set; }
        #endregion

        #region Relacionamentos
        #endregion
    }
}
