namespace EnvInt.Win32.FieldTech
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
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonTag = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNonDrawingTag = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonViewTags = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOldTags = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorOldTags = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripComponentCounter = new EnvInt.Win32.FieldTech.Controls.ToolStripComponentCount();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonACADNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelDrawingNumber = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonACADPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonACADPalettes = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDrawHeavy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADFreehand = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRotate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADZoomWindow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADExtents = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonACADSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.transparentPanel1 = new EnvInt.Win32.FieldTech.Library.TransparentPanel();
            this.adrContainer = new EnvInt.Win32.FieldTech.ADR.ADRContainer();
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.timerAutoSave = new System.Windows.Forms.Timer(this.components);
            this.timerMarkupDelay = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenProject,
            this.toolStripButtonSave,
            this.toolStripButtonSaveAs,
            this.toolStripSeparator4,
            this.toolStripButtonTag,
            this.toolStripButtonNonDrawingTag,
            this.toolStripButtonViewTags,
            this.toolStripSeparator1,
            this.toolStripButtonOldTags,
            this.toolStripSeparatorOldTags,
            this.toolStripButtonSettings,
            this.toolStripComponentCounter,
            this.toolStripSeparator3,
            this.toolStripButtonACADNext,
            this.toolStripLabelDrawingNumber,
            this.toolStripButtonACADPrevious,
            this.toolStripSeparator5,
            this.toolStripButtonACADPalettes,
            this.toolStripButtonDrawHeavy,
            this.toolStripButtonACADFreehand,
            this.toolStripButtonSearch,
            this.toolStripButtonRotate,
            this.toolStripButtonACADZoomWindow,
            this.toolStripButtonACADExtents,
            this.toolStripButtonACADPan,
            this.toolStripButtonACADSelect,
            this.toolStripButtonHelp});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButtonOpenProject
            // 
            this.toolStripButtonOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonOpenProject, "toolStripButtonOpenProject");
            this.toolStripButtonOpenProject.Name = "toolStripButtonOpenProject";
            this.toolStripButtonOpenProject.Click += new System.EventHandler(this.toolStripButtonOpenProject_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSave, "toolStripButtonSave");
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSaveAs, "toolStripButtonSaveAs");
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripButtonTag
            // 
            this.toolStripButtonTag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonTag, "toolStripButtonTag");
            this.toolStripButtonTag.Name = "toolStripButtonTag";
            this.toolStripButtonTag.Click += new System.EventHandler(this.toolStripButtonTag_Click);
            // 
            // toolStripButtonNonDrawingTag
            // 
            this.toolStripButtonNonDrawingTag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonNonDrawingTag, "toolStripButtonNonDrawingTag");
            this.toolStripButtonNonDrawingTag.Name = "toolStripButtonNonDrawingTag";
            this.toolStripButtonNonDrawingTag.Click += new System.EventHandler(this.toolStripButtonNonDrawingTag_Click);
            // 
            // toolStripButtonViewTags
            // 
            this.toolStripButtonViewTags.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonViewTags, "toolStripButtonViewTags");
            this.toolStripButtonViewTags.Name = "toolStripButtonViewTags";
            this.toolStripButtonViewTags.Click += new System.EventHandler(this.toolStripButtonViewTags_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripButtonOldTags
            // 
            this.toolStripButtonOldTags.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonOldTags, "toolStripButtonOldTags");
            this.toolStripButtonOldTags.Name = "toolStripButtonOldTags";
            this.toolStripButtonOldTags.Click += new System.EventHandler(this.toolStripButtonOldTags_Click);
            // 
            // toolStripSeparatorOldTags
            // 
            this.toolStripSeparatorOldTags.Name = "toolStripSeparatorOldTags";
            resources.ApplyResources(this.toolStripSeparatorOldTags, "toolStripSeparatorOldTags");
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSettings, "toolStripButtonSettings");
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // toolStripComponentCounter
            // 
            this.toolStripComponentCounter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComponentCounter.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStripComponentCounter.ChildCount = 0;
            this.toolStripComponentCounter.Name = "toolStripComponentCounter";
            this.toolStripComponentCounter.ParentCount = 0;
            resources.ApplyResources(this.toolStripComponentCounter, "toolStripComponentCounter");
            this.toolStripComponentCounter.Click += new System.EventHandler(this.toolStripComponentCounter_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripButtonACADNext
            // 
            this.toolStripButtonACADNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADNext, "toolStripButtonACADNext");
            this.toolStripButtonACADNext.Name = "toolStripButtonACADNext";
            this.toolStripButtonACADNext.Click += new System.EventHandler(this.toolStripButtonACADNext_Click);
            // 
            // toolStripLabelDrawingNumber
            // 
            this.toolStripLabelDrawingNumber.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelDrawingNumber.Name = "toolStripLabelDrawingNumber";
            resources.ApplyResources(this.toolStripLabelDrawingNumber, "toolStripLabelDrawingNumber");
            this.toolStripLabelDrawingNumber.Click += new System.EventHandler(this.toolStripLabelDrawingNumber_Click);
            // 
            // toolStripButtonACADPrevious
            // 
            this.toolStripButtonACADPrevious.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADPrevious, "toolStripButtonACADPrevious");
            this.toolStripButtonACADPrevious.Name = "toolStripButtonACADPrevious";
            this.toolStripButtonACADPrevious.Click += new System.EventHandler(this.toolStripButtonACADPrevious_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripButtonACADPalettes
            // 
            this.toolStripButtonACADPalettes.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADPalettes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADPalettes, "toolStripButtonACADPalettes");
            this.toolStripButtonACADPalettes.Name = "toolStripButtonACADPalettes";
            this.toolStripButtonACADPalettes.Click += new System.EventHandler(this.toolStripButtonACADPalettes_Click);
            // 
            // toolStripButtonDrawHeavy
            // 
            this.toolStripButtonDrawHeavy.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonDrawHeavy.CheckOnClick = true;
            this.toolStripButtonDrawHeavy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonDrawHeavy, "toolStripButtonDrawHeavy");
            this.toolStripButtonDrawHeavy.Name = "toolStripButtonDrawHeavy";
            this.toolStripButtonDrawHeavy.Click += new System.EventHandler(this.toolStripButtonDrawHeavy_Click);
            // 
            // toolStripButtonACADFreehand
            // 
            this.toolStripButtonACADFreehand.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADFreehand.CheckOnClick = true;
            this.toolStripButtonACADFreehand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADFreehand, "toolStripButtonACADFreehand");
            this.toolStripButtonACADFreehand.Name = "toolStripButtonACADFreehand";
            this.toolStripButtonACADFreehand.Click += new System.EventHandler(this.toolStripButtonACADFreehand_Click);
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSearch, "toolStripButtonSearch");
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
            // 
            // toolStripButtonRotate
            // 
            this.toolStripButtonRotate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonRotate, "toolStripButtonRotate");
            this.toolStripButtonRotate.Name = "toolStripButtonRotate";
            this.toolStripButtonRotate.Click += new System.EventHandler(this.toolStripButtonACADRotate_Click);
            // 
            // toolStripButtonACADZoomWindow
            // 
            this.toolStripButtonACADZoomWindow.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADZoomWindow.CheckOnClick = true;
            this.toolStripButtonACADZoomWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADZoomWindow, "toolStripButtonACADZoomWindow");
            this.toolStripButtonACADZoomWindow.Name = "toolStripButtonACADZoomWindow";
            this.toolStripButtonACADZoomWindow.Click += new System.EventHandler(this.toolStripButtonACADZoomWindow_Click);
            // 
            // toolStripButtonACADExtents
            // 
            this.toolStripButtonACADExtents.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADExtents.CheckOnClick = true;
            this.toolStripButtonACADExtents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADExtents, "toolStripButtonACADExtents");
            this.toolStripButtonACADExtents.Name = "toolStripButtonACADExtents";
            this.toolStripButtonACADExtents.Click += new System.EventHandler(this.toolStripButtonACADExtents_Click);
            // 
            // toolStripButtonACADPan
            // 
            this.toolStripButtonACADPan.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADPan.CheckOnClick = true;
            this.toolStripButtonACADPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADPan, "toolStripButtonACADPan");
            this.toolStripButtonACADPan.Name = "toolStripButtonACADPan";
            this.toolStripButtonACADPan.Click += new System.EventHandler(this.toolStripButtonACADPan_Click);
            // 
            // toolStripButtonACADSelect
            // 
            this.toolStripButtonACADSelect.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonACADSelect.CheckOnClick = true;
            this.toolStripButtonACADSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonACADSelect, "toolStripButtonACADSelect");
            this.toolStripButtonACADSelect.Name = "toolStripButtonACADSelect";
            this.toolStripButtonACADSelect.Click += new System.EventHandler(this.toolStripButtonACADSelect_Click);
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonHelp, "toolStripButtonHelp");
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.transparentPanel1);
            this.panelMain.Controls.Add(this.adrContainer);
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            // 
            // transparentPanel1
            // 
            resources.ApplyResources(this.transparentPanel1, "transparentPanel1");
            this.transparentPanel1.Name = "transparentPanel1";
            // 
            // adrContainer
            // 
            resources.ApplyResources(this.adrContainer, "adrContainer");
            this.adrContainer.Name = "adrContainer";
            // 
            // timerSplash
            // 
            this.timerSplash.Interval = 500;
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // timerAutoSave
            // 
            this.timerAutoSave.Interval = 1200000;
            this.timerAutoSave.Tick += new System.EventHandler(this.timerAutoSave_Tick);
            // 
            // timerMarkupDelay
            // 
            this.timerMarkupDelay.Interval = 1000;
            this.timerMarkupDelay.Tick += new System.EventHandler(this.timerMarkupDelay_Tick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButtonOpenProject;
    private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
    private System.Windows.Forms.Panel panelMain;
    private System.Windows.Forms.ToolStripButton toolStripButtonTag;
    private System.Windows.Forms.Timer timerSplash;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolStripButtonNonDrawingTag;
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
    private System.Windows.Forms.ToolStripButton toolStripButtonACADPalettes;
    private ADR.ADRContainer adrContainer;
    private System.Windows.Forms.ToolStripButton toolStripButtonACADZoomWindow;
    private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
    private System.Windows.Forms.ToolStripButton toolStripButtonRotate;
    private System.Windows.Forms.ToolStripButton toolStripButtonDrawHeavy;
    private System.Windows.Forms.ToolStripButton toolStripButtonSave;
    private System.Windows.Forms.Timer timerAutoSave;
    private Library.TransparentPanel transparentPanel1;
    private System.Windows.Forms.Timer timerMarkupDelay;
  }
}

