using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System.Linq;
using HBSIS.MercadoLes.CustoAdicionalBrf.Enums;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
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
