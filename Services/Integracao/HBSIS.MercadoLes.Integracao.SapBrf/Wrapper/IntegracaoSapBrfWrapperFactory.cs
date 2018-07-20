using HBSIS.MercadoLes.Services.Commons.Integration;
using HBSIS.MercadoLes.Services.Commons.Integration.Config;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Wrapper
{
    public class IntegracaoSapBrfWrapperFactory
    {
        public const string BRF = "brf";

        public static IIntegracaoSapBrfWrapper Get(IIntegrationConfig config)
        {
            IIntegracaoSapBrfWrapper ret = null;

            if (config == null || string.IsNullOrEmpty(config.Name))
                throw new HBIntegrationException("Config not defined.");

            switch (config.Name.ToLower())
            {
                case BRF:
                    ret = new IntegracaoSapBrfWrapper(config);
                    break;
            }

            return ret;
        }
    }
}