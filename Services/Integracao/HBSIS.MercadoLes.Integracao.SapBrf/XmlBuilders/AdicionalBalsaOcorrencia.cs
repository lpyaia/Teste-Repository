using HBSIS.MercadoLes.Infra.Entities;
using HBSIS.MercadoLes.Integracao.SapBrf.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Integracao.SapBrf.XmlBuilders
{
    public static class AdicionalBalsaOcorrencia
    {
        private static AdicionalBalsa _adicionalBalsa;

        public static AdicionalBalsa Processar(IEnumerable<Infra.Entities.Ocorrencia> ocorrencias)
        {
            _adicionalBalsa = new AdicionalBalsa();

            foreach (var ocorrencia in ocorrencias)
            {
                var categoriaPontoInteresse = ocorrencia.Parada?.PontoInteresse?.CategoriaPontoInteresse;

                if (categoriaPontoInteresse != null && 
                    categoriaPontoInteresse.DsCategoriaPontoInteresse.ToLower().Contains("balsa"))
                {
                    _adicionalBalsa.AdicionarItem(ocorrencia.Parada.PontoInteresse.NmPonto);
                }
            }
            
            return _adicionalBalsa.Quantidade > 0 ? _adicionalBalsa : null;
        }
    }
}
