
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class DivergenciaKm: Ocorrencia
    {
        public short HouveDivergencia { get; set; }

        public DivergenciaKm()
        {
            HouveDivergencia = 0;
            Nome = "DivergenciaKM";
            Codigo = "3";
        }

        public override string ToString()
        {
            return $"<{HouveDivergencia}>";
        }
    }
}
