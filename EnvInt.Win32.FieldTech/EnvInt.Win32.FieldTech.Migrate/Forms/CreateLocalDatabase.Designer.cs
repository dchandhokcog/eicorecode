namespace EnvInt.Win32.FieldTech.Migrate.Forms
{
    partial class CreateLocalDatabase
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
            this.textBoxRecipe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSelectRecipe = new System.Windows.Forms.Button();
            this.buttonSelectDestination = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDestinationDB = new System.Windows.Forms.TextBox();
            this.comboBoxConnection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxRecipe
            // 
            this.textBoxRecipe.Location = new System.Drawing.Point(148, 30);
            this.textBoxRecipe.Name = "textBoxRecipe";
            this.textBoxRecipe.Size = new System.Drawing.Size(293, 20);
            this.textBoxRecipe.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Import Recipe File";
            // 
            // buttonSelectRecipe
            // 
            this.buttonSelectRecipe.Location = new System.Drawing.Point(447, 29);
            this.buttonSelectRecipe.Name = "buttonSelectRecipe";
            this.buttonSelectRecipe.Size = new System.Drawing.Size(30, 23);
            this.buttonSelectRecipe.TabIndex = 2;
            this.buttonSelectRecipe.Text = "...";
            this.buttonSelectRecipe.UseVisualStyleBackColor = true;
            this.buttonSelectRecipe.Click += new System.EventHandler(this.buttonSelectRecipe_Click);
            // 
            // buttonSelectDestination
            // 
            this.buttonSelectDestination.Location = new System.Drawing.Point(447, 61);
            this.buttonSelectDestination.Name = "buttonSelectDestination";
            this.buttonSelectDestination.Size = new System.Drawing.Size(30, 23);
            this.buttonSelectDestination.TabIndex = 5;
            this.buttonSelectDestination.Text = "...";
            this.buttonSelectDestination.UseVisualStyleBackColor = true;
            this.buttonSelectDestination.Click += new System.EventHandler(this.buttonSelectDestination_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destination File";
            // 
            // textBoxDestinationDB
            // 
            this.textBoxDestinationDB.Location = new System.Drawing.Point(148, 62);
            this.textBoxDestinationDB.Name = "textBoxDestinationDB";
            this.textBoxDestinationDB.Size = new System.Drawing.Size(293, 20);
            this.textBoxDestinationDB.TabIndex = 3;
            // 
            // comboBoxConnection
            // 
            this.comboBoxConnection.FormattingEnabled = true;
            this.comboBoxConnection.Location = new System.Drawing.Point(148, 98);
            this.comboBoxConnection.Name = "comboBoxConnection";
            this.comboBoxConnection.Size = new System.Drawing.Size(292, 21);
            this.comboBoxConnection.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Database Connection";
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(402, 150);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 8;
            this.buttonContinue.Text = "&Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(312, 150);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // CreateLocalDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 185);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxConnection);
            this.Controls.Add(this.buttonSelectDestination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxDestinationDB);
            this.Controls.Add(this.buttonSelectRecipe);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRecipe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CreateLocalDatabase";
            this.Text = "Create Local Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRecipe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSelectRecipe;
        private System.Windows.Forms.Button buttonSelectDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDestinationDB;
        private System.Windows.Forms.ComboBox comboBoxConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button buttonCancel;
    }
}