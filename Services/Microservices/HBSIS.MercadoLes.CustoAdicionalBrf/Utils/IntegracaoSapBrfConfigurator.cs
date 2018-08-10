using HBSIS.MercadoLes.Commons.Integration.Config;

namespace HBSIS.MercadoLes.CustoAdicionalBrf
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