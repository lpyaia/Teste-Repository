using System;
using System.Collections.Generic;

namespace HBSIS.GE.MicroserviceManagement.Model
{
    public partial class CustomerMicroservice: BaseModel
    {
        public int Id { get; set; }
        public string Directory { get; set; }
        public string FullPath { get; set; }
        public string ProgramArguments { get; set; }
        public bool Active { get; set; }
        public bool HasVisibleWindow { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int MicroserviceId { get; set; }
        public Microservice Microservice { get; set; }

    }
}
