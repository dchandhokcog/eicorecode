using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.HelperClasses;
using EnvInt.Win32.FieldTech.Common;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    public partial class MarkedDrawingsControl : UserControl
    {
        public MarkedDrawingsControl()
        {
            InitializeComponent();
        }

                private MainForm _mainForm = null;

        public bool LoadProject(MainForm mainform)
        {
            _mainForm = mainform;
            UpdateUI(MainForm.CurrentProjectData);
            return true;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            UpdateUI(MainForm.CurrentProjectData);
        }

        private void UpdateUI(ProjectData projectData)
        {
            objectListViewDrawingPackages.SetObjects(MainForm.CurrentProjectData.MarkedDrawings);
        }

        private void objectListViewDrawingPackages_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                CADPackage pkg = (CADPackage)objectListViewDrawingPackages.SelectedObject;
                System.Diagnostics.Process.Start(Globals.WorkingFolder + "\\MarkedDrawings\\" + pkg.FileName);
            }
            catch { }
        }

        private void toolStripButtonSaveDrawingsToFolder_Click(object sender, EventArgs e)
        {

            if (objectListViewDrawingPackages.SelectedObjects.Count == 0)
            {
                MessageBox.Show("Please select at least one item to save");
                return;
            }
            
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                bool overwritewarning = false;
                foreach (CADPackage cp in objectListViewDrawingPackages.SelectedObjects)
                {
                    if (File.Exists(fd.SelectedPath + "\\" + System.IO.Path.GetFileName(cp.FileName))) overwritewarning = true;
                }

                if (overwritewarning)
                {
                    DialogResult overwriteresult = MessageBox.Show("File(s) already exist in selected location, overwrite?", "Overwrite?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (overwriteresult == DialogResult.No)
                    {
                        MessageBox.Show("Action cancelled.");
                        return;
                    }
                }

                try
                {
                    foreach (CADPackage cp in objectListViewDrawingPackages.SelectedObjects)
                    {
                        System.IO.File.Copy(Globals.WorkingFolder + "\\MarkedDrawings\\" + cp.FileName, fd.SelectedPath + "\\" + System.IO.Path.GetFileName(cp.FileName), true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't save files: " + ex.Message);
                    return;
                }
            }

            MessageBox.Show("Done!");
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            if (objectListViewDrawingPackages.SelectedObjects.Count == 0) return;

            foreach (CADPackage pkg in objectListViewDrawingPackages.SelectedObjects)
            {
                string fName = Globals.WorkingFolder + "\\MarkedDrawings\\" + pkg.FileName;
                MainForm.CurrentProjectData.MarkedDrawings.Remove(pkg);
                try
                {
                    System.IO.File.Delete(fName);
                }
                catch { } 
            }
            UpdateUI(MainForm.CurrentProjectData);
            MainForm.CurrentProjectDirty = true;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectListViewDrawingPackages.SelectedObjects.Count != 1)
            {
                MessageBox.Show("Please select one item to rename");
                return;
            }

            foreach (CADPackage pkg in objectListViewDrawingPackages.SelectedObjects)
            {

                string changeFName = pkg.FileName;

                DialogResult dr = Globals.InputBox("Rename file", "Please enter the new name", ref changeFName);

                if (dr == DialogResult.Cancel) return;

                string fName = Globals.WorkingFolder + "\\MarkedDrawings\\" + pkg.FileName;

                changeFName = Path.GetFileNameWithoutExtension(changeFName) + ".dwf";

                try
                {
                    System.IO.File.Move(Globals.WorkingFolder + "\\MarkedDrawings\\" + pkg.FileName, Globals.WorkingFolder + "\\MarkedDrawings\\" + changeFName);
                    pkg.FileName = changeFName;
                }
                catch { }
            }

            objectListViewDrawingPackages.SetObjects(MainForm.CurrentProjectData.MarkedDrawings);

            UpdateUI(MainForm.CurrentProjectData);
            MainForm.CurrentProjectDirty = true;
        }
    }
}
