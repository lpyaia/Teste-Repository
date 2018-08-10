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
    public class DeslocamentoRotaRepository<TEntity> : DapperRepository<TEntity, Guid>, IDeslocamentoRotaRepository<TEntity>
        where TEntity : Deslocamento, Framework.Commons.Entity.IEntity<Guid>, new()
    {
        private List<Deslocamento> _listDeslocamento;
        private TEntity _deslocamento;
        private long _cdRota;

        public DeslocamentoRotaRepository(string dbConnectionString) : base(dbConnectionString)
        {
            _listDeslocamento = new List<Deslocamento>();
            _deslocamento = new TEntity();
        }

        /// <summary>
        /// Obtém os deslocamentos da PIM. 
        /// </summary>
        /// <param name="cdRota"></param>
        /// <returns>Retorna uma lista de deslocamentos, desde que cada PIM haja início e fim.</returns>
        public List<Deslocamento> GetDeslocamentosPIM(long cdRota)
        {
            _cdRota = cdRota;

            var ocorrenciasInicioPIM = GetCdOcorrenciasInicioPIM(cdRota);
            var ocorrenciasFimPIM = GetCdOcorrenciasFimPIM(cdRota);

            if (IsDeslocamentosValidos(ocorrenciasInicioPIM, ocorrenciasFimPIM))
            {
                for (int i = 0; i < ocorrenciasInicioPIM.Count; i++)
                {
                    long ocorrenciaInicioPIM = ocorrenciasInicioPIM[i].CdOcorrencia;
                    long ocorrenciaFimPIM = ocorrenciasFimPIM[i].CdOcorrencia;

                    _deslocamento = new TEntity();

                    _deslocamento.CdOcorrenciaInicioPIM = ocorrenciaInicioPIM;
                    _deslocamento.CdOcorrenciaFimPIM = ocorrenciaFimPIM;

                    _deslocamento.ValorOdometroInicioPIM = GetValorOdometroInicioPIM(ocorrenciaInicioPIM);
                    _deslocamento.ValorOdometroFimPIM = GetValorOdometroFimPIM(ocorrenciaFimPIM);

                    _deslocamento.ValorOdometroEntregaAntesPIM = GetValorOdometroEntregaAntesPIM(ocorrenciaInicioPIM);
                    _deslocamento.ValorOdometroEntregaDepoisPIM = GetValorOdometroEntregaDepoisPIM(ocorrenciaFimPIM);

                    _deslocamento.LocalizacaoEntregaAntesPIM = GetLocalizacaoEntregaAntesPIM(ocorrenciaInicioPIM);
                    _deslocamento.LocalizacaoEntregaDepoisPIM = GetLocalizacaoEntregaDepoisPIM(ocorrenciaFimPIM);

                    _listDeslocamento.Add(_deslocamento);
                }

                return _listDeslocamento;
            }

            // Se a estrutura de deslocamentos da PIM não estiver OK, retorna null
            return null;
        }
        
        public List<(long CdOcorrencia, long IdOcorrencia)> GetCdOcorrenciasInicioPIM(long CdRota)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<(long CdOcorrencia, long IdOcorrencia)>(@"
                    SELECT CdOcorrencia, IdOcorrencia FROM OPMDM.TB_OCORRENCIA WHERE IdOcorrencia = @IdOcorrenciaInicio AND CdRota = @CdRota ORDER BY CdRota, CdOcorrencia",
                    new { IdOcorrenciaInicio = _deslocamento.IdOcorrenciaInicio, CdRota = _cdRota }).ToList();
            }
        }

        public List<(long CdOcorrencia, long IdOcorrencia)> GetCdOcorrenciasFimPIM(long CdRota)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<(long CdOcorrencia, long IdOcorrencia)>(@"
                    SELECT CdOcorrencia, IdOcorrencia FROM OPMDM.TB_OCORRENCIA WHERE IdOcorrencia = @IdOcorrenciaFim AND CdRota = @CdRota ORDER BY CdRota, CdOcorrencia",
                    new { IdOcorrenciaFim = _deslocamento.IdOcorrenciaFim, CdRota = _cdRota }).ToList();
            }
        }

        private long? GetValorOdometroInicioPIM(long cdOcorrenciaInicioPIM)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<long?>(@"
                    SELECT VlOdometro FROM OPMDM.TB_OCORRENCIA (NOLOCK)
                    WHERE CdOcorrencia = @CdOcorrenciaInicioPIM",
                    new { CdOcorrenciaInicioPIM = cdOcorrenciaInicioPIM, CdRota = _cdRota }).FirstOrDefault();
            }
        }

        private long? GetValorOdometroFimPIM(long cdOcorrenciaFimPIM)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<long?>(@"
                    SELECT VlOdometro FROM OPMDM.TB_OCORRENCIA (NOLOCK)
                    WHERE CdOcorrencia = @CdOcorrenciaFimPIM",
                    new { CdOcorrenciaFimPIM = cdOcorrenciaFimPIM, CdRota = _cdRota }).FirstOrDefault();
            }
        }

        private long? GetValorOdometroEntregaAntesPIM(long cdOcorrenciaInicioPIM)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<long?>(@"
                    SELECT TOP 1 VlOdometro FROM OPMDM.TB_OCORRENCIA (NOLOCK)
                    WHERE IdOcorrencia IN (1,7,2,3) AND
                    CdRota = @CdRota AND
                    CdOcorrencia < @CdOcorrenciaInicioPIM
                    ORDER BY DtInclusao DESC",
                    new { CdOcorrenciaInicioPIM = cdOcorrenciaInicioPIM, CdRota = _cdRota }).FirstOrDefault();
            }
        }

        private long? GetValorOdometroEntregaDepoisPIM(long cdOcorrenciaFimPIM)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<long?>(@"
                    SELECT TOP 1 VlOdometro FROM OPMDM.TB_OCORRENCIA (NOLOCK)
                    WHERE IdOcorrencia IN (1,7,2,3) AND
                    CdRota = @CdRota AND
                    CdOcorrencia > @CdOcorrenciaFimPIM
                    ORDER BY DtInclusao ASC",
                    new { CdOcorrenciaFimPIM = cdOcorrenciaFimPIM, CdRota = _cdRota }).FirstOrDefault();
            }
        }

        private (double Lat, double Lon) GetLocalizacaoEntregaAntesPIM(long cdOcorrenciaInicioPIM)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<(double, double)>(@"
                    SELECT TOP 1 NrLatitude, NrLongitude FROM OPMDM.TB_OCORRENCIA (NOLOCK)
                    WHERE IdOcorrencia IN (1,7,2,3) AND
                    CdRota = @CdRota AND
                    CdOcorrencia < @CdOcorrenciaInicioPIM
                    ORDER BY DtInclusao DESC",
                    new { CdOcorrenciaInicioPIM = cdOcorrenciaInicioPIM, CdRota = _cdRota }).FirstOrDefault();
            }
        }

        private (double lat, double lon) GetLocalizacaoEntregaDepoisPIM(long cdOcorrenciaFimPIM)
        {
            using (var dapperConnection = AbreConexao())
            {
                return dapperConnection.Query<(double, double)>(@"
                    SELECT TOP 1 NrLatitude, NrLongitude FROM OPMDM.TB_OCORRENCIA (NOLOCK)
                    WHERE IdOcorrencia IN (1,7,2,3) AND
                    CdRota = @CdRota AND
                    CdOcorrencia > @CdOcorrenciaFimPIM
                    ORDER BY DtInclusao ASC",
                    new { CdOcorrenciaFimPIM = cdOcorrenciaFimPIM, CdRota = _cdRota }).FirstOrDefault();
            }
        }

        private bool IsDeslocamentosValidos(List<(long CdOcorrencia, long IdOcorrencia)> ocorrenciasInicioPIM, List<(long CdOcorrencia, long IdOcorrencia)> ocorrenciasFimPIM)
        {
            if (ocorrenciasInicioPIM.Count != ocorrenciasFimPIM.Count)
                return false;

            for (int i = 0; i < ocorrenciasInicioPIM.Count; i++)
            {
                if (ocorrenciasInicioPIM[i].IdOcorrencia == ocorrenciasFimPIM[i].IdOcorrencia)
                    return false;
            }

            return true;
        }
    }
}
