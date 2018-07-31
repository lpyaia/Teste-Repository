namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    partial class EditMicroserviceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblExecutableFolder = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtFileFolder = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnEditMicroservice = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(14, 38);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(96, 13);
            this.lblDisplayName.TabIndex = 0;
            this.lblDisplayName.Text = "Nome de Exibição:";
            // 
            // lblExecutableFolder
            // 
            this.lblExecutableFolder.AutoSize = true;
            this.lblExecutableFolder.Location = new System.Drawing.Point(47, 63);
            this.lblExecutableFolder.Name = "lblExecutableFolder";
            this.lblExecutableFolder.Size = new System.Drawing.Size(63, 13);
            this.lblExecutableFolder.TabIndex = 1;
            this.lblExecutableFolder.Text = "Executável:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(52, 86);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(58, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Descrição:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(121, 34);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(158, 20);
            this.txtName.TabIndex = 4;
            // 
            // txtFileFolder
            // 
            this.txtFileFolder.Location = new System.Drawing.Point(121, 60);
            this.txtFileFolder.Name = "txtFileFolder";
            this.txtFileFolder.Size = new System.Drawing.Size(158, 20);
            this.txtFileFolder.TabIndex = 5;
            this.txtFileFolder.Click += new System.EventHandler(this.txtFileFolder_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(121, 86);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(158, 20);
            this.txtDescription.TabIndex = 6;
            // 
            // btnEditMicroservice
            // 
            this.btnEditMicroservice.Location = new System.Drawing.Point(121, 124);
            this.btnEditMicroservice.Name = "btnEditMicroservice";
            this.btnEditMicroservice.Size = new System.Drawing.Size(75, 23);
            this.btnEditMicroservice.TabIndex = 7;
            this.btnEditMicroservice.Text = "Editar";
            this.btnEditMicroservice.UseVisualStyleBackColor = true;
            this.btnEditMicroservice.Click += new System.EventHandler(this.btnEditMicroservice_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(204, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Id:";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(124, 15);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 13);
            this.lblId.TabIndex = 10;
            // 
            // EditMicroserviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 163);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEditMicroservice);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtFileFolder);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblExecutableFolder);
            this.Controls.Add(this.lblDisplayName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditMicroserviceForm";
            this.Text = "Editar Microsserviço";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditMicroserviceForm_FormClosing);
            this.Load += new System.EventHandler(this.EditMicroserviceForm_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblExecutableFolder;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFileFolder;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnEditMicroservice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblId;
    }
}