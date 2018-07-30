using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.MicroserviceManagement.Service
{
    public static class LogService
    {
        private static string _appName;

        public static void SetAppName(string appName)
        {
            _appName = appName;
        }

        public static void WriteLog(string message)
        {
            string file = "log-" + _appName + ".txt";
            DateTime dtNow = DateTime.Now;

        }

        private static void CreateFileLog()
        {

        }
    }
}
