using HBSIS.Framework.Bus.EasyNetQRabbit;
using HBSIS.GE.FileImporter.Services.Messages.Message;
using HBSIS.GE.FileImporter.Services.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HBSIS.GE.Microservices.FileImporter.Producer.FileProcessStrategies
{
    public abstract class FileProcessStrategy
    {
        public abstract void Process(DataTable dataSet, string fileName);

        protected void SendMessage(FileImporterMessage message)
        {
            try
            {
                BusEasyNetQFactory busEasyNetQFactory = new BusEasyNetQFactory();
                var bus = busEasyNetQFactory.CreateContext();

                bus.Connect();
                bus.Enqueue<FileImporterMessage>("GE-ImportacaoArquivos", message);
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
