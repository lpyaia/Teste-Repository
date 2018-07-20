namespace HBSIS.Framework.Commons.Entity
{
    public interface IActiveEntity
    {
        void Insert();

        void Update();

        void Delete();

        void InsertOrUpdate();
    }
}