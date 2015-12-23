namespace EnvInt.Win32.FieldTech
{
    partial class FormOldTags
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOldTags));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblNoTagsLoaded = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLocationFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTagFilter = new System.Windows.Forms.TextBox();
            this.labelEquipment = new System.Windows.Forms.Label();
            this.textBoxEquipment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // lblNoTagsLoaded
            // 
            resources.ApplyResources(this.lblNoTagsLoaded, "lblNoTagsLoaded");
            this.lblNoTagsLoaded.ForeColor = System.Drawing.Color.Red;
            this.lblNoTagsLoaded.Name = "lblNoTagsLoaded";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxLocationFilter
            // 
            resources.ApplyResources(this.textBoxLocationFilter, "textBoxLocationFilter");
            this.textBoxLocationFilter.Name = "textBoxLocationFilter";
            this.textBoxLocationFilter.TextChanged += new System.EventHandler(this.textBoxLocationFilter_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxTagFilter
            // 
            resources.ApplyResources(this.textBoxTagFilter, "textBoxTagFilter");
            this.textBoxTagFilter.Name = "textBoxTagFilter";
            this.textBoxTagFilter.TextChanged += new System.EventHandler(this.textBoxTagFilter_TextChanged);
            // 
            // labelEquipment
            // 
            resources.ApplyResources(this.labelEquipment, "labelEquipment");
            this.labelEquipment.Name = "labelEquipment";
            // 
            // textBoxEquipment
            // 
            resources.ApplyResources(this.textBoxEquipment, "textBoxEquipment");
            this.textBoxEquipment.Name = "textBoxEquipment";
            this.textBoxEquipment.TextChanged += new System.EventHandler(this.textBoxEquipment_TextChanged);
            // 
            // FormOldTags
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelEquipment);
            this.Controls.Add(this.textBoxEquipment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxLocationFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTagFilter);
            this.Controls.Add(this.lblNoTagsLoaded);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormOldTags";
            this.Load += new System.EventHandler(this.FormOldTags_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblNoTagsLoaded;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLocationFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTagFilter;
        private System.Windows.Forms.Label labelEquipment;
        private System.Windows.Forms.TextBox textBoxEquipment;
    }
}