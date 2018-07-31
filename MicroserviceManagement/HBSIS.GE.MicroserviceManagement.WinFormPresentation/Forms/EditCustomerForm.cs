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
    public partial class EditCustomerForm : Form
    {
        private CustomerService customerService;
        private int _selectedCustomerId;

        public EditCustomerForm(int selectedCustomerId)
        {
            InitializeComponent();

            customerService = new CustomerService();
            _selectedCustomerId = selectedCustomerId;

            Customer customer = customerService.GetById(_selectedCustomerId);
            txtName.Text = customer.Name;
            txtCustomerFolder.Text = customer.BaseDirectory;
            lblId.Text = customer.Id.ToString();
        }

        private void txtCustomerFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtCustomerFolder.Text = folderDialog.SelectedPath;
                Environment.SpecialFolder root = folderDialog.RootFolder;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sair();
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCustomerFolder.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Preencha os campos nome e pasta do cliente.");
                return;
            }

            try
            {
                Customer customer = customerService.GetById(_selectedCustomerId);
                customer.Name = txtName.Text;
                customer.BaseDirectory = txtCustomerFolder.Text.TrimEnd('\\') + @"\"; ;
                customerService.Update(customer);

                MessageBox.Show("Cliente atualizado com sucesso.");
                Sair();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void Sair()
        {
            ListCustomerForm listCustomerForm = new ListCustomerForm();
            listCustomerForm.Show();

            Dispose();
            Close();
        }

        private void EditCustomerForm_Load(object sender, EventArgs e)
        {
            btnEditCustomer.Focus();
        }
    }
}
