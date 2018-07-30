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
    public partial class EditMicroserviceForm : Form
    {
        private MicroserviceService microserviceService;
        private int _selectedMicroserviceId;

        public EditMicroserviceForm(int selectedMicroserviceId)
        {
            InitializeComponent();
            _selectedMicroserviceId = selectedMicroserviceId;

            microserviceService = new MicroserviceService();
            Microservice microservice = microserviceService.GetById(_selectedMicroserviceId);

            txtName.Text = microservice.DisplayName;
            txtFileFolder.Text = microservice.Directory + microservice.FileName + microservice.FileExtension;
            txtDescription.Text = microservice.Description;
            lblId.Text = microservice.Id.ToString();
        }

        private void btnEditMicroservice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtDescription.Text) ||
                string.IsNullOrEmpty(txtFileFolder.Text))
            {
                MessageBox.Show("Por favor, preencha os três campos.");
                return;
            }

            string[] splittedPath = txtFileFolder.Text.Split('\\');

            if (splittedPath.Count() > 0)
            {
                try
                {
                    string fullName = splittedPath[splittedPath.Count() - 1];
                    
                    Microservice microservice = microserviceService.GetById(_selectedMicroserviceId);
                    microservice.DisplayName = txtName.Text;
                    microservice.Directory = RemoveExecutableNameFromFullPath(txtFileFolder.Text, fullName);
                    microservice.FileName = fullName.Replace(".exe", "");
                    microservice.FileExtension = ".exe";
                    microservice.Description = txtDescription.Text;

                    microserviceService.Update(microservice);

                    MessageBox.Show("Microsserviço atualizado com sucesso.");

                    Sair();
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private string RemoveExecutableNameFromFullPath(string fullPath, string executableFullName)
        {
            return fullPath.Replace(executableFullName, "");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sair();
        }

        private void Sair()
        {
            ListMicroserviceForm listMicroserviceForm = new ListMicroserviceForm();
            listMicroserviceForm.Show();

            this.Dispose();
            this.Close();
        }

        private void txtFileFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtFileFolder.Text = openFileDialog.FileName;
            }
        }

        private void EditMicroserviceForm_Load(object sender, EventArgs e)
        {
            Sair();
        }

        private void EditMicroserviceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sair();
        }
    }
}
