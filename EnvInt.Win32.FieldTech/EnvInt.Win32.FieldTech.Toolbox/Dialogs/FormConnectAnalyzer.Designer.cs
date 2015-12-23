namespace EnvInt.Win32.FieldTech.Dialogs
{
    partial class FormConnectAnalyzer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnectAnalyzer));
            this.buttonSelectDevice = new System.Windows.Forms.Button();
            this.phxDisplay1 = new BOKControls.PhxDisplay();
            this.buttonControl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSelectDevice
            // 
            resources.ApplyResources(this.buttonSelectDevice, "buttonSelectDevice");
            this.buttonSelectDevice.Name = "buttonSelectDevice";
            this.buttonSelectDevice.UseVisualStyleBackColor = true;
            this.buttonSelectDevice.Click += new System.EventHandler(this.buttonSelectDevice_Click);
            // 
            // phxDisplay1
            // 
            this.phxDisplay1.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.phxDisplay1, "phxDisplay1");
            this.phxDisplay1.Name = "phxDisplay1";
            // 
            // buttonControl
            // 
            resources.ApplyResources(this.buttonControl, "buttonControl");
            this.buttonControl.Name = "buttonControl";
            this.buttonControl.UseVisualStyleBackColor = true;
            this.buttonControl.Click += new System.EventHandler(this.buttonControl_Click);
            // 
            // FormConnectAnalyzer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonControl);
            this.Controls.Add(this.phxDisplay1);
            this.Controls.Add(this.buttonSelectDevice);
            this.Name = "FormConnectAnalyzer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectDevice;
        private BOKControls.PhxDisplay phxDisplay1;
        private System.Windows.Forms.Button buttonControl;
    }
}