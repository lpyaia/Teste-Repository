using HBSIS.GE.MicroserviceManagement.Model;
using HBSIS.GE.MicroserviceManagement.Repository;
using System.Collections.Generic;

namespace HBSIS.GE.MicroserviceManagement.Service
{
    public class CustomerService
    {
        private CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public void Insert(Customer customer)
        {
            _customerRepository.Insert(customer);
        }

        public void Update(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public void Delete(int customerId)
        {
            CustomerMicroserviceService customerMicroserviceService = new CustomerMicroserviceService();
            customerMicroserviceService.DeleteMicroservicesByCustomerId(customerId);

            Customer customer = GetById(customerId);
            _customerRepository.Delete(customer);
        }
    }
}
