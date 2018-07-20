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
    public class ParadasTratadasAnaliticoRepository : DapperRepository<ParadaTratadaAnalitico, Guid>, IParadasTratadasAnaliticoRepository<ParadaTratadaAnalitico>
    {

        public ParadasTratadasAnaliticoRepository(string _dbConnectionString) : base(_dbConnectionString)
        {

        }

        public ParadaTratadaAnalitico Get(long cdRota)
        {
            using (var dapperConnection = AbreConexao())
            {
                PersistenceDataContext persistence = new PersistenceDataContext();
                dapperConnection.Open();

                #region ParadasTratadasAnalitico
                var paradaTratada = dapperConnection.Query<ParadaTratadaAnalitico>(@"
                        BEGIN
	                        with tmpRota as(
                            SELECT 
		                        R.cdRota,
		                        R.CdUnidadeNegocio,
		                        R.CdTransportadora,
		                        R.dtRota,
		                        R.CdRotaNegocio,
		                        R.CdPlacaVeiculo,
		                        R.VlDistanciaTotalPrevista,
		                        R.DtPartidaRealizada,
		                        R.DtChegadaRealizada,
		                        R.VlDistanciaTotalRealizada,
		                        R.NmUsuarioFechamento,
		                        R.QtEntregaPrevista,
		                        R.QtEntregaRealizada,
		                        R.QtEntregaDevolvida,
		                        R.IdTipoFechamento
	                        FROM OPMDM.TB_ROTA R (NOLOCK)

	                        WHERE CdSituacao = 3 AND 
                            IdExpurgada = 0 AND
                            CdRota = @CdRota
	                        ),

	                        tmpOcorrencia as (
	                        SELECT 
		                        O.cdRota,
		                        O.DtInclusao,
		                        O.IdOcorrencia, 
		                        O.CdParada, 
		                        O.IdPosicaoCorreta,
		                        t.IdTipoFechamento
	                        FROM OPMDM.TB_OCORRENCIA O (NOLOCK)
	                        INNER JOIN tmpRota T on T.CdRota = O.CdRota
	                        ),

	                        tmpOcorrenciaInicio as (
		                        select 
		                        ROW_NUMBER() over (partition by cdrota order by dtinclusao) myRow,
		                        * from tmpOcorrencia
		                        where IdOcorrencia in (1)
	                        ),

	                        tmpOcorrenciaFim as (
		                        select 
		                        ROW_NUMBER() over (partition by cdrota order by dtinclusao) myRow,
		                        * 
		                        from tmpOcorrencia
		                        where IdOcorrencia in (7)
	                        ),

	                        tmpOcorrenciaInicioFim as (
	                        select distinct
		                        t.CdRota,
		                        t1.IdPosicaoCorreta PosicaoInicio,
		                        isnull(t2.IdPosicaoCorreta,0) PosicaoFim
	                        from tmpOcorrencia t
	                        inner join tmpOcorrenciaInicio t1 on t1.CdRota = t.CdRota and t1.myRow = 1
	                        left join tmpOcorrenciaFim t2 on t2.CdRota = t.CdRota and t2.myRow = 1
	                        ),

	                        tmpParadaOcorrencia as(
	                        SELECT DISTINCT 
		                        P.CdParada, 
		                        O.CdRota, 
		                        DtInicio
	                        FROM tmpOcorrencia O
	                        left JOIN OPMDM.TB_PARADA P  ON (P.CdParada = O.CdParada)
	                        ),

	                        tmpVinculoRotaParada as (
	                        SELECT DISTINCT 
		                        O.CdParada, 
		                        O.CdRota 
	                        FROM tmpParadaOcorrencia O
	                        INNER JOIN tmpRota R ON R.CdRota = O.CdRota
	                        WHERE (R.DtPartidaRealizada <= O.DtInicio AND R.DtChegadaRealizada >= O.DtInicio)
	                        or O.DtInicio is null
	                        ),

	                        tmpRotaParada as (
	                        select
	                         R.cdRota,
	                         R.CdUnidadeNegocio,
	                         uni.NmUnidadeNegocio,
	                         P.CdPontoInteresse,
	                         R.CdTransportadora,
	                         P.CdMotivoParada,
	                         P.IdJustificado,
	                         R.dtRota,
	                         P.DtInicio,
	                         P.DtFim,
	                         R.CdRotaNegocio,
	                         R.CdPlacaVeiculo,
	                         R.VlDistanciaTotalPrevista,
	                         R.DtPartidaRealizada,
	                         R.DtChegadaRealizada,
	                         R.VlDistanciaTotalRealizada,
	                         R.NmUsuarioFechamento,
	                         R.QtEntregaPrevista,
	                         R.QtEntregaRealizada,
	                         R.QtEntregaDevolvida,
	                         P.IdTipoParada, 
	                         P.QtMinutosParado,
	                         P.IdMonitorado,
	                         case when P.IdTipoParada = 4 then 1 else 0 end AS Qtd_PNP,
	                         case
	                          when p.DsJustificativa = 'PNP Tolerada (ClienteNaRota = Não)' then 1
	                          when p.DsJustificativa = 'Erro de Apontamento (ClienteNaRota = Não)' then 1
	                          when p.DsJustificativa = 'PNP Comportamental (ClienteNaRota = Não)' then 1
	                          else 0
	                         end as Qtd_PNP_Real,
	                         case
	                          when p.DsJustificativa = 'Ocorrências de PNP (ClienteNaRota = Não)' then 1
	                          else 0 
	                         end as 'Qtd_PNP_DeltaKM' 
	                        from tmpVinculoRotaParada VRP

	                        INNER JOIN tmpRota R ON VRP.CdRota = R.CdRota
	                        INNER JOIN OPMDM.TB_UNIDADE_NEGOCIO UNI (NOLOCK) ON UNI.CdUnidadeNegocio = R.CdUnidadeNegocio COLLATE Latin1_General_CI_AI
	                        left JOIN OPMDM.TB_PARADA P (NOLOCK) ON VRP.CdParada = P.CdParada
	                        ),

	                        tmpParadasTratadasAnalitico as (
	                        SELECT distinct
	                         R.dtRota AS 'Data da Rota',
	                         R.CdRotaNegocio AS 'Transporte',
	                         GU.DsGrupoUnidadeNegocio 'Regional',
	                         R.CdUnidadeNegocio AS 'Cd Un. Negócio', 
	                         R.NmUnidadeNegocio AS 'Un. Negócio',
	                         R.CdPlacaVeiculo AS 'Veículo',
	                         T.cdTransportadora AS 'Cd Transportadora',
	                         T.NmTransportadora AS 'Transportadora',
	                         R.VlDistanciaTotalPrevista AS 'KM Previsto',
	                         R.DtPartidaRealizada AS 'Partida realizada',
	                         R.DtChegadaRealizada AS 'Fim realizado',
	                         R.VlDistanciaTotalRealizada AS 'Distância Realizada', 
	                         CASE 
	                          WHEN O.PosicaoInicio = 1 THEN 'SIM' 
	                          ELSE 'NÃO' 
	                         END AS 'Início no Raio',
	                         CASE 
	                          WHEN O.PosicaoFim = 1 THEN 'SIM' 
	                          ELSE 'NÃO' 
	                         END AS 'Fim no Raio', 
	                         R.NmUsuarioFechamento AS 'Usuário Fechamento',
	                         R.QtEntregaPrevista AS 'Quantidade Entrega Prevista',
	                         R.QtEntregaRealizada AS 'Quantidade Entrega Realizada',
	                         R.QtEntregaDevolvida AS 'Quantidade Entrega Devolvida',
	                         case when R.IdTipoParada = 4 then 1 else 0 end AS 'Qtd PNP',
	                         R.Qtd_PNP_Real,
	                         CASE WHEN R.IdTipoParada = 4 AND (R.IdJustificado = 0 OR M.CdMotivoParadaCategoria IN (2, 3, 4))  AND C.CdCliente IS NULL THEN 1 ELSE 0 END as 'Qtd_PNP_DeltaKM',
 
	                         CASE WHEN R.IdTipoParada = 7 THEN QtMinutosParado ELSE 0 END AS 'Repouso Realizado',
	                         CASE WHEN R.IdTipoParada = 6 THEN QtMinutosParado ELSE 0 END AS 'Descanso Realizado',
	                         CASE WHEN R.IdTipoParada = 3 THEN QtMinutosParado ELSE 0 END AS 'Refeição Realizada',
	                         CASE WHEN R.IdTipoParada = 5 THEN QtMinutosParado ELSE 0 END AS 'Espera Para Atend.',
	                         CASE WHEN R.IdTipoParada = 8 THEN QtMinutosParado ELSE 0 END AS 'Abastecimento Realizado',
	                         CASE WHEN R.IdTipoParada = 2 THEN QtMinutosParado ELSE 0 END AS 'Tempo de Atendimento',

	                         CASE WHEN R.IdTipoParada = 4 AND M.DsMotivoParada = 'Abastecimento' 
	                          THEN QtMinutosParado ELSE 0 END AS 'Abastecimento não apontado',
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND M.CdMotivoParadaCategoria = 2 AND C.CdCliente IS NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'PNP Tolerada (ClienteNaRota = Não)',
	                         CASE WHEN R.IdTipoParada = 4 AND (M.CdMotivoParadaCategoria = 3 OR R.IdJustificado = 0) AND C.CdCliente IS NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'Erro de Apontamento (ClienteNaRota = Não)',
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND  M.CdMotivoParadaCategoria = 4 AND C.CdCliente IS NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'PNP Comportamental (ClienteNaRota = Não)',
	                         CASE WHEN R.IdTipoParada = 4 AND (R.IdJustificado = 0 OR M.CdMotivoParadaCategoria IN (2, 3, 4))  AND C.CdCliente IS NULL 
	                          THEN 1 ELSE 0 END AS 'Ocorrências de PNP (ClienteNaRota = Não)',
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND M.CdMotivoParadaCategoria = 2 AND C.CdCliente IS NOT NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'PNP Tolerada (ClienteNaRota = Sim)',
	                         CASE WHEN R.IdTipoParada = 4 AND (M.CdMotivoParadaCategoria = 3 OR R.IdJustificado = 0) AND C.CdCliente IS NOT NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'Erro de Apontamento (ClienteNaRota = Sim)',
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND M.CdMotivoParadaCategoria = 4 AND C.CdCliente IS NOT NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'PNP Comportamental (ClienteNaRota = Sim)', 
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND M.DsMotivoParada IN ('Pernoite não apontada', 'Descanso', 'Pernoite na Cidade', 'Parado no hotel', 'Pernoite não apontada') 
	                          THEN QtMinutosParado ELSE 0 END AS 'Pernoite não apontada', --: Filtrar todas as PNP’S que a justificava da pnp é “Pernoite não apontada”.
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND M.DsMotivoParada IN ('Refeição Não Apontada', 'Almoço', 'Parada para o almoço') 
	                          THEN QtMinutosParado ELSE 0 END AS 'Refeição Não Apontada',--: Filtrar todas as PNP’S que a justificava da pnp é “Refeição Não Apontada”.
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND M.DsMotivoParada = 'Aguardando descarga' AND C.CdCliente IS NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'Aguardando descarga (ClienteNaRota = Não)',-- : Filtrar todas as PNP’S que a justificava da pnp é “Aguardando descarga” e a PNP não está vinculada a nenhum cliente na rota
	                         CASE WHEN R.IdTipoParada = 4 AND R.IdJustificado = 1 AND M.DsMotivoParada = 'Aguardando descarga' AND C.CdCliente IS NOT NULL 
	                          THEN QtMinutosParado ELSE 0 END AS 'Aguardando descarga (ClienteNaRota = Sim)' --: Filtrar todas as PNP’S que a justificava da pnp é “Aguardando descarga” e a PNP está vinculada a um cliente na rota
                              ,R.DtInicio,
	                         R.DtFim
	                        FROM tmpRotaParada R

	                        LEFT JOIN tmpOcorrenciaInicioFim O on O.CdRota = r.CdRota

	                        LEFT JOIN OPMDM.TB_UNIDADE_NEGOCIO (NOLOCK) U ON U.CdUnidadeNegocio = R.CdUnidadeNegocio collate Latin1_General_CI_AI
	                        LEFT JOIN OPMDM.TB_GRUPO_UNIDADE_NEGOCIO_UNIDADE (NOLOCK) G ON G.CdUnidadeNegocio = U.CdUnidadeNegocio COLLATE Latin1_General_CI_AS
	                        LEFT JOIN OPMDM.TB_PONTO_INTERESSE (NOLOCK) POI ON POI.CdPontoInteresse = R.CdPontoInteresse
	                        LEFT JOIN OPMDM.TB_GRUPO_UNIDADE_NEGOCIO (NOLOCK) GU ON G.CdGrupoUnidadeNegocio = GU.CdGrupoUnidadeNegocio

	                        LEFT JOIN OPMDM.TB_TRANSPORTADORA (NOLOCK) T ON T.CdTransportadora = R.CdTransportadora
	                        LEFT JOIN OPMDM.TB_MOTIVO_PARADA (NOLOCK) M ON M.CdMotivoParada = R.CdMotivoParada
	                        LEFT JOIN OPMDM.TB_MOTIVO_PARADA_CATEGORIA (NOLOCK) MPC ON M.CdMotivoParadaCategoria = MPC.CdMotivoParadaCategoria

	                        --verificar esse left, é necessario esse sub select?
	                        LEFT JOIN OPMDM.TB_CLIENTE (NOLOCK) C ON C.CdPontoInteresse = R.CdPontoInteresse --AND C.CdCliente IN (SELECT E.CdCliente FROM STAGE.TB_ENTREGA E WHERE E.CdRota = R.CdRota)
	                        )

	                        SELECT [Data da Rota] AS DataRota
		                        ,[Transporte] AS CdRotaNegocio
		                        ,[Regional]
		                        ,[Cd Un. Negócio] AS CdUnNegocio
		                        ,[Un. Negócio] AS UnNegocio
		                        ,[Veículo] AS Veiculo
		                        ,[Cd Transportadora] AS CdTransportadora
		                        ,[Transportadora]
		                        ,[KM Previsto] AS KmPrevisto
		                        ,[Partida realizada] AS PartidaRealizada
		                        ,[Fim realizado] AS FimRealizado
		                        ,[Distância Realizada] AS DistanciaRealizada
		                        ,[Início no Raio] AS InicioNoRaio
		                        ,[Fim no Raio] AS FimNoRaio
		                        ,[Usuário Fechamento] AS UsuarioFechamento
		                        ,[Quantidade Entrega Prevista] AS QuantidadeEntregaPrevista
		                        ,ISNULL([Quantidade Entrega Realizada],0) AS QuantidadeEntregaRealizada
		                        ,ISNULL([Quantidade Entrega Devolvida],0) AS QuantidadeEntregaDevolvida
		                        ,SUM([Qtd PNP]) AS QtdPNP
		                        ,SUM([Qtd_PNP_Real]) as [QtdPNPReal] 
		                        ,SUM([Qtd_PNP_DeltaKM]) as [QtdPNPDeltaKM]
		                        ,SUM([Repouso Realizado]) AS [RepousoRealizado]
		                        ,SUM([Descanso Realizado]) AS [DescansoRealizado]
		                        ,SUM([Refeição Realizada]) AS [RefeicaoRealizada]
		                        ,SUM([Espera Para Atend.]) AS [EsperaParaAtendimento]
		                        ,SUM([Abastecimento Realizado]) AS [AbastecimentoRealizado]
		                        ,SUM([Tempo de Atendimento]) AS [TempoAtendimento]
		                        ,SUM([Abastecimento não apontado]) AS [AbastecimentoNaoApontado]
		                        ,SUM([PNP Tolerada (ClienteNaRota = Não)]) AS [PNPToleradaClienteNaRotaIsNao]
		                        ,SUM([Erro de Apontamento (ClienteNaRota = Não)]) AS [ErroApontamentoClienteNaRotaIsNao]
		                        ,SUM([PNP Comportamental (ClienteNaRota = Não)]) AS [PNPComportamentalClienteNaRotaNao]
		                        ,SUM([Ocorrências de PNP (ClienteNaRota = Não)]) AS [OcorrenciasPNPClienteNaRotaIsNao]
		                        ,SUM([PNP Tolerada (ClienteNaRota = Sim)]) AS [PNPToleradaClienteNaRotaIsSim]
		                        ,SUM([Erro de Apontamento (ClienteNaRota = Sim)]) AS [ErroApontamentoClienteNaRotaIsSim]
		                        ,SUM([PNP Comportamental (ClienteNaRota = Sim)]) AS [PNPComportamentalClienteNaRotaIsSim]
		                        ,SUM([Pernoite não apontada]) AS [PernoiteNaoApontada]
		                        ,SUM([Refeição Não Apontada]) AS [RefeicaoNaoApontada]
		                        ,SUM([Aguardando descarga (ClienteNaRota = Não)]) AS [AguardandoDescargaClienteNaRotaIsNao]
		                        ,SUM([Aguardando descarga (ClienteNaRota = Sim)]) AS [AguardandoDescargaClienteNaRotaIsSim] 
	                        FROM tmpParadasTratadasAnalitico
	                        GROUP BY
		                        [Data da Rota]
		                        ,[Transporte]
		                        ,[Regional]
		                        ,[Cd Un. Negócio]
		                        ,[Un. Negócio]
		                        ,[Veículo]
		                        ,[Cd Transportadora]
		                        ,[Transportadora]
		                        ,[KM Previsto]
		                        ,[Partida realizada]
		                        ,[Fim realizado]
		                        ,[Distância Realizada]
		                        ,[Início no Raio]
		                        ,[Fim no Raio]
		                        ,[Usuário Fechamento]
		                        ,[Quantidade Entrega Prevista]
		                        ,[Quantidade Entrega Realizada]
		                        ,[Quantidade Entrega Devolvida]
                        END", new { CdRota = cdRota }, commandType: CommandType.Text).FirstOrDefault();
                #endregion

                return paradaTratada;
            }
        }
    }
}
