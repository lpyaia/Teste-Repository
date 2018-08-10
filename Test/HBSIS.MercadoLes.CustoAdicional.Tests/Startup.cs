using HBSIS.Framework.Commons.Config;
using HBSIS.MercadoLes.CustoAdicionalBrf;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Tests
{
    public static class Startup
    {
        public static void Configure()
        {
            var config = IntegracaoSapBrfConfigurator.GetInstance().Get();

            Configuration
                 .UseStaticDictionary()
                 .UseThreadContextPersister()
                 .UseAppName("IntegracaoSapBrf")
                 .UseBusEasyNetQFactory()
                 .UseSqlConnectionString("hbsis.les-sql")
                 .UseDataDapperFactory()
                 .Configure();
        }
    }
}
