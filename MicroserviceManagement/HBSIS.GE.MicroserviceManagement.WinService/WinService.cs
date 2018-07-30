using HBSIS.GE.MicroserviceManagement.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBSIS.GE.MicroserviceManagement.WinService
{
    public partial class WinService : ServiceBase
    {
        private ManagementProcessService managementProcessService;
        private Timer processWatcher;

        public WinService()
        {
            InitializeComponent();

#if DEBUG
            managementProcessService = new ManagementProcessService();
            managementProcessService.InitProcesses();

            while (true)
            {
                managementProcessService.CheckLifeCycleProcesses();
                Thread.Sleep(10000);
            }
#endif
        }

        private void WatchProcesses(object state)
        {
            try
            {
                managementProcessService.CheckLifeCycleProcesses();
            }

            catch(Exception ex)
            {

            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                managementProcessService = new ManagementProcessService();
                managementProcessService.InitProcesses();

                processWatcher = new Timer(WatchProcesses, null, 0, 1000 * 10);
            }

            catch(Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            processWatcher.Dispose();
            managementProcessService.StopProcesses();
        }
    }
}
