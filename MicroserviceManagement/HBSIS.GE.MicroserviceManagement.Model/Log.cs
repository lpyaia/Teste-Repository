using System;
using System.Collections.Generic;

namespace HBSIS.GE.MicroserviceManagement
{
    public partial class Log: BaseModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
    }
}
