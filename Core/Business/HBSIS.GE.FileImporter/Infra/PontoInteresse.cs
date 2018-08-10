using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class PontoInteresse : DapperEntity<PontoInteresse>
    {
        public static string TableName
        {
            get
            {
                return "TB_PONTO_INTERESSE";
            }
        }

        #region Propriedades
        public long CdPontoInteresse { get; set; }
        public string NmPonto { get; set; }
        public decimal NrLatitude { get; set; }
        public decimal NrLongitude { get; set; }
        public int QtMetrosRaio { get; set; }
        public DateTime DtCriacao { get; set; }
        public string DsPonto { get; set; }
        public string DsEndereco { get; set; }
        public string NmBairro { get; set; }
        public string NmCidade { get; set; }
        public string NmEstado { get; set; }
        public bool IdExcluido { get; set; }
        public long CdCategoriaPontoInteresse { get; set; }
        public bool IdCalculado { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public bool IdCoordenadaManual { get; set; }
        public bool IdLacrado { get; set; }
        public bool IdMonitorarTodos { get; set; }
        #endregion

        #region Relacionamentos 
        public CategoriaPontoInteresse CategoriaPontoInteresse { get; set; }
        #endregion
    }
}
