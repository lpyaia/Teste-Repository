using HBSIS.MercadoLes.Services.Commons.Integration;
using HBSIS.MercadoLes.Services.Commons.Integration.Log;

namespace HBSIS.MercadoLes.Integracao.SapBrf
{
    public class LogIntegracaoSapBrf : LogIntegrationSender<LogIntegracaoSapBrf>
    {
        public string NumeroRotaNegocio { get; set; }
    }
}