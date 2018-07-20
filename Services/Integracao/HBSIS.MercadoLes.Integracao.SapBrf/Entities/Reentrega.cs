using System.Collections.Generic;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Entities
{
    public class Reentrega : Ocorrencia
    {
        public int Quantidade { get; set; }

        [XmlArray(ElementName = "Itens", Namespace = "NsReentrega")]
        public List<Item> Itens { get; set; }
        
        public Reentrega()
        {
            Nome = "Reentrega";
            Itens = new List<Item>();
            Codigo = "7";
        }


        public void AdicionarItem(string codigoClienteNegocio, string motivo)
        {
            Quantidade++;

            Item item = new Item()
            {
                CodigoClienteNegocio = codigoClienteNegocio,
                Motivo = motivo
            };

            Itens.Add(item);
        }


        public override string ToString()
        {
            string value = $"<{Quantidade}";

            foreach (var items in Itens)
            {
                value += $";<{items.CodigoClienteNegocio};{items.Motivo}>";
            }

            value += ">";

            return value;
        }

        public class Item
        {
            [XmlElement("CodigoCliente")]
            public string CodigoClienteNegocio { get; set; }
            public string Motivo { get; set; }
        }
    }
}
