using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.HelperClasses;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
        
    public partial class DrawingPackagesControl : UserControl
    {
        private MainForm _mainForm = null;

        public DrawingPackagesControl()
        {
            InitializeComponent();
        }

        public bool LoadProject(MainForm mainform)
        {
            _mainForm = mainform;
            UpdateUI(MainForm.CurrentProjectData);
            return true;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            _mainForm.RefreshCADPackage();
            UpdateUI(MainForm.CurrentProjectData);
        }

        private void UpdateUI(ProjectData projectData)
        {
            objectListViewDrawingPackages.SetObjects(MainForm.CurrentProjectData.CADPackages);
        }

        private void toolStripButtonAddDrawing_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Filter = "All files (*.*)|*.*|Drawing Files (*.dwf)|*.dwf|PDF Files (*.pdf)|*.pdf";
            List<string> fileNameList = new List<string>();
            string newFileName = string.Empty;
            
            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    foreach (string fName in fd.FileNames)
                    {
                        if (fileNameList.Contains(Path.GetFileName(fName)))
                        {
                            newFileName = Path.GetFileNameWithoutExtension(fName) + "_" + fileNameList.Select(c => c.Contains(Path.GetFileName(fName))).Count().ToString() + Path.GetExtension(fName);
                            fileNameList.Add(newFileName);
                        }
                        else
                        {
                            fileNameList.Add(Path.GetFileName(fName));
                            newFileName = Path.GetFileName(fName);
                        }
                        CADPackage package = CADPackageFactory.LoadFromFile(fName, false, newFileName);
                        if (package.PackageValid)
                        {
                            MainForm.CurrentProjectData.CADPackages.Add(package);
                            //copy to our working directory
                            System.IO.File.Copy(fName, Globals.WorkingFolder + "\\" + newFileName);
                            MainForm.CurrentProjectDirty = true;
                        }
                        else
                        {
                            MessageBox.Show("Drawing Package Error: " + package.ValidationError);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                objectListViewDrawingPackages.SetObjects(MainForm.CurrentProjectData.CADPackages);
            }
        }

        private void toolStripButtonRemoveDrawing_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("This will remove the drawing(s) from this project, would you like to delete the file also?", "Delete File?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.Cancel) return;
            
            if (objectListViewDrawingPackages.SelectedObjects.Count == 0) return;

            foreach (object selectedDwg in objectListViewDrawingPackages.SelectedObjects)
            {
                CADPackage pkg = (CADPackage)selectedDwg;
                string fName = string.Empty;
                //if we've declared a separate local file name, use that instead
                if (pkg.LocalName == "" || pkg.LocalName == null)
                {
                    fName = Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(pkg.FileName);
                }
                else
                {
                    fName = Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(pkg.LocalName); 
                }
                MainForm.CurrentProjectData.CADPackages.Remove(pkg);
                try
                {
                    if (dr == DialogResult.Yes)
                    {
                        System.IO.File.Delete(fName);
                    }
                }
                catch { }
            }

            UpdateUI(MainForm.CurrentProjectData);
            MainForm.CurrentProjectDirty = true;
        }

        private void objectListViewDrawingPackages_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                CADPackage pkg = (CADPackage)objectListViewDrawingPackages.SelectedObject;
                if (pkg.LocalName != "")
                {
                    System.Diagnostics.Process.Start(Globals.WorkingFolder + "\\" + pkg.LocalName);
                }
                else
                {
                    System.Diagnostics.Process.Start(pkg.FileName);
                }
            }
            catch { }
        }

    }

}
