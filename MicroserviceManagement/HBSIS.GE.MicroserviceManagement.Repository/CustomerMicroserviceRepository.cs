using HBSIS.GE.MicroserviceManagement.Model;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.GE.MicroserviceManagement.Repository
{
    public class CustomerMicroserviceRepository: BaseRepository<CustomerMicroservice>
    {
        public List<CustomerMicroservice> GetAvaibles()
        {
            return _dbSet.Where(cm => cm.Active).ToList();
        }
    }
}
