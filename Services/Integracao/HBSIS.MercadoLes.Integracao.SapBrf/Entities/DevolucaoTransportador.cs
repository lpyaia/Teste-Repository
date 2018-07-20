using System.Collections.Generic;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Entities
{
    public class DevolucaoTransportador : Ocorrencia
    {
        public int Quantidade { get; set; }

        [XmlElement(ElementName = "Item", Namespace = "NsDevolucaoTransportador")]
        public List<Item> Itens { get; set; }
    
        public DevolucaoTransportador()
        {
            Nome = "DevolucaoTransportador";
            Itens = new List<Item>();
            Codigo = "5";
        }


        public void AdicionarItem(string codigoClienteNegocio, string motivo)
        {
            Item item = new Item()
            {
                CodigoClienteNegocio = codigoClienteNegocio,
                Motivo = motivo
            };

            Itens.Add(item);
            Quantidade++;
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
