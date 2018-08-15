using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Commons.Config;
using HBSIS.MercadoLes.CustoAdicionalBrf.Service;
using HBSIS.MercadoLes.Commons.Base.ServiceControl;
using HBSIS.MercadoLes.Services.Messages.Message;
using System;
using System.Threading.Tasks;
using HBSIS.MercadoLes.CustoAdicionalBrf.Utils;

namespace HBSIS.MercadoLes.CustoAdicionalBrf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = IntegracaoSapBrfConfigurator.GetInstance().Get();
            const string INTEGRATOR_SERVICE_TYPE = "";

            Configuration
                     .UseStaticDictionary()
                     .UseThreadContextPersister()
                     .UseBusEasyNetQFactory()
                     .UseSqlConnectionString("hbsis.les-sql")
                     .UseDataDapperFactory()
                     .UseDataMongoFactory()
                     .UseMongoConnectionString("hbsis.les-log")
                     .UseMongoMapClass<IntegracaoSapBrfMongoMap>()
                     .Configure();
            
            //var instance = args.GetInstance()?.ToLower();
            var type = args.Length > 0 ? args[0] : "-j";

            switch (type)
            {
                case INTEGRATOR_SERVICE_TYPE:
                    Configuration.Actual.UseAppName("IntegracaoSapBrfJob").UseJobLog4Net();

                    int IntegracaoSapBrfJobIntervalo = Configuration.Actual.GetJobInterval("IntegracaoSapBrfJob");
                    //new IntegracaoSapBrfJob(IntegracaoSapBrfJobIntervalo, IntegracaoSapBrfConfigurator.GetInstance());

                    break;

                default:
                    Configuration.Actual.UseAppName("IntegracaoSapBrfService").UseServiceLog4Net();

                    LoggerHelper.Info("INFO: Fluxo LES Service iniciado.");

                    // Cria o microserviço para o recebimento das rotas da secundária 
                    var consumerService = ConsumerServiceControl.Create<IntegracaoSapBrfService, IntegracaoSapBrfMessage>(new IntegracaoSapBrfService(IntegracaoSapBrfConfigurator.GetInstance()));


                    while(!consumerService.Start())
                    {
                        LoggerHelper.Error("ERROR: Falha ao iniciar o consumidor do RabbitMQ. Tentando novamente em 60s.");
                        Task.Delay(60000).Wait();
                    }

                    break;
            }

            while (Console.ReadLine() != "q") ;
        }

        private static void JobEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("JobEvent");
        }
    }
}
