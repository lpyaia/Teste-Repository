using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Commons.Config;
using HBSIS.GE.FileImporter.Services.Commons.Base.ServiceControl;
using System;
using System.Threading.Tasks;
using HBSIS.GE.Microservices.FileImporter.Consumer.Utils;
using HBSIS.GE.Microservices.FileImporter.Consumer.Service;
using HBSIS.GE.FileImporter.Services.Messages.Message;

namespace HBSIS.GE.Microservices.FileImporter.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = FileImporterConsumerConfigurator.GetInstance().Get();

            Configuration
                     .UseStaticDictionary()
                     .UseThreadContextPersister()
                     .UseAppName("FileImporterConsumer")
                     .UseServiceLog4Net()
                     .UseBusEasyNetQFactory()
                     .UseSqlConnectionString("hbsis.importer-sql")
                     .UseDataDapperFactory()
                     .UseDataMongoFactory()
                     .UseMongoConnectionString("hbsis.importer-log")
                     .GetEndointWebServiceGE()
                     .Configure();

            var consumerService = ConsumerServiceControl.Create<FileImporterConsumerService, FileImporterMessage>
                (new FileImporterConsumerService(Configuration.Actual), "GE-ImportacaoArquivos");

            while (!consumerService.Start())
            {
                LoggerHelper.Error("ERROR: Falha ao iniciar o consumidor do RabbitMQ. Tentando novamente em 60s.");
                Task.Delay(60000).Wait();
            }

            while (Console.ReadLine() != "q") ;
        }
    }
}
