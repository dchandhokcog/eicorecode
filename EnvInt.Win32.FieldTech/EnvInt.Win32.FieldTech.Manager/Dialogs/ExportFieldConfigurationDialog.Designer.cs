namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    partial class ExportFieldConfigurationDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportFieldConfigurationDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxDrawingPackage = new System.Windows.Forms.GroupBox();
            this.comboBoxDefaultDrawing = new System.Windows.Forms.ComboBox();
            this.checkBoxCheckAllDrawings = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedListBoxDrawings = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListBoxProcessUnits = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxLDAR = new System.Windows.Forms.GroupBox();
            this.checkBoxCheckAllNone = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseFTTD = new System.Windows.Forms.Button();
            this.textBoxExportFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxDrawingPackage.SuspendLayout();
            this.groupBoxLDAR.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(588, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Use this dialog to export data to a Field Device in order to collect tagging info" +
    "rmation.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Default Drawing";
            // 
            // groupBoxDrawingPackage
            // 
            this.groupBoxDrawingPackage.Controls.Add(this.comboBoxDefaultDrawing);
            this.groupBoxDrawingPackage.Controls.Add(this.checkBoxCheckAllDrawings);
            this.groupBoxDrawingPackage.Controls.Add(this.label2);
            this.groupBoxDrawingPackage.Controls.Add(this.label6);
            this.groupBoxDrawingPackage.Controls.Add(this.checkedListBoxDrawings);
            this.groupBoxDrawingPackage.Location = new System.Drawing.Point(334, 55);
            this.groupBoxDrawingPackage.Name = "groupBoxDrawingPackage";
            this.groupBoxDrawingPackage.Size = new System.Drawing.Size(310, 244);
            this.groupBoxDrawingPackage.TabIndex = 1;
            this.groupBoxDrawingPackage.TabStop = false;
            this.groupBoxDrawingPackage.Text = "Drawing Packages";
            // 
            // comboBoxDefaultDrawing
            // 
            this.comboBoxDefaultDrawing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefaultDrawing.FormattingEnabled = true;
            this.comboBoxDefaultDrawing.Location = new System.Drawing.Point(12, 46);
            this.comboBoxDefaultDrawing.Name = "comboBoxDefaultDrawing";
            this.comboBoxDefaultDrawing.Size = new System.Drawing.Size(281, 21);
            this.comboBoxDefaultDrawing.TabIndex = 9;
            // 
            // checkBoxCheckAllDrawings
            // 
            this.checkBoxCheckAllDrawings.AutoSize = true;
            this.checkBoxCheckAllDrawings.Enabled = false;
            this.checkBoxCheckAllDrawings.Location = new System.Drawing.Point(15, 95);
            this.checkBoxCheckAllDrawings.Name = "checkBoxCheckAllDrawings";
            this.checkBoxCheckAllDrawings.Size = new System.Drawing.Size(126, 17);
            this.checkBoxCheckAllDrawings.TabIndex = 8;
            this.checkBoxCheckAllDrawings.Text = "(Check/Uncheck All)";
            this.checkBoxCheckAllDrawings.UseVisualStyleBackColor = true;
            this.checkBoxCheckAllDrawings.CheckedChanged += new System.EventHandler(this.checkBoxCheckAllDrawings_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(9, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Additional Drawings:";
            // 
            // checkedListBoxDrawings
            // 
            this.checkedListBoxDrawings.Enabled = false;
            this.checkedListBoxDrawings.FormattingEnabled = true;
            this.checkedListBoxDrawings.Location = new System.Drawing.Point(12, 114);
            this.checkedListBoxDrawings.Name = "checkedListBoxDrawings";
            this.checkedListBoxDrawings.Size = new System.Drawing.Size(281, 124);
            this.checkedListBoxDrawings.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 50);
            this.label3.TabIndex = 2;
            this.label3.Text = "Use the optional filtering below to optimize the data sent to the field device. L" +
    "oading an entire LDAR Tag History catalog may slow down field activities.";
            // 
            // checkedListBoxProcessUnits
            // 
            this.checkedListBoxProcessUnits.FormattingEnabled = true;
            this.checkedListBoxProcessUnits.Location = new System.Drawing.Point(12, 114);
            this.checkedListBoxProcessUnits.Name = "checkedListBoxProcessUnits";
            this.checkedListBoxProcessUnits.Size = new System.Drawing.Size(281, 124);
            this.checkedListBoxProcessUnits.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Process Unit:";
            // 
            // groupBoxLDAR
            // 
            this.groupBoxLDAR.Controls.Add(this.checkBoxCheckAllNone);
            this.groupBoxLDAR.Controls.Add(this.label4);
            this.groupBoxLDAR.Controls.Add(this.checkedListBoxProcessUnits);
            this.groupBoxLDAR.Controls.Add(this.label3);
            this.groupBoxLDAR.Location = new System.Drawing.Point(7, 55);
            this.groupBoxLDAR.Name = "groupBoxLDAR";
            this.groupBoxLDAR.Size = new System.Drawing.Size(304, 244);
            this.groupBoxLDAR.TabIndex = 2;
            this.groupBoxLDAR.TabStop = false;
            this.groupBoxLDAR.Text = "LDAR Database";
            // 
            // checkBoxCheckAllNone
            // 
            this.checkBoxCheckAllNone.AutoSize = true;
            this.checkBoxCheckAllNone.Checked = true;
            this.checkBoxCheckAllNone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckAllNone.Location = new System.Drawing.Point(15, 95);
            this.checkBoxCheckAllNone.Name = "checkBoxCheckAllNone";
            this.checkBoxCheckAllNone.Size = new System.Drawing.Size(126, 17);
            this.checkBoxCheckAllNone.TabIndex = 5;
            this.checkBoxCheckAllNone.Text = "(Check/Uncheck All)";
            this.checkBoxCheckAllNone.UseVisualStyleBackColor = true;
            this.checkBoxCheckAllNone.CheckedChanged += new System.EventHandler(this.checkBoxCheckAllNone_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(305, 387);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(215, 387);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Export";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseFTTD);
            this.groupBox1.Controls.Add(this.textBoxExportFile);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(7, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 76);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tablet Datafile";
            // 
            // buttonBrowseFTTD
            // 
            this.buttonBrowseFTTD.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.folder_yellow_16;
            this.buttonBrowseFTTD.Location = new System.Drawing.Point(598, 47);
            this.buttonBrowseFTTD.Name = "buttonBrowseFTTD";
            this.buttonBrowseFTTD.Size = new System.Drawing.Size(22, 23);
            this.buttonBrowseFTTD.TabIndex = 10;
            this.buttonBrowseFTTD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBrowseFTTD.UseVisualStyleBackColor = true;
            this.buttonBrowseFTTD.Click += new System.EventHandler(this.buttonBrowseFTTD_Click);
            // 
            // textBoxExportFile
            // 
            this.textBoxExportFile.Location = new System.Drawing.Point(12, 50);
            this.textBoxExportFile.Name = "textBoxExportFile";
            this.textBoxExportFile.Size = new System.Drawing.Size(580, 20);
            this.textBoxExportFile.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(526, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "Specify the EID file you want to export this project into. This is the file you w" +
    "ill use on the Field Tablet device.";
            // 
            // ExportFieldConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(658, 434);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxLDAR);
            this.Controls.Add(this.groupBoxDrawingPackage);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportFieldConfigurationDialog";
            this.Text = "Export Data for Field Activities";
            this.groupBoxDrawingPackage.ResumeLayout(false);
            this.groupBoxDrawingPackage.PerformLayout();
            this.groupBoxLDAR.ResumeLayout(false);
            this.groupBoxLDAR.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxDrawingPackage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBoxProcessUnits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxLDAR;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxExportFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonBrowseFTTD;
        private System.Windows.Forms.CheckBox checkBoxCheckAllNone;
        private System.Windows.Forms.CheckBox checkBoxCheckAllDrawings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox checkedListBoxDrawings;
        private System.Windows.Forms.ComboBox comboBoxDefaultDrawing;

    }
}