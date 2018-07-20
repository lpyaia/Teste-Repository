using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Commons.Integration.Config
{
    public interface IIntegrationConfigBuilder<T>
       where T : class, IIntegrationConfig
    {
        IEnumerable<T> GetAll();

        T Get();

        IntegrationConfigCollection GetParent();
    }
}