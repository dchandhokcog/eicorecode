namespace EnvInt.Win32.FieldTech.ADR
{
    partial class ADRContainer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADRContainer));
            this.axCExpressViewerControl1 = new AxExpressViewerDll.AxCExpressViewerControl();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.axCExpressViewerControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axCExpressViewerControl1
            // 
            this.axCExpressViewerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCExpressViewerControl1.Enabled = true;
            this.axCExpressViewerControl1.Location = new System.Drawing.Point(0, 0);
            this.axCExpressViewerControl1.Margin = new System.Windows.Forms.Padding(2);
            this.axCExpressViewerControl1.Name = "axCExpressViewerControl1";
            this.axCExpressViewerControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCExpressViewerControl1.OcxState")));
            this.axCExpressViewerControl1.Size = new System.Drawing.Size(963, 459);
            this.axCExpressViewerControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(1, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(961, 27);
            this.panel1.TabIndex = 1;
            // 
            // ADRContainer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CausesValidation = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.axCExpressViewerControl1);
            this.Name = "ADRContainer";
            this.Size = new System.Drawing.Size(963, 459);
            ((System.ComponentModel.ISupportInitialize)(this.axCExpressViewerControl1)).EndInit();
            this.ResumeLayout(false);

        }

        private AxExpressViewerDll.AxCExpressViewerControl axCExpressViewerControl1;

        #endregion
        private System.Windows.Forms.Panel panel1;
    }
}
