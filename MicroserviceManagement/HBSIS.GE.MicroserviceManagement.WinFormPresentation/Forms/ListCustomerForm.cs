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
        CustomerService customerService;
        private int _selectedCustomerId;

        public ListCustomerForm()
        {
            InitializeComponent();

            customerService = new CustomerService();

            UpdateCustomerList();
        }

        private void UpdateCustomerList()
        {
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
            if(_selectedCustomerId == 0)
            {
                MessageBox.Show("Por favor, selecione um cliente para editar.");
                return;
            }

            EditCustomerForm editCustomerForm = new EditCustomerForm(_selectedCustomerId);
            editCustomerForm.Show();
            Sair();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Por favor, selecione um cliente para excluir.");
                return;
            }

            DialogResult result = MessageBox.Show("Deseja realmente excluir?", "Atenção", MessageBoxButtons.YesNo);

            if(result == DialogResult.Yes)
            {
                Excluir(_selectedCustomerId);
                UpdateCustomerList();
                _selectedCustomerId = 0;
            }
        }

        private void Excluir(int selectedCustomerId)
        {
            try
            {
                customerService.Delete(selectedCustomerId);
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair();
        }

        private void Sair()
        {
            Close();
            Dispose();
        }

        private void ListCustomerForm_Load(object sender, EventArgs e)
        {
            btnSair.Focus();
        }

        private void lstItemsCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Customer customer = (Customer)lstItemsCustomer.SelectedItem;
            _selectedCustomerId = customer != null ? customer.Id : 0;
        }
    }
}
