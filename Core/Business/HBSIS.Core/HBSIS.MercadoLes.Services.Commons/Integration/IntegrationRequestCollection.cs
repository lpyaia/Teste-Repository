using HBSIS.MercadoLes.Services.Commons.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.MercadoLes.Services.Commons.Integration
{
    public class IntegrationRequestCollection : IEnumerable<IntegrationRequest>, IIntegrationStatus
    {
        public static IntegrationRequestCollection Empty => new IntegrationRequestCollection();

        public StatusLog Status
        {
            get
            {
                var status = List.OrderByDescending(x => x.Status).FirstOrDefault()?.Status;
                return status.GetValueOrDefault();
            }
        }

        protected List<IntegrationRequest> List { get; } = new List<IntegrationRequest>();

        public IntegrationRequest this[int index]
        {
            get
            {
                return List.ElementAt(index);
            }
        }

        public IEnumerator<IntegrationRequest> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(IntegrationRequest item)
        {
            if (item == null) return;

            List.Add(item);
        }
    }
}