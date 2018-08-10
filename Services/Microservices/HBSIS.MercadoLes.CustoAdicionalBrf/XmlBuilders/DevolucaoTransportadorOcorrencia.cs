using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System.Linq;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
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
