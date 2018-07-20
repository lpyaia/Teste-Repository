using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra.Entities;
//using HBSIS.MercadoLes.Integracao.Cutoff.Entities;
using HBSIS.MercadoLes.Services.Commons;
using HBSIS.MercadoLes.Services.Commons.Base.Service;
using HBSIS.MercadoLes.Services.Commons.Enums;
using HBSIS.MercadoLes.Services.Commons.Properties;
using HBSIS.MercadoLes.Services.Messages.Message;
using System;
using System.Linq;
using Dapper;
using HBSIS.MercadoLes.Services.Persistence;
using HBSIS.MercadoLes.Integracao.SapBrf.XmlBuilders;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.Utils;
using System.IO;
using HBSIS.MercadoLes.Integracao.SapBrf.Enums;
using HBSIS.Framework.Commons.Result;
using HBSIS.Framework.Commons.Helper;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Service
{
    public class IntegracaoSapBrfService : BusinessService<IntegracaoSapBrfMessage>
    {
        private const int _tentativas = 10;
        private const int _tempoEspera = 30 * 1000;
        private PersistenceDataContext _dbContext;
        private IIntegracaoSapBrfConfigurator _configurator;
        private IIntegracaoSapBrfIntegrator _integrator;

        public IntegracaoSapBrfService(IIntegracaoSapBrfConfigurator configurator)
        {
            _dbContext = new PersistenceDataContext();
            _integrator = new IntegracaoSapBrfIntegrator(configurator);
            _configurator = configurator;

            var cdRota = 1154411;


            var rota = _dbContext.RotaRepository.GetRotaIndicadoresFluxoLES(cdRota);
            var ocorrenciasRota = _dbContext.OcorrenciaRepository.GetOcorrenciasCompletasOrdenadoDtInclusao(cdRota);
            var metaPainelIndicadores = _dbContext.MetasPainelIndicadoresRepository.GetByUnidadeNegocio(rota.CdUnidadeNegocio);
            var baldeiosEntregaRota = _dbContext.BaldeioEntregaRepository.GetBaldeiosMultiTransporteByRotaDestino(cdRota);
            var unidadeNegocioRota = _dbContext.UnidadeNegocioRepository.GetUnidadesNegocio(rota.CdUnidadeNegocio);
            var depositosUnidadeNegocioRota = _dbContext.DepositoRepository.GetDepositosComGeoCoordenadas(rota.CdUnidadeNegocio);
            var veiculoRota = _dbContext.VeiculoRepository.GetVeiculos(rota.CdPlacaVeiculo).FirstOrDefault();
            var tipoVeiculoRota = _dbContext.TipoVeiculoRepository.GetTipoVeiculo(veiculoRota.CdTipoVeiculo).FirstOrDefault();
            var deslocamentosAlmoco = _dbContext.DeslocamentoAlmocoRotaRepository.GetDeslocamentosPIM(cdRota);
            var deslocamentosAbastecimento = _dbContext.DeslocamentoAbastecimentoRotaRepository.GetDeslocamentosPIM(cdRota);
            var deslocamentosPernoite = _dbContext.DeslocamentoPernoiteRotaRepository.GetDeslocamentosPIM(cdRota);
            var paradas = _dbContext.ParadasTratadasAnaliticoRepository.Get(cdRota);

            _dbContext.EntregaRepository.EntregasComUnidadeNegocio(rota.Entregas);
            _dbContext.EntregaRepository.EntregasComCliente(rota.Entregas);

            Integracao.SapBrf.Entities.Integracao integracaoXml = new Integracao.SapBrf.Entities.Integracao();

            integracaoXml.NumeroRota = rota.CdRotaNegocio;

            // MultiTransporte
            integracaoXml.MultiTransporte = MultiTransporteNode.Processar(baldeiosEntregaRota);

            integracaoXml.Data = rota.DtRota.ToString("yyyy-MM-ddTHH:mm:ssZ");
            integracaoXml.SetDtData(rota.DtRota);
            integracaoXml.Placa = rota.CdPlacaVeiculo;

            // BRF não envia código do motorista
            integracaoXml.CpfMotorista = null;

            integracaoXml.CnpjTransportador = rota.Transportadora?.NrCnpj ?? "";
            integracaoXml.UnidadeNegocio = rota.CdUnidadeNegocio;

            // Indicadores Fluxo LES
            integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaDiariaOcorrencia.Processar(rota));
            integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaPernoiteOcorrencia.Processar(rota));
            integracaoXml.Ocorrencias.AdicionarOcorrencia(CustoDescargaOcorrencia.Processar(rota));
            integracaoXml.Ocorrencias.AdicionarOcorrencia(DevolucaoTransportadorOcorrencia.Processar(rota));
            integracaoXml.Ocorrencias.AdicionarOcorrencia(ReentregaOcorrencia.Processar(rota));
            integracaoXml.Ocorrencias.AdicionarOcorrencia(AdicionalBalsaOcorrencia.Processar(ocorrenciasRota));

            integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaKmOcorrencia.Processar(rota, ocorrenciasRota,
                deslocamentosAlmoco,
                deslocamentosAbastecimento,
                deslocamentosPernoite,
                paradas,
                metaPainelIndicadores.VlMetaAderencia));

            integracaoXml.Ocorrencias.AdicionarOcorrencia(AdicionalMeiaPernoiteOcorrencia.Processar(ocorrenciasRota, depositosUnidadeNegocioRota, rota));
            integracaoXml.Ocorrencias.AdicionarOcorrencia(DiariaClienteOcorrencia.Processar(rota, tipoVeiculoRota));

            var objRequest = ConverterObjetoRequisicaoWS(integracaoXml);

            #region Criação do arquivo XML
            var xml = XmlParser.ObjectToXml(integracaoXml);
            Console.Write(xml + "\n\n");
            XmlParser.CreateXmlFile(xml, @"C:\FluxoLES\xml", rota.CdRota.ToString());
            #endregion

            // Chama o WS
            EnvioXml envio = new EnvioXml()
            {
                CdRota = cdRota,
                CdRotaNegocio = rota.CdRotaNegocio,
                Xml = xml
            };

            _integrator.EnviarTest(objRequest);
        }

        public IIntegracaoSapBrfIntegrator Integrator { get; set; }

        protected override Result Process(IntegracaoSapBrfMessage message)
        {
            Retrier<bool> retrier = new Retrier<bool>();

            LoggerHelper.Info($"INFO: Rota {message.CdRota} recebida.");

            bool resultado = retrier.TryWithDelay(() => ProcessarRotaFinalizada(message.CdRota), _tentativas, _tempoEspera);

            if (!resultado)
            {
                LoggerHelper.Warning($"ATENÇÃO: A ROTA {message.CdRota} NÃO FOI FINALIZADA CORRETAMENTE.");
            }

            return ResultBuilder.Success();
        }

        private SI_CUSTO_ADICIONAL_FRETE_OUTRequest ConverterObjetoRequisicaoWS(Integracao.SapBrf.Entities.Integracao integracao)
        {
            int count = 0;
            SI_CUSTO_ADICIONAL_FRETE_OUTRequest objRequest = new SI_CUSTO_ADICIONAL_FRETE_OUTRequest();

            var ocorrenciasWS = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia[integracao.Ocorrencias.Where(x => x.ExibirOcorrenciaNoXml()).Count()];

            foreach(var ocorrencia in integracao.Ocorrencias.Where(x => x.ExibirOcorrenciaNoXml()))
            {
                var ocorrenciaWS = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia();

                if (ocorrencia.GetType() == typeof(DivergenciaDiaria))
                {
                    DivergenciaDiaria divergenciaDiaria = (DivergenciaDiaria)ocorrencia;

                    ocorrenciaWS.Codigo = divergenciaDiaria.Codigo;
                    ocorrenciaWS.Nome = divergenciaDiaria.Nome;
                    //ocorrenciaWS.QuantidadeDiariaPrevista = divergenciaDiaria.QuantidadeDiariaPrevista;
                    //ocorrenciaWS.QuantidadeDiariaRealizada = divergenciaDiaria.QuantidadeDiariaRealizada;
                }

                else if(ocorrencia.GetType() == typeof(DivergenciaPernoite))
                {
                    DivergenciaPernoite divergenciaPernoite = (DivergenciaPernoite)ocorrencia;

                    ocorrenciaWS.Codigo = divergenciaPernoite.Codigo;
                    ocorrenciaWS.Nome = divergenciaPernoite.Nome;
                    ocorrenciaWS.Quantidade = divergenciaPernoite.Quantidade.ToString();
                }

                else if (ocorrencia.GetType() == typeof(DivergenciaKm))
                {
                    DivergenciaKm divergenciaKM = (DivergenciaKm)ocorrencia;

                    ocorrenciaWS.Codigo = divergenciaKM.Codigo;
                    ocorrenciaWS.Nome = divergenciaKM.Nome;
                    ocorrenciaWS.HouveDivergencia = divergenciaKM.HouveDivergencia.ToString();
                }

                else if (ocorrencia.GetType() == typeof(CustoDescarga))
                {
                    var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

                    CustoDescarga custoDescarga = (CustoDescarga)ocorrencia;

                    ocorrenciaWS.Codigo = custoDescarga.Codigo;
                    ocorrenciaWS.Nome = custoDescarga.Nome;
                    ocorrenciaWS.Itens = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItens();

                    foreach(var item in custoDescarga.Itens)
                    {
                        var ocorrenciaWsItem = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem();

                        ocorrenciaWsItem.CodigoCliente = item.CodigoClienteNegocio;
                        ocorrenciaWsItem.ValorDescargaPrevisto = item.ValorDescargaPrevisto.ToString();
                        ocorrenciaWsItem.ValorDescargaRealizado = item.ValorDescargaRealizado.ToString();

                        itens.Add(ocorrenciaWsItem);
                    }

                    ocorrenciaWS.Itens.Item = itens.ToArray();
                }

                else if (ocorrencia.GetType() == typeof(DevolucaoTransportador))
                {
                    var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

                    DevolucaoTransportador devolucaoTransportador = (DevolucaoTransportador)ocorrencia;

                    ocorrenciaWS.Codigo = devolucaoTransportador.Codigo;
                    ocorrenciaWS.Nome = devolucaoTransportador.Nome;
                    ocorrenciaWS.Quantidade = devolucaoTransportador.Quantidade.ToString();

                    ocorrenciaWS.Itens = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItens();

                    foreach (var item in devolucaoTransportador.Itens)
                    {
                        var ocorrenciaWsItem = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem();

                        ocorrenciaWsItem.CodigoCliente = item.CodigoClienteNegocio;
                        ocorrenciaWsItem.Motivo = item.Motivo;

                        itens.Add(ocorrenciaWsItem);
                    }

                    ocorrenciaWS.Itens.Item = itens.ToArray();
                }

                else if (ocorrencia.GetType() == typeof(DiariaCliente))
                {
                    var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

                    DiariaCliente diariaCliente = (DiariaCliente)ocorrencia;

                    ocorrenciaWS.Codigo = diariaCliente.Codigo;
                    ocorrenciaWS.Nome = diariaCliente.Nome;

                    ocorrenciaWS.Itens = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItens();

                    foreach (var item in diariaCliente.Itens)
                    {
                        var ocorrenciaWsItem = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem();

                        ocorrenciaWsItem.CodigoCliente = item.CodigoCliente;
                        //ocorrenciaWsItem.Quantidade = item.Quantidade.ToString();

                        itens.Add(ocorrenciaWsItem);
                    }

                    ocorrenciaWS.Itens.Item = itens.ToArray();
                }

                else if (ocorrencia.GetType() == typeof(Reentrega))
                {
                    var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

                    Reentrega reentrega = (Reentrega)ocorrencia;

                    ocorrenciaWS.Codigo = reentrega.Codigo;
                    ocorrenciaWS.Nome = reentrega.Nome;
                    ocorrenciaWS.Quantidade = reentrega.Quantidade.ToString();

                    ocorrenciaWS.Itens = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItens();

                    foreach (var item in reentrega.Itens)
                    {
                        var ocorrenciaWsItem = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem();

                        ocorrenciaWsItem.CodigoCliente = item.CodigoClienteNegocio;
                        ocorrenciaWsItem.Motivo = item.Motivo;

                        itens.Add(ocorrenciaWsItem);
                    }

                    ocorrenciaWS.Itens.Item = itens.ToArray();
                }

                else if (ocorrencia.GetType() == typeof(AdicionalBalsa))
                {
                    var lstNomeBalsa = new List<string>();

                    AdicionalBalsa adicionalBalsa = (AdicionalBalsa)ocorrencia;

                    ocorrenciaWS.Codigo = adicionalBalsa.Codigo;
                    ocorrenciaWS.Nome = adicionalBalsa.Nome;
                    ocorrenciaWS.Quantidade = adicionalBalsa.Quantidade.ToString();

                    ocorrenciaWS.Itens = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItens();

                    foreach (var item in adicionalBalsa.Itens.NomeBalsa)
                    {
                        var ocorrenciaWsItem = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem();

                        lstNomeBalsa.Add(item);
                    }

                    ocorrenciaWS.Itens.NomeBalsa = lstNomeBalsa.ToArray();
                }
                
                else if (ocorrencia.GetType() == typeof(AdicionalMeiaPernoite))
                {
                    AdicionalMeiaPernoite adicionalMeiaPernoite = (AdicionalMeiaPernoite)ocorrencia;

                    ocorrenciaWS.Codigo = adicionalMeiaPernoite.Codigo;
                    ocorrenciaWS.Nome = adicionalMeiaPernoite.Nome;
                    ocorrenciaWS.HouveDivergencia = adicionalMeiaPernoite.HouveDivergencia.ToString();
                }

                ocorrenciasWS[count++] = ocorrenciaWS;
            }

            objRequest.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Request = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_Request()
            {
                Integracao = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracao()
                {
                    CnpjTransportador = integracao.CnpjTransportador,
                    Data = integracao.GetDtData(),
                    CpfMotorista = integracao.CpfMotorista,
                    MultiTransporte = integracao.MultiTransporte.NumeroRota.ConvertAll(x => x.ToString()).ToArray(),
                    NumeroRota = integracao.NumeroRota.ToString(),
                    Placa = integracao.Placa,
                    UnidadeNegocio = integracao.UnidadeNegocio,
                    Ocorrencias = ocorrenciasWS
                }
            };

            return objRequest;
        }

        private bool ProcessarRotaFinalizada(long cdRota)
        {
            bool retorno = false;

            var rota = _dbContext.RotaRepository.GetRotaIndicadoresFluxoLES(cdRota);
            var ocorrenciasRota = _dbContext.OcorrenciaRepository.GetOcorrenciasCompletasOrdenadoDtInclusao(cdRota);
            var metaPainelIndicadores = _dbContext.MetasPainelIndicadoresRepository.GetByUnidadeNegocio(rota.CdUnidadeNegocio);
            var baldeiosEntregaRota = _dbContext.BaldeioEntregaRepository.GetBaldeiosMultiTransporteByRotaDestino(cdRota);
            var unidadesNegocio = _dbContext.UnidadeNegocioRepository.GetAll();
            var depositosUnidadeNegocioRota = _dbContext.DepositoRepository.GetDepositosComGeoCoordenadas(rota.CdUnidadeNegocio);
            var veiculoRota = _dbContext.VeiculoRepository.GetVeiculos(rota.CdPlacaVeiculo).FirstOrDefault();
            var tipoVeiculoRota = _dbContext.TipoVeiculoRepository.GetTipoVeiculo(veiculoRota.CdTipoVeiculo).FirstOrDefault();
            var deslocamentosAlmoco = _dbContext.DeslocamentoAlmocoRotaRepository.GetDeslocamentosPIM(cdRota);
            var deslocamentosAbastecimento = _dbContext.DeslocamentoAbastecimentoRotaRepository.GetDeslocamentosPIM(cdRota);
            var deslocamentosPernoite = _dbContext.DeslocamentoPernoiteRotaRepository.GetDeslocamentosPIM(cdRota);
            var paradas = _dbContext.ParadasTratadasAnaliticoRepository.Get(cdRota);

            rota.Entregas = _dbContext.EntregaRepository.EntregasComUnidadeNegocio(rota.Entregas).ToList();
            rota.Entregas = _dbContext.EntregaRepository.EntregasComCliente(rota.Entregas).ToList();

            if (rota.CdSituacao == (long)SituacaoMonitoramento.Finalizada)
            {
                try
                {
                    Integracao.SapBrf.Entities.Integracao integracaoXml = new Integracao.SapBrf.Entities.Integracao();

                    integracaoXml.NumeroRota = rota.CdRotaNegocio;

                    // MultiTransporte
                    integracaoXml.MultiTransporte = MultiTransporteNode.Processar(baldeiosEntregaRota);

                    integracaoXml.Data = rota.DtRota.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    integracaoXml.SetDtData(rota.DtRota);
                    integracaoXml.Placa = rota.CdPlacaVeiculo;

                    // BRF não envia código do motorista
                    integracaoXml.CpfMotorista = null;

                    integracaoXml.CnpjTransportador = rota.Transportadora?.NrCnpj ?? "";
                    integracaoXml.UnidadeNegocio = rota.CdUnidadeNegocio;

                    // Indicadores Fluxo LES
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaDiariaOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaPernoiteOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(CustoDescargaOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DevolucaoTransportadorOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(ReentregaOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(AdicionalBalsaOcorrencia.Processar(ocorrenciasRota));

                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaKmOcorrencia.Processar(rota, ocorrenciasRota, 
                        deslocamentosAlmoco, 
                        deslocamentosAbastecimento, 
                        deslocamentosPernoite,
                        paradas,
                        metaPainelIndicadores.VlMetaAderencia));

                    integracaoXml.Ocorrencias.AdicionarOcorrencia(AdicionalMeiaPernoiteOcorrencia.Processar(ocorrenciasRota, depositosUnidadeNegocioRota, rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DiariaClienteOcorrencia.Processar(rota, tipoVeiculoRota));

                    ConverterObjetoRequisicaoWS(integracaoXml);

                    #region Criação do arquivo XML
                    var xml = XmlParser.ObjectToXml(integracaoXml);
                    Console.Write(xml + "\n\n");
                    XmlParser.CreateXmlFile(xml, @"C:\FluxoLES\xml", rota.CdRota.ToString());
                    #endregion

                    // Chama o WS
                    EnvioXml envio = new EnvioXml()
                    {
                        CdRota = cdRota,
                        CdRotaNegocio = rota.CdRotaNegocio,
                        Xml = xml
                    };

                    _integrator.Enviar(envio);

                    retorno = true;
                }

                catch (Exception ex)
                {
                    retorno = false;
                }

                return retorno;
            }

            return retorno;
        }

        protected override Result ValidateMessage(IntegracaoSapBrfMessage message)
        {
            //if (Guid.Empty.Equals(message.IdTransporteParada)) return ResultBuilder.Warning(ValidationMessages.ParadaNaoInformada);

            //if (string.IsNullOrEmpty(message.EventName)) return ResultBuilder.Warning(ValidationMessages.OperacaoInvalida);

            return ResultBuilder.Success();
        }
    }
}