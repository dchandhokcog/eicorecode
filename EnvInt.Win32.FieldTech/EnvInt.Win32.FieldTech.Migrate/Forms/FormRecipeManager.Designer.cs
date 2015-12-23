namespace EnvInt.Win32.FieldTech.Migrate.Forms
{
    partial class FormRecipeManager
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveCSV = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoadCSV = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRecipeName = new System.Windows.Forms.TextBox();
            this.comboBoxDatabaseType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTables = new System.Windows.Forms.TabPage();
            this.dataGridViewTables = new System.Windows.Forms.DataGridView();
            this.tabPageSQL = new System.Windows.Forms.TabPage();
            this.dataGridViewViews = new System.Windows.Forms.DataGridView();
            this.tabPageInputFields = new System.Windows.Forms.TabPage();
            this.dataGridViewInputFields = new System.Windows.Forms.DataGridView();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxInputTable = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageExtraFields = new System.Windows.Forms.TabPage();
            this.tabPageDestinationTables = new System.Windows.Forms.TabPage();
            this.dataGridViewExtraFields = new System.Windows.Forms.DataGridView();
            this.dataGridViewDestinationTables = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).BeginInit();
            this.tabPageSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewViews)).BeginInit();
            this.tabPageInputFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputFields)).BeginInit();
            this.tabPageExtraFields.SuspendLayout();
            this.tabPageDestinationTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtraFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDestinationTables)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoad,
            this.toolStripButtonSave,
            this.toolStripButtonNew,
            this.toolStripButtonSaveCSV,
            this.toolStripButtonLoadCSV});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(722, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonLoad
            // 
            this.toolStripButtonLoad.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.document_open;
            this.toolStripButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoad.Name = "toolStripButtonLoad";
            this.toolStripButtonLoad.Size = new System.Drawing.Size(53, 22);
            this.toolStripButtonLoad.Text = "Load";
            this.toolStripButtonLoad.Click += new System.EventHandler(this.toolStripButtonLoad_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.document_save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonSave.Text = "Save";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.document_new;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonNew.Text = "New";
            this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
            // 
            // toolStripButtonSaveCSV
            // 
            this.toolStripButtonSaveCSV.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.list_add;
            this.toolStripButtonSaveCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveCSV.Name = "toolStripButtonSaveCSV";
            this.toolStripButtonSaveCSV.Size = new System.Drawing.Size(149, 22);
            this.toolStripButtonSaveCSV.Text = "Save Definitions to CSV";
            this.toolStripButtonSaveCSV.Click += new System.EventHandler(this.toolStripButtonSaveCSV_Click);
            // 
            // toolStripButtonLoadCSV
            // 
            this.toolStripButtonLoadCSV.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.list_add;
            this.toolStripButtonLoadCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadCSV.Name = "toolStripButtonLoadCSV";
            this.toolStripButtonLoadCSV.Size = new System.Drawing.Size(166, 22);
            this.toolStripButtonLoadCSV.Text = "Load Definitions from CSV";
            this.toolStripButtonLoadCSV.Click += new System.EventHandler(this.toolStripButtonLoadCSV_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Recipe Name";
            // 
            // textBoxRecipeName
            // 
            this.textBoxRecipeName.Location = new System.Drawing.Point(127, 47);
            this.textBoxRecipeName.Name = "textBoxRecipeName";
            this.textBoxRecipeName.Size = new System.Drawing.Size(204, 20);
            this.textBoxRecipeName.TabIndex = 2;
            this.textBoxRecipeName.TextChanged += new System.EventHandler(this.textBoxRecipeName_TextChanged);
            // 
            // comboBoxDatabaseType
            // 
            this.comboBoxDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabaseType.FormattingEnabled = true;
            this.comboBoxDatabaseType.Location = new System.Drawing.Point(465, 47);
            this.comboBoxDatabaseType.Name = "comboBoxDatabaseType";
            this.comboBoxDatabaseType.Size = new System.Drawing.Size(212, 21);
            this.comboBoxDatabaseType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Target DB Type";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(127, 82);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(550, 59);
            this.textBoxDescription.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageTables);
            this.tabControl1.Controls.Add(this.tabPageSQL);
            this.tabControl1.Controls.Add(this.tabPageInputFields);
            this.tabControl1.Controls.Add(this.tabPageExtraFields);
            this.tabControl1.Controls.Add(this.tabPageDestinationTables);
            this.tabControl1.Location = new System.Drawing.Point(12, 225);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(698, 355);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPageTables
            // 
            this.tabPageTables.Controls.Add(this.dataGridViewTables);
            this.tabPageTables.Location = new System.Drawing.Point(4, 22);
            this.tabPageTables.Name = "tabPageTables";
            this.tabPageTables.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTables.Size = new System.Drawing.Size(690, 329);
            this.tabPageTables.TabIndex = 0;
            this.tabPageTables.Text = "Tables";
            this.tabPageTables.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTables
            // 
            this.dataGridViewTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTables.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTables.Name = "dataGridViewTables";
            this.dataGridViewTables.Size = new System.Drawing.Size(684, 323);
            this.dataGridViewTables.TabIndex = 1;
            // 
            // tabPageSQL
            // 
            this.tabPageSQL.Controls.Add(this.dataGridViewViews);
            this.tabPageSQL.Location = new System.Drawing.Point(4, 22);
            this.tabPageSQL.Name = "tabPageSQL";
            this.tabPageSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSQL.Size = new System.Drawing.Size(690, 329);
            this.tabPageSQL.TabIndex = 1;
            this.tabPageSQL.Text = "SQL";
            this.tabPageSQL.UseVisualStyleBackColor = true;
            // 
            // dataGridViewViews
            // 
            this.dataGridViewViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewViews.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewViews.Name = "dataGridViewViews";
            this.dataGridViewViews.Size = new System.Drawing.Size(684, 323);
            this.dataGridViewViews.TabIndex = 0;
            // 
            // tabPageInputFields
            // 
            this.tabPageInputFields.Controls.Add(this.dataGridViewInputFields);
            this.tabPageInputFields.Location = new System.Drawing.Point(4, 22);
            this.tabPageInputFields.Name = "tabPageInputFields";
            this.tabPageInputFields.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInputFields.Size = new System.Drawing.Size(690, 329);
            this.tabPageInputFields.TabIndex = 3;
            this.tabPageInputFields.Text = "Input Fields";
            this.tabPageInputFields.UseVisualStyleBackColor = true;
            // 
            // dataGridViewInputFields
            // 
            this.dataGridViewInputFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInputFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInputFields.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewInputFields.Name = "dataGridViewInputFields";
            this.dataGridViewInputFields.Size = new System.Drawing.Size(684, 323);
            this.dataGridViewInputFields.TabIndex = 0;
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(127, 157);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(204, 20);
            this.textBoxVersion.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Version";
            // 
            // textBoxInputTable
            // 
            this.textBoxInputTable.Location = new System.Drawing.Point(473, 157);
            this.textBoxInputTable.Name = "textBoxInputTable";
            this.textBoxInputTable.Size = new System.Drawing.Size(204, 20);
            this.textBoxInputTable.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(382, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Input Table";
            // 
            // tabPageExtraFields
            // 
            this.tabPageExtraFields.Controls.Add(this.dataGridViewExtraFields);
            this.tabPageExtraFields.Location = new System.Drawing.Point(4, 22);
            this.tabPageExtraFields.Name = "tabPageExtraFields";
            this.tabPageExtraFields.Size = new System.Drawing.Size(690, 329);
            this.tabPageExtraFields.TabIndex = 4;
            this.tabPageExtraFields.Text = "Extra Fields";
            this.tabPageExtraFields.UseVisualStyleBackColor = true;
            // 
            // tabPageDestinationTables
            // 
            this.tabPageDestinationTables.Controls.Add(this.dataGridViewDestinationTables);
            this.tabPageDestinationTables.Location = new System.Drawing.Point(4, 22);
            this.tabPageDestinationTables.Name = "tabPageDestinationTables";
            this.tabPageDestinationTables.Size = new System.Drawing.Size(690, 329);
            this.tabPageDestinationTables.TabIndex = 5;
            this.tabPageDestinationTables.Text = "Destination Tables";
            this.tabPageDestinationTables.UseVisualStyleBackColor = true;
            // 
            // dataGridViewExtraFields
            // 
            this.dataGridViewExtraFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExtraFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewExtraFields.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewExtraFields.Name = "dataGridViewExtraFields";
            this.dataGridViewExtraFields.Size = new System.Drawing.Size(690, 329);
            this.dataGridViewExtraFields.TabIndex = 1;
            // 
            // dataGridViewDestinationTables
            // 
            this.dataGridViewDestinationTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDestinationTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDestinationTables.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewDestinationTables.Name = "dataGridViewDestinationTables";
            this.dataGridViewDestinationTables.Size = new System.Drawing.Size(690, 329);
            this.dataGridViewDestinationTables.TabIndex = 1;
            // 
            // FormRecipeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 592);
            this.Controls.Add(this.textBoxInputTable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDatabaseType);
            this.Controls.Add(this.textBoxRecipeName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormRecipeManager";
            this.Text = "Import Recipe Manager";
            this.Load += new System.EventHandler(this.FormRecipeManager_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).EndInit();
            this.tabPageSQL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewViews)).EndInit();
            this.tabPageInputFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInputFields)).EndInit();
            this.tabPageExtraFields.ResumeLayout(false);
            this.tabPageDestinationTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtraFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDestinationTables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadCSV;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoad;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRecipeName;
        private System.Windows.Forms.ComboBox comboBoxDatabaseType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSQL;
        private System.Windows.Forms.DataGridView dataGridViewViews;
        private System.Windows.Forms.TabPage tabPageTables;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveCSV;
        private System.Windows.Forms.DataGridView dataGridViewTables;
        private System.Windows.Forms.TabPage tabPageInputFields;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxInputTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewInputFields;
        private System.Windows.Forms.TabPage tabPageExtraFields;
        private System.Windows.Forms.DataGridView dataGridViewExtraFields;
        private System.Windows.Forms.TabPage tabPageDestinationTables;
        private System.Windows.Forms.DataGridView dataGridViewDestinationTables;
    }
}