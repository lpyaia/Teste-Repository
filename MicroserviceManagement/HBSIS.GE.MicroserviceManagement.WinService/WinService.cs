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
            LogService.SetAppName("WinService");
            LogService.WriteLog("TESTINHO");
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
                LogService.WriteLog(ex.Message);
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
                LogService.WriteLog(ex.Message);
            }
        }

        protected override void OnStop()
        {
            processWatcher.Dispose();
            managementProcessService.StopProcesses();
        }
    }
}
