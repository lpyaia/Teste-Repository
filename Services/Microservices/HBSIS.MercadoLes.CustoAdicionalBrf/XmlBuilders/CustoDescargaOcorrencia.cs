using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class CustoDescargaOcorrencia
    {
        static CustoDescarga custoDescargaXml = null;

        public static CustoDescarga Processar(Rota rota)
        {
            custoDescargaXml = null;

            foreach(var entrega in rota.Entregas)
            {
                if (entrega.SolicitacaoDescarga != null && entrega.SolicitacaoDescarga.IdAprovado)
                {
                    if (custoDescargaXml == null)
                        custoDescargaXml = new CustoDescarga();

                    custoDescargaXml.AdicionarItem(entrega.Cliente.CdClienteNegocio, entrega.SolicitacaoDescarga.VlPreAprovado, entrega.SolicitacaoDescarga.VlCalculado);
                }
            }

            return custoDescargaXml;
        }
    }
}
