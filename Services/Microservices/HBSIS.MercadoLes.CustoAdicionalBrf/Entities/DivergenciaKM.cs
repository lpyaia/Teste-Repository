
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class DivergenciaKm: Ocorrencia
    {
        public decimal KMPrevisto { get; set; }
        public decimal KMRealizado { get; set; }
        public bool HouveDivergencia { get; set; }

        public DivergenciaKm()
        {
            Nome = "DivergenciaKM";
            Codigo = "3";
        }

        public override string ToString()
        {
            return $"<{KMPrevisto};{KMRealizado}>";
        }
    }
}
