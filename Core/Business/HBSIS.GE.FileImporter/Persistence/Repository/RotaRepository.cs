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
    public class RotaRepository : DapperRepository<Rota, Guid>, IRotaRepository<Rota>
    {

        public RotaRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Rota> GetAll()
        {
            return base.GetAll("TB_ROTA");
        }

        //public Rota Get(long cdRota)
        //{
        //    using (var dapperConnection = DbConnection)
        //    {
        //        PersistenceDataContext persistence = new PersistenceDataContext();

        //        dapperConnection.Open();

        //        var rota = dapperConnection.Query<Rota>("SELECT Rota.* FROM TB_ROTA AS Rota WHERE CdRota = @CdRota", new { CdRota = cdRota }).FirstOrDefault();

        //        rota.Entregas = persistence.EntregaRepository.GetByRota(rota.CdRota).OrderBy(_ => _.CdCliente).ToList();
        //        rota.Transportadora = persistence.TransportadoraRepository.Get(rota.CdTransportadora);

        //        return rota;
        //    }
        //}

        public Rota Get(long cdRota)
        {
            using (var dapperConnection = AbreConexao())
            {
                var rota = dapperConnection
                    .Query<Rota>("SELECT * FROM OPMDM.TB_ROTA WHERE CdRota = @CdRota", new { CdRota = cdRota })
                    .FirstOrDefault();

                return rota;
            }
        }

        public Rota GetByCdRotaNegocio(long cdRotaNegocio)
        {
            using (var dapperConnection = AbreConexao())
            {
                var rota = dapperConnection
                    .Query<Rota>("SELECT * FROM OPMDM.TB_ROTA WHERE CdRotaNegocio = @CdRotaNegocio", new { CdRotaNegocio = cdRotaNegocio })
                    .FirstOrDefault();

                return rota;
            }
        }

        public Rota GetRotaIndicadoresFluxoLES(long cdRota)
        {
            // Rota com acesso a one to many
            // Key: cdRota
            Dictionary<long, Rota> rota = new Dictionary<long, Rota>();

            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                // Multi-Mapping
                List<Rota> result = dapperConnection.Query<Rota>(
                    @"SELECT Rota.*, Transportadora.*, Entrega.*, SolicitacaoDescarga.*, MotivoDevolucao.*, Cliente.*, UnidadeNegocio.* FROM OPMDM.TB_ROTA AS Rota
                        LEFT JOIN OPMDM.TB_TRANSPORTADORA AS Transportadora ON Rota.CdTransportadora = Transportadora.CdTransportadora
                        LEFT JOIN OPMDM.TB_ENTREGA Entrega ON Rota.CdRota = Entrega.CdRota
                        LEFT JOIN OPMDM.TB_SOLICITACAO_DESCARGA SolicitacaoDescarga ON Entrega.CdEntrega = SolicitacaoDescarga.CdEntrega
                        LEFT JOIN OPMDM.TB_MOTIVO_DEVOLUCAO MotivoDevolucao ON Entrega.CdMotivoDevolucaoCA = MotivoDevolucao.CdMotivoDevolucao
                        LEFT JOIN OPMDM.TB_CLIENTE Cliente ON Entrega.CdCliente = Cliente.CdCliente
                        LEFT JOIN OPMDM.TB_UNIDADE_NEGOCIO UnidadeNegocio ON UnidadeNegocio.CdUnidadeNegocio = Rota.CdUnidadeNegocio
                        WHERE Rota.CdRota = @CdRota
                        ORDER BY Entrega.CdCliente",
                    new[]
                    {
                        typeof(Rota),
                        typeof(Transportadora),
                        typeof(Entrega),
                        typeof(SolicitacaoDescarga),
                        typeof(MotivoDevolucao),
                        typeof(Cliente),
                        typeof(UnidadeNegocio)
                    },
                    objects =>
                    {
                        Rota Rota = objects[0] as Rota;
                        Transportadora Transportadora = objects[1] as Transportadora;
                        Entrega Entrega = objects[2] as Entrega;
                        SolicitacaoDescarga SolicitacaoDescarga = objects[3] as SolicitacaoDescarga;
                        MotivoDevolucao MotivoDevolucao = objects[4] as MotivoDevolucao;
                        Cliente Cliente = objects[5] as Cliente;
                        UnidadeNegocio UnidadeNegocio = objects[6] as UnidadeNegocio;

                        if (!rota.ContainsKey(cdRota))
                        {
                            rota.Add(cdRota, new Rota());
                            rota[cdRota] = Rota;
                            rota[cdRota].Transportadora = Transportadora;
                            rota[cdRota].UnidadeNegocio = UnidadeNegocio;
                        }

                        if (Entrega != null)
                        {
                            Entrega.SolicitacaoDescarga = SolicitacaoDescarga;
                            Entrega.MotivoDevolucao = MotivoDevolucao;
                            Entrega.Cliente = Cliente;
                        }

                        rota[cdRota].Entregas.Add(Entrega);

                        return Rota;
                    },
                    splitOn: "CdTransportadora, CdEntrega, CdSolicitacaoDescarga, CdMotivoDevolucao, CdPontoInteresse, CdUnidadeNegocio",
                    param: new { CdRota = cdRota }).AsList();

                return rota[cdRota];
            }
        }
    }
}
