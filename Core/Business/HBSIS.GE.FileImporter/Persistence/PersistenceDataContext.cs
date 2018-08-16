using HBSIS.Framework.Data.Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence
{
    public class PersistenceDataContext : DapperDataContext
    {
        private ClienteRepository _clienteRepository;
        private ConfiguracaoRepository _configuracaoRepository;
        private ClienteCelularRepository _clienteCelularRepository;
        private LinhaImportacaoArquivoRepository _linhaImportacaoArquivoRepository;
        
        public ClienteRepository ClienteRepository
        {
            get
            {
                if (_clienteRepository == null)
                    _clienteRepository = new ClienteRepository(ConnectionString);

                return _clienteRepository;
            }
        }

        public ConfiguracaoRepository ConfiguracaoRepository
        {
            get
            {
                if (_configuracaoRepository == null)
                    _configuracaoRepository = new ConfiguracaoRepository(ConnectionString);

                return _configuracaoRepository;
            }
        }

        public ClienteCelularRepository ClienteCelularRepository
        {
            get
            {
                if (_clienteCelularRepository == null)
                    _clienteCelularRepository = new ClienteCelularRepository(ConnectionString);

                return _clienteCelularRepository;
            }
        }

        public LinhaImportacaoArquivoRepository LinhaImportacaoArquivoRepository
        {
            get
            {
                if (_linhaImportacaoArquivoRepository == null)
                    _linhaImportacaoArquivoRepository = new LinhaImportacaoArquivoRepository(ConnectionString);

                return _linhaImportacaoArquivoRepository;
            }
        }
    }
}
