using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class Ocorrencia : DapperEntity<Ocorrencia>
    {
        public static string TableName
        {
            get
            {
                return "TB_OCORRENCIA";
            }
        }

        #region Propriedades
        public long CdOcorrencia { get; set; }
        public long CdRota { get; set; }
        public long CdCliente { get; set; }
        public long CdEntrega { get; set; }
        public string CdPlacaVeiculo { get; set; }
        public long CdMotivoDevolucao { get; set; }
        public long CdMensagem { get; set; }
        public DateTime DtInclusao { get; set; }
        public decimal NrLatitude { get; set; }
        public decimal NrLongitude { get; set; }
        public short IdOcorrencia { get; set; }
        public int VlVelocidade { get; set; }
        public bool IdIgnicaoLigada { get; set; }
        public long VlOdometro { get; set; }
        public bool IdPosicaoCorreta { get; set; }
        public long CdParada { get; set; }
        public long CdNotaFiscal { get; set; }
        public long CdOcorrenciaExterno { get; set; }
        public long CdMotivoEspera { get; set; }
        public long CdItemNotaFiscal { get; set; }
        public DateTime DtInclusaoSistema { get; set; }
        public string VlHdop { get; set; }
        public string VlPdop { get; set; }
        public string VlVdop { get; set; }
        public string VlGeoHeight { get; set; }
        public string VlAgeGps { get; set; }
        public string VlDgps { get; set; }
        public string NmImei { get; set; }
        public decimal QtItemDevolvidoMotorista { get; set; }
        public long CdColeta { get; set; }
        #endregion

        #region Relacionamentos
        public Parada Parada;
        #endregion
    }
}
