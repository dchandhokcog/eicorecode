namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    partial class NewProjectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectDialog));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.labelDatabaseSummary = new System.Windows.Forms.Label();
            this.labelCADSummary = new System.Windows.Forms.Label();
            this.checkBoxAutoLDARDatabase = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoCADProject = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.expandoGroupBox1 = new EnvInt.Win32.Controls.ExpandoGroupBox();
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
            this.expandoGroupBox2 = new EnvInt.Win32.Controls.ExpandoGroupBox();
            this.buttonClearFiles = new System.Windows.Forms.Button();
            this.buttonBrowseCADDWF = new System.Windows.Forms.Button();
            this.textBoxCADDWF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.expandoGroupBox3 = new EnvInt.Win32.Controls.ExpandoGroupBox();
            this.buttonBrowseCADProject = new System.Windows.Forms.Button();
            this.textBoxCADProjectFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.expandoGroupBox1.SuspendLayout();
            this.expandoGroupBox2.SuspendLayout();
            this.expandoGroupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(334, 56);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(432, 56);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(12, 25);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(74, 13);
            this.labelProjectName.TabIndex = 2;
            this.labelProjectName.Text = "Project Name:";
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(108, 22);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(411, 20);
            this.textBoxProjectName.TabIndex = 1;
            // 
            // labelDatabaseSummary
            // 
            this.labelDatabaseSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatabaseSummary.Location = new System.Drawing.Point(165, -1);
            this.labelDatabaseSummary.Name = "labelDatabaseSummary";
            this.labelDatabaseSummary.Size = new System.Drawing.Size(342, 23);
            this.labelDatabaseSummary.TabIndex = 5;
            this.labelDatabaseSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCADSummary
            // 
            this.labelCADSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCADSummary.Location = new System.Drawing.Point(142, 22);
            this.labelCADSummary.Name = "labelCADSummary";
            this.labelCADSummary.Size = new System.Drawing.Size(365, 23);
            this.labelCADSummary.TabIndex = 23;
            this.labelCADSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxAutoLDARDatabase
            // 
            this.checkBoxAutoLDARDatabase.AutoSize = true;
            this.checkBoxAutoLDARDatabase.Checked = true;
            this.checkBoxAutoLDARDatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoLDARDatabase.Location = new System.Drawing.Point(3, 3);
            this.checkBoxAutoLDARDatabase.Name = "checkBoxAutoLDARDatabase";
            this.checkBoxAutoLDARDatabase.Size = new System.Drawing.Size(156, 17);
            this.checkBoxAutoLDARDatabase.TabIndex = 20;
            this.checkBoxAutoLDARDatabase.Text = "Auto-Load LDAR Database";
            this.checkBoxAutoLDARDatabase.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoCADProject
            // 
            this.checkBoxAutoCADProject.AutoSize = true;
            this.checkBoxAutoCADProject.Checked = true;
            this.checkBoxAutoCADProject.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoCADProject.Enabled = false;
            this.checkBoxAutoCADProject.Location = new System.Drawing.Point(3, 26);
            this.checkBoxAutoCADProject.Name = "checkBoxAutoCADProject";
            this.checkBoxAutoCADProject.Size = new System.Drawing.Size(136, 17);
            this.checkBoxAutoCADProject.TabIndex = 24;
            this.checkBoxAutoCADProject.Text = "Auto-Load CAD Project";
            this.checkBoxAutoCADProject.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.expandoGroupBox1);
            this.flowLayoutPanel1.Controls.Add(this.expandoGroupBox2);
            this.flowLayoutPanel1.Controls.Add(this.expandoGroupBox3);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 48);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(513, 468);
            this.flowLayoutPanel1.TabIndex = 28;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // expandoGroupBox1
            // 
            this.expandoGroupBox1.AutoSize = true;
            this.expandoGroupBox1.CollapseText = "Hide";
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
            this.expandoGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.expandoGroupBox1.Name = "expandoGroupBox1";
            this.expandoGroupBox1.Size = new System.Drawing.Size(495, 184);
            this.expandoGroupBox1.TabIndex = 25;
            this.expandoGroupBox1.TabStop = false;
            this.expandoGroupBox1.Text = "LDAR Database";
            // 
            // buttonTestSqlConnection
            // 
            this.buttonTestSqlConnection.Location = new System.Drawing.Point(331, 142);
            this.buttonTestSqlConnection.Name = "buttonTestSqlConnection";
            this.buttonTestSqlConnection.Size = new System.Drawing.Size(155, 23);
            this.buttonTestSqlConnection.TabIndex = 7;
            this.buttonTestSqlConnection.Text = "Test Connection";
            this.buttonTestSqlConnection.UseVisualStyleBackColor = true;
            this.buttonTestSqlConnection.Click += new System.EventHandler(this.buttonTestSqlConnection_Click);
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
            this.comboBoxAuthentication.SelectedIndexChanged += new System.EventHandler(this.comboBoxAuthentication_SelectedIndexChanged);
            // 
            // expandoGroupBox2
            // 
            this.expandoGroupBox2.AutoSize = true;
            this.expandoGroupBox2.CollapseText = "Hide";
            this.expandoGroupBox2.Controls.Add(this.buttonClearFiles);
            this.expandoGroupBox2.Controls.Add(this.buttonBrowseCADDWF);
            this.expandoGroupBox2.Controls.Add(this.textBoxCADDWF);
            this.expandoGroupBox2.Controls.Add(this.label5);
            this.expandoGroupBox2.ExpandedState = EnvInt.Win32.Controls.ExpandoGroupBox.ExpandGroupBoxState.Expanded;
            this.expandoGroupBox2.ExpandText = "Configure";
            this.expandoGroupBox2.Location = new System.Drawing.Point(3, 193);
            this.expandoGroupBox2.Name = "expandoGroupBox2";
            this.expandoGroupBox2.Size = new System.Drawing.Size(499, 97);
            this.expandoGroupBox2.TabIndex = 26;
            this.expandoGroupBox2.TabStop = false;
            this.expandoGroupBox2.Text = "Drawing Packages";
            // 
            // buttonClearFiles
            // 
            this.buttonClearFiles.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.delete_16;
            this.buttonClearFiles.Location = new System.Drawing.Point(467, 55);
            this.buttonClearFiles.Name = "buttonClearFiles";
            this.buttonClearFiles.Size = new System.Drawing.Size(22, 23);
            this.buttonClearFiles.TabIndex = 14;
            this.buttonClearFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClearFiles.UseVisualStyleBackColor = true;
            this.buttonClearFiles.Click += new System.EventHandler(this.buttonClearFiles_Click);
            // 
            // buttonBrowseCADDWF
            // 
            this.buttonBrowseCADDWF.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.folder_yellow_16;
            this.buttonBrowseCADDWF.Location = new System.Drawing.Point(467, 32);
            this.buttonBrowseCADDWF.Name = "buttonBrowseCADDWF";
            this.buttonBrowseCADDWF.Size = new System.Drawing.Size(22, 23);
            this.buttonBrowseCADDWF.TabIndex = 9;
            this.buttonBrowseCADDWF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBrowseCADDWF.UseVisualStyleBackColor = true;
            this.buttonBrowseCADDWF.Click += new System.EventHandler(this.buttonBrowseCADPackage_Click);
            // 
            // textBoxCADDWF
            // 
            this.textBoxCADDWF.Location = new System.Drawing.Point(80, 34);
            this.textBoxCADDWF.Multiline = true;
            this.textBoxCADDWF.Name = "textBoxCADDWF";
            this.textBoxCADDWF.ReadOnly = true;
            this.textBoxCADDWF.Size = new System.Drawing.Size(381, 41);
            this.textBoxCADDWF.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "File(s):";
            // 
            // expandoGroupBox3
            // 
            this.expandoGroupBox3.CollapseText = "Hide";
            this.expandoGroupBox3.Controls.Add(this.buttonBrowseCADProject);
            this.expandoGroupBox3.Controls.Add(this.textBoxCADProjectFile);
            this.expandoGroupBox3.Controls.Add(this.label1);
            this.expandoGroupBox3.ExpandedState = EnvInt.Win32.Controls.ExpandoGroupBox.ExpandGroupBoxState.Expanded;
            this.expandoGroupBox3.ExpandText = "Configure";
            this.expandoGroupBox3.Location = new System.Drawing.Point(3, 296);
            this.expandoGroupBox3.Name = "expandoGroupBox3";
            this.expandoGroupBox3.Size = new System.Drawing.Size(507, 65);
            this.expandoGroupBox3.TabIndex = 27;
            this.expandoGroupBox3.TabStop = false;
            this.expandoGroupBox3.Text = "AutoCAD Project";
            // 
            // buttonBrowseCADProject
            // 
            this.buttonBrowseCADProject.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.folder_yellow_16;
            this.buttonBrowseCADProject.Location = new System.Drawing.Point(467, 31);
            this.buttonBrowseCADProject.Name = "buttonBrowseCADProject";
            this.buttonBrowseCADProject.Size = new System.Drawing.Size(22, 23);
            this.buttonBrowseCADProject.TabIndex = 9;
            this.buttonBrowseCADProject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonBrowseCADProject.UseVisualStyleBackColor = true;
            this.buttonBrowseCADProject.Click += new System.EventHandler(this.buttonBrowseCADProject_Click);
            // 
            // textBoxCADProjectFile
            // 
            this.textBoxCADProjectFile.Location = new System.Drawing.Point(80, 33);
            this.textBoxCADProjectFile.Name = "textBoxCADProjectFile";
            this.textBoxCADProjectFile.Size = new System.Drawing.Size(381, 20);
            this.textBoxCADProjectFile.TabIndex = 8;
            this.textBoxCADProjectFile.Leave += new System.EventHandler(this.textBoxCADProjectFile_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Project File:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxAutoLDARDatabase);
            this.panel1.Controls.Add(this.checkBoxAutoCADProject);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.labelCADSummary);
            this.panel1.Controls.Add(this.labelDatabaseSummary);
            this.panel1.Location = new System.Drawing.Point(3, 367);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 89);
            this.panel1.TabIndex = 29;
            // 
            // NewProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(527, 528);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.textBoxProjectName);
            this.Controls.Add(this.labelProjectName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New Project";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.expandoGroupBox1.ResumeLayout(false);
            this.expandoGroupBox1.PerformLayout();
            this.expandoGroupBox2.ResumeLayout(false);
            this.expandoGroupBox2.PerformLayout();
            this.expandoGroupBox3.ResumeLayout(false);
            this.expandoGroupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.Button buttonTestSqlConnection;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxAuthentication;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDatabaseSummary;
        private System.Windows.Forms.Button buttonBrowseCADProject;
        private System.Windows.Forms.TextBox textBoxCADProjectFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCADSummary;
        private System.Windows.Forms.CheckBox checkBoxAutoLDARDatabase;
        private System.Windows.Forms.CheckBox checkBoxAutoCADProject;
        private System.Windows.Forms.Button buttonBrowseCADDWF;
        private System.Windows.Forms.TextBox textBoxCADDWF;
        private System.Windows.Forms.Label label5;
        private Win32.Controls.ExpandoGroupBox expandoGroupBox1;
        private Win32.Controls.ExpandoGroupBox expandoGroupBox2;
        private Win32.Controls.ExpandoGroupBox expandoGroupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonClearFiles;
    }
}