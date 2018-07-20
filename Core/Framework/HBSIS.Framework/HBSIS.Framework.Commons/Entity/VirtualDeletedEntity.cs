using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBSIS.Framework.Commons.Entity
{
    public class VirtualDeletedEntity<TEntity, TId> : BaseEntity<TEntity, TId>, IVirtualDeletedEntity
        where TEntity : class, IEntity<TId>
        where TId : struct, IEquatable<TId>
    {
        [NotMapped]
        private TrackingInfo trackingInfo;

        public bool IsDeleted { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public TrackingInfo TrackingInfo
        {
            get
            {
                return trackingInfo ?? (trackingInfo = string.IsNullOrEmpty(TrackingData)
                           ? new TrackingInfo()
                           : JsonHelper.Deserialize<TrackingInfo>(TrackingData));
            }
        }

        [MaxLength(1000)]
        public string TrackingData { get; set; }

        public override void Delete()
        {
            Delete(false);
        }

        public void Delete(bool definitely)
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                base.Update();
            }

            if (definitely)
            {
                base.Delete();
            }
        }
    }
}