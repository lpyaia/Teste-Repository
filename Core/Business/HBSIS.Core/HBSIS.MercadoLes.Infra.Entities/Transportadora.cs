using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra.Entities
{
    public class Transportadora: DapperEntity<Transportadora>
    {
        public long CdTransportadora { get; set; }
        public long CdEmpresa { get; set; }
        public string NmTransportadora { get; set; }
        public string NrCnpj { get; set; }
        public bool IdAtivo { get; set; }
        public string CdTransportadoraNegocio { get; set; }
    }
}
