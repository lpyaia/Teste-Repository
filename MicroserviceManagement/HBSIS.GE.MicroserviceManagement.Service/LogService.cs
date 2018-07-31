using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string logPath = assemblyPath + @"\log\";
            string file = string.Format(@"{0}log-{1}-{2}{3}{4}.txt", logPath, _appName, dtNow.Day, dtNow.Month, dtNow.Year);
            
            if(!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
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
