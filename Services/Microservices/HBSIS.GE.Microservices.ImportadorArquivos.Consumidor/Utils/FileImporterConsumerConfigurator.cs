using HBSIS.GE.FileImporter.Services.Commons.Integration.Config;

namespace HBSIS.GE.Microservices.FileImporter.Consumer.Utils
{
    public class FileImporterConsumerConfigurator : IntegrationConfigBuilder, IFileImporterConsumerConfigurator
    {
        public FileImporterConsumerConfigurator()
            : base("ImportadorArquivosConsumidor")
        {
        }

        public static IFileImporterConsumerConfigurator GetInstance()
        {
            return new FileImporterConsumerConfigurator();
        }
    }
}