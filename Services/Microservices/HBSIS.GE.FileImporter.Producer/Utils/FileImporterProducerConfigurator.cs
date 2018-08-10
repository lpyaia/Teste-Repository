using HBSIS.GE.FileImporter.Services.Commons.Integration.Config;

namespace HBSIS.GE.FileImporter.Producer.Utils
{
    public class FileImporterProducerConfigurator : IntegrationConfigBuilder, IFileImporterProducerConfigurator
    {
        public FileImporterProducerConfigurator()
            : base("FileImporterProducer")
        {
        }

        public static IFileImporterProducerConfigurator GetInstance()
        {
            return new FileImporterProducerConfigurator();
        }
    }
}