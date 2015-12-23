namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    partial class RetagControl
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
            this.radPageViewMain = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
            this.objectListViewPendingTagData = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewMain)).BeginInit();
            this.radPageViewMain.SuspendLayout();
            this.radPageViewPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPendingTagData)).BeginInit();
            this.SuspendLayout();
            // 
            // radPageViewMain
            // 
            this.radPageViewMain.Controls.Add(this.radPageViewPage1);
            this.radPageViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageViewMain.Location = new System.Drawing.Point(0, 0);
            this.radPageViewMain.Name = "radPageViewMain";
            this.radPageViewMain.SelectedPage = this.radPageViewPage1;
            this.radPageViewMain.Size = new System.Drawing.Size(646, 404);
            this.radPageViewMain.TabIndex = 0;
            this.radPageViewMain.Text = "radPageView1";
            // 
            // radPageViewPage1
            // 
// TODO: Code generation for '' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.radPageViewPage1.Controls.Add(this.objectListViewPendingTagData);
            this.radPageViewPage1.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPage1.Name = "radPageViewPage1";
            this.radPageViewPage1.Size = new System.Drawing.Size(625, 356);
            this.radPageViewPage1.Text = "Pending Tag Data";
            // 
            // objectListViewPendingTagData
            // 
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn1);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn2);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn3);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn5);
            this.objectListViewPendingTagData.AllColumns.Add(this.olvColumn4);
            this.objectListViewPendingTagData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn5,
            this.olvColumn4});
            this.objectListViewPendingTagData.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewPendingTagData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewPendingTagData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.objectListViewPendingTagData.Location = new System.Drawing.Point(0, 0);
            this.objectListViewPendingTagData.MultiSelect = false;
            this.objectListViewPendingTagData.Name = "objectListViewPendingTagData";
            this.objectListViewPendingTagData.Size = new System.Drawing.Size(625, 356);
            this.objectListViewPendingTagData.TabIndex = 0;
            this.objectListViewPendingTagData.UseCompatibleStateImageBehavior = false;
            this.objectListViewPendingTagData.View = System.Windows.Forms.View.Details;
            this.objectListViewPendingTagData.ItemActivate += new System.EventHandler(this.objectListViewPendingTagData_ItemActivate);
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
            this.olvColumn3.AspectName = "Tags.Count";
            this.olvColumn3.Text = "Items";
            this.olvColumn3.Width = 66;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Images.Count";
            this.olvColumn5.Text = "Images";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "FileName";
            this.olvColumn4.FillsFreeSpace = true;
            this.olvColumn4.ShowTextInHeader = false;
            this.olvColumn4.Text = "Filename";
            // 
            // RetagControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radPageViewMain);
            this.Name = "RetagControl";
            this.Size = new System.Drawing.Size(646, 404);
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewMain)).EndInit();
            this.radPageViewMain.ResumeLayout(false);
            this.radPageViewPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewPendingTagData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView radPageViewMain;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
        private BrightIdeasSoftware.ObjectListView objectListViewPendingTagData;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
    }
}
