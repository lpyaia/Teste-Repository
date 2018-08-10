using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;

namespace HBSIS.Framework.Commons.Config
{
    public abstract class Configurator
    {
        private const string VarConfigKey = "MOV3R_CONFIG_PATH";

        public static IConfigurationRoot AppSettingsConfiguration { get; set; }

        public static string GetPathName()
        {
            AppSettingsConfiguration = LoadAppSettings();

            var fileName = AppSettingsConfiguration["appSettings:Config"];

            if (string.IsNullOrWhiteSpace(fileName))
                fileName = Environment.GetEnvironmentVariable(VarConfigKey, EnvironmentVariableTarget.Machine);

            return fileName;
        }

        public static string GetLogPathName(string targetLog)
        {
            AppSettingsConfiguration = LoadAppSettings();

            var fileName = AppSettingsConfiguration["appSettings:" + targetLog];

            if (string.IsNullOrWhiteSpace(fileName))
                fileName = Environment.GetEnvironmentVariable(VarConfigKey, EnvironmentVariableTarget.Machine);

            return fileName;
        }

        private static IConfigurationRoot LoadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        public static string GetName()
        {
            var path = GetPathName();

            if (path != null)
            {
                var index = path.IndexOf("general-");

                if (index > 0)
                {
                    path = path.Substring(index);
                    path = path.Replace("general-", string.Empty);
                }

                path = path.Replace(".config", string.Empty);
            }

            return path;
        }

        public static int GetJobInterval(string jobName)
        {
            int interval;

            AppSettingsConfiguration = LoadAppSettings();
            var strInterval = AppSettingsConfiguration["appSettings:fluxoLES.IntervaloJob." + jobName];
            
            return int.TryParse(strInterval, out interval) ? interval : 1;
        }
    }
}