using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;
using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class Entrega : DapperEntity<Entrega>
    {
        public static string TableName
        {
            get
            {
                return "TB_ENTREGA";
            }
        }
        
        #region Propriedades
        public long CdEntrega { get; set; }
        public long CdRota { get; set; }
        public long CdCliente { get; set; }
        public long CdDeposito { get; set; }
        public long CdMotivoDevolucao { get; set; }
        public int IdSequencia { get; set; }
        public bool IdEntregaRealizada { get; set; }
        public DateTime DtEntrega { get; set; }
        public DateTime DtPartidaPrevista { get; set; }
        public DateTime DtPartidaRealizada { get; set; }
        public DateTime DtChegadaPrevista { get; set; }
        public DateTime DtChegadaRealizada { get; set; }
        public long VlDistanciaPrevista { get; set; }
        public long VlDistanciaRealizada { get; set; }
        public int QtTempoTotalPrevisto { get; set; }
        public int QtTempoTotalRealizado { get; set; }
        public int QtTempoDirigindoPrevisto { get; set; }
        public int QtTempoDirigindoRealizado { get; set; }
        public int QtTempoAtendimentoPrevisto { get; set; }
        public int QtTempoAtendimentoRealizado { get; set; }
        public int QtTempoAtendimentoExcedido { get; set; }
        public decimal QtPesoPrevisto { get; set; }
        public decimal QtPesoEntregue { get; set; }
        public decimal QtVolumePrevisto { get; set; }
        public decimal QtVolumeEntregue { get; set; }
        public decimal VlVendaPrevisto { get; set; }
        public decimal VlVendaEntregue { get; set; }
        public bool IdRevertida { get; set; }
        public int IdSequenciaRealizada { get; set; }
        public bool IdSequenciaCorreta { get; set; }
        public short IdAcaoDevolucao { get; set; }
        public long CdMotivoDevolucaoCA { get; set; }
        public bool IdDevolvida { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public bool IdPosicaoCorreta { get; set; }
        public int QtNotaFiscalPrevista { get; set; }
        public int QtNotaFiscalRealizada { get; set; }
        public int QtNotaFiscalDevolvida { get; set; }
        public int QtItemNotaFiscalPrevisto { get; set; }
        public int QtItemNotaFiscalRealizado { get; set; }
        public int QtItemNotaFiscalDevolvido { get; set; }
        public DateTime DtRealizada { get; set; }
        public decimal VlDevolucaoValor { get; set; }
        public DateTime DtPrimeiroApontamento { get; set; }
        public string DsPrateleira { get; set; }
        public int IdSequenciaOriginal { get; set; }
        public string DsSenhaDevolucao { get; set; }
        public decimal NrLatitudeDispersao { get; set; }
        public decimal NrLongitudeDispersao { get; set; }
        public Guid CdConfirmacaoEntrega { get; set; }
        public bool IdTokenConfirmacaoEnviado { get; set; }
        public DateTime DtEnviadoGerenciamentoRisco { get; set; }
        public int CdMonitoramentoBrasilRisk { get; set; }
        public string CdComandoPontoDiarioSascar { get; set; }
        public bool IdPdvLacrado { get; set; }
        public bool IdBaldeioAutomatico { get; set; }
        #endregion

        #region Relacionamentos
        public SolicitacaoDescarga SolicitacaoDescarga { get; set; }
        public MotivoDevolucao MotivoDevolucao { get; set; }
        public Cliente Cliente { get; set; }
        public UnidadeNegocio UnidadeNegocio { get; set; }
        #endregion
    }
}
