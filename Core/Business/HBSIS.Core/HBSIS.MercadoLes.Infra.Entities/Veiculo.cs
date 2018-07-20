using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra.Entities
{
    public class Veiculo : DapperEntity<Veiculo>
    {
        public static string TableName
        {
            get
            {
                return "TB_VEICULO";
            }
        }

        #region Propriedades
        public string CdPlacaVeiculo { get; set; }
        public Boolean IdRastreado { get; set; }
        public Boolean IdAtivo { get; set; }
        public int NrOdometro { get; set; }
        public DateTime DtAtualizacaoodometro { get; set; }
        public DateTime DtSincronizacaoOdometro { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public Int16 CdDispositivoRastreador { get; set; }
        public Int16 CdDispositivoTecladoLogistico { get; set; }
        public long CdTransportadora { get; set; }
        public long CdExterno { get; set; }
        public Boolean IdUtilizaFila { get; set; }
        public long CdTipoVeiculo { get; set; }
        public DateTime DtUltimaIntegracaoRastreador { get; set; }
        public Int16 CdDispositivoTemperatura { get; set; }
        public long CdUsuarioResponsavel { get; set; }
        public string DsIdentificadorExterno { get; set; }
        public DateTime DtAlteracao { get; set; }
        public long CdUsuarioAlteracao { get; set; }
        public DateTime DtUltimaAtualizacaoTemperatura { get; set; }
        public string DsObservacao { get; set; }
        #endregion

        #region Relacionamentos
        public TipoVeiculo TipoVeiculo { get; set; }
        #endregion


    }
}
