namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    partial class EditCustomerForm
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
            this.btnEditCustomer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDirecotry = new System.Windows.Forms.Label();
            this.txtCustomerFolder = new System.Windows.Forms.TextBox();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblTextId = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Location = new System.Drawing.Point(106, 109);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(75, 23);
            this.btnEditCustomer.TabIndex = 0;
            this.btnEditCustomer.Text = "Editar";
            this.btnEditCustomer.UseVisualStyleBackColor = true;
            this.btnEditCustomer.Click += new System.EventHandler(this.btnEditCustomer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(187, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(60, 45);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Nome:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(104, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(158, 20);
            this.txtName.TabIndex = 3;
            // 
            // lblDirecotry
            // 
            this.lblDirecotry.AutoSize = true;
            this.lblDirecotry.Location = new System.Drawing.Point(12, 71);
            this.lblDirecotry.Name = "lblDirecotry";
            this.lblDirecotry.Size = new System.Drawing.Size(87, 13);
            this.lblDirecotry.TabIndex = 4;
            this.lblDirecotry.Text = "Pasta do Cliente:";
            // 
            // txtCustomerFolder
            // 
            this.txtCustomerFolder.Location = new System.Drawing.Point(104, 68);
            this.txtCustomerFolder.Name = "txtCustomerFolder";
            this.txtCustomerFolder.Size = new System.Drawing.Size(158, 20);
            this.txtCustomerFolder.TabIndex = 5;
            this.txtCustomerFolder.Click += new System.EventHandler(this.txtCustomerFolder_Click);
            // 
            // lblTextId
            // 
            this.lblTextId.AutoSize = true;
            this.lblTextId.Location = new System.Drawing.Point(77, 23);
            this.lblTextId.Name = "lblTextId";
            this.lblTextId.Size = new System.Drawing.Size(19, 13);
            this.lblTextId.TabIndex = 6;
            this.lblTextId.Text = "Id:";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(107, 23);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 13);
            this.lblId.TabIndex = 7;
            // 
            // EditCustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 151);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblTextId);
            this.Controls.Add(this.txtCustomerFolder);
            this.Controls.Add(this.lblDirecotry);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEditCustomer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditCustomerForm";
            this.Text = "Create Customer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditCustomer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDirecotry;
        private System.Windows.Forms.TextBox txtCustomerFolder;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Label lblTextId;
        private System.Windows.Forms.Label lblId;
    }
}