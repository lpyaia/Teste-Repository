using System.Collections.Generic;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class CustoDescarga: Ocorrencia
    {
        
        [XmlArray(ElementName = "Itens", Namespace = "NsCustoDescarga")]
        public List<Item> Itens { get; set; }

        public CustoDescarga()
        {
            Nome = "CustoDescarga";
            Itens = new List<Item>();
            Codigo = "04";
        }

        public void AdicionarItem(string codigoClienteNegocio, decimal valorDescargaPrevisto, decimal valorDescargaRealizado)
        {
            Item item = new Item()
            {
                CodigoClienteNegocio = codigoClienteNegocio,
                ValorDescargaPrevisto = valorDescargaPrevisto,
                ValorDescargaRealizado = valorDescargaRealizado
            };

            Itens.Add(item);
        }

        public override string ToString()
        {
            string value = "";

            foreach (var items in Itens)
            {
                value += $"<{items.CodigoClienteNegocio};{items.ValorDescargaPrevisto.ToString("F2")};{items.ValorDescargaRealizado.ToString("F2")}>";
            }

            return value;
        }

        public class Item
        {
            [XmlElement("CodigoCliente")]
            public string CodigoClienteNegocio { get; set; }
            public decimal ValorDescargaPrevisto { get; set; }
            public decimal ValorDescargaRealizado { get; set; }
        }
    }
}
