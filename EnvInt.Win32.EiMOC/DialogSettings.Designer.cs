namespace EnvInt.Win32.EiMOC
{
    partial class DialogSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSettings));
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxShowSplash = new System.Windows.Forms.CheckBox();
            this.checkBoxOpenLastProject = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDeviceIdentifier = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(88, 227);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(187, 227);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxShowSplash
            // 
            this.checkBoxShowSplash.AutoSize = true;
            this.checkBoxShowSplash.Location = new System.Drawing.Point(31, 24);
            this.checkBoxShowSplash.Name = "checkBoxShowSplash";
            this.checkBoxShowSplash.Size = new System.Drawing.Size(132, 17);
            this.checkBoxShowSplash.TabIndex = 2;
            this.checkBoxShowSplash.Text = "Show Startup Window";
            this.checkBoxShowSplash.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenLastProject
            // 
            this.checkBoxOpenLastProject.AutoSize = true;
            this.checkBoxOpenLastProject.Location = new System.Drawing.Point(31, 47);
            this.checkBoxOpenLastProject.Name = "checkBoxOpenLastProject";
            this.checkBoxOpenLastProject.Size = new System.Drawing.Size(184, 17);
            this.checkBoxOpenLastProject.TabIndex = 4;
            this.checkBoxOpenLastProject.Text = "Open Previous Project on Startup";
            this.checkBoxOpenLastProject.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Device Identifier:";
            // 
            // textBoxDeviceIdentifier
            // 
            this.textBoxDeviceIdentifier.Location = new System.Drawing.Point(12, 105);
            this.textBoxDeviceIdentifier.Name = "textBoxDeviceIdentifier";
            this.textBoxDeviceIdentifier.Size = new System.Drawing.Size(250, 20);
            this.textBoxDeviceIdentifier.TabIndex = 6;
            // 
            // DialogSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxDeviceIdentifier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxOpenLastProject);
            this.Controls.Add(this.checkBoxShowSplash);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DialogSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DialogSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxShowSplash;
        private System.Windows.Forms.CheckBox checkBoxOpenLastProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDeviceIdentifier;
    }
}