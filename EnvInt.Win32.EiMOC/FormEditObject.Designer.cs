namespace EnvInt.Win32.EiMOC
{
    partial class FormEditObject
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditObject));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLDARTag = new System.Windows.Forms.TextBox();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.contextMenuStripLocation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.userDefinedMessage1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userDefinedMessage2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userDefinedMessage3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAccess = new System.Windows.Forms.ComboBox();
            this.textBoxPreviousTag = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxEngineeringTag = new System.Windows.Forms.TextBox();
            this.textBoxComponentType = new System.Windows.Forms.TextBox();
            this.textBoxStream = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonChildAdd = new System.Windows.Forms.Button();
            this.buttonChildRemove = new System.Windows.Forms.Button();
            this.listViewChildren = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonLocationBuilder = new System.Windows.Forms.Button();
            this.buttonRefreshOldTag = new System.Windows.Forms.Button();
            this.buttonInspect = new System.Windows.Forms.Button();
            this.buttonSetBackground = new System.Windows.Forms.Button();
            this.contextMenuStripLocation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(490, 384);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(111, 33);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Done Tagging";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(622, 384);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 33);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Component:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CAD ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "New Tag:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Location:";
            // 
            // textBoxLDARTag
            // 
            this.textBoxLDARTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLDARTag.Location = new System.Drawing.Point(87, 50);
            this.textBoxLDARTag.Name = "textBoxLDARTag";
            this.textBoxLDARTag.Size = new System.Drawing.Size(194, 26);
            this.textBoxLDARTag.TabIndex = 9;
            this.textBoxLDARTag.TextChanged += new System.EventHandler(this.textBoxLDARTag_TextChanged);
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.ContextMenuStrip = this.contextMenuStripLocation;
            this.textBoxLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLocation.Location = new System.Drawing.Point(87, 136);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(272, 26);
            this.textBoxLocation.TabIndex = 10;
            // 
            // contextMenuStripLocation
            // 
            this.contextMenuStripLocation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userDefinedMessage1ToolStripMenuItem,
            this.userDefinedMessage2ToolStripMenuItem,
            this.userDefinedMessage3ToolStripMenuItem});
            this.contextMenuStripLocation.Name = "contextMenuStripLocation";
            this.contextMenuStripLocation.Size = new System.Drawing.Size(207, 70);
            // 
            // userDefinedMessage1ToolStripMenuItem
            // 
            this.userDefinedMessage1ToolStripMenuItem.Name = "userDefinedMessage1ToolStripMenuItem";
            this.userDefinedMessage1ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.userDefinedMessage1ToolStripMenuItem.Text = "User Defined Message #1";
            // 
            // userDefinedMessage2ToolStripMenuItem
            // 
            this.userDefinedMessage2ToolStripMenuItem.Name = "userDefinedMessage2ToolStripMenuItem";
            this.userDefinedMessage2ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.userDefinedMessage2ToolStripMenuItem.Text = "User Defined Message #2";
            // 
            // userDefinedMessage3ToolStripMenuItem
            // 
            this.userDefinedMessage3ToolStripMenuItem.Name = "userDefinedMessage3ToolStripMenuItem";
            this.userDefinedMessage3ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.userDefinedMessage3ToolStripMenuItem.Text = "User Defined Message #3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Access:";
            // 
            // comboBoxAccess
            // 
            this.comboBoxAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAccess.FormattingEnabled = true;
            this.comboBoxAccess.Items.AddRange(new object[] {
            "NTM",
            "DTM - >2 Meters",
            "DTM - 8 Foot Ladder",
            "DTM - Extension Ladder",
            "DTM - Harness",
            "DTM - Scaffold",
            "DTM - JLG",
            "UTM - Inaccessible Component",
            "UTM - Unsafe Location"});
            this.comboBoxAccess.Location = new System.Drawing.Point(397, 50);
            this.comboBoxAccess.Name = "comboBoxAccess";
            this.comboBoxAccess.Size = new System.Drawing.Size(292, 28);
            this.comboBoxAccess.TabIndex = 14;
            // 
            // textBoxPreviousTag
            // 
            this.textBoxPreviousTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPreviousTag.Location = new System.Drawing.Point(87, 92);
            this.textBoxPreviousTag.Name = "textBoxPreviousTag";
            this.textBoxPreviousTag.Size = new System.Drawing.Size(194, 26);
            this.textBoxPreviousTag.TabIndex = 18;
            this.textBoxPreviousTag.TextChanged += new System.EventHandler(this.textBoxPreviousTag_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Old Tag:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(325, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Stream:";
            // 
            // textBoxEngineeringTag
            // 
            this.textBoxEngineeringTag.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxEngineeringTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEngineeringTag.Location = new System.Drawing.Point(397, 10);
            this.textBoxEngineeringTag.Name = "textBoxEngineeringTag";
            this.textBoxEngineeringTag.Size = new System.Drawing.Size(292, 26);
            this.textBoxEngineeringTag.TabIndex = 23;
            this.textBoxEngineeringTag.TextChanged += new System.EventHandler(this.textBoxEngineeringTag_TextChanged);
            // 
            // textBoxComponentType
            // 
            this.textBoxComponentType.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxComponentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxComponentType.Location = new System.Drawing.Point(87, 10);
            this.textBoxComponentType.Name = "textBoxComponentType";
            this.textBoxComponentType.Size = new System.Drawing.Size(194, 26);
            this.textBoxComponentType.TabIndex = 24;
            // 
            // textBoxStream
            // 
            this.textBoxStream.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStream.Location = new System.Drawing.Point(397, 92);
            this.textBoxStream.Name = "textBoxStream";
            this.textBoxStream.Size = new System.Drawing.Size(292, 26);
            this.textBoxStream.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonChildAdd);
            this.groupBox1.Controls.Add(this.buttonChildRemove);
            this.groupBox1.Controls.Add(this.listViewChildren);
            this.groupBox1.Location = new System.Drawing.Point(12, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(677, 185);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Child Components";
            // 
            // buttonChildAdd
            // 
            this.buttonChildAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChildAdd.Image = global::EnvInt.Win32.EiMOC.Properties.Resources.add_32;
            this.buttonChildAdd.Location = new System.Drawing.Point(617, 19);
            this.buttonChildAdd.Name = "buttonChildAdd";
            this.buttonChildAdd.Size = new System.Drawing.Size(54, 54);
            this.buttonChildAdd.TabIndex = 2;
            this.buttonChildAdd.UseVisualStyleBackColor = true;
            this.buttonChildAdd.Click += new System.EventHandler(this.buttonChildAdd_Click);
            // 
            // buttonChildRemove
            // 
            this.buttonChildRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChildRemove.Image = global::EnvInt.Win32.EiMOC.Properties.Resources.delete_32;
            this.buttonChildRemove.Location = new System.Drawing.Point(617, 79);
            this.buttonChildRemove.Name = "buttonChildRemove";
            this.buttonChildRemove.Size = new System.Drawing.Size(54, 54);
            this.buttonChildRemove.TabIndex = 1;
            this.buttonChildRemove.UseVisualStyleBackColor = true;
            this.buttonChildRemove.Click += new System.EventHandler(this.buttonChildRemove_Click);
            // 
            // listViewChildren
            // 
            this.listViewChildren.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.listViewChildren.FullRowSelect = true;
            this.listViewChildren.Location = new System.Drawing.Point(6, 19);
            this.listViewChildren.Name = "listViewChildren";
            this.listViewChildren.Size = new System.Drawing.Size(605, 160);
            this.listViewChildren.TabIndex = 0;
            this.listViewChildren.UseCompatibleStateImageBehavior = false;
            this.listViewChildren.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 149;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "New Tag";
            this.columnHeader2.Width = 145;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Old Tag";
            this.columnHeader4.Width = 112;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Location";
            this.columnHeader3.Width = 208;
            // 
            // buttonLocationBuilder
            // 
            this.buttonLocationBuilder.Image = global::EnvInt.Win32.EiMOC.Properties.Resources._26;
            this.buttonLocationBuilder.Location = new System.Drawing.Point(365, 136);
            this.buttonLocationBuilder.Name = "buttonLocationBuilder";
            this.buttonLocationBuilder.Size = new System.Drawing.Size(29, 28);
            this.buttonLocationBuilder.TabIndex = 12;
            this.buttonLocationBuilder.UseVisualStyleBackColor = true;
            this.buttonLocationBuilder.Click += new System.EventHandler(this.buttonLocationBuilder_Click);
            // 
            // buttonRefreshOldTag
            // 
            this.buttonRefreshOldTag.Enabled = false;
            this.buttonRefreshOldTag.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshOldTag.Image")));
            this.buttonRefreshOldTag.Location = new System.Drawing.Point(287, 92);
            this.buttonRefreshOldTag.Name = "buttonRefreshOldTag";
            this.buttonRefreshOldTag.Size = new System.Drawing.Size(29, 28);
            this.buttonRefreshOldTag.TabIndex = 27;
            this.buttonRefreshOldTag.UseVisualStyleBackColor = true;
            this.buttonRefreshOldTag.Click += new System.EventHandler(this.buttonRefreshOldTag_Click);
            // 
            // buttonInspect
            // 
            this.buttonInspect.Location = new System.Drawing.Point(343, 384);
            this.buttonInspect.Name = "buttonInspect";
            this.buttonInspect.Size = new System.Drawing.Size(108, 33);
            this.buttonInspect.TabIndex = 42;
            this.buttonInspect.Text = "&Inspect";
            this.buttonInspect.UseVisualStyleBackColor = true;
            // 
            // buttonSetBackground
            // 
            this.buttonSetBackground.Location = new System.Drawing.Point(223, 384);
            this.buttonSetBackground.Name = "buttonSetBackground";
            this.buttonSetBackground.Size = new System.Drawing.Size(108, 33);
            this.buttonSetBackground.TabIndex = 41;
            this.buttonSetBackground.Text = "&Set Background";
            this.buttonSetBackground.UseVisualStyleBackColor = true;
            this.buttonSetBackground.Click += new System.EventHandler(this.buttonSetBackground_Click);
            // 
            // FormEditObject
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(721, 438);
            this.ControlBox = false;
            this.Controls.Add(this.buttonInspect);
            this.Controls.Add(this.buttonSetBackground);
            this.Controls.Add(this.buttonRefreshOldTag);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxStream);
            this.Controls.Add(this.textBoxComponentType);
            this.Controls.Add(this.textBoxEngineeringTag);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxPreviousTag);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxAccess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonLocationBuilder);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.textBoxLDARTag);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(317, 337);
            this.Name = "FormEditObject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit Component Values:";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormEditObject_Load);
            this.contextMenuStripLocation.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxLDARTag;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLocation;
        private System.Windows.Forms.ToolStripMenuItem userDefinedMessage1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userDefinedMessage2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userDefinedMessage3ToolStripMenuItem;
        private System.Windows.Forms.Button buttonLocationBuilder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAccess;
        private System.Windows.Forms.TextBox textBoxPreviousTag;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxEngineeringTag;
        private System.Windows.Forms.TextBox textBoxComponentType;
        private System.Windows.Forms.TextBox textBoxStream;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonChildAdd;
        private System.Windows.Forms.Button buttonChildRemove;
        private System.Windows.Forms.ListView listViewChildren;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonRefreshOldTag;
        private System.Windows.Forms.Button buttonInspect;
        private System.Windows.Forms.Button buttonSetBackground;
    }
}