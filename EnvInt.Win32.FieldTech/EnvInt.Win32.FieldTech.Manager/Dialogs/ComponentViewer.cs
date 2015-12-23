using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Manager.Dialogs;

using Telerik.WinControls.Data;
using Telerik.Collections;
using Telerik.WinControls;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class ComponentViewer : Form
    {

        List<TaggedComponent> _projectTags = new List<TaggedComponent>();
        MainForm _mainForm;
        
        public ComponentViewer(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        public void loadProjectData(List<TaggedComponent> projectTags)
        {
            
           if (projectTags != null)
            {
                _projectTags = projectTags;
                radGridViewComponents.AutoGenerateColumns = true;
                radGridViewComponents.DataSource = _projectTags;
                try 
                {
                    //if (!this.Text.Contains("Duplicate ID")) radGridViewComponents.Columns["Id"].IsVisible = false;
                    radGridViewComponents.Columns["Id"].ReadOnly = true;
                    radGridViewComponents.Columns["Children"].IsVisible = false;
                    //radGridViewComponents.Columns["ExtraFields"].IsVisible = false;
                }
                catch { }
                radGridViewComponents.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells);
            }
        }

        private void radGridViewComponents_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButtonExportEid_Click(object sender, EventArgs e)
        {

            if (radGridViewComponents.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }
            
            List<TaggedComponent> filteredTcList = new List<TaggedComponent>();
            
            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewComponents.SelectedRows)
            {
                filteredTcList.Add((TaggedComponent)tc.DataBoundItem);
            }
                        
            if (MainForm.CurrentProjectData == null)
            {
                MessageBox.Show("There is no project data available.", "Project Data Export", MessageBoxButtons.OK);
            }
            else
            {
                ExportFieldConfigurationDialog ecd = new ExportFieldConfigurationDialog(MainForm.CurrentProjectData);
                DialogResult dr = ecd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {

                    MainForm.CurrentProjectData.DefaultDrawing = ecd.DefaultDrawingPackage;

                    string exportFile = ecd.ExportFileName;
                    List<string> selectedDrawingPackages = new List<string>();
                    selectedDrawingPackages.Add(ecd.DefaultDrawingPackage);
                    if (ecd.DrawingPackagesAdditional.Count > 0) selectedDrawingPackages.AddRange(ecd.DrawingPackagesAdditional);
                    List<string> selectedLDARProcessUnits = ecd.SelectedLDARProcessUnits;

                    _mainForm.SaveProjectFileAs(exportFile, selectedDrawingPackages, selectedLDARProcessUnits, true, filteredTcList.Where(c => !c.isChild).ToList());
                }
            }

        }

        private void toolStripButtonNearMatchxRef_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("This will attempt to find near xRef Matches, this is only for troubleshooting and will not modify tag values", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;
            //todo: near matching stuff
            
            DiffMatchPatch.diff_match_patch dmp = new DiffMatchPatch.diff_match_patch();

            int minDiffValue = 100000;
            int currentDiffValue = 0;
            List<KeyValuePair<int, LDARComponent>> candidates = new List<KeyValuePair<int, LDARComponent>>();

            List<TaggedComponent> filteredTcList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewComponents.SelectedRows)
            {
                filteredTcList.Add((TaggedComponent)tc.DataBoundItem);
            }
                        
            foreach (TaggedComponent tc in filteredTcList)
            {
                minDiffValue = 100000;
                //string compClass = string.Empty;
                //if (tc.ComponentType.Contains(" - "))
                //{
                //    compClass = tc.ComponentType.Split('-')[0].Trim().ToUpper();
                //    if (MainForm.CurrentProjectData.LDARDatabaseType == "LeakDAS")
                //    {
                //        compClassId = LeakDAS.GetComponentClassTypes
                //    }
                //    else
                //    {

                //    }
                //}
                //else
                //{
                //    compClass = tc.ComponentType;
                //}
                foreach (LDARComponent comp in MainForm.CurrentProjectData.LDARData.ExistingComponents)
                {
                    List<DiffMatchPatch.Diff> nextDiff = dmp.diff_main(tc.PreviousTag, comp.ComponentTag);
                    currentDiffValue = dmp.diff_levenshtein(nextDiff);
                    if (currentDiffValue < minDiffValue && currentDiffValue < 5) 
                    {
                        minDiffValue = currentDiffValue;
                        tc.WarningMessage = comp.Id.ToString();
                        tc.ErrorMessage = currentDiffValue.ToString();
                        candidates.Add(new KeyValuePair<int, LDARComponent>(currentDiffValue, comp));
                    }
                }
                if (candidates.Count > 1)
                {
                    foreach (KeyValuePair<int, LDARComponent> candidate2 in candidates.OrderByDescending(c => c.Key))
                    {
                        if (!string.IsNullOrEmpty(tc.Location) && !string.IsNullOrEmpty(candidate2.Value.LocationDescription))
                        {
                            List<DiffMatchPatch.Diff> nextDiff2 = dmp.diff_main(tc.Location, candidate2.Value.LocationDescription);
                            currentDiffValue = dmp.diff_levenshtein(nextDiff2);
                            if (currentDiffValue < minDiffValue && currentDiffValue < 20)
                            {
                                minDiffValue = currentDiffValue;
                                tc.WarningMessage = candidate2.Value.Id.ToString();
                                tc.ErrorMessage = currentDiffValue.ToString();
                            }
                        }
                    }
                }
            }

            radGridViewComponents.MasterTemplate.Refresh(null);
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewComponents_UserAddedRow(object sender, Telerik.WinControls.UI.GridViewRowEventArgs e)
        {

            string ptTarget = string.Empty;

            if (this.Text.Contains(" - "))
            {
                ptTarget = this.Text.Split('-')[1].Trim();
            }

            bool addPtToProject = false;
            
            ProjectTags currentPt = MainForm.CurrentProjectTags.Where(n => n.Device == ptTarget).FirstOrDefault();

            if (currentPt == null)
            {
                currentPt = MainForm.CurrentProjectTags.Where(n => n.Device == "UserAdded").FirstOrDefault();
                if (currentPt == null)
                {
                    currentPt = new ProjectTags();
                    currentPt.setDefaults();
                    currentPt.Device = "UserAdded";
                    addPtToProject = true;
                }
            }

            try
            {
                TaggedComponent newTag = (TaggedComponent)e.Row.DataBoundItem;
                newTag.Children = new List<ChildComponent>();
                currentPt.Tags.Add(newTag);
            }
            catch { }

            if (addPtToProject)
            {
                MainForm.CurrentProjectTags.Add(currentPt);
            }

            MainForm.CurrentProjectDirty = true;


        }

        private void radGridViewComponents_UserAddingRow(object sender, Telerik.WinControls.UI.GridViewRowCancelEventArgs e)
        {
            try
            {
                e.Rows[0].Cells["Id"].Value = Guid.NewGuid().ToString();
            }
            catch { }
        }

        private void toolStripButtonMassEdit_Click(object sender, EventArgs e)
        {

            string errorMessage = string.Empty;
            TaggedComponent tempTc = new TaggedComponent();
            string fieldToChange = string.Empty;
            string changeValue = string.Empty;
            
            if (radGridViewComponents.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }

            Globals.InputBox("Field Selection", "Please enter the field you wish to change", ref fieldToChange);

            if (!tempTc.getHeaderAsList().Contains(fieldToChange))
            {
                MessageBox.Show("A valid field is required");
                return;
            }

            Globals.InputBox("Value Selection", "Please enter the value you wish to change all items to", ref changeValue);

            DialogResult confirmBoxResult = MessageBox.Show("This will change the values for your selected records, are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmBoxResult == System.Windows.Forms.DialogResult.No) return;

            if (string.IsNullOrEmpty(changeValue)) return;

            List<TaggedComponent> filteredTcList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewComponents.SelectedRows)
            {
                filteredTcList.Add((TaggedComponent)tc.DataBoundItem);
            }

            if (MainForm.CurrentProjectData == null)
            {
                MessageBox.Show("There is no project data available.", "Project Data Export", MessageBoxButtons.OK);
            }
            else
            {
                foreach (TaggedComponent component in filteredTcList)
                {
                    PropertyInfo prop = component.GetType().GetProperty(fieldToChange, BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        errorMessage = component.setValueByName(fieldToChange, changeValue);
                        if (errorMessage != string.Empty)
                        {
                            MessageBox.Show(errorMessage);
                            return;
                        }
                    }
                }
            radGridViewComponents.MasterTemplate.Refresh(null);
            MainForm.CurrentProjectDirty = true;
            }
        }

        private void radGridViewComponents_UserDeletedRow(object sender, Telerik.WinControls.UI.GridViewRowEventArgs e)
        {
            radGridViewComponents.MasterTemplate.Refresh(null);
            MainForm.CurrentProjectDirty = true;     
        }

        private void radGridViewComponents_UserDeletingRow(object sender, Telerik.WinControls.UI.GridViewRowCancelEventArgs e)
        {

            DialogResult dr = MessageBox.Show("Are you sure you want to delete " + e.Rows.Count().ToString() + " rows?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.No) return;
            
            foreach( Telerik.WinControls.UI.GridViewRowInfo row in e.Rows )
            {
                foreach (ProjectTags pt in MainForm.CurrentProjectTags)
                {
                    pt.Tags.Remove((TaggedComponent)row.DataBoundItem);
                }
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
           DialogResult dr = MessageBox.Show("Are you sure you want to delete " + radGridViewComponents.SelectedRows.Count().ToString() + " rows?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           if (dr == System.Windows.Forms.DialogResult.No) return;
            
           foreach (Telerik.WinControls.UI.GridViewRowInfo row in radGridViewComponents.SelectedRows)
           {
               foreach (ProjectTags pt in MainForm.CurrentProjectTags)
               {
                   pt.Tags.Remove((TaggedComponent)row.DataBoundItem);
               }
           }
           radGridViewComponents.DataSource = null;
           radGridViewComponents.DataSource = _projectTags;
           radGridViewComponents.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.AllCells);
           Cursor.Current = Cursors.Arrow;
        }

        private void toolStripButtonMove_Click(object sender, EventArgs e)
        {
            CopyTaggedComponent copyForm = new CopyTaggedComponent();
            string copyToSet = string.Empty;

            copyForm.ShowDialog();

            if (copyForm.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;

            ProjectTags pt;

            if (copyForm.selectedDataSet == "[New Set]")
            {
                Globals.InputBox("Please enter the name of the new set", "Name", ref copyToSet);
                ProjectTags newPt = new ProjectTags();
                newPt.setDefaults();
                newPt.Device = copyToSet;
                pt = newPt;
                MainForm.CurrentProjectTags.Add(pt);
            }
            else
            {
                copyToSet = copyForm.selectedDataSet;
                 pt = MainForm.CurrentProjectTags.Where(c => c.Device == copyForm.selectedDataSet).FirstOrDefault();
            }

            if (pt == null) return;

            Cursor.Current = Cursors.WaitCursor;
            
            if (copyForm.CopyOperation)
            {
                List<TaggedComponent> copyList = new List<TaggedComponent>();
                foreach (Telerik.WinControls.UI.GridViewRowInfo row in radGridViewComponents.SelectedRows.Where(c => !c.Cells["Id"].Value.ToString().StartsWith("ChildComponent")))
                {
                    copyList.Add((TaggedComponent)row.DataBoundItem);
                }
                foreach (TaggedComponent tc in copyList)
                {
                    tc.Id = Guid.NewGuid().ToString();
                    tc.LDARTag = tc.LDARTag + "x";
                }
                pt.Tags.AddRange(copyList);
            }
            else
            {
                List<TaggedComponent> copyList = new List<TaggedComponent>();
                foreach (Telerik.WinControls.UI.GridViewRowInfo row in radGridViewComponents.SelectedRows.Where(c => !c.Cells["Id"].Value.ToString().StartsWith("ChildComponent")))
                {
                    copyList.Add((TaggedComponent)row.DataBoundItem);
                    foreach( ProjectTags oldPt in MainForm.CurrentProjectTags.Where(a => a.Device != copyForm.selectedDataSet))
                    {
                        oldPt.Tags.Remove((TaggedComponent)row.DataBoundItem);
                    }
                }
                pt.Tags.AddRange(copyList);
            }

            radGridViewComponents.DataSource = null;
            radGridViewComponents.DataSource = _projectTags;
            radGridViewComponents.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.AllCells);
            Cursor.Current = Cursors.Arrow;
        }

        private void toolStripButtonFindReplace_Click(object sender, EventArgs e)
        {

            string errorMessage = string.Empty;
            TaggedComponent tempTc = new TaggedComponent();
            string fieldToChange = string.Empty;
            string findValue = string.Empty;
            string changeValue = string.Empty;

            if (radGridViewComponents.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }

            Globals.InputBox("Field Selection", "Please enter the field you wish to change", ref fieldToChange);

            if (!tempTc.getHeaderAsList().Contains(fieldToChange))
            {
                MessageBox.Show("A valid field is required");
                return;
            }

            Globals.InputBox("Find Selection", "Please enter the value you wish to find", ref findValue);

            Globals.InputBox("Value Selection", "Please enter the value you wish to replace found items with", ref changeValue);

            DialogResult confirmBoxResult = MessageBox.Show("This will find/replace in the specified field for all selected records, are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmBoxResult == System.Windows.Forms.DialogResult.No) return;

            if (string.IsNullOrEmpty(changeValue)) return;

            List<TaggedComponent> filteredTcList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewComponents.SelectedRows)
            {
                filteredTcList.Add((TaggedComponent)tc.DataBoundItem);
            }

            if (MainForm.CurrentProjectData == null)
            {
                MessageBox.Show("There is no project data available.", "Project Data Export", MessageBoxButtons.OK);
            }
            else
            {
                foreach (TaggedComponent component in filteredTcList)
                {
                    PropertyInfo prop = component.GetType().GetProperty(fieldToChange, BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        errorMessage = component.setValueByName(fieldToChange, component.getValueByName(fieldToChange).Replace(findValue, changeValue));
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            MessageBox.Show(errorMessage);
                            return;
                        }
                    }
                }
            }

            radGridViewComponents.MasterTemplate.Refresh(null);
            MainForm.CurrentProjectDirty = true;

        }

        private void toolStripButtonExportExcel_Click(object sender, EventArgs e)
        {
            
            if (radGridViewComponents.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }

            List<TaggedComponent> exportList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewComponents.SelectedRows)
            {
                exportList.Add((TaggedComponent)tc.DataBoundItem);
            }

            string csvFileName = "Saved_Components";

            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.Filter = "Excel Files (*.xlsx|*.xlsx|Comma Separated Value Files (*.csv|*.csv|All files (*.*)|*.*";
            sd.FileName = csvFileName;
            DialogResult dr = sd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (Path.GetExtension(sd.FileName) == ".csv")
                {
                    string csvData = FileUtilities.GetTaggedComponentsAsCSV(exportList);
                    try
                    {
                        File.WriteAllText(sd.FileName, csvData, Encoding.UTF8);
                        System.Diagnostics.Process.Start(sd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("File is already in use or open in another application. Close the file and then try again.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    string errorMessage = FileUtilities.SendTaggedComponentsToExcel(exportList, sd.FileName);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        MessageBox.Show(errorMessage);
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(sd.FileName);
                        }
                        catch { }
                    }
                }
            }
        }

    }
}
