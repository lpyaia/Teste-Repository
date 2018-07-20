
using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Commons.Helper;
using HBSIS.MercadoLes.Integracao.SapBrf.Service;
using HBSIS.MercadoLes.Services.Persistence.Repository;
using System;
//using Quartz;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Job
{
    public class IntegracaoSapBrfJob : BaseJob
    {
        private IDataContext _mongoContext;
        private EnvioXmlRepository _envioXmlRepository;
        private IntegracaoSapBrfIntegrator _integracaoSap;
        private IIntegracaoSapBrfConfigurator _config;

        public IntegracaoSapBrfJob(int tempoRecorrencia, IIntegracaoSapBrfConfigurator configurator) : base(tempoRecorrencia, typeof(IntegracaoSapBrfJob).Name)
        {   
            var mongoFactory = Configuration.Actual.GetMongoFactory();
            _config = configurator;

            _mongoContext = mongoFactory.GetDataContext();
            _envioXmlRepository = new EnvioXmlRepository(_mongoContext);
            _integracaoSap = new IntegracaoSapBrfIntegrator(_config);
        }
        
        public override void Action()
        {
            var rotasReenvio = _envioXmlRepository.GetByReenvio();

            foreach(var rotaReenvio in rotasReenvio)
            {
                _integracaoSap.Enviar(rotaReenvio);
            }
        }
    }
}