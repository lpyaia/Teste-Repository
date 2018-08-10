using HBSIS.GE.FileImporter.Services.Commons.Integration;
using HBSIS.GE.FileImporter.Services.Commons.Integration.Log;

namespace HBSIS.GE.FileImporter.Producer.Utils
{
    public class LogFileImporterProducer : LogIntegrationSender<LogFileImporterProducer>
    {
        public string NumeroRotaNegocio { get; set; }
    }
}