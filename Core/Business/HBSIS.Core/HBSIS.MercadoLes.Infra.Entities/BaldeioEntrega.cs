using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra.Entities
{
    public class BaldeioEntrega : DapperEntity<BaldeioEntrega>
    {
        public BaldeioEntrega()
        {
            RotaOrigem = new Rota();
        }

        public static string TableName
        {
            get
            {
                return "TB_BALDEIO_ENTREGA";
            }
        }

        #region Propriedades
        public long CdBaldeioEntrega { get; set; }
        public long CdEntrega { get; set; }
        public long CdRotaOrigem { get; set; }
        public long CdRotaDestino { get; set; }
        public DateTime DtOperacao { get; set; }
        public long CdUsuario { get; set; }
        public short IdTipoSituacaoEntrega { get; set; }
        public bool IdBaldeioEfetuadoOrigem { get; set; }
        public bool IdBaldeioEfetuadoDestino { get; set; }
        public DateTime DtBaldeioEfetuadoOrigem { get; set; }
        public DateTime DtBaldeioEfetuadoDestino { get; set; }
        #endregion

        #region Relacionamentos
        public Rota RotaOrigem { get; set; }
        #endregion
    }
}
