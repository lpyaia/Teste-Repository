using System;
using System.Collections.Generic;
using System.IO;
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
            DateTime dtNow = DateTime.Now;
            string file = string.Format("log/log-{0}-{1}{2}{3}.txt", _appName, dtNow.Day, dtNow.Month, dtNow.Year);
            
            if(!Directory.Exists("log"))
            {
                Directory.CreateDirectory("log");
            }

            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }

            TextWriter textWriter = new StreamWriter(file, true);
            textWriter.WriteLine(string.Format("{0} - {1}", dtNow.ToString(), message));
            textWriter.Close();
        }
    }
}
