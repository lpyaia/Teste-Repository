using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Services.Commons.Helpers
{
    public static class ServiceHelper
    {
        private class Args
        {
            [Option('j', "job")]
            public string InstanceName { get; set; }
        }

        public static string GetInstanceOrDefault(this string[] args, string defaultValue)
        {
            return GetInstance(args) ?? defaultValue;
        }

        public static string GetInstance(this string[] args)
        {
            Args argument = new Args();

            var parseResult = Parser.Default.ParseArguments<Args>(args)
                .WithParsed(options => argument = options);

            return argument.InstanceName;
        }
    }
}
