using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class ExportFieldConfigurationDialog : Form
    {
        private ProjectData _projectData = null;
        private List<string> _drawingPackages = new List<string>();

        public string ExportFileName
        {
            get { return textBoxExportFile.Text; }
        }

        public List<string> DrawingPackagesAdditional
        {
            get { return _drawingPackages; }
        }

        public string DefaultDrawingPackage
        {
            get { return comboBoxDefaultDrawing.SelectedItem.ToString(); }
        }

        public List<string> SelectedLDARProcessUnits
        {
            get
            {
                //firstly, if all items are checked, then just don't filter, return NULL
                if (checkedListBoxProcessUnits.Items.Count == checkedListBoxProcessUnits.CheckedItems.Count)
                {
                    return null;
                }
                return checkedListBoxProcessUnits.CheckedItems.Cast<string>().ToList();
            }
        }

        public ExportFieldConfigurationDialog(ProjectData projectData)
        {
            InitializeComponent();
            _projectData = projectData;

            foreach (CADPackage package in _projectData.CADPackages)
            {
                if (package.LocalName == "" || package.LocalName == null)
                {
                    checkedListBoxDrawings.Items.Add(Path.GetFileName(package.FileName));
                    comboBoxDefaultDrawing.Items.Add(Path.GetFileName(package.FileName));
                }
                else
                {
                    checkedListBoxDrawings.Items.Add(Path.GetFileName(package.LocalName));
                    comboBoxDefaultDrawing.Items.Add(Path.GetFileName(package.LocalName));
                }
            }

            try
            {
                comboBoxDefaultDrawing.SelectedIndex = 0;
            }
            catch { }

            foreach (LDARProcessUnit unit in _projectData.LDARData.ProcessUnits.OrderBy(c => c.UnitDescription))
            {
                checkedListBoxProcessUnits.Items.Add(unit.UnitDescription, true);
            }
        }

        private void buttonBrowseFTTD_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Toolbox Data Files (*.eid)|*.eid|All files (*.*)|*.*";
            fd.FilterIndex = 1;
            fd.FileName = _projectData.ProjectName + " Export";
            fd.OverwritePrompt = true;

            DialogResult dr = fd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBoxExportFile.Text = fd.FileName;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            _drawingPackages.Clear();

            if (comboBoxDefaultDrawing.SelectedItem == null)
            {
                MessageBox.Show("Please select a default drawing for this export.", "Export Drawing Error", MessageBoxButtons.OK);
                return;
            }

            
            //validate path
            if (!Directory.Exists(Path.GetDirectoryName(textBoxExportFile.Text)))
            {
                MessageBox.Show("Export file path appears to be invalid. Rebrowse to the location you would like to export to.", "Export Path Error", MessageBoxButtons.OK);
                return;
            }

            //validate drawing
            if (_projectData.CADPackages.Where(p => Path.GetFileName(p.FileName) == comboBoxDefaultDrawing.SelectedItem.ToString()).FirstOrDefault() == null)
            {
                if (_projectData.CADPackages.Where(p => Path.GetFileName(p.LocalName) == comboBoxDefaultDrawing.SelectedItem.ToString()).FirstOrDefault() == null)
                {
                    MessageBox.Show("The selected default drawing package cannot be found in the project. Please reselect the drawing you wish to export.", "Export Package Error", MessageBoxButtons.OK);
                    return;
                }
            }

            //validate additional drawings
            foreach( object itm in checkedListBoxDrawings.CheckedItems)
            {
                if (_projectData.CADPackages.Where(p => Path.GetFileName(p.FileName) == itm.ToString()) != null)
                {
                    if (itm.ToString() != comboBoxDefaultDrawing.SelectedText) _drawingPackages.Add(itm.ToString());
                }
                else
                {
                    if (_projectData.CADPackages.Where(p => Path.GetFileName(p.LocalName) == itm.ToString()) != null)
                    {
                        MessageBox.Show("The selected additional drawing package(s) cannot be found in the project. Please reselect the drawing(s) you wish to export.", "Export Package Error", MessageBoxButtons.OK);
                        return;
                    }
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void checkBoxCheckAllNone_CheckedChanged(object sender, EventArgs e)
        {
            if (checkedListBoxProcessUnits.Items != null)
            {
                for (int x = 0; x < checkedListBoxProcessUnits.Items.Count; x++)
                {
                    checkedListBoxProcessUnits.SetItemChecked(x, checkBoxCheckAllNone.Checked);
                }
            }
        }

        private void checkBoxCheckAllDrawings_CheckedChanged(object sender, EventArgs e)
        {
            if (checkedListBoxDrawings.Items != null)
            {
                for (int x = 0; x < checkedListBoxDrawings.Items.Count; x++)
                {
                    checkedListBoxDrawings.SetItemChecked(x, checkBoxCheckAllDrawings.Checked);
                }
            }
        }

    }
}
