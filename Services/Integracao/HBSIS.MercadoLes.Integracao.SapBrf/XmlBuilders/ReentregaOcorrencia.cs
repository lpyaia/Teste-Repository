using HBSIS.MercadoLes.Infra.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using HBSIS.MercadoLes.Integracao.SapBrf.Enums;

namespace HBSIS.MercadoLes.Integracao.SapBrf.XmlBuilders
{
    public static class ReentregaOcorrencia
    {
        private static Reentrega _reentregaOcorrenciaXml;

        public static Reentrega Processar(Rota rota)
        {
            _reentregaOcorrenciaXml = new Reentrega();

            var entregas = rota.Entregas.Where(entrega => 
                entrega.IdDevolvida == true && 
                entrega.IdAcaoDevolucao == (short)TipoAcaoDevolucao.Reentrega
            );
            
            foreach(var entrega in entregas)
            {
                if (entrega.MotivoDevolucao.IdGeraRemuneracaoReentrega)
                {
                    _reentregaOcorrenciaXml.AdicionarItem(entrega.Cliente.CdClienteNegocio, entrega.MotivoDevolucao.CdMotivoDevolucaoNegocio);
                }
            }
            
            return _reentregaOcorrenciaXml.Itens.Count() > 0 ? _reentregaOcorrenciaXml : null;
        }
    }
}
