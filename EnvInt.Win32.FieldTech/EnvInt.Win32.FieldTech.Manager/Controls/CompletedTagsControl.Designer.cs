namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    partial class CompletedTagsControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.objectListViewExportedTagData = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.returnToNotExportedBinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewExportedTagData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(646, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.delete_16;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "toolStripButton1";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.objectListViewExportedTagData);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(646, 379);
            this.panelMain.TabIndex = 4;
            // 
            // objectListViewExportedTagData
            // 
            this.objectListViewExportedTagData.AllColumns.Add(this.olvColumn6);
            this.objectListViewExportedTagData.AllColumns.Add(this.olvColumn7);
            this.objectListViewExportedTagData.AllColumns.Add(this.olvColumn8);
            this.objectListViewExportedTagData.AllColumns.Add(this.olvColumn10);
            this.objectListViewExportedTagData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn10});
            this.objectListViewExportedTagData.ContextMenuStrip = this.contextMenuStrip1;
            this.objectListViewExportedTagData.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewExportedTagData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewExportedTagData.Location = new System.Drawing.Point(0, 0);
            this.objectListViewExportedTagData.Name = "objectListViewExportedTagData";
            this.objectListViewExportedTagData.Size = new System.Drawing.Size(646, 379);
            this.objectListViewExportedTagData.TabIndex = 3;
            this.objectListViewExportedTagData.UseCompatibleStateImageBehavior = false;
            this.objectListViewExportedTagData.UseHyperlinks = true;
            this.objectListViewExportedTagData.View = System.Windows.Forms.View.Details;
            this.objectListViewExportedTagData.ItemActivate += new System.EventHandler(this.objectListViewExportedTagData_ItemActivate);
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Device";
            this.olvColumn6.Text = "Device";
            this.olvColumn6.Width = 170;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "CreateDate";
            this.olvColumn7.Text = "TagDates";
            this.olvColumn7.Width = 208;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "TagCount";
            this.olvColumn8.Text = "Items";
            this.olvColumn8.Width = 66;
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "FileName";
            this.olvColumn10.FillsFreeSpace = true;
            this.olvColumn10.ShowTextInHeader = false;
            this.olvColumn10.Text = "Filename";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.returnToNotExportedBinToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(226, 26);
            // 
            // returnToNotExportedBinToolStripMenuItem
            // 
            this.returnToNotExportedBinToolStripMenuItem.Name = "returnToNotExportedBinToolStripMenuItem";
            this.returnToNotExportedBinToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.returnToNotExportedBinToolStripMenuItem.Text = "Return to \"Not Exported\" Bin";
            this.returnToNotExportedBinToolStripMenuItem.Click += new System.EventHandler(this.returnToNotExportedBinToolStripMenuItem_Click);
            // 
            // CompletedTagsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CompletedTagsControl";
            this.Size = new System.Drawing.Size(646, 404);
            this.VisibleChanged += new System.EventHandler(this.CompletedTagsControl_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewExportedTagData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panelMain;
        private BrightIdeasSoftware.ObjectListView objectListViewExportedTagData;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem returnToNotExportedBinToolStripMenuItem;

    }
}
