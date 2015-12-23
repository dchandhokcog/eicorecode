using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class ConfigurationEditor : Form
    {

        public ProjectConfiguration _currentConfiguration = new ProjectConfiguration();

        public ConfigurationEditor()
        {
            InitializeComponent();
            
        }

        public void setConfiguration(ProjectConfiguration conf)
        {
            _currentConfiguration = conf;
            
            //create a new field configuration if necessary
            if (_currentConfiguration.FieldConfigurations == null) newFieldConfiguration();
            else if (_currentConfiguration.FieldConfigurations.Count == 0) newFieldConfiguration();
            
            //set elements of form
            radGridViewFieldConfig.DataSource = _currentConfiguration.FieldConfigurations;
            textBoxName.Text = conf.Name;
            textBoxNotes.Text = conf.Notes;
            textBoxTargetTable.Text = conf.DocumentingTable;
            textBoxTargetTableID.Text = conf.MainIndexField;
            checkBoxAllowInspections.Checked = conf.AllowInspections;
        }

        public void newFieldConfiguration()
        {
            TaggedComponent tc = new TaggedComponent();
            List<string> fields = tc.getHeaderAsList();

            if (_currentConfiguration.FieldConfigurations == null) _currentConfiguration.setDefaultFieldConfiguration();
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            _currentConfiguration.Name = textBoxName.Text;
            _currentConfiguration.Notes = textBoxNotes.Text;
            _currentConfiguration.AllowInspections = checkBoxAllowInspections.Checked;
            _currentConfiguration.DocumentingTable = textBoxTargetTable.Text;
            _currentConfiguration.MainIndexField = textBoxTargetTableID.Text;
            
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void ConfigurationEditor_Activated(object sender, EventArgs e)
        {
            radGridViewFieldConfig.BestFitColumns();
        }

    }
}
