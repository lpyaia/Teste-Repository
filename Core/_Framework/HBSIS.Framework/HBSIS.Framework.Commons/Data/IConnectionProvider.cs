namespace HBSIS.Framework.Commons.Data
{
    public interface IConnectionProvider
    {
        string GetConnectionString(string name);
    }
}