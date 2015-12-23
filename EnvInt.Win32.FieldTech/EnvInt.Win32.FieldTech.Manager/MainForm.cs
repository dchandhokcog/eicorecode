using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common.Containers;
using EnvInt.Win32.FieldTech.HelperClasses;
using EnvInt.Win32.FieldTech.Manager.Dialogs;
using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Manager.DataSets;
using Ionic.Zip;
using Telerik.WinControls.UI;


namespace EnvInt.Win32.FieldTech.Manager
{
    /// <summary>
    /// Main Form Class. 
    /// \image html resource/folder_yellow_save_321.png 
    /// </summary>
    public partial class MainForm : RadRibbonForm
    {
        private const string _applicationTitle = "FieldTech Manager";
        private BackgroundWorker _worker = null;
        private ProgressDialog _progressDialog = null;
        private ProgressDialogSimple _progressDialogSimple = null;
        private BackgroundWorker _dbChangeWorker = new BackgroundWorker();
        private BackgroundWorker _LoadProjectWorker = new BackgroundWorker();
        private BackgroundWorker _SaveProjectWorker = new BackgroundWorker();

        public static ProjectData CurrentProjectData = null;
        public static List<ProjectTags> CurrentProjectTags = new List<ProjectTags>();
        public static List<TaggedComponent> CollectedTags = new List<TaggedComponent>();
        public static ProjectTags importErrors = new ProjectTags();
        public static bool CurrentProjectDirty = false;
        public static bool FileMessageBoxAlreadyShowing = false;
        public static string _workingFile = "";
        public static LDARData _previousLDARData = new LDARData();
        public static List<string> _eidImportList = new List<string>();

        /// <summary>
        /// Main Function of the Home page.
        /// Initialize all default settings. 
        /// </summary>
        public MainForm()
        {
            // Call Initialze Component function
            InitializeComponent();

            // Show application name and version on the title bar of the screen
            this.radRibbonBarMain.Text = _applicationTitle + " " + Application.ProductVersion.ToString();
            radPageViewMain.SelectedPage = radPageViewMain.Pages[radPageViewMain.Pages.Count - 1];

            //THIS HIDES THE TITLE BAR AND TABS ON THE NAV PAGE VIEW AND THE TAB PAGE VIEW
            RadPageViewStackElement explorerElement = this.radPageViewMain.ViewElement as RadPageViewStackElement;
            if (explorerElement != null && explorerElement.Children.Count > 0)
            {
                RadPageViewLabelElement labelElement = explorerElement.Children[1] as RadPageViewLabelElement;
                //labelElement.Text = "Project Name";
                labelElement.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            }

            RadPageViewStripElement el = radPageViewProject.ViewElement as RadPageViewStripElement;
            el.ItemContainer.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

            _dbChangeWorker.DoWork += new DoWorkEventHandler(_dbChangeWorker_DoWork);
            _dbChangeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_dbChangeWorker_RunWorkerCompleted);
            _SaveProjectWorker.DoWork += _SaveProjectWorker_DoWork;
            _SaveProjectWorker.RunWorkerCompleted += _SaveProjectWorker_RunWorkerCompleted;
            _LoadProjectWorker.DoWork += _LoadProjectWorker_DoWork;
            _LoadProjectWorker.RunWorkerCompleted += _LoadProjectWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// This event will be run on Completion of "Load Project Background Worker" Process. 
        /// It will set Current Project and initiate "DB Change Worker" Process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _LoadProjectWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetCurrentProject();
            //run background worker that looks for target database changes
            radButtonElementLeakDASImport.Enabled = false;
            _dbChangeWorker.RunWorkerAsync();
            _progressDialogSimple.Close();
            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>
        /// This event will load the Project.
        /// Extract all files and load Tags and collected Tags.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _LoadProjectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (ZipFile zip = ZipFile.Read(e.Argument.ToString()))
            {
                FileUtilities.CheckProjectVersion(zip, new Version(Application.ProductVersion));
                resetWorkingFolder(false);
                zip.ExtractAll(Globals.WorkingFolder);
                radPageViewMain.SelectedPage = radPageViewMain.Pages[radPageViewMain.Pages.Count - 1];

                //string projectJson = FileUtilities.GetZipEntryAsText(zip, "project.json");
                string tagsJson = FileUtilities.GetZipEntryAsText(zip, "tags.json");
                string tagsCollected = FileUtilities.GetZipEntryAsText(zip, "collectedtags.json");
                //projectJson = File.ReadAllText(Globals.WorkingFolder+"\\project.json", Encoding.UTF8);
                //CurrentProjectData = FileUtilities.DeserializeObject<ProjectData>(projectJson);
                CurrentProjectData = FileUtilities.DeserializeProjectData(Globals.WorkingFolder + "\\project.json");
                CurrentProjectTags = FileUtilities.DeserializeObject<List<ProjectTags>>(tagsJson);
                CollectedTags = FileUtilities.DeserializeObject<List<TaggedComponent>>(tagsCollected);
                if (CollectedTags == null) CollectedTags = new List<TaggedComponent>();
                //FileUtilities.DeserializeObject<
            }
        }

        /// <summary>
        /// This event will run after the completion of Save Project process.
        /// It will show the Result message and dispose the Progress bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _SaveProjectWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Arrow;
            saveFileWorkerFinishedArgs fArgs = (saveFileWorkerFinishedArgs)e.Result;

            if (!fArgs.saveFileSuccess)
            {
                MessageBox.Show(fArgs.saveFileMessage);
            }

            _progressDialogSimple.Close();
        }

        /// <summary>
        /// Saves the Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _SaveProjectWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            saveFileWorkerArguments workerArgs = (saveFileWorkerArguments)e.Argument;
            saveFileWorkerFinishedArgs finishArgs = new saveFileWorkerFinishedArgs();
            finishArgs.saveFileSuccess = false;
            finishArgs.saveFileMessage = string.Empty;

            try
            {
                //package up our project file here...
                using (ZipFile zip = new ZipFile())
                {
                    string projectDataSer = string.Empty;
                    string projectTagSer;
                    if (workerArgs.selectedTags == null)
                    {
                        projectTagSer = FileUtilities.SerializeObject(CurrentProjectTags);
                    }
                    else
                    {
                        projectTagSer = FileUtilities.SerializeObject(workerArgs.selectedTags);
                    }

                    string projectCollectedTags = FileUtilities.SerializeObject(CollectedTags);

                    //--------------------------------------------------------
                    //TODO: This is currently a hack because we are still using JSON. Once we switch over to CSV files for each LDAR table
                    //then this will become a bit cleaner since the LDAR data will be saved in seperate files, it will be one step easier
                    //I need to deserialize this and then reserialize it because that is the only way to make a deep copy where I can delete filtered
                    //components, otherwise, I would delete them in the main ProjectData which would be very bad.
                    //ProjectData localProjectData = FileUtilities.DeserializeObject<ProjectData>(projectDataSer);
                    ProjectData localProjectData = new ProjectData(ref CurrentProjectData, null, null);
                    //--------------------------------------------------------
                    if (workerArgs.ldarComponentsProcessUnitFilter != null)
                    {
                        if (CurrentProjectData.LDARDatabaseType == "LeakDAS")
                        {
                            List<int> validProcessUnits;
                            validProcessUnits = localProjectData.LDARData.ProcessUnits.Where(u => workerArgs.ldarComponentsProcessUnitFilter.Contains(u.UnitDescription)).Select(p => p.ProcessUnitId).ToList();

                            //if we index and go backwards, we can delete by index and not ruin things, plus this is super fast
                            for (int i = localProjectData.LDARData.ExistingComponents.Count - 1; i >= 0; i--)
                            {
                                if (!validProcessUnits.Contains(localProjectData.LDARData.ExistingComponents[i].ProcessUnitId))
                                {
                                    localProjectData.LDARData.ExistingComponents.RemoveAt(i);
                                }
                            }
                        }
                        else
                        {
                            //localProjectData.LDARData.ExistingComponents.RemoveAll(c => !ldarComponentsProcessUnitFilter.Contains(c.Location1));
                            List<string> validProcessUnits;
                            validProcessUnits = localProjectData.LDARData.ProcessUnits.Where(u => workerArgs.ldarComponentsProcessUnitFilter.Contains(u.UnitDescription)).Select(p => p.UnitCode).ToList();

                            for (int i = localProjectData.LDARData.ExistingComponents.Count - 1; i >= 0; i--)
                            {

                                if (!validProcessUnits.Contains(localProjectData.LDARData.ExistingComponents[i].Location1))
                                {
                                    localProjectData.LDARData.ExistingComponents.RemoveAt(i);
                                }
                            }
                        }

                    }
                    if (workerArgs.drawingPackageFilter != null)
                    {
                        for (int i = localProjectData.CADPackages.Count - 1; i >= 0; i--)
                        {
                            if (localProjectData.CADPackages[i].LocalName == "" || localProjectData.CADPackages[i].LocalName == null)
                            {
                                if (!workerArgs.drawingPackageFilter.Contains(Path.GetFileName(localProjectData.CADPackages[i].FileName)))
                                {
                                    localProjectData.CADPackages.RemoveAt(i);
                                }
                            }
                            else
                            {
                                if (!workerArgs.drawingPackageFilter.Contains(Path.GetFileName(localProjectData.CADPackages[i].LocalName)))
                                {
                                    localProjectData.CADPackages.RemoveAt(i);
                                }
                            }
                        }
                    }
                    //now add to the file, the one we filtered
                    //projectDataSer = FileUtilities.SerializeObject(localProjectData);
                    FileUtilities.SerializeProjectData(ref localProjectData, Globals.WorkingFolder + "\\projecttmp.json");
                    FileStream pdStream = new FileStream(Globals.WorkingFolder + "\\projecttmp.json", FileMode.OpenOrCreate);

                    //--------------------------------------------------------

                    //ZipEntry e1 = zip.AddEntry("project.json", projectDataSer, Encoding.UTF8);
                    ZipEntry e1 = zip.AddEntry("project.json", pdStream);
                    ZipEntry e2 = zip.AddEntry("tags.json", projectTagSer, Encoding.UTF8);
                    if (!workerArgs.ignoreMarkedDrawings) zip.AddEntry("collectedtags.json", projectCollectedTags, Encoding.UTF8);
                    ZipEntry e3 = zip.AddEntry("version.txt", "Version=" + Application.ProductVersion.ToString(), Encoding.UTF8);
                    ZipEntry e4 = zip.AddEntry("project.id", CurrentProjectData.ProjectId.ToString(), Encoding.UTF8);
                    if (workerArgs.selectedTags != null)
                    {
                        ZipEntry e5 = zip.AddEntry("tags.csv", FileUtilities.GetTaggedComponentsAsCSV(workerArgs.selectedTags), Encoding.UTF8);
                    }
                    if (Directory.Exists(Globals.WorkingFolder + "\\MarkedDrawings") && !workerArgs.ignoreMarkedDrawings)
                    {
                        foreach (CADPackage markedDrawing in CurrentProjectData.MarkedDrawings)
                        {
                            if (File.Exists(Globals.WorkingFolder + "\\MarkedDrawings\\" + System.IO.Path.GetFileName(markedDrawing.FileName)))
                            {
                                //grab file from working directory, not original
                                zip.AddFile(Globals.WorkingFolder + "\\MarkedDrawings\\" + System.IO.Path.GetFileName(markedDrawing.FileName), "MarkedDrawings");
                            }
                        }
                    }

                    foreach (CADPackage package in CurrentProjectData.CADPackages)
                    {
                        bool addPackage = false;
                        string fileToZip = string.Empty;

                        if (workerArgs.drawingPackageFilter == null)
                        {
                            addPackage = true;
                            if (package.LocalName == "" || package.LocalName == null)
                            {
                                if (File.Exists(Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.FileName)))
                                {
                                    fileToZip = Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.FileName);
                                }
                            }
                            else
                            {
                                if (File.Exists(Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.LocalName)))
                                {
                                    fileToZip = Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.LocalName);
                                }
                            }
                        }
                        else
                        {
                            if (package.LocalName == "" || package.LocalName == null)
                            {
                                if (File.Exists(Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.FileName)) && workerArgs.drawingPackageFilter.Contains(Path.GetFileName(package.FileName)))
                                {
                                    addPackage = true;
                                    fileToZip = Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.FileName);
                                }
                            }
                            else
                            {
                                if (File.Exists(Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.LocalName)) && workerArgs.drawingPackageFilter.Contains(Path.GetFileName(package.LocalName)))
                                {
                                    addPackage = true;
                                    fileToZip = Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.LocalName);
                                }
                            }
                        }

                        if (addPackage)
                        {
                            zip.AddFile(fileToZip, "");
                        }

                    }

                    zip.Save(workerArgs.projectFile);
                }
                finishArgs.saveFileSuccess = true;
            }
            catch (Exception ex)
            {
                finishArgs.saveFileSuccess = false;
                finishArgs.saveFileMessage = "There was an error saving project. ProjectFile: " + workerArgs.projectFile + " - " + ex.Message;
            }

            e.Result = finishArgs;
        }

        /// <summary>
        /// Main Function of the Project.
        /// It loads the project file and initiate settings as per the project.
        /// </summary>
        /// <param name="loadFile"></param>
        public MainForm(string loadFile)
        {
            InitializeComponent();

           //Set title for the screen
            this.radRibbonBarMain.Text = _applicationTitle + " " + Application.ProductVersion.ToString();
            radPageViewMain.SelectedPage = radPageViewMain.Pages[radPageViewMain.Pages.Count - 1];

            //THIS HIDES THE TITLE BAR AND TABS ON THE NAV PAGE VIEW AND THE TAB PAGE VIEW
            RadPageViewStackElement explorerElement = this.radPageViewMain.ViewElement as RadPageViewStackElement;
            if (explorerElement != null && explorerElement.Children.Count > 0)
            {
                RadPageViewLabelElement labelElement = explorerElement.Children[1] as RadPageViewLabelElement;
                //labelElement.Text = "Project Name";
                labelElement.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            }

            RadPageViewStripElement el = radPageViewProject.ViewElement as RadPageViewStripElement;
            el.ItemContainer.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

            _dbChangeWorker.DoWork += new DoWorkEventHandler(_dbChangeWorker_DoWork);
            _dbChangeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_dbChangeWorker_RunWorkerCompleted);
            _SaveProjectWorker.DoWork += _SaveProjectWorker_DoWork;
            _SaveProjectWorker.RunWorkerCompleted += _SaveProjectWorker_RunWorkerCompleted;
            _LoadProjectWorker.DoWork += _LoadProjectWorker_DoWork;
            _LoadProjectWorker.RunWorkerCompleted += _LoadProjectWorker_RunWorkerCompleted;

            Application.DoEvents();

            loadProject(loadFile);

        }

        #region Toolbar Buttons
        /// <summary>
        /// This event will create new Project settings.
        /// It will check for Old Unsaved Project. 
        /// Confirmation for Saving Old Project or not.
        /// Initiate Database settings for new project.
        /// Load New Project.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementNewProject_Click(object sender, EventArgs e)
        {
            // Check for Unsaved currently opend Project
            if (CurrentProjectDirty)
            {
                DialogResult psr = MessageBox.Show("Current project is not saved. Would you like to save it before closing?", "Save Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (psr == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                else if (psr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool saveSuccess = SaveCurrentProject(false);
                    splitContainerMain.Visible = false;
                    if (!saveSuccess) return;
                }
            }

            // Delete data related to old project
            qaqcControl1.clearQC();

            // Reset Working folder
            Globals.ResetWorkingFolder();
            
            // New project initiation
            Dialogs.NewProjectDialog npd = new Dialogs.NewProjectDialog();
            DialogResult dr = npd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Database initiation
                ProjectData projectData = new ProjectData();

                projectData.ProjectId = Guid.NewGuid();
                projectData.ProjectName = npd.ProjectName;
                projectData.ProjectVersion = new Version(1, 0, 0, 0);
                projectData.LDARDatabaseType = npd.LDARDatabaseType.ToString();
                if (npd.LDARDatabaseType.ToString().Contains("LeakDAS"))
                {
                    projectData.LDARTagStartChildrenNumber = 1;
                    projectData.LDARTagPaddedZeros = 0;
                }
                else
                {
                    projectData.LDARTagPaddedZeros = 3;
                    projectData.LDARTagStartChildrenNumber = 0;
                }
                projectData.LDARDatabaseVersion = npd.LDARDatabaseVersion;
                projectData.LDARDatabaseServer = npd.DatabaseServer;
                projectData.LDARDatabaseName = npd.DatabaseName;
                projectData.LDARDatabaseAuthentication = npd.DatabaseAuthentication;
                projectData.LDARDatabaseUsername = npd.DatabaseUsername;
                projectData.LDARDatabasePassword = npd.DatabasePassword;
                projectData.LDARDatabaseConnectionString = npd.LDARConnectionString;
                projectData.CADProjectPath = npd.CADProjectFile;
                projectData.CADPackages = npd.CADPackages;

                //-----------------------------------------------------------------------------
                //TODO: Deal with this assumption better in the future
                //we assume that if the packages are PDF, that we are in an EIMOC project
                if (projectData.CADPackages.Select(p=>p.PackageType).Contains(CADPackageType.PDF))
                {
                    projectData.ProjectType = LDARProjectType.EiMOC;
                }
                else
                {
                    projectData.ProjectType = LDARProjectType.FieldTechToolbox;
                }
                //-----------------------------------------------------------------------------

                projectData.Configurations.Add(new ProjectConfiguration() { Name = "Default Configuration", Notes = "The default configuration includes all data tables from LDAR and CAD data sources." });

                CurrentProjectDirty = true;
                CurrentProjectData = projectData;
                Globals.WriteProjectToWorkingFolder();
                SetCurrentProject();

                if (npd.AutoLoadCADProject || npd.AutoLoadLDARDatabase)
                {
                    RefreshProject(npd.AutoLoadLDARDatabase, npd.AutoLoadCADProject);
                }
            }
        }

        /// <summary>
        /// Save old data and initiate settings for Open Project
        /// </summary>
        /// <param name="projectFile"></param>
        private void loadProject(string projectFile = "")
        {
            if (CurrentProjectDirty)
            {
                DialogResult psr = MessageBox.Show("Current project is not saved. Would you like to save it before closing?", "Save Project", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (psr == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                else if (psr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool saveSuccess = SaveCurrentProject(false);
                    splitContainerMain.Visible = false;
                    if (!saveSuccess) return;
                }
            }

            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = false;
            od.Filter = "Toolbox Project Files (*.eip)|*.eip|All files (*.*)|*.*";

            DialogResult dr = System.Windows.Forms.DialogResult.Cancel;
            if (string.IsNullOrEmpty(projectFile))
            {
                dr = od.ShowDialog();
                projectFile = od.FileName;
                _workingFile = od.FileName;
            }
            else
            {
                dr = System.Windows.Forms.DialogResult.OK;
                _workingFile = projectFile;
            }

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                
                if (File.Exists(projectFile))
                {

                    Globals.ResetWorkingFolder();
                    //FileUtilities.VerifyWorkingFolderStatus(Globals.WorkingFolder, FileUtilities.GetProjectId(od.FileName));
                    Cursor.Current = Cursors.WaitCursor;

                    _progressDialogSimple = new ProgressDialogSimple();
                    _progressDialogSimple.Message = "Loading Project Data";
                    _progressDialogSimple.Show();

                    _LoadProjectWorker.RunWorkerAsync(projectFile);
                }
            }
        }

        /// <summary>
        /// Open existing Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementOpenProject_Click(object sender, EventArgs e)
        {
            loadProject();
        }

        /// <summary>
        /// Save Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementSaveProject_Click(object sender, EventArgs e)
        {
            SaveCurrentProject(false);
        }

        /// <summary>
        /// Save As Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementSaveAsProject_Click(object sender, EventArgs e)
        {
            SaveCurrentProject(true);
        }

        /// <summary>
        /// Export data to EID file for Tablet integration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementExportProjectData_Click(object sender, EventArgs e)
        {
            if (CurrentProjectData == null)
            {
                MessageBox.Show("There is no project data available.", "Project Data Export", MessageBoxButtons.OK);
            }
            else
            {
                ExportFieldConfigurationDialog ecd = new ExportFieldConfigurationDialog(CurrentProjectData);
                DialogResult dr = ecd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {

                    CurrentProjectData.DefaultDrawing = ecd.DefaultDrawingPackage;

                    string exportFile = ecd.ExportFileName;
                    List<string> selectedDrawingPackages = new List<string>();
                    selectedDrawingPackages.Add(ecd.DefaultDrawingPackage);
                    if (ecd.DrawingPackagesAdditional.Count > 0) selectedDrawingPackages.AddRange(ecd.DrawingPackagesAdditional);
                    List<string> selectedLDARProcessUnits = ecd.SelectedLDARProcessUnits;

                    SaveProjectFileAs(exportFile, selectedDrawingPackages, selectedLDARProcessUnits, true);
                }
            }
        }

        /// <summary>
        /// Update LDAR database 
        /// Checks for QA/QC
        /// Export data to LeakDAS 
        /// Refresh Staging components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementLeakDASImport_Click(object sender, EventArgs e)
        {

            if (!qaqcControl1.QCRan)
            {
                MessageBox.Show("Please refresh QA/QC first");
                return;
            }

            if (testDBModified())
            {
                DialogResult dr = MessageBox.Show("The target database has been changed since it was refreshed last in FieldTech Manager.  Loading data in the target database without matching data in FieldTech Manager can cause import errors.  Would you still like to continue?", "Database Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dr == System.Windows.Forms.DialogResult.No) return;
            }
            
            if (CurrentProjectData.LDARDatabaseType == "Guideware")
            {
                ExportGuideware();
            }
            else 
            {
                ExportLeakDAS();
            }

            refreshStaging();
        }

        /// <summary>
        /// Component QA/QC result
        /// </summary>
        private void ExportGuideware()
        {
            if (CurrentProjectData == null)
            {
                MessageBox.Show("Project must be open first.", "Project Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MainForm.CollectedTags.Count == 0)
            {
                MessageBox.Show("Error: No Field Data to Export", "Project Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (MainForm.CollectedTags.Count == 0)
                {
                    MessageBox.Show("Error: No Field Data to Export", "Project Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            string errors = projectInformationControl1.getErrors();
            string warnings = projectInformationControl1.getWarnings();

            //don't warn developers - they know what they're doing
            if (errors != "0" && !System.Diagnostics.Debugger.IsAttached)
            {
                MessageBox.Show("The current project contains " + errors + " errors.  Please resolve any errors in the data prior to exporting", "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //don't warn developers - they know what they're doing
            if (warnings != "0" && !System.Diagnostics.Debugger.IsAttached)
            {
                DialogResult mr = MessageBox.Show("The current project contains " + warnings + " warnings.  Would you still like to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mr == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            Guideware_Import wiz = new Guideware_Import(this, CurrentProjectData);
            DialogResult dr = wiz.ShowDialog();
            if (wiz.Completed)
            {
                foreach (ProjectTags xpts in wiz.ExportedTags)
                {
                    foreach (ProjectTags pts in CurrentProjectTags)
                    {
                        if (pts == xpts)
                        {
                            pts.Exported = true;
                            pts.ExportedBy = xpts.ExportedBy;
                            pts.ExportedOn = xpts.ExportedOn;
                        }
                    }
                }
                if (importErrors.Tags != null)
                {
                    if (importErrors.Tags.Count > 0)
                    {
                        CurrentProjectTags.Add(importErrors);
                        importErrors = new ProjectTags();
                    }
                }
                RefreshProjectTagData();
            }
            //ImportWizardLeakDAS wiz = new ImportWizardLeakDAS();
            //wiz.ShowDialog();
        }

        /// <summary>
        /// Check for QA/QC result for Components.
        /// If all "Passed/OK", than export to LeakDAS
        /// </summary>
        private void ExportLeakDAS()
        {
            if (CurrentProjectData == null)
            {
                MessageBox.Show("Project must be open first.", "Project Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string errors = projectInformationControl1.getErrors();
            string warnings = projectInformationControl1.getWarnings();

                                  //don't warn developers - they know what they're doing
            if (errors != "0" && !System.Diagnostics.Debugger.IsAttached)
            {
                MessageBox.Show("The current project contains " + errors + " errors.  Please resolve any errors in the data prior to exporting", "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                                 //don't warn developers - they know what they're doing
            if (warnings != "0" && !System.Diagnostics.Debugger.IsAttached)
            {
                DialogResult mr = MessageBox.Show("The current project contains " + warnings + " warnings.  Would you still like to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (mr == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            LeakDAS_MOCImport wiz = new LeakDAS_MOCImport(this, CurrentProjectData, CurrentProjectTags.Where(c => c.Exported == false).ToList<ProjectTags>());
            DialogResult dr = wiz.ShowDialog();
            if (wiz.Completed)
            {
                foreach (ProjectTags xpts in wiz.ExportedTags)
                {
                    foreach (ProjectTags pts in CurrentProjectTags)
                    {
                        if (pts == xpts)
                        {
                            pts.Exported = true;
                            pts.ExportedBy = xpts.ExportedBy;
                            pts.ExportedOn = xpts.ExportedOn;
                        }
                    }
                }
                if (importErrors.Tags != null)
                {
                    if (importErrors.Tags.Count > 0)
                    {
                        CurrentProjectTags.Add(importErrors);
                        importErrors = new ProjectTags();
                    }
                }
                RefreshProjectTagData();
            }
            //ImportWizardLeakDAS wiz = new ImportWizardLeakDAS();
            //wiz.ShowDialog();
        }

        #endregion

        /// <summary>
        /// Settings for Current Project during Open New or Existing Project
        /// </summary>
        private void SetCurrentProject()
        {
            Cursor.Current = Cursors.WaitCursor;
            radRibbonBarMain.Text = _applicationTitle +" " + Application.ProductVersion.ToString() + ": " + CurrentProjectData.ProjectName;

            //set up each tab control
            projectInformationControl1.LoadProject(this, CurrentProjectData, CurrentProjectTags);
            ldarTagsControl.LoadProject(this);
            projectTagsControl.LoadProjectTags(this, CurrentProjectTags);
            qaqcControl1.LoadProjectTags(this);
            drawingPackagesControl1.LoadProject(this);
            completedTagsControl1.LoadProjectTags(this, CurrentProjectTags);
            markedDrawingsControl1.LoadProject(this);

            if (CurrentProjectData.LDARDatabaseType == "Guideware")
            {
                radButtonElementLeakDASImport.Image = Properties.Resources.GuideWare5;
                radButtonElementLeakDASImport.Text = "Export To Guideware";
            }
            else
            {
                radButtonElementLeakDASImport.Image = Properties.Resources.leakdasexport_32;
                radButtonElementLeakDASImport.Text = "Export To LeakDAS";
            }

            //start button disabled until checks are done
            radButtonElementLeakDASImport.Enabled = false;

            radRibbonBarGroupLDARIntegration.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            radButtonElementProperties.Enabled = true;

            foreach (ProjectTags pt in CurrentProjectTags)
            {
                if (pt.Tags.Count > 0) radButtonElementLeakDASImport.Enabled = true;
            }

            splitContainerMain.Visible = true;
            Cursor.Current = Cursors.Default;

        }

        /// <summary>
        /// Enable/Disable check for LeakDAS Import button
        /// </summary>
        public void setImportEnabled()
        {
            radButtonElementLeakDASImport.Enabled = false;
            if (CollectedTags.Count > 0) radButtonElementLeakDASImport.Enabled = true;
        }

        /// <summary>
        /// Refresh Component on stagin during "Add to Staged" and "export to LeakDAS" process
        /// </summary>
        public void refreshStaging()
        {
            projectTagsControl.refreshStaging();
        }

        /// <summary>
        /// Refresh Project and update data with LDAR database and CAD Database
        /// </summary>
        /// <param name="ldarDatabase"></param>
        /// <param name="cadDatabase"></param>
        public void RefreshProject(bool ldarDatabase, bool cadDatabase)
        {

            _progressDialog = new ProgressDialog();
            // event handler for the Cancel button in AlertForm
            //_progressDialog.Canceled += new EventHandler<EventArgs>(buttonCancel_Click);
            _progressDialog.Show();

            //load each of the tabs in a background thread
            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(backgroundWorker_LoadProject);
            _worker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_Completed);
            _worker.WorkerReportsProgress = true;
            _worker.RunWorkerAsync(CurrentProjectData);
            //if (CurrentProjectData.LDARDatabaseType == "LeakDAS") Load_LeakDAS_Data(this, null);
            //if (CurrentProjectData.LDARDatabaseType == "Guideware") Load_Guideware_Data(this, null);
            setImportEnabled();

            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// Refresh Tags and Components on Project load
        /// </summary>
        public void RefreshProjectTagData()
        {
            Cursor.Current = Cursors.WaitCursor;

            projectTagsControl.LoadProjectTags(this, CurrentProjectTags);
            projectInformationControl1.LoadProject(this, CurrentProjectData, CurrentProjectTags);
            if (Properties.Settings.Default.AutoQAQC) qaqcControl1.LoadProjectTags(this);
            markedDrawingsControl1.LoadProject(this);
            setImportEnabled();

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Perform QA/QC process on the component tags
        /// </summary>
        /// <param name="reRunQC"></param>
        public void RefreshQAQC(bool reRunQC = false)
        {

            Cursor.Current = Cursors.WaitCursor;

            projectTagsControl.LoadProjectTags(this, CurrentProjectTags);
            projectInformationControl1.LoadProject(this, CurrentProjectData, CurrentProjectTags);
            if (reRunQC) qaqcControl1.LoadProjectTags(this);
            projectInformationControl1.setQAQCStats(qaqcControl1.passedTagsCount, qaqcControl1.failedTagsCount, qaqcControl1.warnTagsCount);
            setImportEnabled();

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Refresh CAD Package 
        /// Check for existing CAD Package and load it.
        /// If existing CAD project not available than create a new package
        /// </summary>
        public void RefreshCADPackage()
        {
            Cursor.Current = Cursors.WaitCursor;
            CADPackage[] packages = new CADPackage[CurrentProjectData.CADPackages.Count];
            CurrentProjectData.CADPackages.CopyTo(packages);

            foreach(CADPackage package in packages)
            {
                if (!File.Exists(package.FileName))
                {
                    DialogResult dr = MessageBox.Show("Original CAD Package File no longer exists: " + package.FileName + ". Would you like to remove it from the project.", "CAD Package not Found", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        CurrentProjectData.CADPackages.Remove(package);
                    }
                }
            }

            //now load and parse each project finally
            List<CADPackage> newPackages = new List<CADPackage>();

            foreach (CADPackage package in CurrentProjectData.CADPackages)
            {
                if (File.Exists(package.FileName))
                {
                    CADPackage newPackage = CADPackageFactory.LoadFromFile(package.FileName);
                    //preserve the local name if it differs from original package
                    newPackage.LocalName = package.LocalName;
                    if (newPackage.PackageValid)
                    {
                        bool loadCheckPassed = false;
                        if (newPackage.PackageId.ToLower() != package.PackageId.ToLower())
                        {
                            DialogResult mdr = MessageBox.Show("The identifier is different in this drawing package than the original. This may cause tagging issues if the retag process has already begun. Are you sure you want to continue?", "Verify New Drawings", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            if (mdr == System.Windows.Forms.DialogResult.Cancel)
                            {
                                return;
                            }
                            else if (mdr == System.Windows.Forms.DialogResult.Yes)
                            {
                                loadCheckPassed = true;
                            }
                        }
                        else
                        {
                            loadCheckPassed = true;
                        }
                        //finally load the package
                        if (loadCheckPassed)
                        {
                            newPackages.Add(newPackage);
                            if (package.LocalName == "" || package.LocalName == null)
                            {
                                File.Copy(package.FileName, Globals.WorkingFolder + "\\" + Path.GetFileName(package.FileName), true);
                            }
                            else
                            {
                                File.Copy(package.FileName, Globals.WorkingFolder + "\\" + Path.GetFileName(package.LocalName), true);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The original file doesn't exist and cannot be refreshed: " + package.FileName);
                }
            }

            //okay, we have parsed all the packages and the valid ones will no reside in newPackages... reset the project
            CurrentProjectData.CADDrawingPackageLastRefreshed = DateTime.Now;
            CurrentProjectData.CADPackages = newPackages;
            CurrentProjectDirty = true;
            projectInformationControl1.LoadProject(this, CurrentProjectData, CurrentProjectTags);
            MessageBox.Show("CAD Package reloaded sucessfully.", "Reload", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Load LeadDAS data on Project Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_LeakDAS_Data(object sender, DoWorkEventArgs e)
        {
            _previousLDARData = CurrentProjectData.LDARData;
            CurrentProjectData.LDARData = new LDARData();
            LeakDAS.initializeDatabase(CurrentProjectData.LDARDatabaseConnectionString);

            BackgroundWorker worker = sender as BackgroundWorker;
            //ProjectData projectData = (ProjectData)e.Argument;
            ProjectData projectData = MainForm.CurrentProjectData;
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            worker.ReportProgress(10, "Loading Chemical States...");
            System.Threading.Thread.Sleep(750);
            CurrentProjectData.LDARData.ChemicalStates = LeakDAS.GetChemicalStates();
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(20, "Loading Classes and Types...");
            System.Threading.Thread.Sleep(750);
            CurrentProjectData.LDARData.ComponentClassTypes = LeakDAS.GetComponentClassTypes();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(30, "Loading Streams...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ComponentStreams = LeakDAS.GetComponentStreams();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(40, "Loading Existing Components...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ExistingComponents = LeakDAS.GetExistingComponents();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(50, "Loading Location Plants...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.LocationPlants = LeakDAS.GetLocationPlants();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            worker.ReportProgress(60, "Loading Pressure Services...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.PressureServices = LeakDAS.GetPressureServices();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(70, "Loading Process Units...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ProcessUnits = LeakDAS.GetProcessUnits();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(70, "Loading Process Areas...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.Areas = LeakDAS.GetAreas();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(70, "Loading Manufacturer Codes...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.Manufacturers = LeakDAS.GetManufacturers();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(80, "Loading Component Categories...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ComponentCategories = LeakDAS.GetCategories();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(80, "Loading Component Category Reasons...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ComponentReasons = LeakDAS.GetReasons();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(90, "Loading Technicians...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.Technicians = LeakDAS.GetTechnicians();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(90, "Loading OOSCodes...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.OOSDescriptions = LeakDAS.GetOOSCodes();

            worker.ReportProgress(95, "Syncing picklist selections");
            System.Threading.Thread.Sleep(750);
            syncPicklistData(_previousLDARData);

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            //worker.ReportProgress(95, "Loading Drawing Package...");
            //System.Threading.Thread.Sleep(750);

            //switch (Path.GetExtension(projectData.CADDrawingPackage))
            //{
            //    case ".dwf":
            //        projectData.CADPackageDetails = DWFParser.GetDetails(projectData.CADDrawingPackage);
            //        break;
            //    case ".pdf":
            //        projectData.CADPackageDetails = PDFParser.GetDetails(projectData.CADDrawingPackage);
            //        break;
            //}       

            projectData.LDARDatabaseLastRefreshed = DateTime.Now;
            projectData.CADDrawingPackageLastRefreshed = DateTime.Now;

            try
            {
                if (!System.IO.Directory.Exists(Globals.WorkingFolder)) System.IO.Directory.CreateDirectory(Globals.WorkingFolder);
                foreach (CADPackage cp in projectData.CADPackages)
                {
                    System.IO.File.Copy(cp.FileName, Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(cp.FileName));
                }
            }
            catch { }

            worker.ReportProgress(100, "LDAR Sync Complete");
            System.Threading.Thread.Sleep(2000);

            //projectInformationControl1.SyncProjectDatabase((ProjectData)e.Argument, worker);
        }

        /// <summary>
        /// Load Guideware Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Guideware_Data(object sender, DoWorkEventArgs e)
        {
            _previousLDARData = CurrentProjectData.LDARData;
            CurrentProjectData.LDARData = new LDARData();
            Guideware.initializeDatabase(CurrentProjectData.LDARDatabaseConnectionString);

            BackgroundWorker worker = sender as BackgroundWorker;
            ProjectData projectData = (ProjectData)e.Argument;
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            worker.ReportProgress(10, "Loading Chemical States...");
            System.Threading.Thread.Sleep(750);
            CurrentProjectData.LDARData.ChemicalStates = Guideware.GetChemicalStates();
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(15, "Loading Options...");
            System.Threading.Thread.Sleep(750);
            CurrentProjectData.LDARData.LDAROptions = Guideware.GetLDAROptions();
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(20, "Loading Classes and Types...");
            System.Threading.Thread.Sleep(750);
            CurrentProjectData.LDARData.ComponentClassTypes = Guideware.GetComponentClassTypes();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(30, "Loading Streams...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ComponentStreams = Guideware.GetComponentStreams();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(40, "Loading Existing Components...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ExistingComponents = Guideware.GetExistingComponents();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(50, "Loading Location Plants...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.LocationPlants = Guideware.GetLocationPlants();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            worker.ReportProgress(60, "Loading Pressure Services...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.PressureServices = Guideware.GetPressureServices();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(70, "Loading Process Units...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ProcessUnits = Guideware.GetProcessUnits();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(70, "Loading Process Areas...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.Areas = Guideware.GetAreas();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(80, "Loading Manufacturer Codes...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.Manufacturers = Guideware.GetManufacturers();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(80, "Loading Component Categories...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ComponentCategories = Guideware.GetCategories();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(90, "Loading Component Category Reasons...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.ComponentReasons = Guideware.GetReasons();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(90, "Loading Technicians...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.Technicians = Guideware.GetTechnicians();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(90, "Loading Equipment...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.Equipment = Guideware.GetEquipment();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(90, "Loading OOS Codes...");
            System.Threading.Thread.Sleep(750);
            projectData.LDARData.OOSDescriptions = Guideware.GetOOSReasons();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(95, "Syncing picklist selections");
            System.Threading.Thread.Sleep(750);
            syncPicklistData(_previousLDARData);

            //worker.ReportProgress(95, "Loading Drawing Package...");
            //System.Threading.Thread.Sleep(750);

            //switch (Path.GetExtension(projectData.CADDrawingPackage))
            //{
            //    case ".dwf":
            //        projectData.CADPackageDetails = DWFParser.GetDetails(projectData.CADDrawingPackage);
            //        break;
            //    case ".pdf":
            //        projectData.CADPackageDetails = PDFParser.GetDetails(projectData.CADDrawingPackage);
            //        break;
            //}       

            projectData.LDARDatabaseLastRefreshed = DateTime.Now;
            projectData.CADDrawingPackageLastRefreshed = DateTime.Now;

            try
            {
                if (!System.IO.Directory.Exists(Globals.WorkingFolder)) System.IO.Directory.CreateDirectory(Globals.WorkingFolder);
                foreach (CADPackage cp in projectData.CADPackages)
                {
                    System.IO.File.Copy(cp.FileName, Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(cp.FileName));
                }
            }
            catch { }

            worker.ReportProgress(100, "LDAR Sync Complete");
            System.Threading.Thread.Sleep(2000);

            //projectInformationControl1.SyncProjectDatabase((ProjectData)e.Argument, worker);
        }

        /// <summary>
        /// Sync Picklist Data from LDAR database to Project data
        /// </summary>
        /// <param name="oldLDARData"></param>
        private void syncPicklistData(LDARData oldLDARData)
        {
            foreach (LDARChemicalState cs in oldLDARData.ChemicalStates)
            {
                LDARChemicalState NewChemState = CurrentProjectData.LDARData.ChemicalStates.Where(c => c.ChemicalStateId == cs.ChemicalStateId).FirstOrDefault();
                if (NewChemState != null)
                {
                    NewChemState.showInTablet = cs.showInTablet;
                }
            }

            foreach (LDARArea area in oldLDARData.Areas)
            {
                LDARArea NewArea = CurrentProjectData.LDARData.Areas.Where(c => c.AreaId == area.AreaId).FirstOrDefault();
                if (NewArea != null)
                {
                    NewArea.showInTablet = area.showInTablet;
                }
            }

            foreach (LDARCategory row in oldLDARData.ComponentCategories)
            {
                LDARCategory newItem = CurrentProjectData.LDARData.ComponentCategories.Where(c => c.ComponentCategoryID == row.ComponentCategoryID).FirstOrDefault();
                if (newItem != null)
                {
                    newItem.showInTablet = row.showInTablet;
                }
            }

            bool parentCheckFound = false;
            foreach (LDARComponentClassType row in oldLDARData.ComponentClassTypes)
            {
                LDARComponentClassType newItem = CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClassId == row.ComponentClassId && c.ComponentTypeId == row.ComponentTypeId).FirstOrDefault();
                {
                    if (newItem != null)
                    {
                        newItem.showInTablet = row.showInTablet;
                        newItem.childType = row.childType;
                        newItem.parentType = row.parentType;
                        if (row.parentType) parentCheckFound = true;
                    }

                }
            }

            if (!parentCheckFound)
            {
                //no parents were selected.  this is probably a result of a database upgrade so let's select them all
                foreach (LDARComponentClassType row in CurrentProjectData.LDARData.ComponentClassTypes)
                {
                    row.parentType = true;
                }
            }

            foreach (LDARComponentStream row in oldLDARData.ComponentStreams)
            {
                LDARComponentStream newItem = CurrentProjectData.LDARData.ComponentStreams.Where(c => c.ComponentStreamId == row.ComponentStreamId).FirstOrDefault();
                if (newItem != null)
                {
                    newItem.showInTablet = row.showInTablet;
                }
            }

            foreach (LDAREquipment row in oldLDARData.Equipment)
            {
                LDAREquipment newItem = CurrentProjectData.LDARData.Equipment.Where(c => c.EquipmentId == row.EquipmentId).FirstOrDefault();
                if (newItem != null)
                {
                    newItem.showInTablet = row.showInTablet;
                }
            }

            foreach (LDARProcessUnit row in oldLDARData.ProcessUnits)
            {
                LDARProcessUnit newItem = CurrentProjectData.LDARData.ProcessUnits.Where(c => c.ProcessUnitId == row.ProcessUnitId).FirstOrDefault();
                if (newItem != null)
                {
                    newItem.showInTablet = row.showInTablet;
                }
            }

            foreach (LDARReason row in oldLDARData.ComponentReasons)
            {
                LDARReason newItem = CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentReasonID == row.ComponentReasonID).FirstOrDefault();
                if (newItem != null)
                {
                    newItem.showInTablet = row.showInTablet;
                }
            }

            foreach (LDAROOSDescription row in oldLDARData.OOSDescriptions)
            {
                LDAROOSDescription newItem = CurrentProjectData.LDARData.OOSDescriptions.Where(c => c.OOSId == row.OOSId).FirstOrDefault();
                if (newItem != null)
                {
                    newItem.showInTablet = row.showInTablet;
                }
            }

            foreach (LDARTechnician row in oldLDARData.Technicians)
            {
                LDARTechnician newItem = CurrentProjectData.LDARData.Technicians.Where(c => c.Id == row.Id).FirstOrDefault();
                if (newItem != null)
                {
                    newItem.showInTablet = row.showInTablet;
                }
            }
        }
        
        /// <summary>
        /// Load LeakDAS Data and GuiderWare Data on Project Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_LoadProject(object sender, DoWorkEventArgs e)
        {

            if (CurrentProjectData.LDARDatabaseType == "LeakDAS") Load_LeakDAS_Data(sender, e);
            if (CurrentProjectData.LDARDatabaseType == "Guideware") Load_Guideware_Data(sender, e);

        }
                
        /// <summary>
        /// This event handler updates the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Pass the progress to AlertForm label and progressbar
            _progressDialog.Message = e.UserState.ToString(); 
            _progressDialog.ProgressValue = e.ProgressPercentage;
        }

        /// <summary>
        /// This event will execute on completion of Background worker process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_Completed(object sender, EventArgs e)
        {
            _progressDialog.Close();
            _progressDialog = null;

            SetCurrentProject();
            setImportEnabled();
            CurrentProjectDirty = true;

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Save project
        /// </summary>
        /// <param name="forcePrompt"></param>
        /// <returns></returns>
        private bool SaveCurrentProject(bool forcePrompt)
        {         
            if (CurrentProjectData == null)
            {
                //this wont work..
                MessageBox.Show("Project Data is unknown or has not been set.");
            }
            else
            {
                if (String.IsNullOrEmpty(CurrentProjectData.ProjectPath) || !File.Exists(CurrentProjectData.ProjectPath) || forcePrompt)
                {
                    SaveFileDialog fd = new SaveFileDialog();
                    fd.Filter = "Toolbox Project Files (*.eip)|*.eip|All files (*.*)|*.*";
                    fd.FilterIndex = 1;
                    fd.FileName = CurrentProjectData.ProjectName;
                    fd.OverwritePrompt = true;

                    DialogResult dr = fd.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        CurrentProjectData.ProjectPath = fd.FileName;
                    }
                    else
                    {
                        return false;
                    }
                }
                SaveProjectFileAs(CurrentProjectData.ProjectPath, null, null);
                CurrentProjectDirty = false;
            }
            return false;
        }

        /// <summary>
        /// This is the main Save function for the project. It is called from many places with different parameters to get the file saved appropriately. 
        /// It is also used for exporting data, since an export is essentially the same as a project, but with some filters and limitations (Only one 
        /// DrawingPackage is allowed AND LDARResults may be filtered).
        /// </summary>
        /// <param name="projectFile">The full path and filename of the location to save the project.</param>
        /// <returns>Flag if project was saved sucessfully</returns>
        public void SaveProjectFileAs(string projectFile, List<string> drawingPackageFilter, List<string>ldarComponentsProcessUnitFilter, bool ignoreMarkedDrawings = false, List<TaggedComponent> selectedTags = null)
        {
            Cursor.Current = Cursors.WaitCursor;

            CurrentProjectData.ZeroStatusSet = true;

            _progressDialogSimple = new ProgressDialogSimple();
            _progressDialogSimple.Message = "Saving Project File";
            _progressDialogSimple.Show();

            saveFileWorkerArguments saveArgs = new saveFileWorkerArguments();
            saveArgs.projectFile = projectFile;
            saveArgs.drawingPackageFilter = drawingPackageFilter;
            saveArgs.ldarComponentsProcessUnitFilter = ldarComponentsProcessUnitFilter;
            saveArgs.ignoreMarkedDrawings = ignoreMarkedDrawings;
            saveArgs.selectedTags = selectedTags;

            _SaveProjectWorker.RunWorkerAsync(saveArgs);            

        }

        /// <summary>
        /// Import and Load EID file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_importEidFiles(object sender, DoWorkEventArgs e)
        {

            if (_eidImportList.Count() == 0) return;

            int progressCounter = 0;

            BackgroundWorker worker = sender as BackgroundWorker;
            
            foreach (string filename in _eidImportList)
            {

                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                
                progressCounter += 1;
                
                ZipFile zip;
                int progressPercent =(int)((progressCounter / (double)_eidImportList.Count()) * 100);
                worker.ReportProgress(progressPercent, "Loading " + Path.GetFileName(filename));
                System.Threading.Thread.Sleep(750);
                
                if (Path.GetExtension(filename) != ".eid")
                {
                    ProjectTags npd = new ProjectTags();
                    npd.CreateDate = DateTime.Now;
                    npd.Device = Path.GetFileNameWithoutExtension(filename);
                    npd.Exported = false;
                    npd.ExportedBy = "";
                    npd.ExportedOn = DateTime.Now;
                    npd.FileName = filename;
                    npd.WorkingFileName = filename;
                    npd.Id = Guid.NewGuid().ToString();

                    List<ProjectTags> nptList;

                    switch (Path.GetExtension(filename))
                    {
                        case ".csv":
                            npd.Tags = FileUtilities.GetTaggedComponentsFromCSV(File.ReadAllText(filename), true, true, false);
                            CurrentProjectTags.Add(npd);
                            break;
                        case ".txt":
                            npd.Tags = FileUtilities.GetTaggedComponentsFromCSV(File.ReadAllText(filename), true, true, true);
                            CurrentProjectTags.Add(npd);
                            break;
                        case ".json":
                            nptList = FileUtilities.DeserializeObject<List<ProjectTags>>(File.ReadAllText(filename));
                            CurrentProjectTags.AddRange(nptList);
                            break;
                        case ".xlsx":
                            npd.Tags = FileUtilities.GetTaggedComponentFromExcel(filename);
                            CurrentProjectTags.Add(npd);
                            break;
                        default:
                            break;
                    }

                    RefreshProjectTagData();

                    return;
                }
                
                
                try
                {
                    zip = ZipFile.Read(filename);
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("Error reading zip file: " + ex1.Message);
                    return;
                }

                using (zip)
                {
                    bool goodToImport = false;
                    string projectId = FileUtilities.GetZipEntryAsText(zip, "project.id");
                    string deviceId = FileUtilities.GetZipEntryAsText(zip, "device.id").Replace("{", "").Replace("}", "");
                    if (projectId.ToLower() != CurrentProjectData.ProjectId.ToString().ToLower())
                    {
                        DialogResult dr = MessageBox.Show("The import file '" + filename + "' does not belong to this project. Are you sure you want to import it?", "Confirm Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                        goodToImport = true;
                    }
                    else
                    {
                        goodToImport = true;
                    }

                    if (goodToImport)
                    {
                        //find the tags.csv file now..
                        ZipEntry edj = zip.Entries.Where(entry => entry.FileName.ToLower() == "tags.csv").FirstOrDefault();
                        if (edj != null)
                        {
                            ProjectTags projectTagData = new ProjectTags();
                            using (var ms = new MemoryStream())
                            {
                                try
                                {
                                    edj.Extract(ms);
                                    ms.Position = 0;
                                    string componentCsv = String.Empty;
                                    using (StreamReader sr = new StreamReader(ms))
                                    {
                                        componentCsv = sr.ReadToEnd();
                                    }

                                    ProjectTags tagData = new ProjectTags();
                                    tagData.FileName = filename;
                                    tagData.Tags = FileUtilities.GetTaggedComponentsFromCSV(componentCsv, false, false, false);
                                    tagData.Device = deviceId;
                                    tagData.CreateDate = edj.LastModified;

                                    //TODO: Had to remove Images due to serialization issues

                                    //look for images
                                    //tagData.Images = new Dictionary<string, Image>();
                                    //var imageEntries = zip.Entries.Where(entry => entry.FileName.ToLower().StartsWith("images/"));
                                    //foreach (var imageEntry in imageEntries)
                                    //{
                                    //    if (!imageEntry.IsDirectory)
                                    //    {
                                    //        string id = imageEntry.FileName.ToLower().Replace("images/", "").Replace(".jpg", "");
                                    //        using (var ims = new MemoryStream())
                                    //        {
                                    //            imageEntry.Extract(ims);
                                    //            ims.Position = 0;
                                    //            if (!String.IsNullOrEmpty(id))
                                    //            {
                                    //                Image image = Image.FromStream(ims);
                                    //                tagData.Images.Add(id, image);
                                    //            }
                                    //        }
                                    //    }
                                    //}

                                    CurrentProjectTags.Add(tagData);
                                }
                                catch { }

                                //import error log if it exists
                                //ZipEntry errLog = zip.Entries.Where(entry => entry.FileName.ToLower().Contains("errorLog.txt")).FirstOrDefault();
                                //if (errLog != null)
                                //{
                                //    errLog.Extract(Globals.WorkingFolder);
                                //}
                            }
                        }
                        else
                        {
                            ProjectTags blankPT = new ProjectTags();
                            blankPT.Tags = new List<TaggedComponent>();
                            blankPT.FileName = filename;
                            blankPT.Device = deviceId;
                            blankPT.CreateDate = DateTime.MinValue;
                            CurrentProjectTags.Add(blankPT);
                            //if (od.FileNames.Count() == 1) MessageBox.Show("No tagging data found in file: " + filename, "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        //import the dwf into current project, it may contain marked drawings
                        ZipEntry dwf = zip.Entries.Where(entry => entry.FileName.ToLower().Contains(".dwf")).FirstOrDefault();
                        if (dwf != null)
                        {
                            resetWorkingFolder();
                            resetTempFolder();
                            if (!Directory.Exists(Globals.WorkingFolder + "\\MarkedDrawings")) Directory.CreateDirectory(Globals.WorkingFolder + "\\MarkedDrawings");
                            string fName = Globals.WorkingFolder + "\\Temp\\" + System.IO.Path.GetFileName(dwf.FileName);
                            //ZipFile projectZip = ZipFile.Read(_workingFile);
                            DirectoryInfo dirInfo = new DirectoryInfo(Globals.WorkingFolder + "\\MarkedDrawings");
                            int targetFileCount = dirInfo.EnumerateFiles("*.dwf").Count();
                            dwf.Extract(Globals.WorkingFolder + "\\Temp", ExtractExistingFileAction.OverwriteSilently);
                            System.IO.File.Move(fName, Globals.WorkingFolder + "\\MarkedDrawings\\" + Path.GetFileNameWithoutExtension(fName) + "_" + targetFileCount.ToString() + ".dwf");
                            fName = Globals.WorkingFolder + "\\MarkedDrawings\\" + Path.GetFileNameWithoutExtension(fName) + "_" + targetFileCount.ToString() + ".dwf";
                            CurrentProjectData.MarkedDrawings.Add(CADPackageFactory.LoadFromFile(fName, true));
                            //using (projectZip)
                            //{
                            //    if (projectZip.Entries.Where(ze => ze.FileName == "MarkedDrawings/" && ze.IsDirectory).Count() == 0)
                            //    {
                            //        projectZip.AddDirectoryByName("MarkedDrawings");
                            //    }
                            //    projectZip.AddFile(fName, "MarkedDrawings");
                            //    projectZip.Save(_workingFile);
                            //}
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This event imports tag data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementImportTagData_Click(object sender, EventArgs e)
        {
            if (CurrentProjectData == null || CurrentProjectTags == null)
            {
                MessageBox.Show("Project must be open first.", "Project Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //open up one or more EIE files..
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = true;
            od.Filter = "FieldTech Data Files (*.eid;*.eie)|*.eie;*.eid|Excel Files (*.xlsx)|*.xlsx|Text Files (*.txt;*.json;*.csv)|*.txt;*.json;*.csv|All files (*.*)|*.*";
            od.CheckFileExists = true;
            od.CheckPathExists = true;

            DialogResult odr = od.ShowDialog();
            if (odr == System.Windows.Forms.DialogResult.OK)
            {
                _progressDialog = new ProgressDialog();
                _progressDialog.Show();

                _eidImportList = od.FileNames.ToList<string>();

                //load each of the eids in a background thread
                _worker = new BackgroundWorker();
                _worker.DoWork += new DoWorkEventHandler(backgroundWorker_importEidFiles);
                _worker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
                _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_Completed);
                _worker.WorkerReportsProgress = true;
                _worker.RunWorkerAsync();
                Cursor.Current = Cursors.WaitCursor;   
            }
        }

        /// <summary>
        /// Reset Working folder.
        /// clean old data and set it for new project
        /// </summary>
        /// <param name="leaveContents"></param>
        private void resetWorkingFolder(bool leaveContents = true)
        {
            if (leaveContents)
            {
                if (!System.IO.Directory.Exists(Globals.WorkingFolder)) System.IO.Directory.CreateDirectory(Globals.WorkingFolder);
            }
            else
            {
                if (System.IO.Directory.Exists(Globals.WorkingFolder))
                {
                    System.IO.Directory.Delete(Globals.WorkingFolder, true);
                }
                System.IO.Directory.CreateDirectory(Globals.WorkingFolder);
            }
        }

        /// <summary>
        /// Reset temprary folders
        /// </summary>
        /// <param name="leaveContents"></param>
        private void resetTempFolder(bool leaveContents = true)
        {
            if (leaveContents)
            {
                if (!System.IO.Directory.Exists(Globals.WorkingFolder + "\\Temp")) System.IO.Directory.CreateDirectory(Globals.WorkingFolder + "\\Temp");
            }
            else
            {
                if (System.IO.Directory.Exists(Globals.WorkingFolder + "\\Temp"))
                {
                    System.IO.Directory.Delete(Globals.WorkingFolder + "\\Temp", true);
                }
                System.IO.Directory.CreateDirectory(Globals.WorkingFolder + "\\Temp");
            }
        }

        /// <summary>
        /// Close application.
        /// Check unsaved project before close and notification to save project or not before exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentProjectDirty)
            {
                DialogResult dr = MessageBox.Show("The current project is not saved. Would you like to save before exiting?", "Confirm Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveCurrentProject(false);
                    splitContainerMain.Visible = false;
                }
                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = false;
                }
            }

            //clean the working directory
            try
            {
                Directory.Delete(Globals.WorkingFolder, true);
            }
            catch { }
        }

        /// <summary>
        /// Reset window content on page change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radPageViewMain_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView navPageView = sender as RadPageView;
            if (navPageView != null)
            {
                //for now, on page change, bind the correct control into the main client window area
                radPageViewProject.SelectedPage = radPageViewProject.Pages[radPageViewProject.Pages.Count - 1 - navPageView.Pages.IndexOf(navPageView.SelectedPage)];
            }
        }

        /// <summary>
        /// Main form activated and check for unsaved Tags
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!FileMessageBoxAlreadyShowing)
            {
                if (CurrentProjectTags.Where(p => p.NeedsReloadedFromWorkingFile.HasValue).Count() > 0)
                {
                    FileMessageBoxAlreadyShowing = true;
                    DialogResult dr = MessageBox.Show("There have been changes made in the Tag data. Would you like to reload that data now?", "Confirm Data Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        projectTagsControl.ReloadUpdatedProjectTags();
                        FileMessageBoxAlreadyShowing = false;
                    }
                }
            }
        }

        /// <summary>
        /// Check LDAR Database 
        /// set it as Enable or Disable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _dbChangeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if ((bool)e.Result == true)
            {
                DialogResult dr = MessageBox.Show("The LDAR Database has been changed, refresh now?", "Refresh Database?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    RefreshProject(true, false);
                }
            }

            setImportEnabled();
        }

        /// <summary>
        /// DB Change Worker Process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _dbChangeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = testDBModified();
        }

        /// <summary>
        /// Element settings 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementSettings_Click(object sender, EventArgs e)
        {
            ProgramSettings ps = new ProgramSettings();
            ps.ShowDialog();
        }

        /// <summary>
        /// Project Properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElementProperties_Click(object sender, EventArgs e)
        {
            ProjectProperties pp = new ProjectProperties();

            pp.ProjectName = CurrentProjectData.ProjectName;
            pp.DatabaseAuthentication = CurrentProjectData.LDARDatabaseAuthentication;
            pp.DatabaseName = CurrentProjectData.LDARDatabaseName;
            pp.DatabasePassword = CurrentProjectData.LDARDatabasePassword;
            pp.DatabaseServer = CurrentProjectData.LDARDatabaseServer;
            pp.DatabaseUsername = CurrentProjectData.LDARDatabaseUsername;
            pp.ChildPadding = CurrentProjectData.LDARTagPaddedZeros;
            if (CurrentProjectData.LDARTagStartChildrenNumber == 0) pp.ChildStartAt = CurrentProjectData.LDARTagStartChildrenNumber = 1; else pp.ChildStartAt = CurrentProjectData.LDARTagStartChildrenNumber;
            pp.RoutePadding = CurrentProjectData.LDARRoutePaddedZeros;

            if (CurrentProjectData.LDARTAGFormat == "" || CurrentProjectData.LDARTAGFormat == "None")
            {
                pp.LDARTagRangeFrom = "";
                pp.LDARTagRangeTo = "";                
                pp.LDARTagFormat = "None";
            }
            else
            {
                pp.LDARTagRangeFrom = CurrentProjectData.LDARTAGStartsFrom;
                pp.LDARTagRangeTo = CurrentProjectData.LDARTAGStartsTo;
                pp.LDARTagFormat = CurrentProjectData.LDARTAGFormat;
            }

            pp.ShowDialog();

            if (pp.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                CurrentProjectData.ProjectName = pp.ProjectName;
                CurrentProjectData.LDARDatabaseAuthentication = pp.DatabaseAuthentication;
                CurrentProjectData.LDARDatabaseConnectionString = pp.LDARConnectionString;
                CurrentProjectData.LDARDatabaseName = pp.DatabaseName;
                CurrentProjectData.LDARDatabasePassword = pp.DatabasePassword;
                CurrentProjectData.LDARDatabaseServer = pp.DatabaseServer;
                CurrentProjectData.LDARDatabaseUsername = pp.DatabaseUsername;
                CurrentProjectData.LDARTagPaddedZeros = pp.ChildPadding;
                CurrentProjectData.LDARTagStartChildrenNumber = pp.ChildStartAt;
                CurrentProjectData.LDARRoutePaddedZeros = pp.RoutePadding;

                if (pp.LDARTagFormat == "None" || pp.LDARTagFormat == "")
                {
                    CurrentProjectData.LDARTAGStartsFrom = "0";
                    CurrentProjectData.LDARTAGStartsTo = "0";
                    CurrentProjectData.LDARTAGFormat = "None";
                }
                else
                {
                    CurrentProjectData.LDARTAGStartsFrom = pp.LDARTagRangeFrom;
                    CurrentProjectData.LDARTAGStartsTo = pp.LDARTagRangeTo;
                    CurrentProjectData.LDARTAGFormat = pp.LDARTagFormat;
                }
            }
            radRibbonBarMain.Text = _applicationTitle + " " + Application.ProductVersion.ToString() + ": " + CurrentProjectData.ProjectName;
            CurrentProjectDirty = true;
        }

        /// <summary>
        /// DB Settings
        /// </summary>
        /// <returns></returns>
        private bool testDBModified()
        {
            bool isModified = false;

            int localAreaCount = CurrentProjectData.LDARData.Areas.Where(c => c.AreaId > 0).Count();
            int localCategoryCount = CurrentProjectData.LDARData.ComponentCategories.Where(c => c.ComponentCategoryID > 0).Count();
            int localChemicalStateCount = CurrentProjectData.LDARData.ChemicalStates.Where(c => c.ChemicalStateId > 0).Count();
            int localComponentCount = CurrentProjectData.LDARData.ExistingComponents.Where(c => c.Id > 0).Count();
            int localClassTypeCount = CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClassId > 0).Count();
            int localStreamCount = CurrentProjectData.LDARData.ComponentStreams.Where(c => c.ComponentStreamId > 0).Count();
            int localEquipmentCount = CurrentProjectData.LDARData.Equipment.Where(c => c.EquipmentId > 0).Count();
            int localManufacturerCount = CurrentProjectData.LDARData.Manufacturers.Where(c => c.ManufacturerId > 0).Count();
            int localOOSReasonsCount = CurrentProjectData.LDARData.OOSDescriptions.Where(c => c.OOSId > 0).Count();
            int localUnitCount = CurrentProjectData.LDARData.ProcessUnits.Where(c => c.ProcessUnitId > 0).Count();
            int localReasonCount = CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentReasonID > 0).Count();
            int localTechnicianCount = CurrentProjectData.LDARData.Technicians.Where(c => c.Id > 0).Count();

            ExtendedResult dbTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "SELECT TOP 1 * FROM Component", 3);

            if (dbTest.Success)
            {
                SqlConnection sqlConnect = new SqlConnection();

                if (CurrentProjectData.LDARDatabaseType.Contains("LeakDAS"))
                {
                    ExtendedResult currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select MAX([ChangeDate]) from ComponentArchive", 3);
                    if (currentTest.Success)
                        if ((DateTime)currentTest.Result > CurrentProjectData.LDARDatabaseLastRefreshed) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from ProcessUnit", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localUnitCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from LocationArea", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localAreaCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from Component", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localComponentCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select (select count(*) as Cnt from ComponentClass) + (select count(*) as Cnt from ComponentType inner join ComponentClass on ComponentClass.ComponentClassID = ComponentType.ComponentClassID)", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localClassTypeCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from ComponentStream", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localStreamCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from OutOfServiceCode", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localOOSReasonsCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from ComponentReason", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localReasonCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from LDARUser", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localTechnicianCount) return true;
                }
                else
                {

                    if (CurrentProjectData.LDARData.Areas.Count > 0)
                    {
                        LDARArea lda = CurrentProjectData.LDARData.Areas.Where(c => string.IsNullOrEmpty(c.UnitCode)).FirstOrDefault();
                        if (lda != null) return true;
                    }

                    ExtendedResult currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select MAX([DateTime]) from ComponentChanges", 3);
                    if (currentTest.Success)
                        if ((DateTime)currentTest.Result > CurrentProjectData.LDARDatabaseLastRefreshed) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from Location1", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localUnitCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from Location2", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localAreaCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from Location3", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localEquipmentCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from Component", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localComponentCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select count(*) from ComponentType inner join SubType on ComponentType.ComponentType_ID = SubType.ComponentType_ID", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localClassTypeCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from ProductStream", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localStreamCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select (select COUNT(*) from POOS) + (select count(*) from RemovalReason)", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localOOSReasonsCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select (select COUNT(*) from DTM) + (select count(*) from UTM)", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localReasonCount) return true;

                    currentTest = SqlUtilities.TestSqlConnection(CurrentProjectData.LDARDatabaseConnectionString, "select COUNT(*) from Inspector", 3);
                    if (currentTest.Success)
                        if ((int)currentTest.Result != localTechnicianCount) return true;
                }
            }
            
            return isModified;
        }

    }

    public class saveFileWorkerArguments
    {
        public string projectFile { get; set; }
        public List<string> drawingPackageFilter { get; set; }
        public List<string> ldarComponentsProcessUnitFilter { get; set; }
        public bool ignoreMarkedDrawings { get; set; }
        public List<TaggedComponent> selectedTags { get; set; }
    }

    public class saveFileWorkerFinishedArgs
    {
        public bool saveFileSuccess { get; set; }
        public string saveFileMessage { get; set; }
    }
}
