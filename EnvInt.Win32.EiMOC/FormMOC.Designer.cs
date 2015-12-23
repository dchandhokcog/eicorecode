namespace EnvInt.Win32.EiMOC
{
    partial class FormMOC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMOC));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonChildAdd = new System.Windows.Forms.Button();
            this.buttonChildRemove = new System.Windows.Forms.Button();
            this.listViewChildren = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxState = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDrawing = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.comboBoxUnit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxMOCNumber = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.buttonNextComponent = new System.Windows.Forms.Button();
            this.comboBoxStream = new System.Windows.Forms.ComboBox();
            this.buttonSetBackground = new System.Windows.Forms.Button();
            this.buttonInspect = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.labelInspection = new System.Windows.Forms.Label();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.buttonRefreshOldTag = new System.Windows.Forms.Button();
            this.buttonLocationBuilder = new System.Windows.Forms.Button();
            this.contextMenuStripLocation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(13, 506);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(127, 33);
            this.buttonOK.TabIndex = 11;
            this.buttonOK.Text = "&Done Tagging";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(615, 506);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 33);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(322, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "New Tag:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Location:";
            // 
            // textBoxLDARTag
            // 
            this.textBoxLDARTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLDARTag.Location = new System.Drawing.Point(97, 56);
            this.textBoxLDARTag.Name = "textBoxLDARTag";
            this.textBoxLDARTag.Size = new System.Drawing.Size(183, 26);
            this.textBoxLDARTag.TabIndex = 1;
            this.textBoxLDARTag.TextChanged += new System.EventHandler(this.textBoxLDARTag_TextChanged);
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.ContextMenuStrip = this.contextMenuStripLocation;
            this.textBoxLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLocation.Location = new System.Drawing.Point(97, 184);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(261, 26);
            this.textBoxLocation.TabIndex = 4;
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
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(322, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Access:";
            // 
            // comboBoxAccess
            // 
            this.comboBoxAccess.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxAccess.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
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
            this.comboBoxAccess.Location = new System.Drawing.Point(401, 56);
            this.comboBoxAccess.Name = "comboBoxAccess";
            this.comboBoxAccess.Size = new System.Drawing.Size(287, 28);
            this.comboBoxAccess.TabIndex = 6;
            // 
            // textBoxPreviousTag
            // 
            this.textBoxPreviousTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPreviousTag.Location = new System.Drawing.Point(97, 98);
            this.textBoxPreviousTag.Name = "textBoxPreviousTag";
            this.textBoxPreviousTag.Size = new System.Drawing.Size(183, 26);
            this.textBoxPreviousTag.TabIndex = 2;
            this.textBoxPreviousTag.TextChanged += new System.EventHandler(this.textBoxPreviousTag_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Old Tag:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(322, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "Stream:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonChildAdd);
            this.groupBox1.Controls.Add(this.buttonChildRemove);
            this.groupBox1.Controls.Add(this.listViewChildren);
            this.groupBox1.Location = new System.Drawing.Point(15, 303);
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
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
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
            // columnHeader5
            // 
            this.columnHeader5.Text = "Inspected";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Inspector";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "InspectionDate";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "InspectionReading";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "InspectionBackground";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Instrument";
            // 
            // comboBoxState
            // 
            this.comboBoxState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxState.FormattingEnabled = true;
            this.comboBoxState.Items.AddRange(new object[] {
            "GAS/VAPOR",
            "LIGHT LIQUID",
            "HEAVY LIQUID"});
            this.comboBoxState.Location = new System.Drawing.Point(401, 140);
            this.comboBoxState.Name = "comboBoxState";
            this.comboBoxState.Size = new System.Drawing.Size(287, 28);
            this.comboBoxState.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(334, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "State:";
            // 
            // textBoxDrawing
            // 
            this.textBoxDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDrawing.Location = new System.Drawing.Point(97, 140);
            this.textBoxDrawing.Name = "textBoxDrawing";
            this.textBoxDrawing.Size = new System.Drawing.Size(183, 26);
            this.textBoxDrawing.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(11, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "Drawing:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "AGITATOR",
            "COMPRESSOR",
            "CONNECTOR - ELBOW",
            "CONNECTOR - FLANGE",
            "CONNECTOR - PLUG",
            "CONNECTOR - SCREWED",
            "CONNECTOR - TEE",
            "PUMP",
            "VALVE - BALL",
            "VALVE - CONTROL",
            "VALVE - GATE",
            "VALVE - NEEDLE",
            "VALVE -CHECK"});
            this.comboBoxType.Location = new System.Drawing.Point(401, 12);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(288, 28);
            this.comboBoxType.Sorted = true;
            this.comboBoxType.TabIndex = 5;
            // 
            // comboBoxUnit
            // 
            this.comboBoxUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUnit.FormattingEnabled = true;
            this.comboBoxUnit.Location = new System.Drawing.Point(97, 12);
            this.comboBoxUnit.Name = "comboBoxUnit";
            this.comboBoxUnit.Size = new System.Drawing.Size(183, 28);
            this.comboBoxUnit.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 33;
            this.label2.Text = "Unit:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(420, 190);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 17);
            this.label10.TabIndex = 35;
            this.label10.Text = "Size:";
            // 
            // textBoxMOCNumber
            // 
            this.textBoxMOCNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMOCNumber.Location = new System.Drawing.Point(502, 228);
            this.textBoxMOCNumber.Name = "textBoxMOCNumber";
            this.textBoxMOCNumber.Size = new System.Drawing.Size(187, 26);
            this.textBoxMOCNumber.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(420, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 17);
            this.label11.TabIndex = 37;
            this.label11.Text = "MOC #:";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSize.Location = new System.Drawing.Point(501, 184);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(187, 26);
            this.textBoxSize.TabIndex = 9;
            this.textBoxSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSize_KeyPress);
            // 
            // buttonNextComponent
            // 
            this.buttonNextComponent.Location = new System.Drawing.Point(508, 506);
            this.buttonNextComponent.Name = "buttonNextComponent";
            this.buttonNextComponent.Size = new System.Drawing.Size(88, 33);
            this.buttonNextComponent.TabIndex = 14;
            this.buttonNextComponent.Text = "&New Tag";
            this.buttonNextComponent.UseVisualStyleBackColor = true;
            this.buttonNextComponent.Click += new System.EventHandler(this.buttonNextComponent_Click);
            // 
            // comboBoxStream
            // 
            this.comboBoxStream.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxStream.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStream.FormattingEnabled = true;
            this.comboBoxStream.Items.AddRange(new object[] {
            "VOC"});
            this.comboBoxStream.Location = new System.Drawing.Point(401, 98);
            this.comboBoxStream.Name = "comboBoxStream";
            this.comboBoxStream.Size = new System.Drawing.Size(287, 28);
            this.comboBoxStream.TabIndex = 38;
            // 
            // buttonSetBackground
            // 
            this.buttonSetBackground.Location = new System.Drawing.Point(218, 506);
            this.buttonSetBackground.Name = "buttonSetBackground";
            this.buttonSetBackground.Size = new System.Drawing.Size(108, 33);
            this.buttonSetBackground.TabIndex = 39;
            this.buttonSetBackground.Text = "&Set Background";
            this.buttonSetBackground.UseVisualStyleBackColor = true;
            this.buttonSetBackground.Click += new System.EventHandler(this.buttonSetBackground_Click);
            // 
            // buttonInspect
            // 
            this.buttonInspect.Location = new System.Drawing.Point(338, 506);
            this.buttonInspect.Name = "buttonInspect";
            this.buttonInspect.Size = new System.Drawing.Size(108, 33);
            this.buttonInspect.TabIndex = 40;
            this.buttonInspect.Text = "&Inspect";
            this.buttonInspect.UseVisualStyleBackColor = true;
            this.buttonInspect.Click += new System.EventHandler(this.buttonInspect_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(420, 272);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 17);
            this.label14.TabIndex = 46;
            this.label14.Text = "Inspection:";
            // 
            // labelInspection
            // 
            this.labelInspection.AutoSize = true;
            this.labelInspection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInspection.Location = new System.Drawing.Point(502, 274);
            this.labelInspection.Name = "labelInspection";
            this.labelInspection.Size = new System.Drawing.Size(74, 13);
            this.labelInspection.TabIndex = 47;
            this.labelInspection.Text = "Not Inspected";
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.Image = global::EnvInt.Win32.EiMOC.Properties.Resources.no_photo;
            this.pictureBoxPhoto.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxPhoto.InitialImage")));
            this.pictureBoxPhoto.Location = new System.Drawing.Point(97, 213);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(163, 88);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPhoto.TabIndex = 48;
            this.pictureBoxPhoto.TabStop = false;
            this.pictureBoxPhoto.Click += new System.EventHandler(this.pictureBoxPhoto_Click);
            // 
            // buttonRefreshOldTag
            // 
            this.buttonRefreshOldTag.Enabled = false;
            this.buttonRefreshOldTag.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshOldTag.Image")));
            this.buttonRefreshOldTag.Location = new System.Drawing.Point(286, 98);
            this.buttonRefreshOldTag.Name = "buttonRefreshOldTag";
            this.buttonRefreshOldTag.Size = new System.Drawing.Size(29, 28);
            this.buttonRefreshOldTag.TabIndex = 27;
            this.buttonRefreshOldTag.UseVisualStyleBackColor = true;
            this.buttonRefreshOldTag.Click += new System.EventHandler(this.buttonRefreshOldTag_Click);
            // 
            // buttonLocationBuilder
            // 
            this.buttonLocationBuilder.Image = global::EnvInt.Win32.EiMOC.Properties.Resources._26;
            this.buttonLocationBuilder.Location = new System.Drawing.Point(364, 184);
            this.buttonLocationBuilder.Name = "buttonLocationBuilder";
            this.buttonLocationBuilder.Size = new System.Drawing.Size(29, 28);
            this.buttonLocationBuilder.TabIndex = 12;
            this.buttonLocationBuilder.UseVisualStyleBackColor = true;
            this.buttonLocationBuilder.Click += new System.EventHandler(this.buttonLocationBuilder_Click);
            // 
            // FormMOC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(721, 561);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxPhoto);
            this.Controls.Add(this.labelInspection);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.buttonInspect);
            this.Controls.Add(this.buttonSetBackground);
            this.Controls.Add(this.comboBoxStream);
            this.Controls.Add(this.buttonNextComponent);
            this.Controls.Add(this.textBoxMOCNumber);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBoxUnit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.textBoxDrawing);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxState);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonRefreshOldTag);
            this.Controls.Add(this.groupBox1);
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
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(317, 337);
            this.Name = "FormMOC";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MOC Edit";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormEditObject_Load);
            this.contextMenuStripLocation.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonChildAdd;
        private System.Windows.Forms.Button buttonChildRemove;
        private System.Windows.Forms.ListView listViewChildren;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonRefreshOldTag;
        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDrawing;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.ComboBox comboBoxUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxMOCNumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Button buttonNextComponent;
        private System.Windows.Forms.ComboBox comboBoxStream;
        private System.Windows.Forms.Button buttonSetBackground;
        private System.Windows.Forms.Button buttonInspect;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelInspection;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}