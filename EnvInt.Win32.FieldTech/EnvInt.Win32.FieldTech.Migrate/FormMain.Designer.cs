namespace EnvInt.Win32.FieldTech.Migrate
{
    partial class FormMain
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
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Local Data", 13, 13);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("QC Checks");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Reports");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewFunctions = new System.Windows.Forms.TreeView();
            this.imageListFunctions = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStripCloseTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonEditConnections = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenData = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditRecipes = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCreateLocalDB = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportEid = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelOpenedFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripButtonMakeEIP = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripCloseTabs.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewFunctions);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(839, 407);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeViewFunctions
            // 
            this.treeViewFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFunctions.ImageIndex = 0;
            this.treeViewFunctions.ImageList = this.imageListFunctions;
            this.treeViewFunctions.Location = new System.Drawing.Point(0, 0);
            this.treeViewFunctions.Name = "treeViewFunctions";
            treeNode4.ImageIndex = 13;
            treeNode4.Name = "LocalData";
            treeNode4.SelectedImageIndex = 13;
            treeNode4.Text = "Local Data";
            treeNode5.ImageKey = "edit-find.png";
            treeNode5.Name = "QCChecks";
            treeNode5.SelectedImageKey = "edit-find.png";
            treeNode5.Text = "QC Checks";
            treeNode6.ImageKey = "x-office-document.png";
            treeNode6.Name = "Reports";
            treeNode6.SelectedImageKey = "x-office-document.png";
            treeNode6.Text = "Reports";
            this.treeViewFunctions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeViewFunctions.SelectedImageIndex = 0;
            this.treeViewFunctions.Size = new System.Drawing.Size(224, 407);
            this.treeViewFunctions.TabIndex = 0;
            this.treeViewFunctions.DoubleClick += new System.EventHandler(this.treeViewFunctions_DoubleClick);
            // 
            // imageListFunctions
            // 
            this.imageListFunctions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFunctions.ImageStream")));
            this.imageListFunctions.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListFunctions.Images.SetKeyName(0, "document-new.png");
            this.imageListFunctions.Images.SetKeyName(1, "document-open.png");
            this.imageListFunctions.Images.SetKeyName(2, "document-properties.png");
            this.imageListFunctions.Images.SetKeyName(3, "document-save.png");
            this.imageListFunctions.Images.SetKeyName(4, "edit-find.png");
            this.imageListFunctions.Images.SetKeyName(5, "help-about.png");
            this.imageListFunctions.Images.SetKeyName(6, "libreoffice-oasis-database.png");
            this.imageListFunctions.Images.SetKeyName(7, "libreoffice-oasis-spreadsheet.png");
            this.imageListFunctions.Images.SetKeyName(8, "list-add.png");
            this.imageListFunctions.Images.SetKeyName(9, "view-refresh.png");
            this.imageListFunctions.Images.SetKeyName(10, "view-sort-ascending.png");
            this.imageListFunctions.Images.SetKeyName(11, "view-sort-descending.png");
            this.imageListFunctions.Images.SetKeyName(12, "x-office-document.png");
            this.imageListFunctions.Images.SetKeyName(13, "x-office-spreadsheet.png");
            // 
            // tabControl1
            // 
            this.tabControl1.ContextMenuStrip = this.contextMenuStripCloseTabs;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(611, 407);
            this.tabControl1.TabIndex = 0;
            // 
            // contextMenuStripCloseTabs
            // 
            this.contextMenuStripCloseTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStripCloseTabs.Name = "contextMenuStripCloseTabs";
            this.contextMenuStripCloseTabs.Size = new System.Drawing.Size(104, 26);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(839, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageConnectionsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // manageConnectionsToolStripMenuItem
            // 
            this.manageConnectionsToolStripMenuItem.Name = "manageConnectionsToolStripMenuItem";
            this.manageConnectionsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.manageConnectionsToolStripMenuItem.Text = "&Manage Connections";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEditConnections,
            this.toolStripButtonOpenData,
            this.toolStripButtonEditRecipes,
            this.toolStripButtonCreateLocalDB,
            this.toolStripButtonImportEid,
            this.toolStripButtonMakeEIP});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(839, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonEditConnections
            // 
            this.toolStripButtonEditConnections.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.list_add;
            this.toolStripButtonEditConnections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditConnections.Name = "toolStripButtonEditConnections";
            this.toolStripButtonEditConnections.Size = new System.Drawing.Size(117, 22);
            this.toolStripButtonEditConnections.Text = "Edit Connections";
            this.toolStripButtonEditConnections.Click += new System.EventHandler(this.toolStripButtonEditConnections_Click);
            // 
            // toolStripButtonOpenData
            // 
            this.toolStripButtonOpenData.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.document_open;
            this.toolStripButtonOpenData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenData.Name = "toolStripButtonOpenData";
            this.toolStripButtonOpenData.Size = new System.Drawing.Size(104, 22);
            this.toolStripButtonOpenData.Text = "Open Data File";
            this.toolStripButtonOpenData.Click += new System.EventHandler(this.toolStripButtonOpenData_Click);
            // 
            // toolStripButtonEditRecipes
            // 
            this.toolStripButtonEditRecipes.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.document_properties;
            this.toolStripButtonEditRecipes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditRecipes.Name = "toolStripButtonEditRecipes";
            this.toolStripButtonEditRecipes.Size = new System.Drawing.Size(90, 22);
            this.toolStripButtonEditRecipes.Text = "Edit Recipes";
            this.toolStripButtonEditRecipes.Click += new System.EventHandler(this.toolStripButtonEditRecipes_Click);
            // 
            // toolStripButtonCreateLocalDB
            // 
            this.toolStripButtonCreateLocalDB.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.document_new;
            this.toolStripButtonCreateLocalDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateLocalDB.Name = "toolStripButtonCreateLocalDB";
            this.toolStripButtonCreateLocalDB.Size = new System.Drawing.Size(143, 22);
            this.toolStripButtonCreateLocalDB.Text = "Create Local Database";
            this.toolStripButtonCreateLocalDB.Click += new System.EventHandler(this.toolStripButtonCreateLocalDB_Click);
            // 
            // toolStripButtonImportEid
            // 
            this.toolStripButtonImportEid.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.document_save;
            this.toolStripButtonImportEid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportEid.Name = "toolStripButtonImportEid";
            this.toolStripButtonImportEid.Size = new System.Drawing.Size(82, 22);
            this.toolStripButtonImportEid.Text = "Import EIP";
            this.toolStripButtonImportEid.Click += new System.EventHandler(this.toolStripButtonImportEid_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelOpenedFile});
            this.statusStrip1.Location = new System.Drawing.Point(0, 462);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(839, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // toolStripStatusLabelOpenedFile
            // 
            this.toolStripStatusLabelOpenedFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabelOpenedFile.Name = "toolStripStatusLabelOpenedFile";
            this.toolStripStatusLabelOpenedFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabelOpenedFile.Size = new System.Drawing.Size(680, 17);
            this.toolStripStatusLabelOpenedFile.Spring = true;
            this.toolStripStatusLabelOpenedFile.Text = "No File Open";
            this.toolStripStatusLabelOpenedFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripButtonMakeEIP
            // 
            this.toolStripButtonMakeEIP.Image = global::EnvInt.Win32.FieldTech.Migrate.Properties.Resources.help_about;
            this.toolStripButtonMakeEIP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMakeEIP.Name = "toolStripButtonMakeEIP";
            this.toolStripButtonMakeEIP.Size = new System.Drawing.Size(80, 22);
            this.toolStripButtonMakeEIP.Text = "Create EIP";
            this.toolStripButtonMakeEIP.Click += new System.EventHandler(this.toolStripButtonMakeEIP_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 484);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "FieldTech Migrate";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripCloseTabs.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView treeViewFunctions;
        private System.Windows.Forms.ImageList imageListFunctions;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditConnections;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenData;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditRecipes;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateLocalDB;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelOpenedFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCloseTabs;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportEid;
        private System.Windows.Forms.ToolStripButton toolStripButtonMakeEIP;

    }
}

