using HBSIS.GE.MicroserviceManagement.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HBSIS.GE.MicroserviceManagement.Repository
{
    public class CustomerMicroserviceRepository: BaseRepository<CustomerMicroservice>
    {
        public List<CustomerMicroservice> GetAvaibles()
        {
            return _dbSet.Where(cm => cm.Active).ToList();
        }

        public List<CustomerMicroservice> GetAllWithRelationships()
        {
            return _dbSet.Include(cm => cm.Customer).Include(cm => cm.Microservice).ToList();
        }
    }
}
