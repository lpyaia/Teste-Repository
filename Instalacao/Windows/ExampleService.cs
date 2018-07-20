using HBSIS.MercadoLes.Integracao.SapBrf.Service;
using Microsoft.Extensions.PlatformAbstractions;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Windows
{
    public class ExampleService : IMicroService
    {
        private IMicroServiceController controller;

        public ExampleService()
        {
            controller = null;
        }

        public ExampleService(IMicroServiceController controller)
        {
            this.controller = controller;
        }
        
        public void Start()
        {
            new IntegracaoSapBrfStartup().Start();

            if (controller != null)
            {
                controller.Stop();
            }
        }

        public void Stop()
        {
            Console.WriteLine("I stopped");
        }
    }
}
