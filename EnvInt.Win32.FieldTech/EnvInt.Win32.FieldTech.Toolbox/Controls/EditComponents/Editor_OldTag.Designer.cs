namespace EnvInt.Win32.FieldTech.Controls.EditComponents
{
    partial class Editor_OldTag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor_OldTag));
            this.textBoxPreviousTag = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonRefreshOldTag = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPreviousTag
            // 
            this.textBoxPreviousTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPreviousTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPreviousTag.Location = new System.Drawing.Point(110, 3);
            this.textBoxPreviousTag.Name = "textBoxPreviousTag";
            this.textBoxPreviousTag.Size = new System.Drawing.Size(305, 26);
            this.textBoxPreviousTag.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 29;
            this.label7.Text = "Old Tag:";
            // 
            // buttonRefreshOldTag
            // 
            this.buttonRefreshOldTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshOldTag.Enabled = false;
            this.buttonRefreshOldTag.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshOldTag.Image")));
            this.buttonRefreshOldTag.Location = new System.Drawing.Point(421, 2);
            this.buttonRefreshOldTag.Name = "buttonRefreshOldTag";
            this.buttonRefreshOldTag.Size = new System.Drawing.Size(29, 28);
            this.buttonRefreshOldTag.TabIndex = 30;
            this.buttonRefreshOldTag.UseVisualStyleBackColor = true;
            // 
            // Editor_OldTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxPreviousTag);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonRefreshOldTag);
            this.Name = "Editor_OldTag";
            this.Size = new System.Drawing.Size(453, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPreviousTag;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonRefreshOldTag;
    }
}
