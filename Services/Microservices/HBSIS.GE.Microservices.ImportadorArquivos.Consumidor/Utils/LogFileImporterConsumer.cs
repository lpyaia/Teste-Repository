﻿using HBSIS.GE.FileImporter.Services.Commons.Integration;
using HBSIS.GE.FileImporter.Services.Commons.Integration.Log;

namespace HBSIS.GE.Microservices.FileImporter.Consumer.Utils
{
    public class LogFileImporterConsumer : LogIntegrationSender<LogFileImporterConsumer>
    {
        public string NumeroRotaNegocio { get; set; }
    }
}