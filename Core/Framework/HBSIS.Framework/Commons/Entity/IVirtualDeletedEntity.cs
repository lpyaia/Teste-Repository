namespace HBSIS.Framework.Commons.Entity
{
    public interface IVirtualDeletedEntity
    {
        bool IsDeleted { get; set; }
        string UserName { get; set; }
        string TrackingData { get; set; }
    }
}