using HBSIS.MercadoLes.Infra.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Integracao.SapBrf.XmlBuilders
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
