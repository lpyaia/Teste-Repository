using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities
{
    public class ClienteCelular : DapperEntity<ClienteCelular>
    {
        public static string TableName
        {
            get
            {
                return "TB_CLIENTE_CELULAR";
            }
        }

        public long CdCelular { get; set; }
        public long CdCliente { get; set; }
        public string NrCelular { get; set; }
        public DateTime DtCriacao { get; set; }
        public DateTime DtExclusao { get; set; }
        public bool IdExcluido { get; set; }
        public string NmContato { get; set; }
        public bool IdEnviarSMS { get; set; }
    }
}
