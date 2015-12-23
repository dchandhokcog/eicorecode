using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    public partial class ProjectInformationControl : UserControl
    {
        private MainForm _mainForm = null;

        public ProjectInformationControl()
        {
            InitializeComponent();
        }

        public void setQAQCStats(int passed, int failed, int warnings)
        {
            this.labelFailedComponents.Text = failed.ToString();
            this.labelPassedComponents.Text = passed.ToString();
            this.labelWarnings.Text = warnings.ToString();
            setLabelColors();
        }

        public bool LoadProject(MainForm mainform, ProjectData projectData, List<ProjectTags> projectTags)
        {
            _mainForm = mainform;
            //this.Invoke((MethodInvoker)delegate
            //{
            labelLDARName.Text = projectData.LDARDatabaseType + " " + projectData.LDARDatabaseVersion;
            labelLDARServer.Text = projectData.LDARDatabaseServer + "/" + projectData.LDARDatabaseName;
            labelLDARSynchronized.Text = projectData.LDARDatabaseLastRefreshed == null ? "<never>" : projectData.LDARDatabaseLastRefreshed.ToString();
            //});

            listViewLDARCounts.Items.Clear();
            listViewLDARCounts.Items.Add("Tagged Components: " + projectData.LDARData.ExistingComponents.Count().ToString());
            listViewLDARCounts.Items.Add("Component Class/Type: " + projectData.LDARData.ComponentClassTypes.Count().ToString());
            listViewLDARCounts.Items.Add("Streams: " + projectData.LDARData.ComponentStreams.Count().ToString());
            listViewLDARCounts.Items.Add("Chemical States: " + projectData.LDARData.ChemicalStates.Count().ToString());
            listViewLDARCounts.Items.Add("Pressure Services: " + projectData.LDARData.PressureServices.Count().ToString());
            listViewLDARCounts.Items.Add("Locaton Plants: " + projectData.LDARData.LocationPlants.Count().ToString());
            listViewLDARCounts.Items.Add("Process Units: " + projectData.LDARData.ProcessUnits.Count().ToString());
            listViewLDARCounts.Items.Add("Technicians: " + projectData.LDARData.Technicians.Count().ToString());

            labelCADPackageFilename.Text = String.Join(",", projectData.CADPackages.Select(p => Path.GetFileName(p.FileName)).ToArray());
            labelCADPackageCount.Text = projectData.CADPackages.Count.ToString();
            labelCADPackageDrawingCount.Text = projectData.CADPackages.Sum(p => p.PageCount).ToString();
            labelCADPackagePublishedBy.Text = String.Join(",", projectData.CADPackages.Select(p => Path.GetFileName(p.SourceProductName + " " + p.SourceProductVersion)).Distinct().ToArray()); 
            labelCADPackageLastLoaded.Text = projectData.CADDrawingPackageLastRefreshed.ToString();

            //incoming sets
            labelLoadedSets.Text = projectTags.Where(t=>t.Exported == false).Count().ToString();
            int totalComponents = 0;
            foreach(ProjectTags tags in projectTags.Where(t=>t.Exported == false))
            {
                if (tags.Tags != null) totalComponents += tags.getAllAsTaggedComponent().Count();
            }
            labelTotalComponents.Text = totalComponents.ToString();
            int totalImages = 0;

            //TODO: Had to remove Images due to serialization issues

            //foreach (ProjectTags tags in projectTags.Where(t=>t.Exported == false))
            //{
            //    if (tags.Images != null)
            //    {
            //        totalImages += tags.Images.Count();
            //    }
            //}
            labelTotalImages.Text = totalImages.ToString();

            //QA/QC
            labelPassedComponents.Text = "0";
            ColorizeLabel(labelPassedComponents, totalComponents, Color.Green);

            labelFailedComponents.Text = "0";
            ColorizeLabel(labelFailedComponents, 0, Color.Red);

            //Exported sets
            labelExportedSets.Text = projectTags.Where(t=>t.Exported == true).Count().ToString();
            int totalExportedComponents = 0;
            foreach (ProjectTags tags in projectTags.Where(t => t.Exported == true))
            {
                totalExportedComponents += tags.Tags.Count();
            }
            labelExportedComponents.Text = totalExportedComponents.ToString();
            int totalExportedImages = 0;

            //TODO: Had to remove Images due to serialization issues

            //foreach (ProjectTags tags in projectTags.Where(t => t.Exported == true))
            //{
            //    if (tags.Images != null)
            //    {
            //        totalExportedImages += tags.Images.Count();
            //    }
            //}
            labelExportedImages.Text = totalExportedImages.ToString();

            labelStagedComponents.Text = MainForm.CollectedTags.Count().ToString();

            return true;
        }

        public void ColorizeLabel(Label label, int value, Color color)
        {
            if (value > 0)
            {
                label.ForeColor = color;
            }
            else
            {
                label.ForeColor = Color.Black;
            }
        }

        public bool SyncProjectDatabase(ProjectData projectData, BackgroundWorker worker)
        {
            worker.ReportProgress(10, "Loading LDAR Database Properties");

            System.Threading.Thread.Sleep(500);
            worker.ReportProgress(20, "Loading Existing LDAR Tags");

            System.Threading.Thread.Sleep(500);
            worker.ReportProgress(40, "Loading LDAR Streams");

            System.Threading.Thread.Sleep(500);
            worker.ReportProgress(60, "Loading LDAR Component Classs/Types");

            System.Threading.Thread.Sleep(500);
            worker.ReportProgress(80, "Loading LDAR DTM/UTM");

            System.Threading.Thread.Sleep(500);
            worker.ReportProgress(100, "Loading LDAR Components");

            return true;
        }

        private void buttonLDARSync_Click(object sender, EventArgs e)
        {

            _mainForm.RefreshProject(true, false);
            
        }

        private void buttonCADPackageSync_Click(object sender, EventArgs e)
        {
            _mainForm.RefreshCADPackage();
        }

        private void buttonRefreshQAQC_Click(object sender, EventArgs e)
        {
            _mainForm.RefreshQAQC(true);
            setLabelColors(); 
        }

        private void setLabelColors()
        {
            int failedComps = 0;
            int warnings = 0;
            if (int.TryParse(labelFailedComponents.Text, out failedComps))
            {
                ColorizeLabel(labelFailedComponents, failedComps, Color.Red);
            }
            if (int.TryParse(labelWarnings.Text, out warnings))
            {
                ColorizeLabel(labelWarnings, warnings, Color.Orange);
            }
        }

        public string getErrors()
        {
            return labelFailedComponents.Text;
        }

        public string getWarnings()
        {
            return labelWarnings.Text;
        }
    }
}
