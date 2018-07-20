using System.Collections.Generic;
using System.Linq;

namespace HBSIS.MercadoLes.Services.Commons.Integration.Config
{
    public class IntegrationConfigBuilder : IIntegrationConfigBuilder<IntegrationConfig>
    {
        public IntegrationConfigBuilder(string name)
        {
            Name = name;
        }

        protected string Name { get; }

        public IntegrationConfig Get()
        {
            var configurator = new IntegrationConfigurator(Name);
            return configurator.GetCurrent().FirstOrDefault();
        }

        public IEnumerable<IntegrationConfig> GetAll()
        {
            var configurator = new IntegrationConfigurator(Name);
            return configurator.GetCurrent().ToList();
        }

        public IntegrationConfigCollection GetParent()
        {
            var configurator = new IntegrationConfigurator(Name);
            return configurator.GetCurrent();
        }
    }
}