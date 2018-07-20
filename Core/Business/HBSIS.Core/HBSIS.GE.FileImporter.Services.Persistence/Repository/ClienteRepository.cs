using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class ClienteRepository : DapperRepository<Cliente, Guid>, IClienteRepository<Cliente>
    {

        public ClienteRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Cliente> GetAll()
        {
            return base.GetAll("TB_CLIENTE");
        }

        public Cliente Get(long cdCliente)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<Cliente>("SELECT * FROM OPMDM.TB_CLIENTE WHERE CdCliente = @CdCliente",
                    new { CdCliente = cdCliente }).FirstOrDefault();
            }
        }

        public Cliente GetByCodigoClienteNegocio(string cdClienteNegocio)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                return dapperConnection.Query<Cliente>("SELECT * FROM OPMDM.TB_CLIENTE WHERE CdClienteNegocio = @CdClienteNegocio",
                    new { CdClienteNegocio = cdClienteNegocio }).FirstOrDefault();
            }
        }

        public override void Update(Cliente cliente)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                dapperConnection.ExecuteScalar(@"
                    UPDATE [OPMDM].[TB_CLIENTE]
                    SET DtInicioExpediente = @DtInicioExpediente,
                        DtFimExpediente = @DtFimExpediente
                        DtInicioExpedienteAlternativo = @DtInicioExpedienteAlternativo,
                        DtFimExpedienteAlternativo = @DtFimExpedienteAlternativo,
                        IdDiasRestricao = @IdDiasRestricao,
                        IdPotencialCliente = @IdPotencialCliente,
                        DsObservacao = @DsObservacao,
                        TempoAtendimentoEntrega = @TempoAtendimentoEntrega,
                        TempoTratativaDevEntrega = @TempoTratativaDevEntrega
                    WHERE CdClienteNegocio = @CdClienteNegocio",
                    new
                    {
                        CdClienteNegocio = cliente.CdClienteNegocio,
                        DtInicioExpediente = cliente.DtInicioExpediente,
                        DtFimExpediente = cliente.DtFimExpediente,
                        DtInicioExpedienteAlternativo = cliente.DtInicioExpedienteAlternativo,
                        DtFimExpedienteAlternativo = cliente.DtFimExpedienteAlternativo,
                        IdDiasRestricao = cliente.IdDiasRestricao,
                        IdPotencialCliente = cliente.IdPotencialCliente,
                        DsObservacao = cliente.DsObservacao,
                        TempoAtendimentoEntrega = cliente.TempoAtendimentoEntrega,
                        TempoTratativaDevEntrega = cliente.TempoTratativaDevEntrega
                    });
            }
        }

        public long InsertCliente(Cliente cliente)
        {
            long cdCliente = 0;

            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                cdCliente = dapperConnection.Query<long>(@"
                    INSERT INTO [OPMDM].[TB_CLIENTE]
                               ([CdPontoInteresse], [NmCliente], [DsEndereco], [NmBairro], [NmCidade], [NmEstado], [NmContato], [NrTelefoneContato], 
                                [NrCelularContato], [IdAtivo], [IdTipoLocal], [DtInicioExpediente], [DtFimExpediente], [CdEmpresa], [CdUnidadeNegocio],
                                [IdEnviarNotificacaoSms], [CdUsuario], [IdUtilizaAplicativoAcompanhamento], [DtInicioExpedienteAlternativo], 
                                [DtFimExpedienteAlternativo], [VlAprovado], [IdTipoValorDescarga], [CdClienteNegocio], [IdTipoCliente], [NmDocumento],
                                [IdDiasRestricao], [IdPotencialCliente], [DsObservacao], [TempoAtendimentoEntrega], [TempoTratativaDevEntrega], [IdUnidadeMedida])
                    VALUES (@CdPontoInteresse, @NmCliente, @DsEndereco, @NmBairro, @NmCidade, @NmEstado, @NmContato, @NrTelefoneContato, 
                            @NrCelularContato, @IdAtivo, @IdTipoLocal, @DtInicioExpediente, @DtFimExpediente, @CdEmpresa, @CdUnidadeNegocio, 
                            @IdEnviarNotificacaoSms, @CdUsuario, @IdUtilizaAplicativoAcompanhamento, @DtInicioExpedienteAlternativo, 
                            @DtFimExpedienteAlternativo, @VlAprovado, @IdTipoValorDescarga, @CdClienteNegocio, @IdTipoCliente, @NmDocumento, 
                            @IdDiasRestricao, @IdPotencialCliente, @DsObservacao, @TempoAtendimentoEntrega, @TempoTratativaDevEntrega, @IdUnidadeMedida);
                    SELECT CAST(SCOPE_IDENTITY() AS bigint",
                    new
                    {
                        CdPontoInteresse = (long?)null,
                        NmCliente = cliente.NmCliente,
                        DsEndereco = cliente.DsEndereco,
                        NmBairro = cliente.NmBairro,
                        NmCidade = cliente.NmCidade,
                        NmEstado = cliente.NmEstado,
                        NmContato = (string)null,
                        NrTelefoneContato = (string)null,
                        NrCelularContato = (string)null,
                        IdAtivo = 1,
                        IdTipoLocal = 1,
                        DtInicioExpediente = cliente.DtInicioExpediente,
                        DtFimExpediente = cliente.DtFimExpediente,
                        CdEmpresa = 1,
                        CdUnidadeNegocio = (long?)null, //NÂO PODE NULL
                        IdEnviarNotificacaoSms = 1,
                        CdUsuario = (long?)null,
                        IdUtilizaAplicativoAcompanhamento = 1, // PRECISA VALIDAR
                        DtInicioExpedienteAlternativo = cliente.DtInicioExpedienteAlternativo,
                        DtFimExpedienteAlternativo = cliente.DtFimExpedienteAlternativo,
                        VlAprovado = (decimal?)null,
                        IdTipoValorDescarga = (short?)null,
                        CdClienteNegocio = cliente.CdClienteNegocio,
                        IdTipoCliente = 1, // PRECISA VALIDAR físico ou jurídico
                        NmDocumento = (string)null,
                        IdDiasRestricao = cliente.IdDiasRestricao,
                        IdPotencialCliente = cliente.IdPotencialCliente,
                        DsObservacao = cliente.DsObservacao,
                        TempoAtendimentoEntrega = cliente.TempoAtendimentoEntrega,
                        TempoTratativaDevEntrega = cliente.TempoTratativaDevEntrega,
                        IdUnidadeMedida = (int?)null
                    }).Single();
            }

            return cdCliente;
        }
    }
}
