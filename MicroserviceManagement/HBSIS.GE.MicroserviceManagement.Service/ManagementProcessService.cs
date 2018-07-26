using HBSIS.GE.MicroserviceManagement.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace HBSIS.GE.MicroserviceManagement.Service
{
    public class ManagementProcessService
    {
        private Dictionary<int, RunningCustomerMicroserviceProcess> _runningProcesses;

        public void InitProcesses()
        {
            _runningProcesses = new Dictionary<int, RunningCustomerMicroserviceProcess>();

            StopProcesses();

            var avaiblesMicroservices = LoadEnabledMicroservicesFromCustomers();

            foreach(var microservice in avaiblesMicroservices)
            {
                AddRunningProcess(microservice);
            }
        }

        public List<CustomerMicroservice> LoadEnabledMicroservicesFromCustomers()
        {
            CustomerMicroserviceService customerMicroserviceRepository = new CustomerMicroserviceService();
            List<CustomerMicroservice> avaiblesMicroservices = customerMicroserviceRepository.GetAvaibles();

            return avaiblesMicroservices;
        }

        public ProcessStartInfo ConfigureProcess(CustomerMicroservice customerMicroservice)
        {
            ProcessStartInfo processConfiguration = new ProcessStartInfo(customerMicroservice.FullPath);

            processConfiguration.WorkingDirectory = customerMicroservice.Directory;
            processConfiguration.Arguments = customerMicroservice.ProgramArguments;
            processConfiguration.WindowStyle = customerMicroservice.HasVisibleWindow ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;

            return processConfiguration;
        }
        
        public Process CreateProcess(ProcessStartInfo process)
        {
            Process createdProcess = Process.Start(process);
            createdProcess.EnableRaisingEvents = true;
            createdProcess.Exited += KillProcess;

            return createdProcess;
        }

        public void AddRunningProcess(CustomerMicroservice customerMicroservice)
        {
            ProcessStartInfo configuredProcess = ConfigureProcess(customerMicroservice);
            Process createdProcess = CreateProcess(configuredProcess);

            var runningCustomerMicroserviceProcess = new RunningCustomerMicroserviceProcess(customerMicroservice, createdProcess);
            _runningProcesses.Add(createdProcess.Id, runningCustomerMicroserviceProcess);
        }
        
        public void StopProcesses()
        {
            MicroserviceService microserviceService = new MicroserviceService();
            List<Microservice> microservices = microserviceService.GetAll();

            foreach(var currentMicroservice in microservices)
            {
                Process[] processes = Process.GetProcessesByName(currentMicroservice.FileName);
                
                foreach(var actualProcess in processes)
                {
                    actualProcess.Kill();
                }
            }

            _runningProcesses.Clear();
        }

        public void StopProcess(int pid)
        {
            if (_runningProcesses.ContainsKey(pid))
            {
                RunningCustomerMicroserviceProcess selectedRunningProcess = _runningProcesses[pid];
                Process runningProcess = selectedRunningProcess.Process;

                if (!runningProcess.HasExited)
                {
                    runningProcess.Kill();
                }

                _runningProcesses.Remove(pid);
            }
        }

        public void CheckLifeCycleProcesses()
        {
            CustomerMicroserviceService customerMicroserviceService = new CustomerMicroserviceService();
            List<CustomerMicroservice> lstCustomerMicroservice = customerMicroserviceService.GetAll();

            foreach(var actualCustomerMicroservice in lstCustomerMicroservice)
            {
                var runningProcess = _runningProcesses.Values
                                        .Where(rp => rp.CustomerMicroservice.Id == actualCustomerMicroservice.Id)
                                        .FirstOrDefault();
                
                if (runningProcess != null && !actualCustomerMicroservice.Active)
                {
                    StopProcess(runningProcess.Process.Id);
                }

                if(runningProcess == null && actualCustomerMicroservice.Active)
                {
                    AddRunningProcess(actualCustomerMicroservice);
                }
            }
        }

        private void KillProcess(object sender, EventArgs e)
        {
            Process killedProcess = (Process)sender;
            StopProcess(killedProcess.Id);
        }
    }
}
