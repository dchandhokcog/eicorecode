namespace EnvInt.Win32.EiMOC
{
    partial class FormChildObject
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxComponentType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.textBoxPreviousTag = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonInspect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelInspection = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 168);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(77, 36);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(221, 168);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 36);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // comboBoxComponentType
            // 
            this.comboBoxComponentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComponentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxComponentType.FormattingEnabled = true;
            this.comboBoxComponentType.Location = new System.Drawing.Point(94, 24);
            this.comboBoxComponentType.MaxDropDownItems = 99;
            this.comboBoxComponentType.Name = "comboBoxComponentType";
            this.comboBoxComponentType.Size = new System.Drawing.Size(202, 28);
            this.comboBoxComponentType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Location";
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLocation.Location = new System.Drawing.Point(94, 95);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(202, 26);
            this.textBoxLocation.TabIndex = 7;
            // 
            // textBoxPreviousTag
            // 
            this.textBoxPreviousTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPreviousTag.Location = new System.Drawing.Point(94, 61);
            this.textBoxPreviousTag.Name = "textBoxPreviousTag";
            this.textBoxPreviousTag.Size = new System.Drawing.Size(202, 26);
            this.textBoxPreviousTag.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Old Tag:";
            // 
            // buttonInspect
            // 
            this.buttonInspect.Location = new System.Drawing.Point(117, 168);
            this.buttonInspect.Name = "buttonInspect";
            this.buttonInspect.Size = new System.Drawing.Size(77, 36);
            this.buttonInspect.TabIndex = 10;
            this.buttonInspect.Text = "&Inspect";
            this.buttonInspect.UseVisualStyleBackColor = true;
            this.buttonInspect.Click += new System.EventHandler(this.buttonInspect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Inspection";
            // 
            // labelInspection
            // 
            this.labelInspection.AutoSize = true;
            this.labelInspection.Location = new System.Drawing.Point(91, 134);
            this.labelInspection.Name = "labelInspection";
            this.labelInspection.Size = new System.Drawing.Size(74, 13);
            this.labelInspection.TabIndex = 12;
            this.labelInspection.Text = "Not Inspected";
            // 
            // FormChildObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(327, 216);
            this.ControlBox = false;
            this.Controls.Add(this.labelInspection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonInspect);
            this.Controls.Add(this.textBoxPreviousTag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxComponentType);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormChildObject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Child Object";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxComponentType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.TextBox textBoxPreviousTag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonInspect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelInspection;
    }
}