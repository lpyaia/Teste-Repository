using HBSIS.GE.MicroserviceManagement.Model;
using HBSIS.GE.MicroserviceManagement.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    public partial class EditCustomerMicroserviceForm : Form
    {
        private int _selectedCustomerMicroserviceId;
        private CustomerMicroserviceService customerMicroserviceService;
        private MicroserviceService microserviceService;

        public EditCustomerMicroserviceForm(int selectedCustomerMicroserviceId)
        {
            InitializeComponent();
            _selectedCustomerMicroserviceId = selectedCustomerMicroserviceId;

            try
            {
                customerMicroserviceService = new CustomerMicroserviceService();
                microserviceService = new MicroserviceService();

                LoadCustomerList();
                LoadMicroserviceList();
                
                CustomerMicroservice customerMicroservice = customerMicroserviceService.GetById(selectedCustomerMicroserviceId);

                ddlMicrosservice.SelectedValue = customerMicroservice.MicroserviceId;
                ddlCustomer.SelectedValue = customerMicroservice.CustomerId;

                txtArguments.Text = customerMicroservice.ProgramArguments;
                chkVisibleWindow.Checked = customerMicroservice.HasVisibleWindow;
                lblId.Text = customerMicroservice.Id.ToString();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void LoadCustomerList()
        {
            CustomerService customerService = new CustomerService();
            List<Customer> lstCustomer = customerService.GetAll();

            ddlCustomer.DataSource = lstCustomer;
            ddlCustomer.DisplayMember = "Name";
            ddlCustomer.ValueMember = "Id";
        }

        private void LoadMicroserviceList()
        {
            List<Microservice> lstMicroservice = microserviceService.GetAll();

            ddlMicrosservice.DataSource = lstMicroservice;
            ddlMicrosservice.DisplayMember = "DisplayName";
            ddlMicrosservice.ValueMember = "Id";
        }

        private void Sair()
        {
            Close();
            Dispose();

            Program.MainForm.UpdateMicroservicesStatus();
        }

        private void btnEditCustomerMicrosservice_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerMicroservice customerMicroservice = customerMicroserviceService.GetById(_selectedCustomerMicroserviceId);

                customerMicroservice.ProgramArguments = txtArguments.Text;
                customerMicroservice.HasVisibleWindow = chkVisibleWindow.Checked;

                customerMicroserviceService.Update(customerMicroservice);

                Sair();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sair();
        }
    }
}
