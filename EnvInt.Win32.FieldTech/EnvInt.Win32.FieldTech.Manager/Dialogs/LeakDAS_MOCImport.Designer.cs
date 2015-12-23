namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    partial class LeakDAS_MOCImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeakDAS_MOCImport));
            this.radWizard1 = new Telerik.WinControls.UI.RadWizard();
            this.wizardCompletionPage1 = new Telerik.WinControls.UI.WizardCompletionPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.labelImportResults = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxWelcome = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.comboBoxPlant = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.progressBarImport = new System.Windows.Forms.ProgressBar();
            this.labelImportStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonTestSqlConnection = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxAuthentication = new System.Windows.Forms.ComboBox();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBoxSkip = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxComplianceGroup = new System.Windows.Forms.ComboBox();
            this.checkedListBoxRegs = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.wizardWelcomePage1 = new Telerik.WinControls.UI.WizardWelcomePage();
            this.wizardPage1 = new Telerik.WinControls.UI.WizardPage();
            this.wizardPage4 = new Telerik.WinControls.UI.WizardPage();
            this.wizardPage2 = new Telerik.WinControls.UI.WizardPage();
            this.wizardPage5 = new Telerik.WinControls.UI.WizardPage();
            this.wizardPage10 = new Telerik.WinControls.UI.WizardPage();
            ((System.ComponentModel.ISupportInitialize)(this.radWizard1)).BeginInit();
            this.radWizard1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // radWizard1
            // 
            this.radWizard1.CompletionPage = this.wizardCompletionPage1;
            this.radWizard1.Controls.Add(this.panel3);
            this.radWizard1.Controls.Add(this.panel1);
            this.radWizard1.Controls.Add(this.panel6);
            this.radWizard1.Controls.Add(this.panel7);
            this.radWizard1.Controls.Add(this.panel12);
            this.radWizard1.Controls.Add(this.panel2);
            this.radWizard1.Controls.Add(this.panel4);
            this.radWizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radWizard1.HideCompletionImage = true;
            this.radWizard1.Location = new System.Drawing.Point(0, 0);
            this.radWizard1.Name = "radWizard1";
            this.radWizard1.PageHeaderIcon = ((System.Drawing.Image)(resources.GetObject("radWizard1.PageHeaderIcon")));
            this.radWizard1.Pages.Add(this.wizardWelcomePage1);
            this.radWizard1.Pages.Add(this.wizardPage1);
            this.radWizard1.Pages.Add(this.wizardPage4);
            this.radWizard1.Pages.Add(this.wizardPage2);
            this.radWizard1.Pages.Add(this.wizardPage5);
            this.radWizard1.Pages.Add(this.wizardPage10);
            this.radWizard1.Pages.Add(this.wizardCompletionPage1);
            this.radWizard1.Size = new System.Drawing.Size(596, 398);
            this.radWizard1.TabIndex = 2;
            this.radWizard1.WelcomeImage = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.EI_Logo_Sitefinity;
            this.radWizard1.WelcomePage = this.wizardWelcomePage1;
            this.radWizard1.Finish += new System.EventHandler(this.radWizard1_Finish);
            this.radWizard1.Cancel += new System.EventHandler(this.radWizard1_Cancel);
            this.radWizard1.Help += new System.EventHandler(this.radWizard1_Help);
            this.radWizard1.SelectedPageChanged += new Telerik.WinControls.UI.SelectedPageChangedEventHandler(this.radWizard1_SelectedPageChanged);
            // 
            // wizardCompletionPage1
            // 
            this.wizardCompletionPage1.ContentArea = this.panel3;
            this.wizardCompletionPage1.Header = "";
            this.wizardCompletionPage1.Name = "wizardCompletionPage1";
            this.wizardCompletionPage1.Title = "Import Complete";
            this.wizardCompletionPage1.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.textBoxResults);
            this.panel3.Controls.Add(this.labelImportResults);
            this.panel3.Location = new System.Drawing.Point(0, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(596, 294);
            this.panel3.TabIndex = 2;
            // 
            // textBoxResults
            // 
            this.textBoxResults.Location = new System.Drawing.Point(12, 46);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResults.Size = new System.Drawing.Size(572, 232);
            this.textBoxResults.TabIndex = 1;
            this.textBoxResults.WordWrap = false;
            // 
            // labelImportResults
            // 
            this.labelImportResults.AutoSize = true;
            this.labelImportResults.Location = new System.Drawing.Point(25, 18);
            this.labelImportResults.Name = "labelImportResults";
            this.labelImportResults.Size = new System.Drawing.Size(66, 13);
            this.labelImportResults.TabIndex = 0;
            this.labelImportResults.Text = "Import Log...";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBoxWelcome);
            this.panel1.Location = new System.Drawing.Point(62, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 294);
            this.panel1.TabIndex = 0;
            // 
            // textBoxWelcome
            // 
            this.textBoxWelcome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxWelcome.Location = new System.Drawing.Point(35, 33);
            this.textBoxWelcome.Multiline = true;
            this.textBoxWelcome.Name = "textBoxWelcome";
            this.textBoxWelcome.Size = new System.Drawing.Size(426, 158);
            this.textBoxWelcome.TabIndex = 0;
            this.textBoxWelcome.Text = resources.GetString("textBoxWelcome.Text");
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.comboBoxPlant);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Location = new System.Drawing.Point(0, 81);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(596, 269);
            this.panel6.TabIndex = 5;
            // 
            // comboBoxPlant
            // 
            this.comboBoxPlant.DisplayMember = "PlantDescription";
            this.comboBoxPlant.FormattingEnabled = true;
            this.comboBoxPlant.Location = new System.Drawing.Point(115, 114);
            this.comboBoxPlant.Name = "comboBoxPlant";
            this.comboBoxPlant.Size = new System.Drawing.Size(425, 21);
            this.comboBoxPlant.TabIndex = 3;
            this.comboBoxPlant.ValueMember = "PlantID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Plant:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(32, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(533, 33);
            this.label9.TabIndex = 0;
            this.label9.Text = "LeakDAS requires some fields to be specified about the import components that are" +
    " not collected in the Field. You must specify them here.";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.label14);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Location = new System.Drawing.Point(0, 64);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(596, 286);
            this.panel7.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 155);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(152, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Click \'Next\' to begin the import.";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(25, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(543, 48);
            this.label13.TabIndex = 0;
            this.label13.Text = "All data is loaded and ready to import into the LeakDAS quarantine. After complet" +
    "ion a log of all actions will be viewable and may also be saved for reference. ";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.White;
            this.panel12.Controls.Add(this.progressBarImport);
            this.panel12.Controls.Add(this.labelImportStatus);
            this.panel12.Location = new System.Drawing.Point(0, 64);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(596, 286);
            this.panel12.TabIndex = 11;
            // 
            // progressBarImport
            // 
            this.progressBarImport.Location = new System.Drawing.Point(15, 145);
            this.progressBarImport.Name = "progressBarImport";
            this.progressBarImport.Size = new System.Drawing.Size(569, 23);
            this.progressBarImport.Step = 1;
            this.progressBarImport.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarImport.TabIndex = 2;
            // 
            // labelImportStatus
            // 
            this.labelImportStatus.Location = new System.Drawing.Point(12, 107);
            this.labelImportStatus.Name = "labelImportStatus";
            this.labelImportStatus.Size = new System.Drawing.Size(572, 23);
            this.labelImportStatus.TabIndex = 1;
            this.labelImportStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.buttonTestSqlConnection);
            this.panel2.Controls.Add(this.textBoxPassword);
            this.panel2.Controls.Add(this.textBoxUsername);
            this.panel2.Controls.Add(this.labelPassword);
            this.panel2.Controls.Add(this.labelUsername);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboBoxAuthentication);
            this.panel2.Controls.Add(this.textBoxDatabase);
            this.panel2.Controls.Add(this.textBoxServer);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(596, 286);
            this.panel2.TabIndex = 12;
            // 
            // buttonTestSqlConnection
            // 
            this.buttonTestSqlConnection.Location = new System.Drawing.Point(453, 224);
            this.buttonTestSqlConnection.Name = "buttonTestSqlConnection";
            this.buttonTestSqlConnection.Size = new System.Drawing.Size(114, 23);
            this.buttonTestSqlConnection.TabIndex = 23;
            this.buttonTestSqlConnection.Text = "Test Connection";
            this.buttonTestSqlConnection.UseVisualStyleBackColor = true;
            this.buttonTestSqlConnection.Click += new System.EventHandler(this.buttonTestSqlConnection_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(183, 227);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(152, 20);
            this.textBoxPassword.TabIndex = 22;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(183, 195);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(152, 20);
            this.textBoxUsername.TabIndex = 21;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(108, 230);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 20;
            this.labelPassword.Text = "Password:";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(108, 198);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(58, 13);
            this.labelUsername.TabIndex = 19;
            this.labelUsername.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Authentication:";
            // 
            // comboBoxAuthentication
            // 
            this.comboBoxAuthentication.FormattingEnabled = true;
            this.comboBoxAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Authentication"});
            this.comboBoxAuthentication.Location = new System.Drawing.Point(111, 156);
            this.comboBoxAuthentication.Name = "comboBoxAuthentication";
            this.comboBoxAuthentication.Size = new System.Drawing.Size(224, 21);
            this.comboBoxAuthentication.TabIndex = 17;
            this.comboBoxAuthentication.SelectedIndexChanged += new System.EventHandler(this.comboBoxAuthentication_SelectedIndexChanged);
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(111, 116);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(456, 20);
            this.textBoxDatabase.TabIndex = 16;
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(111, 77);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(456, 20);
            this.textBoxServer.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Database:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Server:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(367, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Enter the database connectivity information for LeakDAS below";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.checkBoxSkip);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.comboBoxComplianceGroup);
            this.panel4.Controls.Add(this.checkedListBoxRegs);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Location = new System.Drawing.Point(0, 81);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(596, 269);
            this.panel4.TabIndex = 13;
            // 
            // checkBoxSkip
            // 
            this.checkBoxSkip.AutoSize = true;
            this.checkBoxSkip.Location = new System.Drawing.Point(234, 127);
            this.checkBoxSkip.Name = "checkBoxSkip";
            this.checkBoxSkip.Size = new System.Drawing.Size(89, 17);
            this.checkBoxSkip.TabIndex = 4;
            this.checkBoxSkip.Text = "Skip this step";
            this.checkBoxSkip.UseVisualStyleBackColor = true;
            this.checkBoxSkip.CheckedChanged += new System.EventHandler(this.checkBoxSkip_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Compliance Group";
            // 
            // comboBoxComplianceGroup
            // 
            this.comboBoxComplianceGroup.DisplayMember = "ComplianceGroupDescription";
            this.comboBoxComplianceGroup.FormattingEnabled = true;
            this.comboBoxComplianceGroup.Location = new System.Drawing.Point(234, 76);
            this.comboBoxComplianceGroup.Name = "comboBoxComplianceGroup";
            this.comboBoxComplianceGroup.Size = new System.Drawing.Size(316, 21);
            this.comboBoxComplianceGroup.TabIndex = 2;
            // 
            // checkedListBoxRegs
            // 
            this.checkedListBoxRegs.CheckOnClick = true;
            this.checkedListBoxRegs.FormattingEnabled = true;
            this.checkedListBoxRegs.Location = new System.Drawing.Point(28, 60);
            this.checkedListBoxRegs.Name = "checkedListBoxRegs";
            this.checkedListBoxRegs.Size = new System.Drawing.Size(175, 184);
            this.checkedListBoxRegs.TabIndex = 1;
            this.checkedListBoxRegs.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxRegs_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(25, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(549, 43);
            this.label5.TabIndex = 0;
            this.label5.Text = "(Optional) Select the regulations and compliance group for new records.  If this " +
    "step is omitted, rules and compliance groups may be assigned in LeakDAS prior to" +
    " processing.";
            // 
            // wizardWelcomePage1
            // 
            this.wizardWelcomePage1.AccessibleDescription = "this is a test";
            this.wizardWelcomePage1.AccessibleName = "this is a test";
            this.wizardWelcomePage1.ContentArea = this.panel1;
            this.wizardWelcomePage1.Header = "";
            this.wizardWelcomePage1.Name = "wizardWelcomePage1";
            this.wizardWelcomePage1.ShowHorizontalLine = false;
            this.wizardWelcomePage1.Text = "this is a test";
            this.wizardWelcomePage1.Title = "LeakDAS Component Import Tool";
            this.wizardWelcomePage1.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // wizardPage1
            // 
            this.wizardPage1.ContentArea = this.panel2;
            this.wizardPage1.Header = "";
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Title = "LeakDAS Connection";
            this.wizardPage1.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // wizardPage4
            // 
            this.wizardPage4.ContentArea = this.panel6;
            this.wizardPage4.Header = "Specifies which are of the facility imported components will be assigned to.";
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.Title = "Facility Selection";
            this.wizardPage4.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // wizardPage2
            // 
            this.wizardPage2.ContentArea = this.panel4;
            this.wizardPage2.Header = "Select regulations and compliance group to assign to new components.";
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Title = "Select Regulations";
            this.wizardPage2.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // wizardPage5
            // 
            this.wizardPage5.ContentArea = this.panel7;
            this.wizardPage5.Header = "";
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.Title = "Start Import";
            this.wizardPage5.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // wizardPage10
            // 
            this.wizardPage10.ContentArea = this.panel12;
            this.wizardPage10.Header = "";
            this.wizardPage10.Name = "wizardPage10";
            this.wizardPage10.Title = "Data is importing...";
            this.wizardPage10.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // LeakDAS_MOCImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 398);
            this.Controls.Add(this.radWizard1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LeakDAS_MOCImport";
            this.Text = "Ei MOC - LeakDAS";
            ((System.ComponentModel.ISupportInitialize)(this.radWizard1)).EndInit();
            this.radWizard1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadWizard radWizard1;
        private Telerik.WinControls.UI.WizardCompletionPage wizardCompletionPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxWelcome;
        private Telerik.WinControls.UI.WizardWelcomePage wizardWelcomePage1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private Telerik.WinControls.UI.WizardPage wizardPage4;
        private Telerik.WinControls.UI.WizardPage wizardPage5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxResults;
        private System.Windows.Forms.Label labelImportResults;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label labelImportStatus;
        private Telerik.WinControls.UI.WizardPage wizardPage10;
        private System.Windows.Forms.ProgressBar progressBarImport;
        private System.Windows.Forms.ComboBox comboBoxPlant;
        private System.Windows.Forms.Panel panel2;
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
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.WizardPage wizardPage1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBoxSkip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxComplianceGroup;
        private System.Windows.Forms.CheckedListBox checkedListBoxRegs;
        private System.Windows.Forms.Label label5;
        private Telerik.WinControls.UI.WizardPage wizardPage2;


    }
}

