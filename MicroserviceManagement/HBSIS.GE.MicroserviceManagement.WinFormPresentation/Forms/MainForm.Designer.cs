namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.microsserviçosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerMicroservicesGrid = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Microservice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddCustomerMicroservice = new System.Windows.Forms.Button();
            this.btnEditSelectedMicroservice = new System.Windows.Forms.Button();
            this.btnStartMicroservice = new System.Windows.Forms.Button();
            this.btnStopMicroservice = new System.Windows.Forms.Button();
            this.btnDeleteSelectedMicroservice = new System.Windows.Forms.Button();
            this.btnUpdateMicroservices = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customerMicroservicesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opçõesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(723, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opçõesToolStripMenuItem
            // 
            this.opçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.microsserviçosToolStripMenuItem});
            this.opçõesToolStripMenuItem.Name = "opçõesToolStripMenuItem";
            this.opçõesToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.opçõesToolStripMenuItem.Text = "Opções";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // microsserviçosToolStripMenuItem
            // 
            this.microsserviçosToolStripMenuItem.Name = "microsserviçosToolStripMenuItem";
            this.microsserviçosToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.microsserviçosToolStripMenuItem.Text = "Microsserviços";
            this.microsserviçosToolStripMenuItem.Click += new System.EventHandler(this.microsserviçosToolStripMenuItem_Click);
            // 
            // customerMicroservicesGrid
            // 
            this.customerMicroservicesGrid.AllowUserToAddRows = false;
            this.customerMicroservicesGrid.AllowUserToDeleteRows = false;
            this.customerMicroservicesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customerMicroservicesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Microservice,
            this.Customer,
            this.Description,
            this.Status});
            this.customerMicroservicesGrid.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.customerMicroservicesGrid.Location = new System.Drawing.Point(9, 61);
            this.customerMicroservicesGrid.MultiSelect = false;
            this.customerMicroservicesGrid.Name = "customerMicroservicesGrid";
            this.customerMicroservicesGrid.ReadOnly = true;
            this.customerMicroservicesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customerMicroservicesGrid.Size = new System.Drawing.Size(544, 411);
            this.customerMicroservicesGrid.TabIndex = 1;
            this.customerMicroservicesGrid.SelectionChanged += new System.EventHandler(this.customerMicroservicesGrid_SelectionChanged);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Microservice
            // 
            this.Microservice.HeaderText = "Microsserviço";
            this.Microservice.Name = "Microservice";
            this.Microservice.ReadOnly = true;
            // 
            // Customer
            // 
            this.Customer.HeaderText = "Cliente";
            this.Customer.Name = "Customer";
            this.Customer.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Descrição";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // btnAddCustomerMicroservice
            // 
            this.btnAddCustomerMicroservice.Location = new System.Drawing.Point(559, 61);
            this.btnAddCustomerMicroservice.Name = "btnAddCustomerMicroservice";
            this.btnAddCustomerMicroservice.Size = new System.Drawing.Size(149, 51);
            this.btnAddCustomerMicroservice.TabIndex = 4;
            this.btnAddCustomerMicroservice.Text = "Adicionar Microsserviço para um Cliente";
            this.btnAddCustomerMicroservice.UseVisualStyleBackColor = true;
            this.btnAddCustomerMicroservice.Click += new System.EventHandler(this.btnAddCustomerMicroservice_Click);
            // 
            // btnEditSelectedMicroservice
            // 
            this.btnEditSelectedMicroservice.Location = new System.Drawing.Point(559, 118);
            this.btnEditSelectedMicroservice.Name = "btnEditSelectedMicroservice";
            this.btnEditSelectedMicroservice.Size = new System.Drawing.Size(149, 51);
            this.btnEditSelectedMicroservice.TabIndex = 5;
            this.btnEditSelectedMicroservice.Text = "Editar Microsserviço Selecionado";
            this.btnEditSelectedMicroservice.UseVisualStyleBackColor = true;
            this.btnEditSelectedMicroservice.Click += new System.EventHandler(this.btnEditSelectedMicroservice_Click);
            // 
            // btnStartMicroservice
            // 
            this.btnStartMicroservice.Location = new System.Drawing.Point(9, 32);
            this.btnStartMicroservice.Name = "btnStartMicroservice";
            this.btnStartMicroservice.Size = new System.Drawing.Size(75, 23);
            this.btnStartMicroservice.TabIndex = 6;
            this.btnStartMicroservice.Text = "Iniciar";
            this.btnStartMicroservice.UseVisualStyleBackColor = true;
            this.btnStartMicroservice.Click += new System.EventHandler(this.btnStartMicroservice_Click);
            // 
            // btnStopMicroservice
            // 
            this.btnStopMicroservice.Location = new System.Drawing.Point(90, 32);
            this.btnStopMicroservice.Name = "btnStopMicroservice";
            this.btnStopMicroservice.Size = new System.Drawing.Size(75, 23);
            this.btnStopMicroservice.TabIndex = 7;
            this.btnStopMicroservice.Text = "Parar";
            this.btnStopMicroservice.UseVisualStyleBackColor = true;
            this.btnStopMicroservice.Click += new System.EventHandler(this.btnStopMicroservice_Click);
            // 
            // btnDeleteSelectedMicroservice
            // 
            this.btnDeleteSelectedMicroservice.Location = new System.Drawing.Point(559, 175);
            this.btnDeleteSelectedMicroservice.Name = "btnDeleteSelectedMicroservice";
            this.btnDeleteSelectedMicroservice.Size = new System.Drawing.Size(149, 51);
            this.btnDeleteSelectedMicroservice.TabIndex = 8;
            this.btnDeleteSelectedMicroservice.Text = "Excluir Microsserviço Selecionado";
            this.btnDeleteSelectedMicroservice.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedMicroservice.Click += new System.EventHandler(this.btnDeleteSelectedMicroservice_Click);
            // 
            // btnUpdateMicroservices
            // 
            this.btnUpdateMicroservices.Location = new System.Drawing.Point(369, 32);
            this.btnUpdateMicroservices.Name = "btnUpdateMicroservices";
            this.btnUpdateMicroservices.Size = new System.Drawing.Size(184, 23);
            this.btnUpdateMicroservices.TabIndex = 9;
            this.btnUpdateMicroservices.Text = "Atualizar Status Microsserviços";
            this.btnUpdateMicroservices.UseVisualStyleBackColor = true;
            this.btnUpdateMicroservices.Click += new System.EventHandler(this.btnUpdateMicroservices_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 484);
            this.Controls.Add(this.btnUpdateMicroservices);
            this.Controls.Add(this.btnDeleteSelectedMicroservice);
            this.Controls.Add(this.btnStopMicroservice);
            this.Controls.Add(this.btnStartMicroservice);
            this.Controls.Add(this.btnEditSelectedMicroservice);
            this.Controls.Add(this.btnAddCustomerMicroservice);
            this.Controls.Add(this.customerMicroservicesGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Gerenciador de Microsserviços";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customerMicroservicesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem microsserviçosToolStripMenuItem;
        private System.Windows.Forms.DataGridView customerMicroservicesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Microservice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button btnAddCustomerMicroservice;
        private System.Windows.Forms.Button btnEditSelectedMicroservice;
        private System.Windows.Forms.Button btnStartMicroservice;
        private System.Windows.Forms.Button btnStopMicroservice;
        private System.Windows.Forms.Button btnDeleteSelectedMicroservice;
        private System.Windows.Forms.Button btnUpdateMicroservices;
    }
}

