﻿using HBSIS.MercadoLes.Commons.Base.Service;
using HBSIS.MercadoLes.Services.Messages.Message;
using System;
using System.Linq;
using HBSIS.MercadoLes.Persistence;
using HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using HBSIS.MercadoLes.CustoAdicionalBrf.Utils;
using HBSIS.MercadoLes.CustoAdicionalBrf.Enums;
using HBSIS.Framework.Commons.Result;
using HBSIS.Framework.Commons.Helper;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.IO;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Service
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

#if DEBUG
            var cdRota = 1333634;
            ProcessarRotaFinalizada(cdRota);
#endif
        }

        public IIntegracaoSapBrfIntegrator Integrator { get; set; }

        protected override Result Process(IntegracaoSapBrfMessage message)
        {
            LoggerHelper.Info($"INFO: Rota {message.CdRota} recebida.");

            try
            {
                ProcessarRotaFinalizada(message.CdRota);
            }

            catch (Exception ex)
            {
                LoggerHelper.Error($"Exception: {ex.Message}");
            }

            LoggerHelper.Info($"INFO: Rota {message.CdRota} concluida.");

            return ResultBuilder.Success();
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
            var depositos = _dbContext.DepositoRepository.GetDepositosComGeoCoordenadas();
            var veiculoRota = _dbContext.VeiculoRepository.GetVeiculos(rota.CdPlacaVeiculo).FirstOrDefault();
            var tipoVeiculoRota = _dbContext.TipoVeiculoRepository.GetTipoVeiculo(veiculoRota.CdTipoVeiculo).FirstOrDefault();
            var deslocamentosAlmoco = _dbContext.DeslocamentoAlmocoRotaRepository.GetDeslocamentosPIM(cdRota);
            var deslocamentosAbastecimento = _dbContext.DeslocamentoAbastecimentoRotaRepository.GetDeslocamentosPIM(cdRota);
            var deslocamentosPernoite = _dbContext.DeslocamentoPernoiteRotaRepository.GetDeslocamentosPIM(cdRota);
            var paradas = _dbContext.ParadasTratadasAnaliticoRepository.Get(cdRota);
            decimal valorMetaAderenciaUnidadeNegocio = metaPainelIndicadores?.VlMetaAderencia ?? 0;

            rota.Entregas = _dbContext.EntregaRepository.EntregasComUnidadeNegocio(rota.Entregas).ToList();
            rota.Entregas = _dbContext.EntregaRepository.EntregasComCliente(rota.Entregas).ToList();

            if (rota.CdSituacao == (long)SituacaoMonitoramento.Finalizada)
            {
                try
                {
                    Integracao integracaoXml = new Integracao();

                    integracaoXml.NumeroRota = rota.CdRotaNegocio;

                    // MultiTransporte
                    integracaoXml.MultiTransporte = MultiTransporteNode.Processar(baldeiosEntregaRota, rota);

                    integracaoXml.Data = rota.DtRota.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    integracaoXml.SetDtData(rota.DtRota);
                    integracaoXml.Placa = rota.CdPlacaVeiculo;

                    // BRF não envia código do motorista
                    integracaoXml.CpfMotorista = string.Empty;

                    integracaoXml.CnpjTransportador = rota.Transportadora?.NrCnpj ?? string.Empty;
                    integracaoXml.UnidadeNegocio = rota.CdUnidadeNegocio;

                    // Indicadores Fluxo LES
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaDiariaOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaPernoiteOcorrencia.Processar(rota, ocorrenciasRota, depositos));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(CustoDescargaOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DevolucaoTransportadorOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(ReentregaOcorrencia.Processar(rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(AdicionalBalsaOcorrencia.Processar(ocorrenciasRota));

                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DivergenciaKmOcorrencia.Processar(rota, ocorrenciasRota,
                        deslocamentosAlmoco,
                        deslocamentosAbastecimento,
                        deslocamentosPernoite,
                        paradas,
                        valorMetaAderenciaUnidadeNegocio));

                    integracaoXml.Ocorrencias.AdicionarOcorrencia(AdicionalMeiaPernoiteOcorrencia.Processar(ocorrenciasRota, depositosUnidadeNegocioRota, rota));
                    integracaoXml.Ocorrencias.AdicionarOcorrencia(DiariaClienteOcorrencia.Processar(rota, tipoVeiculoRota));

                    var objRequest = ConverterObjetoRequisicaoWS(integracaoXml);

                    #region Criação do arquivo XML
                    var xml = XmlParser.ObjectToXml(objRequest);
                    Console.Write(xml + "\n\n");
                    XmlParser.CreateXmlFile(xml, @"C:\FluxoLES\xml", rota.CdRotaNegocio.ToString());
                    #endregion

                    _integrator.Enviar(objRequest);
                    //Execute(xml);

                    retorno = true;
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return retorno;
            }

            return retorno;
        }

        public void Execute(string xmlContent)
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml($@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cus=""http://www.brf.com/hbsis/CUSTO_ADICIONAL_FRETE"">
                    <soapenv:Header/>                  
                        <soapenv:Body>
                        <cus:MT_CUSTO_ADICIONAL_FRETE_HBSIS_Request>
                            {xmlContent}
                        </cus:MT_CUSTO_ADICIONAL_FRETE_HBSIS_Request>
                      </soapenv:Body>
                    </soapenv:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://web.brasilfoods.com:8101/XISOAPAdapter/MessageServlet?senderParty=&senderService=BC_HB_SIS&receiverParty=&receiverService=&interface=SI_CUSTO_ADICIONAL_FRETE_OUT&interfaceNamespace=http://www.brf.com/hbsis/CUSTO_ADICIONAL_FRETE");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Credentials = new System.Net.NetworkCredential("hbsis", "brf6230@");
            return webRequest;
        }

        private SI_CUSTO_ADICIONAL_FRETE_OUTRequest ConverterObjetoRequisicaoWS(Integracao integracao)
        {
            int count = 0;
            SI_CUSTO_ADICIONAL_FRETE_OUTRequest objRequest = new SI_CUSTO_ADICIONAL_FRETE_OUTRequest();

            var ocorrenciasWS = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia[integracao.Ocorrencias.Where(x => x.ExibirOcorrenciaNoXml()).Count()];

            foreach (var ocorrencia in integracao.Ocorrencias.Where(x => x.ExibirOcorrenciaNoXml()))
            {
                var ocorrenciaWS = InicializaOcorrencia();

                if (ocorrencia.GetType() == typeof(DivergenciaDiaria))
                {
                    DivergenciaDiariaOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(DivergenciaPernoite))
                {
                    DivergenciaPernoiteOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(DivergenciaKm))
                {
                    DivergenciaKmOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(CustoDescarga))
                {
                    CustoDescargaOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(DevolucaoTransportador))
                {
                    DevolucaoTransportadorOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(DiariaCliente))
                {
                    DIariaClienteOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(Reentrega))
                {
                    ReentregaOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(AdicionalBalsa))
                {
                    AdicionalBalsaOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                else if (ocorrencia.GetType() == typeof(AdicionalMeiaPernoite))
                {
                    AdicionalMeiaPernoiteOcorrenciaWS(ocorrencia, ocorrenciaWS);
                }

                ocorrenciasWS[count++] = ocorrenciaWS;
            }

            objRequest.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Request = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_Request()
            {
                Integracao = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracao()
                {
                    CnpjTransportador = integracao.CnpjTransportador,
                    Data = integracao.GetDtData(),
                    DataSpecified = true,
                    CpfMotorista = integracao.CpfMotorista,
                    MultiTransporte = integracao.MultiTransporte?.NumeroRota?.ConvertAll(x => x.ToString()).ToArray() ?? null,
                    NumeroRota = integracao.NumeroRota.ToString(),
                    Placa = integracao.Placa,
                    UnidadeNegocio = integracao.UnidadeNegocio,
                    Ocorrencias = ocorrenciasWS
                }
            };

            return objRequest;
        }

        private DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia InicializaOcorrencia()
        {
            DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia();

            ocorrenciaWS.Codigo = string.Empty;
            ocorrenciaWS.HouveDivergencia = string.Empty;
            ocorrenciaWS.Nome = string.Empty;
            ocorrenciaWS.Quantidade = string.Empty;
            ocorrenciaWS.QuantidadeDiariaPrevista = string.Empty;
            ocorrenciaWS.QuantidadeDiariaRealizada = string.Empty;
            ocorrenciaWS.KMPrevisto = string.Empty;
            ocorrenciaWS.KMRealizado = string.Empty;
            ocorrenciaWS.QuantidadePernoitePrevista = string.Empty;
            ocorrenciaWS.QuantidadePernoiteRealizada = string.Empty;
            ocorrenciaWS.Itens = InicializaItens();
            ocorrenciaWS.Itens.Item = new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem[1] { InicializaItem() };

            return ocorrenciaWS;
        }

        private static DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItens InicializaItens()
        {
            return new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItens()
            {
                NomeBalsa = new string[1] { string.Empty },
            };
        }

        private static DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem InicializaItem()
        {
            return new DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem()
            {
                CodigoCliente = string.Empty,
                Motivo = string.Empty,
                Quantidade = string.Empty,
                ValorDescargaPrevisto = string.Empty,
                ValorDescargaRealizado = string.Empty
            };

        }

        private static void AdicionalMeiaPernoiteOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            AdicionalMeiaPernoite adicionalMeiaPernoite = (AdicionalMeiaPernoite)ocorrencia;

            ocorrenciaWS.Codigo = adicionalMeiaPernoite.Codigo;
            ocorrenciaWS.Nome = adicionalMeiaPernoite.Nome;
            ocorrenciaWS.HouveDivergencia = adicionalMeiaPernoite.HouveDivergencia.ToString();
        }

        private static void AdicionalBalsaOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            var lstNomeBalsa = new List<string>();

            AdicionalBalsa adicionalBalsa = (AdicionalBalsa)ocorrencia;

            ocorrenciaWS.Codigo = adicionalBalsa.Codigo;
            ocorrenciaWS.Nome = adicionalBalsa.Nome;
            ocorrenciaWS.Quantidade = adicionalBalsa.Quantidade.ToString();

            ocorrenciaWS.Itens = InicializaItens();

            foreach (var item in adicionalBalsa.Itens.NomeBalsa)
            {
                lstNomeBalsa.Add(item);
            }

            ocorrenciaWS.Itens.NomeBalsa = lstNomeBalsa.ToArray();
        }

        private static void ReentregaOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

            Reentrega reentrega = (Reentrega)ocorrencia;

            ocorrenciaWS.Codigo = reentrega.Codigo;
            ocorrenciaWS.Nome = reentrega.Nome;
            ocorrenciaWS.Quantidade = reentrega.Quantidade.ToString();

            ocorrenciaWS.Itens = InicializaItens();

            foreach (var item in reentrega.Itens)
            {
                var ocorrenciaWsItem = InicializaItem();

                ocorrenciaWsItem.CodigoCliente = item.CodigoClienteNegocio;
                ocorrenciaWsItem.Motivo = item.Motivo;

                itens.Add(ocorrenciaWsItem);
            }

            ocorrenciaWS.Itens.Item = itens.ToArray();
        }

        private static void DIariaClienteOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

            DiariaCliente diariaCliente = (DiariaCliente)ocorrencia;

            ocorrenciaWS.Codigo = diariaCliente.Codigo;
            ocorrenciaWS.Nome = diariaCliente.Nome;

            ocorrenciaWS.Itens = InicializaItens();

            foreach (var item in diariaCliente.Itens)
            {
                var ocorrenciaWsItem = InicializaItem();

                ocorrenciaWsItem.CodigoCliente = item.CodigoCliente;
                ocorrenciaWsItem.Quantidade = item.Quantidade.ToString();

                itens.Add(ocorrenciaWsItem);
            }

            ocorrenciaWS.Itens.Item = itens.ToArray();
        }

        private static void DevolucaoTransportadorOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

            DevolucaoTransportador devolucaoTransportador = (DevolucaoTransportador)ocorrencia;

            ocorrenciaWS.Codigo = devolucaoTransportador.Codigo;
            ocorrenciaWS.Nome = devolucaoTransportador.Nome;
            ocorrenciaWS.Quantidade = devolucaoTransportador.Quantidade.ToString();

            ocorrenciaWS.Itens = InicializaItens();

            foreach (var item in devolucaoTransportador.Itens)
            {
                var ocorrenciaWsItem = InicializaItem();

                ocorrenciaWsItem.CodigoCliente = item.CodigoClienteNegocio;
                ocorrenciaWsItem.Motivo = item.Motivo;

                itens.Add(ocorrenciaWsItem);
            }

            ocorrenciaWS.Itens.Item = itens.ToArray();
        }

        private static void CustoDescargaOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            var itens = new List<DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrenciaItensItem>();

            CustoDescarga custoDescarga = (CustoDescarga)ocorrencia;

            ocorrenciaWS.Codigo = custoDescarga.Codigo;
            ocorrenciaWS.Nome = custoDescarga.Nome;
            ocorrenciaWS.Itens = InicializaItens();

            foreach (var item in custoDescarga.Itens)
            {
                var ocorrenciaWsItem = InicializaItem();

                ocorrenciaWsItem.CodigoCliente = item.CodigoClienteNegocio;
                ocorrenciaWsItem.ValorDescargaPrevisto = item.ValorDescargaPrevisto.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
                ocorrenciaWsItem.ValorDescargaRealizado = item.ValorDescargaRealizado.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);

                itens.Add(ocorrenciaWsItem);
            }

            ocorrenciaWS.Itens.Item = itens.ToArray();
        }

        private static void DivergenciaKmOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            DivergenciaKm divergenciaKM = (DivergenciaKm)ocorrencia;

            ocorrenciaWS.Codigo = divergenciaKM.Codigo;
            ocorrenciaWS.Nome = divergenciaKM.Nome;
            ocorrenciaWS.KMPrevisto = divergenciaKM.KMPrevisto.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
            ocorrenciaWS.KMRealizado = divergenciaKM.KMRealizado.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
        }

        private static void DivergenciaPernoiteOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            DivergenciaPernoite divergenciaPernoite = (DivergenciaPernoite)ocorrencia;

            ocorrenciaWS.Codigo = divergenciaPernoite.Codigo;
            ocorrenciaWS.Nome = divergenciaPernoite.Nome;
            ocorrenciaWS.QuantidadePernoitePrevista = divergenciaPernoite.QuantidadePrevista.ToString();
            ocorrenciaWS.QuantidadePernoiteRealizada = divergenciaPernoite.QuantidadeRealizada.ToString();
        }

        private static void DivergenciaDiariaOcorrenciaWS(Entities.Ocorrencia ocorrencia, DT_CUSTO_ADICIONAL_FRETE_HBSIS_RequestIntegracaoOcorrencia ocorrenciaWS)
        {
            DivergenciaDiaria divergenciaDiaria = (DivergenciaDiaria)ocorrencia;

            ocorrenciaWS.Codigo = divergenciaDiaria.Codigo;
            ocorrenciaWS.Nome = divergenciaDiaria.Nome;
            ocorrenciaWS.QuantidadeDiariaPrevista = divergenciaDiaria.QuantidadeDiariaPrevista.ToString();
            ocorrenciaWS.QuantidadeDiariaRealizada = divergenciaDiaria.QuantidadeDiariaRealizada.ToString();
        }

        protected override Result ValidateMessage(IntegracaoSapBrfMessage message)
        {
            //if (Guid.Empty.Equals(message.IdTransporteParada)) return ResultBuilder.Warning(ValidationMessages.ParadaNaoInformada);

            //if (string.IsNullOrEmpty(message.EventName)) return ResultBuilder.Warning(ValidationMessages.OperacaoInvalida);

            return ResultBuilder.Success();
        }
    }
}