using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Manager.Dialogs;
using EnvInt.Win32.FieldTech.Common;

namespace EnvInt.Win32.FieldTech.Manager
{
    public partial class ConfigurationControl : UserControl
    {
        
        public ConfigurationControl()
        {
            InitializeComponent();

        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            MainForm.CurrentProjectData.Configurations.Remove((ProjectConfiguration)(objectListViewConfigurations.SelectedObject));
            objectListViewConfigurations.SetObjects(MainForm.CurrentProjectData.Configurations);
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {

            ConfigurationEditor ce = new ConfigurationEditor();

            ProjectConfiguration newConfig = new ProjectConfiguration();

            newConfig.setDefaultFieldConfiguration();

            ce.setConfiguration(newConfig);

            ce.ShowDialog();

            if (ce.DialogResult == DialogResult.OK) MainForm.CurrentProjectData.Configurations.Add(ce._currentConfiguration);

            objectListViewConfigurations.SetObjects(MainForm.CurrentProjectData.Configurations);
                        
        }

        private void ConfigurationControl_Load(object sender, EventArgs e)
        {

            if (MainForm.CurrentProjectData != null)
            {
                objectListViewConfigurations.SetObjects(MainForm.CurrentProjectData.Configurations);
            }

        }

        private void objectListViewConfigurations_ItemActivate(object sender, EventArgs e)
        {

            ConfigurationEditor ce = new ConfigurationEditor();

            ce.setConfiguration((ProjectConfiguration)objectListViewConfigurations.SelectedObject);

            ce.ShowDialog();

            if (ce.DialogResult == DialogResult.OK) objectListViewConfigurations.SetObjects(MainForm.CurrentProjectData.Configurations);

        }

        private void toolStripButtonLoad_Click(object sender, EventArgs e)
        {

            OpenFileDialog sd = new OpenFileDialog();
            sd.Filter = "Configuration JSON (*.json)|*.json|All files (*.*)|*.*";
            DialogResult dr = sd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                try
                {
                    string confData = File.ReadAllText(sd.FileName);
                    ProjectConfiguration conf = FileUtilities.DeserializeObject<ProjectConfiguration>(confData);
                    MainForm.CurrentProjectData.Configurations.Add(conf);
                    objectListViewConfigurations.SetObjects(MainForm.CurrentProjectData.Configurations);
                }
                catch
                {
                    MessageBox.Show("File is already in use or open in another application. Close the file and then try again.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {

            if (objectListViewConfigurations.SelectedObject == null)
            {
                MessageBox.Show("Please select a configuration to export");
                return;
            }

            ProjectConfiguration conf = (ProjectConfiguration)objectListViewConfigurations.SelectedObject;

            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "Configuration JSON (*.json)|*.json|All files (*.*)|*.*";
            sd.FileName = conf.Name + "_Config.json";
            DialogResult dr = sd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string confData = FileUtilities.SerializeObject<ProjectConfiguration>(conf);
                try
                {
                    File.WriteAllText(sd.FileName, confData, Encoding.UTF8);
                }
                catch
                {
                    MessageBox.Show("File is already in use or open in another application. Close the file and then try again.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripButtonSetDefault_Click(object sender, EventArgs e)
        {

            if (objectListViewConfigurations.SelectedObjects.Count < 1)
            {
                MessageBox.Show("Please select a configuration");
                return;
            }
            
            foreach (ProjectConfiguration pc in MainForm.CurrentProjectData.Configurations)
            {
                pc.Default = false; 
            }

            ((ProjectConfiguration)objectListViewConfigurations.SelectedObject).Default = true;

            objectListViewConfigurations.SetObjects(MainForm.CurrentProjectData.Configurations);
            
        }
    }
}
