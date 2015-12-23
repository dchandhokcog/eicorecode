using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;
using FirebirdSql.Data.FirebirdClient;

using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Migrate.Forms;
using EnvInt.Win32.FieldTech.Migrate.Controls;


namespace EnvInt.Win32.FieldTech.Migrate
{
    public partial class FormMain : Form
    {

        public BackgroundWorker importWorker = new BackgroundWorker();
        public DataSetSQLExport dt = new DataSetSQLExport();
        private string TargetTransferDB = string.Empty;
        private string RecipeFile = string.Empty;
        private string CurrentConnection = string.Empty;
        private SQLiteConnection localSQLiteConnection = new SQLiteConnection();
        private SQLiteDataAdapter localSQLiteAdapter = new SQLiteDataAdapter();
        private DataTable localTablesList = new DataTable();
        private DataTable localViewsList = new DataTable();
        public string WorkingFolder = string.Empty;
        public string ExecutableFolder = string.Empty;
        public string ReportTemplateDirectory = string.Empty;        
        public FormMain()
        {
            InitializeComponent();
            importWorker.DoWork += new DoWorkEventHandler(importWorker_DoWork);
            importWorker.WorkerReportsProgress = true;
            importWorker.ProgressChanged += new ProgressChangedEventHandler(TransferDataWorker_ProgressChanged);
            importWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TransferDataWorker_Completed);

            WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EiMigrate";
            ExecutableFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).ToString().Replace("file:\\", "");
            ReportTemplateDirectory = ExecutableFolder + "\\Report Templates"; 

            try
            {
                if (!Directory.Exists(WorkingFolder)) Directory.CreateDirectory(WorkingFolder);
            }
            catch { }
        }

        private void importWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            DBConnectionManager connMan = new DBConnectionManager();
            connMan.loadConnections();

            SqlConnection ldConnect = new SqlConnection(connMan.getConnectionStringByName(CurrentConnection));

            ldConnect.Open();

            DatabaseRecipe importRecipe = new DatabaseRecipe();
            importRecipe.LoadRecipeFromXML(RecipeFile);

            DataSetSQLExport dt = new DataSetSQLExport();
            dt.ConnType = DBConnectionType.SQLite;
            dt.DatabaseName = TargetTransferDB;
            dt.FileName = dt.DatabaseName;

            int rowCtr = 0;

            //import tables
            foreach (DataRow tableRow in importRecipe.TableList.Rows)
            {
                rowCtr++;
                dt.xferTableName = tableRow["TableName"].ToString();
                double rc = rowCtr;
                double rt = importRecipe.TableList.Rows.Count;
                double pDone = (rc / rt) * 100;
                int percentDone = Convert.ToInt32(pDone);
                importWorker.ReportProgress(percentDone, "Saving Table:" + dt.xferTableName);
                DataTable newTable = new DataTable();
                SqlDataAdapter newDA = new SqlDataAdapter(tableRow["SQL"].ToString(), ldConnect);
                newDA.Fill(newTable);
                dt.SendToConnection(newTable);
                while (dt.TransferDataWorker.IsBusy)
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
            }

            //import views
            foreach (DataRow tableRow in importRecipe.SQLList.Rows)
            {
                rowCtr++;
                dt.xferTableName = tableRow["ViewName"].ToString();
                importWorker.ReportProgress(99, "Creating View:" + dt.xferTableName);
                dt.createSQLiteView(tableRow["SQL"].ToString());
                while (dt.TransferDataWorker.IsBusy)
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
            }

            //dt.ConnType = DataTransfer.ConnectionTypes.Firebird;
            //dt.DatabaseName = "c:\\users\\devin\\desktop\\testdb.fdb";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolStripButtonCreateLocalDB.Enabled = false;
            importWorker.RunWorkerAsync();
        }

        private void TransferDataWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = "Status: " + e.UserState.ToString();
            Application.DoEvents();
        }

        private void TransferDataWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar.Value = 0;
            toolStripStatusLabel1.Text = "Status: Saved File @ " + DateTime.Now.ToString();
            toolStripButtonCreateLocalDB.Enabled = true;
            Application.DoEvents();
        }

        private void toolStripButtonEditConnections_Click(object sender, EventArgs e)
        {
            FormConnectionManager connMan = new FormConnectionManager();
            connMan.ShowDialog();
        }

        private void toolStripButtonEditRecipes_Click(object sender, EventArgs e)
        {
            FormRecipeManager recipeMan = new FormRecipeManager();

            recipeMan.ShowDialog();
        }

        private void toolStripButtonCreateLocalDB_Click(object sender, EventArgs e)
        {
            CreateLocalDatabase createLocalForm = new CreateLocalDatabase();

            DialogResult dr = createLocalForm.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                CurrentConnection = createLocalForm.Connection;
                RecipeFile = createLocalForm.RecipeFile;
                TargetTransferDB = createLocalForm.TargetFile;
                importWorker.RunWorkerAsync();
            }
        }

        private void toolStripButtonOpenData_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "SQLite Databases (*.s3db)|*.s3db";
            of.Multiselect = false;
            
            DialogResult dr = of.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                localSQLiteConnection.ConnectionString = "Data Source=" + of.FileName + ";Version=3;DateTimeFormat=CurrentCulture;";
                localSQLiteAdapter.SelectCommand = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table'", localSQLiteConnection);
                localSQLiteAdapter.Fill(localTablesList);
                localSQLiteAdapter.SelectCommand = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='view'", localSQLiteConnection);
                localSQLiteAdapter.Fill(localViewsList);

                treeViewFunctions.Nodes[0].Nodes.Clear();
                treeViewFunctions.Nodes[1].Nodes.Clear();
                treeViewFunctions.Nodes[2].Nodes.Clear();

                foreach (DataRow tableRow in localTablesList.Rows)
                {
                    TreeNode newNode = new TreeNode(tableRow["name"].ToString(), 0, 0);
                    treeViewFunctions.Nodes[0].Nodes.Add(newNode);
                }

                foreach (DataRow viewRow in localViewsList.Rows)
                {
                    if (viewRow["name"].ToString().StartsWith("QUERY_"))
                    {
                        TreeNode newNode = new TreeNode(viewRow["name"].ToString().Replace("QUERY_",""), 0, 0);
                        treeViewFunctions.Nodes[1].Nodes.Add(newNode);
                    }

                    if (viewRow["name"].ToString().StartsWith("REPORT_"))
                    {
                        TreeNode newNode = new TreeNode(viewRow["name"].ToString().Replace("REPORT_", ""), 0, 0);
                        treeViewFunctions.Nodes[2].Nodes.Add(newNode);
                    }
                }

                toolStripStatusLabelOpenedFile.Text = of.FileName;
            }
        }

        private void treeViewFunctions_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewFunctions.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode.Parent == null) return;

                if (selectedNode.Parent.Text == "Reports")
                {
                    launchReportTab(selectedNode.Text);
                }
                else
                {
                    launchQueryTab(selectedNode.Text);
                }
            }
        }

        private void launchQueryTab(string Title)
        {
            TabPage newTab = new TabPage(Title);
            newTab.Name = Title;
            tabControl1.TabPages.Add(newTab);
            SQLDataEditor de = new SQLDataEditor();
            de.TableName = Title;
            de._localAdapter = localSQLiteAdapter;
            de._localConnection = localSQLiteConnection;
            de._localCommand = new SQLiteCommand(localSQLiteConnection);
            de.Dock = DockStyle.Fill;

            try
            {
                localSQLiteConnection.Open();
            }
            catch { }

            de.Parent = tabControl1.TabPages[Title];
            tabControl1.SelectedTab = tabControl1.TabPages[Title];
            Application.DoEvents();
        }

        private void launchReportTab(string Title)
        {
            DataSetFileExport exportAdapter = new DataSetFileExport();
            if (Directory.Exists(WorkingFolder + "\\Ei_DefaultReport")) Directory.Delete(WorkingFolder + "\\Ei_DefaultReport", true);
            
            TabPage newTab = new TabPage(Title);
            newTab.Name = Title;
            tabControl1.TabPages.Add(newTab);
            WebReportViewer wRpt = new WebReportViewer();
            wRpt.Dock = DockStyle.Fill;

            //execute the report
            DataTable reportContent = new DataTable();
            localSQLiteAdapter.SelectCommand.CommandText = "Select * from REPORT_" + Title;
            localSQLiteAdapter.Fill(reportContent);
            string importTemplate = File.ReadAllText(ReportTemplateDirectory + "\\Ei_DefaultReport.htm");
            File.Copy(ReportTemplateDirectory + "\\Ei_DefaultReport.css", WorkingFolder + "\\Ei_DefaultReport.css", true);
            Directory.CreateDirectory(WorkingFolder + "\\Ei_DefaultReport");
            foreach (string file in Directory.GetFiles(ReportTemplateDirectory + "\\Ei_DefaultReport"))
            {
                File.Copy(file, WorkingFolder + "\\Ei_DefaultReport\\" + Path.GetFileName(file), true);
            }

            string reportData = string.Empty;
            reportData = exportAdapter.DataTable2HTMLReport(reportContent, "Ei_DefaultReport", ReportTemplateDirectory, Title);
            File.WriteAllText(WorkingFolder + "\\Ei_DefaultReport.htm", reportData);

            wRpt.HTMLSource = WorkingFolder + "\\Ei_DefaultReport.htm";

            wRpt.Parent = tabControl1.TabPages[Title];
            tabControl1.SelectedTab = tabControl1.TabPages[Title];
            Application.DoEvents();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
        }

        private void toolStripButtonImportEid_Click(object sender, EventArgs e)
        {
            LocalSQLiteDatabase lSQL = new LocalSQLiteDatabase();
            lSQL._importWorker.ProgressChanged += new ProgressChangedEventHandler(_importWorker_ProgressChanged);
            lSQL._importWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_importWorker_RunWorkerCompleted);

            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.Filter = "EIP Files|*.eip";
            ofdialog.Multiselect = false;

            SaveFileDialog ofdialog2 = new SaveFileDialog();
            ofdialog2.Filter = "SQLite Files|*.s3db";

            DialogResult dr = ofdialog.ShowDialog();

            DialogResult dr2 = ofdialog2.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK && dr2 == System.Windows.Forms.DialogResult.OK)
            {
                lSQL._SQLiteFileLocation = ofdialog2.FileName;
                lSQL.CreateLocalDatabase(ofdialog.FileName);
                DialogResult saveRecipe = MessageBox.Show("Save Recipe?", "Save Recipe?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveRecipe == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveFileDialog saveRecipeDialog = new SaveFileDialog();
                    saveRecipeDialog.Filter = "XML Recipe Files|*.xml";
                    DialogResult saveDiagResult = saveRecipeDialog.ShowDialog();
                    if (saveDiagResult == System.Windows.Forms.DialogResult.OK)
                    {
                        lSQL._dbRecipe.SaveRecipeToXML(saveRecipeDialog.FileName);
                    }
                }
            }
        }

        void _importWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TransferDataWorker_Completed(this, e);
        }

        void _importWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TransferDataWorker_ProgressChanged(this, e);
        }

        private void toolStripButtonMakeEIP_Click(object sender, EventArgs e)
        {
            //CreateEIP createEipDialog = new CreateEIP();

            //DialogResult dr = createEipDialog.ShowDialog();
        }
    }
}
