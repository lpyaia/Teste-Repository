using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HBSIS.GE.MicroserviceManagement.Model
{
    public class RunningCustomerMicroserviceProcess
    {
        public CustomerMicroservice CustomerMicroservice { get; set; }
        public Process Process { get; set; }

        public RunningCustomerMicroserviceProcess(CustomerMicroservice customerMicroservice, Process process)
        {
            CustomerMicroservice = customerMicroservice;
            Process = process;
        }
    }
}
