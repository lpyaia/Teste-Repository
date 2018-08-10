
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class AdicionalMeiaPernoite : Ocorrencia
    {
        public short HouveDivergencia { get; set; }

        public AdicionalMeiaPernoite()
        {
            HouveDivergencia = 0;
            Nome = "AdicionalMeiaPernoite";
            Codigo = "9";
        }

        public override string ToString()
        {
            return $"<{HouveDivergencia}>";
        }
    }
}
