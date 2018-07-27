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
        public CreateMicroserviceForm()
        {
            InitializeComponent();
        }

        private void btnCreateMicroservice_Click(object sender, EventArgs e)
        {
            string[] splittedPath = txtFileFolder.Text.Split('\\');

            if (splittedPath.Count() > 0)
            {
                string fullName = splittedPath[splittedPath.Count() - 1];

                Microservice microservice = new Microservice();
                microservice.DisplayName = txtName.Text;
                microservice.Directory = RemoveExecutableNameFromFullPath(txtFileFolder.Text, fullName);
                microservice.FileName = fullName.Replace(".exe", "");
                microservice.FileExtension = ".exe";
                microservice.Description = txtDescription.Text;

                MicroserviceService microserviceService = new MicroserviceService();
                microserviceService.Insert(microservice);

                DialogResult dialog = MessageBox.Show("Microsserviço adicionado com sucesso.");

                if (dialog == DialogResult.OK)
                {
                    Sair();
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

        private void CreateMicroserviceForm_Load(object sender, EventArgs e)
        {
            Sair();
        }

        private void CreateMicroserviceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sair();
        }
    }
}
