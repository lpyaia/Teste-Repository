using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Helper;
using HBSIS.GE.Microservices.FileImporter.Producer.Utils;
using HBSIS.GE.FileImporter.Services.Commons.Base.ServiceControl;
using System;
using HBSIS.GE.FileImporter.Services.Messages.Message;

namespace HBSIS.GE.Microservices.FileImporter.Producer.Service
{
    public class ImportadorArquivosProdutorStartup
    {
        public void Start()
        {
            var config = FileImporterProducerConfigurator.GetInstance().Get();
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

                    break;

                default:
                    Configuration.Actual.UseAppName("IntegracaoSapBrfService").UseServiceLog4Net();

                    LoggerHelper.Info("INFO: Fluxo LES Service iniciado");

                    // Cria o microserviço para o recebimento das rotas da secundária 
                    //var consumerService = ConsumerServiceControl.Create<FileImporterProducerService, FileImporterMessage>(new FileImporterProducerService(Configuration.Actual));

                    try
                    {
                        //consumerService.Start();
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
