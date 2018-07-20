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
    public class OcorrenciaRepository : DapperRepository<Ocorrencia, Guid>, IOcorrenciaRepository<Ocorrencia>
    {
        private static string _queryOcorrenciasCompletasOrdenadoDtInclusao = 
            @"SELECT OCORRENCIA.*, PARADA.*, PIN.*, CAT_PIN.* FROM OPMDM.TB_OCORRENCIA OCORRENCIA
            LEFT JOIN OPMDM.TB_PARADA PARADA ON OCORRENCIA.CdParada = PARADA.CdParada
            LEFT JOIN OPMDM.TB_PONTO_INTERESSE PIN ON PARADA.CdPontoInteresse = PIN.CdPontoInteresse
            LEFT JOIN OPMDM.TB_CATEGORIA_PONTO_INTERESSE CAT_PIN ON PIN.CdCategoriaPontoInteresse = CAT_PIN.CdCategoriaPontoInteresse
            WHERE OCORRENCIA.CdRota = @CdRota
            ORDER BY OCORRENCIA.DtInclusao";

        private static string _queryOcorrenciasCompletas =
            @"SELECT OCORRENCIA.*, PARADA.*, PIN.*, CAT_PIN.* FROM OPMDM.TB_OCORRENCIA OCORRENCIA
            LEFT JOIN OPMDM.TB_PARADA PARADA ON OCORRENCIA.CdParada = PARADA.CdParada
            LEFT JOIN OPMDM.TB_PONTO_INTERESSE PIN ON PARADA.CdPontoInteresse = PIN.CdPontoInteresse
            LEFT JOIN OPMDM.TB_CATEGORIA_PONTO_INTERESSE CAT_PIN ON PIN.CdCategoriaPontoInteresse = CAT_PIN.CdCategoriaPontoInteresse
            WHERE OCORRENCIA.CdRota = @CdRota";

        public OcorrenciaRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public IEnumerable<Ocorrencia> GetAll()
        {
            return base.GetAll("TB_OCORRENCIA");
        }

        public IEnumerable<Ocorrencia> GetOcorrencias(long cdRota)
        {
            using (var dapperConnection = AbreConexao())
            {
                var ocorrencias = dapperConnection
                    .Query<Ocorrencia>("SELECT * FROM OPMDM.TB_OCORRENCIA WHERE CdRota = @CdRota ORDER BY CdOcorrencia", new { CdRota = cdRota });

                return ocorrencias;
            }
        }

        public IEnumerable<Ocorrencia> GetOcorrenciasCompletasOrdenadoDtInclusao(long cdRota)
        {
            return GetOcorrenciasCompletas(cdRota, _queryOcorrenciasCompletasOrdenadoDtInclusao);
        }

        public IEnumerable<Ocorrencia> GetOcorrenciasCompletas(long cdRota)
        {
            return GetOcorrenciasCompletas(cdRota, _queryOcorrenciasCompletas);
        }

        private IEnumerable<Ocorrencia> GetOcorrenciasCompletas(long cdRota, string query)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                // Multi-Mapping
                List<Ocorrencia> ocorrencias = dapperConnection.Query<Ocorrencia>(query,
                    new[]
                    {
                        typeof(Ocorrencia),
                        typeof(Parada),
                        typeof(PontoInteresse),
                        typeof(CategoriaPontoInteresse)
                    },
                    objects =>
                    {
                        Ocorrencia Ocorrencia = objects[0] as Ocorrencia;
                        Parada Parada = objects[1] as Parada;
                        PontoInteresse PontoInteresse = objects[2] as PontoInteresse;
                        CategoriaPontoInteresse CategoriaPontoInteresse = objects[3] as CategoriaPontoInteresse;

                        if(PontoInteresse != null)
                            PontoInteresse.CategoriaPontoInteresse = CategoriaPontoInteresse;

                        if(Parada != null)
                            Parada.PontoInteresse = PontoInteresse;

                        if(Ocorrencia != null)
                            Ocorrencia.Parada = Parada;

                        return Ocorrencia;
                    },
                    splitOn: "CdOcorrencia, CdParada, CdPontoInteresse, CdCategoriaPontoInteresse",
                    param: new { CdRota = cdRota }).AsList();

                return ocorrencias;
            }
        }

        public IEnumerable<Ocorrencia> GetOcorrencias(long cdRota, string descricaoCategoriaPontoInteresse)
        {
            using (var dapperConnection = AbreConexao())
            {
                dapperConnection.Open();

                // Multi-Mapping
                List<Ocorrencia> ocorrencias = dapperConnection.Query<Ocorrencia>(
                    @"SELECT CAT_PIM.*, PIN.*, PARADA.*, OCORRENCIA.* FROM TB_OCORRENCIA OCORRENCIA
                        LEFT JOIN TB_PARADA PARADA ON OCORRENCIA.CdParada = PARADA.CdParada
                        LEFT JOIN TB_PONTO_INTERESSE PIN ON PARADA.CdPontoInteresse = PIN.CdPontoInteresse
                        LEFT JOIN TB_CATEGORIA_PONTO_INTERESSE CAT_PIN ON PIN.CdCategoriaPontoInteresse = CAT_PIN.CdCategoriaPontoInteresse
                        WHERE OCORRENCIA.CdRota = @CdRota AND CAT_PIN.DsCategoriaPontoInteresse LIKE '%' + @DsCategoriaPontoInteresse  + '%';",
                    new[]
                    {
                        typeof(Ocorrencia),
                        typeof(Parada),
                        typeof(PontoInteresse),
                        typeof(CategoriaPontoInteresse)
                    },
                    objects =>
                    {
                        Ocorrencia Ocorrencia = objects[0] as Ocorrencia;
                        Parada Parada = objects[1] as Parada;
                        PontoInteresse PontoInteresse = objects[2] as PontoInteresse;
                        CategoriaPontoInteresse CategoriaPontoInteresse = objects[3] as CategoriaPontoInteresse;

                        PontoInteresse.CategoriaPontoInteresse = CategoriaPontoInteresse;
                        Parada.PontoInteresse = PontoInteresse;
                        Ocorrencia.Parada = Parada;

                        return Ocorrencia;
                    },
                    splitOn: "CdParada, CdPontoInteresse, CdCategoriaPontoInteresse",
                    param: new { CdRota = cdRota, DsCategoriaPontoInteresse = descricaoCategoriaPontoInteresse }).AsList();

                return ocorrencias;
            }
        }
    }
}
