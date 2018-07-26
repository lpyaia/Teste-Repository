using HBSIS.GE.MicroserviceManagement.Repository;
using System.Collections.Generic;

namespace HBSIS.GE.MicroserviceManagement.Service
{
    public class MicroserviceService
    {
        private MicroserviceRepository _microserviceRepository;

        public MicroserviceService()
        {
            _microserviceRepository = new MicroserviceRepository();
        }

        public void Insert(Microservice microservice)
        {
            _microserviceRepository.Insert(microservice);
        }

        public void Delete(Microservice microservice)
        {
            _microserviceRepository.Delete(microservice);
        }

        public void Update(Microservice microservice)
        {
            _microserviceRepository.Update(microservice);
        }

        public List<Microservice> GetAll()
        {
            return _microserviceRepository.GetAll();
        }

        public Microservice GetById(int id)
        {
            return _microserviceRepository.GetById(id);
        }
    }
}
