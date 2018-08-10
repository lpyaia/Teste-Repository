using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Data;
using HBSIS.MercadoLes.CustoAdicionalBrf.Wrapper;
using HBSIS.MercadoLes.Commons.Integration.Config;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;
using System;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Service
{
    public class IntegracaoSapBrfIntegrator : IIntegracaoSapBrfIntegrator
    {
        private IDataContext _mongoContext = null;
        private IIntegracaoSapBrfWrapper _wrapper;
        protected Func<IntegrationConfig, IntegracaoSapBrfWrapper> IntegracaoCustoAdicionaBrfWrapperFactory = (config) => new IntegracaoSapBrfWrapper(config);

        public IntegracaoSapBrfIntegrator(IIntegracaoSapBrfConfigurator configurator)
        {
            Config = configurator.Get();

            var mongoFactory = Configuration.Actual.GetMongoFactory();
            _mongoContext = mongoFactory.GetDataContext();
        }

        protected IntegrationConfig Config { get; }

        protected IIntegracaoSapBrfWrapper Wrapper
        {
            get
            {
                if (_wrapper == null)
                    _wrapper = IntegracaoCustoAdicionaBrfWrapperFactory(Config);

                return _wrapper;
            }
            set { _wrapper = value; }
        }
        
        public void Enviar(SI_CUSTO_ADICIONAL_FRETE_OUTRequest request)
        {
            Wrapper.SendSync(request);
        }
        
        public void ReenviarTodos()
        {
            Wrapper.ResendAll();
        }
        
    }
}