using HBSIS.MercadoLes.Infra.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;
using System.Linq;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Integracao.SapBrf.XmlBuilders
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
