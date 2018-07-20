namespace HBSIS.Framework.Commons.Config
{
    public interface IConfiguration : IConfigure
    {
        void Put<T>(string key, T value);

        T Get<T>(string key);
    }
}