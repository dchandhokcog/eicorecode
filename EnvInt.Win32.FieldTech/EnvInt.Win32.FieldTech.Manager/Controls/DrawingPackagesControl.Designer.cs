namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    partial class DrawingPackagesControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingPackagesControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddDrawing = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveDrawing = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.objectListViewDrawingPackages = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnFileName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPageCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLastRefreshed = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnValidationError = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewDrawingPackages)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh,
            this.toolStripButtonAddDrawing,
            this.toolStripButtonRemoveDrawing});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(805, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(165, 22);
            this.toolStripButtonRefresh.Text = "Refresh Drawing Packages";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonAddDrawing
            // 
            this.toolStripButtonAddDrawing.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddDrawing.Image")));
            this.toolStripButtonAddDrawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddDrawing.Name = "toolStripButtonAddDrawing";
            this.toolStripButtonAddDrawing.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonAddDrawing.Text = "Add Drawing";
            this.toolStripButtonAddDrawing.Click += new System.EventHandler(this.toolStripButtonAddDrawing_Click);
            // 
            // toolStripButtonRemoveDrawing
            // 
            this.toolStripButtonRemoveDrawing.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.delete_16;
            this.toolStripButtonRemoveDrawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveDrawing.Name = "toolStripButtonRemoveDrawing";
            this.toolStripButtonRemoveDrawing.Size = new System.Drawing.Size(117, 22);
            this.toolStripButtonRemoveDrawing.Text = "Remove Drawing";
            this.toolStripButtonRemoveDrawing.Click += new System.EventHandler(this.toolStripButtonRemoveDrawing_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.objectListViewDrawingPackages);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(805, 401);
            this.panelMain.TabIndex = 2;
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
            this.objectListViewDrawingPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewDrawingPackages.FullRowSelect = true;
            this.objectListViewDrawingPackages.Location = new System.Drawing.Point(0, 0);
            this.objectListViewDrawingPackages.Name = "objectListViewDrawingPackages";
            this.objectListViewDrawingPackages.ShowGroups = false;
            this.objectListViewDrawingPackages.Size = new System.Drawing.Size(805, 401);
            this.objectListViewDrawingPackages.TabIndex = 0;
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
            // DrawingPackagesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DrawingPackagesControl";
            this.Size = new System.Drawing.Size(805, 426);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewDrawingPackages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.Panel panelMain;
        private BrightIdeasSoftware.ObjectListView objectListViewDrawingPackages;
        private BrightIdeasSoftware.OLVColumn olvColumnFileName;
        private BrightIdeasSoftware.OLVColumn olvColumnPageCount;
        private BrightIdeasSoftware.OLVColumn olvColumnLastRefreshed;
        private BrightIdeasSoftware.OLVColumn olvColumnValidationError;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddDrawing;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveDrawing;

    }
}
