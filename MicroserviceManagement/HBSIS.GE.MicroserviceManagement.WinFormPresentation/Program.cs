using HBSIS.GE.MicroserviceManagement.Data.Entity;
using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using HBSIS.GE.MicroserviceManagement.Service;

namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    static class Program
    {
        public static MainForm MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MicroserviceManagerDbContext context = new MicroserviceManagerDbContext();
            context.Database.Migrate();

            LogService.SetAppName("WinForm");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new MainForm();
            Application.Run(MainForm);
        }
    }
}
