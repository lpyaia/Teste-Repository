using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.GE.FileImporter.Services.Persistence.IRepository;
using HBSIS.GE.FileImporter.Infra.Entities;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
{
    public class BaldeioEntregaRepository : DapperRepository<BaldeioEntrega, Guid>, IBaldeioEntregaRepository<BaldeioEntrega>
    {

        public BaldeioEntregaRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<BaldeioEntrega> GetAll()
        {
            return base.GetAll(BaldeioEntrega.TableName);
        }

        public BaldeioEntrega GetByRota(long cdRota)
        {
            using (var dapperConnection = AbreConexao())
            {
                var baldeioEntrega = dapperConnection
                    .Query<BaldeioEntrega>("SELECT * FROM OPMDM.TB_BALDEIO_ENTREGA WHERE CdRota = @CdRota", new { CdRota = cdRota })
                    .FirstOrDefault();

                return baldeioEntrega;
            }
        }

        public IEnumerable<BaldeioEntrega> GetBaldeiosMultiTransporteByRotaDestino(long cdRotaBaldeada)
        {
            using (var dapperConnection = AbreConexao())
            {
                PersistenceDataContext persistence = new PersistenceDataContext();

                dapperConnection.Open();

                var baldeiosRota = dapperConnection
                    .Query<BaldeioEntrega>(@"SELECT BaldeioEntrega.*, RotaOrigem.* FROM OPMDM.TB_BALDEIO_ENTREGA BaldeioEntrega
                                            INNER JOIN OPMDM.TB_ROTA RotaOrigem ON BaldeioEntrega.CdRotaOrigem = RotaOrigem.CdRota
                                            WHERE BaldeioEntrega.CdRotaDestino = @CdRotaBaldeada",
                    new[]
                    {
                        typeof(BaldeioEntrega),
                        typeof(Rota)
                    },
                    objects =>
                    {
                        BaldeioEntrega BaldeioEntrega = objects[0] as BaldeioEntrega;
                        Rota Rota = objects[1] as Rota;

                        if (BaldeioEntrega != null)
                            BaldeioEntrega.RotaOrigem = Rota;

                        return BaldeioEntrega;
                    },
                    splitOn: "CdRotaNegocio",
                    param: new { CdRotaBaldeada = cdRotaBaldeada }).AsList();

                return baldeiosRota;
            }
        }
    }
}
