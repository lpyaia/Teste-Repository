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
            try
            {
                CustomerMicroserviceService customerMicroserviceRepository = new CustomerMicroserviceService();
                List<CustomerMicroservice> avaiblesMicroservices = customerMicroserviceRepository.GetAvaibles();

                return avaiblesMicroservices;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ProcessStartInfo ConfigureProcess(CustomerMicroservice customerMicroservice)
        {
            try
            {
                ProcessStartInfo processConfiguration = new ProcessStartInfo(customerMicroservice.FullPath);

                processConfiguration.WorkingDirectory = customerMicroservice.Directory;
                processConfiguration.Arguments = customerMicroservice.ProgramArguments;
                processConfiguration.WindowStyle = customerMicroservice.HasVisibleWindow ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;

                return processConfiguration;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public Process CreateProcess(ProcessStartInfo process)
        {
            try
            {
                Process createdProcess = Process.Start(process);
                createdProcess.EnableRaisingEvents = true;
                createdProcess.Exited += KillProcess;

                return createdProcess;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void AddRunningProcess(CustomerMicroservice customerMicroservice)
        {
            try
            {
                ProcessStartInfo configuredProcess = ConfigureProcess(customerMicroservice);
                Process createdProcess = CreateProcess(configuredProcess);

                var runningCustomerMicroserviceProcess = new RunningCustomerMicroserviceProcess(customerMicroservice, createdProcess);
                _runningProcesses.Add(createdProcess.Id, runningCustomerMicroserviceProcess);
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public void StopProcesses()
        {
            try
            {
                MicroserviceService microserviceService = new MicroserviceService();
                List<Microservice> microservices = microserviceService.GetAll();

                foreach (var currentMicroservice in microservices)
                {
                    Process[] processes = Process.GetProcessesByName(currentMicroservice.FileName);

                    foreach (var actualProcess in processes)
                    {
                        actualProcess.Kill();
                    }
                }

                _runningProcesses.Clear();
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void StopProcess(int pid)
        {
            try
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

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CheckLifeCycleProcesses()
        {
            try
            {
                CustomerMicroserviceService customerMicroserviceService = new CustomerMicroserviceService();
                List<CustomerMicroservice> lstCustomerMicroservice = customerMicroserviceService.GetAll();

                foreach (var actualCustomerMicroservice in lstCustomerMicroservice)
                {
                    var runningProcess = _runningProcesses.Values
                                            .Where(rp => rp.CustomerMicroservice.Id == actualCustomerMicroservice.Id)
                                            .FirstOrDefault();

                    if (runningProcess != null && !actualCustomerMicroservice.Active)
                    {
                        StopProcess(runningProcess.Process.Id);
                    }

                    if (runningProcess == null && actualCustomerMicroservice.Active)
                    {
                        AddRunningProcess(actualCustomerMicroservice);
                    }
                }
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void KillProcess(object sender, EventArgs e)
        {
            try
            {
                Process killedProcess = (Process)sender;
                StopProcess(killedProcess.Id);
            }
            
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
