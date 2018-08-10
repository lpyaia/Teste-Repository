using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Helper;
using HBSIS.MercadoLes.Commons.Base.ServiceControl;
using HBSIS.MercadoLes.Messages.Message;
using System;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Service
{
    public class IntegracaoSapBrfStartup
    {
        public void Start()
        {
            var config = IntegracaoSapBrfConfigurator.GetInstance().Get();
            const string INTEGRATOR_SERVICE_TYPE = "-j";
            string type = "";

            Configuration
                     .UseStaticDictionary()
                     .UseThreadContextPersister()
                     .UseBusEasyNetQFactory()
                     .UseSqlConnectionString("hbsis.les-sql")
                     .UseDataDapperFactory()
                     .UseDataMongoFactory()
                     .UseMongoConnectionString("hbsis.les-log")
                     .Configure();

            switch (type)
            {
                case INTEGRATOR_SERVICE_TYPE:
                    Configuration.Actual.UseAppName("IntegracaoSapBrfJob").UseJobLog4Net();

                    LoggerHelper.Info("INFO: Fluxo LES Job iniciado");

                    int IntegracaoSapBrfJobIntervalo = Configuration.Actual.GetJobInterval("IntegracaoSapBrfJob");
                    //new IntegracaoSapBrfJob(IntegracaoSapBrfJobIntervalo, IntegracaoSapBrfConfigurator.GetInstance());

                    break;

                default:
                    Configuration.Actual.UseAppName("IntegracaoSapBrfService").UseServiceLog4Net();

                    LoggerHelper.Info("INFO: Fluxo LES Service iniciado");

                    // Cria o microserviço para o recebimento das rotas da secundária 
                    var consumerService = ConsumerServiceControl.Create<IntegracaoSapBrfService, IntegracaoSapBrfMessage>(new IntegracaoSapBrfService(IntegracaoSapBrfConfigurator.GetInstance()));

                    try
                    {
                        consumerService.Start();
                    }

                    catch (Exception ex)
                    {
                        LoggerHelper.Error("ERROR: Falha ao iniciar o consumidor do RabbitMQ. Exception: " + ex.Message);
                    }

                    break;
            }
        }
    }
}
