using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class DivergenciaDiariaOcorrencia
    {
        public static DivergenciaDiaria Processar(Rota rota)
        {
            DivergenciaDiaria divergenciaDiaria = new DivergenciaDiaria();
            TimeSpan timeRealizados = rota.DtChegadaRealizada.Date - rota.DtPartidaRealizada.Date;
            TimeSpan timePrevistos = rota.DtChegadaPrevista.Date - rota.DtPartidaPrevista.Date;
            
            // Contabiliza o dia em que ele saiu
            int diasRealizados = timeRealizados.Days + 1;
            int diasPrevistos = timePrevistos.Days + 1;

            if (rota.DtChegadaRealizada.Hour < 9)
                diasRealizados--;

            if (rota.DtChegadaPrevista.Hour < 9)
                diasPrevistos--;

            // Não envia o indicador ao WS quando os dias realizados estiverem dentro do previsto
            if (diasRealizados <= diasPrevistos)
                divergenciaDiaria.SetExibirOcorrenciaNoXml(false);

            divergenciaDiaria.QuantidadeDiariaRealizada = diasRealizados < 0 ? 0 : diasRealizados;
            divergenciaDiaria.QuantidadeDiariaPrevista = diasPrevistos < 0 ? 0 : diasPrevistos;

            return divergenciaDiaria;
        }
    }
}
