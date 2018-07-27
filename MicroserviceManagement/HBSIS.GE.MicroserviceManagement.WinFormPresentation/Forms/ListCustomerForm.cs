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
    public partial class ListCustomerForm : Form
    {
        public ListCustomerForm()
        {
            InitializeComponent();
            UpdateCustomerList();
        }

        private void UpdateCustomerList()
        {
            CustomerService customerService = new CustomerService();
            List<Customer> lstCustomer = customerService.GetAll();

            lstItemsCustomer.Items.Clear();
            lstItemsCustomer.DisplayMember = "Name";
            lstItemsCustomer.ValueMember = "Id";

            foreach (var customer in lstCustomer)
            {
                lstItemsCustomer.Items.Add(customer);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CreateCustomerForm createCustomerForm = new CreateCustomerForm();
            createCustomerForm.Show();

            Sair();
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            EditCustomerForm editCustomerForm = new EditCustomerForm();
            editCustomerForm.Show();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair();
        }

        private void Sair()
        {
            this.Close();
            this.Dispose();
        }
    }
}
