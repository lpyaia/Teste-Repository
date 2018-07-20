using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Context;
using HBSIS.MercadoLes.Services.Commons.Enums;
using System.IO;

namespace HBSIS.MercadoLes.Services.Commons.Config
{
    public static class GlobalSettings
    {
        private static readonly object _lock = new object();

        public static string GetPathSolution()
        {
            return AppSettingConfigurator.GetValueOrDefault("mov3r:Path", @"C:\Mov3rPrimaria");
        }

        public static string GetPathContent(string loggerName = null)
        {
            var basePath = GetPathSolution();
            loggerName = loggerName ?? Configuration.Actual.GetAppName();

            var path = Path.Combine(basePath, "contents", loggerName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        #region UserName

        public static string AppUserName
        {
            get { return "SISTEMA"; }
        }

        public static string CurrentUserName
        {
            get { return ApplicationContext.Current["APP_USERNAME"]?.ToString(); }
            set { ApplicationContext.Current["APP_USERNAME"] = value; }
        }

        public static string GetAppUserName(OrigemDados origem)
        {
            var alias = string.Empty;

            switch (origem)
            {
                case OrigemDados.Integracao:
                    alias = "I";
                    break;

                default:
                    break;
            }

            return $"{AppUserName} ({alias})";
        }

        #endregion UserName

        #region Jobs

        public static int GetJobTimer(string name)
        {
            var ret = 0;

            var value = AppSettingConfigurator.GetValueOrDefault($"mov3r:Job{name}Time");

            int.TryParse(value, out ret);

            return ret == 0 ? 5 : ret;
        }

        #endregion Jobs
    }
}