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
    public partial class CreateMicroserviceForm : Form
    {
        private MicroserviceService microserviceService;

        public CreateMicroserviceForm()
        {
            InitializeComponent();
            microserviceService = new MicroserviceService();
        }

        private void btnCreateMicroservice_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtDescription.Text)||
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

                    Microservice microservice = new Microservice();
                    microservice.DisplayName = txtName.Text;
                    microservice.Directory = RemoveExecutableNameFromFullPath(txtFileFolder.Text, fullName);
                    microservice.FileName = fullName.Replace(".exe", "");
                    microservice.FileExtension = ".exe";
                    microservice.Description = txtDescription.Text;

                    microserviceService.Insert(microservice);

                    DialogResult dialog = MessageBox.Show("Microsserviço adicionado com sucesso.");

                    if (dialog == DialogResult.OK)
                    {
                        Sair();
                    }
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro");
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

            Dispose();
            Close();
        }

        private void txtFileFolder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtFileFolder.Text = openFileDialog.FileName;
            }
        }

        private void CreateMicroserviceForm_Load(object sender, EventArgs e)
        {
            Sair();
        }

        private void CreateMicroserviceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sair();
        }

        private void CreateMicroserviceForm_Load_1(object sender, EventArgs e)
        {
            btnCreateMicroservice.Focus();
        }
    }
}
