using System;
using System.Collections.Generic;

namespace HBSIS.GE.MicroserviceManagement.Model
{
    public partial class Customer: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BaseDirectory { get; set; }
    }
}
