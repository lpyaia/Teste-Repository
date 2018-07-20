namespace HBSIS.Framework.Commons.Data
{
    public interface IFactory
    {
        IDataContext CurrentDataContext { get; }

        IDataContext GetDataContext(string connectionStringName = null);
    }
}