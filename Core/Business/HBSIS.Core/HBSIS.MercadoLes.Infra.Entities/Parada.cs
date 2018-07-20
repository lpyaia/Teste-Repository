using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra.Entities
{
    public class Parada : DapperEntity<Parada>
    {
        public static string TableName
        {
            get
            {
                return "TB_PARADA";
            }
        }

        #region Propriedades
        public long CdParada { get; set; }
        public long CdPontoInteresse { get; set; }
        public string CdPlacaVeiculo { get; set; }
        public short IdTipoParada { get; set; }
        public long QtMinutosParado { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int CdMotivoParada { get; set; }
        public string DsJustificativa { get; set; }
        public bool IdJustificado { get; set; }
        public bool IdMonitorado { get; set; }
        public int QtMinutosParadoLigado { get; set; }
        public int QtMinutosParadoDesligado { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public bool IdGeradaPorBaldeio { get; set; }
        #endregion

        #region Relacionamentos
        public PontoInteresse PontoInteresse { get; set; }
        #endregion
    }
}
