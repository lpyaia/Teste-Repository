using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Helper;
using HBSIS.MercadoLes.Infra.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.IntegracaoCutoffWebService;
using HBSIS.MercadoLes.Integracao.SapBrf.Wrapper;
using HBSIS.MercadoLes.Services.Commons.Helpers;
using HBSIS.MercadoLes.Services.Commons.Integration.Config;
using HBSIS.MercadoLes.Services.Persistence.Repository;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;
using System;
using System.Linq;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Service
{
    public class IntegracaoSapBrfIntegrator : IIntegracaoSapBrfIntegrator
    {
        private IDataContext _mongoContext = null;
        private EnvioXmlRepository _envioXmlRepository;
        private IIntegracaoSapBrfWrapper _wrapper;
        protected Func<IntegrationConfig, IntegracaoSapBrfWrapper> IntegracaoCustoAdicionaBrfWrapperFactory = (config) => new IntegracaoSapBrfWrapper(config);

        public IntegracaoSapBrfIntegrator(IIntegracaoSapBrfConfigurator configurator)
        {
            Config = configurator.Get();

            var mongoFactory = Configuration.Actual.GetMongoFactory();
            _mongoContext = mongoFactory.GetDataContext();

            _envioXmlRepository = new EnvioXmlRepository(_mongoContext);
        }

        protected IntegrationConfig Config { get; }

        protected IIntegracaoSapBrfWrapper Wrapper
        {
            get
            {
                if (_wrapper == null)
                    _wrapper = IntegracaoSapBrfWrapperFactory.Get(Config);

                return _wrapper;
            }
            set { _wrapper = value; }
        }

        public void Enviar(IntegracaoSapBrfModel model)
        {

        }

        public void EnviarTest(SI_CUSTO_ADICIONAL_FRETE_OUTRequest request)
        {
            var wrapper = IntegracaoCustoAdicionaBrfWrapperFactory(Config);
            wrapper.SendSync(request);
        }

        public void Enviar(EnvioXml envio)
        {
            string guid = envio.Id != Guid.Empty ? envio.Id.ToString() : string.Empty;
            LoggerHelper.Info($"INFO: Rota {envio.CdRota} ({guid}) será enviada para o WEB SERVICE.");

            try
            {
                throw new Exception("ERRO AO ENVIAR PARA O WS");

                // envio.Reenviar = false;
            }

            catch
            {
                LoggerHelper.Error($"ERRO: Rota {envio.CdRota} ({guid}) não pôde ser enviada ao WEB SERVICE. Tentativas de Reenvio: {envio.Tentativas}");

                // Salva o xml no mongo para ser reprocessado
                if (envio.Id == Guid.Empty)
                {
                    envio.Reenviar = true;

                    try
                    {
                        _envioXmlRepository.Insert(envio);
                    }

                    catch(Exception dbException)
                    {
                        LoggerHelper.Error($"ERRO: Não foi possível inserir a rota {envio.CdRota} ({guid}) na base do MongoDB. {dbException.Message}");
                    }
                }

                else
                {
                    envio.Tentativas++;
                    envio.DtUltimaTentativaReenvio = DateTime.Now;

                    try
                    {
                        _envioXmlRepository.Update(envio);
                    }

                    catch(Exception dbException)
                    {
                        LoggerHelper.Error($"ERRO: Não foi possível atualizar a rota {envio.CdRota} ({guid}) na base do MongoDB. {dbException.Message}");

                    }
                }
            }

            //var request = GetRequest(model);

            //Wrapper.SendAsync(request);
        }

        public void ReenviarTodos()
        {
            Wrapper.ResendAll();
        }

        private static SI_RET_TRANSPORTERequest GetRequest(IntegracaoSapBrfModel model)
        {
            if (model == null) return null;

            var data = model.DataEntrega.ToOffset();

            var dataSend = new DT_RET_TRANSPORTE
            {
                I_MAPA = new DT_RET_TRANSPORTEI_MAPA
                {
                    TRANSPORTE = model.NumeroTransporte,
                    FATURA = model.NumeroFatura,
                    DT_ENTREGA = data.ToString("yyyyMMdd"),
                    HR_ENTREGA = data.ToString("HHmmss")
                }
            };

            var request = new SI_RET_TRANSPORTERequest(dataSend);

            return request;
        }
    }
}