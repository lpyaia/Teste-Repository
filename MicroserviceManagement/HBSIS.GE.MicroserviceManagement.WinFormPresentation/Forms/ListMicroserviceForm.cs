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
        private MicroserviceService microserviceService;
        private int _selectedMicroserviceId;

        public ListMicroserviceForm()
        {
            InitializeComponent();
            microserviceService = new MicroserviceService();

            UpdateMicroserviceList();
        }

        private void UpdateMicroserviceList()
        {
            
            List<Microservice> lstMicroservice = microserviceService.GetAll();

            lblDescricao.Text = "";

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
            if(_selectedMicroserviceId == 0)
            {
                MessageBox.Show("Por favor, selecione um microsserviço para editar.");
                return;
            }

            EditMicroserviceForm editMicroserviceForm = new EditMicroserviceForm(_selectedMicroserviceId);
            editMicroserviceForm.Show();

            Sair();
        }

        private void btnDeleteMicroservice_Click(object sender, EventArgs e)
        {
            if (_selectedMicroserviceId == 0)
            {
                MessageBox.Show("Por favor, selecione um microsserviço para excluir.");
                return;
            }

            DialogResult result = MessageBox.Show("Deseja realmente excluir?", "Atenção", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Excluir(_selectedMicroserviceId);
                UpdateMicroserviceList();

                _selectedMicroserviceId = 0;
            }
        }

        private void Excluir(int microserviceId)
        {
            try
            {
                microserviceService.Delete(microserviceId);
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
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

        private void lstItemsMicroservice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Microservice microservice = (Microservice)lstItemsMicroservice.SelectedItem;
            _selectedMicroserviceId = microservice != null ? microservice.Id : 0;
            lblDescricao.Text = microservice != null ? microservice.Description : "";
        }

        private void ListMicroserviceForm_Load(object sender, EventArgs e)
        {
            btnSair.Focus();
        }
    }
}
