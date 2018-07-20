using HBSIS.MercadoLes.Infra.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HBSIS.MercadoLes.Integracao.SapBrf.Utils;
using HBSIS.MercadoLes.Integracao.SapBrf.Enums;

namespace HBSIS.MercadoLes.Integracao.SapBrf.XmlBuilders
{
    /// <summary>
    /// Para definir se ocorreu divergência em KM deverão ser levados em consideração 6 validações.
    /// Se todas as validações forem verdadeiras, deve sem enviada uma divergência em KM como true. Se não, como false.
    /// </summary>
    public static class DivergenciaKmOcorrencia
    {
        static DivergenciaKm _divergenciaKm = null;

        public static DivergenciaKm Processar(Rota rota,
            IEnumerable<Infra.Entities.Ocorrencia> ocorrencias,
            IEnumerable<Deslocamento> deslocamentosAlmoco,
            IEnumerable<Deslocamento> deslocamentosAbastecimento,
            IEnumerable<Deslocamento> deslocamentosPernoite,
            ParadaTratadaAnalitico parada,
            decimal vlMetaAderencia)
        {
            List<bool> subIndicadoresDivergenciaKm = new List<bool>();
            _divergenciaKm = new DivergenciaKm();

            subIndicadoresDivergenciaKm.Add(AderenciaRaioKPI(rota.Entregas, vlMetaAderencia));
            subIndicadoresDivergenciaKm.Add(InicioOuFimNoRaio(ocorrencias));
            subIndicadoresDivergenciaKm.Add(MotoristaOuSistemaFinalizouRota(rota));
            subIndicadoresDivergenciaKm.Add(DeslocamentosAlmocoPernoiteAbastecimento(rota, deslocamentosAlmoco, deslocamentosAbastecimento, deslocamentosPernoite));
            subIndicadoresDivergenciaKm.Add(AvaliarSombraCelularOuCelularDesligado(ocorrencias.ToList()));
            subIndicadoresDivergenciaKm.Add(AvaliarPNPRota(parada));

            // Se todas os sub indicadores forem verdadeiros significa que houve divergência no Km
            bool houveDivergencia = subIndicadoresDivergenciaKm.Count(indicador => indicador) == subIndicadoresDivergenciaKm.Count;
            _divergenciaKm.HouveDivergencia = Convert.ToInt16(houveDivergencia);

            return _divergenciaKm;
        }

        public static bool AderenciaRaioKPI(IEnumerable<Entrega> entregas, decimal vlMetaAderencia)
        {
            int qtdeEntregas = entregas.Count();
            decimal aderencia = qtdeEntregas > 0 ?
                entregas.Count(entrega => entrega.IdPosicaoCorreta && entrega.IdSequenciaCorreta) * 100 / qtdeEntregas : 0;

            // Houve aderência?
            return aderencia >= vlMetaAderencia;
        }

        public static bool InicioOuFimNoRaio(IEnumerable<Infra.Entities.Ocorrencia> ocorrencias)
        {
            var saidaDeposito = ocorrencias.Where(ocorrencia => ocorrencia.IdOcorrencia == (int)TipoOcorrencia.SaidaRevenda).FirstOrDefault();
            var chegadaDeposito = ocorrencias.Where(ocorrencia => ocorrencia.IdOcorrencia == (int)TipoOcorrencia.ChegadaRevenda).FirstOrDefault();

            bool inicioNoRaio = false;
            bool fimNoRaio = false;

            if (saidaDeposito != null)
                inicioNoRaio = saidaDeposito.IdPosicaoCorreta;

            if (chegadaDeposito != null)
                fimNoRaio = chegadaDeposito.IdPosicaoCorreta;

            return inicioNoRaio || fimNoRaio;
        }

        public static bool MotoristaOuSistemaFinalizouRota(Rota rota)
        {
            string nomeUsuarioFechamento = rota.NmUsuarioFechamento?.ToUpper() ?? "";

            Regex regex = new Regex("^[A-Z]{3}[0-9]{4}$");
            Match match = regex.Match(nomeUsuarioFechamento);

            return nomeUsuarioFechamento.Contains("SISTEMA") || match.Success;
        }

        public static bool DeslocamentosAlmocoPernoiteAbastecimento(Rota rota,
            IEnumerable<Deslocamento> deslocamentosAlmoco,
            IEnumerable<Deslocamento> deslocamentosAbastecimento,
            IEnumerable<Deslocamento> deslocamentosPernoite)
        {
            // Se algum deslocamento for nulo, significa que a estrutura correspondente é inconsistente
            if (deslocamentosAlmoco == null || deslocamentosAbastecimento == null || deslocamentosPernoite == null)
                return false;

            List<IEnumerable<Deslocamento>> lstDeslocamentos = new List<IEnumerable<Deslocamento>>();

            lstDeslocamentos.Add(deslocamentosAlmoco);
            lstDeslocamentos.Add(deslocamentosAbastecimento);
            lstDeslocamentos.Add(deslocamentosPernoite);

            for (int index = 0; index < 3; index++)
            {
                foreach (var deslocamento in lstDeslocamentos[index])
                {
                    bool deslocamentoValido;
                    deslocamentoValido = IsDeslocamentoValido(rota, deslocamento);

                    if (!deslocamentoValido)
                        return false;
                }
            }

            return true;
        }

        public static bool AvaliarSombraCelularOuCelularDesligado(List<Infra.Entities.Ocorrencia> ocorrenciasRota)
        {
            for (int indiceOcorrencia = 1; indiceOcorrencia < ocorrenciasRota.Count; indiceOcorrencia++)
            {
                long vlOdometro;
                long vlOdometroAnterior;
                short tipoParada;
                DateTime dtInclusao;
                DateTime dtInclusaoAnterior;

                vlOdometro = ocorrenciasRota[indiceOcorrencia].VlOdometro;
                vlOdometroAnterior = ocorrenciasRota[indiceOcorrencia - 1].VlOdometro;
                // Verifica se houve para. Se não houver, atribui 0
                tipoParada = ocorrenciasRota[indiceOcorrencia].Parada?.IdTipoParada ?? 0;

                dtInclusao = ocorrenciasRota[indiceOcorrencia].DtInclusao;
                dtInclusaoAnterior = ocorrenciasRota[indiceOcorrencia - 1].DtInclusao;

                long metrosDiffDistancia = vlOdometro - vlOdometroAnterior;
                double minutosDiffTempo = dtInclusao.Subtract(dtInclusaoAnterior).TotalMinutes;

                // Se a diferença de tempo entre uma ocorrência e outra for maior que 30 minutos
                // e a ocorrência atual não houver nenhuma parada ou a mesma possuir parada do tipo PNP.
                // Então o celular foi desligado pelo motorista
                if (minutosDiffTempo > 30 &&
                    (tipoParada == (short)TipoParada.NaoHouveParada || tipoParada == (short)TipoParada.PNP))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Este método é baseado na procedure de BI.
        /// A referida procedure possui regras específicas para avaliar as PNPs das rotas.
        /// O código abaixo reflete a conversão do T-SQL para C#. 
        /// Sendo assim, não foram levados em consideração possíveis otimizações de código.
        /// </summary>
        /// <param name="parada"></param>
        /// <returns></returns>
        public static bool AvaliarPNPRota(ParadaTratadaAnalitico parada)
        {
            if (parada == null)
                return true;

            CalculaQtdPernoiteQtdDiarias(parada);
            CalculaRealEsperadoPIM(parada);
            CalculaExcedentePIM(parada);
            CalculaExcedentePNP(parada);
            CalculaTotalReal(parada);
            AvaliaPNP(parada);

            return parada.PNPAprovado;
        }

        /// <summary>
        /// Cálculos referentes ao CTE tmp6 da procedure
        /// </summary>
        /// <param name="parada"></param>
        private static void AvaliaPNP(ParadaTratadaAnalitico parada)
        {
            #region tmp6
            if (parada.TempoEmRota - parada.TotalProgramadoReal > 0)
            {
                parada.PorcentagemPNP = (decimal)parada.TotalPNPReal / (parada.TempoEmRota - parada.TotalProgramadoReal);
                parada.PorcentagemPNPNovo = (decimal)parada.TotalPNPRealNovo / (parada.TempoEmRota - parada.TotalProgramadoReal);

                if (parada.TotalPNPReal <= 15 || parada.PorcentagemPNP <= 0.0288461538455726m)
                {
                    parada.PNP = "Aprovado";
                }

                else
                {
                    parada.PNP = "Reprovado";
                }

                if (parada.TotalPNPRealNovo <= 15 || parada.PorcentagemPNPNovo <= 0.0288461538455726m)
                {
                    parada.PNPNovo = "Aprovado";
                }

                else
                {
                    parada.PNPNovo = "Reprovado";
                }

                parada.PartidaRealizadoChave = parada.PartidaRealizada.AddDays(1).AddHours(12);
                parada.FimRealizadoChave = parada.FimRealizado.AddDays(1).AddHours(12);
            }

            // Se o tempo em rota for menor que o tempo programado, então aprova todas as PNPs
            else
            {
                parada.PNP = "Aprovado";
                parada.PNPNovo = "Aprovado";
            }
            #endregion

            #region tmp7
            parada.PNPAprovado = parada.PNP == "Aprovado";
            parada.PNPReprovado = !parada.PNPAprovado;
            #endregion
        }

        /// <summary>
        /// Cálculos referentes ao CTE tmp5 da procedure
        /// </summary>
        /// <param name="parada"></param>
        private static void CalculaTotalReal(ParadaTratadaAnalitico parada)
        {
            #region tmp5
            parada.TotalProgramadoReal = parada.TotalProgramado - parada.PNPExcedente;

            parada.TotalPNPReal = parada.PNPToleradaClienteNaRotaIsNao + parada.ErroApontamentoClienteNaRotaIsNao +
                parada.PNPComportamentalClienteNaRotaIsNao + parada.PNPExcedente;

            parada.TotalPNPRealNovo = parada.PNPToleradaClienteNaRotaIsNao + parada.ErroApontamentoClienteNaRotaIsNao +
                parada.PNPComportamentalClienteNaRotaIsNao + parada.AguardandoDescargaClienteNaRotaIsNao + parada.PNPExcedenteNovo;
            #endregion
        }

        /// <summary>
        /// Cálculos referentes ao CTE tmp4 da procedure
        /// </summary>
        /// <param name="parada"></param>
        private static void CalculaExcedentePNP(ParadaTratadaAnalitico parada)
        {
            #region tmp4
            parada.TempoEmRota = (int)parada.FimRealizado.Subtract(parada.PartidaRealizada).TotalMinutes;
            parada.PNPExcedente = parada.TotalProgramado < parada.TotalProgramadoEsperado ? 0 : parada.TotalProgramado - parada.TotalProgramadoEsperado;
            parada.PNPExcedenteNovo = parada.AlmocoExcedente + parada.AbastecimentoExcedente + parada.PernoiteExcedente;
            #endregion
        }

        /// <summary>
        /// Cálculos referentes ao CTE tmp3 da procedure
        /// </summary>
        /// <param name="parada"></param>
        private static void CalculaExcedentePIM(ParadaTratadaAnalitico parada)
        {
            #region tmp3
            parada.TotalProgramadoEsperado = parada.RefeicaoEsperada + parada.AbastecimentoEsperado + parada.PernoiteEsperada;

            parada.TotalProgramado = parada.RefeicaoNaoApontada + parada.RefeicaoRealizada +
                parada.AbastecimentoNaoApontado + parada.AbastecimentoRealizado +
                parada.RepousoRealizado + parada.PernoiteNaoApontada;

            parada.AlmocoExcedente = parada.RefeicaoReal > parada.RefeicaoEsperada ? parada.RefeicaoReal - parada.RefeicaoEsperada : 0;
            parada.AbastecimentoExcedente = parada.AbastecimentoReal > parada.AbastecimentoEsperado ? parada.AbastecimentoReal - parada.AbastecimentoEsperado : 0;
            parada.PernoiteExcedente = parada.PernoiteReal > parada.PernoiteEsperada ? parada.PernoiteReal - parada.PernoiteEsperada : 0;
            #endregion
        }

        /// <summary>
        /// Cálculos referentes ao CTE tmp2 da procedure
        /// </summary>
        /// <param name="parada"></param>
        private static void CalculaRealEsperadoPIM(ParadaTratadaAnalitico parada)
        {
            #region tmp2
            Regex unNegocioRefeicaoEsperadaX120 = new Regex("^CD_BEL|CD_MCP|TSP\\-MC|TSP\\-NT|TSP\\-SLS|CL\\-VS|FTZ|TSP\\-AR|CD_CBA|SVD|ARACROSS|TSP\\-THE$");
            Regex unNegocioRefeicaoEsperadaX90 = new Regex("^JN|TSP_PELOTA|CDIAI|CDVDA|SVD|ITACROSS$");
            Regex unNegocioRefeicaoEsperadaX70 = new Regex("^PTG|APU|SJP$");

            if (unNegocioRefeicaoEsperadaX120.IsMatch(parada.CdUnNegocio))
            {
                parada.RefeicaoEsperada = parada.QtdDiarias * 120;
            }

            else if (unNegocioRefeicaoEsperadaX90.IsMatch(parada.CdUnNegocio))
            {
                parada.RefeicaoEsperada = parada.QtdDiarias * 90;
            }

            else if (unNegocioRefeicaoEsperadaX70.IsMatch(parada.CdUnNegocio))
            {
                parada.RefeicaoEsperada = parada.QtdDiarias * 70;
            }

            else
            {
                parada.RefeicaoEsperada = parada.QtdDiarias * 60;
            }

            parada.AbastecimentoEsperado = parada.QtdDiarias * 20;
            parada.PernoiteEsperada = parada.QtdPernoites * 840;
            parada.RefeicaoReal = parada.RefeicaoNaoApontada + parada.RefeicaoRealizada;
            parada.AbastecimentoReal = parada.AbastecimentoNaoApontado + parada.AbastecimentoRealizado;
            parada.PernoiteReal = parada.RepousoRealizado + parada.PernoiteNaoApontada;
            #endregion
        }

        /// <summary>
        /// Cálculos referentes ao CTE tmp1 da procedure
        /// </summary>
        /// <param name="parada"></param>
        private static void CalculaQtdPernoiteQtdDiarias(ParadaTratadaAnalitico parada)
        {
            #region tmp1
            parada.QtdDiarias = parada.FimRealizado.Date.Subtract(parada.PartidaRealizada.Date).Days + 1;
            parada.QtdPernoites = parada.FimRealizado.Date.Subtract(parada.PartidaRealizada.Date).Days;
            #endregion
        }

        private static bool IsDeslocamentoValido(Rota rota, Deslocamento deslocamento)
        {
            // Se não houver valor cadastrado para limiar de distância deve-se retornar true para que 
            // o indicador de Deslocamentos não influencia na análise
            if (!rota.UnidadeNegocio.VlMaxDistanciaKmDesvioPIM.HasValue &&
                !rota.UnidadeNegocio.VlMaxPorcentagemDesvioPIM.HasValue)
                return true;

            // Se não houve o deslocamento desse tipo de PIM, então deve-se retornar true para que essa PIM não influencie na análise
            if (NaoHouveDeslocamentoPIM(deslocamento))
                return true;

            // O deslocamento da PIM não há dados suficientes para efetuar os cálculos. 
            // Por isso, a Divergência de KM não será remunerada
            if (!IsDeslocamentoValido(deslocamento))
                return false;

            double maxDistanciaMetroDesvioPIM = rota.UnidadeNegocio.VlMaxDistanciaKmDesvioPIM.Value * 1000;
            double maxPorcentagemDesvioPIM = rota.UnidadeNegocio.VlMaxPorcentagemDesvioPIM.Value;

            // Efetuar os cálculos necessário para descobrir o valor de desvio da rota para essa PIM
            double entregaAntesAlmoco = (deslocamento.ValorOdometroInicioPIM.Value - deslocamento.ValorOdometroEntregaAntesPIM.Value);
            double entregaDepoisAlmoco = (deslocamento.ValorOdometroEntregaDepoisPIM.Value - deslocamento.ValorOdometroFimPIM.Value);

            (double distancia, int tempo) = Coordenada.DistanciaLinhaReta(deslocamento.LocalizacaoEntregaAntesPIM.Lat,
                deslocamento.LocalizacaoEntregaAntesPIM.Lon,
                deslocamento.LocalizacaoEntregaDepoisPIM.Lat,
                deslocamento.LocalizacaoEntregaDepoisPIM.Lon);

            double desvioAlmoco = entregaAntesAlmoco + entregaDepoisAlmoco - distancia;

            // Verifica se o desvio foi menor que a distância máxima parametrizada 
            // ou se a porcentagem do desvio com relação a distância entre as entregas foi menor que a porcentagem parametrizada
            return desvioAlmoco < maxDistanciaMetroDesvioPIM || desvioAlmoco * 100 / distancia < maxPorcentagemDesvioPIM;
        }

        private static bool NaoHouveDeslocamentoPIM(Deslocamento deslocamento)
        {
            return !deslocamento.CdOcorrenciaInicioPIM.HasValue && !deslocamento.CdOcorrenciaFimPIM.HasValue;
        }

        /// <summary>
        /// Garante que o deslocamento tem as propriedades com valores para o cálculo
        /// </summary>
        /// <param name="deslocamento"></param>
        /// <returns></returns>
        private static bool IsDeslocamentoValido(Deslocamento deslocamento)
        {
            if (!deslocamento.CdOcorrenciaInicioPIM.HasValue || !deslocamento.CdOcorrenciaFimPIM.HasValue ||
                deslocamento.LocalizacaoEntregaAntesPIM.Lat == 0 || deslocamento.LocalizacaoEntregaAntesPIM.Lon == 0 ||
                deslocamento.LocalizacaoEntregaDepoisPIM.Lat == 0 || deslocamento.LocalizacaoEntregaDepoisPIM.Lon == 0 ||
                !deslocamento.ValorOdometroEntregaAntesPIM.HasValue || !deslocamento.ValorOdometroEntregaDepoisPIM.HasValue ||
                !deslocamento.ValorOdometroFimPIM.HasValue || !deslocamento.ValorOdometroInicioPIM.HasValue)
            {
                return false;
            }

            return true;
        }
    }
}
