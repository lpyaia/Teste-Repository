using HBSIS.GE.FileImporter.Services.Commons.Enums;

namespace HBSIS.GE.FileImporter.Services.Commons.Config
{
    /// <summary>
    /// Configuração de parâmetros
    /// </summary>
    public class Param
    {
        public Param()
        {
            TempoAnomaliaVelocidade = 15;
            TempoAnomaliaTemperatura = 15;
            TempoAnomaliaTempoExcedido = 15;
            TempoAnomaliaAgendamento = 15;
            TempoAnomaliaSinal = 15;
            TempoAnomaliaExcessoJornada = 15;
            MetaExecucaoColetaUnidadeNegocio = 240;
            MetaExecucaoEntregaUnidadeNegocio = 240;
            MetaExecucaoColetaNaoUnidadeNegocio = 240;
            MetaExecucaoEntregaNaoUnidadeNegocio = 240;
            MetaAdiantamentoChegadaUnidadeNegocio = 240;
            MetaAtrasoChegadaUnidadeNegocio = 240;
            MetaAdiantamentoChegadaNaoUnidadeNegocio = 240;
            MetaAtrasoChegadaNaoUnidadeNegocio = 240;
            EficienciaTemperaturaVerde = 80;
            EficienciaTemperaturaAmarelo = 60;
            EficienciaSinalVerde = 80;
            EficienciaSinalAmarelo = 60;
            CalculoETAKmDia = 600;
            CalculoETAKmHora = 60;
            MedidaRaioParada = 500;
            MetaExecucaoNF = 240;
            VelocidadeMaxima = 80;
            VelocidadeMaximaInteracao = 30;
            MedidaRaioPnp = 200;
            TempoPnp = 15;
            TempoExcessoJornadaContinuo = 330;
            TempoExcessoJornadaDiario = 720;
            IntervaloJornadaContinuo = 360;
            IntervaloJornadaDiario = 1440;
            ProvedorMapas = TipoProvedorMapas.Leaflet;
            TempoSemEspelhamento = 24;
            VersaoAplicativo = "1.0.0";
            LimiteCaracteresOrigemAeroporto = 0;
            CalculoAnomaliaTempoExcedido = TipoCalculoTempoExcedido.DataMaior;
            TipoSaidaCliente = TipoSaidaCliente.Automatica;
            TipoSaidaColeta = TipoSaidaColeta.ComFaturamento;
            IntervaloSinal = 60;
            TipoSaida = TipoSaida.PosicaoOuIntegracao;
            TempoRemocao = 1;
        }

        /// <summary>
        /// Define o provedor de mapas utilizado no frontend.
        /// </summary>
        public TipoProvedorMapas ProvedorMapas { get; set; }//TODO: Configurator, remover após!

        /// <summary>
        /// Tempo da verificação da anomalia de velocidade.
        /// </summary>
        public int TempoAnomaliaVelocidade { get; set; }

        /// <summary>
        /// Tempo da verificação da anomalia de temperatura.
        /// </summary>
        public int TempoAnomaliaTemperatura { get; set; }

        /// <summary>
        /// Tempo da verificação da anomalia de tempo excedido.
        /// </summary>
        public int TempoAnomaliaTempoExcedido { get; set; }

        /// <summary>
        /// Tempo da verificação da anomalia de Agendamento.
        /// </summary>
        public int TempoAnomaliaAgendamento { get; set; }

        /// <summary>
        /// Tempo da verificação da anomalia de sinal.
        /// </summary>
        public int TempoAnomaliaSinal { get; set; }

        /// <summary>
        /// Tempo da verificação da anomalia de Execesso Jornada.
        /// </summary>
        public int TempoAnomaliaExcessoJornada { get; set; }

        /// <summary>
        /// Meta para execução da operação da coleta em unidades de negócio.
        /// </summary>
        public int MetaExecucaoColetaUnidadeNegocio { get; set; }

        /// <summary>
        /// Meta para execução da operação da entrega em unidades de negócio.
        /// </summary>
        public int MetaExecucaoEntregaUnidadeNegocio { get; set; }

        /// <summary>
        /// Meta para execução da operação da coleta em paradas que não são unidades de negócio.
        /// </summary>
        public int MetaExecucaoColetaNaoUnidadeNegocio { get; set; }

        /// <summary>
        /// Meta para execução da operação da entrega em paradas que não são unidades de negócio.
        /// </summary>
        public int MetaExecucaoEntregaNaoUnidadeNegocio { get; set; }

        /// <summary>
        /// Meta de adiantamento para chegada em unidades de negócio.
        /// </summary>
        public int MetaAdiantamentoChegadaUnidadeNegocio { get; set; }

        /// <summary>
        /// Meta de atraso para chegada em unidades de negócio.
        /// </summary>
        public int MetaAtrasoChegadaUnidadeNegocio { get; set; }

        /// <summary>
        /// Meta de adiantamento para chegada em paradas que não são unidades de negócio.
        /// </summary>
        public int MetaAdiantamentoChegadaNaoUnidadeNegocio { get; set; }

        /// <summary>
        /// Meta de atraso para chegada em paradas que não são unidades de negócio.
        /// </summary>
        public int MetaAtrasoChegadaNaoUnidadeNegocio { get; set; }

        /// <summary>
        /// Define a eficiência da temperatura mínima.
        /// </summary>
        public int EficienciaTemperaturaVerde { get; set; }

        /// <summary>
        /// Define a eficiência da temperatura mínima.
        /// </summary>
        public int EficienciaTemperaturaAmarelo { get; set; }

        /// <summary>
        /// Define o tempo pro sinal ficar com o status verde.
        /// </summary>
        public int EficienciaSinalVerde { get; set; }

        /// <summary>
        /// Define o tempo pro sinal ficar com o status amarelo.
        /// </summary>
        public int EficienciaSinalAmarelo { get; set; }

        /// <summary>
        /// Define o KM que o veículo percorre por Hora.
        /// </summary>
        public int CalculoETAKmHora { get; set; }

        /// <summary>
        /// Define o KM que o veículo percorre por Dia.
        /// </summary>
        public int CalculoETAKmDia { get; set; }

        /// <summary>
        /// Define o tipo de calculo de tempo excedido.
        /// </summary>
        public TipoCalculoTempoExcedido CalculoAnomaliaTempoExcedido { get; set; }

        /// <summary>
        /// Define se a saída do cliente irá ser automática ou manual.
        /// </summary>
        public TipoSaidaCliente TipoSaidaCliente { get; set; }

        /// <summary>
        /// Define se a saída da coleta será com faturamento, sem faturamento ou com faturamento e início automático da viagem.
        /// </summary>
        public TipoSaidaColeta TipoSaidaColeta { get; set; }

        /// <summary>
        /// Define a medida (metros) do Raio de uma Parada.
        /// </summary>
        public int MedidaRaioParada { get; set; }

        /// <summary>
        /// Meta para execução para Troca de Nota Fiscal.
        /// </summary>
        public int MetaExecucaoNF { get; set; }

        /// <summary>
        /// Velocidade Máxima Permitida.
        /// </summary>
        public int VelocidadeMaxima { get; set; }

        /// <summary>
        /// Velocidade máxima para interação com o aplicativo.
        /// </summary>
        public int VelocidadeMaximaInteracao { get; set; }

        /// <summary>
        /// Define a medida (metros) do Raio de uma PNP.
        /// </summary>
        public int MedidaRaioPnp { get; set; }

        /// <summary>
        /// Define o tempo de permanencia (minutos) no Raio para ser gerado uma PNP
        /// </summary>
        public int TempoPnp { get; set; }

        /// <summary>
        /// Define o tempo contínuo (minutos) de Excesso de Jornada
        /// </summary>
        public int TempoExcessoJornadaContinuo { get; set; }

        /// <summary>
        /// Define o tempo diário (minutos) de Excesso de Jornada
        /// </summary>
        public int TempoExcessoJornadaDiario { get; set; }

        public int IntervaloJornadaContinuo { get; set; }

        public int IntervaloJornadaDiario { get; set; }

        /// <summary>
        /// Define a temperatura minima default
        /// </summary>
        public int? TemperaturaMinima { get; set; }

        /// <summary>
        /// Define a temperatura maxima default
        /// </summary>
        public int? TemperaturaMaxima { get; set; }

        /// <summary>
        /// Define o nome de usuário utilizado em alguns provedores de mapas.
        /// Por exemplo, o Client ID do Google Maps.
        /// </summary>
        public string UsuarioMapas { get; set; }//TODO: Configurator, remover após!

        /// <summary>
        /// Define o tempo sem posição para bloqueio de espelhamento
        /// </summary>
        public int TempoSemEspelhamento { get; set; }

        /// <summary>
        /// Versão mínima requerida para utilizar o aplicativo.
        /// </summary>
        public string VersaoAplicativo { get; set; }

        /// <summary>
        /// Limite de caracteres para valores na coluna origem do painel de aeroporto.
        /// O valor 0 (zero) indica que não há limitação de caracteres.
        /// </summary>
        public int LimiteCaracteresOrigemAeroporto { get; set; }

        /// <summary>
        /// Define o intervalo em minutos para o cálculo da eficiência de sinal  .
        /// </summary>
        public int IntervaloSinal { get; set; }

        /// <summary>
        /// Define o tempo de remoção de rotas.
        /// </summary>
        public int TempoRemocao { get; set; }

        /// <summary>
        /// Define o tipo de saída da parada.
        /// </summary>
        public TipoSaida TipoSaida { get; set; }
    }
}