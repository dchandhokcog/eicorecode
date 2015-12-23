namespace EnvInt.Win32.EiMOC
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblNoTagsLoaded = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(779, 483);
            this.dataGridView1.TabIndex = 0;
            // 
            // lblNoTagsLoaded
            // 
            this.lblNoTagsLoaded.AutoSize = true;
            this.lblNoTagsLoaded.ForeColor = System.Drawing.Color.Red;
            this.lblNoTagsLoaded.Location = new System.Drawing.Point(12, 509);
            this.lblNoTagsLoaded.Name = "lblNoTagsLoaded";
            this.lblNoTagsLoaded.Size = new System.Drawing.Size(498, 13);
            this.lblNoTagsLoaded.TabIndex = 2;
            this.lblNoTagsLoaded.Text = "No existing components loaded yet.  Open a Field Tech Toolbox project file to loa" +
    "d existing components.";
            // 
            // FormOldTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 531);
            this.Controls.Add(this.lblNoTagsLoaded);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormOldTags";
            this.Text = "Existing Tag Inventory";
            this.Load += new System.EventHandler(this.FormOldTags_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblNoTagsLoaded;
    }
}