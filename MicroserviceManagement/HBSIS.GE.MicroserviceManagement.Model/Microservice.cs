using System;
using System.Collections.Generic;

namespace HBSIS.GE.MicroserviceManagement
{
    public partial class Microservice: BaseModel
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Directory { get; set; }
        public string Description { get; set; }
    }
}
