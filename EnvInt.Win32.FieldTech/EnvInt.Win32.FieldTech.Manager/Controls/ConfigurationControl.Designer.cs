namespace EnvInt.Win32.FieldTech.Manager
{
    partial class ConfigurationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.panelObjectView = new System.Windows.Forms.Panel();
            this.objectListViewConfigurations = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStripButtonSetDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panelObjectView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewConfigurations)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonDelete,
            this.toolStripButtonSave,
            this.toolStripButtonLoad,
            this.toolStripButtonSetDefault});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(908, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNew.Image")));
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.Text = "Create new configuration";
            this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "Delete selected configurations";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonSave.Text = "Save";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonLoad
            // 
            this.toolStripButtonLoad.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.import_from_folder_32;
            this.toolStripButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoad.Name = "toolStripButtonLoad";
            this.toolStripButtonLoad.Size = new System.Drawing.Size(53, 22);
            this.toolStripButtonLoad.Text = "Load";
            this.toolStripButtonLoad.Click += new System.EventHandler(this.toolStripButtonLoad_Click);
            // 
            // panelObjectView
            // 
            this.panelObjectView.Controls.Add(this.objectListViewConfigurations);
            this.panelObjectView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelObjectView.Location = new System.Drawing.Point(0, 25);
            this.panelObjectView.Name = "panelObjectView";
            this.panelObjectView.Size = new System.Drawing.Size(908, 414);
            this.panelObjectView.TabIndex = 3;
            // 
            // objectListViewConfigurations
            // 
            this.objectListViewConfigurations.AllColumns.Add(this.olvColumn1);
            this.objectListViewConfigurations.AllColumns.Add(this.olvColumn2);
            this.objectListViewConfigurations.AllColumns.Add(this.olvColumn3);
            this.objectListViewConfigurations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.objectListViewConfigurations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewConfigurations.FullRowSelect = true;
            this.objectListViewConfigurations.Location = new System.Drawing.Point(0, 0);
            this.objectListViewConfigurations.Name = "objectListViewConfigurations";
            this.objectListViewConfigurations.ShowGroups = false;
            this.objectListViewConfigurations.Size = new System.Drawing.Size(908, 414);
            this.objectListViewConfigurations.TabIndex = 0;
            this.objectListViewConfigurations.UseCompatibleStateImageBehavior = false;
            this.objectListViewConfigurations.View = System.Windows.Forms.View.Details;
            this.objectListViewConfigurations.ItemActivate += new System.EventHandler(this.objectListViewConfigurations_ItemActivate);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.Text = "Configuration Name";
            this.olvColumn1.Width = 298;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Notes";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Notes";
            this.olvColumn2.Width = 563;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Default";
            this.olvColumn3.CheckBoxes = true;
            this.olvColumn3.Hyperlink = true;
            this.olvColumn3.Sortable = false;
            this.olvColumn3.Text = "Default";
            // 
            // toolStripButtonSetDefault
            // 
            this.toolStripButtonSetDefault.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSetDefault.Image")));
            this.toolStripButtonSetDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSetDefault.Name = "toolStripButtonSetDefault";
            this.toolStripButtonSetDefault.Size = new System.Drawing.Size(84, 22);
            this.toolStripButtonSetDefault.Text = "Set Default";
            this.toolStripButtonSetDefault.Click += new System.EventHandler(this.toolStripButtonSetDefault_Click);
            // 
            // ConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelObjectView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ConfigurationControl";
            this.Size = new System.Drawing.Size(908, 439);
            this.Load += new System.EventHandler(this.ConfigurationControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelObjectView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewConfigurations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.Panel panelObjectView;
        private BrightIdeasSoftware.ObjectListView objectListViewConfigurations;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoad;
        private System.Windows.Forms.ToolStripButton toolStripButtonSetDefault;
    }
}
