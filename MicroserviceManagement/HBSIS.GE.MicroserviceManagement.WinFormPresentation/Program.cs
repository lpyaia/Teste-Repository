using HBSIS.GE.MicroserviceManagement.Data.Entity;
using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new MainForm();
            Application.Run(MainForm);
        }
    }
}
