using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Commons.Config;
using HBSIS.GE.FileImporter.Services.Commons.Base.ServiceControl;
using System;
using System.Threading.Tasks;
using HBSIS.GE.Microservices.FileImporter.Producer.Utils;
using HBSIS.GE.Microservices.FileImporter.Producer.Service;
using HBSIS.GE.FileImporter.Services.Messages.Message;
using System.Threading;

namespace HBSIS.GE.Microservices.FileImporter.Producer
{
    public class Program
    {
        static FileImporterProducerService fileImporterProducerService;
        static Timer timer;

        public static void Main(string[] args)
        {
            var config = FileImporterProducerConfigurator.GetInstance().Get();
            
            Configuration
                     .UseStaticDictionary()
                     .UseThreadContextPersister()
                     .UseAppName("FileImporterProducer")
                     .UseServiceLog4Net()
                     .UseBusEasyNetQFactory()
                     .UseSqlConnectionString("hbsis.importer-sql")
                     .UseDataDapperFactory()
                     .UseDataMongoFactory()
                     .UseMongoConnectionString("hbsis.importer-log")
                     .Configure();

            fileImporterProducerService = new FileImporterProducerService(Configuration.Actual);
            Timer timer = new Timer(FileProcess, null, 0, 1000);

            while (Console.ReadLine() != "q") ;

            timer.Dispose();
        }

        private static void FileProcess(object state)
        {
            fileImporterProducerService.FileProcess();
        }

        private static void JobEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("JobEvent");
        }
    }
}
