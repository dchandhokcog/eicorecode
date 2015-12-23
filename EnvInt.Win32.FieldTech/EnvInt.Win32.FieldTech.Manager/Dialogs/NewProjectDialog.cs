using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.ModelConfiguration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using EnvInt.Data.DAL.LeakDAS4.DatabaseSpecific;
//using EnvInt.Data.DAL.LeakDAS4.EntityClasses;
//using EnvInt.Data.DAL.LeakDAS4.Linq;
//using EnvInt.Data.DAL.Guideware2.DatabaseSpecific;
//using EnvInt.Data.DAL.Guideware2.EntityClasses;
//using EnvInt.Data.DAL.Guideware2.Linq;
using EnvInt.Win32.Controls;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.HelperClasses;

using EnvInt.Data.LDAR;
using EnvInt.Data.LDAR.Guideware;
using EnvInt.Data.LDAR.LeakDAS;

//using SD.LLBLGen.Pro.ORMSupportClasses;
//using SD.LLBLGen.Pro.LinqSupportClasses;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class NewProjectDialog : Form
    {
        private string _ldarConnectionString = "";
        private bool _ldarConnectionValid = false;
        private ExternalDatabaseType _ldarDatabaseType = ExternalDatabaseType.Undefined;
        private string _ldarDatabaseVersion = "";
        private string _cadProjectFile = "";
        private bool _cadProjectValid = false;
        private List<CADPackage> _cadDrawingPackages = new List<CADPackage>();
        private Guidewarev2 gwEntities;
        private LeakDASv4 ldEntities;
        //private Database currentDB;

        public NewProjectDialog()
        {
            InitializeComponent();
            comboBoxAuthentication.SelectedIndex = 0;

            //WE LOVE DEVS!
            if (System.Diagnostics.Debugger.IsAttached)
            {
                textBoxProjectName.Text = "Test Project " + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
                textBoxServer.Text = "localhost";
                textBoxDatabase.Text = "leakDASv4";
                TestSqlConnection(); 
            }

        }

        #region Public Methods

        public string ProjectName
        {
            get { return textBoxProjectName.Text; }
            set { textBoxProjectName.Text = value; }
        }

        public string DatabaseServer
        {
            get { return textBoxServer.Text; }
            set { textBoxServer.Text = value; }
        }

        public string DatabaseName
        {
            get { return textBoxDatabase.Text; }
            set { textBoxDatabase.Text = value; }
        }

        public string DatabaseAuthentication
        {
            get { return comboBoxAuthentication.SelectedItem.ToString(); }
            set { comboBoxAuthentication.SelectedIndex = comboBoxAuthentication.FindStringExact(value); }
        }

        public string DatabaseUsername
        {
            get { return textBoxUsername.Text; }
            set { textBoxUsername.Text = value; }
        }

        public string DatabasePassword
        {
            get { return textBoxPassword.Text; }
            set { textBoxPassword.Text = value; }
        }

        public ExternalDatabaseType LDARDatabaseType
        {
            get { return _ldarDatabaseType; }
            set { _ldarDatabaseType = value; }
        }

        public string LDARDatabaseVersion
        {
            get { return _ldarDatabaseVersion; }
            set { _ldarDatabaseVersion = value; }
        }

        public string CADProjectFile
        {
            get { return _cadProjectFile; }
            set { _cadProjectFile = value; }
        }

        public List<CADPackage> CADPackages
        {
            get { return _cadDrawingPackages; }
            set { _cadDrawingPackages = value; }
        }

        public string LDARConnectionString
        {
            get { return _ldarConnectionString; }
            set { _ldarConnectionString = value; }
        }

        public bool AutoLoadLDARDatabase
        {
            get { return checkBoxAutoLDARDatabase.Checked; }
            set { checkBoxAutoLDARDatabase.Checked = value; }
        }

        public bool AutoLoadCADProject
        {
            get { return checkBoxAutoCADProject.Checked; }
            set { checkBoxAutoCADProject.Checked = value; }
        }


        #endregion

        #region Private Methods

        private void buttonTestSqlConnection_Click(object sender, EventArgs e)
        {
            TestSqlConnection(); 
        }

        private void TestSqlConnection()
        {

            _ldarConnectionValid = false;
            _ldarConnectionString = "";
            Cursor.Current = Cursors.WaitCursor;
            if (String.IsNullOrEmpty(textBoxServer.Text))
            {
                Cursor.Current = Cursors.Default;
                MessageBoxEx.Show(this, "Server name must be specified.");
            }
            else
            {
                if (String.IsNullOrEmpty(textBoxDatabase.Text))
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxEx.Show(this, "Server name must be specified.");
                }
                else
                {
                    if (comboBoxAuthentication.SelectedIndex > 0) //sql authentication
                    {
                        if (string.IsNullOrEmpty(textBoxUsername.Text))
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBoxEx.Show(this, "SQL Username must be specified.");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(textBoxPassword.Text))
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBoxEx.Show(this, "SQL Password must be specified.");
                            }
                            else
                            {
                                _ldarConnectionValid = true;
                            }
                        }
                    }
                    else
                    {
                        _ldarConnectionValid = true;
                    }
                }
            }
            if (_ldarConnectionValid)
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
                sb.ConnectTimeout = 3;
                sb.DataSource = textBoxServer.Text;
                sb.InitialCatalog = textBoxDatabase.Text;
                if (comboBoxAuthentication.SelectedIndex == 0)
                {
                    //windows auth
                    sb.IntegratedSecurity = true;
                }
                else
                {
                    sb.IntegratedSecurity = false;
                    sb.UserID = textBoxUsername.Text;
                    sb.Password = textBoxPassword.Text;
                }


                try
                {
                    string errMsg = LoadExternalDatabaseType(sb.ConnectionString);

                    if (_ldarDatabaseType != ExternalDatabaseType.Undefined)
                    {
                        _ldarConnectionString = sb.ConnectionString;
                        labelDatabaseSummary.Text = "Found " + _ldarDatabaseType.ToString() + " version " + _ldarDatabaseVersion;
                        labelDatabaseSummary.ForeColor = Color.Green;
                        buttonOK.Enabled = true;
                    }
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        MessageBox.Show("Error connecting to database: " + errMsg);
                        labelDatabaseSummary.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBoxEx.Show(this, "Connection Failed: " + ex.Message);
                    labelDatabaseSummary.Text = "Error connecting to database.";
                    labelDatabaseSummary.ForeColor = Color.Red;
                    buttonOK.Enabled = false;
                }

            }

            Cursor.Current = Cursors.Default;
        }

        private string LoadExternalDatabaseType(string connectionString)
        {
            string errMsg = string.Empty;

            //LEAKDAS TEST
            try
            {

                //Data.DAL.LeakDAS4.DatabaseSpecific.DataAccessAdapter adapter = GetLeakdasAdapter(connectionString);
                //Data.DAL.LeakDAS4.Linq.LinqMetaData md = new Data.DAL.LeakDAS4.Linq.LinqMetaData(adapter);
                //DatabaseVersionEntity ver = md.DatabaseVersion.OrderByDescending(v => v.RecordKey).FirstOrDefault();

                ldEntities = new LeakDASv4(connectionString);

                DatabaseVersion ver = ldEntities.DatabaseVersions.OrderByDescending(v => v.RecordKey).FirstOrDefault();

                if (ver != null)
                {
                    _ldarDatabaseType = ExternalDatabaseType.LeakDAS;
                    _ldarDatabaseVersion = ver.Version;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            //GUIDEWARE TEST
            if (errMsg != string.Empty)
            {
                errMsg = string.Empty;
                try
                {
                    //Data.DAL.Guideware2.DatabaseSpecific.DataAccessAdapter adapter = GetGuideware2Adapter(connectionString);
                    //Data.DAL.Guideware2.Linq.LinqMetaData md = new Data.DAL.Guideware2.Linq.LinqMetaData(adapter);

                    //VersionHistoryEntity ver = md.VersionHistory.OrderByDescending(v => v.DateApplied).ThenByDescending(w => w.VersionNumber).FirstOrDefault();

                    gwEntities = new Guidewarev2(connectionString);
                    gwEntities.Database.Connection.ConnectionString = connectionString;

                    VersionHistory ver = gwEntities.VersionHistories.OrderByDescending(v => v.DateApplied).ThenByDescending(w => w.VersionNumber).FirstOrDefault();

                    if (ver != null)
                    {
                        _ldarDatabaseType = ExternalDatabaseType.Guideware;
                        _ldarDatabaseVersion = ver.VersionNumber.ToString();
                    }
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }

            return errMsg;
        }


        //private Data.DAL.LeakDAS4.DatabaseSpecific.DataAccessAdapter GetLeakdasAdapter(string connectionString)
        //{
        //    Data.DAL.LeakDAS4.DatabaseSpecific.DataAccessAdapter leakdasAdapter = new Data.DAL.LeakDAS4.DatabaseSpecific.DataAccessAdapter();
        //    leakdasAdapter.ConnectionString = connectionString;
        //    leakdasAdapter.CatalogNameUsageSetting = CatalogNameUsage.Clear;
        //    return leakdasAdapter;
        //}

        //private Data.DAL.Guideware2.DatabaseSpecific.DataAccessAdapter GetGuideware2Adapter(string connectionString)
        //{
        //    Data.DAL.Guideware2.DatabaseSpecific.DataAccessAdapter guidewareAdapter = new Data.DAL.Guideware2.DatabaseSpecific.DataAccessAdapter();
        //    guidewareAdapter.ConnectionString = connectionString;
        //    guidewareAdapter.CatalogNameUsageSetting = CatalogNameUsage.Clear;
        //    return guidewareAdapter;
        //}

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxProjectName.Text))
            {
                MessageBoxEx.Show("Project Name is required.");
                return;
            }
            if (!_ldarConnectionValid)
            {
                MessageBox.Show("LDAR Connection is invalid.");
                return;
            }
            //if (!_cadProjectValid)
            //{
            //    MessageBox.Show("AutoCAD P&ID Project is invalid.");
            //    return;
            //}

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBrowseCADProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = false;
            od.Filter = "AutoCAD Project Files (project.xml)|project.xml|All files (*.*)|*.*";
            od.CheckFileExists = true;
            od.CheckPathExists = true;

            DialogResult dr = od.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBoxCADProjectFile.Text = od.FileName;
                _cadProjectFile = od.FileName;
                ValidateCADProject();

            }
        }

        private void ValidateCADProject()
        {
            labelCADSummary.Text = "Invalid Project Folder";
            labelCADSummary.ForeColor = Color.Red;
            _cadProjectValid = false;

            if (File.Exists(Path.GetDirectoryName(_cadProjectFile) + "\\processpower.dcf"))
            {
                _cadProjectValid = true;
                labelCADSummary.Text = "AutoCAD P&ID project valid.";
                labelCADSummary.ForeColor = Color.Green;
            }
        }

        private void textBoxCADProjectFile_Leave(object sender, EventArgs e)
        {
            _cadProjectFile = textBoxCADProjectFile.Text;
            ValidateCADProject();
        }

        private void buttonBrowseCADPackage_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = true;
            od.Filter = "Supported Drawing Files (.pdf,.dwf)|*.pdf;*.dwf|All files (*.*)|*.*";
            od.CheckFileExists = true;
            od.CheckPathExists = true;
            if (!Directory.Exists(Globals.WorkingFolder)) Directory.CreateDirectory(Globals.WorkingFolder);

            DialogResult dr = od.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                CADPackages.Clear();
                foreach (string filename in od.FileNames)
                {
                    try
                    {
                        CADPackage package = CADPackageFactory.LoadFromFile(filename);
                        if (package.PackageValid)
                        {
                            CADPackages.Add(package);
                            textBoxCADDWF.Text = String.Join(";", CADPackages.Select(p => Path.GetFileName(p.FileName)).ToArray());
                            File.Copy(filename, Globals.WorkingFolder + "\\" + Path.GetFileName(filename), true);
                        }
                        else
                        {
                            MessageBox.Show("Drawing Package Error: " + package.ValidationError);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void comboBoxAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAuthentication.Text == "Windows Authentication")
            {
                textBoxUsername.Enabled = false;
                textBoxPassword.Enabled = false;
            }
            else
            {
                textBoxPassword.Enabled = true;
                textBoxUsername.Enabled = true;
            }
        }

        private void buttonClearFiles_Click(object sender, EventArgs e)
        {
            _cadDrawingPackages.Clear();
            textBoxCADDWF.Text = "";
        }


    }

}
