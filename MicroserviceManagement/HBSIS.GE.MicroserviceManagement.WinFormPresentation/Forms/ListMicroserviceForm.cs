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
    public partial class ListMicroserviceForm : Form
    {
        public ListMicroserviceForm()
        {
            InitializeComponent();
            UpdateMicroserviceList();
        }

        private void UpdateMicroserviceList()
        {
            MicroserviceService microserviceService = new MicroserviceService();
            List<Microservice> lstMicroservice = microserviceService.GetAll();

            lstItemsMicroservice.Items.Clear();
            lstItemsMicroservice.DisplayMember = "DisplayName";
            lstItemsMicroservice.ValueMember = "Id";

            foreach (var microservice in lstMicroservice)
            {
                lstItemsMicroservice.Items.Add(microservice);
            }
        }

        private void btnAddMicroservice_Click(object sender, EventArgs e)
        {
            CreateMicroserviceForm createMicroserviceForm = new CreateMicroserviceForm();
            createMicroserviceForm.Show();

            Sair();
        }

        private void btnEditMicroservice_Click(object sender, EventArgs e)
        {
            EditMicroserviceForm editMicroserviceForm = new EditMicroserviceForm();
            editMicroserviceForm.Show();
        }

        private void btnDeleteMicroservice_Click(object sender, EventArgs e)
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
