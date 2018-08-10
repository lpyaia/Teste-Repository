using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HBSIS.MercadoLes.Persistence.IRepository;

namespace HBSIS.MercadoLes.Persistence.Repository
{
    public class EntregaRepository : DapperRepository<Entrega, Guid>, IEntregaRepository<Entrega>
    {
        private PersistenceDataContext persistence;

        public EntregaRepository(string _dbConnectionString) : base(_dbConnectionString)
        {
            persistence =new PersistenceDataContext();
        }

        public IEnumerable<Entrega> GetAll()
        {
            return base.GetAll("TB_ENTREGA");
        }

        public IEnumerable<Entrega> EntregasComUnidadeNegocio(List<Entrega> entregas)
        {
            entregas.ForEach(entrega => {
                entrega.UnidadeNegocio = persistence.UnidadeNegocioRepository.GetUnidadesNegocio(entrega.CdUnidadeNegocio).FirstOrDefault();

            });

            return entregas;
        }

        public IEnumerable<Entrega> EntregasComCliente(List<Entrega> entregas)
        {
            entregas.ForEach(entrega => {
                entrega.Cliente = persistence.ClienteRepository.Get(entrega.CdCliente);

            });

            return entregas;
        }

        public IEnumerable<Entrega> GetByRota(long cdRota)
        {
            using (var dapperConnection = AbreConexao())
            {
                PersistenceDataContext persistence = new PersistenceDataContext();

                dapperConnection.Open();

                var entregas = dapperConnection.Query<Entrega>("SELECT * FROM OPMDM.TB_ENTREGA WHERE CdRota = @CdRota",
                    new { CdRota = cdRota }).OrderBy(entrega => entrega.CdCliente).ToList();

                entregas.ForEach(entrega =>
                {
                    entrega.MotivoDevolucao = persistence.MotivoDevolucaoRepository.Get(entrega.CdMotivoDevolucao);
                    entrega.SolicitacaoDescarga = persistence.SolicitacaoDescargaRepository.Get(entrega.CdEntrega);
                    entrega.Cliente = persistence.ClienteRepository.Get(entrega.CdCliente);
                });

                return entregas;
            }
        }
    }
}
