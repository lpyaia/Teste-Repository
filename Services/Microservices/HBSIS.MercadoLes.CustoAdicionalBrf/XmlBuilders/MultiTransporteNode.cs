using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System.Linq;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class MultiTransporteNode
    {
        public static MultiTransporte Processar(IEnumerable<BaldeioEntrega> baldeiosEntregaRota)
        {
            MultiTransporte multiTransporte = null;

            if (baldeiosEntregaRota.Count() > 0)
            {
                multiTransporte = new MultiTransporte();

                multiTransporte.NumeroRota.AddRange(baldeiosEntregaRota
                    .GroupBy(baldeioEntregaRota => baldeioEntregaRota.CdRotaOrigem)
                    .Select(baldeioEntregaRota => baldeioEntregaRota.First().RotaOrigem.CdRotaNegocio));
            }

            return multiTransporte;
        }
    }
}
