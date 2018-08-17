using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System.Linq;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class MultiTransporteNode
    {
        public static MultiTransporte Processar(IEnumerable<BaldeioEntrega> baldeiosEntregaRota, Rota rota)
        {
            MultiTransporte multiTransporte = new MultiTransporte();
            multiTransporte.NumeroRota.Add(rota.CdRotaNegocio);

            if (baldeiosEntregaRota.Count() > 0)
            {
                multiTransporte.NumeroRota.AddRange(baldeiosEntregaRota
                    .GroupBy(baldeioEntregaRota => baldeioEntregaRota.CdRotaOrigem)
                    .Select(baldeioEntregaRota => baldeioEntregaRota.First().RotaOrigem.CdRotaNegocio));
            }

            return multiTransporte;
        }
    }
}
