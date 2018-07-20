using System;

namespace HBSIS.MercadoLes.Integracao.SapBrf
{
    public class IntegracaoSapBrfModel
    {
        public string NumeroFatura { get; set; }

        public string NumeroTransporte { get; set; }

        public DateTime DataEntrega { get; set; }
    }
}