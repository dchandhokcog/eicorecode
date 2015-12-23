namespace EnvInt.Win32.FieldTech
{
    partial class Splash
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonSplashOK = new System.Windows.Forms.Button();
            this.checkBoxShowSplash = new System.Windows.Forms.CheckBox();
            this.checkBoxShowSample = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTesting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EnvInt.Win32.FieldTech.Properties.Resources.EI_Logo_Sitefinity;
            this.pictureBox1.Location = new System.Drawing.Point(303, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 61);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // buttonSplashOK
            // 
            this.buttonSplashOK.Location = new System.Drawing.Point(147, 172);
            this.buttonSplashOK.Name = "buttonSplashOK";
            this.buttonSplashOK.Size = new System.Drawing.Size(75, 23);
            this.buttonSplashOK.TabIndex = 1;
            this.buttonSplashOK.Text = "OK";
            this.buttonSplashOK.UseVisualStyleBackColor = true;
            this.buttonSplashOK.Click += new System.EventHandler(this.buttonSplashOK_Click);
            // 
            // checkBoxShowSplash
            // 
            this.checkBoxShowSplash.AutoSize = true;
            this.checkBoxShowSplash.Checked = true;
            this.checkBoxShowSplash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowSplash.Location = new System.Drawing.Point(34, 211);
            this.checkBoxShowSplash.Name = "checkBoxShowSplash";
            this.checkBoxShowSplash.Size = new System.Drawing.Size(155, 17);
            this.checkBoxShowSplash.TabIndex = 4;
            this.checkBoxShowSplash.Text = "Show this dialog on Startup";
            this.checkBoxShowSplash.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowSample
            // 
            this.checkBoxShowSample.AutoSize = true;
            this.checkBoxShowSample.Checked = true;
            this.checkBoxShowSample.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowSample.Location = new System.Drawing.Point(34, 234);
            this.checkBoxShowSample.Name = "checkBoxShowSample";
            this.checkBoxShowSample.Size = new System.Drawing.Size(197, 17);
            this.checkBoxShowSample.TabIndex = 5;
            this.checkBoxShowSample.Text = "Open Previous Project Automatically";
            this.checkBoxShowSample.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 49);
            this.label1.TabIndex = 6;
            this.label1.Text = "FieldTech Toolbox / EiMOC gives you the software needed onsite for efficient and " +
    "informed data collection.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 82);
            this.label2.TabIndex = 7;
            this.label2.Text = "- Tagging by Engineered Drawings or Isometrics\r\n- Auto Tag Generator\r\n- Location " +
    "Tracking\r\n- DTM/UTM Flagging\r\n- Export Field Activities directly into the target" +
    " LDAR database.";
            // 
            // labelTesting
            // 
            this.labelTesting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTesting.ForeColor = System.Drawing.Color.Red;
            this.labelTesting.Location = new System.Drawing.Point(250, 117);
            this.labelTesting.Name = "labelTesting";
            this.labelTesting.Size = new System.Drawing.Size(124, 133);
            this.labelTesting.TabIndex = 8;
            this.labelTesting.Text = "This version of the software is an engineering sample to be used for testing only" +
    "!";
            this.labelTesting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTesting.Visible = false;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 270);
            this.ControlBox = false;
            this.Controls.Add(this.labelTesting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxShowSample);
            this.Controls.Add(this.checkBoxShowSplash);
            this.Controls.Add(this.buttonSplashOK);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome!";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonSplashOK;
        private System.Windows.Forms.CheckBox checkBoxShowSplash;
        private System.Windows.Forms.CheckBox checkBoxShowSample;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTesting;

    }
}