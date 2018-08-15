using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.EasyNetQRabbit;
using HBSIS.GE.FileImporter.Services.Messages.Message;
using HBSIS.GE.FileImporter.Services.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HBSIS.GE.FileImporter.Producer.FileProcessStrategies
{
    public abstract class FileProcessStrategy
    {
        private BusEasyNetQFactory _busEasyNetQFactory = new BusEasyNetQFactory();
        private IBusContext _busContext;

        public FileProcessStrategy()
        {
            _busContext = _busEasyNetQFactory.CreateContext();
            _busContext.Connect();
        }

        public abstract void Process(DataTable dataSet, string fileName);

        protected void SendMessage(FileImporterMessage message)
        {
            try
            {
                _busContext.Enqueue<FileImporterMessage>("GE-ImportacaoArquivos", message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
