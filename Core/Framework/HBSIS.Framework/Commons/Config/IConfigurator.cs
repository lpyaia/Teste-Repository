namespace HBSIS.Framework.Commons.Config
{
    public interface IConfigurator<TModel>
        where TModel : class
    {
        TModel GetCurrent();
    }
}