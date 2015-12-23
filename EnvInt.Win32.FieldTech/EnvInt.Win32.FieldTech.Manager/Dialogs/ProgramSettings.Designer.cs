namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    partial class ProgramSettings
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
            this.checkBoxStrictUnitChecking = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxLimitQAQC = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxStrictUnitChecking
            // 
            this.checkBoxStrictUnitChecking.AutoSize = true;
            this.checkBoxStrictUnitChecking.Location = new System.Drawing.Point(21, 26);
            this.checkBoxStrictUnitChecking.Name = "checkBoxStrictUnitChecking";
            this.checkBoxStrictUnitChecking.Size = new System.Drawing.Size(120, 17);
            this.checkBoxStrictUnitChecking.TabIndex = 0;
            this.checkBoxStrictUnitChecking.Text = "Strict Unit Checking";
            this.checkBoxStrictUnitChecking.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(75, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save and Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxLimitQAQC
            // 
            this.checkBoxLimitQAQC.AutoSize = true;
            this.checkBoxLimitQAQC.Location = new System.Drawing.Point(21, 61);
            this.checkBoxLimitQAQC.Name = "checkBoxLimitQAQC";
            this.checkBoxLimitQAQC.Size = new System.Drawing.Size(116, 17);
            this.checkBoxLimitQAQC.TabIndex = 2;
            this.checkBoxLimitQAQC.Text = "Auto-refresh QAQC";
            this.checkBoxLimitQAQC.UseVisualStyleBackColor = true;
            // 
            // ProgramSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 192);
            this.Controls.Add(this.checkBoxLimitQAQC);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxStrictUnitChecking);
            this.Name = "ProgramSettings";
            this.Text = "Settings";
            this.Activated += new System.EventHandler(this.ProgramSettings_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxStrictUnitChecking;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxLimitQAQC;
    }
}