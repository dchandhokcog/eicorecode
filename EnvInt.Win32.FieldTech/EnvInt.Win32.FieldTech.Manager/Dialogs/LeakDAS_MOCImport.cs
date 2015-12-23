using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Json;

using EnvInt.Win32.Controls;
//using EnvInt.Data.DAL.LeakDAS4.DatabaseSpecific;
//using EnvInt.Data.DAL.LeakDAS4.EntityClasses;
//using EnvInt.Data.DAL.LeakDAS4.FactoryClasses;
//using EnvInt.Data.DAL.LeakDAS4.HelperClasses;
//using EnvInt.Data.DAL.LeakDAS4.Linq;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Common.Containers;
//using SD.LLBLGen.Pro.ORMSupportClasses;
//using SD.LLBLGen.Pro.LinqSupportClasses;
using Ionic.Zip;

using EnvInt.Data.LDAR.LeakDAS;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class LeakDAS_MOCImport : Form
    {
        private Dictionary<string, string> _streamDefinitions = new Dictionary<string,string>();
        //private string _currentConnectionString = "";
        //private List<DataTable> _currentDataTables = new List<DataTable>();
        private BackgroundWorker _worker = new BackgroundWorker();
        private BackgroundWorker _worker_Regs = new BackgroundWorker();
        private StringBuilder _importMessages = new StringBuilder();
        private int _currentComponentCount = 0;
        private ProjectData _projectData = null;
        private List<ProjectTags> _projectTags = null;
        private List<string> _selectedRegs = new List<string>();
        private string _selectedCompGroup = "";
        private bool _componentImportComplete = false;
        private List<string> _validLastinsGuids = new List<string>();

        private LeakDASv4 ldEntities;

        public bool Completed = false;
        public List<ProjectTags> ExportedTags = new List<ProjectTags>();

        //private List<TaggedComponent> _projectData;

        private Dictionary<string, string> _issuesList = new Dictionary<string, string>() {
            {"Column", ""},
            {"Issue", ""}
        };

        public LeakDAS_MOCImport(MainForm mainForm, ProjectData projectData, List<ProjectTags> projectTags)
        {
            InitializeComponent();
            _projectData = projectData;
            _projectTags = projectTags;
        }

        private void radWizard1_SelectedPageChanged(object sender, Telerik.WinControls.UI.SelectedPageChangedEventArgs e)
        {

            if (e.SelectedPage == this.radWizard1.Pages[1])
            {
                //STATIC VARIABLES
                this.radWizard1.NextButton.Enabled = false;

                textBoxDatabase.Text = _projectData.LDARDatabaseName;
                textBoxServer.Text = _projectData.LDARDatabaseServer;
                textBoxUsername.Text = _projectData.LDARDatabaseUsername;
                textBoxPassword.Text = _projectData.LDARDatabasePassword;
                comboBoxAuthentication.SelectedIndex = comboBoxAuthentication.FindStringExact(_projectData.LDARDatabaseAuthentication);

                ExtendedResult res = TestSqlConnection();
                this.radWizard1.NextButton.Enabled = res.Success;
                if (!res.Success)
                {
                    MessageBox.Show(res.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (e.SelectedPage == this.radWizard1.Pages[2])
            {
                //STATIC VARIABLES
                this.radWizard1.NextButton.Enabled = false;

                //Facility
                //DataAccessAdapter adapter = GetLeakdasAdapter();
                //LocationPlant plants = ldEntities.LocationPlants;
                //adapter.FetchEntityCollection(plants, null);
                //comboBoxPlant.DataSource = plants;

                ldEntities.LocationPlants.Load();
                comboBoxPlant.DataSource = ldEntities.LocationPlants.ToList();

                if (comboBoxPlant.Items.Count > 0)
                {
                    comboBoxPlant.SelectedIndex = 0;
                }

                //Process Unit
                //EntityCollection<ProcessUnitEntity> processUnits = new EntityCollection<ProcessUnitEntity>();
                //adapter.FetchEntityCollection(processUnits, null);

                this.radWizard1.NextButton.Enabled = true;
            }
            //regulations setup
            else if (e.SelectedPage == this.radWizard1.Pages[3])
            {
                //DataAccessAdapter adapter = GetLeakdasAdapter();
                //EntityCollection<RegulationEntity> regs = new EntityCollection<RegulationEntity>();
                //adapter.FetchEntityCollection(regs, null);

                foreach (Regulation reg in ldEntities.Regulations)
                {
                    if (!string.IsNullOrEmpty(reg.LicenseKey)) checkedListBoxRegs.Items.Add(reg.RegulationDescription);
                }

                //EntityCollection<ComplianceGroupEntity> compGroup = new EntityCollection<ComplianceGroupEntity>();
                //adapter.FetchEntityCollection(compGroup, null);

                comboBoxComplianceGroup.DataSource = ldEntities.ComplianceGroups.ToList();

                this.radWizard1.NextButton.Enabled = false;
            }
            else if (e.SelectedPage == this.radWizard1.Pages[4])
            {
                this.radWizard1.NextButton.Enabled = true;
            }
            else if (e.SelectedPage == this.radWizard1.Pages[5])
            {
                Cursor.Current = Cursors.WaitCursor;
                this.radWizard1.NextButton.Enabled = false;
                this.radWizard1.BackButton.Enabled = false;

                _currentComponentCount = MainForm.CollectedTags.Count;

                progressBarImport.Maximum = _currentComponentCount;

                _importMessages.Clear();

                //IMPORT 
                foreach (string item in checkedListBoxRegs.CheckedItems)
                {
                    _selectedRegs.Add(item);
                }
                _selectedCompGroup = comboBoxComplianceGroup.Text;
                _worker = new BackgroundWorker();

                ImportComponentsEventArgs args = new ImportComponentsEventArgs();

                if (comboBoxPlant.SelectedValue != null)
                {
                    args.PlantId = (int)comboBoxPlant.SelectedValue;
                }

                _worker.DoWork += new DoWorkEventHandler(backgroundWorker_ImportComponents);
                _worker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
                _worker.WorkerReportsProgress = true;
                _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_Completed);
                _worker.RunWorkerAsync(args);

                _worker_Regs.DoWork += new DoWorkEventHandler(backgroundWorker_Regs_ImportComponents);
                _worker_Regs.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_Regs_ProgressChanged);
                _worker_Regs.WorkerReportsProgress = true;
                _worker_Regs.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_Regs_Completed);
                _worker_Regs.RunWorkerAsync(args);

            }
        }

        private void AccessValidation()
        {
                //DTM/UTM VALIDATION
                this.radWizard1.NextButton.Enabled = false;
                List<string> componentCategories = new List<string>();
                
                bool definitionError = false;

                foreach (var pt in MainForm.CollectedTags)
                {
                    if (!componentCategories.Contains(pt.Access)) componentCategories.Add(pt.Access);
                }

                foreach (string componentCategory in componentCategories)
                {
                    //translate to LeakDAS category/reason
                    string ldCategory = string.Empty;
                    string ldReason = string.Empty;

                    if (componentCategory.Contains("-"))
                    {
                        if (componentCategory.Split('-')[0].StartsWith("U"))
                        {
                            ldCategory = "U";
                            ldReason = componentCategory.Split('-')[1].Trim();  // everything after the dash
                        }
                        if (componentCategory.Split('-')[0].StartsWith("D"))
                        {
                            ldCategory = "D";
                            ldReason = componentCategory.Split('-')[1].Trim();  // everything after the dash
                        }
                        if (componentCategory.Split('-')[0].StartsWith("N"))
                        {
                            ldCategory = "N";
                            ldReason = componentCategory.Split('-')[1].Trim();  // everything after the dash
                        }
                    }
                    else 
                    {
                        if (componentCategory.Length > 0)
                        {
                            ldCategory = componentCategory.Substring(0, 1);
                        }
                        else
                        {
                            ldCategory = "UNDEFINED";
                        }


                    }
                    //see if it is found in the category codes, build message if not

                    long LeakDASCategoryCode = GetLeakdasCategoryCodeId(ldCategory);
                    long LeakDASCategoryReasonCode = GetLeakdasCategoryReasonCodeId(ldReason);
                    string userMessage = string.Empty;

                    if (LeakDASCategoryCode == 0)
                        {
                            userMessage = "CODE";
                        }
                    if (LeakDASCategoryReasonCode == 0 && !string.IsNullOrEmpty(ldReason)) 
                    {
                        if (userMessage.Length > 0)
                            userMessage = userMessage + "/REASON";
                        else
                            userMessage = "REASON";
                    }
                    if (userMessage.Length > 0)
                    {
                        userMessage = userMessage + " NOT IN LEAKDAS";
                        definitionError = true;
                    }

                    string[] row = new string[] { ldCategory, ldReason, userMessage };
                    ListViewItem newItem = new ListViewItem(row);
                    //listviewCategoriesValidation.Items.Add(newItem);
                }
  
                this.radWizard1.NextButton.Enabled = !definitionError;
        }



        // This event handler updates the UI
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int count = e.ProgressPercentage;
            string message = e.UserState.ToString();
            progressBarImport.PerformStep();
            labelImportStatus.Text = "Importing component " + count.ToString() + "/" + _currentComponentCount.ToString();
            _importMessages.AppendLine(message);
        }

        // This event handler updates the UI
        private void backgroundWorker_Regs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int count = e.ProgressPercentage;
            string message = e.UserState.ToString();
            progressBarImport.PerformStep();
            labelImportStatus.Text = "Applying Regulations " + count.ToString() + "/" + _currentComponentCount.ToString();
            _importMessages.AppendLine(message);
        }

        private void backgroundWorker_Regs_Completed(object sender, EventArgs e)
        {
            
            textBoxResults.Text = _importMessages.ToString();
            if (textBoxResults.Text.Contains("ERROR:"))
            {
                labelImportResults.Text = "THERE WERE ERRORS: review the log below for more information.";
            }
            else
            {
                if (textBoxResults.Text.Contains("SKIPPED:"))
                {
                    labelImportResults.Text = "IMPORT SUCCESSFUL WITH SKIPS: Review the log below for more information.";
                }
                else
                {
                    labelImportResults.Text = "IMPORT SUCCESSFUL: Review the log below for more information.";
                }
            }
            this.radWizard1.BackButton.Enabled = false;
            labelImportStatus.Text = "Import Complete!";
            this.radWizard1.SelectNextPage();
            Cursor.Current = Cursors.Default;
        }

        private void backgroundWorker_Completed(object sender, EventArgs e)
        {
            _componentImportComplete = true;
        }

        private void backgroundWorker_ImportComponents(object sender, DoWorkEventArgs e)
        {
            //this must be transactional
            
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);
            // Since this is more "granular" anyway and we're having "transaction" problems at Shell, disabling transaction block for now
            //using (TransactionScope ts = new TransactionScope())
            //{
                try
                {
                    bool success = false;
                    success = AddProjectTags(e);
                    //ts.Complete();
                    //pts.Exported = true;
                    //pts.ExportedOn = DateTime.Now;
                    //pts.ExportedBy = Environment.UserName;
                    //ExportedTags.Add(pts);

                    //if (!success)
                    //{
                    //    break;
                    //}
                    //else
                    //{
                    //    ts.Complete();
                    //    pts.Exported = true;
                    //    pts.ExportedOn = DateTime.Now;
                    //    pts.ExportedBy = Environment.UserName;
                    //    ExportedTags.Add(pts);
                    //}
                }
                catch (Exception ex)
                {
                    _worker.ReportProgress(0, "Transaction Rolled Back: " + ex.Message);
                    //transaciton is automatically rolled back here if not completed when it goes out of scope.
                }
 //               }

        }

        private void backgroundWorker_Regs_ImportComponents(object sender, DoWorkEventArgs e)
        {

            while (!_componentImportComplete)
            {
                System.Threading.Thread.Sleep(10);
            }
            
            //add regulations
            if (!checkBoxSkip.Checked)
            {
                //using (TransactionScope ts2 = new TransactionScope())
                //{
                    try
                    {
                        string errMsg = string.Empty;
                        errMsg = AddRegulations();
                        //ts2.Complete();
                    }
                    catch (Exception ex)
                    {
                        _worker.ReportProgress(0, "Transaction Rolled Back: " + ex.Message);
                        //transaciton is automatically rolled back here if not completed when it goes out of scope.
                    }
                //}
            }

            Completed = true;
        }

        private bool AddProjectTags(DoWorkEventArgs e, int routeStart = -1)
        {
            ImportComponentsEventArgs args = e.Argument as ImportComponentsEventArgs;

            int i = 0;
            bool addSuccess = false;
            string msg = "";
            
            Guid leakDASMobileSyncGuid;
            int existingComponentID = 0;

            _validLastinsGuids.Clear();
            
            List<TaggedComponent> importErrors = new List<TaggedComponent>();
            List<TaggedComponent> importSuccesses = new List<TaggedComponent>();

            foreach (TaggedComponent comp in MainForm.CollectedTags)
            {
                leakDASMobileSyncGuid = Guid.NewGuid();
                Guid compGuid = AddComponent(comp, args.PlantId, routeStart, ref existingComponentID, ref i, ref msg);

                if (compGuid == Guid.Empty)
                {
                    addSuccess = false;
                    _worker.ReportProgress(i, "Error Adding " + comp.LDARTag + " " + msg);
                    comp.ErrorMessage = msg;
                    importErrors.Add(comp);
                }
                else
                {
                    importSuccesses.Add(comp);
                }

                string addMessage = string.Empty;
                //if ((compGuid != Guid.Empty) && comp.Inspected)
                //{
                //    addMessage = AddInspection(comp, compGuid, leakDASMobileSyncGuid, existingComponentID);
                //}

                i++;
                _worker.ReportProgress(i, msg);
                if (addMessage != string.Empty)
                {
                    _worker.ReportProgress(i, "Inspection Skipped: " + addMessage);
                }
                addSuccess = true;
            }

            if (importErrors.Count > 0)
            {
                ProjectTags pt = new ProjectTags();
                string dtFileName = string.Format("ImportErrors_{0:yyyy-MM-dd_hh-mm-ss-tt}.csv", DateTime.Now);
                pt.CreateDate = DateTime.Now;
                pt.Device = "ImportErrors_" + DateTime.Today.ToString();
                pt.Exported = false;
                pt.FileName = dtFileName;
                pt.Id = Guid.NewGuid().ToString();
                pt.WorkingFileName = dtFileName;
                pt.Tags = importErrors;
                MainForm.importErrors = pt;
                MainForm.CurrentProjectDirty = true;
            }

            if (importSuccesses.Count > 0)
            {
                ProjectTags pt = new ProjectTags();
                string dtFileName = string.Format("ImportedTags_{0:yyyy-MM-dd_hh-mm-ss-tt}.csv", DateTime.Now);
                pt.CreateDate = DateTime.Now;
                pt.Device = "ImportedTags_" + DateTime.Today.ToString();
                pt.Exported = true;
                pt.FileName = dtFileName;
                pt.Id = Guid.NewGuid().ToString();
                pt.WorkingFileName = dtFileName;
                pt.Tags = importSuccesses;
                MainForm.CurrentProjectTags.Add(pt);
                MainForm.CurrentProjectDirty = true;
                foreach (TaggedComponent tc in pt.Tags)
                {
                    MainForm.CollectedTags.RemoveAll(c => c.Id == tc.Id);
                }
            }

            return addSuccess;
        }

        private Guid AddComponent(TaggedComponent comp, int? plantId, int routeStart, ref int existingComponentId, ref int count, ref string msg)
        {
            Guid compGuid = Guid.Empty;
            Guid leakDASMobileSyncGuid = Guid.NewGuid();

            try
            {
                ComponentLastInspection cli = new ComponentLastInspection();
                string previousTag = comp.PreviousTag;

                //see if there is a component in the LeakDAS database with this tag already
                Data.LDAR.LeakDAS.Component existingComponent = ldEntities.Components.Where(c => c.ComponentTag.ToLower().Trim() == previousTag.ToLower().Trim()).FirstOrDefault();

                cli.ComponentTag = comp.LDARTag;
                if (!Guid.TryParse(comp.Id, out compGuid))
                {
                    //if this didn't work we'll just conjure one up
                    compGuid = Guid.NewGuid();
                }
                cli.GUID = compGuid;
                cli.MobileSyncLogID = leakDASMobileSyncGuid;
                cli.ComponentClassID = GetLeakdasComponentClassId(GetLeakDASClassFromSheetValues(comp.ComponentType));
                int typeId = GetLeakdasComponentTypeId(GetLeakDASTypeFromSheetValues(comp.ComponentType), cli.ComponentClassID.Value);
                if (typeId != 0)
                    cli.ComponentTypeID = typeId;
                else
                    cli.ComponentTypeID = null;
                cli.ChemicalStateID = GetLeakdasChemicalStateId(comp.ChemicalState);
                cli.ManufacturerID = GetLeakdasManufacturerId(comp.Manufacturer, cli.ComponentClassID);
                cli.Property = comp.Property;
                cli.LeakDASUser = comp.InspectionInspector;
                
                int strLen = comp.Drawing.Length > 20 ? 20 : comp.Drawing.Length;
                cli.Drawing = comp.Drawing.Substring(0, strLen);  //leakdas components table only allows 20 chars
                int ldCategoryID = GetLeakdasCategoryCodeId(GetLeakDASCategoryCodeFromSheetValues(comp.Access));
                if (ldCategoryID == 0)
                    cli.ComponentCategoryID = 1;  //if we've failed until now default to NTM
                else
                    cli.ComponentCategoryID = ldCategoryID;
                int ldReasonID = GetLeakdasCategoryReasonCodeId(GetLeakDASReasonCodeFromSheetValues(comp.Access));
                if (ldReasonID == 0)
                    cli.ComponentReasonID = null;
                else
                    cli.ComponentReasonID = ldReasonID;

                if (comp.TagOOS)
                {
                    cli.OOSCodeID = GetLeakdasOOSCodeId(comp.TagOOSReason);
                }

                cli.Size = comp.Size;
                cli.ComponentStreamID = GetLeakdasComponentStreamId(comp.Stream);
                cli.PlantID = plantId;
                cli.ProcessUnitID = GetLeakdasUnitId(comp.Unit);
                //TODO: this should be variable, may not belong in UserGroup5
                cli.UserGroup5 = comp.PreviousTag;
                //TODO: this should be variable, may not belong in UserGroup4
                cli.UserGroup4 = comp.MOCNumber;
                cli.ComponentBatchID = GetLeakdasComponentBatchId(comp.Batch);
                cli.AreaID = GetLeakdasAreaId(comp.Area);

                cli.Location = comp.Location;

                bool skip = false;

                DateTime modifiedDate = DateTime.MinValue;
                if (!DateTime.TryParse(comp.ModifiedDate, out modifiedDate))
                {
                    //DATE IS BAD
                    modifiedDate = DateTime.Now;
                }

                //this will keep children sequenced after their parents
                if (routeStart > -1)
                {
                    cli.FITRouteSequence = routeStart + ((count + 1) / 100);
                }
                else
                {
                    cli.FITRouteSequence = count;
                }

                if (!skip)
                {
                    //TEST EXISTING OR NOT
                    if (existingComponent == null)
                    {
                        //new component...
                        cli.NewRecord = true;
                        cli.Edited = false;
                        cli.Uploaded = true;
                        cli.NewRoute = true;
                        cli.WorkOrder = false;

                        cli.NumberofPoints = 1;
                        cli.PressureServiceID = 2; //default to positive pressure
                        cli.RecordAdded = modifiedDate;
                        cli.InspectionDate = cli.RecordAdded;
                        //cli.Longitude = 0;
                        //cli.Latitude = 0;
                        cli.Tested = false;
                    }
                    else
                    {
                        //update component...
                        cli.NewRecord = false;
                        cli.Edited = true;
                        cli.Uploaded = true;
                        cli.NewRoute = false;
                        cli.WorkOrder = false;
                        cli.Tested = false;

                        if (comp.TagPOS)
                        {
                            cli.Deleted = true;
                            cli.DeleteDate = DateTime.Parse(comp.ModifiedDate);
                            cli.DeleteReason = comp.TagPOSReason;
                        }

                        cli.InspectionDate = existingComponent.DateAdded;
                        cli.ComponentID = existingComponent.ComponentID;
                        existingComponentId = existingComponent.ComponentID;
                        cli.NumberofPoints = existingComponent.NumberofPoints;
                        cli.PressureServiceID = existingComponent.PressureServiceID;
                        cli.ComponentCategoryID = existingComponent.ComponentCategoryID;
                    }

                    //leakdasAdapter.SaveEntity(cli, true);

                    ldEntities.ComponentLastInspections.Add(cli);

                    if ((compGuid != Guid.Empty) && comp.Inspected)
                    {
                        AddInspection(comp, compGuid, leakDASMobileSyncGuid, cli.ComponentID);
                    }

                    if (cli.ComponentID.HasValue)
                    {
                        msg = "UPDATED: " + cli.ComponentTag;
                    }
                    else
                    {
                        msg = "CREATED: " + cli.ComponentTag;
                    }
                }
                else
                {
                    //skipped component due to date 
                    msg = "SKIPPED: " + cli.ComponentTag;
                    return Guid.Empty;
                }

                if (checkedListBoxRegs.CheckedIndices.Count > 0 && !checkBoxSkip.Checked)
                {
 
                }

                //i swore i would never need to use recursion again, but here we go
                if (comp.Children.Count > 0)
                {
                    foreach (var child in comp.GetChildrenAsTaggedComponent())
                    {
                        AddComponent(child, plantId, routeStart, ref existingComponentId, ref count, ref msg);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "ERROR: " + comp.LDARTag + "-" + ex.Message;
                return Guid.Empty;
            }

            _validLastinsGuids.Add(compGuid.ToString());
            ldEntities.SaveChanges();
            return compGuid;
        }


        private string AddInspection(TaggedComponent comp, Guid guidLink, Guid syncLogGuid, int? leakDASCompID)
        {

            //adds inspection record.  returns error code if unsuccessful.

            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);
            string errMsg = string.Empty;

            try
            {
                ComponentLastInspection cli = new ComponentLastInspection();
                cli.ComponentTag = comp.LDARTag;
                cli.GUID = guidLink;
                cli.MobileSyncLogID = syncLogGuid;
                cli.LeakDASUser = comp.InspectionInspector;
                int strLen = comp.Drawing.Length > 20 ? 20 : comp.Drawing.Length;
                cli.Drawing = comp.Drawing.Substring(0, strLen);  //leakdas components table only allows 20 chars
                cli.NewRecord = false;
                cli.Edited = false;
                cli.Uploaded = true;
                cli.NewRoute = false;
                cli.WorkOrder = false;
                cli.Tested = true;

                cli.InspectionDate = comp.InspectionDate;
                cli.InspectionBackground = comp.InspectionBackground;
                cli.InspectionMaximum = comp.InspectionReading;
                if (leakDASCompID != 0) cli.ComponentID = leakDASCompID;
                //TODO: Assuming 499 leak threshold is safe, but not always true
                cli.MaximumAllowed = 499;
                if (comp.InspectionReading - comp.InspectionBackground > 499)
                {
                    cli.M21Results = "F";
                }
                else
                {
                    cli.M21Results = "P";
                }

                cli.InspectionDate = comp.InspectionDate;
                cli.Inspector = comp.InspectionInspector;
                cli.Instrument = comp.InspectionInstrument;
                //leakdasAdapter.SaveEntity(cli, true);
                ldEntities.ComponentLastInspections.Add(cli);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return errMsg;
        }

        private string AddRegulations()
        {

            //adds regulation record.  returns error code if unsuccessful.

            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);
            string errMsg = string.Empty;

            //EntityCollection<ComponentLastInspectionEntity> lastins = new EntityCollection<ComponentLastInspectionEntity>();
            //leakdasAdapter.FetchEntityCollection(lastins, null);

            int compGroupID = GetLeakDASComplianceGroupId(_selectedCompGroup);
            if (compGroupID == 0) return "Regs not applied, no compliance group was found";

            List<int> regIdList = new List<int>();

            foreach (string regName in _selectedRegs)
            {
                int regId = GetLeakDASRegulationId(regName);
                if (regId > 0) regIdList.Add(regId);
            }

            int i = 0;
            
            foreach (ComponentLastInspection li in ldEntities.ComponentLastInspections)
            {
                i++;
                try
                {
                    if (li.ComponentID == null && !li.Tested && _validLastinsGuids.Contains(li.GUID.ToString()))
                    {
                        _worker_Regs.ReportProgress(i, "Adding Regs For " + li.ComponentTag);
                        foreach (int reg in regIdList)
                        {
                            ComponentLastInsRule clir = new ComponentLastInsRule();
                            clir.ComponentLastInspectionID = li.ComponentLastInspectionID;
                            clir.ComplianceGroupID = compGroupID;
                            clir.ComponentCategoryID = li.ComponentCategoryID;
                            clir.ComponentClassID = li.ComponentClassID;
                            clir.ComponentReasonID = li.ComponentReasonID;
                            clir.RegulationID = reg;
                            clir.LeaklessDesign = false;
                            clir.NoDetectibleEmissions = false;
                            clir.DualMechanicalSeal = false;
                            clir.NormalDesign = true;
                            clir.Active = true;
                            clir.Deleted = false;
                            //leakdasAdapter.SaveEntity(clir, true);

                            ldEntities.ComponentLastInsRules.Add(clir);

                        }
                    }
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }

            return errMsg;
        }


        #region Validation Routines


        private void ValidateComponentDates()
        {
            int missingDateValues = 0;

            foreach (var pts in _projectTags)
            {
                foreach (var pt in pts.Tags)
                {
                    string mDate = pt.ModifiedDate;
                    DateTime dateTime = DateTime.Now;
                    if (!DateTime.TryParse(mDate, out dateTime))
                    {
                        missingDateValues++;
                    }
                }
            }

            if (missingDateValues == 0)
            {
                //labelDateMessage.Text = "SUCCESS: All components have a valid Date collected value.";
                //groupBoxDateFix.Visible = false;
            }
            else
            {
                //labelDateMessage.Text = "There are " + missingDateValues.ToString() + " components with missing or unrecognizable date values.";
                //groupBoxDateFix.Visible = true;
            }
        }

        private bool ValidateStreams()
        {
            this.radWizard1.NextButton.Enabled = true;
            //listViewStreamValidation.Items.Clear();
            List<string> componentStreams = new List<string>();

            //get a list of unique streams from the main component import list

            foreach (var pts in _projectTags)
            {
                foreach (var pt in pts.Tags)
                {
                    string stream = pt.Stream;
                    if (String.IsNullOrEmpty(stream))
                    {
                        componentStreams.Add("Empty");
                    }
                    else
                    {
                        if (!componentStreams.Contains(stream))
                        {
                            componentStreams.Add(stream);
                        }
                    }
                }
            }

            foreach (string str in componentStreams)
            {
                //listViewStreamValidation.Items.Add(str);
            }



            //iterate through the stream definitions data to make sure that all of the streams in the component import
            //are actually defined here
 

                //status column here...
            return true;
        }

        private bool ValidateComponents()
        {
            return true;
        }

        private bool ValidateTypes()
        {
            Dictionary<string, int> components = new Dictionary<string, int>();
            bool dataValid = true;

            foreach (var pts in _projectTags)
            {
                foreach (var pt in pts.Tags)
                {
                    string componentClass = GetLeakDASClassFromSheetValues(pt.ComponentType);
                    string componentType = GetLeakDASTypeFromSheetValues(pt.ComponentType);
                    string key = componentClass.ToLower() + "|" + componentType.ToLower();

                    if (!components.ContainsKey(key))
                    {
                        components.Add(key, 0);
                    }
                    components[key]++;
                }
            }

            foreach (KeyValuePair<string, int> component in components)
            {
                string[] s = component.Key.Split('|');
                string componentClass = s[0];
                string componentType = s[1];
                int componentClassId = GetLeakdasComponentClassId(componentClass);

                if (componentClassId == 0)
                {
                    ListViewItem item = new ListViewItem(componentClass);
                    item.SubItems.Add(componentType);
                    item.SubItems.Add(component.Value.ToString());
                    item.SubItems.Add("Component Class does not exist in LeakDAS");
                    dataValid = false;
                    //listViewInvalidComponentConfigurations.Items.Add(item);
                }
                else
                {
                    int componentTypeId = GetLeakdasComponentTypeId(componentType, componentClassId);
                    if (componentTypeId == 0)
                    {
                        ListViewItem item = new ListViewItem(componentClass);
                        item.SubItems.Add(componentType);
                        item.SubItems.Add(component.Value.ToString());
                        item.SubItems.Add("Component Type does not exist in LeakDAS");
                        dataValid = false;
                        // listViewInvalidComponentConfigurations.Items.Add(item);
                    }
                }
            }

            return dataValid;
        }

        // JMA: Don' think this is used any more?
        //private bool ValidateBatches()
        //{
        //    List<String> batches = new List<string>();
        //    //listBoxRecongnizedBatches.Items.Clear();
        //    //listBoxUnrecognizedBatches.Items.Clear();

        //    foreach (var pts in _projectTags)
        //    {
        //        foreach (var pt in pts.Tags)
        //        {
        //            string batch = pt.Batch;
        //            if (!String.IsNullOrEmpty(batch) && !batches.Contains(batch))
        //            {
        //                batches.Add(batch);
        //            }
        //        }
        //    }

        //    //decide
        //    foreach (string batch in batches)
        //    {
        //        long batchId = GetLeakdasComponentBatchId(batch);
        //        if (batchId > 0)
        //        {
        //            //listBoxRecongnizedBatches.Items.Add(batch);
        //        }
        //        else
        //        {
        //            //listBoxUnrecognizedBatches.Items.Add(batch);
        //        }
        //    }

        //    return true;
        //}

        #endregion

        #region LeakDAS Utilities


        private Database GetLeakdasAdapter()
        {
            string ldcs = _projectData.LDARDatabaseConnectionString;
            Database ldDatabase = ldEntities.Database;
            ldDatabase.Connection.ConnectionString = ldcs;
            return ldDatabase;
        }

        private int GetLeakdasComponentStreamId(string stream)
        {
            int id = 0;
            try
            {
                //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
                //LinqMetaData md = new LinqMetaData(ldAdapter);

                ComponentStream cs = ldEntities.ComponentStreams.Where(c => c.StreamDescription.ToLower().Trim() == stream.ToLower().Trim()).FirstOrDefault();
                if (cs != null)
                {
                    id = cs.ComponentStreamID;
                }
                else
                {
                    cs = ldEntities.ComponentStreams.Where(c => c.ComponentStream1.ToLower().Trim() == stream.ToLower().Trim()).FirstOrDefault();
                    if (cs != null)
                    {
                        id = cs.ComponentStreamID;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return id;
        }

        private int GetLeakdasChemicalStateId(string chemicalState)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
           // LinqMetaData md = new LinqMetaData(ldAdapter);

            ChemicalState cs = ldEntities.ChemicalStates.Where(c => c.ChemicalState1.ToLower().Trim() == chemicalState.ToLower().Trim()).FirstOrDefault();
            if (cs != null)
            {
                id = cs.ChemicalStateID;
            }
            else
            {
                cs = ldEntities.ChemicalStates.Where(c => c.StateDescription.ToLower().Trim() == chemicalState.ToLower().Trim()).FirstOrDefault();
                if (cs != null)
                {
                    id = cs.ChemicalStateID;
                }
            }
            return id;
        }


        private int GetLeakdasComponentClassId(string componentClass)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ComponentClass cc = ldEntities.ComponentClasses.Where(c => c.ComponentClass1.ToLower().Trim() == componentClass.ToLower().Trim()).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComponentClassID;
            }
            return id;
        }

        private int GetLeakdasUnitId(string processUnit)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ProcessUnit cc = ldEntities.ProcessUnits.Where(c => c.UnitDescription.ToLower().Trim() == processUnit.ToLower().Trim()).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ProcessUnitID;
            }
            return id;
        }

        private int GetLeakdasAreaId(string areaDescription)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            LocationArea cc = ldEntities.LocationAreas.Where(c => c.AreaDescription.ToLower().Trim() == areaDescription.ToLower().Trim()).FirstOrDefault();
            if (cc != null)
            {
                id = cc.AreaID;
            }
            return id;
        }

        private int GetLeakdasComponentTypeId(string componentType, int componentClassId)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ComponentType ct = ldEntities.ComponentTypes.Where(c => c.ComponentType1.ToLower().Trim() == componentType.ToLower().Trim() && c.ComponentClassID == componentClassId).FirstOrDefault();
            if (ct != null)
            {
                id = ct.ComponentTypeID;
            }
            return id;
        }

        private int GetLeakdasManufacturerId(string manufacturerCode, int? componentClassId)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            Manufacturer mf = ldEntities.Manufacturers.Where(c => c.ManufacturerCode.ToLower().Trim() == manufacturerCode.ToLower().Trim() && c.ComponentClassID == componentClassId).FirstOrDefault();
            if (mf != null)
            {
                id = mf.ManufacturerID;
            }
            return id;
        }

        private int GetLeakdasServiceTypeId(string serviceType)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ChemicalState cs = ldEntities.ChemicalStates.Where(s => s.ChemicalState1 == serviceType).FirstOrDefault();
            if (cs != null)
            {
                id = cs.ChemicalStateID;
            }
            return id;
        }

        private int GetLeakdasComponentBatchId(string componentBatch)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ComponentBatch cb = ldEntities.ComponentBatches.Where(c => c.ComponentBatch1.ToLower().Trim() == componentBatch.ToLower().Trim()).FirstOrDefault();
            if (cb != null)
            {
                id = cb.ComponentBatchID;
            }

            return id;
        }

        private int GetLeakdasCategoryCodeId(string catCode)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ComponentCategory cc = ldEntities.ComponentCategories.Where(c => c.CategoryCode.Equals(catCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComponentCategoryID;
            }
            return id;
        }

        private int GetLeakDASComplianceGroupId(string compGroup)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ComplianceGroup cc = ldEntities.ComplianceGroups.Where(c => c.ComplianceGroupDescription.Equals(compGroup)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComplianceGroupID;
            }
            return id;
        }

        private int GetLeakDASRegulationId(string reg)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
           // LinqMetaData md = new LinqMetaData(leakdasAdapter);

            Regulation cc = ldEntities.Regulations.Where(c => c.RegulationDescription.Equals(reg)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.RegulationID;
            }
            return id;
        }

        private int GetLeakdasCategoryReasonCodeId(string reasonCode)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ComponentReason cc = ldEntities.ComponentReasons.Where(c => c.ReasonDescription.Equals(reasonCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComponentReasonID;
            }
            return id;
        }

        private int GetLeakdasOOSCodeId(string reasonCode)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            OutofServiceCode cc = ldEntities.OutofServiceCodes.Where(c => c.OOSDescription.Equals(reasonCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.OOSCodeID;
            }
            return id;
        }

        private string GetLeakDASCategoryCodeFromSheetValues(string Access)
        {
            
            string returnVal = string.Empty;

            if (Access != null)
            {
                switch (Access.Substring(0,1).ToUpper())
                {
                    case "N":
                        returnVal = "N";
                        break;
                    case "D":
                        returnVal = "D";
                        break;
                    case "U":
                        returnVal = "U";
                        break;
                    default:
                        returnVal = "N";
                        break;
                }
 
            }
            else
            {
                returnVal = "N";
            }
            return returnVal;
        }

        private string GetLeakDASReasonCodeFromSheetValues(string Access)
        {

            string returnVal = string.Empty;

            if (Access.Contains(" - "))
            {
                returnVal = Access.Split('-')[1].Trim().ToUpper();
            }

            return returnVal;
        }

        private string GetLeakDASClassFromSheetValues(string sheetType)
        {

            string returnVal = string.Empty;

            if (sheetType.Contains(" - "))
            {
                returnVal = sheetType.Split('-')[0].Trim().ToUpper();
            }
            else
            {
                returnVal = sheetType;
            }

            return returnVal;
        }

        private string GetLeakDASTypeFromSheetValues(string sheetType)
        {

            string returnVal = string.Empty;

            if (sheetType.Contains(" - "))
            {
                returnVal = sheetType.Split('-')[1].Trim().ToUpper();
            }

            return returnVal;
        }

        #endregion

        private void radWizard1_Cancel(object sender, EventArgs e)
        {
            DialogResult dr = MessageBoxEx.Show("Are you sure you want to exit the import wizard?", "Confirm", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Close();
            }
        }

        private void radWizard1_Finish(object sender, EventArgs e)
        {
            Close();
        }

        private void radWizard1_Help(object sender, EventArgs e)
        {
            //open the help file here...
            ProcessStartInfo psi = new ProcessStartInfo("EiMOC_LeakDAS_Help.doc");
            psi.UseShellExecute = true;
            Process.Start(psi);
        }

        //private bool LoadComponentsFromJson(string fName)
        //{
        //    string jsonText = string.Empty;
        //    bool loadSuccess = true;
        //    try
        //    {
        //        if (File.Exists(fName))
        //        {
        //            using (ZipFile zip = ZipFile.Read(fName))
        //            {
        //                jsonText = FileUtilities.GetZipEntryText(zip, "tags.json");
        //                _projectData = FileUtilities.DeserializeObject<List<TaggedComponent>>(jsonText);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Could not import project data" + ex.Message);
        //        loadSuccess = false;
        //    }

        //    return loadSuccess;
        //}

        private void buttonTestSqlConnection_Click(object sender, EventArgs e)
        {
            ExtendedResult res = TestSqlConnection();
            radWizard1.NextButton.Enabled = res.Success;
            if (res.Success)
            {
                MessageBox.Show("Sucessfully tested connection.", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBoxAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAuthentication.SelectedIndex == 0)
            {
                labelUsername.Visible = false;
                labelPassword.Visible = false;
                textBoxUsername.Visible = false;
                textBoxPassword.Visible = false;
            }
            else
            {
                labelUsername.Visible = true;
                labelPassword.Visible = true;
                textBoxUsername.Visible = true;
                textBoxPassword.Visible = true;
            }
        }

        private ExtendedResult TestSqlConnection()
        {

            ExtendedResult er = new ExtendedResult();

            try
            {
                ldEntities = new LeakDASv4(_projectData.LDARDatabaseConnectionString);
                int connectTestResult = ldEntities.Database.ExecuteSqlCommand("Select count(*) from DatabaseVersion");
                er.Success = true;
            }
            catch (Exception ex)
            {
                er.Success = false;
                er.Message = ex.Message;
            }

            return er;
        }

        private void checkBoxSkip_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSkip.Checked)
            {
                checkedListBoxRegs.Enabled = false;
                comboBoxComplianceGroup.Enabled = false;
                this.radWizard1.NextButton.Enabled = true;
            }
            else
            {
                checkedListBoxRegs.Enabled = true;
                comboBoxComplianceGroup.Enabled = true;
                if (checkedListBoxRegs.CheckedItems.Count > 0)
                {
                    this.radWizard1.NextButton.Enabled = true;
                }
                else
                {
                    this.radWizard1.NextButton.Enabled = false;
                }
            }
        }

        private void checkedListBoxRegs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxRegs.CheckedIndices.Count > 0)
            {
                this.radWizard1.NextButton.Enabled = true;
            }
            else
            {
                this.radWizard1.NextButton.Enabled = false;
            }
        }

    }
}
