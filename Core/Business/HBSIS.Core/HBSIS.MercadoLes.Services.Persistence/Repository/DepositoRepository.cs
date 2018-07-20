using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.MercadoLes.Services.Persistence.IRepository;

namespace HBSIS.MercadoLes.Services.Persistence.Repository
{
    public class DepositoRepository : DapperRepository<Deposito, Guid>, IDepositoRepository<Deposito>
    {

        public DepositoRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Deposito> GetAll()
        {
            return base.GetAll(Deposito.TableName);
        }
        
        public IEnumerable<Deposito> GetDepositosComGeoCoordenadas(string cdUnidadeNegocio)
        {
            using (var dapperConnection = AbreConexao())
            {
                PersistenceDataContext persistence = new PersistenceDataContext();

                dapperConnection.Open();

                var depositosUnidadeNegocio = dapperConnection
                    .Query<Deposito>(@"SELECT * FROM OPMDM.TB_DEPOSITO Deposito
                                    INNER JOIN OPMDM.TB_PONTO_INTERESSE PontoInteresse ON PontoInteresse.CdPontoInteresse = Deposito.CdPontoInteresse
                                    WHERE Deposito.CdUnidadeNegocio = @CdUnidadeNegocio",
                    new[]
                    {
                        typeof(Deposito),
                        typeof(PontoInteresse)
                    },
                    objects =>
                    {
                        Deposito Deposito = objects[0] as Deposito;
                        PontoInteresse PontoInteresse = objects[1] as PontoInteresse;

                        if (Deposito != null)
                            Deposito.PontoInteresse = PontoInteresse;

                        return Deposito;
                    },
                    splitOn: "CdPontoInteresse",
                    param: new { CdUnidadeNegocio = cdUnidadeNegocio }).AsList();

                return depositosUnidadeNegocio;
            }
        }
    }
}
