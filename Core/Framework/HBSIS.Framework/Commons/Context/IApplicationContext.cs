namespace HBSIS.Framework.Commons.Context
{
    public interface IApplicationContext
    {
        object this[string key] { get; set; }
    }
}