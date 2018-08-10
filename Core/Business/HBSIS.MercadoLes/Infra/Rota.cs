using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class Rota: DapperEntity<Rota>
    {
        public static string TableName
        {
            get
            {
                return "TB_ROTA";
            }
        }

        public Rota()
        {
            Transportadora = new Transportadora();
            Entregas = new List<Entrega>();
            UnidadeNegocio = new UnidadeNegocio();
        }

        #region Propriedades
        public long CdRotaNegocio { get; set; }
        public string NmRota { get; set; }
        public DateTime DtRota { get; set; }
        public long CdSituacao { get; set; }
        public string CdPlacaVeiculo { get; set; }
        public long CdMotorista { get; set; }
        public long CdAjudante1 { get; set; }
        public long CdAjudante2 { get; set; }
        public DateTime DtUltimaAtividade { get; set; }
        public DateTime DtPartidaPrevista { get; set; }
        public DateTime DtChegadaPrevista { get; set; }
        public DateTime DtPartidaRealizada { get; set; }
        public DateTime DtChegadaRealizada { get; set; }
        public int QtTempoTotalRealizado { get; set; }
        public int QtTempoTotalPrevisto { get; set; }
        public int QtEntregaRealizada { get; set; }
        public int QtEntregaPrevista { get; set; }
        public int QtEntregaDevolvida { get; set; }
        public int QtCliente { get; set; }
        public int QtNotaFiscal { get; set; }
        public int QtPedido { get; set; }
        public int QtNotaFiscalDevolvida { get; set; }
        public decimal QtVolumePrevisto { get; set; }
        public decimal QtVolumeEntregue { get; set; }
        public decimal QtVolumeDevolvido { get; set; }
        public decimal VlVendaPrevisto { get; set; }
        public decimal VlVendaEntregue { get; set; }
        public decimal VlVendaDevolvido { get; set; }
        public decimal QtPesoPrevisto { get; set; }
        public decimal QtPesoEntregue { get; set; }
        public decimal QtPesoDevolvido { get; set; }
        public long VlDistanciaTotalPrevista { get; set; }
        public long VlDistanciaTotalRealizada { get; set; }
        public decimal VlHodometroInicial { get; set; }
        public decimal VlHodometroFinalPrevisto { get; set; }
        public decimal VlHodometroFinalRealizado { get; set; }
        public string DsComentario { get; set; }
        public int QtTempoExcedidoAtual { get; set; }
        public int QtTempoExcedidoTotal { get; set; }
        public int QtTempoAlmoco { get; set; }
        public DateTime DtCriacao { get; set; }
        public int IdTipoRota { get; set; }
        public bool IdCadastroManual { get; set; }
        public bool IdEnviado { get; set; }
        public int CdRotaExterno { get; set; }
        public string NmUsuarioFechamento { get; set; }
        public int IdTipoFechamento { get; set; }
        public DateTime DtRotaOriginal { get; set; }
        public long CdRota { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public bool IdRoteirizado { get; set; }
        public long CdClasse { get; set; }
        public long CdConjunto { get; set; }
        public long CdDispositivo { get; set; }
        public string DsEnderecoVeiculo { get; set; }
        public bool IdBotaoPanico { get; set; }
        public DateTime DtEnvio { get; set; }
        public long CdTransportadora { get; set; }
        public long CdTipoProdutoMomento { get; set; }
        public int QtSegundosTempoAtualRota { get; set; }
        public int QtSegundosTemperaturaCorreta { get; set; }
        public bool IdTemperaturaCorreta { get; set; }
        public bool IdTemperaturaAprovada { get; set; }
        public decimal VlEficienciaAtual { get; set; }
        public decimal VlMetaTemperaturaVeiculo { get; set; }
        public DateTime DtVeiculoVazio { get; set; }
        public bool IdExpurgada { get; set; }
        public DateTime DtSaidaDeposito { get; set; }
        public DateTime DtEntradaDeposito { get; set; }
        public bool IdImportadaFoxtrot { get; set; }
        public bool IdEmParada { get; set; }
        public string NmUsuarioResponsavelVeiculo { get; set; }
        public long CdDeposito { get; set; }
        public decimal VlTemperaturaInicial { get; set; }
        public decimal VlTemperaturaAVG3 { get; set; }
        public decimal VlTemperaturaAVG10 { get; set; }
        public bool IdSeguro { get; set; }
        public bool IdTemperaturaAtualizada { get; set; }
        public decimal QtPesoOriginal { get; set; }
        #endregion

        #region Relacionamentos
        public Transportadora Transportadora { get; set; }

        public List<Entrega> Entregas { get; set; }

        public UnidadeNegocio UnidadeNegocio { get; set; }

        public Veiculo Veiculo { get; set; }
        #endregion
    }
}
