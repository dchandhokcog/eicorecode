using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

using EnvInt.Data.LDAR.Guideware;
using EnvInt.Data.LDAR.LeakDAS;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

//using SD.LLBLGen.Pro.ORMSupportClasses;
//using SD.LLBLGen.Pro.LinqSupportClasses;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class ProjectProperties : Form
    {
        private string _ldarConnectionString = "";
        private bool _ldarConnectionValid = false;
        private ExternalDatabaseType _ldarDatabaseType = ExternalDatabaseType.Undefined;
        private string _ldarDatabaseVersion = "";
        private string _cadProjectFile = "";
        private bool _cadProjectValid = false;
        private List<CADPackage> _cadDrawingPackages = new List<CADPackage>();

        public ProjectProperties()
        {
            InitializeComponent();
            comboBoxAuthentication.SelectedIndex = 0;
            cbTagFormat.SelectedIndex = 0;
        }

        #region Public Methods

        public string ProjectName
        {
            get { return textBoxProjectName.Text; }
            set { textBoxProjectName.Text = value; }
        }

        public int ChildPadding
        {
            get { return (int)numericUpDownPaddedZeros.Value; }
            set { numericUpDownPaddedZeros.Value = value; }
        }

        public int RoutePadding
        {
            get { return (int)numericUpDownRoutePaddedZeros.Value; }
            set { numericUpDownRoutePaddedZeros.Value = value; }
        }

        public int ChildStartAt
        {
            get { return (int)numericUpDownChildStart.Value; }
            set { numericUpDownChildStart.Value = value; }
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

        public string LDARTagRangeFrom
        {
            get { return (string)txtRangeFrom.Text; }
            set { txtRangeFrom.Text = value; }
        }

        public string LDARTagRangeTo
        {
            get { return (string)txtRangeTo.Text; }
            set { txtRangeTo.Text = value; }
        }

        public string LDARTagFormat
        {
            get { return Convert.ToString(cbTagFormat.SelectedItem); }
            set { cbTagFormat.SelectedIndex = cbTagFormat.FindStringExact(value); }
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

                LeakDASv4 md = new LeakDASv4(connectionString);

                DatabaseVersion ver = md.DatabaseVersions.OrderByDescending(v => v.RecordKey).FirstOrDefault();
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

                    Guidewarev2 md = new Guidewarev2(connectionString);

                    VersionHistory ver = md.VersionHistories.OrderByDescending(v => v.DateApplied).ThenByDescending(w => w.VersionNumber).FirstOrDefault();
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
            //if (String.IsNullOrEmpty(cbTagFormat.SelectedText))
            //{
            //    MessageBoxEx.Show("Tag Format is required.");
            //    return;
            //}

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

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(this.LDARTagFormat)))
            {
                MessageBoxEx.Show(this, "Please select Tag Format.");
                return;
            }
            else
            {
                if (this.LDARTagFormat != "None")
                {

                    if (this.txtRangeFrom.Text.Length != this.txtRangeTo.Text.Length)
                    {
                        MessageBoxEx.Show(this, "Please check number of digits in 'Range From' and 'Range To'. Both should have same number of digits.");
                        return;
                    }

                    Int64 rangeFrom=0;
                    if (!(Int64.TryParse(this.txtRangeFrom.Text, out rangeFrom)))
                    {
                        MessageBoxEx.Show(this, "Please enter valid 'Range From'. It should be Numeric Value.");
                        return;
                    }

                    Int64 rangeTo = 0;
                    if (!(Int64.TryParse(this.txtRangeTo.Text, out rangeTo)))
                    {
                        MessageBoxEx.Show(this, "Please enter valid 'Range To'.  It should be Numeric Value.");
                        return;
                    }

                    if (rangeTo <= rangeFrom)
                    {
                        MessageBoxEx.Show(this, "'Range To' should be greater than 'Range From'.");
                        return;
                    }

                    //bool isNumeric = false;
                    //Int64 RangeFrom = 0;
                    //try
                    //{
                    //    RangeFrom = Convert.ToInt64(this.txtRangeFrom.Text);
                    //     isNumeric = true;
                    //}
                    //catch
                    //{
                    //    isNumeric = false;
                    //}

                    //if (!isNumeric)
                    //{
                    //    MessageBoxEx.Show(this, "Please select valid value for 'Range From'.");
                    //    return;
                    //}

                    //isNumeric = false;
                    //Int64 RangeTo = 0;
                    //try
                    //{
                    //    RangeFrom = Convert.ToInt64(this.txtRangeFrom.Text);
                    //    isNumeric = true;
                    //}
                    //catch
                    //{
                    //    isNumeric = false;
                    //}
                    //if (!isNumeric)
                    //{
                    //    MessageBoxEx.Show(this, "Please select valid value for 'Range From'.");
                    //    return;
                    //}
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();


        }

        private void buttonTestSqlConnection_Click_1(object sender, EventArgs e)
        {
            TestSqlConnection();
        }

        private void cbTagFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbTagFormat != null)
            {
                if (Convert.ToString(this.cbTagFormat.SelectedItem) == "None")
                {
                    this.txtRangeFrom.Enabled = false;
                    this.txtRangeTo.Enabled = false;
                }
                else
                {
                    this.txtRangeFrom.Enabled = true;
                    this.txtRangeTo.Enabled = true;
                }
            }
        }

        private void ProjectProperties_Load(object sender, EventArgs e)
        {
            if (this.cbTagFormat != null)
            {
                if (LDARTagFormat == "None")
                {
                    this.txtRangeFrom.Enabled = false;
                    this.txtRangeTo.Enabled = false;
                }
                else
                {
                    this.txtRangeFrom.Enabled = true;
                    this.txtRangeTo.Enabled = true;
                }
                //if (LDARTagFormat == "None" || LDARTagFormat == "" || LDARTagFormat == null)
                //{
                //    this.cbTagFormat.Enabled = true;
                //}
                //else
                //{
                //    this.cbTagFormat.Enabled = false;
                //}
            }
        }
    }
}
