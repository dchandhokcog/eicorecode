namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    partial class ProjectProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectProperties));
            this.labelProjectName = new System.Windows.Forms.Label();
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.expandoGroupBox1 = new EnvInt.Win32.Controls.ExpandoGroupBox();
            this.labelDatabaseSummary = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonTestSqlConnection = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.comboBoxAuthentication = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownChildStart = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPaddedZeros = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRoutePaddedZeros = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.gbTagFormat = new System.Windows.Forms.GroupBox();
            this.cbTagFormat = new System.Windows.Forms.ComboBox();
            this.lblTagFormat = new System.Windows.Forms.Label();
            this.txtRangeTo = new System.Windows.Forms.TextBox();
            this.txtRangeFrom = new System.Windows.Forms.TextBox();
            this.lblRangeTo = new System.Windows.Forms.Label();
            this.lblRangeFrom = new System.Windows.Forms.Label();
            this.expandoGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChildStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPaddedZeros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoutePaddedZeros)).BeginInit();
            this.gbTagFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(12, 15);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(74, 13);
            this.labelProjectName.TabIndex = 2;
            this.labelProjectName.Text = "Project Name:";
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(120, 12);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(399, 20);
            this.textBoxProjectName.TabIndex = 1;
            // 
            // expandoGroupBox1
            // 
            this.expandoGroupBox1.AutoSize = true;
            this.expandoGroupBox1.CollapseText = "Hide";
            this.expandoGroupBox1.Controls.Add(this.labelDatabaseSummary);
            this.expandoGroupBox1.Controls.Add(this.buttonOK);
            this.expandoGroupBox1.Controls.Add(this.buttonCancel);
            this.expandoGroupBox1.Controls.Add(this.buttonTestSqlConnection);
            this.expandoGroupBox1.Controls.Add(this.textBoxPassword);
            this.expandoGroupBox1.Controls.Add(this.textBoxUsername);
            this.expandoGroupBox1.Controls.Add(this.label2);
            this.expandoGroupBox1.Controls.Add(this.labelPassword);
            this.expandoGroupBox1.Controls.Add(this.label3);
            this.expandoGroupBox1.Controls.Add(this.labelUsername);
            this.expandoGroupBox1.Controls.Add(this.textBoxServer);
            this.expandoGroupBox1.Controls.Add(this.label4);
            this.expandoGroupBox1.Controls.Add(this.textBoxDatabase);
            this.expandoGroupBox1.Controls.Add(this.comboBoxAuthentication);
            this.expandoGroupBox1.ExpandedState = EnvInt.Win32.Controls.ExpandoGroupBox.ExpandGroupBoxState.Expanded;
            this.expandoGroupBox1.ExpandText = "Configure";
            this.expandoGroupBox1.Location = new System.Drawing.Point(15, 199);
            this.expandoGroupBox1.Name = "expandoGroupBox1";
            this.expandoGroupBox1.Size = new System.Drawing.Size(492, 251);
            this.expandoGroupBox1.TabIndex = 26;
            this.expandoGroupBox1.TabStop = false;
            this.expandoGroupBox1.Text = "LDAR Database";
            // 
            // labelDatabaseSummary
            // 
            this.labelDatabaseSummary.AutoSize = true;
            this.labelDatabaseSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelDatabaseSummary.Location = new System.Drawing.Point(12, 189);
            this.labelDatabaseSummary.Name = "labelDatabaseSummary";
            this.labelDatabaseSummary.Size = new System.Drawing.Size(0, 13);
            this.labelDatabaseSummary.TabIndex = 20;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(411, 209);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(313, 209);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click_1);
            // 
            // buttonTestSqlConnection
            // 
            this.buttonTestSqlConnection.Location = new System.Drawing.Point(331, 142);
            this.buttonTestSqlConnection.Name = "buttonTestSqlConnection";
            this.buttonTestSqlConnection.Size = new System.Drawing.Size(155, 23);
            this.buttonTestSqlConnection.TabIndex = 7;
            this.buttonTestSqlConnection.Text = "Test Connection";
            this.buttonTestSqlConnection.UseVisualStyleBackColor = true;
            this.buttonTestSqlConnection.Click += new System.EventHandler(this.buttonTestSqlConnection_Click_1);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(165, 144);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(152, 20);
            this.textBoxPassword.TabIndex = 6;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(165, 117);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(152, 20);
            this.textBoxUsername.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Server:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(90, 147);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 19;
            this.labelPassword.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Database:";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(90, 120);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(58, 13);
            this.labelUsername.TabIndex = 18;
            this.labelUsername.Text = "Username:";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(93, 34);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(393, 20);
            this.textBoxServer.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Authentication:";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(93, 62);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(393, 20);
            this.textBoxDatabase.TabIndex = 3;
            // 
            // comboBoxAuthentication
            // 
            this.comboBoxAuthentication.FormattingEnabled = true;
            this.comboBoxAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Authentication"});
            this.comboBoxAuthentication.Location = new System.Drawing.Point(93, 90);
            this.comboBoxAuthentication.Name = "comboBoxAuthentication";
            this.comboBoxAuthentication.Size = new System.Drawing.Size(224, 21);
            this.comboBoxAuthentication.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Project Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Start Children at:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(311, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Number of Child Digits:";
            // 
            // numericUpDownChildStart
            // 
            this.numericUpDownChildStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownChildStart.Location = new System.Drawing.Point(120, 41);
            this.numericUpDownChildStart.Name = "numericUpDownChildStart";
            this.numericUpDownChildStart.Size = new System.Drawing.Size(73, 26);
            this.numericUpDownChildStart.TabIndex = 32;
            // 
            // numericUpDownPaddedZeros
            // 
            this.numericUpDownPaddedZeros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownPaddedZeros.Location = new System.Drawing.Point(446, 41);
            this.numericUpDownPaddedZeros.Name = "numericUpDownPaddedZeros";
            this.numericUpDownPaddedZeros.Size = new System.Drawing.Size(73, 26);
            this.numericUpDownPaddedZeros.TabIndex = 33;
            // 
            // numericUpDownRoutePaddedZeros
            // 
            this.numericUpDownRoutePaddedZeros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownRoutePaddedZeros.Location = new System.Drawing.Point(446, 78);
            this.numericUpDownRoutePaddedZeros.Name = "numericUpDownRoutePaddedZeros";
            this.numericUpDownRoutePaddedZeros.Size = new System.Drawing.Size(73, 26);
            this.numericUpDownRoutePaddedZeros.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(311, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Route Padded Zeros:";
            // 
            // gbTagFormat
            // 
            this.gbTagFormat.Controls.Add(this.cbTagFormat);
            this.gbTagFormat.Controls.Add(this.lblTagFormat);
            this.gbTagFormat.Controls.Add(this.txtRangeTo);
            this.gbTagFormat.Controls.Add(this.txtRangeFrom);
            this.gbTagFormat.Controls.Add(this.lblRangeTo);
            this.gbTagFormat.Controls.Add(this.lblRangeFrom);
            this.gbTagFormat.Location = new System.Drawing.Point(15, 110);
            this.gbTagFormat.Name = "gbTagFormat";
            this.gbTagFormat.Size = new System.Drawing.Size(492, 78);
            this.gbTagFormat.TabIndex = 36;
            this.gbTagFormat.TabStop = false;
            this.gbTagFormat.Text = "Tag Settings";
            // 
            // cbTagFormat
            // 
            this.cbTagFormat.FormattingEnabled = true;
            this.cbTagFormat.Items.AddRange(new object[] {
            "None",
            "Integer",
            "Integer-Alpha",
            "String-Integer",
            "String-Integer-Alpha"});
            this.cbTagFormat.Location = new System.Drawing.Point(105, 16);
            this.cbTagFormat.Name = "cbTagFormat";
            this.cbTagFormat.Size = new System.Drawing.Size(135, 21);
            this.cbTagFormat.TabIndex = 21;
            this.cbTagFormat.SelectedIndexChanged += new System.EventHandler(this.cbTagFormat_SelectedIndexChanged);
            // 
            // lblTagFormat
            // 
            this.lblTagFormat.AutoSize = true;
            this.lblTagFormat.Location = new System.Drawing.Point(15, 24);
            this.lblTagFormat.Name = "lblTagFormat";
            this.lblTagFormat.Size = new System.Drawing.Size(64, 13);
            this.lblTagFormat.TabIndex = 37;
            this.lblTagFormat.Text = "Tag Format:";
            // 
            // txtRangeTo
            // 
            this.txtRangeTo.Location = new System.Drawing.Point(373, 46);
            this.txtRangeTo.Name = "txtRangeTo";
            this.txtRangeTo.Size = new System.Drawing.Size(113, 20);
            this.txtRangeTo.TabIndex = 3;
            // 
            // txtRangeFrom
            // 
            this.txtRangeFrom.Location = new System.Drawing.Point(105, 46);
            this.txtRangeFrom.Name = "txtRangeFrom";
            this.txtRangeFrom.Size = new System.Drawing.Size(135, 20);
            this.txtRangeFrom.TabIndex = 2;
            // 
            // lblRangeTo
            // 
            this.lblRangeTo.AutoSize = true;
            this.lblRangeTo.Location = new System.Drawing.Point(296, 49);
            this.lblRangeTo.Name = "lblRangeTo";
            this.lblRangeTo.Size = new System.Drawing.Size(58, 13);
            this.lblRangeTo.TabIndex = 1;
            this.lblRangeTo.Text = "Range To:";
            // 
            // lblRangeFrom
            // 
            this.lblRangeFrom.AutoSize = true;
            this.lblRangeFrom.Location = new System.Drawing.Point(15, 49);
            this.lblRangeFrom.Name = "lblRangeFrom";
            this.lblRangeFrom.Size = new System.Drawing.Size(68, 13);
            this.lblRangeFrom.TabIndex = 0;
            this.lblRangeFrom.Text = "Range From:";
            // 
            // ProjectProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(527, 453);
            this.Controls.Add(this.gbTagFormat);
            this.Controls.Add(this.numericUpDownRoutePaddedZeros);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownPaddedZeros);
            this.Controls.Add(this.numericUpDownChildStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.expandoGroupBox1);
            this.Controls.Add(this.textBoxProjectName);
            this.Controls.Add(this.labelProjectName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Properties";
            this.Load += new System.EventHandler(this.ProjectProperties_Load);
            this.expandoGroupBox1.ResumeLayout(false);
            this.expandoGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChildStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPaddedZeros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoutePaddedZeros)).EndInit();
            this.gbTagFormat.ResumeLayout(false);
            this.gbTagFormat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private Win32.Controls.ExpandoGroupBox expandoGroupBox1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonTestSqlConnection;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.ComboBox comboBoxAuthentication;
        private System.Windows.Forms.Label labelDatabaseSummary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownChildStart;
        private System.Windows.Forms.NumericUpDown numericUpDownPaddedZeros;
        private System.Windows.Forms.NumericUpDown numericUpDownRoutePaddedZeros;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbTagFormat;
        private System.Windows.Forms.TextBox txtRangeTo;
        private System.Windows.Forms.TextBox txtRangeFrom;
        private System.Windows.Forms.Label lblRangeTo;
        private System.Windows.Forms.Label lblRangeFrom;
        private System.Windows.Forms.ComboBox cbTagFormat;
        private System.Windows.Forms.Label lblTagFormat;
    }
}