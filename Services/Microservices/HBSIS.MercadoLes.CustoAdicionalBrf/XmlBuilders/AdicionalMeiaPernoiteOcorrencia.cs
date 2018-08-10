using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using HBSIS.MercadoLes.CustoAdicionalBrf.Utils;
using HBSIS.MercadoLes.CustoAdicionalBrf.Enums;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class AdicionalMeiaPernoiteOcorrencia
    {
        private static AdicionalMeiaPernoite _adicionalMeiaPernoite;

        public static AdicionalMeiaPernoite Processar(IEnumerable<Infra.Ocorrencia> ocorrencias, 
            IEnumerable<Infra.Deposito> depositos,
            Rota rota)
        {
            _adicionalMeiaPernoite = new AdicionalMeiaPernoite();
            
            if (rota.UnidadeNegocio != null && 
                rota.UnidadeNegocio.DtHoraLimRestanteRetorno.HasValue && 
                rota.UnidadeNegocio.QtQuilometroRestante.HasValue)
            {
                bool validacaoDistanciaMotoristaDeposito = true;
                double qtMetrosRestante = Convert.ToDouble(rota.UnidadeNegocio.QtQuilometroRestante.Value * 1000);
                
                var ultimaOcorrenciaAntesDoHorarioLimite = ocorrencias
                    .Where(ocorrencia => ocorrencia.IdOcorrencia != (short)TipoOcorrencia.ChegadaRevenda && ocorrencia.DtInclusao.TimeOfDay <= rota.UnidadeNegocio.DtHoraLimRestanteRetorno.Value.TimeOfDay)
                    .OrderByDescending(ocorrencia => ocorrencia.DtInclusao)
                    .First();

                foreach (var deposito in depositos)
                {
                    (double distanciaMetros, int tempo) = Coordenada.Distancia(Convert.ToDouble(ultimaOcorrenciaAntesDoHorarioLimite.NrLatitude),
                        Convert.ToDouble(ultimaOcorrenciaAntesDoHorarioLimite.NrLongitude),
                        Convert.ToDouble(deposito.PontoInteresse.NrLatitude),
                        Convert.ToDouble(deposito.PontoInteresse.NrLongitude));

                    if (deposito.IdAtivo && distanciaMetros <= qtMetrosRestante)
                    {
                        validacaoDistanciaMotoristaDeposito = false;
                        break;
                    }
                }

                var divergenciaDiaria = DivergenciaDiariaOcorrencia.Processar(rota);

                _adicionalMeiaPernoite.HouveDivergencia = Convert.ToInt16(validacaoDistanciaMotoristaDeposito && divergenciaDiaria.QuantidadeDiariaRealizada > 0);
            }

            return _adicionalMeiaPernoite;
        }
    }
}
