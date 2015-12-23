namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    partial class MarkedDrawingsControl
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
            this.objectListViewDrawingPackages = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnFileName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPageCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLastRefreshed = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnValidationError = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSaveDrawingsToFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewDrawingPackages)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectListViewDrawingPackages
            // 
            this.objectListViewDrawingPackages.AllColumns.Add(this.olvColumnFileName);
            this.objectListViewDrawingPackages.AllColumns.Add(this.olvColumnPageCount);
            this.objectListViewDrawingPackages.AllColumns.Add(this.olvColumnLastRefreshed);
            this.objectListViewDrawingPackages.AllColumns.Add(this.olvColumnValidationError);
            this.objectListViewDrawingPackages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnFileName,
            this.olvColumnPageCount,
            this.olvColumnLastRefreshed,
            this.olvColumnValidationError});
            this.objectListViewDrawingPackages.ContextMenuStrip = this.contextMenuStrip1;
            this.objectListViewDrawingPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewDrawingPackages.FullRowSelect = true;
            this.objectListViewDrawingPackages.Location = new System.Drawing.Point(0, 25);
            this.objectListViewDrawingPackages.Name = "objectListViewDrawingPackages";
            this.objectListViewDrawingPackages.ShowGroups = false;
            this.objectListViewDrawingPackages.Size = new System.Drawing.Size(978, 461);
            this.objectListViewDrawingPackages.TabIndex = 2;
            this.objectListViewDrawingPackages.UseCompatibleStateImageBehavior = false;
            this.objectListViewDrawingPackages.View = System.Windows.Forms.View.Details;
            this.objectListViewDrawingPackages.ItemActivate += new System.EventHandler(this.objectListViewDrawingPackages_ItemActivate);
            // 
            // olvColumnFileName
            // 
            this.olvColumnFileName.AspectName = "FileName";
            this.olvColumnFileName.Text = "File";
            this.olvColumnFileName.Width = 349;
            // 
            // olvColumnPageCount
            // 
            this.olvColumnPageCount.AspectName = "PageCount";
            this.olvColumnPageCount.Text = "Pages";
            this.olvColumnPageCount.Width = 45;
            // 
            // olvColumnLastRefreshed
            // 
            this.olvColumnLastRefreshed.AspectName = "LastRefreshed";
            this.olvColumnLastRefreshed.Text = "LastRefreshed";
            this.olvColumnLastRefreshed.Width = 121;
            // 
            // olvColumnValidationError
            // 
            this.olvColumnValidationError.AspectName = "ValidationError";
            this.olvColumnValidationError.FillsFreeSpace = true;
            this.olvColumnValidationError.Text = "ValidationError";
            this.olvColumnValidationError.Width = 262;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSaveDrawingsToFolder,
            this.toolStripButtonRemove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(978, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSaveDrawingsToFolder
            // 
            this.toolStripButtonSaveDrawingsToFolder.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.folder_yellow_save_32;
            this.toolStripButtonSaveDrawingsToFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveDrawingsToFolder.Name = "toolStripButtonSaveDrawingsToFolder";
            this.toolStripButtonSaveDrawingsToFolder.Size = new System.Drawing.Size(104, 22);
            this.toolStripButtonSaveDrawingsToFolder.Text = "Save To Folder";
            this.toolStripButtonSaveDrawingsToFolder.Click += new System.EventHandler(this.toolStripButtonSaveDrawingsToFolder_Click);
            // 
            // toolStripButtonRemove
            // 
            this.toolStripButtonRemove.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.delete_16;
            this.toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemove.Name = "toolStripButtonRemove";
            this.toolStripButtonRemove.Size = new System.Drawing.Size(117, 22);
            this.toolStripButtonRemove.Text = "Remove Drawing";
            this.toolStripButtonRemove.Click += new System.EventHandler(this.toolStripButtonRemove_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // MarkedDrawingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.objectListViewDrawingPackages);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MarkedDrawingsControl";
            this.Size = new System.Drawing.Size(978, 486);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewDrawingPackages)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListViewDrawingPackages;
        private BrightIdeasSoftware.OLVColumn olvColumnFileName;
        private BrightIdeasSoftware.OLVColumn olvColumnPageCount;
        private BrightIdeasSoftware.OLVColumn olvColumnLastRefreshed;
        private BrightIdeasSoftware.OLVColumn olvColumnValidationError;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveDrawingsToFolder;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemove;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;

    }
}
