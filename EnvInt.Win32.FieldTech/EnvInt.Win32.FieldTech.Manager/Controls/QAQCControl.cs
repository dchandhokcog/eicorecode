using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Manager.Dialogs;
using EnvInt.Win32.FieldTech.Manager.Controls;
using EnvInt.Win32.FieldTech.Migrate;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    public partial class QAQCControl : UserControl
    {
        private QASet _QASet = new QASet();
        private BackgroundWorker _qcProcess = new BackgroundWorker();
        private MainForm _mainform = null;
        public int failedTagsCount { get; set; }
        public int warnTagsCount { get; set; }
        public int passedTagsCount { get; set; }
        public bool QCRan { get; set; }

        public QAQCControl()
        {
            InitializeComponent();
            _qcProcess.DoWork += new DoWorkEventHandler(Background_QC);
            _qcProcess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Background_QC_Complete);
        }

        public void LoadProjectTags(MainForm mainform)
        {

            _mainform = mainform;
            
            if (MainForm.CurrentProjectData.LDARDatabaseType == "LeakDAS")
            {
                _QASet.setQAItems_LeakDAS();
            }
            else
            {
                _QASet.setQAItems_Guideware();
            }

            if (Properties.Settings.Default.AutoQAQC) SyncTags();


        }

        private void Background_QC(object sender, DoWorkEventArgs e)
        {
            _QASet.processCurrentSet();
        }

        private void Background_QC_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            objectListViewItems.SetObjects(_QASet.qaItems);
            setPassedFailed();
            toolStripButtonRefreshQAQC.Text = "Refresh QAQC";
            toolStripButtonRefreshQAQC.Enabled = true;
            _mainform.RefreshQAQC();
            QCRan = true;
            Application.DoEvents();
        }

        public void SyncTags()
        {

            if (!_qcProcess.IsBusy)
            {
                clearAllMessages();
                toolStripButtonRefreshQAQC.Text = "Working...";
                toolStripButtonRefreshQAQC.Enabled = false;
                Application.DoEvents();
                _qcProcess.RunWorkerAsync();
            }

        }

        private void objectListViewItems_ItemActivate(object sender, EventArgs e)
        {
            ComponentViewer cv = new ComponentViewer(_mainform);
            cv.Text = "Component Viewer - " + ((QAItem)objectListViewItems.SelectedObject).QACheck;

            List<TaggedComponent> taggedComps = new List<TaggedComponent>();

            taggedComps = ((QAItem)objectListViewItems.SelectedObject).getAffectedComponents();

            cv.loadProjectData(taggedComps);

            cv.ShowDialog();

            MainForm.CurrentProjectDirty = true;

        }

        public void clearAllMessages()
        {
            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                tc.ErrorMessage = "";
                tc.WarningMessage = "";
            }
        }

        public void setPassedFailed()
        {
            failedTagsCount = 0;
            passedTagsCount = 0;
            warnTagsCount = 0;
            
                foreach (TaggedComponent tc in MainForm.CollectedTags)
                {
                    if (string.IsNullOrEmpty(tc.ErrorMessage))
                    {
                        passedTagsCount += 1;
                    }
                    else
                    {
                        failedTagsCount += 1;
                    }
                    if (!string.IsNullOrEmpty(tc.WarningMessage))
                    {
                        warnTagsCount += 1;
                    }
                }

            _mainform.RefreshQAQC();
 
        }

        private void toolStripButtonRefreshQAQC_Click(object sender, EventArgs e)
        {
            //_mainform.RefreshQAQC();
            SyncTags();
        }

        public void clearQC()
        {
            objectListViewItems.Clear();
        }

        private void toolStripButtonExportCSV_Click(object sender, EventArgs e)
        {
            string html = objectListViewItems.ObjectsToHtml(_QASet.qaItems);

            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.Filter = "HTML Files (*.html)|*.html|All files (*.*)|*.*";
            sd.FileName = "QA_Results.html";
            DialogResult dr = sd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(sd.FileName, html, Encoding.UTF8);
                }
                catch { }
            }

        }

        private void toolStripButtonViewReport(object sender, EventArgs e)
        {

            string WorkingFolder = Globals.ReportTempFolder;
            string ReportTemplateDirectory = Globals.ExecutableFolder + "\\Report Templates";
            ExportAdapter exportAdapter = new ExportAdapter();
            ReportViewer rptView = new ReportViewer();

            if (Directory.Exists(WorkingFolder + "\\Ei_DefaultReport")) Directory.Delete(WorkingFolder + "\\Ei_DefaultReport", true);
            
            //execute the report
            DataTable reportContent = Globals.GetObjectListAsTable<QAItem>(_QASet.qaItems);

            reportContent.Columns.RemoveAt(10);
            reportContent.Columns.RemoveAt(9);
            reportContent.Columns.RemoveAt(8);
            reportContent.Columns.RemoveAt(7);
            reportContent.Columns.RemoveAt(6);


            string importTemplate = File.ReadAllText(ReportTemplateDirectory + "\\Ei_DefaultReport.htm");
            File.Copy(ReportTemplateDirectory + "\\Ei_DefaultReport.css", WorkingFolder + "\\Ei_DefaultReport.css", true);
            Directory.CreateDirectory(WorkingFolder + "\\Ei_DefaultReport");
            foreach (string file in Directory.GetFiles(ReportTemplateDirectory + "\\Ei_DefaultReport"))
            {
                File.Copy(file, WorkingFolder + "\\Ei_DefaultReport\\" + Path.GetFileName(file), true);
            }

            string reportData = string.Empty;
            reportData = exportAdapter.DataTable2HTMLReport(reportContent, "Ei_DefaultReport", ReportTemplateDirectory, "FieldTech Manager QA/QC Summary");
            File.WriteAllText(WorkingFolder + "\\Ei_DefaultReport.htm", reportData, Encoding.UTF8);

            rptView.localReportViewer.HTMLSource = WorkingFolder + "\\Ei_DefaultReport.htm";
            rptView.ShowDialog();

            Application.DoEvents();
        }


    }
}
