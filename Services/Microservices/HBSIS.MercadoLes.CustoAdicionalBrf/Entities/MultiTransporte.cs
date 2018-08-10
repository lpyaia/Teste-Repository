using System.Collections.Generic;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class MultiTransporte 
    {
        [XmlElement("")]
        public List<long> NumeroRota { get; set; }
        
        public MultiTransporte()
        {
            NumeroRota = new List<long>();
        }

        public override string ToString()
        {
            string value = $"<";

            foreach (var numeroRota in NumeroRota)
            {
                value += $"<{numeroRota}>;";
            }

            value = value.TrimEnd(';');
            value += ">";

            return value;
        }
    }
}
