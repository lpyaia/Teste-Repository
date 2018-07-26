using HBSIS.GE.MicroserviceManagement.Model;
using HBSIS.GE.MicroserviceManagement.Repository;
using System.Collections.Generic;

namespace HBSIS.GE.MicroserviceManagement.Service
{
    public class CustomerMicroserviceService
    {
        private CustomerMicroserviceRepository _customerMicroserviceRepository;

        public CustomerMicroserviceService()
        {
            _customerMicroserviceRepository = new CustomerMicroserviceRepository();
        }

        public void Insert(CustomerMicroservice customerMicroservice)
        {
            _customerMicroserviceRepository.Insert(customerMicroservice);
        }

        public void Delete(CustomerMicroservice customerMicroservice)
        {
            _customerMicroserviceRepository.Delete(customerMicroservice);
        }

        public void Update(CustomerMicroservice customerMicroservice)
        {
            _customerMicroserviceRepository.Update(customerMicroservice);
        }

        public List<CustomerMicroservice> GetAll()
        {
            return _customerMicroserviceRepository.GetAll();
        }

        public List<CustomerMicroservice> GetAvaibles()
        {
            return _customerMicroserviceRepository.GetAvaibles();
        }

        public CustomerMicroservice GetById(int id)
        {
            return _customerMicroserviceRepository.GetById(id);
        }

    }
}
