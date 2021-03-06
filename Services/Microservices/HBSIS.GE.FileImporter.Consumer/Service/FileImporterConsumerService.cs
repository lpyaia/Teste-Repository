﻿using HBSIS.GE.FileImporter.Services.Commons.Base.Service;
using HBSIS.GE.FileImporter.Services.Persistence;
using HBSIS.Framework.Commons.Result;
using HBSIS.Framework.Commons.Helper;
using HBSIS.GE.FileImporter.Consumer.Utils;
using HBSIS.GE.FileImporter.Services.Messages.Message;
using System.Collections.Generic;
using HBSIS.GE.FileImporter.Consumer.FileImporterStrategies;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using System.Net.Http;
using HBSIS.Framework.Commons.Config;
using Newtonsoft.Json;
using System.Text;
using System;
using HBSIS.GE.FileImporter.Consumer.Extensions;
using System.Threading;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Consumer.Service
{
    public class FileImporterConsumerService : BusinessService<FileImporterMessage>
    {
        private PersistenceDataContext _dbContext;
        private IConfiguration _configurator;
        private string _endpointWebServiceGE;
        private HttpClient client;
        private Thread[] threads;
        private int limitThread = 4;

        /// <summary>
        /// Key: Nome do tipo do arquivo a ser processado.
        /// Value: A partir do nome do tipo do arquivo irá se obter o strategy correspondente
        /// </summary>
        private Dictionary<string, IFileImporterStrategy> _singletonFileProcessStrategies;

        public FileImporterConsumerService(IConfiguration configurator)
        {
            _dbContext = new PersistenceDataContext();
            _configurator = configurator;

            _endpointWebServiceGE = configurator.Get<string>("FWK_ENDPOINTWSGE");

            _singletonFileProcessStrategies = new Dictionary<string, IFileImporterStrategy>();
            _singletonFileProcessStrategies.Add("GE-CLIENTES-01-", new ClienteFileImporter());

            client = new HttpClient();
            client.BaseAddress = new System.Uri(_endpointWebServiceGE);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override Result Process(FileImporterMessage message)
        {
            bool erroImportacaoLinha = false;

            try
            {
                ConsumeMessage(message);   
            }

            catch (Exception ex)
            {
                erroImportacaoLinha = true;
                LoggerHelper.Error($"Process => {ex.Message} - INNER EXCEPTION: {ex.GetInnerExceptionMessage()}");
                throw ex;
            }

            finally
            {
                LinhaImportacaoArquivo linhaImportacao = new LinhaImportacaoArquivo()
                {
                    DsConteudoLinha = message.Data.JsonSerialize(),
                    DsNomeArquivo = message.FileName,
                    DtInclusao = DateTime.Now,
                    IdErroImportacao = erroImportacaoLinha,
                    VlNumeroLinha = message.CountRows,
                    VlTotalLinhasArquivo = message.TotalFileRows,
                    // Deixar variável
                    IdTipoImportacao = 1
                };

                _dbContext.LinhaImportacaoArquivoRepository.Insert(linhaImportacao);
            }

            return ResultBuilder.Success();
        }

        private void ConsumeMessage(FileImporterMessage message)
        {
            if (message.FileType.ToUpper().Contains("GE-CLIENTES-01-"))
            {
                ClienteSpreadsheetLine clienteMessage = (ClienteSpreadsheetLine)message.Data;

                ClienteFileImporter clienteFileImporter = new ClienteFileImporter();
                clienteFileImporter.ImportData(clienteMessage);
            }

            var data = new { fileName = message.FileName, totalFileRows = message.TotalFileRows };
            string jsonData = JsonConvert.SerializeObject(data);

            client.PostAsync("Servico.svc/servico/AtualizarStatusImportacaoArquivos", new StringContent(jsonData, Encoding.UTF8, "application/json"));
        }

        protected override Result ValidateMessage(FileImporterMessage message)
        {
            return ResultBuilder.Success();
        }
    }
}