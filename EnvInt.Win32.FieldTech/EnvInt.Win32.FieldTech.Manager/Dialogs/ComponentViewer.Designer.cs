namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    partial class ComponentViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportEid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNearMatchxRef = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMassEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFindReplace = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMove = new System.Windows.Forms.ToolStripButton();
            this.radGridViewComponents = new Telerik.WinControls.UI.RadGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewComponents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewComponents.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExportExcel,
            this.toolStripButtonExportEid,
            this.toolStripButtonNearMatchxRef,
            this.toolStripButtonMassEdit,
            this.toolStripButtonFindReplace,
            this.toolStripSeparator1,
            this.toolStripButtonDelete,
            this.toolStripButtonMove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1049, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExportExcel
            // 
            this.toolStripButtonExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportExcel.Image")));
            this.toolStripButtonExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportExcel.Name = "toolStripButtonExportExcel";
            this.toolStripButtonExportExcel.Size = new System.Drawing.Size(138, 24);
            this.toolStripButtonExportExcel.Text = "Export to Excel";
            this.toolStripButtonExportExcel.Click += new System.EventHandler(this.toolStripButtonExportExcel_Click);
            // 
            // toolStripButtonExportEid
            // 
            this.toolStripButtonExportEid.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.cube_32;
            this.toolStripButtonExportEid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportEid.Name = "toolStripButtonExportEid";
            this.toolStripButtonExportEid.Size = new System.Drawing.Size(124, 24);
            this.toolStripButtonExportEid.Text = "Export to EID";
            this.toolStripButtonExportEid.Click += new System.EventHandler(this.toolStripButtonExportEid_Click);
            // 
            // toolStripButtonNearMatchxRef
            // 
            this.toolStripButtonNearMatchxRef.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNearMatchxRef.Image")));
            this.toolStripButtonNearMatchxRef.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNearMatchxRef.Name = "toolStripButtonNearMatchxRef";
            this.toolStripButtonNearMatchxRef.Size = new System.Drawing.Size(138, 24);
            this.toolStripButtonNearMatchxRef.Text = "Try xRef Match";
            this.toolStripButtonNearMatchxRef.Click += new System.EventHandler(this.toolStripButtonNearMatchxRef_Click);
            // 
            // toolStripButtonMassEdit
            // 
            this.toolStripButtonMassEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMassEdit.Image")));
            this.toolStripButtonMassEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMassEdit.Name = "toolStripButtonMassEdit";
            this.toolStripButtonMassEdit.Size = new System.Drawing.Size(170, 24);
            this.toolStripButtonMassEdit.Text = "Mass Edit Selection";
            this.toolStripButtonMassEdit.Click += new System.EventHandler(this.toolStripButtonMassEdit_Click);
            // 
            // toolStripButtonFindReplace
            // 
            this.toolStripButtonFindReplace.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFindReplace.Image")));
            this.toolStripButtonFindReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFindReplace.Name = "toolStripButtonFindReplace";
            this.toolStripButtonFindReplace.Size = new System.Drawing.Size(124, 24);
            this.toolStripButtonFindReplace.Text = "Find/Replace";
            this.toolStripButtonFindReplace.Click += new System.EventHandler(this.toolStripButtonFindReplace_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::EnvInt.Win32.FieldTech.Manager.Properties.Resources.delete_16;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 24);
            this.toolStripButtonDelete.Text = "toolStripButton1";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonMove
            // 
            this.toolStripButtonMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMove.Image")));
            this.toolStripButtonMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMove.Name = "toolStripButtonMove";
            this.toolStripButtonMove.Size = new System.Drawing.Size(23, 24);
            this.toolStripButtonMove.Text = "Move/Copy";
            this.toolStripButtonMove.Click += new System.EventHandler(this.toolStripButtonMove_Click);
            // 
            // radGridViewComponents
            // 
            this.radGridViewComponents.AutoSizeRows = true;
            this.radGridViewComponents.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radGridViewComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridViewComponents.Location = new System.Drawing.Point(0, 27);
            this.radGridViewComponents.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // radGridViewComponents
            // 
            this.radGridViewComponents.MasterTemplate.MultiSelect = true;
            this.radGridViewComponents.Name = "radGridViewComponents";
            // 
            // 
            // 
            this.radGridViewComponents.RootElement.AccessibleDescription = null;
            this.radGridViewComponents.RootElement.AccessibleName = null;
            this.radGridViewComponents.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.radGridViewComponents.Size = new System.Drawing.Size(1049, 577);
            this.radGridViewComponents.TabIndex = 2;
            this.radGridViewComponents.Text = "radGridView1";
            // 
            // ComponentViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1049, 604);
            this.Controls.Add(this.radGridViewComponents);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ComponentViewer";
            this.Text = "Component Viewer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewComponents.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewComponents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportEid;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportExcel;
        private System.Windows.Forms.ToolStripButton toolStripButtonNearMatchxRef;
        private System.Windows.Forms.ToolStripButton toolStripButtonMassEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonMove;
        private System.Windows.Forms.ToolStripButton toolStripButtonFindReplace;
        private Telerik.WinControls.UI.RadGridView radGridViewComponents;


    }
}