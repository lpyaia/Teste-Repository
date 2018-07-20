using HBSIS.GE.FileImporter.Services.Commons.Integration;
using HBSIS.GE.FileImporter.Services.Commons.Integration.Config;

namespace HBSIS.GE.Microservices.FileImporter.Producer.Utils
{
    public interface IFileImporterProducerConfigurator : IIntegrationConfigBuilder<IntegrationConfig>
    {
    }
}