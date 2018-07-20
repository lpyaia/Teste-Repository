using System.Xml.Serialization;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Entities
{
    [XmlInclude(typeof(DivergenciaKm))]
    [XmlInclude(typeof(DivergenciaDiaria))]
    [XmlInclude(typeof(DivergenciaPernoite))]
    [XmlInclude(typeof(CustoDescarga))]
    [XmlInclude(typeof(DevolucaoTransportador))]
    [XmlInclude(typeof(Reentrega))]
    [XmlInclude(typeof(AdicionalBalsa))]
    [XmlInclude(typeof(AdicionalMeiaPernoite))]
    [XmlInclude(typeof(DiariaCliente))]
    public class Ocorrencia
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }

        private bool _exibir;

        public Ocorrencia()
        {
            _exibir = true;
        }

        public void SetExibirOcorrenciaNoXml(bool value)
        {
            _exibir = value;
        }
        
        public bool ExibirOcorrenciaNoXml()
        {
            return _exibir;
        }
    }
}
