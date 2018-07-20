using HBSIS.MercadoLes.Services.Commons.Integration.Config;

namespace HBSIS.MercadoLes.Integracao.SapBrf
{
    public class IntegracaoSapBrfConfigurator : IntegrationConfigBuilder, IIntegracaoSapBrfConfigurator
    {
        public IntegracaoSapBrfConfigurator()
            : base("IntegracaoSapBrf")
        {
        }

        public static IIntegracaoSapBrfConfigurator GetInstance()
        {
            return new IntegracaoSapBrfConfigurator();
        }
    }
}