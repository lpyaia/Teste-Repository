﻿using HBSIS.GE.FileImporter.Services.Commons.Base.Service;
using HBSIS.GE.FileImporter.Services.Persistence;
using HBSIS.Framework.Commons.Result;
using HBSIS.Framework.Commons.Helper;
using HBSIS.GE.Microservices.FileImporter.Consumer.Utils;
using HBSIS.GE.FileImporter.Services.Messages.Message;
using System.Collections.Generic;
using HBSIS.GE.Microservices.FileImporter.Consumer.FileImporterStrategies;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;

namespace HBSIS.GE.Microservices.FileImporter.Consumer.Service
{
    public class FileImporterConsumerService : BusinessService<FileImporterMessage>
    {
        private const int _tentativas = 10;
        private const int _tempoEspera = 30 * 1000;
        private PersistenceDataContext _dbContext;
        private IFileImporterConsumerConfigurator _configurator;

        /// <summary>
        /// Key: Nome do tipo do arquivo a ser processado.
        /// Value: A partir do nome do tipo do arquivo irá se obter o strategy correspondente
        /// </summary>
        private Dictionary<string, IFileImporterStrategy> _singletonFileProcessStrategies;

        public FileImporterConsumerService(IFileImporterConsumerConfigurator configurator)
        {
            _dbContext = new PersistenceDataContext();
            _configurator = configurator;

            _singletonFileProcessStrategies = new Dictionary<string, IFileImporterStrategy>();
            _singletonFileProcessStrategies.Add("GE-CLIENTES-01-", new ClienteFileImporter());
        }
        
        protected override Result Process(FileImporterMessage message)
        {
            foreach(var strategy in _singletonFileProcessStrategies)
            {
                if(strategy.Key.Contains(message.FileType))
                {
                    ((FileImporterStrategy<SpreadsheetLineBase>)strategy.Value).ImportData(message.Data);
                    break;
                }
            }

            return ResultBuilder.Success();
        }
        
        protected override Result ValidateMessage(FileImporterMessage message)
        {
            //if (Guid.Empty.Equals(message.IdTransporteParada)) return ResultBuilder.Warning(ValidationMessages.ParadaNaoInformada);

            //if (string.IsNullOrEmpty(message.EventName)) return ResultBuilder.Warning(ValidationMessages.OperacaoInvalida);

            return ResultBuilder.Success();
        }
    }
}