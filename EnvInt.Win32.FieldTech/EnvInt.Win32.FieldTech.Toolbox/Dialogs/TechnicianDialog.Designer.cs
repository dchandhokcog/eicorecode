namespace EnvInt.Win32.FieldTech.Dialogs
{
    partial class TechnicianDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechnicianDialog));
            this.listBoxTechnicians = new System.Windows.Forms.ListBox();
            this.buttonAddTechnician = new System.Windows.Forms.Button();
            this.buttonDeleteTechnician = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxTechnicians
            // 
            this.listBoxTechnicians.FormattingEnabled = true;
            this.listBoxTechnicians.Location = new System.Drawing.Point(12, 12);
            this.listBoxTechnicians.Name = "listBoxTechnicians";
            this.listBoxTechnicians.Size = new System.Drawing.Size(271, 264);
            this.listBoxTechnicians.TabIndex = 0;
            // 
            // buttonAddTechnician
            // 
            this.buttonAddTechnician.Image = global::EnvInt.Win32.FieldTech.Properties.Resources.add_32;
            this.buttonAddTechnician.Location = new System.Drawing.Point(298, 12);
            this.buttonAddTechnician.Name = "buttonAddTechnician";
            this.buttonAddTechnician.Size = new System.Drawing.Size(43, 43);
            this.buttonAddTechnician.TabIndex = 1;
            this.buttonAddTechnician.UseVisualStyleBackColor = true;
            this.buttonAddTechnician.Click += new System.EventHandler(this.buttonAddTechnician_Click);
            // 
            // buttonDeleteTechnician
            // 
            this.buttonDeleteTechnician.Image = global::EnvInt.Win32.FieldTech.Properties.Resources.delete_32;
            this.buttonDeleteTechnician.Location = new System.Drawing.Point(298, 75);
            this.buttonDeleteTechnician.Name = "buttonDeleteTechnician";
            this.buttonDeleteTechnician.Size = new System.Drawing.Size(43, 43);
            this.buttonDeleteTechnician.TabIndex = 2;
            this.buttonDeleteTechnician.UseVisualStyleBackColor = true;
            this.buttonDeleteTechnician.Click += new System.EventHandler(this.buttonDeleteTechnician_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(208, 290);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(114, 290);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // TechnicianDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 322);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonDeleteTechnician);
            this.Controls.Add(this.buttonAddTechnician);
            this.Controls.Add(this.listBoxTechnicians);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TechnicianDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Technicians";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TechnicianDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTechnicians;
        private System.Windows.Forms.Button buttonAddTechnician;
        private System.Windows.Forms.Button buttonDeleteTechnician;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}