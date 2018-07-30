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
    public partial class CreateCustomerForm : Form
    {
        public CreateCustomerForm()
        {
            InitializeComponent();
        }
        
        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            if(!IsValidForm())
            {
                MessageBox.Show("Preencha todos os campos do formulário.");
                return;
            }

            Customer customer = new Customer();
            customer.Name = txtName.Text;
            customer.BaseDirectory = txtCustomerFolder.Text.TrimEnd('\\') + @"\";

            CustomerService customerService = new CustomerService();
            customerService.Insert(customer);

            DialogResult dialog = MessageBox.Show("Cliente adicionado com sucesso.");

            if(dialog == DialogResult.OK)
            {
                Sair();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sair();
        }

        private void Sair()
        {
            ListCustomerForm listCustomerForm = new ListCustomerForm();
            listCustomerForm.Show();

            this.Dispose();
            this.Close();
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

        private void CreateCustomerForm_Load(object sender, EventArgs e)
        {
            Sair();
        }

        private void CreateCustomerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sair();
        }

        private bool IsValidForm()
        {
            if(string.IsNullOrEmpty(txtCustomerFolder.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                return false;
            }

            return true;
        }
    }
}
