namespace EnvInt.Win32.FieldTech
{
    partial class FormOOS
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
            this.checkOOS = new System.Windows.Forms.CheckBox();
            this.checkBoxRemoved = new System.Windows.Forms.CheckBox();
            this.comboBoxOOSReason = new System.Windows.Forms.ComboBox();
            this.comboBoxPOSReason = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkOOS
            // 
            this.checkOOS.AutoSize = true;
            this.checkOOS.Location = new System.Drawing.Point(50, 48);
            this.checkOOS.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkOOS.Name = "checkOOS";
            this.checkOOS.Size = new System.Drawing.Size(70, 24);
            this.checkOOS.TabIndex = 0;
            this.checkOOS.Text = "OOS";
            this.checkOOS.UseVisualStyleBackColor = true;
            this.checkOOS.CheckedChanged += new System.EventHandler(this.checkOOS_CheckedChanged);
            // 
            // checkBoxRemoved
            // 
            this.checkBoxRemoved.AutoSize = true;
            this.checkBoxRemoved.Location = new System.Drawing.Point(48, 119);
            this.checkBoxRemoved.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxRemoved.Name = "checkBoxRemoved";
            this.checkBoxRemoved.Size = new System.Drawing.Size(103, 24);
            this.checkBoxRemoved.TabIndex = 1;
            this.checkBoxRemoved.Text = "Removed";
            this.checkBoxRemoved.UseVisualStyleBackColor = true;
            this.checkBoxRemoved.CheckedChanged += new System.EventHandler(this.checkBoxRemoved_CheckedChanged);
            // 
            // comboBoxOOSReason
            // 
            this.comboBoxOOSReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxOOSReason.FormattingEnabled = true;
            this.comboBoxOOSReason.Location = new System.Drawing.Point(202, 40);
            this.comboBoxOOSReason.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxOOSReason.Name = "comboBoxOOSReason";
            this.comboBoxOOSReason.Size = new System.Drawing.Size(325, 33);
            this.comboBoxOOSReason.TabIndex = 2;
            // 
            // comboBoxPOSReason
            // 
            this.comboBoxPOSReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPOSReason.FormattingEnabled = true;
            this.comboBoxPOSReason.Location = new System.Drawing.Point(202, 116);
            this.comboBoxPOSReason.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxPOSReason.Name = "comboBoxPOSReason";
            this.comboBoxPOSReason.Size = new System.Drawing.Size(325, 33);
            this.comboBoxPOSReason.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(320, 194);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(148, 55);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(99, 194);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(148, 55);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormOOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 294);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxPOSReason);
            this.Controls.Add(this.comboBoxOOSReason);
            this.Controls.Add(this.checkBoxRemoved);
            this.Controls.Add(this.checkOOS);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOOS";
            this.Text = "OOS";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkOOS;
        private System.Windows.Forms.CheckBox checkBoxRemoved;
        private System.Windows.Forms.ComboBox comboBoxOOSReason;
        private System.Windows.Forms.ComboBox comboBoxPOSReason;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}