using HBSIS.Framework.Bus;
using HBSIS.Framework.Commons.Config;
using System;
using System.Collections.Generic;
using System.Text;

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
