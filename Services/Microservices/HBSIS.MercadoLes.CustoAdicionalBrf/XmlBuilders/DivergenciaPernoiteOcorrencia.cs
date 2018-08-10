using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class DivergenciaPernoiteOcorrencia
    {
        public static DivergenciaPernoite Processar(Rota rota)
        {
            DivergenciaPernoite divergenciaPernoite = new DivergenciaPernoite();
            int quantidade = DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaRealizada - 1;

            divergenciaPernoite.Quantidade = quantidade < 0 ? 0 : quantidade;

            return divergenciaPernoite;
        }
    }
}
