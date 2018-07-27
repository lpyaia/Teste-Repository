﻿namespace HBSIS.GE.MicroserviceManagement.WinFormPresentation.Forms
{
    partial class CreateMicroserviceForm
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
            this.SuspendLayout();
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(19, 28);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(96, 13);
            this.lblDisplayName.TabIndex = 0;
            this.lblDisplayName.Text = "Nome de Exibição:";
            // 
            // lblExecutableFolder
            // 
            this.lblExecutableFolder.AutoSize = true;
            this.lblExecutableFolder.Location = new System.Drawing.Point(7, 54);
            this.lblExecutableFolder.Name = "lblExecutableFolder";
            this.lblExecutableFolder.Size = new System.Drawing.Size(108, 13);
            this.lblExecutableFolder.TabIndex = 1;
            this.lblExecutableFolder.Text = "Pasta do Executável:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(52, 77);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(58, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Descrição:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(121, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(158, 20);
            this.txtName.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(121, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(121, 77);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(158, 20);
            this.textBox2.TabIndex = 6;
            // 
            // btnCreateMicroservice
            // 
            this.btnCreateMicroservice.Location = new System.Drawing.Point(121, 115);
            this.btnCreateMicroservice.Name = "btnCreateMicroservice";
            this.btnCreateMicroservice.Size = new System.Drawing.Size(75, 23);
            this.btnCreateMicroservice.TabIndex = 7;
            this.btnCreateMicroservice.Text = "Criar";
            this.btnCreateMicroservice.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(204, 115);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // CreateMicroserviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 151);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateMicroservice);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblExecutableFolder);
            this.Controls.Add(this.lblDisplayName);
            this.Name = "CreateMicroserviceForm";
            this.Text = "Criar Microsserviço";
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
    }
}