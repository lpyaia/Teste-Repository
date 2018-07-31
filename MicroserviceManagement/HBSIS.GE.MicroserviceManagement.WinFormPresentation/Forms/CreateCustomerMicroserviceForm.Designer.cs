namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    partial class CreateCustomerMicroserviceForm
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
            this.lblCustomer = new System.Windows.Forms.Label();
            this.ddlCustomer = new System.Windows.Forms.ComboBox();
            this.ddlMicrosservice = new System.Windows.Forms.ComboBox();
            this.lblMicroservice = new System.Windows.Forms.Label();
            this.lblProgramArguments = new System.Windows.Forms.Label();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.chkVisibleWindow = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreateCustomerMicrosservice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(101, 32);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(42, 13);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Cliente:";
            // 
            // ddlCustomer
            // 
            this.ddlCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCustomer.FormattingEnabled = true;
            this.ddlCustomer.Location = new System.Drawing.Point(153, 24);
            this.ddlCustomer.Name = "ddlCustomer";
            this.ddlCustomer.Size = new System.Drawing.Size(210, 21);
            this.ddlCustomer.TabIndex = 1;
            // 
            // ddlMicrosservice
            // 
            this.ddlMicrosservice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMicrosservice.FormattingEnabled = true;
            this.ddlMicrosservice.Location = new System.Drawing.Point(153, 56);
            this.ddlMicrosservice.Name = "ddlMicrosservice";
            this.ddlMicrosservice.Size = new System.Drawing.Size(210, 21);
            this.ddlMicrosservice.TabIndex = 3;
            // 
            // lblMicroservice
            // 
            this.lblMicroservice.AutoSize = true;
            this.lblMicroservice.Location = new System.Drawing.Point(68, 59);
            this.lblMicroservice.Name = "lblMicroservice";
            this.lblMicroservice.Size = new System.Drawing.Size(75, 13);
            this.lblMicroservice.TabIndex = 2;
            this.lblMicroservice.Text = "Microsserviço:";
            // 
            // lblProgramArguments
            // 
            this.lblProgramArguments.AutoSize = true;
            this.lblProgramArguments.Location = new System.Drawing.Point(17, 92);
            this.lblProgramArguments.Name = "lblProgramArguments";
            this.lblProgramArguments.Size = new System.Drawing.Size(126, 13);
            this.lblProgramArguments.TabIndex = 4;
            this.lblProgramArguments.Text = "Argumentos do Programa";
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(153, 89);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(210, 20);
            this.txtArguments.TabIndex = 5;
            // 
            // chkVisibleWindow
            // 
            this.chkVisibleWindow.AutoSize = true;
            this.chkVisibleWindow.Location = new System.Drawing.Point(153, 127);
            this.chkVisibleWindow.Name = "chkVisibleWindow";
            this.chkVisibleWindow.Size = new System.Drawing.Size(92, 17);
            this.chkVisibleWindow.TabIndex = 6;
            this.chkVisibleWindow.Text = "Janela Visível";
            this.chkVisibleWindow.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(288, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreateCustomerMicrosservice
            // 
            this.btnCreateCustomerMicrosservice.Location = new System.Drawing.Point(207, 166);
            this.btnCreateCustomerMicrosservice.Name = "btnCreateCustomerMicrosservice";
            this.btnCreateCustomerMicrosservice.Size = new System.Drawing.Size(75, 23);
            this.btnCreateCustomerMicrosservice.TabIndex = 7;
            this.btnCreateCustomerMicrosservice.Text = "Criar";
            this.btnCreateCustomerMicrosservice.UseVisualStyleBackColor = true;
            this.btnCreateCustomerMicrosservice.Click += new System.EventHandler(this.btnCreateCustomerMicrosservice_Click);
            // 
            // CreateCustomerMicroserviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 202);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateCustomerMicrosservice);
            this.Controls.Add(this.chkVisibleWindow);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.lblProgramArguments);
            this.Controls.Add(this.ddlMicrosservice);
            this.Controls.Add(this.lblMicroservice);
            this.Controls.Add(this.ddlCustomer);
            this.Controls.Add(this.lblCustomer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateCustomerMicroserviceForm";
            this.Text = "Criar Microsserviços de Clientes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox ddlCustomer;
        private System.Windows.Forms.ComboBox ddlMicrosservice;
        private System.Windows.Forms.Label lblMicroservice;
        private System.Windows.Forms.Label lblProgramArguments;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.CheckBox chkVisibleWindow;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreateCustomerMicrosservice;
    }
}