using HBSIS.Framework.Commons.Entity;
using System;

namespace HBSIS.MercadoLes.Infra.Entities
{
    public class EnvioXml : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public long CdRota { get; set; }
        public string Xml { get; set; }
        public DateTime DtCriacao { get; set; }
        public int Tentativas { get; set; }
        public DateTime? DtUltimaTentativaReenvio { get; set; }
        public long CdRotaNegocio { get; set; }
        public bool Reenviar { get; set; }

        public EnvioXml()
        {
            DtCriacao = DateTime.Now;
            Tentativas = 0;
            Reenviar = false;
        }
    }
}
