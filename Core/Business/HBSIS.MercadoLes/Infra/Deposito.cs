using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class Deposito : DapperEntity<Deposito>
    {
        public static string TableName
        {
            get
            {
                return "TB_DEPOSITO";
            }
        }

        public Deposito()
        {
            PontoInteresse = new PontoInteresse();
        }

        #region Propriedades
        public long CdDeposito { get; set; }
        public long CdPontoInteresse { get; set; }
        public string NmDeposito { get; set; }
        public bool IdAtivo { get; set; }
        public long CdEmpresa { get; set; }
        public string CdUnidadeNegocio { get; set; }
        public long CdExterno { get; set; }
        #endregion

        #region Relacionamentos
        public PontoInteresse PontoInteresse { get; set; }
        #endregion
    }
}
