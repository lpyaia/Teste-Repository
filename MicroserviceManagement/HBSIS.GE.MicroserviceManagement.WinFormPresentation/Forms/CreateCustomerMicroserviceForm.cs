using HBSIS.GE.MicroserviceManagement.Model;
using HBSIS.GE.MicroserviceManagement.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    public partial class CreateCustomerMicroserviceForm : Form
    {
        public CreateCustomerMicroserviceForm()
        {
            InitializeComponent();

            CustomerService customerService = new CustomerService();
            List<Customer> lstCustomer = customerService.GetAll();

            ddlCustomer.DataSource = lstCustomer;
            ddlCustomer.DisplayMember = "Name";
            ddlCustomer.ValueMember = "Id";

            MicroserviceService microserviceService = new MicroserviceService();
            List<Microservice> lstMicroservice = microserviceService.GetAll();

            ddlMicrosservice.DataSource = lstMicroservice;
            ddlMicrosservice.DisplayMember = "DisplayName";
            ddlMicrosservice.ValueMember = "Id";
        }

        private void btnCreateCustomerMicrosservice_Click(object sender, EventArgs e)
        {
            CustomerMicroserviceService customerMicroserviceService = new CustomerMicroserviceService();
            CustomerService customerService = new CustomerService();
            MicroserviceService microserviceService = new MicroserviceService();
            
            int selectedCustomerId = (int)ddlCustomer.SelectedValue;
            int selectedMicroserviceId = (int)ddlMicrosservice.SelectedValue;

            Customer customer = customerService.GetById(selectedCustomerId);
            Microservice microservice = microserviceService.GetById(selectedMicroserviceId);

            CustomerMicroservice customerMicroservice = new CustomerMicroservice();
            customerMicroservice.Active = false;
            customerMicroservice.HasVisibleWindow = chkVisibleWindow.Checked;
            customerMicroservice.ProgramArguments = txtArguments.Text;
            customerMicroservice.CustomerId = selectedCustomerId;
            customerMicroservice.MicroserviceId = selectedMicroserviceId;

            customerMicroserviceService.Insert(customerMicroservice);

            string sourceFolder = microservice.Directory.TrimEnd('\\');
            string destinationFolder = customer.BaseDirectory + microservice.DisplayName + " - " + customerMicroservice.Id;

            customerMicroservice.Directory = destinationFolder;
            customerMicroservice.FullPath = destinationFolder + "\\" + microservice.FileName + microservice.FileExtension;

            customerMicroserviceService.Update(customerMicroservice);

            CopyMicroserviceFolderToCustomerFolder(sourceFolder, destinationFolder);

            DialogResult dialog = MessageBox.Show("Microsserviço adicionado com sucesso.");

            if (dialog == DialogResult.OK)
            {
                Sair();
            }
        }

        private void Sair()
        {
            Close();
            Dispose();

            Program.MainForm.UpdateMicroservicesStatus();
        }

        private void CopyMicroserviceFolderToCustomerFolder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            foreach (var fileName in Directory.GetFiles(sourceFolder))
            {
                FileInfo file = new FileInfo(fileName);
                File.Copy(fileName, destinationFolder + "\\" + file.Name);
            }

            var directories = Directory.GetDirectories(sourceFolder);

            foreach(var directoryName in directories)
            {
                DirectoryInfo directory = new DirectoryInfo(directoryName);
                CopyMicroserviceFolderToCustomerFolder(directoryName, destinationFolder + "\\" + directory.Name);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
