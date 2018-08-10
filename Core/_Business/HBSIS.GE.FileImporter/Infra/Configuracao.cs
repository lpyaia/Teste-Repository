using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class Configuracao : DapperEntity<Configuracao>
    {
        public static string TableName
        {
            get
            {
                return "TB_CONFIGURACAO";
            }
        }


        #region Propriedades
        public long CdConfiguracao { get; set; }
        public decimal VlConversaoVolume { get; set; }
        public decimal QtMinutosParadaDesligado { get; set; }
        public int QtMetrosRaioParadaDesligado { get; set; }
        public decimal QtMinutosParadaLigado { get; set; }
        public int QtMetrosRaioParadaLigado { get; set; }
        public int QtMetrosRaioCliente { get; set; }
        public int QtMinutosAlmocoMotorista { get; set; }
        public string DsServidorEmail { get; set; }
        public int NrPortaEmail { get; set; }
        public string DsLoginEmail { get; set; }
        public string DsSenhaEmail { get; set; }
        public string DsRemetenteEmail { get; set; }
        public decimal QtMetrosRaioClienteProximoPNP { get; set; }
        public decimal QtMetrosRaioCalibrarCoordenadas { get; set; }
        public int QtSemanasCalibrarCoordenadas { get; set; }
        public string DsAvisoFechamentoForaDoRaio { get; set; }
        public string DsUrlPedidos { get; set; }
        public bool IdUtilizarBaldeio { get; set; }
        public bool IdExibirMenuParadasMobile { get; set; }
        public bool IdExibirMenuDetalhesRotaMobile { get; set; }
        public bool IdExigirGpsInicioRotaMobile { get; set; }
        public bool IdExibirSenhaMotorista { get; set; }
        public int QtMinutosParadoDepositoDesligar { get; set; }
        public bool IdFinalizarRotaAutomaticamenteDeposito { get; set; }
        public string DsChavePrivadaApiMapa { get; set; }
        public string DsClientIdApiMapa { get; set; }
        public decimal QtMinutosParadaEntrega { get; set; }
        public bool IdAtivarAssistenteNavegacao { get; set; }
        public string DsCaminhoEvidencias { get; set; }
        public bool IdUtilizarTrocaEquipe { get; set; }
        public bool IdContabilizarPermanenciaDeposito { get; set; }
        public bool IdReimportarItens { get; set; }
        public int QtPrefetchFila { get; set; }
        public int QtInstanciasFila { get; set; }
        public int QtSegundosEmProcessamentoFila { get; set; }
        public string DsEnderecoServidor { get; set; }
        public int IdPorta { get; set; }
        public string DsUsuario { get; set; }
        public string DsSenha { get; set; }
        public int IdRegraSequenciaCorreta { get; set; }
        public bool IdAtivarModuloAlertasPerigo { get; set; }
        public long RaioAntecedenteAlertaPerigo { get; set; }
        public long QtdMetrosBuscaAlerta { get; set; }
        public long TempoAtualizacaoAlertas { get; set; }
        public long QtdKmPerimetroAlerta { get; set; }
        public bool IdPermitirAlterarSequenciaEntrega { get; set; }
        public int QtDigitosRoadshowAntes { get; set; }
        public int QtDigitosRoadshowDepois { get; set; }
        public bool IdFecharMapasAutomaticoAoReceberNovo { get; set; }
        public string DsUrlServidorWeb { get; set; }
        public bool IdNovoVeiculoUtilizaFila { get; set; }
        public bool IdRegistrarLogMobile { get; set; }
        public string DsDiretorioLogMobile { get; set; }
        public int QtDiasValidadeLogMobile { get; set; }
        public bool IdPermitirReversaoSemAcaoCentral { get; set; }
        public string DsCaminhoFotoRecebimentoMobile { get; set; }
        public string DsEnderecoSistema { get; set; }
        public bool IdEnableSsl { get; set; }
        public bool IdUtilizarTokenMobile { get; set; }
        public bool IdUtilizarFotoMobile { get; set; }
        public bool IdUtilizarAssinaturaMobile { get; set; }
        public bool IdUtilizarRecebimentoFila { get; set; }
        public bool IdFinalizarRotaBaldeio { get; set; }
        public bool SomarKmPrevistoEntregaRota { get; set; }
        public bool IdManterPNPTratada { get; set; }
        public bool IdLacrarPdv { get; set; }
        public bool IdExibirParadasDescanso { get; set; }
        public bool IdExibirParadasEspera { get; set; }
        public bool IdExibirPromptFinalizarCDD { get; set; }
        public int QtTempoPromptFinalizarCDD { get; set; }
        public bool IdUtilizarCpfSenhaMobile { get; set; }
        public bool IdContabilizarTempoAtendimentoAposApontamento { get; set; }
        public short VlVelocidadeAlerta { get; set; }
        public bool IdExibirPanicoMobile { get; set; }
        public int QtMinutosAlertaEntregaDemorada { get; set; }
        public bool IdUsaPedagio { get; set; }
        public bool IdExibirTokenMobile { get; set; }
        public bool IdExibirFotoMobile { get; set; }
        public bool IdExibirAssinaturaMobile { get; set; }
        public string DsCaminhoFotoAnomalia { get; set; }
        public string DsCaminhoUploadIntegracao { get; set; }
        public bool IdConsolidarRotas { get; set; }
        public string DsEnderecoIntegracaoRoteirizadorHBSis { get; set; }
        public string DsUsuarioIntegracaoRoteirizadorHBSis { get; set; }
        public string DsSenhaIntegracaoRoteirizadorHBSis { get; set; }
        public string DsVirtualHost { get; set; }
        public bool IdUtilizaCluster { get; set; }
        public bool IdFecharMapasVeltec { get; set; }
        public bool IdConsolidarRotaSemPrevisao { get; set; }
        public bool IdRealizarBaldeioAutomatico { get; set; }
        public bool IdUtilizarRaioClienteTempoAtendimento { get; set; }
        public bool IdExibirBotaoSairMobile { get; set; }
        public string DsVirtualHostRoteirizadorHBSis { get; set; }
        public string DsNodesRoteirizadorHBSis { get; set; }
        public bool IdImportarColetas { get; set; }
        public bool IdRoterizarRotaFechadaAndamento { get; set; }
        public string DsWsIdioma { get; set; }
        public string DsWsUsuario { get; set; }
        public string DsWsSenha { get; set; }
        public bool IdTravarInicioRota { get; set; }
        public string DsHoraLiberacaoRota { get; set; }
        public bool IdArmazenamentoExternoEvidencias { get; set; }
        public bool IdValidarRiscoRota { get; set; }
        public bool IdTempoEsperaCliente { get; set; }
        public string DsDiretorioImportacaoArquivo { get; set; }
        #endregion
    }
}
