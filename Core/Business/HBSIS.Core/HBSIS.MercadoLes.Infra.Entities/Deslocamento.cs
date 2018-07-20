using System;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.Infra.Entities
{
    [XmlInclude(typeof(DeslocamentoAlmoco))]
    [XmlInclude(typeof(DeslocamentoPernoite))]
    [XmlInclude(typeof(DeslocamentoAbastecimento))]
    [Serializable]
    public abstract class Deslocamento: Framework.Commons.Entity.IEntity<Guid>
    {
        public Guid Id { get; set; }
        public abstract int IdOcorrenciaInicio { get; }
        public abstract int IdOcorrenciaFim { get; }
        public long? CdOcorrenciaInicioPIM { get; set; }
        public long? CdOcorrenciaFimPIM { get; set; }
        public long? ValorOdometroInicioPIM { get; set; }
        public long? ValorOdometroFimPIM { get; set; }
        public long? ValorOdometroEntregaAntesPIM { get; set; }
        public long? ValorOdometroEntregaDepoisPIM { get; set; }
        public (double Lat, double Lon) LocalizacaoEntregaAntesPIM { get; set; }
        public (double Lat, double Lon) LocalizacaoEntregaDepoisPIM { get; set; }
    }
}
