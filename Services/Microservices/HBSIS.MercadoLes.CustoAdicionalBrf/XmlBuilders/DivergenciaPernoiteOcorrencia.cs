using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System.Collections.Generic;
using System.Linq;
using HBSIS.MercadoLes.CustoAdicionalBrf.Enums;
using HBSIS.MercadoLes.CustoAdicionalBrf.Utils;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class DivergenciaPernoiteOcorrencia
    {
        public static DivergenciaPernoite Processar(Rota rota, IEnumerable<Infra.Ocorrencia> ocorrenciasRepouso, IEnumerable<Deposito> depositos)
        {
            DivergenciaPernoite divergenciaPernoite = new DivergenciaPernoite();
            int quantidadeRealizada = DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaRealizada - 1;
            int quantidadePrevista = DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaPrevista - 1;

            var pernoites = ObtemPernoites(ocorrenciasRepouso);

            foreach(var pernoite in pernoites)
            {
                decimal latInicioRepouso = pernoite.GetCoordenadasInicioRepouso().lat;
                decimal lonInicioRepouso = pernoite.GetCoordenadasInicioRepouso().lon;

                decimal latFimRepouso = pernoite.GetCoordenadasFimRepouso().lat;
                decimal lonFimRepouso = pernoite.GetCoordenadasFimRepouso().lon;

                foreach (var deposito in depositos)
                {
                    if(deposito.PontoInteresse != null)
                    {
                        int metrosRaioDeposito = deposito.PontoInteresse.QtMetrosRaio;

                        decimal latDeposito = deposito.PontoInteresse.NrLatitude;
                        decimal lonDeposito = deposito.PontoInteresse.NrLongitude;

                        (double distInicioRepousoAoDeposito, _) = Coordenada.DistanciaLinhaReta(latInicioRepouso, lonInicioRepouso, latDeposito, lonDeposito);
                        (double distFimRepousoAoDeposito, _) = Coordenada.DistanciaLinhaReta(latFimRepouso, lonFimRepouso, latDeposito, lonDeposito);

                        // Se o motorista estiver dentro do raio de algum dos CDs
                        // (ou seja, dormiu em um CD),
                        // então é desconsiderada a pernoite referente a esse repouso.
                        if(distInicioRepousoAoDeposito <= metrosRaioDeposito || distFimRepousoAoDeposito <= metrosRaioDeposito)
                        {
                            quantidadeRealizada--;
                            break;
                        }
                    }
                }
            }

            divergenciaPernoite.QuantidadeRealizada = quantidadeRealizada < 0 ? 0 : quantidadeRealizada;
            divergenciaPernoite.QuantidadePrevista = quantidadePrevista < 0 ? 0 : quantidadePrevista;

            // Não envia o indicador ao WS quando os dias realizados estiverem dentro do previsto
            if (divergenciaPernoite.QuantidadeRealizada <= divergenciaPernoite.QuantidadePrevista)
                divergenciaPernoite.SetExibirOcorrenciaNoXml(false);

            return divergenciaPernoite;
        }

        private static List<Pernoite> ObtemPernoites(IEnumerable<Infra.Ocorrencia> ocorrencias)
        {
            int indexInicioRepouso = 0;
            int indexFimRepouso = 1;
            List<Pernoite> pernoites = new List<Pernoite>();

            var ocorrenciasRepouso = ocorrencias.Where(ocorrencia =>
                    ocorrencia.IdOcorrencia == (short)TipoOcorrencia.InicioRepouso ||
                    ocorrencia.IdOcorrencia == (short)TipoOcorrencia.FimRepouso)
                .OrderBy(ocorrencia => ocorrencia.DtInclusao)
                .ToList();

            while(indexInicioRepouso < ocorrenciasRepouso.Count() - 1)
            {
                if(ocorrenciasRepouso[indexInicioRepouso].IdOcorrencia == (short)TipoOcorrencia.InicioRepouso &&
                    ocorrenciasRepouso[indexFimRepouso].IdOcorrencia == (short)TipoOcorrencia.FimRepouso)
                {
                    Pernoite pernoite = new Pernoite(ocorrenciasRepouso[indexInicioRepouso], ocorrenciasRepouso[indexFimRepouso]);
                    pernoites.Add(pernoite);

                    indexInicioRepouso += 2;
                }

                else
                {
                    indexInicioRepouso++;
                }

                indexFimRepouso = indexInicioRepouso + 1;
            }

            return pernoites;
        }
    }
}

