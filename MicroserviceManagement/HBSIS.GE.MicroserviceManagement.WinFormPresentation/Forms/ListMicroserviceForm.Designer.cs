namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    partial class ListMicroserviceForm
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
            this.lstCustomer = new System.Windows.Forms.ListBox();
            this.btnAddMicroservice = new System.Windows.Forms.Button();
            this.btnEditMicroservice = new System.Windows.Forms.Button();
            this.btnDeleteMicroservice = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.gboxDescricao = new System.Windows.Forms.GroupBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.gboxDescricao.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCustomer
            // 
            this.lstCustomer.FormattingEnabled = true;
            this.lstCustomer.Location = new System.Drawing.Point(159, 12);
            this.lstCustomer.Name = "lstCustomer";
            this.lstCustomer.Size = new System.Drawing.Size(203, 277);
            this.lstCustomer.TabIndex = 0;
            // 
            // btnAddMicroservice
            // 
            this.btnAddMicroservice.Location = new System.Drawing.Point(368, 12);
            this.btnAddMicroservice.Name = "btnAddMicroservice";
            this.btnAddMicroservice.Size = new System.Drawing.Size(157, 87);
            this.btnAddMicroservice.TabIndex = 1;
            this.btnAddMicroservice.Text = "Adicionar Microsserviço";
            this.btnAddMicroservice.UseVisualStyleBackColor = true;
            // 
            // btnEditMicroservice
            // 
            this.btnEditMicroservice.Location = new System.Drawing.Point(368, 105);
            this.btnEditMicroservice.Name = "btnEditMicroservice";
            this.btnEditMicroservice.Size = new System.Drawing.Size(157, 87);
            this.btnEditMicroservice.TabIndex = 4;
            this.btnEditMicroservice.Text = "Editar Microsserviço";
            this.btnEditMicroservice.UseVisualStyleBackColor = true;
            // 
            // btnDeleteMicroservice
            // 
            this.btnDeleteMicroservice.Location = new System.Drawing.Point(368, 202);
            this.btnDeleteMicroservice.Name = "btnDeleteMicroservice";
            this.btnDeleteMicroservice.Size = new System.Drawing.Size(157, 87);
            this.btnDeleteMicroservice.TabIndex = 5;
            this.btnDeleteMicroservice.Text = "Excluir Microsserviço";
            this.btnDeleteMicroservice.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(369, 304);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(157, 23);
            this.btnSair.TabIndex = 6;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            // 
            // gboxDescricao
            // 
            this.gboxDescricao.Controls.Add(this.lblDescricao);
            this.gboxDescricao.Location = new System.Drawing.Point(9, 12);
            this.gboxDescricao.Name = "gboxDescricao";
            this.gboxDescricao.Size = new System.Drawing.Size(144, 277);
            this.gboxDescricao.TabIndex = 7;
            this.gboxDescricao.TabStop = false;
            this.gboxDescricao.Text = "Descrição";
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(9, 20);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(0, 13);
            this.lblDescricao.TabIndex = 0;
            // 
            // ListMicroserviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 339);
            this.Controls.Add(this.gboxDescricao);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnDeleteMicroservice);
            this.Controls.Add(this.btnEditMicroservice);
            this.Controls.Add(this.btnAddMicroservice);
            this.Controls.Add(this.lstCustomer);
            this.Name = "ListMicroserviceForm";
            this.Text = "Microsserviços Cadastrados";
            this.gboxDescricao.ResumeLayout(false);
            this.gboxDescricao.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstCustomer;
        private System.Windows.Forms.Button btnAddMicroservice;
        private System.Windows.Forms.Button btnEditMicroservice;
        private System.Windows.Forms.Button btnDeleteMicroservice;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.GroupBox gboxDescricao;
        private System.Windows.Forms.Label lblDescricao;
    }
}