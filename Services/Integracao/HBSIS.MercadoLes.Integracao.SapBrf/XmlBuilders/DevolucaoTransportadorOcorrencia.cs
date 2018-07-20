using HBSIS.MercadoLes.Infra.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Integracao.SapBrf.XmlBuilders
{
    public static class DevolucaoTransportadorOcorrencia
    {
        public static DevolucaoTransportador Processar(Rota rota)
        {
            DevolucaoTransportador devolucaoTransportador = null;
            var devolucoesRemuneradas = rota.Entregas.Where(_ => _.MotivoDevolucao?.IdGeraRemuneracao == true);
            
            if(devolucoesRemuneradas.Count() > 0)
            {
                devolucaoTransportador = new DevolucaoTransportador();
            }

            foreach(var devolucaoRemunerada in devolucoesRemuneradas)
            {
                devolucaoTransportador.AdicionarItem(devolucaoRemunerada.Cliente.CdClienteNegocio, devolucaoRemunerada.MotivoDevolucao.CdMotivoDevolucaoNegocio);
            }

            return devolucaoTransportador;
        }
    }
}
