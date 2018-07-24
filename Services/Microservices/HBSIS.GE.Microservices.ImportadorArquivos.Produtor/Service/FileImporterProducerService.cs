using HBSIS.GE.FileImporter.Services.Commons.Base.Service;
using HBSIS.GE.FileImporter.Services.Persistence;
using HBSIS.Framework.Commons.Result;
using HBSIS.GE.Microservices.FileImporter.Producer.Utils;
using HBSIS.GE.FileImporter.Services.Messages.Message;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Text;
using System.Linq;
using HBSIS.Framework.Commons.Config;
using System;
using HBSIS.Framework.Commons.Helper;
using HBSIS.GE.Microservices.FileImporter.Producer.FileProcessStrategies;
using System.Collections.Generic;

namespace HBSIS.GE.Microservices.FileImporter.Producer.Service
{
    public class FileImporterProducerService : BusinessService<FileImporterMessage>
    {
        private string _filePath;
        private string _sentFiles;
        private PersistenceDataContext _dbContext;
        private IConfiguration _configurator;
        private List<string> _lockedFiles;

        /// <summary>
        /// Key: Nome do tipo do arquivo a ser processado.
        /// Value: A partir do nome do tipo do arquivo irá se obter o strategy correspondente
        /// </summary>
        private Dictionary<string, FileProcessStrategy> _singletonFileProcessStrategies;

        public FileImporterProducerService(IConfiguration configurator)
        {
            _dbContext = new PersistenceDataContext();
            _configurator = configurator;

            _filePath = configurator.Get<string>("FWK_FILEIMPORTER_PATH");
            _sentFiles = configurator.Get<string>("FWK_SENTFILES_PATH");

            CreateDirectoryIfNotExists(_filePath);
            CreateDirectoryIfNotExists(_sentFiles);

            _lockedFiles = new List<string>();

            _singletonFileProcessStrategies = new Dictionary<string, FileProcessStrategy>();
            _singletonFileProcessStrategies.Add("GE-CLIENTES-01-", new ClienteFileProcess());
        }

        private void CreateDirectoryIfNotExists(string path)
        {
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void FileProcess()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_filePath);
            var directoryFiles = Directory
                .EnumerateFiles(_filePath)
                .Where(file => file.ToLower().EndsWith(".xls")
                    || file.ToLower().EndsWith(".xlsx")
                    || file.ToLower().EndsWith(".csv"));

            // Registra o encoding para funcionar o ExcelDataReader no dotnet core
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            foreach (var filePath in directoryFiles)
            {
                try
                {
                    string fileName = GetFileName(filePath);

                    if (File.Exists(filePath) && !IsFileLocked(fileName))
                    {
                        bool processedFile = false;

                        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                        {
                            var excelDataSet = ConvertExcelToDataSet(stream);

                            // Seleciona a strategy a ser utilizada através do nome do arquivo lido
                            foreach (var fileProcessPair in _singletonFileProcessStrategies)
                            {
                                if (stream.Name.ToLower().Contains(fileProcessPair.Key.ToLower()))
                                {
                                    var strategy = fileProcessPair.Value;
                                    strategy.Process(excelDataSet, fileName);

                                    processedFile = true;
                                    break;
                                }
                            }
                        }

                        if (processedFile)
                        {
                            System.IO.File.Move(filePath, _sentFiles + fileName);
                            _lockedFiles.Remove(fileName);
                        }
                    }
                }

                catch (Exception ex)
                {
                    LoggerHelper.Error($"{ex.Message} - INNER EXCEPTION: {ex.InnerException?.ToString()}");
                }
            }
        }

        protected override Result Process(FileImporterMessage message)
        {
            throw new NotImplementedException();
        }

        private bool IsFileLocked(string fileName)
        {
            if (_lockedFiles.Contains(fileName))
                return true;

            _lockedFiles.Add(fileName);
            return false;
        }
        
        private DataSet ConvertExcelToDataSet(FileStream stream)
        {
            IExcelDataReader excelReader;

            if (stream.Name.Contains(".xlsx"))
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            else if (stream.Name.Contains(".csv"))
            {
                excelReader = ExcelReaderFactory.CreateCsvReader(stream);
            }

            else
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }

            // Converte o arquivo do excel convertendo-o para um DataSet.
            // UseHeaderRow = true => Indica que a primeira linha contém dados de cabeçalho que deverão ser ignorados
            DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            return result;
        }

        private string GetFileName(string filePath)
        {
            var splitedFilePath = filePath.Split(@"\");

            if (splitedFilePath.Count() == 0)
                throw new Exception("Invalid file name.");

            return splitedFilePath[splitedFilePath.Count() - 1];
        }

    }
}