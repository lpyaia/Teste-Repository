using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using HBSIS.GE.FileImporter.Infra.Entities;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class ClienteCelularRepository : DapperRepository<ClienteCelular, Guid>, IClienteCelularRepository<ClienteCelular>
    {

        public ClienteCelularRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<ClienteCelular> GetAll()
        {
            return base.GetAll("TB_CLIENTE_CELULAR");
        }

        public ClienteCelular Get(long cdCliente)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<ClienteCelular>("SELECT * FROM OPMDM.TB_CLIENTE_CELULAR WHERE CdCliente = @CdCliente",
                    new { CdCliente = cdCliente }).FirstOrDefault();
            }
        }

        public ClienteCelular GetByNumeroCelularAndCdCliente(long cdCliente, string telefone)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<ClienteCelular>(@"
                    SELECT * FROM OPMDM.TB_CLIENTE_CELULAR
                    WHERE CdCliente = @CdCliente AND NrCelular = @NrCelular",
                    new { CdCliente = cdCliente, NrCelular = telefone }).FirstOrDefault();
            }
        }

        public void InsertImportacao(ClienteCelular clienteCelular)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                dapperConnection.ExecuteAsync(@"
                    INSERT INTO [OPMDM].[TB_CLIENTE_CELULAR]
                        ([CdCliente], [NrCelular], [DtCriacao], [DtExclusao], [IdExcluido], [NmContato], [IdEnviarSMS])
                    VALUES 
                        (@CdCliente, @NrCelular, @DtCriacao, @DtExclusao, @IdExcluido, @NmContato, @IdEnviarSMS)",
                    new
                    {
                        CdCliente = clienteCelular.CdCliente,
                        NrCelular = clienteCelular.NrCelular,
                        DtCriacao = clienteCelular.DtCriacao,
                        DtExclusao = (DateTime?)null,
                        IdExcluido = clienteCelular.IdExcluido,
                        NmContato = clienteCelular.NmContato,
                        IdEnviarSMS = clienteCelular.IdEnviarSMS
                    });
            }
        }

        public CommandDefinition GetInsertImportacaoCommand(ClienteCelular clienteCelular)
        {
            CommandDefinition commandDefinition = new CommandDefinition(@"
                    INSERT INTO [OPMDM].[TB_CLIENTE_CELULAR]
                        ([CdCliente], [NrCelular], [DtCriacao], [DtExclusao], [IdExcluido], [NmContato], [IdEnviarSMS])
                    VALUES 
                        (@CdCliente, @NrCelular, @DtCriacao, @DtExclusao, @IdExcluido, @NmContato, @IdEnviarSMS)",
                    new
                    {
                        CdCliente = clienteCelular.CdCliente,
                        NrCelular = clienteCelular.NrCelular,
                        DtCriacao = clienteCelular.DtCriacao,
                        DtExclusao = (DateTime?)null,
                        IdExcluido = clienteCelular.IdExcluido,
                        NmContato = clienteCelular.NmContato,
                        IdEnviarSMS = clienteCelular.IdEnviarSMS
                    });

            return commandDefinition;
        }

        public void AtualizarNomeContato(long cdCelular, string nome)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                dapperConnection.ExecuteAsync(@"
                    UPDATE [OPMDM].[TB_CLIENTE_CELULAR]
                    SET NmContato = @NmContato
                    WHERE CdCelular = @CdCelular",
                    new { CdCelular = cdCelular, NmContato = nome });
            }
        }

        public CommandDefinition GetAtualizarNomeContadoCommand(long cdCelular, string nome, bool enviarSMS)
        {
            CommandDefinition commandDefinition = new CommandDefinition(@"
                    UPDATE[OPMDM].[TB_CLIENTE_CELULAR]
                    SET NmContato = @NmContato, IdExcluido = 0, IdEnviarSMS = @EnviarSMS
                    WHERE CdCelular = @CdCelular",
                    new { CdCelular = cdCelular, NmContato = nome, EnviarSMS = enviarSMS });

            return commandDefinition;
        }
    }
}
