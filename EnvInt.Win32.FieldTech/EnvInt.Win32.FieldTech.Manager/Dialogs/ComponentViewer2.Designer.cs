namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    partial class ComponentViewer2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentViewer2));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportCSV = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportEid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNearMatchxRef = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMassEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExportCSV,
            this.toolStripButtonExportEid,
            this.toolStripButtonNearMatchxRef,
            this.toolStripButtonMassEdit,
            this.toolStripButtonDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(787, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExportCSV
            // 
            this.toolStripButtonExportCSV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportCSV.Image")));
            this.toolStripButtonExportCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportCSV.Name = "toolStripButtonExportCSV";
            this.toolStripButtonExportCSV.Size = new System.Drawing.Size(98, 22);
            this.toolStripButtonExportCSV.Text = "Export to CSV";
            // 
            // toolStripButtonExportEid
            // 
            this.toolStripButtonExportEid.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.cube_32;
            this.toolStripButtonExportEid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportEid.Name = "toolStripButtonExportEid";
            this.toolStripButtonExportEid.Size = new System.Drawing.Size(94, 22);
            this.toolStripButtonExportEid.Text = "Export to EID";
            // 
            // toolStripButtonNearMatchxRef
            // 
            this.toolStripButtonNearMatchxRef.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNearMatchxRef.Image")));
            this.toolStripButtonNearMatchxRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNearMatchxRef.Name = "toolStripButtonNearMatchxRef";
            this.toolStripButtonNearMatchxRef.Size = new System.Drawing.Size(106, 22);
            this.toolStripButtonNearMatchxRef.Text = "Try xRef Match";
            // 
            // toolStripButtonMassEdit
            // 
            this.toolStripButtonMassEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMassEdit.Image")));
            this.toolStripButtonMassEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMassEdit.Name = "toolStripButtonMassEdit";
            this.toolStripButtonMassEdit.Size = new System.Drawing.Size(128, 22);
            this.toolStripButtonMassEdit.Text = "Mass Edit Selection";
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.delete_16;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "toolStripButton1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer1.Size = new System.Drawing.Size(787, 466);
            this.splitContainer1.SplitterDistance = 313;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(787, 313);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(787, 149);
            this.dataGridView2.TabIndex = 0;
            // 
            // ComponentViewer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 491);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ComponentViewer2";
            this.Text = "Component Viewer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportEid;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportCSV;
        private System.Windows.Forms.ToolStripButton toolStripButtonNearMatchxRef;
        private System.Windows.Forms.ToolStripButton toolStripButtonMassEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;


    }
}