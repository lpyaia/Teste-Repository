namespace HBSIS.Framework.Commons.Config
{
    public abstract class Configurator<TModel> : Configurator, IConfigurator<TModel>
      where TModel : class
    {
        public abstract TModel GetCurrent();
    }
}