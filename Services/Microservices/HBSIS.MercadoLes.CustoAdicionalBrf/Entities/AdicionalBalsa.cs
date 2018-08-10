using System.Collections.Generic;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class AdicionalBalsa : Ocorrencia
    {
        public int Quantidade { get; set; }

        [XmlElement(Namespace = "NsAdicionalBalsa")]
        public Balsas Itens { get; set; }
        
        public AdicionalBalsa()
        {
            Nome = "AdicionalBalsa";
            Itens = new Balsas();
            Codigo = "8";
        }

        public void AdicionarItem(string nomeBalsa)
        {
            Itens.NomeBalsa.Add(nomeBalsa);
            Quantidade++;
        }

        public override string ToString()
        {
            string value = $"<{Quantidade}";

            foreach (var nomeBalsa in Itens.NomeBalsa)
            {
                value += $";<{nomeBalsa}>";
            }

            value += ">";

            return value;
        }
        
        public class Balsas
        {
            public Balsas()
            {
                NomeBalsa = new List<string>();
            }

            [XmlElement("")]
            public List<string> NomeBalsa { get; set; }
        }
    }
}
