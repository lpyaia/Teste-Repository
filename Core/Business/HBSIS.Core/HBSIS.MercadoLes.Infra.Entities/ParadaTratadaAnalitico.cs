using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra.Entities
{
    public class ParadaTratadaAnalitico : DapperEntity<ParadaTratadaAnalitico>
    {
        public static string TableName
        {
            get
            {
                return "";
            }
        }


        #region Propriedades
        public DateTime DataRota { get; set; }
        public long CdRotaNegocio { get; set; }
        public string Regional { get; set; }
        public string CdUnNegocio { get; set; }
        public string UnNegocio { get; set; }
        public string Veiculo { get; set; }
        public long CdTransportadora { get; set; }
        public string Transportadora { get; set; }
        public long KmPrevisto { get; set; }
        public DateTime PartidaRealizada { get; set; }
        public DateTime FimRealizado { get; set; }
        public long DistanciaRealizada { get; set; }
        public string InicioNoRaio { get; set; }
        public string FimNoRaio { get; set; }
        public string UsuarioFechamento { get; set; }
        public int QuantidadeEntregaPrevista { get; set; }
        public int QuantidadeEntregaRealizada { get; set; }
        public int QuantidadeEntregaDevolvida { get; set; }
        public int QtdPNP { get; set; }
        public int QtdPNPReal { get; set; }
        public int QtdPNPDeltaKM { get; set; }
        public int RepousoRealizado { get; set; }
        public int DescansoRealizado { get; set; }
        public int RefeicaoRealizada { get; set; }
        public int EsperaParaAtendimento { get; set; }
        public int AbastecimentoRealizado { get; set; }
        public int TempoAtendimento { get; set; }
        public int AbastecimentoNaoApontado { get; set; }
        public int PNPToleradaClienteNaRotaIsNao { get; set; }
        public int ErroApontamentoClienteNaRotaIsNao { get; set; }
        public int PNPComportamentalClienteNaRotaIsNao { get; set; }
        public int OcorrenciasPNPClienteNaRotaIsNao { get; set; }
        public int PNPToleradaClienteNaRotaIsSim { get; set; }
        public int ErroApontamentoClienteNaRotaIsSim { get; set; }
        public int PNPComportamentalClienteNaRotaIsSim { get; set; }
        public int PernoiteNaoApontada { get; set; }
        public int RefeicaoNaoApontada { get; set; }
        public int AguardandoDescargaClienteNaRotaIsNao { get; set; }
        public int AguardandoDescargaClienteNaRotaIsSim { get; set; }
        #endregion

        #region Campos Calculados
        public int QtdDiarias { get; set; }
        public int QtdPernoites { get; set; }
        public int RefeicaoEsperada { get; set; }
        public int AbastecimentoEsperado { get; set; }
        public int PernoiteEsperada { get; set; }
        public int RefeicaoReal { get; set; }
        public int AbastecimentoReal { get; set; }
        public int PernoiteReal { get; set; }
        public int TotalProgramadoEsperado { get; set; }
        public int TotalProgramado { get; set; }
        public int AlmocoExcedente { get; set; }
        public int AbastecimentoExcedente { get; set; }
        public int PernoiteExcedente { get; set; }
        public int TempoEmRota { get; set; }
        public int PNPExcedente { get; set; }
        public int PNPExcedenteNovo { get; set; }
        public int TotalProgramadoReal { get; set; }
        public int TotalPNPReal { get; set; }
        public int TotalPNPRealNovo { get; set; }
        public decimal PorcentagemPNP { get; set; }
        public decimal PorcentagemPNPNovo { get; set; }
        public string PNP { get; set; }
        public string PNPNovo { get; set; }
        public DateTime PartidaRealizadoChave { get; set; }
        public DateTime FimRealizadoChave { get; set; }
        public bool PNPAprovado { get; set; }
        public bool PNPReprovado { get; set; }
        #endregion
    }
}
