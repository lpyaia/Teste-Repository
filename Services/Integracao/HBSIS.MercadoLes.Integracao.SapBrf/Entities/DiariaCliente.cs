using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Entities
{
    public class DiariaCliente:Ocorrencia
    {       
        [XmlElement(ElementName = "Item", Namespace = "NsDiariaCliente")]
        public List<Item> Itens { get; set; }
        public DiariaCliente()
        {
            Nome = "DiariaCliente";
            Itens = new List<Item>();
            Codigo = "6";
        }
        public class Item
        {
            [XmlElement("CodigoCliente")]
            public string CodigoCliente { get; set; }

            [XmlElement("Quantidade")]
            public int Quantidade { get; set; }
        }
        public void AdicionarItem(string codigoCliente, int quantidade)
        {
            Item item = new Item()
            {
                CodigoCliente = codigoCliente,
                Quantidade = quantidade
            };

            Itens.Add(item);
        }

        public override string ToString()
        {
            string value = $"<";

            foreach (var item in Itens)
            {
                value += $"<{item.CodigoCliente+","+item.Quantidade}>;";
            }

            value = value.TrimEnd(';');
            value += ">";

            return value;
        }
    }
}
