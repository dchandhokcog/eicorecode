namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    partial class QAQCControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QAQCControl));
            this.objectListViewItems = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefreshQAQC = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportCSV = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewItems)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectListViewItems
            // 
            this.objectListViewItems.AllColumns.Add(this.olvColumn1);
            this.objectListViewItems.AllColumns.Add(this.olvColumn2);
            this.objectListViewItems.AllColumns.Add(this.olvColumn4);
            this.objectListViewItems.AllColumns.Add(this.olvColumn3);
            this.objectListViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListViewItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn4,
            this.olvColumn3});
            this.objectListViewItems.Location = new System.Drawing.Point(0, 28);
            this.objectListViewItems.Name = "objectListViewItems";
            this.objectListViewItems.Size = new System.Drawing.Size(715, 570);
            this.objectListViewItems.TabIndex = 0;
            this.objectListViewItems.UseCompatibleStateImageBehavior = false;
            this.objectListViewItems.View = System.Windows.Forms.View.Details;
            this.objectListViewItems.ItemActivate += new System.EventHandler(this.objectListViewItems_ItemActivate);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Device";
            this.olvColumn1.Text = "Device";
            this.olvColumn1.Width = 151;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "QACheck";
            this.olvColumn2.Text = "QA Check";
            this.olvColumn2.Width = 192;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "WarningMsg";
            this.olvColumn4.Text = "Warnings";
            this.olvColumn4.Width = 138;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Message";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.Text = "Errors";
            this.olvColumn3.Width = 356;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefreshQAQC,
            this.toolStripButtonExportCSV});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(715, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRefreshQAQC
            // 
            this.toolStripButtonRefreshQAQC.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.refresh_16;
            this.toolStripButtonRefreshQAQC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefreshQAQC.Name = "toolStripButtonRefreshQAQC";
            this.toolStripButtonRefreshQAQC.Size = new System.Drawing.Size(103, 22);
            this.toolStripButtonRefreshQAQC.Text = "Refresh QAQC";
            this.toolStripButtonRefreshQAQC.Click += new System.EventHandler(this.toolStripButtonRefreshQAQC_Click);
            // 
            // toolStripButtonExportCSV
            // 
            this.toolStripButtonExportCSV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportCSV.Image")));
            this.toolStripButtonExportCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportCSV.Name = "toolStripButtonExportCSV";
            this.toolStripButtonExportCSV.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonExportCSV.Text = "Print Preview";
            this.toolStripButtonExportCSV.Click += new System.EventHandler(this.toolStripButtonViewReport);
            // 
            // QAQCControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.objectListViewItems);
            this.Name = "QAQCControl";
            this.Size = new System.Drawing.Size(715, 598);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewItems)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListViewItems;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefreshQAQC;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportCSV;
    }
}
