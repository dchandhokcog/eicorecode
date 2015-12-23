using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml;
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
using System.Data.SqlServerCe;

using EnvInt.Win32.Controls;
using EnvInt.Win32.FieldTech.Manager.DataSets;
//using EnvInt.Data.DAL.Guideware2.DatabaseSpecific;
//using EnvInt.Data.DAL.Guideware2.EntityClasses;
//using EnvInt.Data.DAL.Guideware2.FactoryClasses;
//using EnvInt.Data.DAL.Guideware2.HelperClasses;
//using EnvInt.Data.DAL.Guideware2.Linq;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Common.Containers;
//using SD.LLBLGen.Pro.ORMSupportClasses;
//using SD.LLBLGen.Pro.LinqSupportClasses;

using EnvInt.Data.LDAR.Guideware;
using System.Data.Entity;
using System.Data.SqlClient;

using Ionic.Zip;


namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class Guideware_Import : Form
    {
        private Dictionary<string, string> _streamDefinitions = new Dictionary<string,string>();
        //private string _currentConnectionString = "";
        //private List<DataTable> _currentDataTables = new List<DataTable>();
        private BackgroundWorker _worker = new BackgroundWorker();
        private BackgroundWorker _worker_Regs = new BackgroundWorker();
        private StringBuilder _importMessages = new StringBuilder();
        private int _currentComponentCount = 1;
        private ProjectData _projectData = null;
        //private List<ProjectTags> _projectTags = null;
        private List<string> _selectedRegs = new List<string>();
        private List<string> _validLastinsGuids = new List<string>();
        private GuideWareMobileDataSet ds = new GuideWareMobileDataSet();
        private DataSets.GuideWareMobileDataSetTableAdapters.TableAdapterManager tableAdapterManager = new DataSets.GuideWareMobileDataSetTableAdapters.TableAdapterManager();
        private DataSets.GuideWareMobileDataSetTableAdapters.ComponentTableAdapter compAdapter = new DataSets.GuideWareMobileDataSetTableAdapters.ComponentTableAdapter();
        private DataSets.GuideWareMobileDataSetTableAdapters.InspectionTableAdapter insAdapter = new DataSets.GuideWareMobileDataSetTableAdapters.InspectionTableAdapter();
        private DataSets.GuideWareMobileDataSetTableAdapters.OptionsTableAdapter optionsAdapter = new DataSets.GuideWareMobileDataSetTableAdapters.OptionsTableAdapter();
        private Guidewarev2 gwEntities;

        public bool Completed = false;
        public List<ProjectTags> ExportedTags = new List<ProjectTags>();

        //private List<TaggedComponent> _projectData;

        private Dictionary<string, string> _issuesList = new Dictionary<string, string>() {
            {"Column", ""},
            {"Issue", ""}
        };

        public Guideware_Import(MainForm mainForm, ProjectData projectData)
        {
            InitializeComponent();
            _projectData = projectData;
            //_projectTags = projectTags;

            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    textBoxFile.Text = "c:\\users\\devin\\desktop\\gwtestexport.sdf";
            //    radioButtonCreateSDF.Checked = true;
            //}

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
                    MessageBox.Show("Failed to connect to database, please check your connection under Project Properties", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.SelectedPage == this.radWizard1.Pages[2])
            {
                this.radWizard1.NextButton.Enabled = true;
            }
            else if (e.SelectedPage == this.radWizard1.Pages[3])
            {
                Cursor.Current = Cursors.WaitCursor;
                this.radWizard1.NextButton.Enabled = false;
                this.radWizard1.BackButton.Enabled = false;

                _currentComponentCount = MainForm.CollectedTags.Count;
                progressBarImport.Maximum = _currentComponentCount;

                _importMessages.Clear();

                //IMPORT 
                _worker = new BackgroundWorker();

                ImportComponentsEventArgs args = new ImportComponentsEventArgs();

                _worker.DoWork += new DoWorkEventHandler(backgroundWorker_ImportComponents);
                _worker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
                _worker.WorkerReportsProgress = true;
                _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_Completed);
                _worker.RunWorkerAsync(args);

            }
            else if (e.SelectedPage == this.radWizard1.Pages[4])
            {
                this.radWizard1.NextButton.Enabled = true;
            }
            else if (e.SelectedPage == this.radWizard1.Pages[5])
            {

                //radioButtonSendDirect.Checked = true;
                
                this.radWizard1.NextButton.Enabled = true;

                ds.Tables["Options"].Rows.Add(GetGuidewareOptionsRow());

                if (radioButtonCreateSDF.Checked)
                {
                    File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\DataSets\\GuideWareMobile.sdf", textBoxFile.Text, true);
                    SqlCeConnection guidewareConnection = new SqlCeConnection("Data Source=" + textBoxFile.Text + ";Persist Security Info=False;");
                    compAdapter.Connection = guidewareConnection;
                    insAdapter.Connection = guidewareConnection;
                    optionsAdapter.Connection = guidewareConnection;
                    guidewareConnection.Open();
                    compAdapter.Update(ds);
                    insAdapter.Update(ds);
                    optionsAdapter.Update(ds);
                    guidewareConnection.Close();
                }
                else
                {
                    //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
                    //LinqMetaData md = new LinqMetaData(GuidewareAdapter);

                    //DownloadEntity ce = new DownloadEntity();

                    Download ce = new Download();

                    ce.Components = ds.Tables["Component"].Rows.Count;
                    ds.DataSetName = "NewDataSet";
                    ce.Device = "Ei Manager Upload";
                    ce.Downloaded = DateTime.Now;
                    ce.AOV = false;
                    ce.Description = MainForm.CurrentProjectData.ProjectName;
                    ce.RunID = 0;
                    MemoryStream ms = new MemoryStream();
                    ds.WriteXml(ms, XmlWriteMode.WriteSchema);
                    //ce.ComponentData = Encoding.ASCII.GetString(ms.ToArray());
                    ce.ComponentData = Encoding.UTF8.GetString(ms.ToArray());
                    //GuidewareAdapter.SaveEntity(ce, true);
                    gwEntities.Downloads.Add(ce);
                    gwEntities.SaveChanges();
                }
            }
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


        private void backgroundWorker_Completed(object sender, EventArgs e)
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
            labelImportStatus.Text = "Import Complete!";
            this.radWizard1.SelectNextPage();
            Cursor.Current = Cursors.Default;
            Completed = true;
        }

        private void backgroundWorker_ImportComponents(object sender, DoWorkEventArgs e)
        {
            //this must be transactional
            
                //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
                //LinqMetaData md = new LinqMetaData(GuidewareAdapter);
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

                    this.radWizard1.BackButton.Enabled = false;
 //               }
        }


        private bool AddProjectTags(DoWorkEventArgs e, int routeStart = -1)
        {
            ImportComponentsEventArgs args = e.Argument as ImportComponentsEventArgs;

            int i = 0;
            bool addSuccess = false;
            string msg = "";
            
            Guid GuidewareMobileSyncGuid = Guid.NewGuid();
            int existingComponentID = 0;

            _validLastinsGuids.Clear();
            
            List<TaggedComponent> importErrors = new List<TaggedComponent>();
            List<TaggedComponent> importSuccesses = new List<TaggedComponent>();

            foreach (TaggedComponent comp in MainForm.CollectedTags)
            {
                string compGuid = AddComponent(comp, routeStart, ref existingComponentID, ref i, ref msg);

                if (compGuid == string.Empty)
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

                i++;
                _worker.ReportProgress(i, "UPDATED: " + comp.LDARTag + "-" + comp.Extension );
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
                foreach (TaggedComponent tc in importSuccesses)
                {
                    MainForm.CollectedTags.RemoveAll(c => c.Id == tc.Id);
                }
            }

            return addSuccess;
        }

        private string AddComponent(TaggedComponent comp, int routeStart, ref int existingComponentId, ref int count, ref string msg)
        {

            string currentGuid = string.Empty;
            existingComponentId = 0;

            DataRow compRow = ds.Tables["Component"].NewRow();

            //append editor to "inspector" table
            if (ds.Tables["Inspector"].Rows.Count < 1)
            {
                DataRow inspectorRow = ds.Tables["Inspector"].NewRow();
                inspectorRow["Description"] = comp.ModifiedBy;
                ds.Tables["Inspector"].Rows.Add(inspectorRow);
            }

            string previousTag = comp.PreviousTag;

            //see if there is a component in the Guideware database with this tag already
            Data.LDAR.Guideware.Component existingComponent = null;
            if (comp.PreviousTag != null)
            {
                    existingComponent = gwEntities.Components.Where(c => c.Tag.ToLower().Trim() == previousTag.ToLower().Trim() && c.Extension == comp.PreviousTagExtension).FirstOrDefault();
            }

            if (existingComponent == null)
            {
                DateTime addDate = DateTime.Now;
                DateTime.TryParse(comp.CreatedDate, out addDate);
                compRow["AddDate"] = addDate;
                compRow["Modified"] = false;
                compRow["Component_ID"] = 0 - _currentComponentCount;
                _currentComponentCount++;
            }
            else
            {
                compRow["Component_ID"] = existingComponent.Component_ID;
                compRow["Modified"] = true;
            }

            compRow["Permit"] = "";
            compRow["Checked"] = false;
            compRow["ImageChanged"] = false;
            compRow["Inaccessible"] = false;
            compRow["AllowAOVRepair"] = false;
            compRow["NonDetectableEmitter"] = false;
            compRow["Cvs"] = !string.IsNullOrEmpty(comp.CVSReason);
            if (!string.IsNullOrEmpty(comp.CVSReason))
            {
                if (GetGuidewareCVSReasonId(comp.CVSReason) != 0)
                {
                    compRow["CVSType"] = GetGuidewareCVSReasonId(comp.CVSReason);
                }
            }
            compRow["Insulated"] = false;
            compRow["OnDor"] = false;
            if (comp.TagOOS)
            {
                compRow["OnRemoval"] = true;
                compRow["ExtendRemoval"] = true;
                compRow["RemovalReason_ID"] = GetGuidewareRemovalId(comp.TagOOSReason);
                compRow["RemovalDate"] = comp.ModifiedDate;
            }
            else
            {
                compRow["OnRemoval"] = false;
                compRow["ExtendRemoval"] = false;
            }
            compRow["OnTank"] = false;
            compRow["Under300Hours"] = false;
            compRow["VacuumService"] = false;
            compRow["Tag"] = comp.LDARTag;
            compRow["TagMove"] = false;
            compRow["LocMove"] = false;
            compRow["Moc"] = comp.MOCNumber;
            compRow["ComponentType_ID"] = GetGuidewareComponentTypeId(GetGuidewareClassFromSheetValues(comp.ComponentType));
            compRow["SubType_ID"] = GetGuidewareComponentSubTypeId(int.Parse(compRow["ComponentType_ID"].ToString()), GetGuidewareTypeFromSheetValues(comp.ComponentType));
            compRow["ServiceType_ID"] = GetGuidewareServiceTypeId(comp.ChemicalState);
            compRow["Drawing"] = comp.Drawing;
            compRow["Notes"] = compRow["Notes"] + "Ei Upload";
            compRow["Threshold"] = 499;
            compRow["FinalThreshold"] = 499;
            compRow["AOVThreshold"] = 499;
            compRow["Commit"] = 0;
            compRow["Inspected"] = comp.Inspected;

            int ldReasonID = GetGuidewareCategoryDTMReasonCodeId(comp.Access);
            if (ldReasonID == 0)
            {
                compRow["Dtm"] = false;
            }
            else
            {
                compRow["Dtm"] = true;
                compRow["DTM_ID"] = ldReasonID;
            }

            int ldUTMReasonID = GetGuidewareCategoryUTMReasonCodeId(comp.UTMReason);
            if (ldUTMReasonID == 0)
            {
                compRow["Utm"] = false;
            }
            else
            {
                compRow["Utm"] = true;
                compRow["UTM_ID"] = ldUTMReasonID;
            }

            compRow["Size"] = comp.Size;
            compRow["Product_ID"] = GetGuidewareComponentStreamId(comp.Stream);
            compRow["Location1_Code"] = GetGuidewareLocation1Id(comp.Unit);
            compRow["Location2_Code"] = GetGuidewareLocation2Id(comp.Area, compRow["Location1_Code"].ToString());
            compRow["Location3_Code"] = GetGuidewareLocation3Id(comp.Equipment, compRow["Location1_Code"].ToString(), compRow["Location2_Code"].ToString());
            compRow["LocationDescription"] = comp.Location;
            compRow["Route"] = comp.RouteSequence;
            compRow["Extension"] = comp.Extension;
            if (comp.TagPOS)
            {
                compRow["Poos"] = true;
                compRow["Poos_ID"] = GetGuidewarePOOSId(comp.TagPOSReason);
                compRow["POOSDate"] = DateTime.Now;
            }
            else
            {
                compRow["Poos"] = false;
            }

            try
            {
                ds.Tables["Component"].Rows.Add(compRow);
                msg = "UPDATED: " + compRow["Tag"] + "-" + compRow["Extension"];
                currentGuid = comp.Id;
                existingComponentId = int.Parse(compRow["Component_ID"].ToString());
                if (comp.Inspected)
                {
                    msg += AddInspection(comp, existingComponentId);
                    compRow["Inspected"] = true;
                }
                else
                {

                    compRow["Inspected"] = true;
                }
            }
            catch (Exception ex)
            {
                msg = "ERROR: " + comp.LDARTag + "-" + ex.Message;
                return string.Empty;
            }

            //i swore i would never need to use recursion again, but here we go
            if (comp.Children.Count > 0)
            {
                foreach (var child in comp.GetChildrenAsTaggedComponent())
                {
                    AddComponent(child, routeStart, ref existingComponentId, ref count, ref msg);
                    _worker.ReportProgress(count, "UPDATED: " + child.LDARTag + "-" + child.Extension);
                }
            }

            return currentGuid;

        }


        private string AddInspection(TaggedComponent comp, int? GuidewareCompID)
        {

            //adds inspection record.  returns error code if unsuccessful.

            //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(GuidewareAdapter);

            DataRow inspectionRow = ds.Tables["Inspection"].NewRow();

            string errMsg = string.Empty;

            if (GuidewareCompID == 0) return "No matching component for inspection: " + comp.LDARTag;

            try
            {
                //InspectionEntity cli = new InspectionEntity();
                inspectionRow["Component_ID"] = (int)GuidewareCompID;
                inspectionRow["DateTime"] = comp.InspectionDate;
                inspectionRow["DateTimeStart"] = comp.InspectionDate;
                inspectionRow["Inspector"] = comp.InspectionInspector;
                inspectionRow["Background"] = (int)comp.InspectionBackground;
                inspectionRow["RawReading"] = comp.InspectionReading;
                inspectionRow["Reading"] = (int)(comp.InspectionReading - comp.InspectionBackground);
                inspectionRow["OOS"] = comp.TagOOS;
                inspectionRow["Running"] = !comp.TagOOS;
                if (comp.InspectionReading > 499)
                {
                    inspectionRow["Leaking"] = true;
                }
                else
                {
                    inspectionRow["Leaking"] = false;
                }
                inspectionRow["Instrument"] = comp.InspectionInstrument;
                //GuidewareAdapter.SaveEntity(cli, true);
                ds.Tables["Inspection"].Rows.Add(inspectionRow);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return errMsg;
        }

        #region Validation Routines


        private void ValidateComponentDates()
        {
            int missingDateValues = 0;

            foreach (var pt in MainForm.CollectedTags)
            {
                string mDate = pt.ModifiedDate;
                DateTime dateTime = DateTime.Now;
                if (!DateTime.TryParse(mDate, out dateTime))
                {
                    missingDateValues++;
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

            foreach (var pt in MainForm.CollectedTags)
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

            foreach (var pt in MainForm.CollectedTags)
            {
                string componentClass = GetGuidewareClassFromSheetValues(pt.ComponentType);
                string componentType = GetGuidewareTypeFromSheetValues(pt.ComponentType);
                string key = componentClass.ToLower() + "|" + componentType.ToLower();

                if (!components.ContainsKey(key))
                {
                    components.Add(key, 0);
                }
                components[key]++;
            }

            foreach (KeyValuePair<string, int> component in components)
            {
                string[] s = component.Key.Split('|');
                string componentClass = s[0];
                string componentType = s[1];
                int componentClassId = GetGuidewareComponentTypeId(componentClass);

                if (componentClassId == 0)
                {
                    ListViewItem item = new ListViewItem(componentClass);
                    item.SubItems.Add(componentType);
                    item.SubItems.Add(component.Value.ToString());
                    item.SubItems.Add("Component Class does not exist in Guideware");
                    dataValid = false;
                    //listViewInvalidComponentConfigurations.Items.Add(item);
                }
                else
                {
                    int componentTypeId = GetGuidewareComponentSubTypeId(componentClassId, componentType);
                    if (componentTypeId == 0)
                    {
                        ListViewItem item = new ListViewItem(componentClass);
                        item.SubItems.Add(componentType);
                        item.SubItems.Add(component.Value.ToString());
                        item.SubItems.Add("Component Type does not exist in Guideware");
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
        //        long batchId = GetGuidewareComponentBatchId(batch);
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

        #region Guideware Utilities


        //private DataAccessAdapter GetGuidewareAdapter()
        //{
        //    string ldcs = _projectData.LDARDatabaseConnectionString;
        //    DataAccessAdapter GuidewareAdapter = new DataAccessAdapter();
        //    GuidewareAdapter.ConnectionString = ldcs;
        //    GuidewareAdapter.CatalogNameUsageSetting = CatalogNameUsage.Clear;
        //    return GuidewareAdapter;
        //}

        private int GetGuidewareComponentStreamId(string stream)
        {
            int id = 0;
            try
            {
                //DataAccessAdapter ldAdapter = GetGuidewareAdapter();
                //LinqMetaData md = new LinqMetaData(ldAdapter);

                ProductStream cs = gwEntities.ProductStreams.Where(c => c.Description.ToLower().Trim() == stream.ToLower().Trim()).FirstOrDefault();
                if (cs != null)
                {
                    id = cs.Product_ID;
                }
            }
            catch {}
            return id;
        }

        private int GetGuidewareCVSReasonId(string CVSReason)
        {
            int id = 0;

            switch (CVSReason)
            {
                case "Unknown":
                    id = 1;
                    break;
                case "Ductwork":
                    id = 2;
                    break;
                case "Hard Pipe":
                    id = 3;
                    break;
                default:
                    id = 0;
                    break;
            }

            return id;
        }

        private DataTable GetGuidewareOptions()
        {
            DataTable OptionsTable = new DataTable();
            OptionsTable.Columns.Add(new DataColumn("Name"));
            OptionsTable.Columns.Add(new DataColumn("Value"));

            try
            {
                //DataAccessAdapter ldAdapter = GetGuidewareAdapter();
                //LinqMetaData md = new LinqMetaData(ldAdapter);

                List<Option> options = gwEntities.Options.ToList();
                foreach (Option oe in options)
                {
                    DataRow dr = OptionsTable.NewRow();
                    dr["Name"] = oe.Name;
                    dr["Value"] = oe.Value;
                    OptionsTable.Rows.Add(dr);
                }
            }
            catch { }
            return OptionsTable;
        }

        private GuideWareMobileDataSet.OptionsRow GetGuidewareOptionsRow()
        {
            DataRow returnRow = ds.Tables["Options"].NewRow();

            foreach (DataRow optionRow in GetGuidewareOptions().Rows)
            {
                switch (optionRow["Name"].ToString())
                {
                    case "Location1Name":
                        returnRow["Location1Name"] = optionRow["Value"].ToString();
                        break;
                    case "Location2Name":
                        returnRow["Location2Name"] = optionRow["Value"].ToString();
                        break;
                    case "Location3Name":
                        returnRow["Location3Name"] = optionRow["Value"].ToString();
                        break;
                    case "RoutingType":
                        returnRow["RoutingType"] = optionRow["Value"].ToString();
                        break;
                    case "CalDriftPercentage":
                        returnRow["DriftPercentage"] = optionRow["Value"].ToString();
                        break;
                    case "CalPercentage":
                        returnRow["CalPercentage"] = optionRow["Value"].ToString();
                        break;
                    case "CalGWMWarning":
                        returnRow["CalWarning"] = optionRow["Value"].ToString();
                        break;
                    case "NoPrecisionGWMWarning":
                        returnRow["NoPrecisionWarning"] = optionRow["Value"].ToString();
                        break;
                    case "GUID":
                        returnRow["Location3Name"] = Guid.NewGuid().ToString();
                        break;
                    case "BlockOOSInspections":
                        returnRow["BlockOOSInspections"] = false;
                        break;
                    case "AllowHigherCals":
                        returnRow["AllowHigherCals"] = true;
                        break;
                    case "ClientCode":
                        returnRow["ClientCode"] = optionRow["Value"].ToString();
                        break;
                    case "MOCName":
                        returnRow["MOCName"] = optionRow["Value"].ToString();
                        break;
                    case "PermitName":
                        returnRow["PermitName"] = optionRow["Value"].ToString();
                        break;
                    case "BarcodeEnforcement":
                        returnRow["BarcodeEnforcement"] = optionRow["Value"].ToString();
                        break;
                }
            }

            returnRow["Facility_ID"] = GetGuidewareFacilityCodeId();
            returnRow["RoutingEnabled"] = true;
            returnRow["RunID"] = 0;

            return (GuideWareMobileDataSet.OptionsRow)returnRow;
        }


        private string GetGuidewareLocation1Id(string location1)
        {
            string code = "";
            //DataAccessAdapter ldAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            Location1 cc = gwEntities.Location1.Where(c => c.Description.ToLower().Trim() == location1.ToLower().Trim()).FirstOrDefault();
            if (cc != null)
            {
                code = cc.Location1_Code;
            }
            return code;
        }

        private string GetGuidewareLocation2Id(string location2, string location1code)
        {
            string code = "";
            //DataAccessAdapter ldAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            Location2 cc = gwEntities.Location2.Where(c => c.Description.ToLower().Trim() == location2.ToLower().Trim() && c.Location1_Code == location1code).FirstOrDefault();
            if (cc != null)
            {
                code = cc.Location2_Code;
            }
            return code;
        }

        private string GetGuidewareLocation3Id(string location3, string location1code, string location2code)
        {
            string code = "";
            //DataAccessAdapter ldAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            Location3 cc = gwEntities.Location3.Where(c => c.Description.ToLower().Trim() == location3.ToLower().Trim() && c.Location1_Code == location1code && c.Location2_Code == location2code).FirstOrDefault();
            if (cc != null)
            {
                code = cc.Location3_Code;
            }
            return code;
        }

        private int GetGuidewareComponentTypeId(string componentType)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ComponentType ct = gwEntities.ComponentTypes.Where(c => c.Description.ToLower().Trim() == componentType.ToLower().Trim()).FirstOrDefault();
            if (ct != null)
            {
                id = ct.ComponentType_ID;
            }
            return id;
        }

        private int GetGuidewareComponentSubTypeId(int componentTypeId, string subType)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            SubType ct = gwEntities.SubTypes.Where(c => c.Description.ToLower().Trim() == subType.ToLower().Trim() && c.ComponentType_ID == componentTypeId ).FirstOrDefault();
            if (ct != null)
            {
                id = ct.SubType_ID;
            }
            return id;
        }

        private int GetGuidewareServiceTypeId(string serviceType)
        {
            int id = 0;
            //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(GuidewareAdapter);

            ServiceType cs = gwEntities.ServiceTypes.Where(s => s.Description == serviceType).FirstOrDefault();
            if (cs != null)
            {
                id = cs.ServiceType_ID;
            }
            return id;
        }

        private int GetGuidewarePOOSId(string serviceType)
        {
            int id = 0;
            //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(GuidewareAdapter);

            POO cs = gwEntities.POOS.Where(s => s.Description == serviceType).FirstOrDefault();
            if (cs != null)
            {
                id = cs.POOS_ID;
            }
            return id;
        }

        private int GetGuidewareRemovalId(string RemovalDescription)
        {
            int id = 0;
            //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(GuidewareAdapter);

            RemovalReason cs = gwEntities.RemovalReasons.Where(s => s.Description == RemovalDescription).FirstOrDefault();
            if (cs != null)
            {
                id = cs.RemovalReason_ID;
            }
            return id;
        }

        private int GetGuidewareCategoryDTMReasonCodeId(string reasonCode)
        {
            int id = 0;
            //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(GuidewareAdapter);

            DTM cc = gwEntities.DTMs.Where(c => c.Description.Equals(reasonCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.DTM_ID;
            }
            return id;
        }

        private int GetGuidewareCategoryUTMReasonCodeId(string reasonCode)
        {
            int id = 0;
            //DataAccessAdapter GuidewareAdapter = GetGuidewareAdapter();
            //LinqMetaData md = new LinqMetaData(GuidewareAdapter);

            UTM cc = gwEntities.UTMs.Where(c => c.Description.Equals(reasonCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.UTM_ID;
            }
            return id;
        }

        private int GetGuidewareFacilityCodeId()
        {
            int id = 0;

            //todo: this is hacky because it assumes the login for ldarcore is same as guideware database            
            string ldcs = _projectData.LDARDatabaseConnectionString.Replace(textBoxDatabase.Text, "ldarcore");

            SqlConnection sqlConn = new SqlConnection(ldcs);
            SqlCommand sqlCmd = new SqlCommand("select Facility_ID from Facility where DatabaseName = '" + textBoxDatabase.Text + "'", sqlConn);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            try
            {
                sqlConn.Open();
                DataSet tmpDs = new DataSet();
                sqlDa.Fill(tmpDs);
                DataRow dr = tmpDs.Tables[0].Rows[0];
                id = int.Parse(dr["Facility_ID"].ToString());
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get facility ID from LDARCore database: " + ex.Message);
            }

            return id;
        }

        private void SendToUploadBin()
        {
            int id = 0;

            //todo: this is hacky because it assumes the login for ldarcore is same as guideware database            
            string ldcs = _projectData.LDARDatabaseConnectionString;
           string selectCommand = "SELECT [Download_ID],[Components],[Device],[LogSheet],[Downloaded],[AOV],[Description],[ComponentData],[RunID],[RouteID] FROM [Download]";
           // string insertCommand = "INSERT INTO [Download]([Download_ID],[Components],[Device],[LogSheet],[Downloaded],[AOV],[Description],[ComponentData],[RunID],[RouteID])

            SqlConnection sqlConn = new SqlConnection(ldcs);
            SqlCommand sqlCmd = new SqlCommand(selectCommand, sqlConn);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            try
            {
                sqlConn.Open();
                DataSet tmpDs = new DataSet();
                sqlDa.Fill(tmpDs);
                DataRow dr = tmpDs.Tables[0].Rows[0];
                id = int.Parse(dr["Facility_ID"].ToString());
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get facility ID from LDARCore database: " + ex.Message);
            }
        }
 
        private string GetGuidewareClassFromSheetValues(string sheetType)
        {

            string returnVal = string.Empty;

            if (sheetType.Contains(" - "))
            {
                returnVal = sheetType.Substring(0, sheetType.IndexOf('-')).Trim().ToUpper();// sheetType.Split('-')[0].Trim().ToUpper();
            }
            else
            {
                returnVal = sheetType;
            }

            return returnVal;
        }

        private string GetGuidewareTypeFromSheetValues(string sheetType)
        {

            string returnVal = string.Empty;

            if (sheetType.Contains(" - "))
            {
                returnVal = sheetType.Substring((sheetType.Substring(0, sheetType.IndexOf('-'))).Length + 1).Trim().ToUpper();//sheetType.Split('-')[1].Trim().ToUpper();
                //ldClass = type.Substring(0, type.IndexOf('-')).Trim().ToUpper(); //type.Split('-')[0].ToUpper().Trim();
               // ldType = type.Substring((type.Substring(0, type.IndexOf('-'))).Length + 1).Trim().ToUpper(); //type.Split('-')[1].ToUpper().Trim();
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
            ProcessStartInfo psi = new ProcessStartInfo("EiMOC_Guideware_Help.doc");
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
                gwEntities = new Guidewarev2(_projectData.LDARDatabaseConnectionString);
                int connectTestResult = gwEntities.Database.ExecuteSqlCommand("SELECT COUNT(*) FROM Component");
                er.Success = true;
            }
            catch (Exception ex)
            {
                er.Success = false;
                er.Message = ex.Message;
            }

            return er;

        }

        private void radioButtonCreateSDF_CheckedChanged(object sender, EventArgs e)
        {
            textBoxFile.Enabled = radioButtonCreateSDF.Checked;
            if (radioButtonCreateSDF.Checked)
            {
                if (string.IsNullOrEmpty(textBoxFile.Text))
                {
                    this.radWizard1.NextButton.Enabled = false;
                }
                else
                {
                    this.radWizard1.NextButton.Enabled = true;
                }
            }
            else
            {
                this.radWizard1.NextButton.Enabled = true;
            }
        }

        private void textBoxFile_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFile.Text))
            {
                this.radWizard1.NextButton.Enabled = false;
            }
            else
            {
                this.radWizard1.NextButton.Enabled = true;
            }
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Guidware Mobile (*.sdf)|*.sdf|All files (*.*)|*.*";
            DialogResult dr = sf.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFile.Text = sf.FileName;
            }
        }

    }
}
