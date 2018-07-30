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
    public partial class MainForm : Form
    {
        private CustomerMicroserviceService customerMicroserviceService;
        private int selectedId = 0;

        public MainForm()
        {
            InitializeComponent();
            customerMicroserviceService = new CustomerMicroserviceService();
        }

        private void microsserviçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListMicroserviceForm listMicroserviceForm = new ListMicroserviceForm();
            listMicroserviceForm.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListCustomerForm listCustomerForm = new ListCustomerForm();
            listCustomerForm.Show();
        }

        private void btnAddCustomerMicroservice_Click(object sender, EventArgs e)
        {
            CreateCustomerMicroserviceForm createCustomerMicroservice = new CreateCustomerMicroserviceForm();
            createCustomerMicroservice.Show();
        }

        private void btnEditSelectedMicroservice_Click(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                EditCustomerMicroserviceForm editCustomerMicroserviceForm = new EditCustomerMicroserviceForm(selectedId);
                editCustomerMicroserviceForm.Show();
            }
        }

        private void btnDeleteSelectedMicroservice_Click(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                DialogResult result = MessageBox.Show("Deseja excluir o microsserviço para este cliente?", "Atenção", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        customerMicroserviceService.Delete(selectedId);
                        UpdateMicroservicesStatus();

                        if (customerMicroservicesGrid.Rows.Count == 0)
                            selectedId = 0;
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro");
                    }
                }
            }
        }

        private void btnStartMicroservice_Click(object sender, EventArgs e)
        {
            if(selectedId > 0)
            {
                try
                {
                    CustomerMicroservice customerMicroservice = customerMicroserviceService.GetById(selectedId);

                    customerMicroservice.Active = true;

                    customerMicroserviceService.Update(customerMicroservice);

                    UpdateMicroservicesStatus();
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro");
                }
            }
        }

        private void btnStopMicroservice_Click(object sender, EventArgs e)
        {
            if(selectedId > 0)
            {
                try
                {
                    CustomerMicroservice customerMicroservice = customerMicroserviceService.GetById(selectedId);

                    customerMicroservice.Active = false;

                    customerMicroserviceService.Update(customerMicroservice);

                    UpdateMicroservicesStatus();
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro");
                }
            }
        }

        private void customerMicroservicesGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (customerMicroservicesGrid.SelectedRows.Count > 0)
            {
                selectedId = Convert.ToInt32(customerMicroservicesGrid.SelectedRows[0].Cells["Id"].Value.ToString());
                EnableSelectedMicroserviceButtons(true);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateMicroservicesStatus();
        }

        public void UpdateMicroservicesStatus()
        {
            List<CustomerMicroservice> lstCustomerMicroservice = customerMicroserviceService.GetAllWithRelationships();

            customerMicroservicesGrid.Rows.Clear();

            foreach (var customerMicroservice in lstCustomerMicroservice)
            {
                string status = customerMicroservice.Active ? "Em execução" : "Parado";

                customerMicroservicesGrid.Rows.Add(
                    customerMicroservice.Id,
                    customerMicroservice.Microservice.DisplayName,
                    customerMicroservice.Customer.Name,
                    customerMicroservice.Microservice.Description,
                    status);
            }

            if (selectedId == 0)
                EnableSelectedMicroserviceButtons(false);
        }

        private void EnableSelectedMicroserviceButtons(bool enable)
        {
            btnStartMicroservice.Enabled = enable;
            btnStopMicroservice.Enabled = enable;
            btnDeleteSelectedMicroservice.Enabled = enable;
            btnEditSelectedMicroservice.Enabled = enable;
        }

        private void btnUpdateMicroservices_Click(object sender, EventArgs e)
        {
            UpdateMicroservicesStatus();
        }
    }
}
