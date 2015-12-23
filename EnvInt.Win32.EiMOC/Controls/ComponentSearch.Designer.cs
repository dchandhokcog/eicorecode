namespace EnvInt.Win32.EiMOC.Controls
{
    partial class ComponentSearch
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
            this.textBoxTerm = new System.Windows.Forms.TextBox();
            this.buttonFind = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxTerm
            // 
            this.textBoxTerm.Location = new System.Drawing.Point(3, 3);
            this.textBoxTerm.Name = "textBoxTerm";
            this.textBoxTerm.Size = new System.Drawing.Size(107, 20);
            this.textBoxTerm.TabIndex = 0;
            // 
            // buttonFind
            // 
            this.buttonFind.Image = global::EnvInt.Win32.EiMOC.Properties.Resources.binocs_16;
            this.buttonFind.Location = new System.Drawing.Point(20, 30);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(75, 23);
            this.buttonFind.TabIndex = 1;
            this.buttonFind.Text = "Find";
            this.buttonFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // ComponentSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.textBoxTerm);
            this.Name = "ComponentSearch";
            this.Size = new System.Drawing.Size(116, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTerm;
        private System.Windows.Forms.Button buttonFind;
    }
}
