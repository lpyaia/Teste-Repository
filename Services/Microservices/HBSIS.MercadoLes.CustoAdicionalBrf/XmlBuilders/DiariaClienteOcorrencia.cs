using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.  XmlBuilders
{
    public static class DiariaClienteOcorrencia
    {
        public static DiariaCliente Processar(Rota rota,TipoVeiculo tipoVeiculo)
        {
            List<Entrega> entregas = rota.Entregas;
            DiariaCliente diariaCliente = null;          

            entregas.ForEach(entrega =>
            {
                if (entrega.DtChegadaRealizada!=null && entrega.DtPartidaRealizada != null && entrega.UnidadeNegocio!=null)
                {
                    TimeSpan datPercorrido = (entrega.DtPartidaRealizada - entrega.DtChegadaRealizada);
                    //Comparação feita em dias
                  
                        if (datPercorrido.TotalSeconds > (entrega.UnidadeNegocio.QtHoraMaxPermanenciaCarreta * 3600) && tipoVeiculo.DsTipo.Contains("CTA"))
                        {
                            if (diariaCliente == null) diariaCliente = new DiariaCliente();
                            int qtdDiarias = ((int)datPercorrido.TotalDays) + 1;
                            diariaCliente.AdicionarItem(entrega.Cliente.CdClienteNegocio, qtdDiarias);
                        }
                    
                }               
            });
            
            return diariaCliente;
        }
    }
}
