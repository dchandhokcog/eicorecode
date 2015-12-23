namespace EnvInt.Win32.FieldTech
{
    partial class FormInspect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInspect));
            this.comboInspectionUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxInstrument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBackground = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxReading = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkBoxRemoveInspection = new System.Windows.Forms.CheckBox();
            this.pictureBoxEditMode = new System.Windows.Forms.PictureBox();
            this.buttonResetReading = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAnalyzerPPM = new System.Windows.Forms.TextBox();
            this.buttonDeviceSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEditMode)).BeginInit();
            this.SuspendLayout();
            // 
            // comboInspectionUser
            // 
            resources.ApplyResources(this.comboInspectionUser, "comboInspectionUser");
            this.comboInspectionUser.FormattingEnabled = true;
            this.comboInspectionUser.Name = "comboInspectionUser";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxInstrument
            // 
            resources.ApplyResources(this.textBoxInstrument, "textBoxInstrument");
            this.textBoxInstrument.Name = "textBoxInstrument";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxBackground
            // 
            resources.ApplyResources(this.textBoxBackground, "textBoxBackground");
            this.textBoxBackground.Name = "textBoxBackground";
            this.textBoxBackground.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBackground_KeyPress);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxReading
            // 
            resources.ApplyResources(this.textBoxReading, "textBoxReading");
            this.textBoxReading.Name = "textBoxReading";
            this.textBoxReading.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxReading_KeyPress);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkBoxRemoveInspection
            // 
            resources.ApplyResources(this.checkBoxRemoveInspection, "checkBoxRemoveInspection");
            this.checkBoxRemoveInspection.Name = "checkBoxRemoveInspection";
            this.checkBoxRemoveInspection.UseVisualStyleBackColor = true;
            this.checkBoxRemoveInspection.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // pictureBoxEditMode
            // 
            resources.ApplyResources(this.pictureBoxEditMode, "pictureBoxEditMode");
            this.pictureBoxEditMode.Name = "pictureBoxEditMode";
            this.pictureBoxEditMode.TabStop = false;
            // 
            // buttonResetReading
            // 
            resources.ApplyResources(this.buttonResetReading, "buttonResetReading");
            this.buttonResetReading.Name = "buttonResetReading";
            this.buttonResetReading.UseVisualStyleBackColor = true;
            this.buttonResetReading.Click += new System.EventHandler(this.buttonResetReading_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxAnalyzerPPM
            // 
            resources.ApplyResources(this.textBoxAnalyzerPPM, "textBoxAnalyzerPPM");
            this.textBoxAnalyzerPPM.Name = "textBoxAnalyzerPPM";
            this.textBoxAnalyzerPPM.TextChanged += new System.EventHandler(this.textBoxAnalyzerPPM_TextChanged);
            // 
            // buttonDeviceSettings
            // 
            this.buttonDeviceSettings.Image = global::EnvInt.Win32.FieldTech.Properties.Resources._26;
            resources.ApplyResources(this.buttonDeviceSettings, "buttonDeviceSettings");
            this.buttonDeviceSettings.Name = "buttonDeviceSettings";
            this.buttonDeviceSettings.UseVisualStyleBackColor = true;
            this.buttonDeviceSettings.Click += new System.EventHandler(this.buttonDeviceSettings_Click);
            // 
            // FormInspect
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDeviceSettings);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxAnalyzerPPM);
            this.Controls.Add(this.buttonResetReading);
            this.Controls.Add(this.pictureBoxEditMode);
            this.Controls.Add(this.checkBoxRemoveInspection);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxReading);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxBackground);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxInstrument);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboInspectionUser);
            this.Name = "FormInspect";
            this.Load += new System.EventHandler(this.FormInspect_Load);
            this.VisibleChanged += new System.EventHandler(this.FormInspect_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEditMode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboInspectionUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxInstrument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBackground;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxReading;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxRemoveInspection;
        private System.Windows.Forms.PictureBox pictureBoxEditMode;
        private System.Windows.Forms.Button buttonResetReading;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAnalyzerPPM;
        private System.Windows.Forms.Button buttonDeviceSettings;
    }
}