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

        private void txtCustomerFolder_TextChanged(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                txtCustomerFolder.Text = folderDialog.SelectedPath;
                Environment.SpecialFolder root = folderDialog.RootFolder;
            }
        }
    }
}
