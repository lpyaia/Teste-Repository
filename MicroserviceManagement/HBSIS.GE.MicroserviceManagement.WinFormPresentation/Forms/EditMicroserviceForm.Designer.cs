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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnCreateMicroservice = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTextId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(32, 50);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(75, 13);
            this.lblDisplayName.TabIndex = 0;
            this.lblDisplayName.Text = "Display Name:";
            // 
            // lblExecutableFolder
            // 
            this.lblExecutableFolder.AutoSize = true;
            this.lblExecutableFolder.Location = new System.Drawing.Point(12, 76);
            this.lblExecutableFolder.Name = "lblExecutableFolder";
            this.lblExecutableFolder.Size = new System.Drawing.Size(95, 13);
            this.lblExecutableFolder.TabIndex = 1;
            this.lblExecutableFolder.Text = "Executable Folder:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(44, 99);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(113, 47);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(158, 20);
            this.txtName.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(113, 73);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(113, 99);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(158, 20);
            this.textBox2.TabIndex = 6;
            // 
            // btnCreateMicroservice
            // 
            this.btnCreateMicroservice.Location = new System.Drawing.Point(113, 137);
            this.btnCreateMicroservice.Name = "btnCreateMicroservice";
            this.btnCreateMicroservice.Size = new System.Drawing.Size(75, 23);
            this.btnCreateMicroservice.TabIndex = 7;
            this.btnCreateMicroservice.Text = "Create";
            this.btnCreateMicroservice.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(196, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(115, 28);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 13);
            this.lblId.TabIndex = 10;
            // 
            // lblTextId
            // 
            this.lblTextId.AutoSize = true;
            this.lblTextId.Location = new System.Drawing.Point(88, 29);
            this.lblTextId.Name = "lblTextId";
            this.lblTextId.Size = new System.Drawing.Size(19, 13);
            this.lblTextId.TabIndex = 9;
            this.lblTextId.Text = "Id:";
            // 
            // EditMicroserviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 234);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblTextId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateMicroservice);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblExecutableFolder);
            this.Controls.Add(this.lblDisplayName);
            this.Name = "EditMicroserviceForm";
            this.Text = "Create Microservice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblExecutableFolder;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnCreateMicroservice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTextId;
    }
}