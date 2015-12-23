namespace EnvInt.Win32.EiMOC
{
  partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMOC = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonViewTags = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOldTags = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorOldTags = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComponentCounter = new EnvInt.Win32.EiMOC.Controls.ToolStripComponentCount();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonACADNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelDrawingNumber = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonACADPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonACADPalettes = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADFreehand = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADZoomWindow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADExtents = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADSelect = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.adrContainer = new EnvInt.Win32.FieldTech.ADR.ADRContainer();
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenProject,
            this.toolStripButtonMOC,
            this.toolStripButtonViewTags,
            this.toolStripSeparator4,
            this.toolStripButtonExport,
            this.toolStripSeparator1,
            this.toolStripButtonOldTags,
            this.toolStripSeparatorOldTags,
            this.toolStripButtonSettings,
            this.toolStripSeparator6,
            this.toolStripComponentCounter,
            this.toolStripSeparator5,
            this.toolStripButtonACADNext,
            this.toolStripLabelDrawingNumber,
            this.toolStripButtonACADPrevious,
            this.toolStripSeparator3,
            this.toolStripButtonACADPalettes,
            this.toolStripButtonACADFreehand,
            this.toolStripButtonSearch,
            this.toolStripButtonACADZoomWindow,
            this.toolStripButtonACADExtents,
            this.toolStripButtonACADPan,
            this.toolStripButtonACADSelect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1129, 55);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpenProject
            // 
            this.toolStripButtonOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenProject.Image")));
            this.toolStripButtonOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenProject.Name = "toolStripButtonOpenProject";
            this.toolStripButtonOpenProject.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonOpenProject.Text = "Open Drawing Project";
            this.toolStripButtonOpenProject.Click += new System.EventHandler(this.toolStripButtonOpenProject_Click);
            // 
            // toolStripButtonMOC
            // 
            this.toolStripButtonMOC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMOC.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMOC.Image")));
            this.toolStripButtonMOC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMOC.Name = "toolStripButtonMOC";
            this.toolStripButtonMOC.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonMOC.Text = "MOC";
            this.toolStripButtonMOC.Click += new System.EventHandler(this.toolStripButtonMOC_Click);
            // 
            // toolStripButtonViewTags
            // 
            this.toolStripButtonViewTags.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonViewTags.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonViewTags.Image")));
            this.toolStripButtonViewTags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonViewTags.Name = "toolStripButtonViewTags";
            this.toolStripButtonViewTags.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonViewTags.Text = "View all Tagged Components";
            this.toolStripButtonViewTags.Click += new System.EventHandler(this.toolStripButtonViewTags_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExport.Image")));
            this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonExport.Text = "Export All Tagged Info";
            this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripButtonOldTags
            // 
            this.toolStripButtonOldTags.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOldTags.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOldTags.Image")));
            this.toolStripButtonOldTags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOldTags.Name = "toolStripButtonOldTags";
            this.toolStripButtonOldTags.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonOldTags.Text = "Tag History";
            this.toolStripButtonOldTags.Click += new System.EventHandler(this.toolStripButtonOldTags_Click);
            // 
            // toolStripSeparatorOldTags
            // 
            this.toolStripSeparatorOldTags.Name = "toolStripSeparatorOldTags";
            this.toolStripSeparatorOldTags.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSettings.Image")));
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonSettings.Text = "Settings";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripComponentCounter
            // 
            this.toolStripComponentCounter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComponentCounter.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStripComponentCounter.ChildCount = 0;
            this.toolStripComponentCounter.Name = "toolStripComponentCounter";
            this.toolStripComponentCounter.ParentCount = 0;
            this.toolStripComponentCounter.Size = new System.Drawing.Size(162, 52);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripButtonACADNext
            // 
            this.toolStripButtonACADNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADNext.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonACADNext.Image")));
            this.toolStripButtonACADNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADNext.Name = "toolStripButtonACADNext";
            this.toolStripButtonACADNext.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADNext.Text = "toolStripButton5";
            this.toolStripButtonACADNext.Click += new System.EventHandler(this.toolStripButtonACADNext_Click);
            // 
            // toolStripLabelDrawingNumber
            // 
            this.toolStripLabelDrawingNumber.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelDrawingNumber.Name = "toolStripLabelDrawingNumber";
            this.toolStripLabelDrawingNumber.Size = new System.Drawing.Size(36, 52);
            this.toolStripLabelDrawingNumber.Text = "0 of 0";
            // 
            // toolStripButtonACADPrevious
            // 
            this.toolStripButtonACADPrevious.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADPrevious.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonACADPrevious.Image")));
            this.toolStripButtonACADPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADPrevious.Name = "toolStripButtonACADPrevious";
            this.toolStripButtonACADPrevious.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADPrevious.Text = "toolStripButton4";
            this.toolStripButtonACADPrevious.Click += new System.EventHandler(this.toolStripButtonACADPrevious_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripButtonACADPalettes
            // 
            this.toolStripButtonACADPalettes.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADPalettes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADPalettes.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonACADPalettes.Image")));
            this.toolStripButtonACADPalettes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADPalettes.Name = "toolStripButtonACADPalettes";
            this.toolStripButtonACADPalettes.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADPalettes.Text = "Show/Hide Palettes";
            this.toolStripButtonACADPalettes.ToolTipText = "Hide All Palettes";
            this.toolStripButtonACADPalettes.Click += new System.EventHandler(this.toolStripButtonACADPalettes_Click);
            // 
            // toolStripButtonACADFreehand
            // 
            this.toolStripButtonACADFreehand.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADFreehand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADFreehand.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonACADFreehand.Image")));
            this.toolStripButtonACADFreehand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADFreehand.Name = "toolStripButtonACADFreehand";
            this.toolStripButtonACADFreehand.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADFreehand.Text = "toolStripButton1";
            this.toolStripButtonACADFreehand.ToolTipText = "Freehand Tool";
            this.toolStripButtonACADFreehand.Click += new System.EventHandler(this.toolStripButtonACADFreehand_Click);
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSearch.Image")));
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonSearch.Text = "Find";
            this.toolStripButtonSearch.ToolTipText = "Find Components";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
            // 
            // toolStripButtonACADZoomWindow
            // 
            this.toolStripButtonACADZoomWindow.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADZoomWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADZoomWindow.Image = global::EnvInt.Win32.EiMOC.Properties.Resources.rectangle_zoom_64;
            this.toolStripButtonACADZoomWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADZoomWindow.Name = "toolStripButtonACADZoomWindow";
            this.toolStripButtonACADZoomWindow.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADZoomWindow.Text = "Zoom Window";
            this.toolStripButtonACADZoomWindow.Click += new System.EventHandler(this.toolStripButtonACADZoomWindow_Click);
            // 
            // toolStripButtonACADExtents
            // 
            this.toolStripButtonACADExtents.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADExtents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADExtents.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonACADExtents.Image")));
            this.toolStripButtonACADExtents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADExtents.Name = "toolStripButtonACADExtents";
            this.toolStripButtonACADExtents.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADExtents.Text = "toolStripButton4";
            this.toolStripButtonACADExtents.Click += new System.EventHandler(this.toolStripButtonACADExtents_Click);
            // 
            // toolStripButtonACADPan
            // 
            this.toolStripButtonACADPan.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADPan.CheckOnClick = true;
            this.toolStripButtonACADPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADPan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonACADPan.Image")));
            this.toolStripButtonACADPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADPan.Name = "toolStripButtonACADPan";
            this.toolStripButtonACADPan.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADPan.Text = "toolStripButton5";
            this.toolStripButtonACADPan.ToolTipText = "Pan";
            this.toolStripButtonACADPan.Click += new System.EventHandler(this.toolStripButtonACADPan_Click);
            // 
            // toolStripButtonACADSelect
            // 
            this.toolStripButtonACADSelect.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADSelect.CheckOnClick = true;
            this.toolStripButtonACADSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonACADSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonACADSelect.Image")));
            this.toolStripButtonACADSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonACADSelect.Name = "toolStripButtonACADSelect";
            this.toolStripButtonACADSelect.Size = new System.Drawing.Size(52, 52);
            this.toolStripButtonACADSelect.Text = "toolStripButton4";
            this.toolStripButtonACADSelect.ToolTipText = "Select Component";
            this.toolStripButtonACADSelect.Click += new System.EventHandler(this.toolStripButtonACADSelect_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.adrContainer);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 55);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1129, 462);
            this.panelMain.TabIndex = 11;
            // 
            // adrContainer
            // 
            this.adrContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adrContainer.Location = new System.Drawing.Point(0, 0);
            this.adrContainer.Name = "adrContainer";
            this.adrContainer.Size = new System.Drawing.Size(1129, 462);
            this.adrContainer.TabIndex = 0;
            // 
            // timerSplash
            // 
            this.timerSplash.Interval = 500;
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 517);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "EiMOC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButtonOpenProject;
    private System.Windows.Forms.ToolStripButton toolStripButtonExport;
    private System.Windows.Forms.Panel panelMain;
    private System.Windows.Forms.Timer timerSplash;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
    private System.Windows.Forms.ToolStripButton toolStripButtonViewTags;
    private System.Windows.Forms.ToolStripButton toolStripButtonOldTags;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparatorOldTags;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADSelect;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADPan;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADExtents;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADPrevious;
    private System.Windows.Forms.ToolStripLabel toolStripLabelDrawingNumber;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADNext;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADFreehand;
    private Controls.ToolStripComponentCount toolStripComponentCounter;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADPalettes;
    private EnvInt.Win32.FieldTech.ADR.ADRContainer adrContainer;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADZoomWindow;
    private System.Windows.Forms.ToolStripButton toolStripButtonMOC;
  }
}

