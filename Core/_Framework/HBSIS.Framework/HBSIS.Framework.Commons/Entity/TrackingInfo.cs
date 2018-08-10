namespace HBSIS.Framework.Commons.Entity
{
    public class TrackingInfo
    {
        public TrackingInfo()
        {
            InsertInfo = new OperationInfo();
            UpdateInfo = new OperationInfo();
            DeleteInfo = new OperationInfo();
        }

        public OperationInfo InsertInfo { get; set; }
        public OperationInfo UpdateInfo { get; set; }
        public OperationInfo DeleteInfo { get; set; }
    }
}