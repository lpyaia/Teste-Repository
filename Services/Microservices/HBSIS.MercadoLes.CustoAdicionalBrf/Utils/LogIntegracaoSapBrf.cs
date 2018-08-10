using HBSIS.MercadoLes.Commons.Integration;
using HBSIS.MercadoLes.Commons.Integration.Log;

namespace HBSIS.MercadoLes.CustoAdicionalBrf
{
    public class LogIntegracaoSapBrf : LogIntegrationSender<LogIntegracaoSapBrf>
    {
        public string NumeroRotaNegocio { get; set; }
    }
}