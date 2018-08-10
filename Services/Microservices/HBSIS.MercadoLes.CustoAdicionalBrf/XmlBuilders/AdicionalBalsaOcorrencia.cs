using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders
{
    public static class AdicionalBalsaOcorrencia
    {
        private static AdicionalBalsa _adicionalBalsa;

        public static AdicionalBalsa Processar(IEnumerable<Infra.Ocorrencia> ocorrencias)
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
