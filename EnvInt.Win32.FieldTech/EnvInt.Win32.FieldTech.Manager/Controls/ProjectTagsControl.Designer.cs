namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    partial class ProjectTagsControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectTagsControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditTags = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReloadDelimited = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRemoveDups = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddToCollected = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonQuickView = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.radPageViewTags = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPageSets = new Telerik.WinControls.UI.RadPageViewPage();
            this.objectListViewPendingTagData = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStripObjectControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radPageViewPageAllTags = new Telerik.WinControls.UI.RadPageViewPage();
            this.radGridViewCollectedTags = new Telerik.WinControls.UI.RadGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportEid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMassEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFindReplace = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewTags)).BeginInit();
            this.radPageViewTags.SuspendLayout();
            this.radPageViewPageSets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPendingTagData)).BeginInit();
            this.contextMenuStripObjectControl.SuspendLayout();
            this.radPageViewPageAllTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCollectedTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCollectedTags.MasterTemplate)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonQuickView,
            this.toolStripButtonExportAll,
            this.toolStripButtonEditTags,
            this.toolStripButtonImportExcel,
            this.toolStripButtonReloadDelimited,
            this.toolStripSeparator2,
            this.toolStripButtonDelete,
            this.toolStripButtonImportAll,
            this.toolStripSeparator3,
            this.toolStripButtonRemoveDups,
            this.toolStripButtonAddToCollected});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExportAll
            // 
            this.toolStripButtonExportAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportAll.Image")));
            this.toolStripButtonExportAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportAll.Name = "toolStripButtonExportAll";
            this.toolStripButtonExportAll.Size = new System.Drawing.Size(77, 22);
            this.toolStripButtonExportAll.Text = "Export All";
            this.toolStripButtonExportAll.Click += new System.EventHandler(this.toolStripButtonExportAll_Click);
            // 
            // toolStripButtonEditTags
            // 
            this.toolStripButtonEditTags.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditTags.Image")));
            this.toolStripButtonEditTags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditTags.Name = "toolStripButtonEditTags";
            this.toolStripButtonEditTags.Size = new System.Drawing.Size(98, 22);
            this.toolStripButtonEditTags.Text = "Open in Excel";
            this.toolStripButtonEditTags.Click += new System.EventHandler(this.toolStripButtonEditTags_Click);
            // 
            // toolStripButtonImportExcel
            // 
            this.toolStripButtonImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportExcel.Image")));
            this.toolStripButtonImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportExcel.Name = "toolStripButtonImportExcel";
            this.toolStripButtonImportExcel.Size = new System.Drawing.Size(121, 22);
            this.toolStripButtonImportExcel.Text = "Reload from Excel";
            this.toolStripButtonImportExcel.Click += new System.EventHandler(this.toolStripButtonImportExcel_Click);
            // 
            // toolStripButtonReloadDelimited
            // 
            this.toolStripButtonReloadDelimited.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReloadDelimited.Image")));
            this.toolStripButtonReloadDelimited.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReloadDelimited.Name = "toolStripButtonReloadDelimited";
            this.toolStripButtonReloadDelimited.Size = new System.Drawing.Size(146, 22);
            this.toolStripButtonReloadDelimited.Text = "Reload from Delimited";
            this.toolStripButtonReloadDelimited.Click += new System.EventHandler(this.toolStripButtonReloadDelimited_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(107, 22);
            this.toolStripButtonDelete.Text = "Delete Selected";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonImportAll
            // 
            this.toolStripButtonImportAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportAll.Image")));
            this.toolStripButtonImportAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportAll.Name = "toolStripButtonImportAll";
            this.toolStripButtonImportAll.Size = new System.Drawing.Size(61, 22);
            this.toolStripButtonImportAll.Text = "Merge";
            this.toolStripButtonImportAll.Click += new System.EventHandler(this.toolStripButtonImportAll_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRemoveDups
            // 
            this.toolStripButtonRemoveDups.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.delete_16;
            this.toolStripButtonRemoveDups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveDups.Name = "toolStripButtonRemoveDups";
            this.toolStripButtonRemoveDups.Size = new System.Drawing.Size(100, 20);
            this.toolStripButtonRemoveDups.Text = "Remove Dups";
            this.toolStripButtonRemoveDups.Click += new System.EventHandler(this.toolStripButtonRemoveDups_Click);
            // 
            // toolStripButtonAddToCollected
            // 
            this.toolStripButtonAddToCollected.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddToCollected.Image")));
            this.toolStripButtonAddToCollected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddToCollected.Name = "toolStripButtonAddToCollected";
            this.toolStripButtonAddToCollected.Size = new System.Drawing.Size(102, 20);
            this.toolStripButtonAddToCollected.Text = "Add to Staged";
            this.toolStripButtonAddToCollected.Click += new System.EventHandler(this.toolStripButtonAddToCollected_Click);
            // 
            // toolStripButtonQuickView
            // 
            this.toolStripButtonQuickView.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonQuickView.Image")));
            this.toolStripButtonQuickView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQuickView.Name = "toolStripButtonQuickView";
            this.toolStripButtonQuickView.Size = new System.Drawing.Size(86, 22);
            this.toolStripButtonQuickView.Text = "Quick View";
            this.toolStripButtonQuickView.Click += new System.EventHandler(this.toolStripButtonQuickView_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.radPageViewTags);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(813, 439);
            this.panelMain.TabIndex = 2;
            // 
            // radPageViewTags
            // 
            this.radPageViewTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radPageViewTags.Controls.Add(this.radPageViewPageSets);
            this.radPageViewTags.Controls.Add(this.radPageViewPageAllTags);
            this.radPageViewTags.Location = new System.Drawing.Point(0, 28);
            this.radPageViewTags.Name = "radPageViewTags";
            this.radPageViewTags.SelectedPage = this.radPageViewPageSets;
            this.radPageViewTags.Size = new System.Drawing.Size(813, 411);
            this.radPageViewTags.TabIndex = 2;
            this.radPageViewTags.Text = "Collected Tags";
            this.radPageViewTags.SelectedPageChanged += new System.EventHandler(this.radPageViewTags_SelectedPageChanged);
            // 
            // radPageViewPageSets
            // 
// TODO: Code generation for '' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.radPageViewPageSets.Controls.Add(this.objectListViewPendingTagData);
            this.radPageViewPageSets.Description = null;
            this.radPageViewPageSets.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPageSets.Name = "radPageViewPageSets";
            this.radPageViewPageSets.Size = new System.Drawing.Size(792, 363);
            this.radPageViewPageSets.Text = "Incoming Sets";
            this.radPageViewPageSets.Title = "Incoming Sets";
            // 
            // objectListViewPendingTagData
            // 
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn1);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn2);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn3);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn4);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn5);
            this.objectListViewPendingTagData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5});
            this.objectListViewPendingTagData.ContextMenuStrip = this.contextMenuStripObjectControl;
            this.objectListViewPendingTagData.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewPendingTagData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewPendingTagData.FullRowSelect = true;
            this.objectListViewPendingTagData.Location = new System.Drawing.Point(0, 0);
            this.objectListViewPendingTagData.Name = "objectListViewPendingTagData";
            this.objectListViewPendingTagData.Size = new System.Drawing.Size(792, 363);
            this.objectListViewPendingTagData.TabIndex = 1;
            this.objectListViewPendingTagData.UseCompatibleStateImageBehavior = false;
            this.objectListViewPendingTagData.UseHyperlinks = true;
            this.objectListViewPendingTagData.View = System.Windows.Forms.View.Details;
            this.objectListViewPendingTagData.ItemsChanged += new System.EventHandler<BrightIdeasSoftware.ItemsChangedEventArgs>(this.objectListViewPendingTagData_ItemsChanged);
            this.objectListViewPendingTagData.DoubleClick += new System.EventHandler(this.objectListViewPendingTagData_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Device";
            this.olvColumn1.Text = "Device";
            this.olvColumn1.Width = 170;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "CreateDate";
            this.olvColumn2.Text = "TagDates";
            this.olvColumn2.Width = 208;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "TagCount";
            this.olvColumn3.Text = "Items";
            this.olvColumn3.Width = 66;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "FileName";
            this.olvColumn4.FillsFreeSpace = true;
            this.olvColumn4.ShowTextInHeader = false;
            this.olvColumn4.Text = "Filename";
            this.olvColumn4.Width = 123;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "SentToCollected";
            this.olvColumn5.Text = "Staged";
            this.olvColumn5.Width = 133;
            // 
            // contextMenuStripObjectControl
            // 
            this.contextMenuStripObjectControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem});
            this.contextMenuStripObjectControl.Name = "contextMenuStripObjectControl";
            this.contextMenuStripObjectControl.Size = new System.Drawing.Size(118, 26);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // radPageViewPageAllTags
            // 
// TODO: Code generation for '' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.radPageViewPageAllTags.Controls.Add(this.radGridViewCollectedTags);
            this.radPageViewPageAllTags.Description = null;
            this.radPageViewPageAllTags.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPageAllTags.Name = "radPageViewPageAllTags";
            this.radPageViewPageAllTags.Size = new System.Drawing.Size(792, 363);
            this.radPageViewPageAllTags.Text = "Staged Tags";
            this.radPageViewPageAllTags.Title = "Staged Tags";
            // 
            // radGridViewCollectedTags
            // 
            this.radGridViewCollectedTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridViewCollectedTags.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.radGridViewCollectedTags.MasterTemplate.MultiSelect = true;
            this.radGridViewCollectedTags.Name = "radGridViewCollectedTags";
            this.radGridViewCollectedTags.Size = new System.Drawing.Size(792, 363);
            this.radGridViewCollectedTags.TabIndex = 0;
            this.radGridViewCollectedTags.Text = "Collected Tags";
            this.radGridViewCollectedTags.RowsChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.radGridViewCollectedTags_RowsChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExportEid,
            this.toolStripButtonMassEdit,
            this.toolStripButtonFindReplace});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(813, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonExportEid
            // 
            this.toolStripButtonExportEid.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.cube_32;
            this.toolStripButtonExportEid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportEid.Name = "toolStripButtonExportEid";
            this.toolStripButtonExportEid.Size = new System.Drawing.Size(94, 22);
            this.toolStripButtonExportEid.Text = "Export to EID";
            this.toolStripButtonExportEid.Click += new System.EventHandler(this.toolStripButtonExportEid_Click);
            // 
            // toolStripButtonMassEdit
            // 
            this.toolStripButtonMassEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMassEdit.Image")));
            this.toolStripButtonMassEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMassEdit.Name = "toolStripButtonMassEdit";
            this.toolStripButtonMassEdit.Size = new System.Drawing.Size(128, 22);
            this.toolStripButtonMassEdit.Text = "Mass Edit Selection";
            this.toolStripButtonMassEdit.Click += new System.EventHandler(this.toolStripButtonMassEdit_Click);
            // 
            // toolStripButtonFindReplace
            // 
            this.toolStripButtonFindReplace.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFindReplace.Image")));
            this.toolStripButtonFindReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFindReplace.Name = "toolStripButtonFindReplace";
            this.toolStripButtonFindReplace.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonFindReplace.Text = "Find/Replace";
            this.toolStripButtonFindReplace.Click += new System.EventHandler(this.toolStripButtonFindReplace_Click);
            // 
            // ProjectTagsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ProjectTagsControl";
            this.Size = new System.Drawing.Size(813, 464);
            this.SizeChanged += new System.EventHandler(this.ProjectTagsControl_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.ProjectTagsControl_VisibleChanged);
            this.Enter += new System.EventHandler(this.ProjectTagsControl_Enter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewTags)).EndInit();
            this.radPageViewTags.ResumeLayout(false);
            this.radPageViewPageSets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPendingTagData)).EndInit();
            this.contextMenuStripObjectControl.ResumeLayout(false);
            this.radPageViewPageAllTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCollectedTags.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCollectedTags)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panelMain;
        private BrightIdeasSoftware.ObjectListView objectListViewPendingTagData;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditTags;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportExcel;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveDups;
        private System.Windows.Forms.ToolStripButton toolStripButtonReloadDelimited;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripObjectControl;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private Telerik.WinControls.UI.RadPageView radPageViewTags;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPageSets;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPageAllTags;
        private Telerik.WinControls.UI.RadGridView radGridViewCollectedTags;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddToCollected;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportEid;
        private System.Windows.Forms.ToolStripButton toolStripButtonMassEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonFindReplace;
        private System.Windows.Forms.ToolStripButton toolStripButtonQuickView;

    }
}
