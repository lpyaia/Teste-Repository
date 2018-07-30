using HBSIS.GE.MicroserviceManagement.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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

        public void DeleteMicroservicesByCustomerId(int customerId)
        {
            var microservicesByCustomer = _dbSet.Where(cm => cm.CustomerId == customerId).ToList();
            _dbSet.RemoveRange(microservicesByCustomer);
        }

        public void DeleteMicroservicesByMicroserviceId(int microserviceId)
        {
            var microservices = _dbSet.Where(cm => cm.MicroserviceId == microserviceId).ToList();
            _dbSet.RemoveRange(microservices);
        }
    }
}
